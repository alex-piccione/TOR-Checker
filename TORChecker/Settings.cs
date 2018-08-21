using System;

namespace TORChecker
{
    public class Settings
    {
        public const string DefaultIPListCsvUrl = "https://torstatus.blutmagie.de/ip_list_exit.php/Tor_ip_list_EXIT.csv";

        public string IPListCsvFileUrl { get; set; }
    }
}
