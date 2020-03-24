using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMqtt : MonoBehaviour
{
    public Camera camera;
    public BridgeHandler bridge;
    public string clickedTopic = "/clicked";
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnMouseDown()
    {
        // this object was clicked - do something
        Debug.Log("Clicked");
        bridge.SendValueOnTopic(clickedTopic, "true");
        animator.Play("Jump");
    }
    // Update is called once per frame
    void Update()
    {
        
       

    }
}
