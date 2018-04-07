﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Models;
using static Simulator_of_Light.Simulator.Resources.Constants;

namespace Simulator_of_Light.Simulator.Resources
{
    public interface AuraDAO
    {

        Dictionary<string, BaseAura> getAurasByJobID(JobID jobID);
        BaseAura getAuraByName(string name);

    }
}
