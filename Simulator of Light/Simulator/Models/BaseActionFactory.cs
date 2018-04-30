using System;
using System.Collections.Generic;
using System.Linq;
using Simulator_of_Light.Simulator.Resources;

namespace Simulator_of_Light.Simulator.Models {

    /// <summary>
    /// This factory acts as a flyweight for BaseActions.  Job Actions should be initialized from here.
    /// </summary>
    /// <remarks>
    /// Each actor will have a fairly large list of Actions available for them to execute.  However,
    /// each action has a large number of static properties, and only a very limited number of
    /// dynamic properties.  To cut down on initialization costs, Actions are split into BaseActions,
    /// which summarize the Actions' static properties, such as Name, Potency, etc. and Actions, which
    /// summarizes their dynamic properties, such as recast availability.
    /// 
    /// Each Action should have a BaseAction property to give access to its corresponding static properties.
    /// Since BaseActions are effectively immutable, many actions can refer to the same BaseAction, such
    /// as when multiple Actors of the same Job are initialized.
    /// </remarks>
    public static class BaseActionFactory {

        private static Dictionary<JobID, Dictionary<string, BaseAction>> _baseActions;

        static BaseActionFactory() {
            // init static dictionary for use as a flyweight
            _baseActions = new Dictionary<JobID, Dictionary<string, BaseAction>>();
        }

        public static Dictionary<string, BaseAction> getBaseActionsByJobID(JobID jobID) {
            
            // return if that job's actions have already been initialized.
            if (_baseActions.Keys.Contains(jobID)) {
                return _baseActions[jobID];
            }

            // initialize from JSON configuration if needed.
            ActionDAO DAO = new ActionDAOJsonImpl();
            var dict = DAO.getActionsByJobID(jobID);
            _baseActions.Add(jobID, dict);

            return _baseActions[jobID];

        }

    }
}
