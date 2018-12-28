#!/usr/bin/env python

import sys

from client import config
from client.gui import SysTrayIcon
from client.misc import process_exists
from client.rpc import RPCUpdateLoop

sys.stderr = open(config.log_file_path, "a")

# Check if Discord is running.

if not (process_exists(config.discord_process)):
    print("Discord not running.")
    sys.exit()

# Start the application.
systray = SysTrayIcon()
rpcloop = RPCUpdateLoop()

rpcloop.start()
systray.start()
