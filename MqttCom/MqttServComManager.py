import paho.mqtt.client as mqtt
from datetime import datetime
from OscCom.OscEmitter import OscEmitter
import time
class MqttServComManager():

    def __init__(self,brokerAdress,oscServer,oscemitterList):
        now = datetime.now()
        current_time = now.strftime("%H:%M:%S")
        self.oscEmitterList=oscemitterList
        self.client =mqtt.Client("mqttClient"+current_time,clean_session=False)
        self.brokerAdress=brokerAdress
        self.oscServer=oscServer
        self.wasDiconnected=False
        
    def RegisterOnDisconnect(self):
        for emitter in self.oscEmitterList:
            self.client.subscribe(emitter.GetTopic())

    def RegisterOSCEmitter(self, topicName,oscClient):
        for client in self.oscEmitterList:
            if(client.HasSameVals(topicName,oscClient)):
                print("Already registered")
                return

        oscEmitter=OscEmitter(topicName,oscClient)
        print("Topic:"+oscEmitter.GetTopic())
        self.oscEmitterList.append(oscEmitter)
        self.client.subscribe(oscEmitter.GetTopic())
        print("registered")
        #Add to server 

    def OnMessage(self,topic ,msg:str):
            for e in self.oscEmitterList:
                e.OnMessage(topic,msg)

    def StartListening(self):
        try:
            self.client.on_connect=self.On_connect
            self.client.on_disconnect=self.On_disconnect
            self.client.connect(self.brokerAdress) #connect to broker
            self.client.on_subscribe=self.on_subscribe
            self.client.on_log=self.on_log
            self.client.on_message=self.On_message
            self.client.loop_start()
        except:
            print("Couldnt connect to mqtt broker")

    def On_message(self,client, userdata, message):
            self.OnMessage(message.topic,str(message.payload.decode("utf-8")))
    
    def On_connect(self,client:mqtt.Client, userdata, flags, rc):
        self.OnMessage("/connect","con")
        if(self.wasDiconnected):
            self.RegisterOnDisconnect()
            self.wasDiconnected=False
            
    def On_disconnect(self,client:mqtt.Client, userdata,rc=0):
        self.OnMessage("/disconnect","dis")
        self.wasDiconnected=True
    
    def on_subscribe(self,client, userdata, mid, granted_qos):
        #print(str(userdata))
        pass
    def on_log(self,client, userdata, level, buf):
        #print(str(buf))
        pass
        
    def SendMessage(self,topic,value):
        self.client.publish(topic,value)