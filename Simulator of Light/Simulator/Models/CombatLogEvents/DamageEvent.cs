using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models.CombatLogEvents {
    public class DamageEvent : CombatLogEvent {

        public ITarget Target { get; set; }

        // Action information.
        public BaseAction Action { get; set; }
        public BaseAura Aura { get; set; }

        // Damage values.
        public int RawDamage { get; set; }
        public int EffectiveDamage { get; set; }
        public int AbsorbedDamage { get; set; }
        public int BlockedDamage { get; set; }
        public int OverkillDamage { get; set; }

        // Healing values.
        public int RawHealing { get; set; }
        public int EffectiveHealing { get; set; }
        public int AbsorbedHealing { get; set; }

        // Modifiers.
        public bool IsCritical { get; set; }
        public bool IsDirectHit { get; set; }
        public bool IsTick { get; set; }

        public DamageEvent(long time, IActor source, ITarget target, BaseAction action) : base(time, source) {
            this.Target = target;
            this.Action = action;
        }

        public override string ToString() {

            // TODO
            throw new NotImplementedException();
        }
    }
}
