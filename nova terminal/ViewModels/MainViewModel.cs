using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using NovaConsole.Models;
using NovaConsole.Services;

namespace NovaConsole.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _currentInput = "";
        private string _consoleOutput = "";
        private double _cpuUsage;
        private double _ramUsage;
        private int _selectedTabIndex = 0;

        public ObservableCollection<string> Tabs { get; } = new();
        public CommandHistory History { get; private set; }
        public ConfigModel Config { get; private set; }

        public ICommand ExecuteCommand { get; }
        public ICommand ClearConsole { get; }
        public ICommand AddTab { get; }
        public ICommand RemoveTab { get; }

        public MainViewModel()
        {
            History = new CommandHistory();
            Config = ConfigService.Config;

            ExecuteCommand = new RelayCommand(_ => ExecuteCommandHandler());
            ClearConsole = new RelayCommand(_ => ConsoleOutput = "");
            AddTab = new RelayCommand(_ => AddNewTab());
            RemoveTab = new RelayCommand(param => RemoveCurrentTab(param));

            // Initialize tabs
            Tabs.Add("Tab 1");

            // Start system monitor
            StartSystemMonitor();
        }

        public string CurrentInput
        {
            get => _currentInput;
            set { _currentInput = value; OnPropertyChanged(); }
        }

        public string ConsoleOutput
        {
            get => _consoleOutput;
            set { _consoleOutput = value; OnPropertyChanged(); }
        }

        public double CpuUsage
        {
            get => _cpuUsage;
            set { _cpuUsage = value; OnPropertyChanged(); }
        }

        public double RamUsage
        {
            get => _ramUsage;
            set { _ramUsage = value; OnPropertyChanged(); }
        }

        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set { _selectedTabIndex = value; OnPropertyChanged(); }
        }

        private void ExecuteCommandHandler()
        {
            if (string.IsNullOrWhiteSpace(CurrentInput)) return;

            string command = CurrentInput.Trim().ToLower();
            History.Add(command);

            string output = CommandParser.Parse(command);
            ConsoleOutput += $"> {CurrentInput}\n{output}\n";

            CurrentInput = "";
        }

        private void AddNewTab()
        {
            int newTabNumber = Tabs.Count + 1;
            Tabs.Add($"Tab {newTabNumber}");
            SelectedTabIndex = Tabs.Count - 1;
        }

        private void RemoveCurrentTab(object param)
        {
            if (Tabs.Count > 1 && int.TryParse(param?.ToString(), out int index))
            {
                if (index >= 0 && index < Tabs.Count)
                {
                    Tabs.RemoveAt(index);
                }
            }
        }

        private void StartSystemMonitor()
        {
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += (s, e) =>
            {
                CpuUsage = SystemInfo.GetCpuUsage();
                RamUsage = SystemInfo.GetRamUsage();
            };
            timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}