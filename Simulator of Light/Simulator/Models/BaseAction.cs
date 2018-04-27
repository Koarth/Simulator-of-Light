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
        private string _name;
        private JobID _jobID;
        private PrimaryStat _stat;
        private ActionAspect _aspect;

        private bool _isOGCD;
        private double _potency;
        private double _mpCost;
        private double _tpCost;
        private double _castTime;
        private double _recastTime;
        private double _range;
        private double _radius;

        private List<BaseAura> _aurasApplied;

        [JsonConstructor]
        public BaseAction(string name, JobID jobID, PrimaryStat stat, ActionAspect aspect, bool isOGCD,
            double potency, double mpCost, double tpCost, double castTime, double recastTime, double range,
            double radius, List<BaseAura> aurasApplied) {

            Name = name;
            JobID = jobID;
            Stat = stat;
            Aspect = aspect;
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

        public string Name { get => _name; private set => _name = value; }
        public JobID JobID { get => _jobID; private set => _jobID = value; }
        public ActionAspect Aspect { get => _aspect; private set => _aspect = value; }

        [DefaultValue(PrimaryStat.UNKNOWN)]
        public PrimaryStat Stat { get => _stat; private set => _stat = value; }
        [DefaultValue(false)]
        public bool IsOGCD { get => _isOGCD; private set => _isOGCD = value; }
        [DefaultValue(0)]
        public double Potency { get => _potency; private set => _potency = value; }
        [DefaultValue(0)]
        public double MpCost { get => _mpCost; private set => _mpCost = value; }
        [DefaultValue(0)]
        public double TpCost { get => _tpCost; private set => _tpCost = value; }
        [DefaultValue(0)]
        public double CastTime { get => _castTime; private set => _castTime = value; }
        [DefaultValue(0)]
        public double RecastTime { get => _recastTime; private set => _recastTime = value; }
        [DefaultValue(3)]
        public double Range { get => _range; private set => _range = value; }
        [DefaultValue(0)]
        public double Radius { get => _radius; private set => _radius = value; }
        [DefaultValue(null)]
        public List<BaseAura> AurasApplied { get => _aurasApplied; private set => _aurasApplied = value; }
    }
}
