using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Simulator_of_Light.Simulator.Models {

    public class GearItem {

        [JsonConstructor]
        public GearItem(string name, int ilvl, JobID[] jobs, EquipSlot slot,
            Dictionary<CharacterStat, double> stats, Materia[] materia) {

            Name = name;
            Ilvl = ilvl;
            Jobs = new HashSet<JobID>(jobs);
            Slot = slot;
            Stats = stats;
            Materia = materia;
        }

        // Metadata
        public string Name { get; private set; }
        public int Ilvl { get; private set; }
        public HashSet<JobID> Jobs { get; private set; }
        public EquipSlot Slot { get; private set; }

        // Stats
        public Dictionary<CharacterStat, double> Stats { get; private set; }
        public Materia[] Materia { get; private set; }
    }
}
