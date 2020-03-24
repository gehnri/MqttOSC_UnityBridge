using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHandler : TopicHandler
{

    public Light l;
    public float maxMap;
    
    public override void OnConnect()
    {
        Debug.Log("Connected");
    }

    public override void OnDisconnect()
    {
        Debug.Log("Disconnected");
    }

    public override void OnTopic(OscMessage message)
    {
        //Set your own code
        float intens = float.Parse(message.values[0].ToString());
        this.l.intensity = map(intens,0,100,0,maxMap);
    }

    private float map(float value, float fromLow, float fromHigh, float toLow, float toHigh)
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }
}
