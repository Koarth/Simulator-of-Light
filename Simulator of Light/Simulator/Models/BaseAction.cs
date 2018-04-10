using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Resources;
using static Simulator_of_Light.Simulator.Resources.Constants;
using Newtonsoft.Json;

namespace Simulator_of_Light.Simulator.Models {

    public sealed class BaseAction {

        public BaseAction(string name) {
            // TODO: retrieve base aura information from configuration
            throw new NotImplementedException();
        }

        [JsonConstructor]
        public BaseAction(string name, JobID jobID, ActionType type, ActionAspect aspect, bool isOGCD, 
            double potency, double mpCost, double tpCost,double castTime,double recastTime,double range,
            double radius, List<BaseAura> aurasApplied = null) {

            Name = name;
            JobID = jobID;
            Type = type;
            Aspect = aspect;
            IsOGCD = isOGCD;
            Potency = potency;
            MpCost = mpCost;
            TpCost = tpCost;
            CastTime = castTime;
            RecastTime = recastTime;
            Range = range;
            Radius = radius;
            AurasApplied = null; //TODO
        }

        public string Name { get; private set; }
        public JobID JobID { get; private set; }
        public ActionType Type { get; private set; }
        public ActionAspect Aspect { get; private set; }

        [DefaultValue(false)]
        public bool IsOGCD { get; private set; }
        [DefaultValue(0)]
        public double Potency { get; private set; }
        [DefaultValue(0)]
        public double MpCost { get; private set; }
        [DefaultValue(0)]
        public double TpCost { get; private set; }
        [DefaultValue(0)]
        public double CastTime { get; private set; }
        [DefaultValue(0)]
        public double RecastTime { get; private set; }
        [DefaultValue(3)]
        public double Range { get; private set; }
        [DefaultValue(0)]
        public double Radius { get; private set; }
        [DefaultValue(null)]
        public List<BaseAura> AurasApplied { get; private set; }
    }
}
