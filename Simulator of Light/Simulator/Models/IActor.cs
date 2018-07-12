using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C5;

namespace Simulator_of_Light.Simulator.Models {
    public interface IActor : ITarget {

        JobID JobID { get; }

        BattleEvent DecideAction(long time,
            ITarget[] friendlyTargets,
            ITarget[] enemyTargets);
        long BeginCast(Action action, long time);
        BattleEvent[] ExecuteAction(Action action, long time);

        double getStat(CharacterStat statID);

        double getCriticalHitRate(Action action);
        double getDirectHitRate();
    }
}
