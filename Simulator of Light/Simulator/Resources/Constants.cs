using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Resources
{
    

    public static class Constants
    {

        /* Credit for the derivation of all formulas and constants goes 
         to the TheoryJerks and their contributors. */

        /* Base Statistics at Level 70 */
        public static readonly double BaseDetermination70 = 292;
        public static readonly double BaseCriticalHit70 = 364;
        public static readonly double BaseDirectHit70 = 364;
        public static readonly double BaseSkillSpeed70 = 364;
        public static readonly double BaseSpellSpeed70 = 364;
        public static readonly double BaseTenacity70 = 364;
        public static readonly double BaseAttackPower70 = 292; // TODO: verify that there is no variance between jobs.

        /* "Magic Numbers" for secondary stat formulas. */
        public static readonly double SecondaryLevelMod70 = 2170;
        public static readonly double DeterminationModifier = 130;
        public static readonly double CriticalHitModifier = 200;
        public static readonly double DirectHitModifier = 550;
        public static readonly double SpeedModifier = 130;
        public static readonly double TenacityModifier = 100;

        public static readonly double BaseCriticalHitRate = 0.05;
        public static readonly double BaseCriticalHitMultiplier = 1.4;
        public static readonly double DirectHitMultiplier = 1.25;

        /* Job IDs to help store constants and for easy reference. */
        public enum JobID
        {
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

        public enum ActionType
        {
            ATTACK,
            MAGIC,
            HEAL,
            UNKNOWN
        }

        public static ActionType DefaultActionType(JobID id)
        {
            switch (id)
            {
                case JobID.AST: return ActionType.HEAL;
                case JobID.BLM: return ActionType.MAGIC;
                case JobID.BRD: return ActionType.ATTACK;
                case JobID.DRG: return ActionType.ATTACK;
                case JobID.DRK: return ActionType.ATTACK;
                case JobID.MCH: return ActionType.ATTACK;
                case JobID.MNK: return ActionType.ATTACK;
                case JobID.NIN: return ActionType.ATTACK;
                case JobID.PLD: return ActionType.ATTACK;
                case JobID.RDM: return ActionType.MAGIC;
                case JobID.SAM: return ActionType.ATTACK;
                case JobID.SCH: return ActionType.HEAL;
                case JobID.SMN: return ActionType.MAGIC;
                case JobID.WAR: return ActionType.ATTACK;
                case JobID.WHM: return ActionType.HEAL;
            }
            throw new System.ArgumentException("Invalid JobID");
        }


        // TODO: verify JobMods are still accurate; this is data from level 60.
        public static double JobMod(JobID id, ActionType type=ActionType.UNKNOWN)
        {
            if (type == ActionType.UNKNOWN)
            {
                return JobMod(id, DefaultActionType(id));
            }

            if (type == ActionType.ATTACK)
            {
                switch (id)
                {
                    case JobID.AST: return 50;
                    case JobID.BLM: return 45;
                    case JobID.BRD: return 115;
                    case JobID.DRG: return 115;
                    case JobID.DRK: return 105;
                    case JobID.MCH: return 115;
                    case JobID.MNK: return 110;
                    case JobID.NIN: return 105;
                    case JobID.PLD: return 100;
                    case JobID.RDM: return 90; // Assumed
                    case JobID.SAM: return 112; // Assumed
                    case JobID.SCH: return 90;
                    case JobID.SMN: return 90;
                    case JobID.WAR: return 105;
                    case JobID.WHM: return 40;
                }
                throw new System.ArgumentException("Invalid JobID");
            }
            else if (type == ActionType.MAGIC)
            {
                switch (id)
                {
                    case JobID.AST: return 105;
                    case JobID.BLM: return 115;
                    case JobID.BRD: return 85;
                    case JobID.DRG: return 45;
                    case JobID.DRK: return 60;
                    case JobID.MCH: return 80;
                    case JobID.MNK: return 50;
                    case JobID.NIN: return 65;
                    case JobID.PLD: return 60;
                    case JobID.RDM: return 115;
                    case JobID.SAM: return 50;
                    case JobID.SCH: return 105;
                    case JobID.SMN: return 115;
                    case JobID.WAR: return 40;
                    case JobID.WHM: return 115;
                }
                throw new System.ArgumentException("Invalid JobID");
            }
            else if (type == ActionType.HEAL)
            {
                switch (id)
                {
                    case JobID.AST: return 115;
                    case JobID.BLM: return 75;
                    case JobID.BRD: return 80;
                    case JobID.DRG: return 65;
                    case JobID.DRK: return 40;
                    case JobID.MCH: return 85;
                    case JobID.MNK: return 90;
                    case JobID.NIN: return 75;
                    case JobID.PLD: return 100;
                    case JobID.RDM: return 100; // Unknown
                    case JobID.SAM: return 90; // Unknown
                    case JobID.SCH: return 115;
                    case JobID.SMN: return 80;
                    case JobID.WAR: return 55;
                    case JobID.WHM: return 115;
                }
                throw new System.ArgumentException("Invalid JobID");
            }
            throw new System.ArgumentException("Invalid Action Type");

        }
    }
}
