using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebugHandler : TopicHandler
{
    /*
     *An Monobehaviour for demo.
     * 
     *Dont forgett to implement TopicHandler
     *
     */

    string msg="";
    public Text text;

    public override void OnConnect()
    {
        //Set your own code
        msg = "Connected";
    }

    public override void OnDisconnect()
    {
        //Set your own code
        msg = "Disconnected";
    }

    public override void OnTopic(OscMessage message)
    {
        //Set your own code
        Debug.Log(message.values[0]);
        this.msg = message.values[0].ToString();
        this.text.text= message.values[0].ToString();
    }
    void OnGUI()
    {
        // Make a text field that modifies stringToEdit.
        GUI.Label(new Rect(10, 120, 200, 20), "DEBUGMSG");
        this.msg = GUI.TextField(new Rect(10, 150, 200, 20), this.msg, 30);
  
    }
}
