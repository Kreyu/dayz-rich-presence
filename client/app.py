#!/usr/bin/env python

from pypresence import Presence
from client.misc import process_exists, get_json_data
from pypresence.exceptions import InvalidPipe
from client.gui import SysTrayIcon
from client import config
import threading
import random
import time
import sys

# Connect to RPC
RPC = Presence(config.client_id)

try:
    RPC.connect()
except InvalidPipe:
    print("Discord not running.")
    sys.exit()
    pass


class RPCUpdateLoop(threading.Thread):
    def __init__(self):
        threading.Thread.__init__(self)
        self.setDaemon(True)
        self.start_epoch = int(time.time())

    def run(self):
        while True:
            if (process_exists(config.process_name)):
                data = get_json_data(config.json_file_path)

                # Default values
                if ("status" not in data or len(data["status"]) < 1):
                    data["status"] = "In main menu"

                # Truncate strings
                data["status"] = data["status"][:128]

                payload = {
                    "details": data["status"],
                    "large_image": "dz-logo",
                    "large_text": random.choice(config.tooltips),
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
systray = SysTrayIcon()
rpcloop = RPCUpdateLoop()

rpcloop.start()
systray.start()
