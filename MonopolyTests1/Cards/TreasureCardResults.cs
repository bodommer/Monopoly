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
    public class TreasureCardResults
    {
        [TestMethod()]
        public void TreasureCardTest()
        {
            TreasureCard tc = new TreasureCard("text;2.5");
            Assert.AreEqual(tc.MoneyChange, 2.5);
            Assert.AreEqual(tc.Description, "text");
        }

        [TestMethod()]
        [ExpectedException (typeof(FormatException))]
        public void TreasureCardTest1()
        {
            TreasureCard tc = new TreasureCard("aa;aa");
        }
    }
}