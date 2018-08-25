using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TorChecker.Providers;

namespace TorChecker.Test.Unit_Tests.Mocks
{
    public class AddressesProviderMock : IAddressesProvider
    {
        private string name;
        private Func<HashSet<string>> listIpFunc;

        public AddressesProviderMock(string name, Func<HashSet<string>> listIpFunc)
        {
            this.name = name;
            this.listIpFunc = listIpFunc;
        }


        public string Name => name;

        public Task<HashSet<string>> ListIpAsync() => Task.Run(listIpFunc);
    }
}
