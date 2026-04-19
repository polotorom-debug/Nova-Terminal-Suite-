using System.Windows;
using System.Windows.Input;
using NovaConsole.ViewModels;
using NovaConsole.Services;

namespace NovaConsole.Views
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                _viewModel.ExecuteCommand.Execute(null);
                e.Handled = true;
            }
            else if (e.Key == Key.Up)
            {
                var previous = _viewModel.History.GetPrevious();
                if (previous != null)
                {
                    _viewModel.CurrentInput = previous;
                    InputBox.CaretIndex = InputBox.Text.Length;
                }
                e.Handled = true;
            }
            else if (e.Key == Key.Down)
            {
                var next = _viewModel.History.GetNext();
                if (next != null)
                {
                    _viewModel.CurrentInput = next;
                }
                e.Handled = true;
            }
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}