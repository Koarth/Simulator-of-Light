using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models.CombatLogEvents {
    public class HealingEvent : CombatLogEvent {

        public ITarget Target { get; set; }

        // Action information.
        public BaseAction Action { get; set; }
        public BaseAura Aura { get; set; }

        // Healing values.
        public int RawHealing { get; set; }
        public int EffectiveHealing { get; set; }
        public int AbsorbedHealing { get; set; }

        public HealingEvent(long time, IActor source, ITarget target, BaseAction action) : base(time, source) {
            this.Target = target;
            this.Action = action;
        }

        // Modifiers.
        public bool IsCritical { get; set; }
        public bool IsDirectHit { get; set; }
        public bool IsTick { get; set; }

        public override string ToString() {
            throw new NotImplementedException();
        }


    }
}
