using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models {

    public sealed class Aura {

        public BaseAura BaseAura { get; set; }

        public double DamageModifier { get; set; }
        public double CriticalHitRate { get; set; }
        public double DirectHitRate { get; set; }
        public IActor Source { get; set; }

    }
}
