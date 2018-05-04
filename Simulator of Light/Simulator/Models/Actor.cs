using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Resources;

namespace Simulator_of_Light.Simulator.Models {

    public class Actor {

        // Static properties
        private string _name;
        private CharacterClan _clan;
        private JobID _jobID;

        // Dynamic properties
        private double _currentHP;
        private double _currentMP;
        private double _currentTP;
        private Dictionary<string, Action> _actions;
        private Dictionary<CharacterStat, double> _stats;
        private GearSet _gearSet;

        public Actor(string name, CharacterClan clan, JobID jobID) {
            Name = name;
            JobID = jobID;
            Clan = clan;

            Actions = new Dictionary<string, Action>();
            var actionDict = BaseActionFactory.getBaseActionsByJobID(jobID);
            foreach (string key in actionDict.Keys) {
                Actions.Add(key, new Action(actionDict[key]));
            }
        }

        public Actor(string name, CharacterClan clan, JobID jobID, GearSet gearset)
            : this(name, clan, jobID) {

            this.EquipGearset(gearset);
        }

        public string Name { get => _name; private set => _name = value; }
        public CharacterClan Clan { get => _clan; private set => _clan = value; }
        public JobID JobID { get => _jobID; private set => _jobID = value; }
        public double CurrentHP { get => _currentHP; set => _currentHP = value; }
        public double CurrentMP { get => _currentMP; set => _currentMP = value; }
        public double CurrentTP { get => _currentTP; set => _currentTP = value; }
        public Dictionary<string, Action> Actions { get => _actions; private set => _actions = value; }
        public Dictionary<CharacterStat, double> Stats { get => _stats; private set => _stats = value; }
        public GearSet GearSet { get => _gearSet; private set => _gearSet = value; }


        public void EquipGearset(GearSet gearset) {
            
            if (!this._validateGearSetForJob(gearset)) {
                throw new ArgumentException("Gearset not valid for actor's job.");
            }

            GearSet = gearset;
            this._recalculateStats();
        }

        private bool _validateGearSetForJob(GearSet gearset) {

            // Actors must have a weapon equipped.
            if (gearset.GetItemBySlot(EquipSlot.WEAPON) == null) {
                return false;
            }

            // Each item must be usable by this Actor's job.
            foreach (GearItem item in gearset.Items) {
                if (item != null && !item.Jobs.Contains(this.JobID)) {
                    return false;
                }
            }

            return true;
        }

        private void _recalculateStats() {
            this.Stats = new Dictionary<CharacterStat, double>();

            // Populate the simple base stats from constant definitions.
            foreach (CharacterStat stat in Enum.GetValues(typeof(CharacterStat))) {
                try {
                    this.Stats.Add(stat, Constants.getBaseStat(stat));
                } catch (ArgumentException) { }

            }

            // Base stats are multiplied by the Actor's jobmod for that stat.
            // This must be done before gear, race stats and traits are added.
            foreach (CharacterStat stat in this.Stats.Keys.ToList()) {
                this.Stats[stat] = Math.Floor(this.Stats[stat] 
                    * Formulas.getBaseStatMultiplier(this.JobID, stat));
            }

            // Add race bonuses/penalties.
            var clanStats = Constants.getClanBaseStats(this.Clan);
            foreach (CharacterStat stat in clanStats.Keys) {
                this.Stats[stat] += clanStats[stat];
            }


            // TODO ADD STATS FROM TRAITS

            // Add stats from gear.
            var statsFromGear = GearSet.StatSummary;
            foreach (CharacterStat stat in statsFromGear.Keys) {
                if (!this.Stats.ContainsKey(stat)) {
                    this.Stats[stat] = statsFromGear[stat];
                } else {
                    this.Stats[stat] += statsFromGear[stat];
                }
            }

            // TODO: ADD STATS FROM FOOD

            // Calculate the dependent stats, HP and MP:
            this.Stats[CharacterStat.HP] = Formulas.calculateTotalHP(this.Stats[CharacterStat.VITALITY], this.JobID);
            this.Stats[CharacterStat.MP] = Formulas.calculateTotalMana(this.Stats[CharacterStat.PIETY], this.JobID);
            
        }
    }
}
