
using UnityEngine;

public class Topic
{   /*{
            "ip":"1.1.1",
            "port": 1800,
            "topic":"topicName"
     }*/
    
    public string ip;
    public int port;
    public string topic;
    public Topic(string ip, int port, string topic)
    {
        this.ip = ip;
        this.port = port;
        this.topic = topic;
    }
    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}
