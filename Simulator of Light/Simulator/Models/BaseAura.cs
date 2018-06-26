using Newtonsoft.Json;
using System.ComponentModel;

namespace Simulator_of_Light.Simulator.Models {

    public sealed class BaseAura {

        [JsonConstructor]
        public BaseAura(string name, JobID jobID, double duration, AuraTarget targets,
            double damageOverTimePotency, double healingOverTimePotency, ActionAspect damageAspect, 
            double refreshPotency, double magicDamageModifier, double healingSpellModifier, 
            double physicalDamageModifier, double globalDamageModifier, double type1Haste, 
            double type2Haste, double autoAttackDelayModifier, 
            double criticalHitRateModifier, double directHitRateModifier, double mpCostModifier, 
            double slashingResistanceModifier, double piercingResistanceModifier, 
            double bluntResistanceModifier, double damageTakenModifier, 
            double incomingCriticalHitRateModifier) {

            Name = name;
            JobID = jobID;
            Duration = duration;
            Targets = targets;

            DamageOverTimePotency = damageOverTimePotency;
            HealingOverTimePotency = healingOverTimePotency;
            DamageAspect = damageAspect;
            RefreshPotency = refreshPotency;

            MagicDamageModifier = magicDamageModifier;
            HealingSpellModifier = healingSpellModifier;
            PhysicalDamageModifier = physicalDamageModifier;
            GlobalDamageModifier = globalDamageModifier;

            Type1Haste = type1Haste;
            Type2Haste = type2Haste;
            AutoAttackDelayModifier = autoAttackDelayModifier;

            CriticalHitRateModifier = criticalHitRateModifier;
            DirectHitRateModifier = directHitRateModifier;

            MpCostModifier = mpCostModifier;

            SlashingResistanceModifier = slashingResistanceModifier;
            PiercingResistanceModifier = piercingResistanceModifier;
            BluntResistanceModifier = bluntResistanceModifier;
            DamageTakenModifier = damageTakenModifier;
            IncomingCriticalHitRateModifier = incomingCriticalHitRateModifier;
        }

        override public string ToString() {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());
        }

        // Aura properties
        public string Name { get; private set; }
        public JobID JobID { get; private set; }
        public double Duration { get; private set; }
        [DefaultValue(AuraTarget.TARGET)]
        public AuraTarget Targets { get; private set; }

        public double DamageOverTimePotency { get; private set; }
        public double HealingOverTimePotency { get; private set; }
        public ActionAspect DamageAspect { get; private set; }
        public double RefreshPotency { get; private set; }

        // Properties affecting outgoing actions
        public double MagicDamageModifier { get; private set; }
        public double HealingSpellModifier { get; private set; }
        public double PhysicalDamageModifier { get; private set; }
        public double GlobalDamageModifier { get; private set; }

        public double Type1Haste { get; private set; }
        public double Type2Haste { get; private set; }
        public double AutoAttackDelayModifier { get; private set; }

        public double CriticalHitRateModifier { get; private set; }
        public double DirectHitRateModifier { get; private set; }

        public double MpCostModifier { get; private set; }

        // Properties affecting incoming actions
        public double SlashingResistanceModifier { get; private set; }
        public double PiercingResistanceModifier { get; private set; }
        public double BluntResistanceModifier { get; private set; }
        public double DamageTakenModifier { get; private set; }
        public double IncomingCriticalHitRateModifier { get; private set; }

    }
}
