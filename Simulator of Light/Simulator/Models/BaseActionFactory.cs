using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models
{
    class BaseActionFactory
    {
        private static Dictionary<string, BaseAction> _baseActions = new Dictionary<string, BaseAction>();

        public static BaseAction getBaseAura(string name)
        {
            if (_baseActions.ContainsKey(name))
            {
                return _baseActions["name"];
            }
            else
            {
                var newAction = new BaseAction(name);
                _baseActions.Add(name, newAction);
                return newAction;
            }
        }

    }
}
