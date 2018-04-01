# Simulator of Light
(working name)

This goal of this project is to accurately simulate the Battle System for the MMORPG [Final Fantasy XIV Online](https://www.finalfantasyxiv.com/), with the purpose of allowing users to simulate the results of varying their rotations, gear, and raid composition.

**Below is a rough plan for development:**

Status:  
~~Probably complete~~  
*In progress*  
Not yet begun  

### Stage 1
* ~~Gather all requisite formulas and constants for calculating damage and healing to ensure project viability.~~
* *Implement all basic formulas.*
* *Implement unit test framework; test formula implementation by comparing agiainst in-game results.*

### Stage 2
* *Implement models for Actions, Actors, Auras, and their interactions.*
* Implement support for static and dynamic configuration files for Actions, Actors, and Auras.
* Create a barebones configuration for simulating a single character, including their stats, actions, auras).
* Ensure the resulting Actor can be used to accurately calculate damage values for all Actions.  
  
### Stage 3
* Implement the beginnings of a Battle object - this will act as the "server", which manages the state of Actors, the interactions between Actors, resolves their Actions, and will eventually also manage the passage of time by simulating server ticks.
* Implement the beginnings of the Simulator, which will manage configuration of the Battle, and eventually run multiple Battles concurrently.
* Model critical hits, direct hits, and self/enemy aura effects.

### Stage 4
???
