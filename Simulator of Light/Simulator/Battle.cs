using System;
using System.Linq;
using C5;
using Simulator_of_Light.Simulator.Models;
using Simulator_of_Light.Simulator.Models.CombatLogEvents;

namespace Simulator_of_Light.Simulator {
    public partial class Battle {

        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        // Entities in the fight.  Actors are generally understood to be players,
        // pets, or "NPCs" that will stand-in as raid buff simulators.  Enemies do
        // not act, but rather keep track of auras and other state effects.
        public IActor[] Actors { get; private set; }
        public ITarget[] Friendlies { get; private set; }
        public ITarget[] Enemies { get; private set; }
        public ITarget[] Targets { get; private set; }

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
        public IntervalHeap<BattleEvent> EventQueue { get; private set; }

        public ArrayList<CombatLogEvent> EventLog { get; private set; }

        public Battle(IActor[] actors, int length = 180, int num_enemies = 1) {

            Actors = actors;
            Friendlies = actors;
            FightLength = length * 1000;
            _logger.Debug("Fight length: {0}", FightLength);

            Time = 0;
            TickOffset = new Random().Next(0, 3000);
            _logger.Debug("Tick offset: {0}", TickOffset);

            foreach (IActor actor in actors) {
                _logger.Debug("Actor added: [{0}] {1}", actor.JobID.ToString(), actor.Name);
            }

            // Initialize enemies.
            Enemies = new ITarget[num_enemies];
            for (int i = 0; i < num_enemies; i++) {
                Enemies[i] = new StrikingDummy();
                _logger.Debug("Enemy added: {0}", Enemies[i].Name);
            }

            Targets = Friendlies.ToList().Concat(Enemies.ToList()).ToArray();

            EventQueue = new IntervalHeap<BattleEvent>();
            EventLog = new ArrayList<CombatLogEvent>();

            // Add the inital Actor decisions to the queue.  These should drive the rest of the
            // simulation.
            foreach (IActor actor in Actors) {
                EventQueue.Add(new BattleEvent(BattleEventType.ACTOR_READY, Time, actor));
            }

            // Event to signal the end of the fight.
            EventQueue.Add(new BattleEvent(BattleEventType.FIGHT_COMPLETE, FightLength));

            // Event to signal the next Aura tick.
            EventQueue.Add(new BattleEvent(BattleEventType.AURA_TICK, Time + TickOffset));

            if (TickOffset > 1500) {
                EventQueue.Add(new BattleEvent(BattleEventType.REGEN_TICK, 1500 - TickOffset));
            } else {
                EventQueue.Add(new BattleEvent(BattleEventType.REGEN_TICK, TickOffset + 1500));
            }
        }

        public void Run() {

            // TODO: Log time advancements.
            throw new NotImplementedException();
        }
        
    }
}
