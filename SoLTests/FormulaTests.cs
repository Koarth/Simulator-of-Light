
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simulator_of_Light.Simulator.Resources;

namespace SoLTests
{
    [TestClass]
    public class FormulaTests
    {
        [TestMethod,Description("Ensure accuracy of basic damage formula for action compared to in-game testing.")]
        public void TestActionDamageAccuracyWHM() {

            // Example: i368 WHM using Stone IV
            double potency = 250;
            double weaponDamage = 140;
            double attackPower = 2837;
            double determination = 1489;
            Constants.JobID jobID = Constants.JobID.WHM;
            Constants.PrimaryStat stat = Constants.PrimaryStat.MND;

            double empiricalMax = 7497; // via in-game testing
            double empiricalMin = 6814; // via in-game testing
            double empiricalMid = (empiricalMax + empiricalMin) / 2;

            double traitMultiplier = 1.3;
            double determinationMultiplier = Formulas.calculateDeterminationMultiplier(determination);

            double result = Formulas.calculateActionDamage(potency, weaponDamage, attackPower, jobID, stat);
            result = Math.Truncate(result * traitMultiplier * determinationMultiplier);

            Assert.IsTrue((result > (empiricalMid * 0.99)) && (result < (empiricalMid * 1.01)));
        }

        [TestMethod,Description("Ensure accuracy of basic damage formula for action compared to in-game testing.")]
        public void TestActionDamageAccuracyWAR() {

            // Example: i340 WAR using Heavy Swing
            double potency = 160;
            double weaponDamage = 99;
            double attackPower = 2149;
            double determination = 1236;
            Constants.JobID jobID = Constants.JobID.WAR;
            Constants.PrimaryStat stat = Constants.PrimaryStat.STR;
            double tenacity = 1007;

            double empiricalMax = 2095; // via in-game testing
            double empiricalMin = 1903; // via in-game testing
            double empiricalMid = (empiricalMax + empiricalMin) / 2;

            double traitMultiplier = 1;
            double tenacityMultiplier = Formulas.calculateTenacityMultiplier(tenacity);
            double determinationMultiplier = Formulas.calculateDeterminationMultiplier(determination);

            double result = Formulas.calculateActionDamage(potency, weaponDamage, attackPower, jobID, stat);
            result = Math.Truncate(result * tenacityMultiplier * determinationMultiplier * traitMultiplier);

            Assert.IsTrue((result > (empiricalMid * 0.99)) && (result < (empiricalMid * 1.01)));
        }

        [TestMethod,Description("Ensure accuracy of basic damage formula for autoattacks compared to in-game testing.")]
        public void TestAutoAttackDamageAccuracyWHM() {

            // Example: i368 WHM autoattacks
            Constants.JobID jobID = Constants.JobID.WHM;
            double weaponDamage = 104;
            double attackPower = 162;
            double autoAttackDelay = 3.44;
            double determination = 1489;
            double skillSpeed = 364;

            double empiricalMax = 74; // via in-game testing
            double empiricalMin = 67; // via in-game testing
            double empiricalMid = Math.Floor((empiricalMax + empiricalMin) / 2);

            double determinationMultipler = Formulas.calculateDeterminationMultiplier(determination);
            double speedMultiplier = Formulas.calculateSpeedMultiplier(skillSpeed);

            double result = Formulas.calculateAutoAttackDamage(weaponDamage, autoAttackDelay, attackPower, jobID);
            result = Math.Truncate(Math.Truncate(result * determinationMultipler) * speedMultiplier);

            Assert.IsTrue((result > (empiricalMid * 0.99)) && (result < (empiricalMid * 1.01)));
        }

