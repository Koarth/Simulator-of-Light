using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models {
    public interface ITarget {

        string Name { get; }
        long CurrentHP { get; }
        long MaxHP { get; }

        SortedList<long, Aura> Auras { get; }

        void ApplyDamage();
        Aura GetAuraByName(string auraName);

    }
}
