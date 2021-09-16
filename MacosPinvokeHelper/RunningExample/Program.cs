using System;

namespace RunningExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------- PINVOKE -----------------------------");
            using (var macWindowHelper = new MacWindowHelper())
            {
                // All visible applications
                foreach (IntPtr appPtr in macWindowHelper.VisibleApplications)
                {
                    Console.WriteLine($"Application name: {macWindowHelper.GetApplicationName(appPtr)}");
                    Console.WriteLine($"- Active: {macWindowHelper.IsApplicationActive(appPtr)}");
                }

                // Frontmost Application
                Console.WriteLine("----------------------- Frontmost Application ---------------------");
                IntPtr frontmostApplication = macWindowHelper.FrontmostApplication;

                Console.WriteLine($"Application name: {macWindowHelper.GetApplicationName(frontmostApplication)}");
                Console.WriteLine($"- Active: {macWindowHelper.IsApplicationActive(frontmostApplication)}");

                // VisibleWindows
                Console.WriteLine("----------------------- Visible Windows ---------------------");
                var a = macWindowHelper.VisibleWindows;
            }
        }
    }
}
