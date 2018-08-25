using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TorChecker.Providers
{
    public interface IAddressesProvider
    {
        string Name { get; }
        //HashSet<string> ListIp();
        Task<HashSet<string>> ListIpAsync();
    }
}
