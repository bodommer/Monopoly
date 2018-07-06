using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyTests.Tests
{
    [TestClass()]
    public class MonopolyTestResults
    {
        [TestMethod()]
        public void RiskCardManagerTest()
        {
            RiskCardManager rcm = new RiskCardManager("aaaa");
        }

        [TestMethod()]
        public void RiskCardManagerTest1()
        {
            Assert.Fail();
        }
    }
}