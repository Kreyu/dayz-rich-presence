#!/usr/bin/env python

from misc import process_exists, get_json_data
from pypresence import Presence
from tray import TrayIcon
import threading
import random
import json
import time
import sys
import os
import io

# Configuration
version             = "v0.1.0"
hover_text          = "DayZ Rich Presence"
client_id           = "527063225661915136"
json_file           = "rich_presence.json"
process_name        = "DayZ_x64.exe"

# Paths
application_path    = os.path.dirname(__file__)
local_appdata_path  = os.getenv("LOCALAPPDATA")
tray_icon_path      = os.path.join(application_path, "icon.ico")
json_file_path      = os.path.join(local_appdata_path, "DayZ\\" + json_file)

# Misc
tooltips = [
    "Months, not years",
    "Would you look at that",
    "Who's shooting in cherno?",
    "Heyy hey, I'm friendly",
    "Beans before friends"
]

# Connect to RPC
RPC = Presence(client_id)
RPC.connect()


class RPCUpdateLoop(threading.Thread):
    def __init__(self):
        threading.Thread.__init__(self)
        self.setDaemon(True)
        self.start_epoch = int(time.time())

    def run(self):
        while True:
            if (process_exists(process_name)):
                data = get_json_data(json_file_path)

                # Default values
                if ("status" not in data or len(data["status"]) < 1):
                    data["status"] = "In main menu"

                # Truncate strings
                data["status"] = data["status"][:128]

                payload = {
                    "details": data["status"],
                    "large_image": "dz-logo",
                    "large_text": random.choice(tooltips),
                    "start": self.start_epoch,
                }

                request = RPC.update(**payload)
                print(request)
            else:
                RPC.clear()
                self.start_epoch = int(time.time())
                print("DayZ process not running")
            time.sleep(15)

# Threading
systray = TrayIcon(tray_icon_path, hover_text, version)
rpcloop = RPCUpdateLoop()

rpcloop.start()
systray.start()
