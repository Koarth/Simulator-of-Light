using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Models;
using Action = Simulator_of_Light.Simulator.Models.Action;

namespace Simulator_of_Light.Simulator {

    public partial class Battle {

        private void _handleEvent(BattleEvent e) {

            switch (e.Type) {
                // Actor-driven events.
                case BattleEventType.ACTOR_READY:
                    _handleActorReadyEvent(e.Source);
                    break;
                case BattleEventType.USE_ACTION:
                    // Initiate the use of an action.
                    // Generates:
                    //     Begin cast events
                    //     RESOLVE_ACTION
                    //     ACTOR_READY
                    _handleUseActionEvent(e.Source, e.Target, e.Action);
                    break;
                case BattleEventType.RESOLVE_ACTION:
                    // Resolve the effects of an action.
                    // Generates:
                    //     Damage/healing battle events
                    //     Cast battle events
                    //     ACTOR_READY
                    //     APPLY_AURA
                    //     APPLY_AURA_STACK
                    //     EXPIRE_AURA (?)
                    //     EXPIRE_AURA_STACK
                    _handleResolveActionEvent(e.Source, e.Target, e.Action);
                    break;
                case BattleEventType.APPLY_AURA:
                    // Apply an aura to a target.
                    // Generates:
                    //     Aura applied battle events
                    //     Aura refreshed battle events
                    //     EXPIRE_AURA
                    //     APPLY_AURA_STACK
                    _handleApplyAuraEvent(e.Source, e.Target, e.BaseAura);
                    break;
                case BattleEventType.APPLY_AURA_STACK:
                    // Add a stack to an existing aura.
                    // Generates:
                    //     Aura stack applied battle events.
                    //     Aura refreshed battle events.
                    //     EXPIRE_AURA
                    _handleApplyAuraStackEvent();
                    break;
                case BattleEventType.EXPIRE_AURA:
                    // Remove an aura from the target.
                    // Generates:
                    //     Aura expired battle event.
                    _handleExpireAuraEvent(e.Target, e.Aura);
                    break;
                case BattleEventType.REMOVE_AURA_STACK:
                    // Remove a stack from an existing aura.
                    // Generates:
                    //     Aura stack removed battle event.
                    _handleRemoveAuraStackEvent();
                    break;

                // Independent events.
                case BattleEventType.AURA_TICK:
                    // Tick all auras on all targets.
                    // Generates:
                    //     Damage/Healing battle events.
                    //     Refresh battle events.
                    //     AURA_TICK
                    _handleAuraTickEvent();
                    break;
                case BattleEventType.REGEN_TICK:
                    // Tick passive regeneration on all _players_.
                    // Generates:
                    //     HP/MP/TP regeneration battle events.
                    //     REGEN_TICK
                    _handleRegenTickEvent();
                    break;
                case BattleEventType.FIGHT_COMPLETE:
                    // Cleanup; ignore all future events and report all
                    // generated battle events.
                    _handleFightCompleteEvent();
                    break;
            }

        }

        private void _handleActorReadyEvent(IActor source) {
            // Decision should be either ACTOR_READY or RESOLVE_ACTION.
            BattleEvent decision = source.DecideAction(Time, Actors, Enemies);
            EventQueue.Add(decision);
        }

        private void _handleUseActionEvent(IActor source, ITarget target, Action action) {

            // Queue next actor availability.
            long t = source.BeginCast(action, Time);
            EventQueue.Add(new BattleEvent(BattleEventType.ACTOR_READY, t, source));

            // Resolves later if it has a cast time, else resolves now.
            if (action.BaseAction.CastTime > 0) {
                EventQueue.Add(new BattleEvent(
                                BattleEventType.RESOLVE_ACTION,
                                t,
                                source,
                                target,
                                action: action));
                EventLog.Add(new CombatLogEvent(
                                CombatLogEventType.BEGINCAST,
                                Time,
                                source,
                                action: action.BaseAction));
            } else {
                EventQueue.Add(new BattleEvent(
                                BattleEventType.RESOLVE_ACTION,
                                Time,
                                source,
                                target,
                                action: action));
                //EventLog.Add(new CombatLogEvent(CombatLogEventType.CAST, Time, e.Source, action: e.Action.BaseAction));
            }
        }

