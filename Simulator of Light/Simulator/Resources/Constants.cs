﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator_of_Light.Simulator.Resources {

    public static class Constants {

        /* Credit for the derivation of all formulas and constants goes 
         to the TheoryJerks and their contributors. */

        /* Base Statistics at Level 70 */
        /**
        public static readonly double BaseDetermination70 = 292;
        public static readonly double BaseCriticalHit70 = 364;
        public static readonly double BaseDirectHit70 = 364;
        public static readonly double BaseSpeed70 = 364;
        public static readonly double BaseTenacity70 = 364;
        public static readonly double BasePiety70 = 292;
        **/
        public static readonly double BaseMain70 = 292;
        public static readonly double BaseMP70 = 12000;
        public static readonly double BaseHP70 = 3600;
        public static readonly double BaseTP70 = 1000;

        public static readonly double MainTraitIncrease70 = 48;

        /* "Magic Numbers" for secondary stat formulas. */
        // Characters earn less benefit per stat point at higher levels.  This is the penalty divisor at 70.
        public static readonly double LevelGrowthPenalty70 = 2170;
        // These determine the difference in conversion rates from stat to multiplier for secondary stats.
        public static readonly double DeterminationGrowthModifier = 130;
        public static readonly double CriticalHitGrowthModifier = 200;
        public static readonly double DirectHitGrowthModifier = 550;
        public static readonly double SpeedGrowthModifier = 130;
        public static readonly double TenacityGrowthModifier = 100;
        public static readonly double PietyGrowthModifier = 6000;

        /* These are the base multipliers for various secondary stats. */
        public static readonly double BaseCriticalHitRate = 0.05;
        public static readonly double BaseCriticalHitMultiplier = 1.4;
        public static readonly double DirectHitMultiplier = 1.25;
        public static readonly double BaseRecastDelay = 2.5;

        /* Global animation delay (subject to adjustment) */
        public static readonly long GlobalAnimationDelay = 650;
        /* Base global recast time */
        public static readonly long GlobalRecastTime = 2500;
        /* This is an empirical value representing how long an auto-attack is delayed 
         by an action with a cast time.  For example, casting Holy Spirit (1.5 second cast time)
         appears to delay the timing of the next auto-attack by ~(1.5 * 0.7 = 1.05) seconds.
         The next auto-attack will be executed at the newly delayed timing, or at the end of the
         current cast, whichever is later. */
        public static readonly double AutoAttackCastDelayFactor = 0.70;

        private static Dictionary<CharacterStat, double> baseStats
            = new Dictionary<CharacterStat, double>() {
            {CharacterStat.STRENGTH, BaseMain70},
            {CharacterStat.DEXTERITY, BaseMain70},
            {CharacterStat.INTELLIGENCE, BaseMain70},
            {CharacterStat.MIND, BaseMain70},
            {CharacterStat.VITALITY, BaseMain70},
            {CharacterStat.DETERMINATION, 292},
            {CharacterStat.DIRECTHIT, 364},
            {CharacterStat.CRITICALHIT, 364},
            {CharacterStat.SKILLSPEED, 364},
            {CharacterStat.SPELLSPEED, 364},
            {CharacterStat.TENACITY, 364},
            {CharacterStat.PIETY, 292}
        };

        private static Dictionary<JobID, CharacterStat> weaponskillPrimaryStat
            = new Dictionary<JobID, CharacterStat>() {
            {JobID.AST, CharacterStat.STRENGTH},
            {JobID.BLM, CharacterStat.STRENGTH},
            {JobID.BRD, CharacterStat.DEXTERITY},
            {JobID.DRG, CharacterStat.STRENGTH},
            {JobID.DRK, CharacterStat.STRENGTH},
            {JobID.MCH, CharacterStat.DEXTERITY},
            {JobID.MNK, CharacterStat.STRENGTH},
            {JobID.NIN, CharacterStat.DEXTERITY},
            {JobID.PLD, CharacterStat.STRENGTH},
            {JobID.RDM, CharacterStat.STRENGTH},
            {JobID.SAM, CharacterStat.STRENGTH},
            {JobID.SCH, CharacterStat.STRENGTH},
            {JobID.SMN, CharacterStat.STRENGTH},
            {JobID.WAR, CharacterStat.STRENGTH},
            {JobID.WHM, CharacterStat.STRENGTH}
        };

        private static Dictionary<JobID, CharacterStat> defaultActionPrimaryStat
            = new Dictionary<JobID, CharacterStat>() {
            {JobID.AST, CharacterStat.MIND},
            {JobID.BLM, CharacterStat.INTELLIGENCE},
            {JobID.BRD, CharacterStat.DEXTERITY},
            {JobID.DRG, CharacterStat.STRENGTH},
            {JobID.DRK, CharacterStat.STRENGTH},
            {JobID.MCH, CharacterStat.DEXTERITY},
            {JobID.MNK, CharacterStat.STRENGTH},
            {JobID.NIN, CharacterStat.DEXTERITY},
            {JobID.PLD, CharacterStat.STRENGTH},
            {JobID.RDM, CharacterStat.INTELLIGENCE},
            {JobID.SAM, CharacterStat.STRENGTH},
            {JobID.SCH, CharacterStat.MIND},
            {JobID.SMN, CharacterStat.INTELLIGENCE},
            {JobID.WAR, CharacterStat.STRENGTH},
            {JobID.WHM, CharacterStat.MIND}
        };

        private static Dictionary<JobID, ActionType> DefaultActionTypes
            = new Dictionary<JobID, ActionType>() {
            {JobID.AST, ActionType.HEAL},
            {JobID.BLM, ActionType.MAGIC},
            {JobID.BRD, ActionType.ATTACK},
            {JobID.DRG, ActionType.ATTACK},
            {JobID.DRK, ActionType.ATTACK},
            {JobID.MCH, ActionType.ATTACK},
            {JobID.MNK, ActionType.ATTACK},
            {JobID.NIN, ActionType.ATTACK},
            {JobID.PLD, ActionType.ATTACK},
            {JobID.RDM, ActionType.MAGIC},
            {JobID.SAM, ActionType.ATTACK},
            {JobID.SCH, ActionType.HEAL},
            {JobID.SMN, ActionType.MAGIC},
            {JobID.WAR, ActionType.ATTACK},
            {JobID.WHM, ActionType.HEAL}
        };

        private static Dictionary<JobID, double> strengthJobMods
            = new Dictionary<JobID, double>() {
            {JobID.AST, 50},
            {JobID.BLM, 45},
            {JobID.BRD, 90},
            {JobID.DRG, 115},
            {JobID.DRK, 105},
            {JobID.MCH, 85},
            {JobID.MNK, 110},
            {JobID.NIN, 85},
            {JobID.PLD, 100},
            {JobID.RDM, 55},
            {JobID.SAM, 112},
            {JobID.SCH, 90},
            {JobID.SMN, 90},
            {JobID.WAR, 105},
            {JobID.WHM, 55}
        };

        private static Dictionary<JobID, double> dexterityJobMods
            = new Dictionary<JobID, double>() {
            {JobID.AST, 100},
            {JobID.BLM, 100},
            {JobID.BRD, 115},
            {JobID.DRG, 100},
            {JobID.DRK, 95},
            {JobID.MCH, 115},
            {JobID.MNK, 105},
            {JobID.NIN, 110},
            {JobID.PLD, 95},
            {JobID.RDM, 105},
            {JobID.SAM, 108},
            {JobID.SCH, 100},
            {JobID.SMN, 100},
            {JobID.WAR, 95},
            {JobID.WHM, 105}
        };

        private static Dictionary<JobID, double> intelligenceJobMods
            = new Dictionary<JobID, double>() {
            {JobID.AST, 105},
            {JobID.BLM, 115},
            {JobID.BRD, 85},
            {JobID.DRG, 45},
            {JobID.DRK, 60},
            {JobID.MCH, 80},
            {JobID.MNK, 50},
            {JobID.NIN, 65},
            {JobID.PLD, 60},
            {JobID.RDM, 115},
            {JobID.SAM, 60},
            {JobID.SCH, 105},
            {JobID.SMN, 115},
            {JobID.WAR, 40},
            {JobID.WHM, 105}
        };

        private static Dictionary<JobID, double> mindJobMods
            = new Dictionary<JobID, double>() {
            {JobID.AST, 115},
            {JobID.BLM, 75},
            {JobID.BRD, 80},
            {JobID.DRG, 65},
            {JobID.DRK, 40},
            {JobID.MCH, 85},
            {JobID.MNK, 90},
            {JobID.NIN, 75},
            {JobID.PLD, 100},
            {JobID.RDM, 110},
            {JobID.SAM, 50},
            {JobID.SCH, 115},
            {JobID.SMN, 80},
            {JobID.WAR, 55},
            {JobID.WHM, 115}
        };

        private static Dictionary<JobID, double> vitalityJobMods
            = new Dictionary<JobID, double>() {
            {JobID.AST, 100},
            {JobID.BLM, 100},
            {JobID.BRD, 100},
            {JobID.DRG, 105},
            {JobID.DRK, 110},
            {JobID.MCH, 100},
            {JobID.MNK, 100},
            {JobID.NIN, 100},
            {JobID.PLD, 110},
            {JobID.RDM, 100},
            {JobID.SAM, 100},
            {JobID.SCH, 100},
            {JobID.SMN, 100},
            {JobID.WAR, 110},
            {JobID.WHM, 100}
        };

        private static Dictionary<JobID, double> hpJobMods
            = new Dictionary<JobID, double>() {
            {JobID.AST, 105},
            {JobID.BLM, 105},
            {JobID.BRD, 105},
            {JobID.DRG, 115},
            {JobID.DRK, 120},
            {JobID.MCH, 105},
            {JobID.MNK, 110},
            {JobID.NIN, 108},
            {JobID.PLD, 120},
            {JobID.RDM, 105},
            {JobID.SAM, 109},
            {JobID.SCH, 105},
            {JobID.SMN, 105},
            {JobID.WAR, 125},
            {JobID.WHM, 105}
        };

        private static Dictionary<JobID, double> mpJobMods
            = new Dictionary<JobID, double>() {
            {JobID.AST, 124},
            {JobID.BLM, 129},
            {JobID.BRD, 79},
            {JobID.DRG, 49},
            {JobID.DRK, 79},
            {JobID.MCH, 79},
            {JobID.MNK, 43},
            {JobID.NIN, 48},
            {JobID.PLD, 59},
            {JobID.RDM, 120},
            {JobID.SAM, 40},
            {JobID.SCH, 119},
            {JobID.SMN, 111},
            {JobID.WAR, 38},
            {JobID.WHM, 124}
        };

        private static Dictionary<CharacterStat, Dictionary<JobID, double>> jobMods
            = new Dictionary<CharacterStat, Dictionary<JobID, double>>() {
            {CharacterStat.STRENGTH, strengthJobMods},
            {CharacterStat.DEXTERITY, dexterityJobMods},
            {CharacterStat.INTELLIGENCE, intelligenceJobMods},
            {CharacterStat.MIND, mindJobMods},
            {CharacterStat.VITALITY, vitalityJobMods},
            {CharacterStat.HP, hpJobMods},
            {CharacterStat.MP, mpJobMods},
        };

        private static Dictionary<JobID, CharacterStat> statsFromTraits
            = new Dictionary<JobID, CharacterStat>() {
            {JobID.AST, CharacterStat.MIND},
            {JobID.BLM, CharacterStat.INTELLIGENCE},
            {JobID.BRD, CharacterStat.DEXTERITY},
            {JobID.DRG, CharacterStat.STRENGTH},
            {JobID.DRK, CharacterStat.VITALITY},
            {JobID.MCH, CharacterStat.DEXTERITY},
            {JobID.MNK, CharacterStat.STRENGTH},
            {JobID.NIN, CharacterStat.DEXTERITY},
            {JobID.PLD, CharacterStat.VITALITY},
            {JobID.RDM, CharacterStat.INTELLIGENCE},
            {JobID.SAM, CharacterStat.STRENGTH},
            {JobID.SCH, CharacterStat.MIND},
            {JobID.SMN, CharacterStat.INTELLIGENCE},
            {JobID.WAR, CharacterStat.VITALITY},
            {JobID.WHM, CharacterStat.MIND}
        };


        private static int[][] clanMods = new int[][] {
            new int[]{ 0,  3, -1,  2, -1}, // Elezen Wildwood
            new int[]{ 0,  0, -1,  3,  1}, // Elezen Duskwight
            new int[]{ 2, -1,  0,  3, -1}, // Hyur Midlander
            new int[]{ 3,  0,  2, -2,  0}, // Hyur Highlander
            new int[]{-1,  3, -1,  2,  0}, // Lalafell Plainsfolk
            new int[]{-1,  1, -2,  2,  3}, // Lalafell Dunesfolk
            new int[]{ 2,  3,  0, -1, -1}, // Miqo'te Seeker of the Sun
            new int[]{-1,  2, -2,  1,  3}, // Miqo'te Keeper of the Moon
            new int[]{ 2, -1,  3, -2,  1}, // Roegadyn Sea Wolf
            new int[]{ 0, -2,  3,  0,  2}, // Roegadyn Hellsguard
            new int[]{-1,  2, -1,  0,  3}, // Au Ra Raen
            new int[]{ 3,  0,  2,  0, -2}  // Au Ra Xaela
        };

        /*
        // I hate everything about this.
        private static Dictionary<CharacterClan, Dictionary<CharacterStat, double>> clanMods
            = new Dictionary<CharacterClan, Dictionary<CharacterStat, double>>() {

                {CharacterClan.ELEZEN_WILDWOOD,
                    new Dictionary<CharacterStat, double>(){
                        { CharacterStat.STRENGTH, 0 },
                        { CharacterStat.DEXTERITY, 3 },
                        { CharacterStat.VITALITY, -1 },
                        { CharacterStat.INTELLIGENCE, 2 },
                        { CharacterStat.MIND, -1 }
                    }
                },
                {CharacterClan.ELEZEN_DUSKWIGHT,
                    new Dictionary<CharacterStat, double>(){
                        { CharacterStat.STRENGTH, 0 },
                        { CharacterStat.DEXTERITY, 0 },
                        { CharacterStat.VITALITY, -1 },
                        { CharacterStat.INTELLIGENCE, 3 },
                        { CharacterStat.MIND, 1 }
                    }
                },
                {CharacterClan.HYUR_MIDLANDER,
                    new Dictionary<CharacterStat, double>(){
                        { CharacterStat.STRENGTH, 2 },
                        { CharacterStat.DEXTERITY, -1 },
                        { CharacterStat.VITALITY, 0 },
                        { CharacterStat.INTELLIGENCE, 3 },
                        { CharacterStat.MIND, -1 }
                    }
                },
                {CharacterClan.HYUR_HIGHLANDER,
                    new Dictionary<CharacterStat, double>(){
                        { CharacterStat.STRENGTH, 3 },
                        { CharacterStat.DEXTERITY, 0 },
                        { CharacterStat.VITALITY, 2 },
                        { CharacterStat.INTELLIGENCE, -2 },
                        { CharacterStat.MIND, 0 }
                    }
                },
                {CharacterClan.LALAFELL_PLAINSFOLK,
                    new Dictionary<CharacterStat, double>(){
                        { CharacterStat.STRENGTH, -1 },
                        { CharacterStat.DEXTERITY, 3 },
                        { CharacterStat.VITALITY, -1 },
                        { CharacterStat.INTELLIGENCE, 2 },
                        { CharacterStat.MIND, 0 }
                    }
                },
                {CharacterClan.LALAFELL_DUNESFOLK,
                    new Dictionary<CharacterStat, double>(){
                        { CharacterStat.STRENGTH, -1 },
                        { CharacterStat.DEXTERITY, 1 },
                        { CharacterStat.VITALITY, -2 },
                        { CharacterStat.INTELLIGENCE, 2 },
                        { CharacterStat.MIND, 3 }
                    }
                },
                {CharacterClan.MIQOTE_SEEKER,
                    new Dictionary<CharacterStat, double>(){
                        { CharacterStat.STRENGTH, 2 },
                        { CharacterStat.DEXTERITY, 3 },
                        { CharacterStat.VITALITY, 0 },
                        { CharacterStat.INTELLIGENCE, -1 },
                        { CharacterStat.MIND, -1 }
                    }
                },
                {CharacterClan.MIQOTE_KEEPER,
                    new Dictionary<CharacterStat, double>(){
                        { CharacterStat.STRENGTH, -1 },
                        { CharacterStat.DEXTERITY, 2 },
                        { CharacterStat.VITALITY, -2 },
                        { CharacterStat.INTELLIGENCE, 1 },
                        { CharacterStat.MIND, 3 }
                    }
                },
                {CharacterClan.ROEGADYN_SEAWOLF,
                    new Dictionary<CharacterStat, double>(){
                        { CharacterStat.STRENGTH, 2 },
                        { CharacterStat.DEXTERITY, -1 },
                        { CharacterStat.VITALITY, 3 },
                        { CharacterStat.INTELLIGENCE, -2 },
                        { CharacterStat.MIND, 1 }
                    }
                },
                {CharacterClan.ROEGADYN_HELLSGUARD,
                    new Dictionary<CharacterStat, double>(){
                        { CharacterStat.STRENGTH, 0 },
                        { CharacterStat.DEXTERITY, -2 },
                        { CharacterStat.VITALITY, 3 },
                        { CharacterStat.INTELLIGENCE, 0 },
                        { CharacterStat.MIND, 2 }
                    }
                },
                {CharacterClan.AURA_RAEN,
                    new Dictionary<CharacterStat, double>(){
                        { CharacterStat.STRENGTH, -1 },
                        { CharacterStat.DEXTERITY, 2 },
                        { CharacterStat.VITALITY, -1 },
                        { CharacterStat.INTELLIGENCE, 0 },
                        { CharacterStat.MIND, 3 }
                    }
                },
                {CharacterClan.AURA_XAELA,
                    new Dictionary<CharacterStat, double>(){
                        { CharacterStat.STRENGTH, 3 },
                        { CharacterStat.DEXTERITY, 0 },
                        { CharacterStat.VITALITY, 2 },
                        { CharacterStat.INTELLIGENCE, 0 },
                        { CharacterStat.MIND, -2 }
                    }
                }

            };
        */

        private static Dictionary<JobID, double> traitDamageModifier
            = new Dictionary<JobID, double>() {
                {JobID.AST, 1.3},
                {JobID.BLM, 1},
                {JobID.BRD, 1},
                {JobID.DRG, 1},
                {JobID.DRK, 1},
                {JobID.MCH, 1.2},
                {JobID.MNK, 1},
                {JobID.NIN, 1.2},
                {JobID.PLD, 1},
                {JobID.RDM, 1.3},
                {JobID.SAM, 1},
                {JobID.SCH, 1.3},
                {JobID.SMN, 1.3},
                {JobID.WAR, 1},
                {JobID.WHM, 1.3}
            };

        private static Dictionary<JobID, double> traitHealingModifier
            = new Dictionary<JobID, double>() {
                {JobID.AST, 1.3},
                {JobID.BLM, 1},
                {JobID.BRD, 1},
                {JobID.DRG, 1},
                {JobID.DRK, 1},
                {JobID.MCH, 1},
                {JobID.MNK, 1},
                {JobID.NIN, 1},
                {JobID.PLD, 1},
                {JobID.RDM, 1.3},
                {JobID.SAM, 1},
                {JobID.SCH, 1.3},
                {JobID.SMN, 1.3},
                {JobID.WAR, 1},
                {JobID.WHM, 1.3}
            };

        public static double getBaseStat(CharacterStat stat) {
            try {
                return baseStats[stat];
            } catch (KeyNotFoundException) {
                throw new ArgumentException("Invalid stat.");
            }
        }

        public static int[] getClanBaseStats(CharacterClan race) {
            try {
                return clanMods[(int)race];
            } catch (KeyNotFoundException) {
                throw new ArgumentException("Invalid race/clan");
            }
        }

        public static double getJobMod(JobID id, CharacterStat stat) {

            Dictionary<JobID, double> dict;

            try {
                dict = jobMods[stat];
            } catch (KeyNotFoundException) {
                throw new ArgumentException("Invalid primary stat");
            }

            try { 
                return dict[id];
            } catch (KeyNotFoundException) {
                throw new ArgumentException("Invalid JobID");
            }
        }

        public static double getAutoAttackPotency(JobID id) {
            if (id == JobID.BRD || id == JobID.MCH) {
                return 100;
            }

            return 110;
        }

        public static CharacterStat getWeaponskillStat(JobID id) {
            try { 
                return weaponskillPrimaryStat[id];
            } catch (KeyNotFoundException) {
                throw new ArgumentException("Invalid JobID");
            }
        }

        public static CharacterStat getDefaultPrimaryStat(JobID id) {
            try {
                return defaultActionPrimaryStat[id];
            } catch (KeyNotFoundException) {
                throw new ArgumentException("Invalid JobID");
            }
        }

        public static CharacterStat getTraitStat(JobID id) {
            try {
                return statsFromTraits[id];
            } catch (KeyNotFoundException) {
                throw new ArgumentException("Invalid JobID");
            }
        }

        public static double getTraitDamageModifier(JobID id) {
            try {
                return traitDamageModifier[id];
            } catch (KeyNotFoundException) {
                throw new ArgumentException("Invalid JobID");
            }
        }

        public static double getTraitHealingModifier(JobID id) {
            try {
                return traitHealingModifier[id];
            } catch (KeyNotFoundException) {
                throw new ArgumentException("Invalid JobID");
            }
        }
    }
}
