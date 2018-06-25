using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Simulator_of_Light.Simulator.Resources.Constants;

namespace Simulator_of_Light.Simulator.Resources {

    public static class Formulas {

        /* Credit for the derivation of all formulas and constants goes 
         to the TheoryJerks and their contributors. */

        /// <summary>
        /// Calculates the base damage of an action, before being modified by auras,
        /// traits, or critical/direct hits.
        /// </summary>
        /// <param name="potency">The potency value of the action.</param>
        /// <param name="weaponDamage">The source's weapon damage statistic.</param>
        /// <param name="attackPower">The source's attack power statistic (equal to the source's main stat).</param>
        /// <param name="jobID">The jobID of the action's source.</param>
        /// <param name="stat">The stat that scales the action.</param>
        /// <returns>The base damage of an action, before being modified by auras,
        /// traits, or critical/direct hits.</returns>
        public static double calculateActionDamage(double potency, double weaponDamage,
            double attackPower, JobID jobID,
            CharacterStat stat = CharacterStat.UNKNOWN) {

            if (stat == CharacterStat.UNKNOWN) {
                stat = getDefaultPrimaryStat(jobID);
            }

            // Base damage from job and weapon.
            double jobBaseDamage = Math.Truncate(BaseMain70 * getJobMod(jobID, stat) / 1000);
            double baseDamage = weaponDamage + jobBaseDamage;

            // Multiplier from Attack Power
            double apMultiplier = Math.Truncate(125 * (attackPower - BaseMain70) / BaseMain70 + 100) / 100;

            // Total damage after Attack Power and Potency multipliers.
            double totalDamage = Math.Truncate(potency * baseDamage * apMultiplier / 100);

            // Total damage after all multipliers.
            return totalDamage;
        }

        /// <summary>
        /// Calculates the base damage of autoattacks before modifications.
        /// </summary>
        /// <param name="weaponDamage">The character's weapon damage.</param>
        /// <param name="autoAttackDelay">The character's auto attack delay.</param>
        /// <param name="attackPower">The character's attack power.</param>
        /// <param name="jobID">The character's job.</param>
        /// <returns></returns>
        public static double calculateAutoAttackDamage(double weaponDamage, double autoAttackDelay, double attackPower,
            JobID jobID) {

            CharacterStat weaponskillStat = getWeaponskillStat(jobID);

            double jobBaseDamage = Math.Truncate(getBaseStat(weaponskillStat) * getJobMod(jobID, weaponskillStat) / 1000);
            double baseDamage = (weaponDamage + jobBaseDamage) * (autoAttackDelay / 3);

            double apMultiplier = Math.Truncate(125 * (attackPower - BaseMain70) / 292 + 100) / 100;

            double totalDamage = Math.Truncate(getAutoAttackPotency(jobID) * baseDamage * apMultiplier / 100);

            return totalDamage;
        }

        /// <summary>
        /// Calculates critical hit rate from the critical hit statistic.  This is the 
        /// critical hit rate BEFORE auras and traits.
        /// </summary>
        /// <param name="criticalHit">The critical hit statistic to convert.</param>
        /// <returns></returns>
        public static double calculateCriticalHitRate(double criticalHit) {
            if (criticalHit < getBaseStat(CharacterStat.CRITICALHIT)) {
                throw new ArgumentException("Critical Hit value lower than base!");
            }
            return Math.Floor(CriticalHitGrowthModifier * (criticalHit - getBaseStat(CharacterStat.CRITICALHIT)) / LevelGrowthPenalty70 + 50) / 1000;
        }

        /// <summary>
        /// Calculates the critical hit multiplier for a given critical hit statistic.
        /// </summary>
        /// <param name="criticalHit">The critical hit statistic to convert.</param>
        /// <returns></returns>
        public static double calculateCriticalHitMultiplier(double criticalHit) {
            if (criticalHit < getBaseStat(CharacterStat.CRITICALHIT)) {
                throw new ArgumentException("Critical Hit value lower than base!");
            }
            return Math.Floor(CriticalHitGrowthModifier * (criticalHit - getBaseStat(CharacterStat.CRITICALHIT)) / LevelGrowthPenalty70 + 1400) / 1000;
        }

        /// <summary>
        /// Calculates the determination damage multiplier for a given determination statistic.
        /// </summary>
        /// <param name="determination">The determination statistic to be converted.</param>
        /// <returns></returns>
        public static double calculateDeterminationMultiplier(double determination) {
            if (determination < getBaseStat(CharacterStat.DETERMINATION)) {
                throw new ArgumentException("Determination value lower than base!");
            }
            return Math.Floor(DeterminationGrowthModifier * (determination - getBaseStat(CharacterStat.DETERMINATION)) / LevelGrowthPenalty70 + 1000) / 1000;
        }

