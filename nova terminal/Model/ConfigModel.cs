using System;
using System.Collections.Generic;

namespace NovaConsole.Models
{
    public class ConfigModel
    {
        public ThemeSettings Theme { get; set; }
        public ConsoleSettings Console { get; set; }
        public List<string> CustomAliases { get; set; }
        public List<string> PluginsEnabled { get; set; }
    }

    public class ThemeSettings
    {
        public string Name { get; set; } = "Dark";
        public string BackgroundColor { get; set; } = "#1E1E1E";
        public string ForegroundColor { get; set; } = "#00FF00";
        public string AccentColor { get; set; } = "#0078D4";
        public double Opacity { get; set; } = 0.95;
        public string Font { get; set; } = "Cascadia Code";
        public int FontSize { get; set; } = 12;
    }

    public class ConsoleSettings
    {
        public int MaxHistoryLines { get; set; } = 1000;
        public bool EnableAutoComplete { get; set; } = true;
        public bool EnableBlur { get; set; } = true;
        public bool ShowSystemPanel { get; set; } = true;
        public int TabCount { get; set; } = 1;
    }
}