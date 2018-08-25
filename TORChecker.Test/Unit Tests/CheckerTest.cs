using NUnit.Framework;
using System;
using System.Collections.Generic;
using TorChecker.Providers;
using TorChecker.Test.Unit_Tests.Mocks;

namespace TorChecker.Test.Unit_Tests
{
    [TestFixture]
    public class CheckerTest
    {
        [Test]
        public void IsUsingTor__when__a_Provider_Fail__should__throw_an_Exception()
        {
            var ipAddress = "1.1.1.1";
            var checker = new Checker();


            var goodAddressesProvider = new AddressesProviderMock("good", () => new HashSet<string>());
            var badAddressesProvider = new AddressesProviderMock("bad", () => throw new Exception("error"));


            checker.Providers = new List<IAddressesProvider>() {
                goodAddressesProvider, badAddressesProvider
            };

            try
            {
                checker.IsUsingTor(ipAddress);
            }
            catch (Exception exc)
            {
                Assert.AreEqual("error", exc.Message);
            }
        }

        [Test]
        public void IsUdingTor__should__return_True()
        {
            var ipAddress = "1.1.1.1";
            var checker = new Checker();

            var goodAddressesProvider = new AddressesProviderMock("good", () => new HashSet<string>() { ipAddress });

            checker.Providers = new List<IAddressesProvider>() { goodAddressesProvider };

            // execute
            var result = checker.IsUsingTor(ipAddress);

            Assert.IsTrue(result);
        }

    }
}
