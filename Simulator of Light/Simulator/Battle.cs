using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C5;
using Simulator_of_Light.Simulator.Models;
using Simulator_of_Light.Simulator.Resources;

namespace Simulator_of_Light.Simulator {
    public class Battle {

        // Entities in the fight.  Actors are generally understood to be players,
        // pets, or "NPCs" that will stand-in as raid buff simulators.  Enemies do
        // not act, but rather keep track of auras and other state effects.
        public IActor[] Actors { get; private set; }
        public ITarget[] Friendlies { get; private set; }
        public ITarget[] Enemies { get; private set; }

        // Length of the fight in milliseconds.  Battle will dump the queue and report results
        // once this time is reached.
        public long FightLength { get; private set; }
        // Current fight time in milliseconds
        public long Time { get; private set; }

        // Offset for aura ticks in ms;  
        // ALL auras tick on the same 3-second interval, which is not deterministic
        // with regards to the beginning of the fight, but rather the spawning of the
        // instance server.  Randomly generate an offset from [0,3000) to simulate this.
        public int TickOffset { get; private set; }

        // The queue of events to resolve, implemented as a priority queue.
        // key = time in milliseconds at which the event is to be resolved.
        // value = event to be resolved
        public IntervalHeap<QueuedEvent> EventQueue { get; private set; }

        public ArrayList<BattleEvent> EventLog { get; private set; }

        public Battle(IActor[] actors, int length = 180, int num_enemies = 1) {

            Actors = actors;
            Friendlies = actors;
            FightLength = length * 1000;

            Time = 0;
            TickOffset = new Random().Next(0, 3000);

            // Initialize enemies.
            Enemies = new ITarget[num_enemies];
            for (int i = 0; i < num_enemies; i++) {
                Enemies[i] = new StrikingDummy();
            }

            EventQueue = new IntervalHeap<QueuedEvent>();
            EventLog = new ArrayList<BattleEvent>();

            // Add the inital Actor decisions to the queue.  These should drive the rest of the
            // simulation.
            foreach (IActor actor in Actors) {
                EventQueue.Add(new QueuedEvent(QueuedEventType.ACTOR_READY, Time, actor));
            }

            // Event to signal the end of the fight.
            EventQueue.Add(new QueuedEvent(QueuedEventType.FIGHT_COMPLETE, FightLength));

            // Event to signal the next Aura tick.
            EventQueue.Add(new QueuedEvent(QueuedEventType.AURA_TICK, Time + TickOffset));
        }

        public void Run() {
            throw new NotImplementedException();
        }

        private void HandleEvent(QueuedEvent e) {

            switch (e.Type) {
                // Actor-driven events.
                case QueuedEventType.ACTOR_READY:
                    // Decision should be either ACTOR_READY or RESOLVE_ACTION.
                    QueuedEvent decision = e.Source.DecideAction(Time, Actors, Enemies);
                    EventQueue.Add(decision);
                    break;
                case QueuedEventType.USE_ACTION:
                    // Initiate the use of an action.
                    // Generates:
                    //     Begin cast events
                    //     RESOLVE_ACTION
                    //     ACTOR_READY

                    // Queue next actor availability.
                    long t = e.Source.BeginCast(e.Action, Time);
                    EventQueue.Add(new QueuedEvent(QueuedEventType.ACTOR_READY, t, e.Source));

                    // Resolves later if it has a cast time, else resolves now.
                    if (e.Action.BaseAction.CastTime > 0) {
                        EventQueue.Add(new QueuedEvent(QueuedEventType.RESOLVE_ACTION, t, e.Source, action: e.Action));
                        EventLog.Add(new BattleEvent(BattleEventType.BEGINCAST, Time, e.Source, action: e.Action.BaseAction));
                    } else {
                        EventQueue.Add(new QueuedEvent(QueuedEventType.RESOLVE_ACTION, Time, e.Source, action: e.Action));
                        EventLog.Add(new BattleEvent(BattleEventType.CAST, Time, e.Source, action: e.Action.BaseAction));
                    }

                    break;
                case QueuedEventType.RESOLVE_ACTION:
                    // Resolve the effects of an action.
                    // Generates:
                    //     Damage/healing battle events
                    //     Cast battle events
                    //     ACTOR_READY
                    //     APPLY_AURA
                    //     APPLY_AURA_STACK
                    //     EXPIRE_AURA (?)
                    //     EXPIRE_AURA_STACK
                    break;
                case QueuedEventType.APPLY_AURA:
                    // Apply an aura to a target.
                    // Generates:
                    //     Aura applied battle events
                    //     Aura refreshed battle events
                    //     EXPIRE_AURA
                    //     APPLY_AURA_STACK
                    break;
                case QueuedEventType.APPLY_AURA_STACK:
                    // Add a stack to an existing aura.
                    // Generates:
                    //     Aura stack applied battle events.
                    //     Aura refreshed battle events.
                    //     EXPIRE_AURA
                    break;
                case QueuedEventType.EXPIRE_AURA:
                    // Remove an aura from the target.
                    // Generates:
                    //     Aura expired battle event.
                    break;
                case QueuedEventType.REMOVE_AURA_STACK:
                    // Remove a stack from an existing aura.
                    // Generates:
                    //     Aura stack removed battle event.
                    break;

                // Independent events.
                case QueuedEventType.AURA_TICK:
                    // Tick all auras on all targets.
                    // Generates:
                    //     Damage/Healing battle events.
                    //     Refresh battle events.
                    //     AURA_TICK
                    break;
                case QueuedEventType.REGEN_TICK:
                    // Tick passive regeneration on all _players_.
                    // Generates:
                    //     HP/MP/TP regeneration battle events.
                    //     REGEN_TICK
                    break;
                case QueuedEventType.FIGHT_COMPLETE:
                    // Cleanup; ignore all future events and report all
                    // generated battle events.
                    break;
            }

        }
    }
}
