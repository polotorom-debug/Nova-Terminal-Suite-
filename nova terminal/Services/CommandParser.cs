using System;
using System.Diagnostics;
using System.IO;

namespace NovaConsole.Services
{
    public static class CommandParser
    {
        public static string Parse(string command)
        {
            return command switch
            {
                "help" => GetHelp(),
                "clear" => "",
                "exit" => Environment.Exit(0) ?? "",
                "cls" => "",
                "whoami" => Environment.UserName,
                "date" => DateTime.Now.ToString(),
                "time" => DateTime.Now.ToShortTimeString(),
                "abrir chrome" or "open chrome" or "chrome" => OpenChrome(),
                "musica on" or "música on" => "Música activada ♪",
                "musica off" or "música off" => "Música desactivada",
                "modo hacker" => EnableHackerMode(),
                "system" => GetSystemInfo(),
                "dir" or "ls" => GetDirectoryListing(),
                "pwd" => Directory.GetCurrentDirectory(),
                var cmd when cmd.StartsWith("cd ") => ChangeDirectory(cmd.Substring(3)),
                var cmd when cmd.StartsWith("echo ") => cmd.Substring(5),
                _ => $"Comando no reconocido: '{command}'. Escribe 'help' para ver comandos disponibles."
            };
        }

        private static string GetHelp()
        {
            return @"
╔════════════════════════════════════════════════════════════╗
║              NOVA CONSOLE - COMANDOS DISPONIBLES            ║
╚════════════════════════════════════════════════════════════╝

🔹 BÁSICOS:
  help             - Muestra este menú
  clear / cls      - Limpia la consola
  exit             - Cierra Nova Console
  whoami           - Muestra usuario actual
  date / time      - Fecha y hora actual

🔹 NAVEGACIÓN:
  dir / ls         - Lista archivos del directorio
  pwd              - Directorio actual
  cd [ruta]        - Cambiar directorio

🔹 APLICACIONES:
  chrome / abrir chrome     - Abre Google Chrome
  musica on / off           - Control de música

🔹 SISTEMA:
  system           - Información del sistema
  modo hacker      - Activa tema hacker

🔹 EXTRAS:
  echo [texto]     - Imprime texto

═══════════════════════════════════════════════════════════════";
        }

        private static string OpenChrome()
        {
            try
            {
                string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
                if (File.Exists(chromePath))
                {
                    Process.Start(chromePath);
                    return "Chrome abierto exitosamente ✓";
                }
                return "Chrome no encontrado en la ruta estándar.";
            }
            catch
            {
                return "Error al abrir Chrome.";
            }
        }

        private static string EnableHackerMode()
        {
            return @"
    ██╗  ██╗ █████╗  ██████╗██╗  ██╗███████╗██████╗ ███╗   ███╗ ██████╗ ██████╗ ███████╗
    ██║  ██║██╔══██╗██╔════╝██║ ██╔╝██╔════╝██╔══██╗████╗ ████║██╔═══██╗██╔══██╗██╔════╝
    ███████║███████║██║     █████╔╝ █████╗  ██████╔╝██╔████╔██║██║   ██║██║  ██║█████╗
    ██╔══██║██╔══██║██║     ██╔═██╗ ██╔══╝  ██╔══██╗██║╚██╔╝██║██║   ██║██║  ██║██╔══╝
    ██║  ██║██║  ██║╚██████╗██║  ██╗███████╗██║  ██║██║ ╚═╝ ██║╚██████╔╝██████╔╝███████╗
    ╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝ ╚═════╝ ╚═════╝ ╚══════╝

🔓 ACCESO NIVEL HACKER ACTIVADO ⚡
═══════════════════════════════════════════════════════════════";
        }

        private static string GetSystemInfo()
        {
            return $@"
╔════════════════════════════════════════════╗
║      INFORMACIÓN DEL SISTEMA NOVA          ║
╚════════════════════════════════════════════╝

OS: {Environment.OSVersion}
Procesadores: {Environment.ProcessorCount}
Memoria Disponible: {GC.GetTotalMemory(false) / (1024 * 1024)} MB
Usuario: {Environment.UserName}
Máquina: {Environment.MachineName}
";
        }

        private static string GetDirectoryListing()
        {
            try
            {
                string currentDir = Directory.GetCurrentDirectory();
                var info = new DirectoryInfo(currentDir);
                string output = $"📁 {currentDir}\n";
                output += "─────────────────────────────────────\n";

                foreach (var dir in info.GetDirectories())
                {
                    output += $"  📂 {dir.Name}\n";
                }

                foreach (var file in info.GetFiles())
                {
                    output += $"  📄 {file.Name} ({file.Length / 1024} KB)\n";
                }

                return output;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        private static string ChangeDirectory(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.SetCurrentDirectory(path);
                    return $"Directorio cambiado a: {Directory.GetCurrentDirectory()}";
                }
                return $"Error: La ruta '{path}' no existe.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}