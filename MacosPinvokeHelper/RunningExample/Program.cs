using System;
using System.Collections.Generic;
using MacosPinvokeHelper;

namespace RunningExample
{
    class Program
    {
        public static void PrintApplicationData(MacWindowHelper macWindowHelper, IntPtr appPtr)
        {
            Console.WriteLine($"Application name: {macWindowHelper.GetApplicationName(appPtr)}");
            Console.WriteLine($"- Active: {macWindowHelper.IsApplicationActive(appPtr)}");
            Console.WriteLine($"- PID: {macWindowHelper.GetApplicationPid(appPtr)}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("------------------- PINVOKE -----------------------------");
            using (var macWindowHelper = new MacWindowHelper())
            {
                var pids = new HashSet<int>();

                // All visible applications
                foreach (IntPtr appPtr in macWindowHelper.VisibleApplications)
                {
                    var pid = macWindowHelper.GetApplicationPid(appPtr);
                    pids.Add(pid);

                    PrintApplicationData(macWindowHelper, appPtr);
                }

                // Frontmost Application
                Console.WriteLine("----------------------- Frontmost Application ---------------------");
                IntPtr frontmostApplication = macWindowHelper.FrontmostApplication;
                PrintApplicationData(macWindowHelper, frontmostApplication);

                // VisibleWindows
                Console.WriteLine("----------------------- Visible Windows ---------------------");
                macWindowHelper.VisibleWindows(pids);
            }
        }
    }
}
