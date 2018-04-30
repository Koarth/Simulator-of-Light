using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models {

    public class Actor {

        // Static properties
        private string _name;
        private JobID _jobID;

        // Dynamic properties
        private double _hp;
        private double _mp;
        private double _tp;
        private Dictionary<string, Action> _actions;

        public Actor(string name, JobID jobID) {
            _name = name;
            _jobID = jobID;

            _actions = new Dictionary<string, Action>();
            var actionDict = BaseActionFactory.getBaseActionsByJobID(jobID);
            foreach (string key in actionDict.Keys) {
                _actions.Add(key, new Action(actionDict[key]));
            }
        }
    }
}
