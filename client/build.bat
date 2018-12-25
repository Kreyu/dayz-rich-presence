REM Cleanup
rd /s/q ./build
rd /s/q ./dist
del DZRichPresence.spec

REM Let's build it!
pyinstaller --windowed --onefile --add-data icon.ico;. --hidden-import pkg_resources --hidden-import infi.systray --name DZRichPresence --icon=./icon.ico app.py