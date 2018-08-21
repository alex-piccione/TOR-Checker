using System;
using System.Collections.Generic;
using System.IO;
using Flurl.Http;

namespace TORChecker
{
    public class Checker : IChecker
    {
        private Settings settings;

        private HashSet<string> ipList;


        public Checker(Settings settings)
        {
            this.settings = settings;
        }

        public bool IsUsingTor(string ipAddress)
        {
            LoadIPLIst();

            return ipList.Contains(ipAddress);
        }


        private void LoadIPLIst()
        {
            ipList = new HashSet<string>();

            string csvData = null;
            Exception latException = null;

            int retry = 0;
            while (csvData == null && retry++ < 3)
            {
                try
                {
                    csvData = settings.IPListCsvFileUrl.GetAsync().ReceiveString().Result;
                }
                catch (Exception exc)
                {
                    latException = exc;
                }
            }
            
            if (csvData == null && latException != null)
                throw latException;

            using (var reader = new StringReader(csvData)) { 
                var read = true;
                while (read) {
                    var ip = reader.ReadLine();
                    if (ip == null)
                        read = false;
                    else
                        ipList.Add(ip);
                }
            }

            if (ipList.Count == 0)
                throw new Exception("Fail to load or process CSV data0");
        }
    }
}
