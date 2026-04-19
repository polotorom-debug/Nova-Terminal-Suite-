using System;
using System.Diagnostics;
using NovaConsole.Models;

namespace NovaConsole.Services
{
    public static class SystemService
    {
        public static void MonitorSystem(Action<double, double> onUpdate)
        {
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += (s, e) =>
            {
                double cpu = SystemInfo.GetCpuUsage();
                double ram = SystemInfo.GetRamUsage();
                onUpdate?.Invoke(cpu, ram);
            };
            timer.AutoReset = true;
            timer.Start();
        }
    }
}