using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models {
    public class StrikingDummy : ITarget {

        public StrikingDummy(string name = "Fluffy Pillow", long maxHP = long.MaxValue) {

            Name = name;
            MaxHP = maxHP;

            CurrentHP = MaxHP;
            Auras = new SortedList<long, Aura>();

        }

        public string Name { get; private set; }
        public long CurrentHP { get; private set; }
        public long MaxHP { get; private set; }

        public SortedList<long, Aura> Auras { get; }

        public void ApplyDamage() {
            throw new NotImplementedException();
        }

        public Aura GetAuraByName(string auraName) {
            foreach (Aura aura in this.Auras.Values) {
                if (aura.BaseAura.Name == auraName) {
                    return aura;
                }
            }

            return null;
        }

    }
}
