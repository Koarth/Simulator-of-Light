import urllib.request
import json
import hashlib
import time

ITEM_LIST_URL = "https://api.xivdb.com/item"
ITEM_LIST_ARGS = "?columns=id,level_equip"

# We'll use this to check for updates to DB data.
PATCH_LIST = "https://api.xivdb.com/data/patchlist"
PATCH_HASH_FILE = "patch_hash"

OUTFILE = "./../../Simulator of Light/db/gear_items.json"

MAX_LEVEL = 70
ATTRIBUTES = {
    'attributes_base',
    'attributes_params',
    'can_be_hq',
    'classjobs',
    'color',
    'id',
    'level_equip',
    'level_item',
    'materia_slot_count',
    'name',
    'rarity',
    'slot_equip',
    'slot_name'
}


def api_request_backoff(request, max_retries=5):

    delay = 0
    retries = 0

    while retries < max_retries:

        time.sleep(delay)

        try:
            return urllib.request.urlopen(request).read()
        except urllib.request.HTTPError:
            delay += 2
            retries += 1

    raise(TimeoutError("Ran out of retries for an item request."))


if __name__ == "__main__":

    req = urllib.request.Request(
        PATCH_LIST,
        data=None,
        headers={
            'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.47 Safari/537.36'
        }
    )

    response = urllib.request.urlopen(req).read()
    response_hash = hashlib.md5(response).hexdigest()

    try:
        patchlist_hash = open(PATCH_HASH_FILE, 'r').read()
    except FileNotFoundError:
        patchlist_hash = ""

    if patchlist_hash == response_hash:
        print("No new patches published to XIVDB.")
        exit(0)

    # Get list of all item ids with their level requirements.
    url = ITEM_LIST_URL + ITEM_LIST_ARGS
    req = urllib.request.Request(
        url,
        data=None,
        headers={
            'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.47 Safari/537.36'
        }
    )
    response = urllib.request.urlopen(req).read()
    itemlist = json.loads(response.decode("utf-8"))

    # Request all max-level items, and save all desired attributes to a JSON db.
    data = dict()
    for item in itemlist:
        if item['level_equip'] == MAX_LEVEL:
            id = item['id']
            url = ITEM_LIST_URL + "/" + str(id)
            req = urllib.request.Request(
                url,
                data=None,
                headers={
                    'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.47 Safari/537.36'
                }
            )

            response = api_request_backoff(req)

            raw = json.loads(response.decode("utf-8"))
            data[id] = dict()
            for attr in ATTRIBUTES:
                data[id][attr] = raw[attr]
            print("Created item with item ID " + str(id))

    # Write JSON db.
    fp = open(OUTFILE, 'w+')
    fp.write(json.dumps(data, indent=2))
    fp.close()

    # Since we were successful, update the patch hash to indicate that we're up-to-date.
    fp = open(PATCH_HASH_FILE, 'w+')
    fp.write(response_hash)
    fp.close()

