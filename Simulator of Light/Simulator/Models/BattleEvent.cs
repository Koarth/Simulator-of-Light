using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models {
    public class BattleEvent {

        public BattleEventType Type { get; set; }
        public long Time { get; set; }

        // Involved entities.
        public IActor Source { get; set; }
        public ITarget Target { get; set; }

        public Aura Aura { get; set; }

        // Action information.
        public BaseAction Ability { get; set; }

        // Damage values.
        public int RawDamage { get; set; }
        public int RffectiveDamage { get; set; }
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

        // Aura information.
        public BaseAura AuraApplied { get; set; }
        public int AuraStack { get; set; }
    }
}
