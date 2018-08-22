using System;
using System.Collections.Generic;
using System.Text;

namespace TORChecker
{
    public class ExampleOfUsage
    {

        public void MyMethod()
        {
            // Initialize

            // Set it to autoupdate the list every "settings.DefaultBacgroundUpdateIntervalMilliseconds" minutes
            var settings = new Settings { BacgroundUpdateEnabled = true };            
            var checker = new Checker(settings);


            // Use
            var clientIsUsingTor = checker.IsUsingTor("1.1.1.1");

            // it raise an Exception if fail
        }


    }
}
