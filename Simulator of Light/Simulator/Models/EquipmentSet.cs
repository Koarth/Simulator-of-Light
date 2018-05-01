using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Simulator_of_Light.Simulator.Models {

    public class EquipmentSet {

        private Equipment[] _equipment;
        private Dictionary<CharacterStat, double> _statSummary;

        [JsonConstructor]
        public EquipmentSet(Equipment[] equipment) {

            // Put each item in its correct slot in the array.  So that we can't equip 10 weapons.
            // TODO: A configuration with overwriting slots should generate a warning.

            Equipment = new Equipment[Enum.GetNames(typeof(EquipSlot)).Length - 1];
            foreach (Equipment item in equipment) {
                Equipment[(int)item.Slot - 1] = item;
            }

            StatSummary = new Dictionary<CharacterStat, double>();

            // Sum the stats on equipment for a cached summary.
            foreach (Equipment e in Equipment) {
                if (!(e == null)) {
                    foreach (CharacterStat stat in e.Stats.Keys) {
                        if (!StatSummary.ContainsKey(stat)) {
                            StatSummary.Add(stat, e.Stats[stat]);
                        } else {
                            StatSummary[stat] += e.Stats[stat];
                        }
                    }

                    foreach (Materia materia in e.Materia) {
                        if (!StatSummary.ContainsKey(materia.Stat)) {
                            StatSummary.Add(materia.Stat, materia.Value);
                        } else {
                            StatSummary[materia.Stat] += materia.Value;
                        }
                    }
                }
                
            }
        }

        public Equipment[] Equipment { get => _equipment; set => _equipment = value; }
        public Dictionary<CharacterStat, double> StatSummary { get => _statSummary; private set => _statSummary = value; }
    }
}
