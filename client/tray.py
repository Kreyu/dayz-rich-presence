from infi.systray import SysTrayIcon
import webbrowser
import sys

class TrayIcon:
    def __init__(self, icon, hover_text, version):
        self.version = version
        actions = self.get_actions()
        self.systray = SysTrayIcon(icon, hover_text, menu_options=actions, default_menu_index=1, on_quit=self.on_quit)

    def start(self):
        self.systray.start()

    def get_actions(self):
        return (
            (f"DayZ Rich Presence {self.version}", None, self.action_none),
            ("GitHub", None, self.action_github),
        )

    def action_github(self, systray): 
        webbrowser.open("https://github.com/Kreyu/dayz-rich-presence")

    def action_none(self, systray):
        pass

    def on_quit(self, systray):
        sys.exit()
        systray.shutdown()