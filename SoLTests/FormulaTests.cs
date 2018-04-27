
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simulator_of_Light.Simulator.Resources;

namespace SoLTests
{
    [TestClass]
    public class FormulaTests
    {
        [TestMethod]
        public void TestActionDamageAccuracy() {

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

            double traitMultiplier = 0.3;

            double result = Formulas.calculateActionDamage(potency, weaponDamage, attackPower, determination, jobID, stat);
            result = result * (1 + traitMultiplier);

            Assert.IsTrue((result > (empiricalMid * 0.995)) && (result < (empiricalMid * 1.005)));
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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
    }
}
