﻿using System;

namespace Simulator_of_Light.Simulator.Models {

    public sealed class Action {

        // BaseAction property, which contains all the static properties of this Action.
        public BaseAction BaseAction { get; private set; }
        // Time at which the action is next available to use.
        public long RecastAvailable { get; set; }

        public Action(BaseAction baseAction) {
            BaseAction = baseAction;
            RecastAvailable = long.MinValue;
        }

    }

}