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
        /// <param name="determination">The source's determination statistic.</param>
        /// <param name="jobID">The jobID of the action's source.</param>
        /// <param name="stat">The stat that scales the action.</param>
        /// <returns>The base damage of an action, before being modified by auras,
        /// traits, or critical/direct hits.</returns>
        public static double calculateActionDamage(double potency, double weaponDamage,
            double attackPower, JobID jobID,
            PrimaryStat stat = PrimaryStat.UNKNOWN) {

            if (stat == PrimaryStat.UNKNOWN) {
                stat = getDefaultPrimaryStat(jobID);
            }

            // Base damage from job and weapon.
            double jobBaseDamage = Math.Truncate(BaseDetermination70 * getJobMod(jobID, stat) / 1000);
            double baseDamage = weaponDamage + jobBaseDamage;

            // Multiplier from Attack Power
            double apMultiplier = Math.Truncate(125 * (attackPower - BaseAttackPower70) / BaseAttackPower70 + 100) / 100;

            // Total damage after Attack Power and Potency multipliers.
            double totalDamage = Math.Truncate(potency * baseDamage * apMultiplier / 100);

            // Total damage after all multipliers.
            return totalDamage;
        }

        public static double calculateAutoAttackDamage(double weaponDamage, double autoAttackDelay, double attackPower,
            JobID jobID) {

            double jobBaseDamage = Math.Truncate(BaseDetermination70 * getJobMod(jobID, getWeaponskillStat(jobID)) / 1000);
            double baseDamage = (weaponDamage + jobBaseDamage) * (autoAttackDelay / 3);

            double apMultiplier = Math.Truncate(125 * (attackPower - BaseAttackPower70) / 292 + 100) / 100;

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
            if (criticalHit < BaseCriticalHit70) {
                throw new ArgumentException("Critical Hit value lower than base!");
            }
            return Math.Floor(CriticalHitGrowthModifier * (criticalHit - BaseCriticalHit70) / LevelGrowthPenalty70 + 50) / 1000;
        }

        /// <summary>
        /// Calculates the critical hit multiplier for a given critical hit statistic.
        /// </summary>
        /// <param name="criticalHit">The critical hit statistic to convert.</param>
        /// <returns></returns>
        public static double calculateCriticalHitMultiplier(double criticalHit) {
            if (criticalHit < BaseCriticalHit70) {
                throw new ArgumentException("Critical Hit value lower than base!");
            }
            return Math.Floor(CriticalHitGrowthModifier * (criticalHit - BaseCriticalHit70) / LevelGrowthPenalty70 + 1400) / 1000;
        }

        /// <summary>
        /// Calculates the determination damage multiplier for a given determination statistic.
        /// </summary>
        /// <param name="determination">The determination statistic to be converted.</param>
        /// <returns></returns>
        public static double calculateDeterminationMultiplier(double determination) {
            if (determination < BaseDetermination70) {
                throw new ArgumentException("Determination value lower than base!");
            }
            return Math.Floor(DeterminationGrowthModifier * (determination - BaseDetermination70) / LevelGrowthPenalty70 + 1000) / 1000;
        }

        /// <summary>
        /// Calculates the direct hit rate for a given direct hit statistic.
        /// </summary>
        /// <param name="directHit">The direct hit statistic to convert.</param>
        /// <returns></returns>
        public static double calculateDirectHitRate(double directHit) {
            if (directHit < BaseDirectHit70) {
                throw new ArgumentException("Direct Hit value lower than base!");
            }
            return Math.Floor(DirectHitGrowthModifier * (directHit - BaseDirectHit70) / LevelGrowthPenalty70) / 1000;
        }

        public static double calculateTotalMana(double piety, JobID jobID) {
            if (piety < BasePiety70) {
                throw new ArgumentException("Piety value lower than base!");
            }
            return Math.Floor((getJobMod(jobID, PrimaryStat.MP) / 100) * (PietyGrowthModifier * (piety - BasePiety70) / LevelGrowthPenalty70 + BaseMana70));
        }

        public static double calculateSpeedMultiplier(double speed) {
            if (speed < BaseSpeed70) {
                throw new ArgumentException("Speed value lower than base!");
            }
            return Math.Floor(SpeedGrowthModifier * (speed - BaseSpeed70) / LevelGrowthPenalty70 + 1000) / 1000;
        }

        public static double calculateTenacityMultiplier(double tenacity) {
            if (tenacity < BaseTenacity70) {
                throw new ArgumentException("Tenacity value lower than base!");
            }
            return Math.Floor(TenacityGrowthModifier * (tenacity - BaseTenacity70) / LevelGrowthPenalty70 + 1000) / 1000;
        }

        public static double calculateTotalHP(double vitality, JobID jobID) {
            return Math.Floor(BaseHP70 * (getJobMod(jobID, PrimaryStat.HP) / 100)) + Math.Floor((vitality - BaseMain70) * 21.5);
        }

    }
}
