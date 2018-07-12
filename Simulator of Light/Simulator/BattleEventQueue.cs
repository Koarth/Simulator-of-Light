using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C5;

namespace Simulator_of_Light.Simulator {
    public class BattleEventQueue : IntervalHeap<BattleEvent> {

        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public BattleEventQueue() : base() {

        }

        public new bool Add(BattleEvent item) {

            #region Logging
            switch (item.Type) {
                case BattleEventType.ACTOR_READY:
                    _logger.Debug("Event added: Actor {0} ready to act at time {1}", item.Source, item.Time);
                    break;
                case BattleEventType.APPLY_AURA:
                    _logger.Debug("Event added: Apply {0}'s aura {1} to {2}", item.Source, item.BaseAura.Name, item.Target);
                    break;
                case BattleEventType.APPLY_AURA_STACK:
                    _logger.Debug("Event added: Apply a stack to {0}'s aura {1} on {2}", item.Source, item.Aura.BaseAura.Name, item.Target);
                    break;
                case BattleEventType.AURA_TICK:
                    _logger.Debug("Event added: Next aura tick at time {0}", item.Time);
                    break;
                case BattleEventType.EXPIRE_AURA:
                    _logger.Debug("Event added: Expire {0}'s aura {1} on {2}", item.Source, item.Aura.BaseAura.Name, item.Target);
                    break;
                case BattleEventType.FIGHT_COMPLETE:
                    _logger.Debug("Event added: Fight finishes at time {0}", item.Time);
                    break;
                case BattleEventType.REGEN_TICK:
                    _logger.Debug("Event added: Next regen tick at time {0}", item.Time);
                    break;
                case BattleEventType.REMOVE_AURA_STACK:
                    _logger.Debug("Event added: Remove a stack to {0}'s aura {1} on {2}", item.Source, item.Aura.BaseAura.Name, item.Target);
                    break;
                case BattleEventType.RESOLVE_ACTION:
                    _logger.Debug("Event added: Resolve {0}'s {1} on {2} at {3}", item.Source, item.Action.BaseAction.Name, item.Target, item.Time);
                    break;
                case BattleEventType.USE_ACTION:
                    _logger.Debug("Event added: Actor {0} will use {1} on {2}", item.Source, item.Action.BaseAction.Name, item.Target);
                    break;
                default:
                    break;
            }
            #endregion

            return base.Add(item);
        }

        // TODO: log popping.



    }
}
