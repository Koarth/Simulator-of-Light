using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Models;

namespace Simulator_of_Light.Simulator.Resources {

    public interface IGearsetDAO {

        GearSet GetEquipmentSetByName(string name);

    }
}
