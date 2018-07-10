using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator {

    public enum JobID {
        UNKNOWN,
        AST,
        BLM,
        BRD,
        DRG,
        DRK,
        MCH,
        MNK,
        NIN,
        PLD,
        RDM,
        SAM,
        SCH,
        SMN,
        WAR,
        WHM
    }

    public enum ActionType {
        UNKNOWN,
        ATTACK,
        MAGIC,
        HEAL
    }

    public enum ActionAspect {
        BLUNT,
        SLASHING,
        PIERCING,
        MAGIC,
        UNASPECTED
    }

    public enum CharacterStat {
        UNKNOWN,
        STRENGTH,
        DEXTERITY,
        INTELLIGENCE,
        MIND,
        VITALITY,
        DETERMINATION,
        DIRECTHIT,
        CRITICALHIT,
        SKILLSPEED,
        SPELLSPEED,
        TENACITY,
        PIETY,
        PHYSICALDAMAGE,
        MAGICDAMAGE,
        AUTOATTACK,
        AUTOATTACKDELAY,
        DEFENSE,
        MAGICDEFENSE,
        HP,
        MP,
        TP,
        ATTACKPOWER,
        WEAPONDAMAGE,
        SPEED
    }

    public enum EquipSlot {
        UNKNOWN,
        WEAPON,
        SHIELD,
        HEAD,
        BODY,
        HANDS,
        WAIST,
        LEGS,
        FEET,
        EARRINGS,
        NECKLACE,
        BRACELET,
        RINGLEFT,
        RINGRIGHT
    }

    public enum CharacterClan {
        ELEZEN_WILDWOOD,
        ELEZEN_DUSKWIGHT,
        HYUR_MIDLANDER,
        HYUR_HIGHLANDER,
        LALAFELL_PLAINSFOLK,
        LALAFELL_DUNESFOLK,
        MIQOTE_SEEKER,
        MIQOTE_KEEPER,
        ROEGADYN_SEAWOLF,
        ROEGADYN_HELLSGUARD,
        AURA_RAEN,
        AURA_XAELA
    }

    /*
     * Based on WarcraftLogs/FFLogs events.
     * TODO: Determine which of these may not apply to FFXIV; these events are from WoW.
     */
    public enum CombatLogEventType {
        BEGINCAST,
        CAST,
        DAMAGE,
        HEAL,
        ABSORBED,
        HEALABSORBED,
        APPLYBUFF,
        APPLYDEBUFF,
        APPLYBUFFSTACK,
        APPLYDEBUFFSTACK,
        REFRESHBUFF,
        REFRESHDEBUFF,
        REMOVEBUFF,
        REMOVEDEBUFF,
        REMOVEBUFFSTACK,
        REMOVEDEBUFFSTACK,
        SUMMON,
        CREATE,
        DEATH,
        DESTROY,
        AURABROKEN,
        DISPEL,
        INTERRUPT,
        STEAL,
        LEECH,
        ENERGIZE,
        DRAIN,
        RESURRECT
    }

    public enum BattleEventType {
        ACTOR_READY,
        USE_ACTION,
        AURA_TICK,
        REGEN_TICK,
        RESOLVE_ACTION,
        APPLY_AURA,
        EXPIRE_AURA,
        APPLY_AURA_STACK,
        REMOVE_AURA_STACK,
        FIGHT_COMPLETE
    }

    public enum ActionTarget {
        TARGET,
        SELF,
        AREA
    }

    public enum AuraTarget {
        TARGET,
        SOURCE
    }
}
