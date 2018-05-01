using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Models;
using Newtonsoft.Json;
using System.IO;

namespace Simulator_of_Light.Simulator.Resources {
    class GearsetDAOJsonImpl : IGearsetDAO {

        public EquipmentSet GetEquipmentSetByName(string name) {

            string partialpath = Simulator_of_Light.Resources.ResourceManager.GetString("GEARSET_DIRECTORY");
            if (partialpath == null) {
                throw new ArgumentException("No resource configuration for gearset configuration directory.");
            }
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), partialpath, name + ".json");

            Equipment[] retrievedSet;
            try {
                retrievedSet = JsonConvert.DeserializeObject<Equipment[]>(File.ReadAllText(filePath), 
                    new JsonSerializerSettings {
                        DefaultValueHandling = DefaultValueHandling.Populate
                    }
                );
            } catch (FileNotFoundException e) {
                throw new FileNotFoundException("Gearset configuration not found for "
                    + name + " at " + filePath);
            }

            return new EquipmentSet(retrievedSet);


        }
    }
}
