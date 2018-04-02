using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Simulator_of_Light.Simulator.Resources.Constants;

namespace Simulator_of_Light.Simulator.Models
{
    public sealed class BaseAction
    {

        private string _name;
        private JobID _jobID;
        private ActionType _type;
        private ActionAspect _aspect;
        private bool _ogcd;
        private double _potency;
        private double _mpCost;
        private double _tpCost;
        private double _castTime;
        private double _recastTime;
        private double range;
        private double radius;

        public BaseAction(string name)
        {
            // TODO: retrieve base aura information from configuration
            throw new NotImplementedException();
        }

    }
}
