using System;
using System.Windows.Forms;
using PowerArgs;
using Serilog;

namespace Clippy
{
    class ClippyProgram
    {
        [ArgCantBeCombinedWith("Path")]
        [ArgDescription("Extracts the file name from this path and adds it to the clipboard.")]
        public string Name { get; set; }

        [ArgCantBeCombinedWith("Name")]
        [ArgDescription("Adds this path to the clipboard.")]
        public string Path { get; set; }

        public void Main()
        {
            try
            {
                if (!Path.IsNullOrEmpty())
                {
                    Clipboard.SetText(Path);
                }
                else if (!Name.IsNullOrEmpty())
                {
                    var fileName = System.IO.Path.GetFileName(Name);
                    Clipboard.SetText(fileName);
                }
                else
                {
                    Clipboard.SetText("Clippy isn't meant to be run directly. :)");
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Clippy failed. Check logs for details.");
                Log.Logger.Error(ex, "An exception occured when running clippy.");
            }
        }
    }
}