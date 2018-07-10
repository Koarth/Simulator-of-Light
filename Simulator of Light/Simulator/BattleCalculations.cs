using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Models;
using Simulator_of_Light.Simulator.Resources;
using Action = Simulator_of_Light.Simulator.Models.Action;

namespace Simulator_of_Light.Simulator {
    public static class BattleCalculations {

        // Allow RNG to be overridden for testing purposes.
        public static Random r = new Random();

        /// <summary>
        /// Contextually calculate the damage of an action before critical or direct hit
        /// calculations and before 5% variance. Requires initialized and active source 
        /// actor and target, as well as the action information.
        /// </summary>
        /// <param name="action">The action being used.</param>
        /// <param name="source">The source actor using the action.</param>
        /// <param name="target">The target being affected by the action.</param>
        /// <returns>Damage before RNG effects.</returns>
        public static int CalculateDamage(Action action, IActor source, ITarget target) {

            var potency = action.BaseAction.Potency;

            if (potency == 0) {
                return 0;
            }

            double damage = 0;
            var primaryStat = Constants.getDefaultPrimaryStat(source.JobID);

            // Collect relevant stats and multipliers.
            var weaponDamage = source.getStat(CharacterStat.WEAPONDAMAGE);
            var attackPower = source.getStat(CharacterStat.ATTACKPOWER);
            var determinationMultiplier = Formulas.calculateDeterminationMultiplier(source.getStat(CharacterStat.DETERMINATION));
            var tenacityMultiplier = Formulas.calculateTenacityMultiplier(source.getStat(CharacterStat.TENACITY));

            // Base damage, innate actor multipliers (determination, tenacity, traits)
            damage = Formulas.calculateActionDamage(potency, weaponDamage, attackPower, source.JobID, primaryStat);
            damage = damage * determinationMultiplier;
            damage = damage * tenacityMultiplier;
            damage = Math.Floor(damage * Constants.getTraitDamageModifier(source.JobID));

            // Include source buff damage multipliers.
            foreach (Aura aura in source.Auras) {
                var baseAura = aura.BaseAura;

                damage = Math.Floor(damage * baseAura.GlobalDamageModifier);

                if (action.BaseAction.Aspect != ActionAspect.MAGIC && action.BaseAction.Aspect != ActionAspect.UNASPECTED) {
                    damage = Math.Floor(damage * baseAura.PhysicalDamageModifier);
                } else if (action.BaseAction.Aspect == ActionAspect.MAGIC) {
                    damage = Math.Floor(damage * baseAura.MagicDamageModifier);
                } else {
                    throw new ArgumentException("Action aspect is unknown: " + action.BaseAction.Name);
                }
            }

            // Include target debuff damage multipliers.
            foreach (Aura aura in target.Auras) {
                var baseAura = aura.BaseAura;

                damage = Math.Floor(damage * baseAura.DamageTakenModifier);

                if (action.BaseAction.Aspect == ActionAspect.BLUNT) {
                    damage = Math.Floor(damage * baseAura.BluntResistanceModifier);
                } else if (action.BaseAction.Aspect == ActionAspect.PIERCING) {
                    damage = Math.Floor(damage * baseAura.PiercingResistanceModifier);
                } else if (action.BaseAction.Aspect == ActionAspect.SLASHING) {
                    damage = Math.Floor(damage * baseAura.SlashingResistanceModifier);
                }
            }

            return (int)damage;
        }

