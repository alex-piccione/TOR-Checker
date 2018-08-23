using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using Flurl.Http;

namespace TorChecker
{
    public class Checker : IChecker
    {
        private Settings settings;

        private HashSet<string> ipList;
        private Timer timer;
        private object upateLock = new object();

        /// <summary>
        /// Last successfull update.
        /// </summary>
        public DateTime LastUpdate { get; set; }


        public Checker()
        {
            this.settings = new Settings();
        }

        public Checker(Settings settings)
        {
            this.settings = settings;

            VerifySettings(settings);

            if (settings.BackgroundUpdateEnabled)
                StartBackgroundUpdateProcess();
        }


        public bool IsUsingTor(string ipAddress)
        {
            if (ipList == null)
                lock (upateLock)
                {
                    LoadIPLIst();
                }

            return ipList.Contains(ipAddress);
        }


        private void VerifySettings(Settings settings)
        {
            if (settings.IPListCsvFileUrl == null) settings.IPListCsvFileUrl = Settings.DefaultIPListCsvUrl;
            if (settings.LoadCsvRetry == 0) settings.LoadCsvRetry = Settings.DefaultLoadCsvRetry;
            if (settings.BackgroundUpdateEnabled && settings.BackgroundUpdateInterval == default(TimeSpan))
                settings.BackgroundUpdateInterval = TimeSpan.FromMilliseconds(Settings.DefaultBackgroundUpdateIntervalMilliseconds);
        }

        private void StartBackgroundUpdateProcess()
        {
            timer = new Timer(settings.BackgroundUpdateInterval.TotalMilliseconds);
            timer.Elapsed += RunBackgroundUpdate;
            timer.AutoReset = true;
            timer.Start();
        }

        private void RunBackgroundUpdate(object sender, ElapsedEventArgs e)
        {
            lock (upateLock)
            {
                try
                {
                    LoadIPLIst();
                }
                catch { }
            }
        }

        private void LoadIPLIst()
        {
            ipList = new HashSet<string>();

            string csvData = null;
            Exception latException = null;

            int retry = 0;
            while (csvData == null && retry++ < settings.LoadCsvRetry)
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
                throw new Exception("Fail to load or process CSV data.");

            LastUpdate = DateTime.UtcNow;
        }
    }
}
