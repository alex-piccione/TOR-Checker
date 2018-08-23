using System;

namespace TorChecker
{
    public class Settings
    {
        public const string DefaultIPListCsvUrl = "https://torstatus.blutmagie.de/ip_list_exit.php/Tor_ip_list_EXIT.csv";
        public const ushort DefaultLoadCsvRetry = 5;
        public const uint DefaultBackgroundUpdateIntervalMilliseconds = 1000 * 60 * 10; // 10 minutes

  
        public Settings () {
            BackgroundUpdateEnabled = true;
        }

        /// <summary>
        /// URL of the CSV file with IP list.
        /// Default: "https://torstatus.blutmagie.de/ip_list_exit.php/Tor_ip_list_EXIT.csv".
        /// </summary>
        public string IPListCsvFileUrl { get; set; }

        /// <summary>
        /// Number of retries of loading the CSV file.
        /// Default: 5
        /// </summary>
        public ushort LoadCsvRetry { get; set; }

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
