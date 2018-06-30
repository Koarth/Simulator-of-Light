using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C5;

namespace Simulator_of_Light.Simulator.Models {
    public interface IActor : ITarget {

        QueuedEvent DecideAction(long time,
            ITarget[] friendlyTargets,
            ITarget[] enemyTargets);
        long BeginCast(Action action, long time);
        QueuedEvent[] ExecuteAction(Action action, long time);



    }
}
