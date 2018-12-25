#!/usr/bin/env python

from pypresence import Presence
from infi.systray import SysTrayIcon
import webbrowser
import threading
import json
import time
import os
import io

# Configuration
hover_text  = "DayZ Rich Presence"
client_id   = '527063225661915136'
json_path   = os.getenv("LOCALAPPDATA") + "\DayZ\\rich_presence.json"

# Actions
def open_github(sysTrayIcon): 
    webbrowser.open('https://github.com/Kreyu/dayz-rich-presence')
def do_nothing(sysTrayIcon):
    pass

# Tray options
menu_options = (
    ("DayZ Rich Presence", None, do_nothing),
    ("GitHub", None, open_github),
)

# Connect to RPC
RPC = Presence(client_id)
RPC.connect()

class RPCUpdateLoop(threading.Thread):
    def __init__(self): 
        threading.Thread.__init__(self)
        self.setDaemon(True)

    # Check if json file exists, create if not.
    def check_json(self, path):
        if not os.path.isfile(path) or os.access(path, os.R_NOT_OK):
            with io.open(path, 'w') as json_file:
                json_file.write(json.dumps({}))

    # Run the thread loop
    def run(self):
        while True:
            # If game is launched (process exists)
            if ("DayZ_x64.exe" in os.popen("tasklist").read()):
                self.check_json(json_path)

                with open(json_path) as data_file:
                    data = json.load(data_file)

                # Default values
                if ("status" not in data or len(data["status"]) < 1):
                    data["status"] = "In main menu"
                
                # Truncate strings
                data["status"] = data["status"][:128]
                
                request = RPC.update(details=data["status"], large_image="dz-logo")
                print(request)
            else:
                RPC.clear()
                print("DayZ process not running")
            time.sleep(15)

# Threading
systray = SysTrayIcon("icon.ico", hover_text, menu_options, default_menu_index=1)
rpcloop = RPCUpdateLoop()

rpcloop.start()
systray.start()