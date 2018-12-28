import os
import sys

# Configuration

version             = "v0.1.0"
app_name            = "DayZ Rich Presence"
client_id           = "527063225661915136"
json_file           = "rich_presence.json"
log_file_path       = "rich_presence_client.log"
game_process        = "DayZ_x64.exe"
discord_process     = "Discord.exe"
client_process      = "DZRichPresence.exe"
tick_delay          = 15

# Paths

if hasattr(sys, '_MEIPASS'):
    application_path = os.path.join(sys._MEIPASS)
else:
    application_path = os.path.dirname(__file__)

local_appdata_path  = os.getenv("LOCALAPPDATA")
game_appdata_path   = os.path.join(local_appdata_path, "DayZ")
app_icon_path       = os.path.join(application_path, "icon.ico")
json_file_path      = os.path.join(game_appdata_path,  json_file)
log_file_path       = os.path.join(game_appdata_path, log_file_path)

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
    "Yikes",
    "Hitreg supported",
    "Oh boy, that wasn't a desync",
    "*licks the battery*",
    "I have a funny taste in my mouth",
    "My stomach grumbled violently",
    "Battle Royale included",
    "That's a lot of damage",
]
