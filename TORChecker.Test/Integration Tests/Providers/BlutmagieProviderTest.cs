using System;
using System.Collections.Generic;

using NUnit.Framework;
using TorChecker.Providers;

namespace TorChecker.Test.Unit_Tests.Providers
{

    [TestFixture]
    public  class BlutmagieProviderTest
    {

        [Test]
        public void ListIpAsync__should__return_a_not_empty_list() {

            BlutmagieProvider provider = new BlutmagieProvider(TorChecker.Settings.DefaultBlutmagieCsvFileUrl, 1);

            // execute 
            var list = provider.ListIpAsync().Result;

            Assert.NotNull(list);
            Assert.IsNotEmpty(list);
        }

    }
}
