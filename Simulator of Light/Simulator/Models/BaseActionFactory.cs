using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Simulator_of_Light.Simulator.Resources.Constants;
using Simulator_of_Light.Simulator.Resources;

namespace Simulator_of_Light.Simulator.Models {

    public static class BaseActionFactory {

        private static Dictionary<JobID, Dictionary<string, BaseAction>> _baseActions = new Dictionary<JobID, Dictionary<string,BaseAction>>(); 

        public static Dictionary<string, BaseAction> getBaseActionsByJobID(JobID jobID) {
            
            if (_baseActions.Keys.Contains(jobID)) {
                return _baseActions[jobID];
            }

            ActionDAO DAO = new ActionDAOJsonImpl();
            var dict = DAO.getActionsByJobID(jobID);
            _baseActions.Add(jobID, dict);

            return _baseActions[jobID];

        }

    }
}
