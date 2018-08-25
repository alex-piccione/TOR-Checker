using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TorChecker.Providers
{
    public interface IAddressesProvider
    {
        string Name { get; }
        Task<HashSet<string>> ListIpAsync();
    }
}
