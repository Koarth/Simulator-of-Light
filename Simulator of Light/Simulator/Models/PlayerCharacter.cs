﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Resources;

namespace Simulator_of_Light.Simulator.Models {

    public class PlayerCharacter : IActor, ITarget {

        public PlayerCharacter(string name, CharacterClan clan, JobID jobID) {
            Name = name;
            JobID = jobID;
            Clan = clan;

            Actions = new Dictionary<string, Action>();
            var actionDict = BaseActionFactory.getBaseActionsByJobID(jobID);
            foreach (string key in actionDict.Keys) {
                Actions.Add(key, new Action(actionDict[key]));
            }
        }

        public PlayerCharacter(string name, CharacterClan clan, JobID jobID, GearSet gearset)
            : this(name, clan, jobID) {

            this.EquipGearset(gearset);
        }

        // Static properties
        public string Name { get; private set; }
        public CharacterClan Clan { get; private set; }
        public JobID JobID { get; private set; }

        // Dynamic properties
        public long CurrentHP { get; set; }
        public long MaxHP { get; private set; }
        public int CurrentMP { get; set; }
        public int MaxMP { get; private set; }
        public double CurrentTP { get; set; }
        public Dictionary<string, Action> Actions { get; private set; }
        public Dictionary<CharacterStat, double> Stats { get; private set; }
        public GearSet GearSet { get; private set; }

        public SortedList<long, Aura> Auras { get; private set; }


        public void ApplyDamage() {
            throw new NotImplementedException();
        }

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

            // Each item must be usable by this PlayerCharacter's job.
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

            // Base stats are multiplied by the PlayerCharacter's jobmod for that stat.
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
            //this.Stats[CharacterStat.HP] = Formulas.calculateTotalHP(this.Stats[CharacterStat.VITALITY], this.JobID);
            //this.Stats[CharacterStat.MP] = Formulas.calculateTotalMana(this.Stats[CharacterStat.PIETY], this.JobID);
            this.MaxHP = (int)Formulas.calculateTotalHP(this.Stats[CharacterStat.VITALITY], this.JobID);
            this.MaxMP = (int)Formulas.calculateTotalMana(this.Stats[CharacterStat.PIETY], this.JobID);

        }
    }
}
