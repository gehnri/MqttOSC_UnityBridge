using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TopicHandler : MonoBehaviour
{
    public string topicName = "";
    public abstract void OnTopic(OscMessage message);

    public abstract void OnDisconnect();

    public abstract void OnConnect();

}
