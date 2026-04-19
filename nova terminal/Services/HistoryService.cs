using System;
using System.Collections.Generic;
using System.IO;
using NovaConsole.Models;

namespace NovaConsole.Services
{
    public static class HistoryService
    {
        private static readonly string HistoryPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "NovaConsole",
            "history.txt"
        );

        public static void SaveCommand(string command)
        {
            try
            {
                string directory = Path.GetDirectoryName(HistoryPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.AppendAllText(HistoryPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {command}\n");
            }
            catch { }
        }

        public static List<HistoryItem> LoadHistory()
        {
            var items = new List<HistoryItem>();
            try
            {
                if (File.Exists(HistoryPath))
                {
                    foreach (var line in File.ReadAllLines(HistoryPath))
                    {
                        var parts = line.Split(new[] { " | " }, StringSplitOptions.None);
                        if (parts.Length == 2 && DateTime.TryParse(parts[0], out var date))
                        {
                            items.Add(new HistoryItem { Timestamp = date, Command = parts[1] });
                        }
                    }
                }
            }
            catch { }

            return items;
        }
    }
}