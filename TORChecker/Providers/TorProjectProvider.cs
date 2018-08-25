using Flurl.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TorChecker.Providers
{
    internal class TorProjectProvider : IAddressesProvider
    {
        public string Name => "TorProject";

        private string exitAddressesUrl;
        private int retryLimit;

        public TorProjectProvider(string exitAddressesUrl, int retryLimit)
        {
            if (retryLimit <= 0)
                throw new ArgumentOutOfRangeException(nameof(retryLimit), retryLimit, "Must be greater than zero.");

            this.exitAddressesUrl = exitAddressesUrl ?? throw new ArgumentOutOfRangeException(nameof(exitAddressesUrl), exitAddressesUrl, "Cannot be null.");
            this.retryLimit = retryLimit;
        }

        public async Task<HashSet<string>> ListIpAsync()
        {
            Exception lastException = null;
            int retry = 0;
            string data = null;
            while (data == null && retry++ < retryLimit)
            {
                try
                {
                    data = await exitAddressesUrl.GetAsync().ReceiveString();
                }
                catch (Exception exc)
                {
                    lastException = exc;
                }
            }

            if (data == null && lastException != null)
                throw lastException;

            var list = new HashSet<string>();

            using (var reader = new StringReader(data))
            {   
                while (true) {
                    string line = reader.ReadLine();
                    if (line == null) break;
                    var ip = ExtractIp(line);
                    if(ip != null)
                        list.Add(ip);
                }
            }

            return list;
        }


        // Lines with IP address: "ExitAddress 199.249.223.62 2018-08-25 07:07:23"
        private static Regex ipAddress = new Regex(@"^ExitAddress ([\d.]+)", RegexOptions.Compiled);
        private string ExtractIp(string line)
        {
            var match = ipAddress.Match(line);
            return match.Success ? match.Groups[1].Value : null;
        }
    }
}
