using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

using Flurl.Http;
using TorChecker.Providers;

namespace TorChecker
{
    public class Checker : IChecker
    {
        private Settings settings;
        private HashSet<string> ipList;
        private IEnumerable<IAddressesProvider> providers;
        private System.Timers.Timer timer;
        private object updateLock = new object();

        /// <summary>
        /// Last successfull update.
        /// </summary>
        public DateTime LastUpdate { get; set; }


        public Checker()
        {
            settings = new Settings();

            InitializeProvidsers();
        }


        public Checker(Settings settings)
        {
            this.settings = settings;

            VerifySettings(settings);

            if (settings.BackgroundUpdateEnabled)
                StartBackgroundUpdateProcess();

            InitializeProvidsers();
        }



        public bool IsUsingTor(string ipAddress)
        {
            if (ipList == null)
                lock (updateLock)
                {
                    LoadIPLIst();
                }

            return ipList.Contains(ipAddress);
        }


        private void VerifySettings(Settings settings)
        {
            if (settings.BlutmagieCsvFileUrl == null) settings.BlutmagieCsvFileUrl = Settings.DefaultBlutmagieCsvFileUrl;
            if (settings.ProviderRetryLimit == 0) settings.ProviderRetryLimit = Settings.DefaultProviderRetryLimit;
            if (settings.BackgroundUpdateEnabled && settings.BackgroundUpdateInterval == default(TimeSpan))
                settings.BackgroundUpdateInterval = TimeSpan.FromMilliseconds(Settings.DefaultBackgroundUpdateIntervalMilliseconds);
        }

        private void InitializeProvidsers()
        {
            providers = new IAddressesProvider[] {
                new BlutmagieProvider(settings.BlutmagieCsvFileUrl, settings.ProviderRetryLimit),
                new TorProjectExitAddressesProvider()
            };
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
            lock (updateLock)
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

            var tasks = new List<Task<HashSet<string>>>();
            foreach (var provider in providers)
                tasks.Add(Task.Run(() => provider.ListIpAsync()));

            var continuation = Task.WhenAll(tasks);

            try {
                continuation.Wait();
            }
            catch (AggregateException exc) {
                throw exc.GetBaseException();
            }


            if (continuation.Status == TaskStatus.RanToCompletion)
                foreach (var result in continuation.Result)
                    ipList.UnionWith(result);
            else
            {
                // todo: get the provider Name
                var errors = from t in tasks
                             where t.Status != TaskStatus.RanToCompletion
                             select $"Status: {t.Status}, Error: {t.Exception}";

                throw new Exception(string.Join(" ", errors));
            }

            LastUpdate = DateTime.UtcNow;
        }

    }
}
