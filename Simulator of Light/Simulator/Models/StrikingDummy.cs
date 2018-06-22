using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models {
    public class StrikingDummy : ITarget {

        private string _name;
        private long _currentHP;
        private long _maxHP;

        private SortedList<long, Aura> _auras;

        public StrikingDummy(string name = "Fluffy Pillow", long maxHP = long.MaxValue) {

            Name = name;
            MaxHP = maxHP;

            CurrentHP = MaxHP;
            Auras = new SortedList<long, Aura>();

        }

        public void ApplyDamage() {
            throw new NotImplementedException();
        }

        public SortedList<long, Aura> Auras { get; }

        public string Name { get => _name; private set => _name = value; }
        public long CurrentHP { get => _currentHP; private set => _currentHP = value; }
        public long MaxHP { get => _maxHP; private set => _currentHP = value; }

    }
}
