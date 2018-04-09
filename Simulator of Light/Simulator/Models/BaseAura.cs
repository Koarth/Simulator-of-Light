using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Simulator_of_Light.Simulator.Resources.Constants;

namespace Simulator_of_Light.Simulator.Models {

    public sealed class BaseAura {

        // Flyweight for base auras.
        private static Dictionary<string, BaseAura> _baseAuras = new Dictionary<string, BaseAura>();

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


        public BaseAura(string name) {
            // TODO: retrieve base aura information from configuration
            throw new NotImplementedException();
        }
    }
}
