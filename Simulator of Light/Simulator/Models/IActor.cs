﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models {
    public interface IActor {

        string Name { get; }
        SortedList<long, Aura> Auras { get; }

        QueuedEvent DecideAction(long time,
            ITarget[] friendlyTargets,
            ITarget[] enemyTargets);

    }
}
