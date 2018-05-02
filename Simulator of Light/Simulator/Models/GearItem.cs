using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Simulator_of_Light.Simulator.Models {

    public class GearItem {

        // Metadata
        private string _name;
        private int _ilvl;
        private HashSet<JobID> _jobs;
        private EquipSlot _slot;

        // Stats
        private Dictionary<CharacterStat, double> _stats;
        private Materia[] _materia;

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

        public string Name { get => _name; private set => _name = value; }
        public int Ilvl { get => _ilvl; private set => _ilvl = value; }
        public HashSet<JobID> Jobs { get => _jobs; private set => _jobs = value; }
        public EquipSlot Slot { get => _slot; private set => _slot = value; }

        public Dictionary<CharacterStat, double> Stats { get => _stats; private set => _stats = value; }
        public Materia[] Materia { get => _materia; private set => _materia = value; }
    }
}
