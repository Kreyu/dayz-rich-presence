from lib.infi_systray.SysTrayIcon import SysTrayIcon as BaseSysTrayIcon
from client import config
import webbrowser
import sys

class SysTrayIcon:
    def __init__(self):
        actions = self.get_actions()
        self.systray = BaseSysTrayIcon(config.tray_icon_path, config.hover_text, menu_options=actions, default_menu_index=1, on_quit=self.on_quit)

    def start(self):
        self.systray.start()

    def get_actions(self):
        return (
            (f"DayZ Rich Presence {config.version}", None, self.action_none),
            ("GitHub", None, self.action_github),
        )

    def action_github(self, systray): 
        webbrowser.open("https://github.com/Kreyu/dayz-rich-presence")

    def action_none(self, systray):
        pass

    def on_quit(self, systray):
        sys.exit()
        systray.shutdown()