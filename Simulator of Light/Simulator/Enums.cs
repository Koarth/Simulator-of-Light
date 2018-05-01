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
}