        /// <summary>
        /// Calculates the direct hit rate for a given direct hit statistic.
        /// </summary>
        /// <param name="directHit">The direct hit statistic to convert.</param>
        /// <returns></returns>
        public static double calculateDirectHitRate(double directHit) {
            if (directHit < getBaseStat(CharacterStat.DIRECTHIT)) {
                throw new ArgumentException("Direct Hit value lower than base!");
            }
            return Math.Floor(DirectHitGrowthModifier * (directHit - getBaseStat(CharacterStat.DIRECTHIT)) / LevelGrowthPenalty70) / 1000;
        }
        
        /// <summary>
        /// Calculates the multiplier for damage-over-time effects and autoattacks
        /// derived from skillspeed or spellspeed.
        /// </summary>
        /// <param name="speed">The character's skill or spell speed statistic.</param>
        /// <returns></returns>
        public static double calculateSpeedMultiplier(double speed) {
            if (speed < getBaseStat(CharacterStat.SKILLSPEED)) {
                throw new ArgumentException("Speed value lower than base!");
            }
            return Math.Floor(SpeedGrowthModifier * (speed - getBaseStat(CharacterStat.SKILLSPEED)) / LevelGrowthPenalty70 + 1000) / 1000;
        }

        /// <summary>
        /// Calculates the damage multiplier derived from tenacity.  This is the same
        /// for both damage dealt and damage taken.  For example, a multiplier of 0.12 would
        /// mean the character deals 1.12x outgoing damage, or takes 0.88x incoming damage.
        /// </summary>
        /// <param name="tenacity">The character's tenacity statistic.</param>
        /// <returns></returns>
        public static double calculateTenacityMultiplier(double tenacity) {
            if (tenacity < getBaseStat(CharacterStat.TENACITY)) {
                throw new ArgumentException("Tenacity value lower than base!");
            }
            return Math.Floor(TenacityGrowthModifier * (tenacity - getBaseStat(CharacterStat.TENACITY)) / LevelGrowthPenalty70 + 1000) / 1000;
        }

        /// <summary>
        /// Calculates a character's total mana.
        /// </summary>
        /// <param name="piety">The character's piety statistic.</param>
        /// <param name="jobID">The character's job.</param>
        /// <returns></returns>
        public static double calculateTotalMana(double piety, JobID jobID) {
            if (piety < getBaseStat(CharacterStat.PIETY)) {
                throw new ArgumentException("Piety value lower than base!");
            }
            return Math.Floor((getJobMod(jobID, CharacterStat.MP) / 100) * (PietyGrowthModifier * (piety - getBaseStat(CharacterStat.PIETY)) / LevelGrowthPenalty70 + BaseMP70));
        }

        /// <summary>
        /// Calculates a character's total health pool.
        /// </summary>
        /// <param name="vitality">The character's vitality statistic.</param>
        /// <param name="jobID">The character's job.</param>
        /// <returns></returns>
        public static double calculateTotalHP(double vitality, JobID jobID) {
            return Math.Floor(BaseHP70 * (getJobMod(jobID, CharacterStat.HP) / 100)) + Math.Floor((vitality - BaseMain70) * 21.5);
        }

        /// <summary>
        /// Calculates a character's global cooldown incurrence given their speed and the recast
        /// time of the action.  GCD calculated is BEFORE any buffs (Fey Wind, Arrow, etc.)
        /// </summary>
        /// <param name="speed">The character's speed statisitc.</param>
        /// <param name="recastDelay">The recast delay of the action used.</param>
        /// <returns></returns>
        public static long calculateGlobalCooldown(double speed) {
            if (speed < getBaseStat(CharacterStat.SKILLSPEED)) {
                throw new ArgumentException("Speed value lower than base!");
            }

            double GCD = Math.Floor(1000 - 
                - Math.Floor(SpeedGrowthModifier 
                            * (speed - getBaseStat(CharacterStat.SKILLSPEED)) 
                            / LevelGrowthPenalty70) 
                            * (BaseRecastDelay * 1000) 
                            / 1000);

            return (long)GCD;
        }

        /// <summary>
        /// Provides a multiplier for a character's base stats before clan, traits, and gear.
        /// This multiplier is derived from the character's jobmod for that stat.  If there is
        /// no jobmod for that stat, a multiplier of 1x is returned.
        /// 
        /// Currently, only main stats (STR, DEX, INT, MND, VIT) are multiplied at base.
        /// </summary>
        /// <param name="jobID"></param>
        /// <param name="stat"></param>
        /// <returns></returns>
        public static double getBaseStatMultiplier(JobID jobID, CharacterStat stat) {
            double multiplier = 1;
            try {
                multiplier = getJobMod(jobID, stat) / 100;
            } catch (ArgumentException) { }
            return multiplier;
        }
    }
}
