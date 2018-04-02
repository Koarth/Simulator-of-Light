﻿using System;
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

        private BaseAura _baseAura;
        
        private double _damageModifier;
        private double _criticalHitRate;
        private double _directHitRate;
        private Actor _source;

        public BaseAura BaseAura { get => _baseAura; set => BaseAura = value; }
        public double DamageModifier { get => _damageModifier; set => _damageModifier = value; }
        public double CriticalHitRate { get => _criticalHitRate; set => _criticalHitRate = value; }
        public double DirectHitRate { get => _directHitRate; set => _directHitRate = value; }
        public Actor Source { get => _source; set => _source = value; }
        
    }
}
