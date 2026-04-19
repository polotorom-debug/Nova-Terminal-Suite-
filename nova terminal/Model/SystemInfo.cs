using System;
using System.Diagnostics;

namespace NovaConsole.Models
{
    public class SystemInfo
    {
        public static double GetCpuUsage()
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuCounter.NextValue(); // First call always returns 0
            System.Threading.Thread.Sleep(100);
            return Math.Round(cpuCounter.NextValue(), 2);
        }

        public static double GetRamUsage()
        {
            var ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            return Math.Round(ramCounter.NextValue(), 2);
        }

        public static string GetSystemInfo()
        {
            return $"OS: {Environment.OSVersion}\n" +
                   $"Processor Count: {Environment.ProcessorCount}\n" +
                   $"Available Memory: {GC.GetTotalMemory(false) / (1024 * 1024)} MB";
        }
    }
}