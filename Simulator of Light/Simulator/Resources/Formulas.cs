using System;
using static System.Double;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Resources
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
            System.Diagnostics.Debug.WriteLine("JOBID: " + jobID.ToString());
            System.Diagnostics.Debug.WriteLine("ActionType " + type.ToString());

            double jobBaseDamage = Math.Truncate(Constants.BaseDetermination70 * Constants.JobMod(jobID) / 1000);
            double baseDamage = weaponDamage + jobBaseDamage;
            System.Diagnostics.Debug.WriteLine("Base Job Damage: " + jobBaseDamage.ToString());
            System.Diagnostics.Debug.WriteLine("Base Damage: " + baseDamage.ToString());

            double apMultiplier = Math.Truncate(125 * (attackPower - Constants.AttackPowerDivisor70) / 292 + 100) / 100;
            /*double powerAdded = Math.Truncate(
                (
                attackPower / Constants.AttackPowerDivisor70 + 
                (1 - Constants.BaseDetermination70 / Constants.AttackPowerDivisor70)
                ) 
                * 100)
                / 100;*/
            System.Diagnostics.Debug.WriteLine("Power Multiplier: " + apMultiplier.ToString());

            double totalDamage = Math.Truncate(potency * baseDamage * apMultiplier / 100);

            double determinationMultiplier = calculateDeterminationMultiplier(determination);
            System.Diagnostics.Debug.WriteLine("Determination Multiplier: " + determinationMultiplier.ToString());
            System.Diagnostics.Debug.WriteLine("Final value: " + Math.Truncate(Math.Round(totalDamage * determinationMultiplier, 1)).ToString());
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
            return Math.Floor(Constants.CriticalHitModifier * (criticalHit - Constants.BaseCriticalHit70) / Constants.SecondaryLevelMod70) / 1000 + 0.05;
        }

        /// <summary>
        /// Calculates the critical hit multiplier for a given critical hit statistic.
        /// </summary>
        /// <param name="criticalHit">The critical hit statistic to convert.</param>
        /// <returns></returns>
        public static double calculateCriticalHitMultiplier(double criticalHit)
        {
            return Math.Floor(Constants.CriticalHitModifier * (criticalHit - Constants.BaseCriticalHit70) / Constants.SecondaryLevelMod70) / 1000 + Constants.BaseCriticalHitMultiplier;
        }

        /// <summary>
        /// Calculates the determination damage multiplier for a given determination statistic.
        /// </summary>
        /// <param name="determination">The determination statistic to be converted.</param>
        /// <returns></returns>
        public static double calculateDeterminationMultiplier(double determination)
        {
            return 1 + Math.Floor(Constants.DeterminationModifier * (determination - Constants.BaseDetermination70) / Constants.SecondaryLevelMod70) / 1000;
        }

        /// <summary>
        /// Calculates the direct hit rate for a given direct hit statistic.
        /// </summary>
        /// <param name="directHit">The direct hit statistic to convert.</param>
        /// <returns></returns>
        public static double calculateDirectHitRate(double directHit)
        {
            return Math.Floor(Constants.DirectHitModifier * (directHit - Constants.BaseDirectHit70) / Constants.SecondaryLevelMod70) / 1000;
        }


    }
}
