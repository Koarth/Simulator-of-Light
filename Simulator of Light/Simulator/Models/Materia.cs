using Newtonsoft.Json;

namespace Simulator_of_Light.Simulator.Models {
    public class Materia {

        private string _name;
        private CharacterStat _stat;
        private double _value;

        [JsonConstructor]
        public Materia(string name, CharacterStat stat, double value) {
            Name = name;
            Stat = stat;
            Value = value;
        }

        public string Name { get => _name; private set => _name = value; }
        public CharacterStat Stat { get => _stat; private set => _stat = value; }
        public double Value { get => _value; private set => _value = value; }

        // TODO
    }
}
