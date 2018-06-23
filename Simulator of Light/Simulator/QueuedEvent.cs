using Simulator_of_Light.Simulator.Models;

namespace Simulator_of_Light.Simulator {
    public class QueuedEvent {

        private QueuedEventType _type;
        private IActor _source;
        private ITarget _target;
        private long _time;

        private Aura _aura;
        private Action _action;
    }
}
