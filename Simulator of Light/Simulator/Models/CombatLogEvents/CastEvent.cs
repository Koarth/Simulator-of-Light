using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Models.CombatLogEvents {
    public class CastEvent : CombatLogEvent {

        public ITarget Target { get; set; }
        public BaseAction Action { get; set; }

        public CastEvent(long time, IActor source, ITarget target, BaseAction action) : base(time, source) {
            this.Target = target;
            this.Action = action;
        }

        public override string ToString() {
            throw new NotImplementedException();
        }

    }
}
