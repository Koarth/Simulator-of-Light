# Simulator of Light
(working name)

**I am not currently accepting contributors or pull requests.  The current purpose of this project is independent exploration and personal skill development.  In the event the project reaches a critical point and is met with community interest, I will open things up.**

This goal of this project is to accurately simulate the Battle System for the MMORPG [Final Fantasy XIV Online](https://www.finalfantasyxiv.com/), with the purpose of allowing users to simulate the results of varying their rotations, gear, and raid composition.  Inspired by [SimulationCraft](http://simulationcraft.org/) for World of Warcraft.

**Below is a rough plan for development:**

Status:  
~~Probably complete~~  
*In progress*  
Not yet begun  

### Stage 1
* ~~Gather all requisite formulas and constants for calculating damage and healing to ensure project viability.~~
* ~~Implement all basic formulas.~~
* ~~Implement unit test framework; test formula implementation by comparing agiainst in-game results.~~

### Stage 2
* *Implement models for Actions, Actors, Auras, and their interactions.*
* ~~Implement support for static and dynamic configuration files for Actions, Actors, and Auras.~~
* ~~Create a barebones configuration for simulating a single character, including their stats, actions, auras).~~
* ~~Ensure the resulting Actor can be used to accurately calculate damage values for Actions.~~
  
### Stage 3
* *Implement the beginnings of a Battle object - this will act as the "server", which manages the state of Actors, the interactions between Actors, resolves their Actions, and will eventually also manage the passage of time by simulating server ticks.*
* Implement the beginnings of the Simulator, which will manage configuration of the Battle, and eventually run multiple Battles concurrently.
* *Model critical hits, direct hits, and self/enemy aura effects.*

### Stage 4
* Implement a prototype ActionList for actors - this will eventually be managed via a configuration script à la SimCraft, but begin with basic code implementation of a class rotation/priority.
* Model interactions between the Actor and the Battle by tracking time and calling upon the Actor to make decisions from their ActionList.  This will include implementations for:
	* Recast Timers
	* Cast Timers
	* Global Recast Timer (aka Global Cooldown, GCD)
	* Animation locks (example: between two OGCD abilities)
	* (possibly) Latency modelling (affects animation locks, because east coast players are not allowed to play Machinist)
	* Aura modelling, application/expiry
	* DoT/HoT ticks
* Track very basic stats about a Battle and report to the simulator.  To start, raw event log, and tabulated DPS over the fight duration.

### MVP
* Model a 3-minute fight against a fluffy pillow using a live White Mage profile and action list, and report the resulting DPS and event log.
* Run a large sample of these simulations to determine an average and standard deviation of DPS.

### Beyond
* ActionList scripts
* More action lists.
* Model additional classes with unusual mechanics and job gauges (samurai...)
* Support for "openers"; priority action lists rarely result in the optimal opening sequence of actions, so these should be custom-made.
* Multiple actors to model class synergy via raid buffs and debuffs
* Additional distillation of data - graphs, crit/dh stats, etc.
* Stat weight simulations (let's be honest, that's the entire purpose of a simulator like this, apart from modelling Bard RNG or comparing gear)
