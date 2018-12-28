import sys
import webbrowser

from client import config
from lib.infi_systray.SysTrayIcon import SysTrayIcon as BaseSysTrayIcon


def action_github(self): 
    webbrowser.open("https://github.com/Kreyu/dayz-rich-presence")

def action_none(self):
    pass

menu_options = (
    (f"DayZ Rich Presence {config.version}", None, action_none),
    ("GitHub", None, action_github),
)

class SysTrayIcon(BaseSysTrayIcon):
    def __init__(self):
        print(config.app_icon_path)
        super().__init__(config.app_icon_path, config.app_name, menu_options=menu_options, default_menu_index=1)
