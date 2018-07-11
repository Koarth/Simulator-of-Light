using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models.CombatLogEvents {
    public class AuraEvent : CombatLogEvent {

        public ITarget Target { get; set; }

        // Aura information.
        public BaseAura Aura { get; set; }
        public int AuraStack { get; set; }

        public AuraEvent(long time, IActor source, ITarget target, BaseAura aura) : base(time, source) {
            this.Target = target;
            this.Aura = aura;

            this.AuraStack = 1;
        }

        public override string ToString() {
            throw new NotImplementedException();
        }

    }
}
