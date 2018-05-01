using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simulator_of_Light.Simulator;
using Simulator_of_Light.Simulator.Models;
using static Simulator_of_Light.Simulator.Resources.Constants;
using System.IO;
using Newtonsoft.Json;

namespace SoLTests {

    [TestClass]
    public class EquipmentTests {

        private Equipment[] testSet;
        private Equipment[] replacementSet;

        [TestInitialize]
        public void Initialize() {

            string partialpath = "TestConfigs\\WHMGear.json";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), partialpath);
            Equipment[] retrievedSet;
            retrievedSet = JsonConvert.DeserializeObject<Equipment[]>(File.ReadAllText(filePath),
                new JsonSerializerSettings {
                   DefaultValueHandling = DefaultValueHandling.Populate
                }
            );
            testSet = retrievedSet;


            partialpath = "TestConfigs\\ReplacementGear.json";
            filePath = Path.Combine(Directory.GetCurrentDirectory(), partialpath);
            retrievedSet = JsonConvert.DeserializeObject<Equipment[]>(File.ReadAllText(filePath),
                new JsonSerializerSettings {
                    DefaultValueHandling = DefaultValueHandling.Populate
                }
            );
            replacementSet = retrievedSet;


        }

        [TestMethod]
        public void TestEquipmentSetInstantiationSuccess() {
            EquipmentSet set = new EquipmentSet(testSet);
            Assert.IsNotNull(set);
        }

        [TestMethod]
        public void TestEquipmentSetInstantiationArrayPopulated() {
            EquipmentSet set = new EquipmentSet(testSet);

            // Test set has every item defined except a shield.
            for (int i = 0; i < set.Equipment.Length; i++) {
                if (i != ((int)EquipSlot.SHIELD - 1)) {
                    Assert.IsNotNull(set.Equipment[i]);
                } else {
                    Assert.IsNull(set.Equipment[i]);
                }
            }
        }

        [TestMethod]
        public void TestEquipmentSetInstantiationSlotsCorrect() {
            EquipmentSet set = new EquipmentSet(testSet);

            for (int i = 0; i < set.Equipment.Length; i++) {
                if (set.Equipment[i] != null) {
                    Assert.AreEqual((EquipSlot)(i + 1), set.Equipment[i].Slot);
                }
            }
        }

        [TestMethod]
        public void TestEquipmentSetInstantiationStatsCorrect() {
            EquipmentSet set = new EquipmentSet(testSet);

            var summ = set.StatSummary;

            // Comparing resulting stat summary to in-game character summary.
            Assert.AreEqual(2204, summ[CharacterStat.CRITICALHIT] + BaseCriticalHit70);
            Assert.AreEqual(1564, summ[CharacterStat.DETERMINATION] + BaseDetermination70);
            Assert.AreEqual(404, summ[CharacterStat.DIRECTHIT] + BaseDirectHit70);
            Assert.AreEqual(683, summ[CharacterStat.SPELLSPEED] + BaseSpeed70);
            Assert.AreEqual(1164, summ[CharacterStat.PIETY] + BasePiety70);

            Assert.AreEqual(2159, summ[CharacterStat.DEFENSE]);
            Assert.AreEqual(3774, summ[CharacterStat.MAGICDEFENSE]);

            Assert.AreEqual(2046, summ[CharacterStat.VITALITY] + BaseMain70);
            Assert.AreEqual(2837, summ[CharacterStat.MIND] 
                + Math.Round((BaseMain70 + 40) * getJobMod(JobID.WHM, CharacterStat.MIND) / 100));
        }

        [TestMethod]
        public void TestEquipmentSetGetBySlot() {
            EquipmentSet set = new EquipmentSet(testSet);

            Equipment item = set.GetItemBySlot(EquipSlot.NECKLACE);
            Assert.AreEqual(EquipSlot.NECKLACE, item.Slot);
        }

        [TestMethod]
        public void TestEquipmentSetAddItem() {
            EquipmentSet set = new EquipmentSet();

            set.AddItem(replacementSet[1]);

            Assert.IsNotNull(set.GetItemBySlot(EquipSlot.HEAD));
            Assert.AreEqual("Ryumyakyu Hitai-ate of Healing", set.GetItemBySlot(EquipSlot.HEAD).Name);
        }

        [TestMethod]
        public void TestEquipmentSetAddItemSummary() {
            EquipmentSet set = new EquipmentSet();

            set.AddItem(replacementSet[1]);

            Assert.AreEqual(189, set.StatSummary[CharacterStat.MIND]);
        }

        [TestMethod]
        public void TestEquipmentSetRemoveItem() {
            EquipmentSet set = new EquipmentSet(testSet);

            set.RemoveItem(EquipSlot.HEAD);
            Assert.IsNull(set.GetItemBySlot(EquipSlot.HEAD));
        }

        [TestMethod]
        public void TestEquipmentSetRemoveItemSummary() {
            EquipmentSet set = new EquipmentSet(testSet);

            double mind = set.StatSummary[CharacterStat.MIND];
            double vitality = set.StatSummary[CharacterStat.VITALITY];

            set.RemoveItem(EquipSlot.HEAD);

            Assert.AreEqual(mind - 198, set.StatSummary[CharacterStat.MIND]);
            Assert.AreEqual(vitality - 195, set.StatSummary[CharacterStat.VITALITY]);
        }

        [TestMethod]
        public void TestEquipmentSetReplaceItemInSet() {
            EquipmentSet set = new EquipmentSet(testSet);
            set.AddItem(replacementSet[1]);

            Assert.AreEqual("Ryumyakyu Hitai-ate of Healing", set.GetItemBySlot(EquipSlot.HEAD).Name);
            Assert.AreEqual(360, set.GetItemBySlot(EquipSlot.HEAD).Ilvl);

        }

        [TestMethod]
        public void TestEquipmentSetReplaceItemSummary() {
            EquipmentSet set = new EquipmentSet(testSet);

            double mind = set.StatSummary[CharacterStat.MIND];
            double vitality = set.StatSummary[CharacterStat.VITALITY];
            double crit = set.StatSummary[CharacterStat.CRITICALHIT];
            double det = set.StatSummary[CharacterStat.DETERMINATION];
            double def = set.StatSummary[CharacterStat.DEFENSE];
            double mdef = set.StatSummary[CharacterStat.MAGICDEFENSE];

            // Adds a new helm, which is the downgraded version of the testSet's helm.
            set.AddItem(replacementSet[1]);

            Assert.AreEqual(mind - 9, set.StatSummary[CharacterStat.MIND]);
            Assert.AreEqual(vitality - 11, set.StatSummary[CharacterStat.VITALITY]);
            Assert.AreEqual(crit - 7, set.StatSummary[CharacterStat.CRITICALHIT]);
            Assert.AreEqual(det - 6, set.StatSummary[CharacterStat.DETERMINATION]);
            Assert.AreEqual(def - 8, set.StatSummary[CharacterStat.DEFENSE]);
            Assert.AreEqual(mdef - 13, set.StatSummary[CharacterStat.MAGICDEFENSE]);
        }

        [TestMethod]
        public void TestEquipmentSetReplaceItemMateriaSummary() {
            EquipmentSet set = new EquipmentSet(testSet);
            
            double crit = set.StatSummary[CharacterStat.CRITICALHIT];
            double directhit = set.StatSummary[CharacterStat.DIRECTHIT];

            // Replaces weapon with identical weapon but with different materia.
            set.AddItem(replacementSet[0]);

            Assert.AreEqual(crit - 80, set.StatSummary[CharacterStat.CRITICALHIT]);
            Assert.AreEqual(directhit + 80, set.StatSummary[CharacterStat.DIRECTHIT]);
        }

    }
}
