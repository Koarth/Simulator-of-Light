using Simulator_of_Light.Simulator.Models;

namespace Simulator_of_Light.Simulator {
    public class QueuedEvent {

        public QueuedEventType Type { get; private set; }
        public IActor Source { get; private set; }
        public ITarget Target { get; private set; }
        public long Time { get; private set; }

        public Aura Aura { get; private set; }
        public Action Action { get; private set; }


    }
}
