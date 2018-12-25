# Dayz Rich Presence

> A mod for DayZ Standalone, integrating the Discord rich presence feature.

Written by: [Kreyu](https://github.com/Kreyu)

[![pypresence](https://img.shields.io/badge/using-pypresence-00bb88.svg?style=for-the-badge&logo=discord&logoWidth=20)](https://github.com/qwertyquerty/pypresence)

# About

Due to modding limitations, it is required to run the client alongside the mod. 

**Mod** is used to store the present data into `%localappdata%/DayZ/rich_presence.json` file.  
**Client** is used to send the presence data to Discord, using the [pypresence](https://github.com/qwertyquerty/pypresence) wrapper.

# Prerequisites

- [DayZ Tools](https://store.steampowered.com/app/830640/DayZ_Tools/)
- [Python 3](https://www.python.org/downloads/)
- [PyInstaller](https://www.pyinstaller.org/)

If you wish to run the client locally from the command line:
- [pywin32](https://pypi.org/project/pywin32/)

After installing the **pywin32**, navigate to your **Python PATH** directory, and finish the installation by running:
```bash
python Scripts/pywin32_postinstall.py -install
```

# Installation

Clone the repository

```bash
git clone https://github.com/Kreyu/dayz-rich-presence.git
```

Navigate to the cloned repository

```bash
cd dayz-rich-presence
```

Create the client installer

```bash
pyinstaller ./client/app.py
```

Open **DayZ Tools** and launch the **Addon builder** tool, setting it up as follows:

- Addon source directory:   
  `<Repository>\mod\RichPresence`

- Destination directory:  
  `<DayZ Path>\@RichPresence\Addons`

- Addon prefix:  
  `RichPresence`
