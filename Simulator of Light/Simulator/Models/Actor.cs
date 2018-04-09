using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Simulator_of_Light.Simulator.Resources.Constants;

namespace Simulator_of_Light.Simulator.Models {

    public class Actor {

        // Static properties
        private string _name;
        private JobID _jobID;

        // Dynamic properties
        private double _hp;
        private double _mp;
        private double _tp;
    }
}
