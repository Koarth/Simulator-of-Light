using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Resources;
using C5;

namespace Simulator_of_Light.Simulator.Models {

    public class PlayerCharacter : IActor {

        public PlayerCharacter(string name, CharacterClan clan, JobID jobID) {
            Name = name;
            JobID = jobID;
            Clan = clan;

            Actions = new Dictionary<string, Action>();
            var actionDict = BaseActionFactory.getBaseActionsByJobID(jobID);
            foreach (string key in actionDict.Keys) {
                Actions.Add(key, new Action(actionDict[key]));
            }

            this._initializeState();
        }

        public PlayerCharacter(string name, CharacterClan clan, JobID jobID, GearSet gearset)
            : this(name, clan, jobID) {

            this.EquipGearset(gearset);

            this._initializeState();
        }

        // Static properties
        public string Name { get; private set; }
        public CharacterClan Clan { get; private set; }
        public JobID JobID { get; private set; }

        // Configured properties
        public long MaxHP { get; private set; }
        public int MaxMP { get; private set; }
        public int MaxTP { get; private set; }
        public Dictionary<CharacterStat, double> Stats { get; private set; }
        public GearSet GearSet { get; private set; }

        // Dynamic properties
        public long CurrentHP { get; set; }
        public int CurrentMP { get; set; }
        public int CurrentTP { get; set; }

        public Dictionary<string, Action> Actions { get; private set; }
        public IntervalHeap<Aura> Auras { get; private set; }

        // Timing properties
        public long GlobalRecastAvailable { get; set; }
        public long AnimationLockExpires { get; set; }


        // MOCK FUNCTION, REPLACE WITH ACTION LIST DECISIONS
        public QueuedEvent DecideAction(long time, 
            ITarget[] friendlyTargets, 
            ITarget[] enemyTargets) {

            if (this.JobID != JobID.WHM) {
                throw new NotImplementedException("This is a mock function for a WHM actionlist.");
            }

            // Arbitrarily choose target
            ITarget target = enemyTargets[0];

            if (time > this.GlobalRecastAvailable) {
                // Aero III
                var a = target.GetAuraByName("Aero III");
                if (a == null || ((a.Expires - time) < 3000)) {
                    return new QueuedEvent(QueuedEventType.USE_ACTION, time, this,
                        target, action: Actions["Aero III"]);
                }
                // Aero II
                a = target.GetAuraByName("Aero II");
                if (a == null || ((a.Expires - time) < 3000)) {
                    return new QueuedEvent(QueuedEventType.USE_ACTION, time, this,
                        target, action: this.Actions["Aero II"]);
                }
                // Stone IV (no conditions)
                return new QueuedEvent(QueuedEventType.USE_ACTION, time, this, 
                    target, action: this.Actions["Stone IV"]);

            } else if (time > this.AnimationLockExpires) {

                // Cleric Stance
                var a = Actions["Cleric Stance"];
                if (a.RecastAvailable < time) {
                    return new QueuedEvent(QueuedEventType.USE_ACTION, time, this,
                        action: a);
                }

                // Presence of Mind
                a = Actions["Presence of Mind"];
                if (a.RecastAvailable < time) {
                    return new QueuedEvent(QueuedEventType.USE_ACTION, time, this,
                        action: a);
                }

                // In-between GCDs and no OGCDs available - delay for a bit.
                long tryNextAction = this.GlobalRecastAvailable - Constants.GlobalAnimationDelay;
                if (time > tryNextAction) {
                    tryNextAction = this.GlobalRecastAvailable;
                }
                return new QueuedEvent(QueuedEventType.ACTOR_READY, tryNextAction, this);
            }

            throw new Exception("Control passed to actor early.");
            //return new QueuedEvent(QueuedEventType.ACTOR_READY, this, 
            //    time: Math.Min(GlobalRecastAvailable, AnimationLockExpires));
        }

        /// <summary>
        /// This will be the default function used in actionlist decisions, laying
        /// out the minimum requirements for an ability to actually be used.  If these
        /// requirements are not met, the ability should be passed over.
        /// </summary>
        /// <param name="action">The action to check.</param>
        /// <param name="time">The current fight time in milliseconds.</param>
        /// <returns></returns>
        public bool IsActionUsable(Action action, long time) {

            var a = Actions[action.BaseAction.Name];

            // Animation locked.
            if (time < this.AnimationLockExpires) {
                return false;
            }

            // Non-GCD ability, and GCD isn't up yet.
            if (!a.BaseAction.IsOGCD && time < this.GlobalRecastAvailable) {
                return false;
            }

            // Action recast isn't up yet.
            if (time < a.RecastAvailable) {
                return false;
            }

            // Resource requirements.
            if (CurrentMP < a.BaseAction.MpCost
                || CurrentTP < a.BaseAction.TpCost) {
                return false;
            }

            // TODO: Combo action requirements.
            // TODO: Range requirements.
            // Targetting requirements?

            return true;

        }

        /// <summary>
        /// Begins an animation lock for an action without resolving its other effects.
        /// This is primarily to model abilities with cast times, where the effects of
        /// the action take effect only after the cast completes.
        /// 
        /// This function will almost certainly be overridden by job-specific modules
        /// that handle job-specific auras, such as Greased Lightning.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public long BeginCast(Action action, long time) {

            // TODO: ADD AURAS TO CALCULATION
            // Class specific auras will probably be part of a class-specific module.

            long castTime = (long)(action.BaseAction.CastTime * 1000);
            double speed = Math.Max(this.Stats[CharacterStat.SKILLSPEED], this.Stats[CharacterStat.SPELLSPEED]);

            // Cast time, else animation lock if instant-cast.
            if (castTime > 0) {
                long hastedCastTime = Formulas.calculateRecastDelay(speed, castTime);
                this.AnimationLockExpires = time + hastedCastTime;
            } else {
                this.AnimationLockExpires = time + Constants.GlobalAnimationDelay;
            }

            // Global recast
            if (!action.BaseAction.IsOGCD) {
                this.GlobalRecastAvailable = time + Formulas.calculateRecastDelay(speed, Constants.GlobalRecastTime);
            }

            return this.AnimationLockExpires;

            //return new QueuedEvent(QueuedEventType.ACTOR_READY, this.AnimationLockExpires, this);

        }

        /// <summary>
        /// This function will perform necessary state changes to the actor
        /// that take place when executing the given action.  It does not handle
        /// the external effects of the action, nor does it apply auras to the
        /// executing actor.
        /// 
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="time">Current fight time.</param>
        public void ExecuteAction(Action action, long time) {

            this.CurrentMP -= action.BaseAction.MpCost;
            this.CurrentTP -= action.BaseAction.TpCost;

            // TODO: combo actions

        }

        public Aura GetAuraByName(string auraName) {
            foreach (Aura aura in this.Auras) {
                if (aura.BaseAura.Name == auraName) {
                    return aura;
                }
            }

            return null;
        }


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
            this.MaxHP = (int)Formulas.calculateTotalHP(this.Stats[CharacterStat.VITALITY], this.JobID);
            this.MaxMP = (int)Formulas.calculateTotalMana(this.Stats[CharacterStat.PIETY], this.JobID);
            this.MaxTP = (int)Constants.BaseTP70;

        }

        private void _initializeState() {
            this.CurrentHP = this.MaxHP;
            this.CurrentMP = this.MaxMP;
            this.CurrentTP = this.MaxTP;

            this.GlobalRecastAvailable = long.MinValue;
            this.AnimationLockExpires = long.MinValue;
        }
    }
}
