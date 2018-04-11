using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Models;
using static Simulator_of_Light.Simulator.Resources.Constants;
using Newtonsoft.Json;
using System.IO;

namespace Simulator_of_Light.Simulator.Resources {

    public class ActionDAOJsonImpl : ActionDAO {

        public ActionDAOJsonImpl() { }

        public Dictionary<string, BaseAction> getActionsByJobID(JobID jobID) {

            // Build the path to the configuration file.
            string jobString = jobID.ToString();
            string partialpath = Simulator_of_Light.Resources.ResourceManager.GetString("BASEACTIONS_JSON_" + jobString);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), partialpath);

            // Deserialize the file.
            var retrievedActions = new List<BaseAction>();
            retrievedActions = JsonConvert.DeserializeObject<List<BaseAction>>(File.ReadAllText(filePath), new JsonSerializerSettings {
                DefaultValueHandling = DefaultValueHandling.Populate
            });

            // Convert to dictionary.
            var dict = new Dictionary<string, BaseAction>();
            foreach (BaseAction action in retrievedActions) {
                dict.Add(action.Name, action);
            }

            return dict;

        }

        public BaseAction getActionByName(string name) {
            throw new NotImplementedException();
        }

    }
}
