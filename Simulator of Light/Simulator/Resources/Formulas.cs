using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Resources
{
    public static class Formulas
    {

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
        /// <param name="type">The type of action (attack/spell/heal).</param>
        /// <returns>The base damage of an action, before being modified by auras,
        /// traits, or critical/direct hits.</returns>
        public static double calculateActionDamage(double potency, double weaponDamage, 
            double attackPower, double determination, 
            Constants.JobID jobID, 
            Constants.ActionType type = Constants.ActionType.UNKNOWN)
        {
            // Base damage from job and weapon.
            double jobBaseDamage = Math.Truncate(Constants.BaseDetermination70 * Constants.JobMod(jobID) / 1000);
            double baseDamage = weaponDamage + jobBaseDamage;

            // Multiplier from Attack Power
            double apMultiplier = Math.Truncate(125 * (attackPower - Constants.BaseAttackPower70) / 292 + 100) / 100;

            // Total damage after Attack Power and Potency multipliers.
            double totalDamage = Math.Truncate(potency * baseDamage * apMultiplier / 100);

            // Multiplier from Determination.
            double determinationMultiplier = calculateDeterminationMultiplier(determination);
            
            // Total damage after all multipliers.
            return Math.Truncate(Math.Round(totalDamage * determinationMultiplier, 1));
        }

        public static double calculateAutoAttackDamage(double weaponDamage, double autoAttackDelay, double attackPower, 
            double determination, Constants.JobID jobID)
        {
            double jobBaseDamage = Math.Truncate(Constants.BaseDetermination70 * Constants.JobMod(jobID) / 1000);
            double baseDamage = (weaponDamage + jobBaseDamage) * (autoAttackDelay / 3);

            double apMultiplier = Math.Truncate(125 * (attackPower - Constants.BaseAttackPower70) / 292 + 100) / 100;

            double totalDamage = Math.Truncate(Constants.AutoAttackPotency(jobID) * baseDamage * apMultiplier / 100);

            double determinationMultiplier = calculateDeterminationMultiplier(determination);

            return Math.Truncate(Math.Round(totalDamage * determinationMultiplier, 1));
        }

        /// <summary>
        /// Calculates critical hit rate from the critical hit statistic.  This is the 
        /// critical hit rate BEFORE auras and traits.
        /// </summary>
        /// <param name="criticalHit">The critical hit statistic to convert.</param>
        /// <returns></returns>
        public static double calculateCriticalHitRate(double criticalHit)
        {
            return Math.Floor(Constants.CriticalHitGrowthModifier * (criticalHit - Constants.BaseCriticalHit70) / Constants.LevelGrowthPenalty70) / 1000 + 0.05;
        }

        /// <summary>
        /// Calculates the critical hit multiplier for a given critical hit statistic.
        /// </summary>
        /// <param name="criticalHit">The critical hit statistic to convert.</param>
        /// <returns></returns>
        public static double calculateCriticalHitMultiplier(double criticalHit)
        {
            return Math.Floor(Constants.CriticalHitGrowthModifier * (criticalHit - Constants.BaseCriticalHit70) / Constants.LevelGrowthPenalty70) / 1000 + Constants.BaseCriticalHitMultiplier;
        }

        /// <summary>
        /// Calculates the determination damage multiplier for a given determination statistic.
        /// </summary>
        /// <param name="determination">The determination statistic to be converted.</param>
        /// <returns></returns>
        public static double calculateDeterminationMultiplier(double determination)
        {
            return 1 + Math.Floor(Constants.DeterminationGrowthModifier * (determination - Constants.BaseDetermination70) / Constants.LevelGrowthPenalty70) / 1000;
        }

        /// <summary>
        /// Calculates the direct hit rate for a given direct hit statistic.
        /// </summary>
        /// <param name="directHit">The direct hit statistic to convert.</param>
        /// <returns></returns>
        public static double calculateDirectHitRate(double directHit)
        {
            return Math.Floor(Constants.DirectHitGrowthModifier * (directHit - Constants.BaseDirectHit70) / Constants.LevelGrowthPenalty70) / 1000;
        }

        public static double calculateTotalMana(double piety, Constants.JobID jobID)
        {
            return Math.Floor((Constants.MPMod(jobID) / 100) * (Constants.PietyGrowthModifier * (piety - Constants.BasePiety70) / Constants.LevelGrowthPenalty70 + Constants.BaseMana70));
        }


    }
}
