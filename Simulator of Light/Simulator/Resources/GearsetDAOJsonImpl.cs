using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Models;
using Newtonsoft.Json;
using System.IO;

namespace Simulator_of_Light.Simulator.Resources {
    public class GearsetDAOJsonImpl : IGearsetDAO {

        public GearSet GetEquipmentSetByName(string name) {

            string partialpath = Simulator_of_Light.Resources.ResourceManager.GetString("GEARSET_DIRECTORY");
            if (partialpath == null) {
                throw new ArgumentException("No resource configuration for gearset configuration directory.");
            }
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), partialpath, name + ".json");

            GearItem[] retrievedSet;
            try {
                retrievedSet = JsonConvert.DeserializeObject<GearItem[]>(File.ReadAllText(filePath), 
                    new JsonSerializerSettings {
                        DefaultValueHandling = DefaultValueHandling.Populate
                    }
                );
            } catch (FileNotFoundException) {
                throw new FileNotFoundException("Gearset configuration not found for "
                    + name + " at " + filePath);
            }

            return new GearSet(retrievedSet);


        }
    }
}
