using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TorChecker.Providers
{
    internal class TorProjectExitAddressesProvider : IAddressesProvider
    {
        public string Name => "TorProjectExitAddresses";

        public Task<HashSet<string>> ListIpAsync()
        {
            //throw new NotImplementedException();
            return Task.FromResult(new HashSet<string>());
        }
    }
}
