# Dayz Rich Presence

A mod for DayZ Standalone, integrating the Discord rich presence feature.

# About

Due to modding limitations, it is required to run the client alongside the mod. 

- **Mod** is used to store the present data into the json file.  
- **Client** is used to send the presence data to Discord.

# Download

To download the current version, navigate to the [releases page.](https://github.com/Kreyu/dayz-rich-presence/releases)  
All releases consists of two .zip archives- one for the client, one for the packed and signed modification.

# Client

> The release v0.1.0 contains deprecated client written entirely in Python, therefore this section only concerns the later releases.

**Building from source**

- Todo

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