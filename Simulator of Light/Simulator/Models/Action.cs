using System;
using static Simulator_of_Light.Simulator.Resources.Constants;

namespace Simulator_of_Light.Simulator.Models
{

    public sealed class Action
    {
        private string _name;
        private JobID _jobID;
        private ActionType _type;
        private ActionAspect _aspect;
        private double _potency;
        private double _mpCost;
        private double _tpCost;
        private double _cast;
        private double _recast;

        public string Name { get => _name; set => _name = value; }
        public JobID JobID { get => _jobID; set => _jobID = value; }
        public ActionType Type { get => _type; set => _type = value; }
        public ActionAspect Aspect { get => _aspect; set => _aspect = value; }
        public double Potency { get => _potency; set => _potency = value; }
        public double MpCost { get => _mpCost; set => _mpCost = value; }
        public double TpCost { get => _tpCost; set => _tpCost = value; }
        public double Cast { get => _cast; set => _cast = value; }
        public double Recast { get => _recast; set => _recast = value; }

        public Action(string name, JobID jobID, ActionType type, ActionAspect aspect,
            double potency, double mpCost, double tpCost, double cast, double recast)
        {
            Name = name;
            JobID = jobID;
            Type = type;
            Aspect = aspect;
            Potency = potency;
            MpCost = mpCost;
            TpCost = tpCost;
            Cast = cast;
            Recast = recast;
        }

        private Action()
        {

        }

    }

}