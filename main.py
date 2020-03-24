import argparse
from MqttCom.MqttServComManager import MqttServComManager
from OscCom.OscServer import OscServer
import time

def start():
    oscEmitterList=[]
    parser = argparse.ArgumentParser()
    parser.add_argument("--ip",
    default="127.0.0.1", help="Set the OSCServer Ip.")

    parser.add_argument("--port",
    type=int, default=5001, help="The OSC port to create")
    
    parser.add_argument("--brokerAdress",
    default="127.0.0.1", help="The Broker Ip.")
    args = parser.parse_args()

    oscServer=OscServer(args.ip,args.port)
    mqttManager=MqttServComManager(args.brokerAdress,oscServer,oscEmitterList)
    
    mqttManager.StartListening()
    oscServer.start(mqttManager)

start()

