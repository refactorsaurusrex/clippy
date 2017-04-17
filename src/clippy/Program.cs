using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PowerArgs;
using Serilog;

namespace Clippy
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.RollingFile("Logs\\log-{Date}.txt")
                .CreateLogger();

            Args.InvokeMain<ClippyProgram>(args);
        }
    }
}
