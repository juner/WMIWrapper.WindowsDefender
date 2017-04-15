using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMIWrapper.WindowsDefender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMIWrapper.WindowsDefender.Tests
{
    [TestClass()]
    public class MSFT_MpThreatTests
    {
        [TestMethod()]
        public void SearchAllTest()
        {
            var collection = MSFT_MpThreat.SearchAll();
        }
        [TestMethod()]
        public void SearchByThreatIDTest()
        {
            var threat = MSFT_MpThreat.SearchAll().FirstOrDefault();
            if (threat != null)
            {
                var newThreat = MSFT_MpThreat.SearchByThreatID(threat.ThreatID);
                Assert.AreEqual(newThreat.ThreatID, threat.ThreatID);
                return;
            }
            Assert.Inconclusive("Not have " + nameof(MSFT_MpThreat) + " Item.");
        }
        [TestMethod()]
        public void ToStringTest()
        {
            var threat = MSFT_MpThreat.SearchAll().FirstOrDefault();
            if (threat != null)
            {
                var str = threat.ToString();
                return;
            }
            Assert.Inconclusive("Not have " + nameof(MSFT_MpThreat) + " Item.");
        }
        [TestMethod()]
        public void RemoveTest()
        {
            var threat = MSFT_MpThreat.SearchAll().FirstOrDefault();
            if (threat != null)
            {
                threat.Remove();
                return;
            }
            Assert.Inconclusive("Not have " + nameof(MSFT_MpThreat) + " Item.");
        }
    }
}