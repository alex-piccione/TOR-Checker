using System;
using System.Collections.Generic;

using NUnit.Framework;
using Flurl.Http;

namespace TorChecker.Test.Integration_Tests
{

    [TestFixture]
    public class CheckerTest
    {

        //[Test]
        //public void Check__should__return_False()
        //{
        //    var ipAddress = "1.1.1.1";
        //    var settings = new TorChecker.Settings();
        //    settings.BlutmagieCsvFileUrl = "https://torstatus.blutmagie.de/ip_list_exit.php/Tor_ip_list_EXIT.csv";

        //    // execute
        //    var checker = new TorChecker.Checker(settings);

        //    var result = checker.IsUsingTor(ipAddress);

        //    Assert.IsFalse(result);
        //}



        private string GetIpAddressFromList(string ipListCsvUrl)
        {
            var list = ipListCsvUrl.GetStringAsync().Result.Substring(0, 100);
            var ip = list.Split('\n')[0];
            return ip;
        }
    }
}
