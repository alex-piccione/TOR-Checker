using System;
using System.Collections.Generic;

using NUnit.Framework;
using TorChecker.Providers;

namespace TorChecker.Test.Inytegration_Tests.Providers
{

    [TestFixture]
    public  class TorProjectProviderTest
    {

        [Test]
        public void ListIpAsync__should__return_a_not_empty_list() {

            TorProjectProvider provider = new TorProjectProvider(Settings.DefaultTorProjectExitAddressesUrl, 1);

            // execute 
            var list = provider.ListIpAsync().Result;

            Assert.NotNull(list);
            Assert.IsNotEmpty(list);

            foreach (var ip in list) {
                // todo: use a Regex
                Assert.That(ip.Split('.').Length == 4, $"{ip} is not an IP address");
                Assert.That(!ip.Contains(" "), $"{ip} is not an IP address");
            }
        }

    }
}
