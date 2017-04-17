using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentConsole.Library;
using Microsoft.Win32;
using Serilog;
using static System.ConsoleColor;

namespace Clippy.Installer
{
    class Program
    {
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.RollingFile("Logs\\log-{Date}.txt")
                .CreateLogger();

            "Hello! Welcome to Clippy.".WriteLine(Cyan, 2);
            var answer = "Would you like to install (i) or remove (r) Clippy?".WriteLineWait(Cyan, 1);

            while (true)
            {
                switch (answer.Key)
                {
                    case ConsoleKey.I:
                        RunInstaller();
                        return;

                    case ConsoleKey.R:
                        RunUninstaller();
                        return;

                    case ConsoleKey.Escape:
                        return;
                    
                    default:
                        answer = $"{answer.Key} isn't an option. Press 'i' to install, 'r' to remove, or 'Esc' exit.".WriteLineWait(Red);
                        break;
                }
            }
        }

        static void RunInstaller()
        {
            try
            {
                var installerLocation = Assembly.GetExecutingAssembly().Location;
                var clippyPath = Path.Combine(Path.GetDirectoryName(installerLocation), "clippy.exe");

                using (var registryKey = Registry.ClassesRoot.CreateSubKey(@"*\Shell\Copy File Path\Command"))
                {
                    registryKey?.SetValue(null, $"{clippyPath} -p \"%1\"");
                }

                using (var registryKey = Registry.ClassesRoot.CreateSubKey(@"Directory\Shell\Copy Folder Path\Command"))
                {
                    registryKey?.SetValue(null, $"{clippyPath} -p \"%1\"");
                }

                using (var registryKey = Registry.ClassesRoot.CreateSubKey(@"*\Shell\Copy File Name\Command"))
                {
                    registryKey?.SetValue(null, $"{clippyPath} -n \"%1\"");
                }

                using (var registryKey = Registry.ClassesRoot.CreateSubKey(@"Directory\Shell\Copy Folder Name\Command"))
                {
                    registryKey?.SetValue(null, $"{clippyPath} -n \"%1\"");
                }

                "Installed! Press any key to exit...".WriteLineWait(Cyan);
            }
            catch (Exception ex)
            {
                "Installation failed. See logs for more details. Did you run the program as administrator?".WriteLine(Red, 1);
                ExceptionHandler(ex, nameof(RunInstaller));
            }
        }

        static void RunUninstaller()
        {
            try
            {
                Registry.ClassesRoot.DeleteSubKeyTree(@"*\Shell\Copy File Path");
                Registry.ClassesRoot.DeleteSubKeyTree(@"Directory\Shell\Copy Folder Path");
                Registry.ClassesRoot.DeleteSubKeyTree(@"*\Shell\Copy File Name");
                Registry.ClassesRoot.DeleteSubKeyTree(@"Directory\Shell\Copy Folder Name");

                "Clippy hath been removed! Press any key to exit...".WriteLineWait(Cyan);
            }
            catch (Exception ex)
            {
                "Removal failed. See logs for more details. Did you run the program as administrator?".WriteLine(Red, 1);
                ExceptionHandler(ex, nameof(RunUninstaller));
            }
        }

        static void ExceptionHandler(Exception ex, string sourceName)
        {
            Log.Error(ex, $"Exception during {sourceName}.");
            "Press any key to exit...".WriteLineWait(Cyan);
        }
    }
}
