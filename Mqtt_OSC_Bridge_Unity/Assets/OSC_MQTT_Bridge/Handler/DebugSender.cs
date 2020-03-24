using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSender : MonoBehaviour
{
    public BridgeHandler bridge;
    public string topic ="/debugUnity";
    public string message = "HELLO WORLD!";

  
    void OnGUI()
    {
        GUI.Label(new Rect(10, 250, 200, 20), "TOPIC");
        this.topic = GUI.TextField(new Rect(10, 270, 200, 20), this.topic, 60);
        if (GUI.Button(new Rect(10, 300,100, 100), "Send test"))
        {
            this.bridge.SendValueOnTopic(this.topic,this.message);
        }

    }
}
