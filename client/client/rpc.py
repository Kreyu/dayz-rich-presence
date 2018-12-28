import random
import threading
import time

from pypresence import Presence
from pypresence.exceptions import InvalidPipe

from client import config
from client.misc import get_json_data, process_exists


class RPCUpdateLoop(threading.Thread):
    def __init__(self):
        threading.Thread.__init__(self)

        self.presence = self.get_presence()
        self.start_epoch = int(time.time())
        self.setDaemon(True)

    def get_presence(self):
        presence = Presence(config.client_id)

        try:
            presence.connect()
        except InvalidPipe:
            print("Cannot connect - InvalidPipe")
            sys.exit()

        return presence

    def run(self):
        while True:
            self.on_tick()
            time.sleep(config.tick_delay)

    def on_tick(self):
        if not (process_exists(config.discord_process)):
            print("Discord not running.")
            return

        if not (process_exists(config.game_process)):
            self.presence.clear()
            self.start_epoch = int(time.time())
            print("DayZ not running.")
            return

        data = self.validate(get_json_data(config.json_file_path))

        payload = {
            "details": data["status"],
            "large_image": "dz-logo",
            "large_text": random.choice(config.tooltips),
            "start": self.start_epoch,
        }

        request = self.presence.update(**payload)
        
        print(request)

    def validate(self, data):
        if ("status" not in data or len(data["status"]) < 1):
            data["status"] = "In main menu"

        data["status"] = data["status"][:128]

        return data
