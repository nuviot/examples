import nuvpy.nuviot_device as nuviot_device
import nuvpy.nuviot_auth as nuviot_auth
from nuvpy.nuviot_auth import AuthContext
import os

# Ensure you have ran pip install -r requirements.txt from this directory
# to download the NuvIoT libraries and dependencies.
#
# Please enusre you have added the following environment variables to your system
# you may need to restart your python shell for those enviornment variables to 
# take effect.
#
# Optionally you can just add your client id and auth keys in the code below.
client_id =  os.environ["NUVIOT_CLIENT_APP_ID"]
auth_token = os.environ["NUVIOT_CLIENT_APP_AUTH"]
ctx = AuthContext.for_client_app(client_id, auth_token)
nuviot_device.print_devices(ctx)