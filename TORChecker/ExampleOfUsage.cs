using System;

namespace Example
{
    public class ExampleOfUsage
    {

        public void Example_1()
        {
            // The checker does not make any update in background and execute the "check" on demand
            var checker = new TorChecker.Checker();


            // Use
            var clientIsUsingTor = checker.IsUsingTor("1.1.1.1");
        }


        public void Example_2()
        {
            // Set it to autoupdate the list every "settings.DefaultBacgroundUpdateIntervalMilliseconds" minutes
            var settings = new TorChecker.Settings { BackgroundUpdateEnabled = true };
            var checker = new TorChecker.Checker(settings);


            // Use
            var clientIsUsingTor = checker.IsUsingTor("1.1.1.1");
        }


    }
}
