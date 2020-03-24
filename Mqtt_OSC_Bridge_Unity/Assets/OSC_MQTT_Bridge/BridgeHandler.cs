using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class BridgeHandler : MonoBehaviour
{
    public OSC osc;
    public TopicHandler[] topicHandlerList;
    
    string ip;
    
    // Use this for initialization
    void Start()
    {
            // Get device ip
            
            var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (var ipDev in host.AddressList)
            {
                if (ipDev.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    this.ip = ipDev.ToString();
                }
            }
        StartBrige();
    }

    void RegisterTopicHandler(TopicHandler topicHandler)
    {
        
        osc.SetAddressHandler(topicHandler.topicName, topicHandler.OnTopic);
        InformServerOnRegistration(CreateRegisterJsonString(topicHandler.topicName));
    }

    
    void InformServerOnRegistration(string json)
    {
        OscMessage message;

        message = new OscMessage();
        message.address = "/register";
        message.values.Add(json);
        osc.Send(message);
    }

    public void SendValueOnTopic(string topic,string val)
    {
        OutputValue outVal = new OutputValue(topic, val);
        OscMessage message;

        message = new OscMessage();
        message.address = "/incoming";
        message.values.Add(outVal.SaveToString());
        osc.Send(message);
    }
    void OnDisconnect(OscMessage message)
    {
        foreach (TopicHandler thandler in topicHandlerList)
        {
            thandler.OnDisconnect();
        }
    }
    void OnConnect(OscMessage message)
    {
        foreach (TopicHandler thandler in topicHandlerList)
        {
            thandler.OnConnect();
        }
    }

    string CreateRegisterJsonString(string topicName)
    {
        /*{
            "ip":"1.1.1"
            "port": 1800
            "topic":"topicName"
        }*/

        Topic currentT = new Topic(ip, osc.inPort, topicName);
        string registerJson = currentT.SaveToString();
        return registerJson;

    }
    void StartBrige()
    {
        osc.ClearAdressHandler();
        osc.SetAddressHandler("/connect", this.OnConnect);
        InformServerOnRegistration(CreateRegisterJsonString("/connect"));

        osc.SetAddressHandler("/disconnect", this.OnDisconnect);
        InformServerOnRegistration(CreateRegisterJsonString("/disconnect"));

        foreach (TopicHandler topicHandler in topicHandlerList)
        {
            RegisterTopicHandler(topicHandler);
        }
    }
    
    void OnGUI()
    {
        
        //For demo purposes - delete if not needed.
        ip = GUI.TextField(new Rect(10, 10, 200, 20), ip, 25);
        if (GUI.Button(new Rect(10, 50, 50, 30), "Start with ip"))
        {
            StartBrige();
        }
    }
}
