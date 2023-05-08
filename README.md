# Shell Frosting
Shell Frosting is a fun, unconventional "shell extension" designed to enable **unfinished** shell features hidden in Windows Insider builds.

As of right now it enables the following features:
- Taskbar item ungrouping & labeling
  - all builds that contain this feature should be supported (*rs_prerelease* 25246+, *ni_prerelease* 23403+)
  - *zn_release* builds **do NOT** support this feature

[![Release downloads](https://img.shields.io/github/downloads/thebookisclosed/ShellFrosting/total.svg)](https://GitHub.com/thebookisclosed/ShellFrosting/releases/)

# Components
The code fixing various broken or incomplete functions of the OS is located in the `frosting` library project.

`FrostingCfg` is a simple UI which lets you install/uninstall the extension, as well as configure the newly enabled features that aren't yet covered by Windows' Settings app.

![FrostingCfg screenshot](https://user-images.githubusercontent.com/13197516/236929895-a2743987-11b4-4feb-8579-27484574eeca.png)

# Known issues
- Taskbar items sometimes lack icons or have outdated icons and titles if grouping is off
  - The root cause resides within Windows-provided code and is not a bug caused by the extension.
  - An easy workaround is to turn grouping on and back off again.

# Todos
- [ ] Arm64 support
- [ ] Fix icon and title desync if possible

# Support
If you like this kind of project and would like to see more, any support is greatly appreciated.

You can support the project using GitHub Sponsors in the sidebar or [here](https://www.paypal.me/tfwboredom).
