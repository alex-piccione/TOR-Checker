using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Flurl.Http;

namespace TorChecker.Providers
{
    internal class DanMeUkProvider : IAddressesProvider
    {
        public string Name => "dan.me.uk";
        public TimeSpan CacheDuration = TimeSpan.FromMinutes(30);

        public async Task<HashSet<string>> ListIpAsync()
        {
            // cached data exists and is still valid ?
            if (cachedIPs != null && DateTime.Now > lastRead.Value + CacheDuration)
                return cachedIPs;            

            try
            {
                var list = await "https://www.dan.me.uk/torlist/".GetAsync().ReceiveString();

                var ips = new HashSet<string>();

                using (var reader = new StringReader(list)) {                    
                    string line = "";
                    do
                    {
                        line = reader.ReadLine();
                        ips.Add(line);
                    }
                    while (line != "");
                }

                cachedIPs = ips;
                lastRead = DateTime.Now;
                return ips;
            }
            catch (Exception exc)
            {
                throw new Exception($@"Fail to load data from ""{Name}"".", exc);
            }
        }


        //private readonly TimeSpan cacheDuration = TimeSpan.FromMinutes(30);        
        private HashSet<string> cachedIPs = null;
        private DateTime? lastRead = null;
    }
}
