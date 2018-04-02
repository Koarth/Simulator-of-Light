using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Simulator_of_Light.Simulator.Models
{
    public static class BaseAuraFactory
    {

        private static Dictionary<string, BaseAura> _baseAuras = new Dictionary<string, BaseAura>();

        public static BaseAura getBaseAura(string name)
        {
            if (_baseAuras.ContainsKey(name))
            {
                return _baseAuras["name"];
            }
            else
            {
                var newAura = new BaseAura(name);
                _baseAuras.Add(name, newAura);
                return newAura;
            }
        }

    }
}
