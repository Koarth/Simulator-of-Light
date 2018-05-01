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

        public EquipmentSet() {
            Equipment = new Equipment[Enum.GetNames(typeof(EquipSlot)).Length - 1];
            StatSummary = new Dictionary<CharacterStat, double>();
        }

        [JsonConstructor]
        public EquipmentSet(Equipment[] equipment) {

            // Put each item in its correct slot in the array.  So that we can't equip 10 weapons.
            // TODO: A configuration with overwriting slots should generate a warning.

            Equipment = new Equipment[Enum.GetNames(typeof(EquipSlot)).Length - 1];
            StatSummary = new Dictionary<CharacterStat, double>();

            // Add each initial piece to the set.
            foreach (Equipment e in equipment) {
                if (!(e == null)) {
                    AddItem(e);
                }
            }
        }

        public void AddItem(Equipment item) {
            EquipSlot slot = item.Slot;

            // Properly remove any item already in that slot.
            if (Equipment[(int)slot - 1] != null) {
                RemoveItem(slot);
            }

            // Add the item.
            Equipment[(int)slot - 1] = item;

            // Sum the stats on the item into the set's stat summary.
            foreach (CharacterStat stat in item.Stats.Keys) {
                if (!StatSummary.ContainsKey(stat)) {
                    StatSummary.Add(stat, item.Stats[stat]);
                } else {
                    StatSummary[stat] += item.Stats[stat];
                }
            }

            // Sum the stats from Materia into the set's stat summary.
            foreach (Materia materia in item.Materia) {
                if (!StatSummary.ContainsKey(materia.Stat)) {
                    StatSummary.Add(materia.Stat, materia.Value);
                } else {
                    StatSummary[materia.Stat] += materia.Value;
                }
            }

            return;
        }

        public void RemoveItem(EquipSlot slot) {
            // Already nothing in that slot.
            if (Equipment[(int)slot - 1] == null) {
                // TODO: generate warning?
                return;
            }

            // Subtract the item's stats from the summary.
            Equipment item = Equipment[(int)slot - 1];
            foreach (Materia materia in item.Materia) {
                StatSummary[materia.Stat] -= materia.Value;
            }
            foreach (CharacterStat stat in item.Stats.Keys) {
                StatSummary[stat] -= item.Stats[stat];
            }

            // Remove the item.
            Equipment[(int)slot - 1] = null;

            return;
        }

        public Equipment[] Equipment { get => _equipment; private set => _equipment = value; }
        public Dictionary<CharacterStat, double> StatSummary { get => _statSummary; private set => _statSummary = value; }
    }
}
