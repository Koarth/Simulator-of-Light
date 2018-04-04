using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Resources;
using static Simulator_of_Light.Simulator.Resources.Constants;
using Newtonsoft.Json;

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
        private double _range;
        private double _radius;

        public BaseAction(string name)
        {
            // TODO: retrieve base aura information from configuration
            throw new NotImplementedException();
        }

        [JsonConstructor]
        public BaseAction(string name, JobID jobID, ActionType type, ActionAspect aspect, bool ogcd, double potency,
            double mpCost, double tpCost, double castTime, double recastTime, double range, double radius)
        {
            _name = name;
            _jobID = jobID;
            _type = type;
            _aspect = aspect;
            _ogcd = ogcd;
            _potency = potency;
            _mpCost = mpCost;
            _tpCost = tpCost;
            _castTime = castTime;
            _recastTime = recastTime;
            _range = range;
            _radius = radius;
        }

        public string Name { get => _name; }
        public JobID JobID { get => _jobID; }
        public ActionType Type { get => _type; }
        public ActionAspect Aspect { get => _aspect; }
        public bool Ogcd { get => _ogcd; }
        public double Potency { get => _potency; }
        public double MpCost { get => _mpCost; }
        public double TpCost { get => _tpCost; }
        public double CastTime { get => _castTime; }
        public double RecastTime { get => _recastTime; }
        public double Range { get => _range; }
        public double Radius { get => _radius; }
    }
}
