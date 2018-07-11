using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models.CombatLogEvents {
    public abstract class CombatLogEvent : IComparable<CombatLogEvent> {

        public long Time { get; set; }
        public IActor Source { get; set; }

        protected CombatLogEvent(long time, IActor source) {
            this.Time = time;
            this.Source = source;
        }


        public int CompareTo(CombatLogEvent that) {
            if (this.Time > that.Time) return -1;
            if (this.Time == that.Time) return 0;
            return 1;
        }

        public abstract override string ToString();

    }
}
