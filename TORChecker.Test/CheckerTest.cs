using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace TORChecker.Test
{

    [TestFixture]
    public class CheckerTest
    {

        [Test]
        public void Check__should__return_True()
        {
            // get a TOR exit node from this list: https://torstatus.blutmagie.de/ip_list_exit.php/Tor_ip_list_EXIT.csv
            var ipAddress = "5.9.195.140";

            // execute
            var checker = new Checker();

            var result = checker.IsUsingTor(ipAddress);

            Assert.IsTrue(result);
        }

        [Test]
        public void Check__should__return_False()
        {
            // get my current IP
            var ipAddress = "83.244.202.4";

            // execute
            var checker = new Checker();

            var result = checker.IsUsingTor(ipAddress);

            Assert.IsFalse(result);
        }
    }
}
