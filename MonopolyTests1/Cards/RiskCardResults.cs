using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyTests.Tests
{
    [TestClass()]
    public class RiskCardResults
    {
        [TestMethod()]
        public void RiskCardTest()
        {
            RiskCard rc = new RiskCard("description;plus;50;12");
            Assert.AreEqual(rc.MoneyChange, 50);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void RiskCardTest1()
        {
            RiskCard rc = new RiskCard(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void RiskCardTest2()
        {
            RiskCard rc = new RiskCard("aaaa;aa;a;a");

        }
    }
}