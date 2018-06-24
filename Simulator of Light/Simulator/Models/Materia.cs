using Newtonsoft.Json;

namespace Simulator_of_Light.Simulator.Models {
    public class Materia {

        [JsonConstructor]
        public Materia(string name, CharacterStat stat, double value) {
            Name = name;
            Stat = stat;
            Value = value;
        }

        public string Name { get; private set; }
        public CharacterStat Stat { get; private set; }
        public double Value { get; private set; }

    }
}
