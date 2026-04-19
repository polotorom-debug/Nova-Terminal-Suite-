using System;
using System.IO;
using System.Text.Json;
using NovaConsole.Models;

namespace NovaConsole.Services
{
    public static class ConfigService
    {
        private static readonly string ConfigPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "NovaConsole",
            "config.json"
        );

        public static ConfigModel Config { get; private set; }

        public static void LoadConfig()
        {
            try
            {
                string directory = Path.GetDirectoryName(ConfigPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                if (File.Exists(ConfigPath))
                {
                    string json = File.ReadAllText(ConfigPath);
                    Config = JsonSerializer.Deserialize<ConfigModel>(json) ?? CreateDefaultConfig();
                }
                else
                {
                    Config = CreateDefaultConfig();
                    SaveConfig();
                }
            }
            catch
            {
                Config = CreateDefaultConfig();
            }
        }

        public static void SaveConfig()
        {
            try
            {
                string directory = Path.GetDirectoryName(ConfigPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(Config, options);
                File.WriteAllText(ConfigPath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando config: {ex.Message}");
            }
        }

        private static ConfigModel CreateDefaultConfig()
        {
            return new ConfigModel
            {
                Theme = new ThemeSettings
                {
                    Name = "Dark",
                    BackgroundColor = "#1E1E1E",
                    ForegroundColor = "#00FF00",
                    AccentColor = "#0078D4",
                    Opacity = 0.95,
                    Font = "Cascadia Code",
                    FontSize = 12
                },
                Console = new ConsoleSettings
                {
                    MaxHistoryLines = 1000,
                    EnableAutoComplete = true,
                    EnableBlur = true,
                    ShowSystemPanel = true,
                    TabCount = 1
                },
                CustomAliases = new(),
                PluginsEnabled = new()
            };
        }
    }
}