import os
from subprocess import call

from client import config

basedir = os.path.dirname(__file__)

if (basedir):
    os.chdir(basedir)

command = "pyinstaller app.py"

options = [
    "windowed",
    "onefile",
    "add-data .\\client\\icon.ico;.",
    "name DZRichPresence",
    "icon=./client/icon.ico"
]

for option in options:
    command += " --" + option

call(command, shell=True)
