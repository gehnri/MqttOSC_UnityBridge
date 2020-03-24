from pythonosc import dispatcher as dp
from pythonosc import osc_server
from MqttCom.MqttServComManager import MqttServComManager
import json
import asyncio

class OscServer():
    def __init__(self,ip,port):
        self.ip=ip
        self.port=port
        self.server=None
        self.isRunning=False
        self.dispatcher=None

    def InitServer(self):
        if(self.isRunning==False):
            self.dispatcher = dp.Dispatcher()
            self.dispatcher.map("/register", self.OnRegisterEmitter)
            self.dispatcher.map("/incoming",self.OnIncoming)
            self.server = osc_server.ThreadingOSCUDPServer(
            (self.ip, self.port), self.dispatcher)
            print("Serving on {}".format(self.server.server_address))
            self.isRunning=True
            self.server.serve_forever()
    
    def StopServer(self):
        if(self.isRunning):
            self.isRunning=False
            self.server.shutdown()
    
    def start(self,mqttServer:MqttServComManager):
        self.mqttServer= mqttServer
        self.InitServer()

    def OnRegisterEmitter(self,*msg):
        """{
            "ip":"1.1.1"
            "port": 1800
            "topic":"topicName"
        }"""
        emitterData=json.loads(msg[1])
        if "ip" in emitterData and "port" in emitterData and "topic" in emitterData:      
            self.mqttServer.RegisterOSCEmitter(emitterData["topic"],emitterData)
    
   
    def OnIncoming(self,*msg):
        """
        {
            "topic":"topicName"
            "val":"val
        }
        """
        emitterData=json.loads(msg[1])
        if "topic" in emitterData and "val" in emitterData:
            self.mqttServer.SendMessage(emitterData["topic"],emitterData["val"])
