using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator_of_Light.Simulator.Models;
using Simulator_of_Light.Simulator.Resources;

namespace Simulator_of_Light.Simulator {
    public class Battle {

        private long fightLength;
        private IActor[] actors;

        private long time;
        private int tickOffset;


        public Battle(IActor[] actors, int length = 180, int num_enemies = 1) {

            Actors = actors;
            FightLength = length * 1000;

            Time = 0;
            TickOffset = new Random().Next(0, 3000);

        }

        public void Run() {

        }

        public IActor[] Actors { get => actors; private set => actors = value; }
        public long FightLength { get => fightLength; private set => fightLength = value; }
        public long Time { get => time; private set => time = value; }
        public int TickOffset { get => tickOffset; private set => tickOffset = value; }

        


    }
}
