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
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Simulator\\Resources\\BaseActions\\" + jobString + ".json");
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
