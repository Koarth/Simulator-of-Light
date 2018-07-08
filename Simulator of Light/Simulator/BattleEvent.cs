using Simulator_of_Light.Simulator.Models;
using System;
using Action = Simulator_of_Light.Simulator.Models.Action;

namespace Simulator_of_Light.Simulator {
    public class BattleEvent : IComparable<BattleEvent> {

        public BattleEventType Type { get; private set; }
        public IActor Source { get; private set; }
        public ITarget Target { get; private set; }
        public long Time { get; private set; }

        public Aura Aura { get; private set; }
        public Action Action { get; private set; }
        public BaseAura BaseAura { get; private set; }

        public BattleEvent(BattleEventType type, long time) {
            Type = type;
            Time = time;

            Source = null;
            Target = null;
            Aura = null;
            Action = null;
            BaseAura = null;
        }

        public BattleEvent (BattleEventType type,
                            long time,
                            IActor source, 
                            ITarget target = null,
                            Action action = null,
                            Aura aura = null,
                            BaseAura baseAura = null) {

            Type = type;
            Source = source;
            Target = target;
            Time = time;

            Action = action;
            Aura = aura;
            BaseAura = baseAura;

        }

        // Sortable by execution time.
        public int CompareTo(BattleEvent that) {
            if (this.Time > that.Time) return -1;
            if (this.Time == that.Time) return 0;
            return 1;
        }




    }
}
