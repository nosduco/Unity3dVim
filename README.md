# Unity3dVim (for Linux)

Simplified Vim support as an External Editor in Unity3d running on any Linux environment using bash. Using the built-in external editor functions, this plugin allows developers to set their default editor to vim, open the project directory in vim (NERDTree support), and edit and sync as you save and load.

##Functionality

- Overrides the open/double-click action on any MonoScript file to open in Vim.

- Adds an additional dialog button "Open C# Project Director in Vim" to the right-click menu

- If a Vim window is already initalized on the open of another file will open the file in the same window.

- Vim instances are open in your designated terminal (gVim support not yet added)

## How to install

**Copy plugin files to your project**

Must be done for every project (Unity does not support global plugins yet)

1. Copy the `Assets` folder and all contents to the `Assets` folder in your project.
2. In Unity Select Edit &rarr; Preferences &rarr; External Tools.
3. Browse and set your external editor to the `Assets/Plugins/Edtior/Vim/VimRunner.sh` file in your project directory. (This step can be skipped if already completed for another project)

##Configuration

**Changing the default terminal**

The plugin uses `xfce4-terminal` on default. To change this follow the steps below:

1. Navigate to the project/folder where you chose the `VimRunner.sh` file to be your external editor.
2. At the head of the file under `#[Configuration]` change the `TERM="xfce4-terminal"` line to use your terminal of choice.
3. If your terminal of choice does not use the `-e` command to execute, change the line containing `$TERM -e` to use the correct execution argument.

## Questions and Bugs
Please fill out bug reports or feature requests as needed. Thanks.