        [TestMethod,Description("Ensure accuracy of basic damage formula for autoattacks compared to in-game testing.")]
        public void TestAutoAttackDamageAccuracyWAR() {

            // Example: i340 WAR autoattacks
            Constants.JobID jobID = Constants.JobID.WAR;
            double weaponDamage = 99;
            double attackPower = 2149;
            double autoAttackDelay = 3.36;
            double determination = 1236;
            double skillSpeed = 1313;
            double tenacity = 1007;

            double empiricalMax = 1699; // via in-game testing
            double empiricalMin = 1541; // via in-game testing
            double empiricalMid = Math.Floor((empiricalMax + empiricalMin) / 2);

            double determinationMultiplier = Formulas.calculateDeterminationMultiplier(determination);
            double speedMultiplier = Formulas.calculateSpeedMultiplier(skillSpeed);
            double tenacityMultiplier = Formulas.calculateTenacityMultiplier(tenacity);

            double result = Formulas.calculateAutoAttackDamage(weaponDamage, autoAttackDelay, attackPower, jobID);
            result = Math.Truncate(Math.Truncate(result * tenacityMultiplier * determinationMultiplier) * speedMultiplier);

            Assert.IsTrue((result > (empiricalMid * 0.99)) && (result < (empiricalMid * 1.01)));
        }

        [TestMethod,Description("Ensure determination formula matches publically posted stat intervals.")]
        public void TestDeterminationIntervals() {
            try {
                Formulas.calculateDeterminationMultiplier(200);
                Assert.Fail();
            } catch (ArgumentException) {
            } catch (Exception) {
                Assert.Fail();
            }
            Assert.AreEqual(1, Formulas.calculateDeterminationMultiplier(292));
            Assert.AreEqual(1.001, Formulas.calculateDeterminationMultiplier(309));
            Assert.AreEqual(1.002, Formulas.calculateDeterminationMultiplier(326));
            Assert.AreEqual(1.003, Formulas.calculateDeterminationMultiplier(343));
            Assert.AreEqual(1.003, Formulas.calculateDeterminationMultiplier(358));
            Assert.AreEqual(1.001, Formulas.calculateDeterminationMultiplier(309));
            Assert.AreEqual(1.01, Formulas.calculateDeterminationMultiplier(459));
            Assert.AreEqual(1.04, Formulas.calculateDeterminationMultiplier(960));
            Assert.AreEqual(1.1, Formulas.calculateDeterminationMultiplier(1962));

        }

        [TestMethod,Description("Ensure critical rate formula matches publically posted stat intervals.")]
        public void TestCriticalRateIntervals() {
            try {
                Formulas.calculateCriticalHitRate(200);
                Assert.Fail();
            } catch (ArgumentException) {
            } catch (Exception) {
                Assert.Fail();
            }
            Assert.AreEqual(0.05, Formulas.calculateCriticalHitRate(364));
            Assert.AreEqual(0.051, Formulas.calculateCriticalHitRate(375));
            Assert.AreEqual(0.052, Formulas.calculateCriticalHitRate(386));
            Assert.AreEqual(0.053, Formulas.calculateCriticalHitRate(397));
            Assert.AreEqual(0.053, Formulas.calculateCriticalHitRate(407));
            Assert.AreEqual(0.06, Formulas.calculateCriticalHitRate(473));
            Assert.AreEqual(0.1, Formulas.calculateCriticalHitRate(907));
            Assert.AreEqual(0.2, Formulas.calculateCriticalHitRate(1992));
        }

        [TestMethod,Description("Ensure critical multiplier formula matches publically posted stat intervals.")]
        public void TestCriticalMultiplierIntervals() {
            try {
                Formulas.calculateCriticalHitMultiplier(200);
                Assert.Fail();
            } catch (ArgumentException) {
            } catch (Exception) {
                Assert.Fail();
            }
            Assert.AreEqual(1.4, Formulas.calculateCriticalHitMultiplier(364));
            Assert.AreEqual(1.401, Formulas.calculateCriticalHitMultiplier(375));
            Assert.AreEqual(1.402, Formulas.calculateCriticalHitMultiplier(386));
            Assert.AreEqual(1.403, Formulas.calculateCriticalHitMultiplier(397));
            Assert.AreEqual(1.403, Formulas.calculateCriticalHitMultiplier(407));
            Assert.AreEqual(1.410, Formulas.calculateCriticalHitMultiplier(473));
            Assert.AreEqual(1.45, Formulas.calculateCriticalHitMultiplier(907));
            Assert.AreEqual(1.55, Formulas.calculateCriticalHitMultiplier(1992));
        }

