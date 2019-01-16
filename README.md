# DayZ Rich Presence

A mod for DayZ Standalone, integrating the Discord rich presence feature.

# About

Due to modding limitations, it is required to run the client alongside the mod. 

- **Mod** is used to store the present data into the json file.  
- **Client** is used to send the presence data to Discord.

# Download

To download the current version, navigate to the [releases page.](https://github.com/Kreyu/dayz-rich-presence/releases)  
All releases consists of two .zip archives- one for the client, one for the packed and signed modification.

# Client

**Running locally from source**

Official Discord .dll release is now packed in the repository, so simply:

- Extract the archive
- Open the solution in the _Microsoft Visual Studio_
- Compile & run the program

# Mod

**Packing the mod**

Open **DayZ Tools** and launch the **Addon builder** tool, setting it up as follows:

- Addon source directory:   
  `<Repository>\mod\RichPresence`

- Destination directory:  
  `<DayZ Path>\@RichPresence\Addons`

- Addon prefix:  
  `RichPresence`

# Todo

- Display server name / IP address
- Stop being bad at writing readme
- Make it work without a client
