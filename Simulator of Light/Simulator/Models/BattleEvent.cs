using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models {
    public class BattleEvent {

        private BattleEventType _type;
        private long _time;

        // Involved entities.
        private IActor _source;
        private ITarget _target;

        private Aura _aura;

        // Action information.
        private BaseAction _ability;

        // Damage values.
        private int _rawDamage;
        private int _effectiveDamage;
        private int _absorbedDamage;
        private int _blockedDamage;
        private int _overkillDamage;

        // Healing values.
        private int _rawHealing;
        private int _effectiveHealing;
        private int _absorbedHealing;

        // Modifiers.
        private bool _isCritical;
        private bool _isDirectHit;
        private bool _isTick;

        // Aura information.
        private BaseAura _auraApplied;
        private int _auraStack;

    }
}
