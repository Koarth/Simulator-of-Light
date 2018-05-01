using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simulator_of_Light.Simulator.Models;
using Simulator_of_Light.Simulator;


namespace SoLTests {

    [TestClass]
    public class ActionAuraTests {

        [TestMethod]
        public void TestActionInstantiationSuccess() {

            var actions = BaseActionFactory.getBaseActionsByJobID(JobID.WHM);

            Assert.IsInstanceOfType(actions, typeof(System.Collections.Generic.Dictionary<string, BaseAction>));
        }

        [TestMethod]
        public void TestActionListNotEmpty() {
            var actions = BaseActionFactory.getBaseActionsByJobID(JobID.WHM);

            Assert.IsTrue(actions.Count > 0);
        }

        [TestMethod]
        public void TestActionFlyweight() {
            var actions = BaseActionFactory.getBaseActionsByJobID(JobID.WHM);
            var actions2 = BaseActionFactory.getBaseActionsByJobID(JobID.WHM);
            Assert.AreSame(actions, actions2);

            var action = actions["Aero"];
            var action2 = actions2["Aero"];
            Assert.AreSame(action, action2);
        }

        [TestMethod]
        public void TestActionDefaultValues() {
            var actions = BaseActionFactory.getBaseActionsByJobID(JobID.WHM);

            var action = actions["Presence of Mind"];
            Assert.AreEqual(3, action.Range);
            Assert.AreEqual(0, action.Radius);
        }

        [TestMethod]
        public void TestAurasInitialized() {
            var actions = BaseActionFactory.getBaseActionsByJobID(JobID.WHM);

            var action = actions["Aero"];
            Assert.IsFalse(action.AurasApplied.Count == 0);
            Assert.AreEqual("Aero", action.AurasApplied[0].Name);
        }

        [TestMethod]
        public void TestActionIntegrity() {
            var actions = BaseActionFactory.getBaseActionsByJobID(JobID.WHM);

            var action = actions["Aero"];
            Assert.AreEqual("Aero", action.Name);
            Assert.AreEqual(JobID.WHM, action.JobID);
            Assert.AreEqual(CharacterStat.MIND, action.Stat);
            Assert.AreEqual(ActionAspect.MAGIC, action.Aspect);
            Assert.AreEqual(30.0, action.Potency);
            Assert.AreEqual(480.0, action.MpCost);
            Assert.AreEqual(2.5, action.RecastTime);
            Assert.AreEqual(25.0, action.Range);
        }
        
        [TestMethod]
        public void TestActionAuraIntegrity() {
            var actions = BaseActionFactory.getBaseActionsByJobID(JobID.WHM);
            var action = actions["Aero"];
            var aura = action.AurasApplied[0];
            Assert.AreEqual("Aero", aura.Name);
            Assert.AreEqual(JobID.WHM, aura.JobID);
            Assert.AreEqual(18.0, aura.Duration);
            Assert.AreEqual(30.0, aura.DamageOverTimePotency);
            Assert.AreEqual(ActionAspect.MAGIC, aura.DamageAspect);
        }
    }
}