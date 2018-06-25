using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Simulator_of_Light.Simulator.Models {

    public sealed class BaseAction {

        [JsonConstructor]
        public BaseAction(string name, JobID jobID, CharacterStat stat, ActionAspect aspect, 
            ActionTarget targets, bool isOGCD, double potency, int mpCost, 
            int tpCost, double castTime, double recastTime, double range, 
            double radius, List<BaseAura> aurasApplied) {

            Name = name;
            JobID = jobID;
            Stat = stat;
            Aspect = aspect;
            Targets = targets;
            IsOGCD = isOGCD;
            Potency = potency;
            MpCost = mpCost;
            TpCost = tpCost;
            CastTime = castTime;
            RecastTime = recastTime;
            Range = range;
            Radius = radius;
            AurasApplied = aurasApplied;

        }

        override public string ToString() {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());
        }

        public string Name { get; private set; }
        public JobID JobID { get; private set; }
        public ActionAspect Aspect { get; private set; }

        [DefaultValue(ActionTarget.TARGET)]
        public ActionTarget Targets { get; private set; }
        [DefaultValue(CharacterStat.UNKNOWN)]
        public CharacterStat Stat { get; private set; }
        [DefaultValue(false)]
        public bool IsOGCD { get; private set; }
        [DefaultValue(0)]
        public double Potency { get; private set; }
        [DefaultValue(0)]
        public int MpCost { get; private set; }
        [DefaultValue(0)]
        public int TpCost { get; private set; }
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
