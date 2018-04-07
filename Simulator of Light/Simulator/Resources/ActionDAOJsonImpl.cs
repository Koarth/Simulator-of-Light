using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Models;
using static Simulator_of_Light.Simulator.Resources.Constants;
using Newtonsoft.Json;
using System.IO;

namespace Simulator_of_Light.Simulator.Resources
{
    public class ActionDAOJsonImpl : ActionDAO
    {

        public ActionDAOJsonImpl() { }

        public Dictionary<string, BaseAction> getActionsByJobID(JobID jobID)
        {
            string jobString = jobID.ToString();
            //string partialpath = Simulator_of_Light.Resources.WHMActions;
            string partialpath = Simulator_of_Light.Resources.ResourceManager.GetString("BASEACTIONS_JSON_" + jobString);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), partialpath);
            StreamReader file = File.OpenText(filePath);
            System.Diagnostics.Debug.WriteLine(filePath);

            //StreamReader file = file.OpenText()

            return null;

        }
        public BaseAction getActionByName(string name)
        {
            return null;
        }

    }
}
