The OSC Mqtt Bridge handles communication between OSC and Mqtt.

Especially for Unity3d.
The Unityproject uses Osc which seems quite stable with Unity. 
 
The Unityproject registers itself to the bridge and is able to send data to the bridge or recieves data from the bridge.
The bridge passes the data to the Mqtt broker. The bridge should be running in parallel either on the same device or a device in the network. ( Be aware of firewall configurations I usually use it in a local network which is disconnected from the internet)
 
When everything is setup, you can add a lot of IOT devices.

By the way:
The Unity OSC implementation is written by Thomas O Fredericks. 
I included it in the project and made some little changes. 
It is a very lightweight good to read piece of code and seems very stable. 
 
The bridge is written in python 3.
You will also find a Unity Testproject in /Mqtt_OSC_Bridge_Unity
 
I tested it with the Occolus GO in a local network. 


If you want to use the bridge with common OSC devices:

You can register a topiclistener by sending a json to "/register".
The format of the json is documented in register.md

You can send a messag to topic from an Osc Device by sending a json to "/incoming".
The format of the json is documented in incoming.md

 
Arguments:
 
--ip            
Set the ip of the OSCServer your Unity Project needs to connect to.  
--port          
Set the port of the OSCServer Your Unity Project needs to connect to.
--brokerAdress  
Set the Ip of the Mqtt broker 
 
python main.py --ip "127.0.0.1" --port 5001 --brokerAdress "127.0.0.2"
 
 
Needed packages:
 
Paho Mqtt 
pip install paho-mqtt
 
Python Osc
pip install python-osc


