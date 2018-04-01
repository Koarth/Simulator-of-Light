using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Resources;
using static Simulator_of_Light.Simulator.Resources.Constants;

namespace Simulator_of_Light.Simulator.Models
{
    public sealed class Aura
    {

        // Variable modifiers
        private double _damageModifier;
        private double _criticalHitRate;
        private double _directHitRate;
        private Actor _source;

        // Aura properties
        private string _name;
        private double _duration;

        private double _damageOverTimePotency;
        private ActionAspect _damageAspect;
        private double _healingOverTimePotency;

        // Properties affecting outgoing actions
        private double _magicDamageModifier;
        private double _healingSpellModifier;
        private double _physicalDamageModifier;
        private double _globalDamageModifier;

        private double _castTimeModifier;
        private double _castTimeModifierFlat;
        private double _recastTimeModifier;
        private double _autoAttackDelayModifier;

        private double _criticalHitRateModifier;
        private double _directHitRateModifier;

        private double _mpCostModifier;

        // Properties affecting incoming actions
        private double _slashingResistanceModifier;
        private double _piercingResistanceModifier;
        private double _bluntResistanceModifier;
        private double _damageTakenModifier;
        private double _incomingCriticalHitRateModifier;

        public double DamageModifier { get => _damageModifier; set => _damageModifier = value; }
        public double CriticalHitRate { get => _criticalHitRate; set => _criticalHitRate = value; }
        public double DirectHitRate { get => _directHitRate; set => _directHitRate = value; }

        public string Name { get => _name; set => _name = value; }
        public double Duration { get => _duration; set => _duration = value; }
        internal Actor Source { get => _source; set => _source = value; }

        public double DamageOverTimePotency { get => _damageOverTimePotency; set => _damageOverTimePotency = value; }
        public ActionAspect DamageAspect { get => _damageAspect; set => _damageAspect = value; }
        public double HealingOverTimePotency { get => _healingOverTimePotency; set => _healingOverTimePotency = value; }

        public double MagicDamageModifier { get => _magicDamageModifier; set => _magicDamageModifier = value; }
        public double HealingSpellModifier { get => _healingSpellModifier; set => _healingSpellModifier = value; }
        public double PhysicalDamageModifier { get => _physicalDamageModifier; set => _physicalDamageModifier = value; }
        public double GlobalDamageModifier { get => _globalDamageModifier; set => _globalDamageModifier = value; }

        public double CastTimeModifier { get => _castTimeModifier; set => _castTimeModifier = value; }
        public double CastTimeModifierFlat { get => _castTimeModifierFlat; set => _castTimeModifierFlat = value; }
        public double RecastTimeModifier { get => _recastTimeModifier; set => _recastTimeModifier = value; }
        public double AutoAttackDelayModifier { get => _autoAttackDelayModifier; set => _autoAttackDelayModifier = value; }

        public double CriticalHitRateModifier { get => _criticalHitRateModifier; set => _criticalHitRateModifier = value; }
        public double DirectHitRateModifier { get => _directHitRateModifier; set => _directHitRateModifier = value; }

        public double MpCostModifier { get => _mpCostModifier; set => _mpCostModifier = value; }

        public double SlashingResistanceModifier { get => _slashingResistanceModifier; set => _slashingResistanceModifier = value; }
        public double PiercingResistanceModifier { get => _piercingResistanceModifier; set => _piercingResistanceModifier = value; }
        public double BluntResistanceModifier { get => _bluntResistanceModifier; set => _bluntResistanceModifier = value; }
        public double DamageTakenModifier { get => _damageTakenModifier; set => _damageTakenModifier = value; }

        public double IncomingCriticalHitRateModifier { get => _incomingCriticalHitRateModifier; set => _incomingCriticalHitRateModifier = value; }
        
    }
}
