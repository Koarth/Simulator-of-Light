using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models {
    public class CombatLogEvent : IComparable<CombatLogEvent> {

        public CombatLogEventType Type { get; set; }
        public long Time { get; set; }

        // Involved entities.
        public IActor Source { get; set; }
        public ITarget Target { get; set; }

        public BaseAura Aura { get; set; }

        // Action information.
        public BaseAction Ability { get; set; }

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

        // Aura information.
        public BaseAura AuraApplied { get; set; }
        public int AuraStack { get; set; }

        public CombatLogEvent(CombatLogEventType type, long time, IActor source,
            IActor target = null, BaseAura aura = null, BaseAction action = null) {

            Type = type;
            Time = time;
            Source = source;
            Target = target;
            Aura = aura;
            Ability = action;

        }

        public int CompareTo(CombatLogEvent that) {
            if (this.Time > that.Time) return -1;
            if (this.Time == that.Time) return 0;
            return 1;
        }
    }
}