        [TestMethod,Description("Ensure direct hit rate formula matches publically posted stat intervals.")]
        public void TestDirectHitRateIntervals() {
            try {
                Formulas.calculateDirectHitRate(200);
                Assert.Fail();
            } catch (ArgumentException) {
            } catch (Exception) {
                Assert.Fail();
            }
            Assert.AreEqual(0, Formulas.calculateDirectHitRate(364));
            Assert.AreEqual(0.001, Formulas.calculateDirectHitRate(368));
            Assert.AreEqual(0.002, Formulas.calculateDirectHitRate(372));
            Assert.AreEqual(0.003, Formulas.calculateDirectHitRate(376));
            Assert.AreEqual(0.003, Formulas.calculateDirectHitRate(379));
            Assert.AreEqual(0.05, Formulas.calculateDirectHitRate(562));
            Assert.AreEqual(0.55, Formulas.calculateDirectHitRate(2534));
        }

        [TestMethod,Description("Ensure speed formula matches publically posted stat intervals.")]
        public void TestSpeedIntervals() {
            try {
                Formulas.calculateSpeedMultiplier(200);
                Assert.Fail();
            } catch (ArgumentException) {
            } catch (Exception) {
                Assert.Fail();
            }
            Assert.AreEqual(1, Formulas.calculateSpeedMultiplier(364));
            Assert.AreEqual(1.001, Formulas.calculateSpeedMultiplier(381));
            Assert.AreEqual(1.002, Formulas.calculateSpeedMultiplier(398));
            Assert.AreEqual(1.003, Formulas.calculateSpeedMultiplier(415));
            Assert.AreEqual(1.003, Formulas.calculateSpeedMultiplier(430));
            Assert.AreEqual(1.01, Formulas.calculateSpeedMultiplier(531));
            Assert.AreEqual(1.05, Formulas.calculateSpeedMultiplier(1199));
            Assert.AreEqual(1.1, Formulas.calculateSpeedMultiplier(2034));
        }

        [TestMethod,Description("Ensure tenacity formula matches publically posted stat intervals.")]
        public void TestTenacityIntervals() {
            try {
                Formulas.calculateTenacityMultiplier(200);
                Assert.Fail();
            } catch (ArgumentException) {
            } catch (Exception) {
                Assert.Fail();
            }
            Assert.AreEqual(1, Formulas.calculateTenacityMultiplier(364));
            Assert.AreEqual(1.001, Formulas.calculateTenacityMultiplier(386));
            Assert.AreEqual(1.002, Formulas.calculateTenacityMultiplier(408));
            Assert.AreEqual(1.003, Formulas.calculateTenacityMultiplier(430));
            Assert.AreEqual(1.003, Formulas.calculateTenacityMultiplier(450));
            Assert.AreEqual(1.01, Formulas.calculateTenacityMultiplier(581));
            Assert.AreEqual(1.05, Formulas.calculateTenacityMultiplier(1449));
            Assert.AreEqual(1.1, Formulas.calculateTenacityMultiplier(2534));
        }

        [TestMethod]
        public void TestMPFormulaWHM() {
            Constants.JobID jobID = Constants.JobID.WHM;
            double piety = 1039;

            Assert.AreEqual(17441, Formulas.calculateTotalMana(piety, jobID));
        }

        [TestMethod]
        public void TestMPFormulaAST() {
            Constants.JobID jobID = Constants.JobID.AST;
            double piety = 1350;

            Assert.AreEqual(18507, Formulas.calculateTotalMana(piety, jobID));
        }
    }
}
