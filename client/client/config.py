import os

# Configuration
version             = "v0.1.0"
hover_text          = "DayZ Rich Presence"
client_id           = "527063225661915136"
json_file           = "rich_presence.json"
process_name        = "DayZ_x64.exe"

# Paths
application_path    = os.path.dirname(__file__)
local_appdata_path  = os.getenv("LOCALAPPDATA")
tray_icon_path      = os.path.join(application_path, "..\\icon.ico")
json_file_path      = os.path.join(local_appdata_path, "DayZ\\" + json_file)

# Misc
tooltips = [
    "Months, not years",
    "Would you look at that",
    "Who's shooting in cherno?",
    "Heyy hey, I'm friendly",
    "Beans before friends",
    "Loot spawned for you",
    "/scream",
    "Party at Balota",
    "Connection with the host has been lost",
    "Rotten kiwis everywhere",
    
]