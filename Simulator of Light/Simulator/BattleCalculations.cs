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

        public static Random r = new Random();

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

        public static double CalculateCriticalHitRate(IActor source, ITarget target, Action action) {

            double rate = source.getCriticalHitRate(action);

            foreach (Aura aura in source.Auras) {
                rate += aura.BaseAura.CriticalHitRateModifier;
            }

            return Math.Min(1, rate);
        }

        public static double CalculateDirectHitRate(IActor source, ITarget target, Action action) {

            double rate = source.getDirectHitRate();

            foreach (Aura aura in source.Auras) {
                rate += aura.BaseAura.IncomingCriticalHitRateModifier;
            }

            return Math.Min(1, rate);
        }

        public static bool RollConditionalSuccess(double rate) {

            // Proc rates use 1 decimal of precision (XX.X% success rate)
            int maximum = (int)(rate * 1000);
            int roll = BattleCalculations.r.Next(1000) + 1;

            return (roll <= maximum);
        }




    }
}
