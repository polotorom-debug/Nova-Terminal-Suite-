using System;
using System.Collections.ObjectModel;

namespace NovaConsole.Models
{
    public class CommandHistory
    {
        public ObservableCollection<HistoryItem> Items { get; set; } = new();
        private int _currentIndex = -1;

        public void Add(string command)
        {
            Items.Add(new HistoryItem 
            { 
                Command = command, 
                Timestamp = DateTime.Now 
            });
        }

        public string GetPrevious()
        {
            if (_currentIndex < Items.Count - 1)
            {
                _currentIndex++;
                return Items[Items.Count - 1 - _currentIndex].Command;
            }
            return null;
        }

        public string GetNext()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                return Items[Items.Count - 1 - _currentIndex].Command;
            }
            return null;
        }

        public void Reset() => _currentIndex = -1;
    }

    public class HistoryItem
    {
        public string Command { get; set; }
        public DateTime Timestamp { get; set; }
    }
}