        private void _handleResolveActionEvent(IActor source, ITarget target, Action action) {
            var a = action.BaseAction;

            ITarget[] targets = getTargetsInRadius(target, a.Radius);
            EventLog.Add(new CombatLogEvent(CombatLogEventType.CAST, Time, source, action: a));

            foreach (ITarget tar in targets) {
                // Calculate and apply damage.
                if (a.Potency > 0) {
                    CombatLogEvent damage = calculateActionDamage(a, source, tar);
                    EventLog.Add(damage);
                }

                // Apply auras.
                if (a.AurasApplied != null) {
                    foreach (BaseAura aura in a.AurasApplied) {
                        if (aura.Targets == AuraTarget.TARGET) {
                            EventQueue.Add(new BattleEvent(
                                            BattleEventType.APPLY_AURA,
                                            Time,
                                            source,
                                            tar,
                                            baseAura: aura));
                        }
                    }
                }
            }

            // Apply self-auras.
            if (a.AurasApplied != null) {
                foreach (BaseAura aura in a.AurasApplied) {
                    if (aura.Targets == AuraTarget.SOURCE) {
                        EventQueue.Add(new BattleEvent(
                                        BattleEventType.APPLY_AURA,
                                        Time,
                                        source,
                                        source,
                                        baseAura: aura));
                    }
                }
            }

            // Resolve action effects on the source actor, and add any
            // resulting events to the queue.
            var newEvents = source.ExecuteAction(action, Time);
            foreach (BattleEvent ev in newEvents) {
                EventQueue.Add(ev);
            }
        }

        private void _handleApplyAuraEvent(IActor source, ITarget target, BaseAura aura) {

            // Apply the aura.
            var appliedAura = ApplyAura(aura, source, target);

            // Log event.
            CombatLogEventType type;
            if (appliedAura.isBuff) {
                type = CombatLogEventType.APPLYBUFF;
            } else {
                type = CombatLogEventType.APPLYDEBUFF;
            }
            EventLog.Add(new CombatLogEvent(type, appliedAura.Expires, appliedAura.Source, target, appliedAura.BaseAura));

            // Add aura expiration event in the future.
            EventQueue.Add(new BattleEvent(BattleEventType.EXPIRE_AURA, 
                            appliedAura.Expires, appliedAura.Source, 
                            target, aura: appliedAura));
        }

        private void _handleApplyAuraStackEvent() {
            throw new NotImplementedException();
        }

        private void _handleExpireAuraEvent(ITarget target, Aura aura) {

            // Expire the aura.
            var expiredAura = ExpireAura(aura, target);

            // The target aura may not be found;  this is normal in the case
            // an aura was refreshed early and therefore overwritten, or otherwise
            // consumed by an action.
            // Just pass over the event.
            if (expiredAura == null) {
                return;
            }

            // Combat log event for expiring the aura.
            CombatLogEventType type;
            if (expiredAura.isBuff) {
                type = CombatLogEventType.REMOVEBUFF;
            } else {
                type = CombatLogEventType.REMOVEDEBUFF;
            }
            EventLog.Add(new CombatLogEvent(type, Time, expiredAura.Source, target, expiredAura.BaseAura));
        }

        private void _handleRemoveAuraStackEvent() {
            throw new NotImplementedException();
        }

        private void _handleAuraTickEvent() {
            throw new NotImplementedException();
        }

        private void _handleRegenTickEvent() {
            throw new NotImplementedException();
        }

        private void _handleFightCompleteEvent() {
            throw new NotImplementedException();
        }

        public CombatLogEvent calculateActionDamage(BaseAction action, IActor source, ITarget target) {

            // Calculate base damage.
            // Roll for critical.
            // Roll for direct hit.

            throw new NotImplementedException();
        }

        public Aura ApplyAura(BaseAura aura, IActor source, ITarget target) {


            throw new NotImplementedException();
        }

        public Aura ExpireAura(Aura aura, ITarget target) {


            throw new NotImplementedException();

        }

        public ITarget[] getTargetsInRadius(ITarget primary, double radius, bool friendly = false) {

            if (primary == null) {
                return new ITarget[] { };
            }

            // One day we'll actually check range, for now simply distinguish
            // between AoE and non-AoE.
            if (radius <= 0) {
                return new ITarget[] { primary };
            }

            if (friendly) {
                return Friendlies;
            }

            return Enemies;
        }

    }
}