        /// <summary>
        /// Contextually calculate the damage of an aura before critical or direct hit
        /// calculations and before 5% variance. Requires initialized and active source 
        /// actor and target, as well as the aura information.  This should be calculated
        /// on application of the aura, and attached to the active aura, as aura effects
        /// snapshot temporary effects (e.g. Trick Attack).
        /// </summary>
        /// <param name="aura">The aura being applied.</param>
        /// <param name="source">The source actor using the action.</param>
        /// <param name="target">The target being affected by the action.</param>
        /// <returns>Damage before RNG effects.</returns>
        public static int CalculateDamage(BaseAura aura, IActor source, ITarget target) {

            var potency = aura.DamageOverTimePotency;

            if (potency == 0) {
                return 0;
            }

            double damage = 0;
            var primaryStat = Constants.getDefaultPrimaryStat(source.JobID);

            // Collect relevant stats and multipliers.
            var weaponDamage = source.getStat(CharacterStat.WEAPONDAMAGE);
            var attackPower = source.getStat(CharacterStat.ATTACKPOWER);
            var determinationMultiplier = Formulas.calculateDeterminationMultiplier(source.getStat(CharacterStat.DETERMINATION));
            var tenacityMultiplier = Formulas.calculateTenacityMultiplier(source.getStat(CharacterStat.TENACITY));
            var speedMultiplier = Formulas.calculateSpeedMultiplier(source.getStat(CharacterStat.SPEED));

            // Base damage, innate actor multipliers (determination, tenacity, speed, traits)
            damage = Formulas.calculateActionDamage(potency, weaponDamage, attackPower, source.JobID, primaryStat);
            damage = damage * determinationMultiplier;
            damage = damage * tenacityMultiplier;
            damage = Math.Floor(damage * Constants.getTraitDamageModifier(source.JobID));
            damage = Math.Floor(damage * speedMultiplier);

            // Include source buff damage multipliers.
            foreach (Aura a in source.Auras) {
                var baseAura = a.BaseAura;

                damage = Math.Floor(damage * baseAura.GlobalDamageModifier);

                if (aura.DamageAspect != ActionAspect.MAGIC && aura.DamageAspect != ActionAspect.UNASPECTED) {
                    damage = Math.Floor(damage * baseAura.PhysicalDamageModifier);
                } else if (aura.DamageAspect == ActionAspect.MAGIC) {
                    damage = Math.Floor(damage * baseAura.MagicDamageModifier);
                } else {
                    throw new ArgumentException("Action aspect is unknown: " + aura.Name);
                }
            }

            // Include target debuff damage multipliers.
            foreach (Aura a in target.Auras) {
                var baseAura = a.BaseAura;

                damage = Math.Floor(damage * baseAura.DamageTakenModifier);

                if (aura.DamageAspect == ActionAspect.BLUNT) {
                    damage = Math.Floor(damage * baseAura.BluntResistanceModifier);
                } else if (aura.DamageAspect == ActionAspect.PIERCING) {
                    damage = Math.Floor(damage * baseAura.PiercingResistanceModifier);
                } else if (aura.DamageAspect == ActionAspect.SLASHING) {
                    damage = Math.Floor(damage * baseAura.SlashingResistanceModifier);
                }
            }

            return (int)damage;
        }

        /// <summary>
        /// Contextually calculates the critical hit rate for an action.
        /// This will account for aura effects on the source actor, and the 
        /// target if applicable.  
        /// 
        /// This function will defer to the source actor to determine the
        /// base critical hit rate before auras, as a subclass of actor
        /// may adjust the outgoing critical hit rate contextually.
        /// (e.g. Warrior's beast gauge effects crit rate, Bootshine is a 
        /// guaranteed crit when performed from the rear in Opo-opo Form, etc.)
        /// </summary>
        /// <param name="source">Source actor performing the action.</param>
        /// <param name="target">Target the action will affect.</param>
        /// <param name="action">The action being used.</param>
        /// <returns>The current critical hit rate for the action being used.</returns>
        public static double CalculateCriticalHitRate(IActor source, ITarget target, Action action) {

            double rate = source.getCriticalHitRate(action);

            foreach (Aura aura in source.Auras) {
                rate += aura.BaseAura.CriticalHitRateModifier;
            }

            if (target != null) {
                foreach (Aura aura in target.Auras) {
                    rate += aura.BaseAura.IncomingCriticalHitRateModifier;
                }
            }

            return Math.Min(1, rate);
        }


        /// <summary>
        /// Contextually calculates the direct hit rate for an action.
        /// This will account for aura effects on the source actor, and the 
        /// target if applicable.  
        /// 
        /// This function will defer to the source actor to determine the
        /// base critical hit rate before auras, as a subclass of actor
        /// may adjust the outgoing direct hit rate contextually.
        /// There are no non-aura effects that do this currently, but
        /// they may be added in the future.
        /// <param name="source">Source actor performing the action.</param>
        /// <param name="target">Target the action will affect.</param>
        /// <param name="action">The action being used.</param>
        /// <returns>The current direct hit rate for the action being used.</returns>
        public static double CalculateDirectHitRate(IActor source, ITarget target, Action action) {

            double rate = source.getDirectHitRate();

            foreach (Aura aura in source.Auras) {
                rate += aura.BaseAura.DirectHitRateModifier;
            }

            return Math.Min(1, rate);
        }

        /// <summary>
        /// Basic function to roll for a success or failure against the
        /// given proc rate.  The rate given can be a critical hit rate,
        /// a direct hit rate, or an individual action's (e.g. Verfire).
        /// 
        /// Rates should be given as a double between 0 and 1, and are
        /// expected to have a precision of 3 decimal places.
        /// (XX.X% success rate)
        /// </summary>
        /// <param name="rate">The success rate to be rolled against.</param>
        /// <returns>True if the roll succeeded, false otherwise.</returns>
        public static bool RollConditionalSuccess(double rate) {
            int maximum = (int)(rate * 1000);
            int roll = BattleCalculations.r.Next(1000) + 1;

            return (roll <= maximum);
        }

    }
}
