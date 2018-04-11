﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Simulator_of_Light.Simulator.Resources.Constants;

namespace Simulator_of_Light.Simulator.Models {

    public sealed class BaseAura {

        // Aura properties
        private string _name;
        private double _duration;

        private double _damageOverTimePotency;
        private double _healingOverTimePotency;
        private ActionAspect _damageAspect;

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

        public BaseAura(string name) {
            // TODO: retrieve base aura information from configuration
            throw new NotImplementedException();
        }

        public string Name { get => _name; private set => _name = value; }
        public double Duration { get => _duration; private set => _duration = value; }

        public double DamageOverTimePotency { get => _damageOverTimePotency; private set => _damageOverTimePotency = value; }
        public double HealingOverTimePotency { get => _healingOverTimePotency; private set => _healingOverTimePotency = value; }
        public ActionAspect DamageAspect { get => _damageAspect; private set => _damageAspect = value; }

        public double MagicDamageModifier { get => _magicDamageModifier; private set => _magicDamageModifier = value; }
        public double HealingSpellModifier { get => _healingSpellModifier; private set => _healingSpellModifier = value; }
        public double PhysicalDamageModifier { get => _physicalDamageModifier; private set => _physicalDamageModifier = value; }
        public double GlobalDamageModifier { get => _globalDamageModifier; private set => _globalDamageModifier = value; }

        public double CastTimeModifier { get => _castTimeModifier; private set => _castTimeModifier = value; }
        public double CastTimeModifierFlat { get => _castTimeModifierFlat; private set => _castTimeModifierFlat = value; }
        public double RecastTimeModifier { get => _recastTimeModifier; private set => _recastTimeModifier = value; }
        public double AutoAttackDelayModifier { get => _autoAttackDelayModifier; private set => _autoAttackDelayModifier = value; }

        public double CriticalHitRateModifier { get => _criticalHitRateModifier; private set => _criticalHitRateModifier = value; }
        public double DirectHitRateModifier { get => _directHitRateModifier; private set => _directHitRateModifier = value; }

        public double MpCostModifier { get => _mpCostModifier; private set => _mpCostModifier = value; }

        public double SlashingResistanceModifier { get => _slashingResistanceModifier; private set => _slashingResistanceModifier = value; }
        public double PiercingResistanceModifier { get => _piercingResistanceModifier; private set => _piercingResistanceModifier = value; }
        public double BluntResistanceModifier { get => _bluntResistanceModifier; private set => _bluntResistanceModifier = value; }
        public double DamageTakenModifier { get => _damageTakenModifier; private set => _damageTakenModifier = value; }
        public double IncomingCriticalHitRateModifier { get => _incomingCriticalHitRateModifier; private set => _incomingCriticalHitRateModifier = value; }

    }
}
