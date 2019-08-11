using System;
using System.IO;
using System.Threading;
using static AutoShot.HelperFunctions;

namespace AutoShot {
    class Program {
        private static int timeSeconds;
        private static long screenshotQuality;
        static void Main(string[] args) {
            log("---STARTING---");
            Console.Title = "AutoShot by Viktor Eriksson";

            log("checking if config file exists");
            Config config = new Config("config.xml");
            if(!config.ConfigExists()) {
                log("writing default config");
                config.WriteDefaultConfig();

                Console.WriteLine("kindly configure config.xml to your liking,\nthen run this program again");
                Console.ReadKey();
                return;
            }
            log("reading config");
            timeSeconds = int.Parse(config
                .ReadConfigVariable("Config/Options/ScreenshotDelaySeconds"));
            screenshotQuality = long.Parse(config
                .ReadConfigVariable("Config/Options/ScreenshotQuality"));

            log("delay set to " + timeSeconds + " seconds");
            log("quality set to " + screenshotQuality);
            log("checking if directory AutoShot exists");
            if(!Directory.Exists("AutoShot")) {
                log("does not, creating");
                Directory.CreateDirectory("AutoShot");
            }

            log("starting loop");
            while(true) {
                string formatString = String.Format("AutoShot\\autoshot_{0}.jpeg",
                GetDateString("ddMMyyyy_HH_mm_ss"));

                TakeScreenshot(formatString, screenshotQuality);
                log(String.Format("saved screenshot to {0}", formatString));

                Thread.Sleep(timeSeconds*1000);
            }
        }
    }
}
