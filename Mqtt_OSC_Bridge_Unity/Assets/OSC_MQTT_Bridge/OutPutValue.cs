using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputValue 
{
    /*{
           "ip":"1.1.1"
           "port": 1800
           "topic":"topicName"
    }*/

    public string val;
    public string topic;
    public OutputValue(string topic, string value)
    {
        this.val = value;
        this.topic = topic;
    }
    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}
