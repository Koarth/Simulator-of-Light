using System;
using static Simulator_of_Light.Simulator.Resources.Constants;

namespace Simulator_of_Light.Simulator.Models {

    public sealed class Action {

        // BaseAction property, which contains all the static properties of this Action.
        private BaseAction _baseAction;
        private long _recastAvailable;

        public Action(BaseAction baseAction) {
            _baseAction = baseAction;
            _recastAvailable = long.MinValue;
        }

        public long RecastAvailable { get => _recastAvailable; private set => _recastAvailable = value; }
    }

}