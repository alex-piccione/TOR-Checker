using System;

namespace TorChecker
{
    public class Settings
    {
        //public const string DefaultBlutmagieCsvFileUrl = "https://torstatus.blutmagie.de/ip_list_exit.php/Tor_ip_list_EXIT.csv";
        public const string DefaultTorProjectExitAddressesUrl = "https://check.torproject.org/exit-addresses";
        public const ushort DefaultProviderRetryLimit = 5;
        public const uint DefaultBackgroundUpdateIntervalMilliseconds = 1000 * 60 * 10; // 10 minutes

        //https://check.torproject.org/cgi-bin/TorBulkExitList.py?ip=1.1.1.1

  
        public Settings () {
            BackgroundUpdateEnabled = true;
        }

        /// <summary>
        /// URL of the CSV file with IP list.
        /// Default: "https://torstatus.blutmagie.de/ip_list_exit.php/Tor_ip_list_EXIT.csv".
        /// </summary>
        //public string BlutmagieCsvFileUrl { get; set; }

        /// <summary>
        /// Exit addresses provided by Tor Project.
        /// </summary>
        public string TorProjectExitAddressesUrl { get; set; }

        /// <summary>
        /// MAx number of retries of every provider for retrieving the IP list.
        /// Default: 5
        /// </summary>
        public ushort ProviderRetryLimit { get; set; }

        /// <summary>
        /// Specify if a background update repeated process should be run.
        /// </summary>
        public bool BackgroundUpdateEnabled { get; set; }

        /// <summary>
        /// Specify the interval for the background update repeated process, when enabled.
        /// </summary>
        public TimeSpan BackgroundUpdateInterval { get; set; }
    }
}
