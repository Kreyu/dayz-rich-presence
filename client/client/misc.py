import io
import json
import os

import psutil


def process_exists(name):
    for proc in psutil.process_iter(): 
        process = psutil.Process(proc.pid)
        pname = process.name()
        if pname == name: 
            return True
    return False

def get_json_data(path):
    if not os.path.isfile(path) or not os.access(path, os.R_OK):
        with io.open(path, 'w') as json_file:
            json_file.write(json.dumps({}))

    with open(path) as data_file:
        return json.load(data_file)
