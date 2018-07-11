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

        // Properties for damage-over-time auras.
        public double BaseDamage { get; set; }
        public double CriticalHitRate { get; set; }
        public double CriticalHitMultiplier { get; set; }
        public double DirectHitRate { get; set; }
        public List<double> DamageMultipliers { get; set; }

        public Aura(BaseAura baseAura, IActor source, long expires, bool isBuff) {

            BaseAura = baseAura;
            Source = source;
            Expires = expires;

            BaseDamage = 0;
            CriticalHitRate = 0;
            CriticalHitMultiplier = 0;
            DirectHitRate = 0;
            DamageMultipliers = null;
        }

        // Sortable by expiry time.
        public int CompareTo(Aura that) {
            if (this.Expires > that.Expires) return -1;
            if (this.Expires == that.Expires) return 0;
            return 1;
        }

    }
}
