using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Models;
using Simulator_of_Light.Simulator.Resources;

namespace Simulator_of_Light.Simulator {
    public class Battle {

        public Battle(IActor[] actors, int length = 180, int num_enemies = 1) {

            Actors = actors;
            FightLength = length * 1000;

            Time = 0;
            TickOffset = new Random().Next(0, 3000);

        }

        public void Run() {
            throw new NotImplementedException();
        }

        public IActor[] Actors { get; private set; }
        public long FightLength { get; private set; }
        public long Time { get; private set; }
        public int TickOffset { get; private set; }

    }
}
