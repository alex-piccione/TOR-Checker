using System;

namespace TORChecker
{
    public class Settings
    {
        public const string DefaultIPListCsvUrl = "https://torstatus.blutmagie.de/ip_list_exit.php/Tor_ip_list_EXIT.csv";

        public const ushort DefaultLoadCsvRetry = 5;

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
    }
}
