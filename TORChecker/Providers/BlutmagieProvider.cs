using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Flurl.Http;

namespace TorChecker.Providers
{
    internal class BlutmagieProvider : IAddressesProvider
    {
        private string blutmagieUrl;
        private int retryLimit;
        

        public BlutmagieProvider(string blutmagieUrl, int retryLimit) {
            this.blutmagieUrl = blutmagieUrl;
            this.retryLimit = retryLimit;
        }
        public string Name => "Blutmagie";


        public HashSet<string> ListIp() => ListIpAsync().Result;

        public async Task<HashSet<string>> ListIpAsync()
        {
            string csvData = null;
            Exception latException = null;

            int retry = 0;
            while (csvData == null && retry++ < retryLimit)
            {
                try
                {
                    csvData = await blutmagieUrl.GetAsync().ReceiveString();
                }
                catch (Exception exc)
                {
                    latException = exc;
                }
            }

            if (csvData == null && latException != null)
                throw latException;

            var ipList = new HashSet<string>();

            using (var reader = new StringReader(csvData))
            {
                var read = true;
                while (read)
                {
                    var ip = reader.ReadLine();
                    if (ip == null)
                        read = false;
                    else
                        ipList.Add(ip);
                }
            }

            if (ipList.Count == 0)
                throw new Exception("Fail to load or process CSV data.");

            return ipList;
        }
  
    }
}
