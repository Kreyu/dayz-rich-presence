import json
import os
import io

def process_exists(name):
    return name in os.popen("tasklist").read()

def get_json_data(path):
    if not os.path.isfile(path) or not os.access(path, os.R_OK):
        with io.open(path, 'w') as json_file:
            json_file.write(json.dumps({}))

    with open(path) as data_file:
        return json.load(data_file)
