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
        /// Contextually roll the damage of an action.  The order of operations for ACTION
        /// damage is as follows:
        /// 
        /// D = ⌊ f(ptc) * f(wd) * f(ap) * f(det) * f(tnc) * traits ⌋ * f(chr) ⌋ 
        /// * f(dhr) ⌋ * rand[ 0.95, 1.05 ] ⌋ * buff_1 ⌋ * buff_2 ⌋ * ...
        /// (credit: TheoryJerks)
        /// 
        /// Using BattleCalculations functions, this translates to:
        /// 1. Calculate base damage (ptc, wd, ap, det, tnc, traits)
        /// 2. Roll critical hit multiplier (chr)
        /// 3. Roll direct hit multiplier (dhr)
        /// 4. Roll damage variance (5% variance)
        /// 5. Calculate aura multipliers. (buff_1, buff_2, ...)
        /// 
        /// </summary>
        /// <param name="source">The source actor performing the action.</param>
        /// <param name="target">The target being affected by the action.</param>
        /// <param name="action">The action being used.</param>
        /// <returns>Final damage value after all considerations.</returns>
        public static int RollActionDamage(IActor source, ITarget target, Action action) {

            double damage = _calculateActionBaseDamage(source, target, action);

            // Roll for critical and direct hits, and multiply.
            bool isCrit = RollConditionalSuccess(_calculateCriticalHitRate(source, target, action));
            bool isDirect = RollConditionalSuccess(_calculateDirectHitRate(source, target));
            if (isCrit) {
                damage = Math.Floor(damage * Formulas.calculateCriticalHitMultiplier(source.getStat(CharacterStat.CRITICALHIT)));
            }
            if (isDirect) {
                damage = Math.Floor(damage * Constants.DirectHitMultiplier);
            }

            // 5% damage variance.
            damage = _rollDamageVariance(damage);

            // Aura multipliers.
            var modifiers = _getAuraMultipliers(source, target, action.BaseAction.Aspect);
            foreach (double modifier in modifiers) {
                damage = Math.Floor(damage * modifier);
            }

            return (int)damage;
        }

        /// <summary>
        /// Contextually roll the damage of an aura.  The order of operations for AURA
        /// damage is as follows:
        /// 
        /// D = ⌊ f(ptc) * f(wd) * f(ap) * f(det) × f(tnc) * traits ⌋ * f(ss) ⌋ 
        /// * rand[0.95, 1.05] ⌋ * f(chr) ⌋ * f(dhr) ⌋ * buff_1 ⌋ * buff_2 ⌋ * ...
        /// (credit: TheoryJerks)
        /// 
        /// Using BattleCalculations functions, this translates to:
        /// 1. Retrieve base damage (ptc, wd, ap, det, tnc, traits, ss)
        /// 2. Roll damage variance (5% variance)
        /// 3. Roll critical hit multiplier (chr)
        /// 4. Roll direct hit multiplier (dhr)
        /// 5. Calculate aura multipliers. (buff_1, buff_2, ...)
        /// 
        /// </summary>
        /// <param name="aura">The aura instance being 'ticked'.</param>
        /// <returns>The rolled damage for the aura's tick.</returns>
        public static int RollAuraTick(Aura aura) {

            double damage = aura.BaseDamage;

            // 5% damage variance.
            damage = _rollDamageVariance(damage);

            // Roll for critical and direct hits, and multiply.
            bool isCrit = RollConditionalSuccess(aura.CriticalHitRate);
            bool isDirect = RollConditionalSuccess(aura.DirectHitRate);
            if (isCrit) {
                damage = Math.Floor(damage * aura.CriticalHitMultiplier);
            }
            if (isDirect) {
                damage = Math.Floor(damage * Constants.DirectHitMultiplier);
            }

            // Aura multipliers.
            foreach (double modifier in aura.DamageMultipliers) {
                damage = Math.Floor(damage * modifier);
            }

            return (int)damage;


        }

        /// <summary>
        /// Modifies an input aura to be used as a damage-over-time effect.  This is required
        /// as auras will "snapshot" character state and auras upon application, while individual
        /// damage events are calculated separately.
        /// </summary>
        /// <param name="source">The source actor applying the aura.</param>
        /// <param name="target">The target the aura is being applied to.</param>
        /// <param name="aura">The incomplete aura being applied.</param>
        public static void CalculateDoTProperties(IActor source, ITarget target, Aura aura) {
            aura.BaseDamage = _calculateAuraBaseDamage(source, target, aura.BaseAura);
            aura.CriticalHitRate = _calculateCriticalHitRate(source, target, aura: aura);
            aura.DirectHitRate = _calculateDirectHitRate(source, target);
            aura.CriticalHitMultiplier = Formulas.calculateCriticalHitMultiplier(source.getStat(CharacterStat.CRITICALHIT));
            aura.DamageMultipliers = _getAuraMultipliers(source, target, aura.BaseAura.DamageAspect);
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

        /// <summary>
        /// Contextually calculate the damage of an action before critical or direct hit
        /// calculations and before 5% variance. Requires initialized and active source 
        /// actor and target, as well as the action information.
        /// </summary>
        /// <param name="source">The source actor using the action.</param>
        /// <param name="target">The target being affected by the action.</param>
        /// <param name="action">The action being used.</param>
        /// <returns>Damage before RNG effects.</returns>
        private static double _calculateActionBaseDamage(IActor source, ITarget target, Action action) {

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

            return damage;

        }

        /// <summary>
        /// Contextually calculate the damage of an aura before critical or direct hit
        /// calculations and before 5% variance. Requires initialized and active source 
        /// actor and target, as well as the aura information.  This should be calculated
        /// on application of the aura, and attached to the active aura, as aura effects
        /// snapshot temporary effects (e.g. Trick Attack).
        /// </summary>
        /// <param name="source">The source actor using the action.</param>
        /// <param name="target">The target being affected by the action.</param>
        /// <param name="aura">The aura being applied.</param>
        /// <returns>Damage before RNG effects.</returns>
        public static int _calculateAuraBaseDamage(IActor source, ITarget target, BaseAura aura) {

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

            return (int)damage;
        }

        private static List<double> _getAuraMultipliers(IActor source, ITarget target, ActionAspect aspect) {

            List<double> damageModifiers = new List<double>();
            
            // Include source buff damage multipliers.
            foreach (Aura aura in source.Auras) {
                var baseAura = aura.BaseAura;

                if (baseAura.GlobalDamageModifier != 1) {
                    damageModifiers.Add(baseAura.GlobalDamageModifier);
                }

                if (aspect != ActionAspect.MAGIC && aspect != ActionAspect.UNASPECTED) {
                    if (baseAura.PhysicalDamageModifier != 1) {
                        damageModifiers.Add(baseAura.PhysicalDamageModifier);
                    }
                } else if (aspect == ActionAspect.MAGIC) {
                    if (baseAura.MagicDamageModifier != 1) {
                        damageModifiers.Add(baseAura.MagicDamageModifier);
                    }
                } else {
                    throw new ArgumentException("Action aspect is unknown: " + aspect.ToString());
                }
            }

            // Include target debuff damage multipliers.
            foreach (Aura aura in target.Auras) {
                var baseAura = aura.BaseAura;

                if (baseAura.DamageTakenModifier != 1) {
                    damageModifiers.Add(baseAura.DamageTakenModifier);
                }

                if (aspect == ActionAspect.BLUNT) {
                    if (baseAura.BluntResistanceModifier != 1) {
                        damageModifiers.Add(baseAura.BluntResistanceModifier);
                    }
                } else if (aspect == ActionAspect.PIERCING) {
                    if (baseAura.PiercingResistanceModifier != 1) {
                        damageModifiers.Add(baseAura.PiercingResistanceModifier);
                    }
                } else if (aspect == ActionAspect.SLASHING) {
                    if (baseAura.PiercingResistanceModifier != 1) {
                        damageModifiers.Add(baseAura.PiercingResistanceModifier);
                    }
                }
            }

            return damageModifiers;
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
        private static double _calculateCriticalHitRate(IActor source, ITarget target, Action action = null, Aura aura = null) {

            double rate = source.getCriticalHitRate(action);

            foreach (Aura a in source.Auras) {
                rate += a.BaseAura.CriticalHitRateModifier;
            }

            if (target != null) {
                foreach (Aura a in target.Auras) {
                    rate += a.BaseAura.IncomingCriticalHitRateModifier;
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
        private static double _calculateDirectHitRate(IActor source, ITarget target) {

            double rate = source.getDirectHitRate();

            foreach (Aura a in source.Auras) {
                rate += a.BaseAura.DirectHitRateModifier;
            }

            return Math.Min(1, rate);
        }

        private static int _rollDamageVariance(double damage) {

            int min = (int)(damage * 0.95);
            int max = (int)(damage * 1.05);

            return min + r.Next(max - min);
        }

    }
}
