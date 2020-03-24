from pythonosc import udp_client

class OscEmitter():

    def __init__(self,topicName,registeredOSCClient:{}):
        self.topicName=topicName
        self.registeredOSCClient=registeredOSCClient

    def GetTopic(self):
        return self.topicName
        
    def OnMessage(self,topicName,msg):
        if(topicName == self.topicName):
            print("Try sending to  " + self.registeredOSCClient["ip"] +" port:" +str(self.registeredOSCClient["port"]) )
            client = udp_client.SimpleUDPClient(self.registeredOSCClient["ip"], self.registeredOSCClient["port"])
            client.send_message(topicName,msg )
            
    def HasSameVals(self,topicName,checkClient):
        if(self.registeredOSCClient["ip"] == checkClient["ip"] and self.topicName==topicName):
            return True
    