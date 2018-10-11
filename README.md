# Clippy!
"Copy as path"... but better

![clippy](https://raw.githubusercontent.com/refactorsaurusrex/clippy/master/images/clippy.jpg)

# What is this?

Clippy (this 'clippy', anyway) adds two items to your Windows Explorer right-click context menu:

<img src="https://raw.githubusercontent.com/refactorsaurusrex/clippy/master/images/context-menu.png" />

This works on both files and directories.

## *That's it?*

Yep. I find myself copying file names and paths to my clipboard *all* the time, so I like having these commands front and center. Windows does have a native 'Copy as path' command, but I don't like that you have to hold Shift while right clicking to access that option and I also don't like that it encapsulates the value in quotes. Plus, sometimes I just want the file name without its full path.

# Ok, I want it. How do I get it?
Head over to the [releases page][releases] and download the most recent version. (In all likelihood there'll only be one... I seriously doubt this project will require any updates past v1.0.) Then, extract the archive to a location of your choosing. Next, run `ClippyInstaller.exe` **as administrator**. You'll see a window like this:

<img src="https://raw.githubusercontent.com/refactorsaurusrex/clippy/master/images/ClippyInstaller.png" />

Press `i` to install or `r` to remove. That's it!

> In case you're wondering why you have to run this as administrator, it's because [Clippy needs to create a couple of registry entries][installer] in order to work. 

# Got feedback?
[Open an issue][issues] and I'll get back to you.


[releases]: https://github.com/refactorsaurusrex/clippy/releases
[installer]: https://github.com/refactorsaurusrex/clippy/blob/master/src/Clippy.Installer/Program.cs#L49-L83
[issues]: https://github.com/refactorsaurusrex/clippy/issues