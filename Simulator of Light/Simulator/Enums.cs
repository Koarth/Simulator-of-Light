using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator {

    public enum JobID {
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
        ATTACK,
        MAGIC,
        HEAL,
        UNKNOWN
    }

    public enum ActionAspect {
        BLUNT,
        SLASHING,
        PIERCING,
        MAGIC,
        UNASPECTED
    }

    public enum PrimaryStat {
        STR,
        DEX,
        INT,
        MND,
        VIT,
        HP,
        MP,
        UNKNOWN
    }

    public enum EquipSlot {
        WEAPON,
        SHIELD,
        HEAD,
        BODY,
        HANDS,
        LEGS,
        FEET,
        EARRINGS,
        NECKLACE,
        BRACELET,
        RINGLEFT,
        RINGRIGHT
    }
}
