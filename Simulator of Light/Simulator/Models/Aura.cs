using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models {

    public sealed class Aura : IComparable<Aura> {

        public BaseAura BaseAura { get; set; }

        public IActor Source { get; set; }
        public long Expires { get; set; }
        public bool isBuff { get; set; }

        public double DamageModifier { get; set; }
        public double CriticalHitRate { get; set; }
        public double DirectHitRate { get; set; }

        public Aura(BaseAura baseAura, IActor source, long expires, bool isBuff,
            double damageModifier = 1,
            double criticalHitRate = 0,
            double directHitRate = 0) {

            BaseAura = baseAura;
            Source = source;
            Expires = expires;

            DamageModifier = damageModifier;
            CriticalHitRate = criticalHitRate;
            DirectHitRate = directHitRate;
        }

        // Sortable by expiry time.
        public int CompareTo(Aura that) {
            if (this.Expires > that.Expires) return -1;
            if (this.Expires == that.Expires) return 0;
            return 1;
        }

    }
}
