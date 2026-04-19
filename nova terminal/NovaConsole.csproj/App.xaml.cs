using System.Windows;
using NovaConsole.Services;

namespace NovaConsole
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigService.LoadConfig();
        }
    }
}