using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelatorMovementScript : MonoBehaviour
{
    //Amount to move left and right from the start point
    public float delta = 5.3f;
    //The speed it moves at
    public float speed = 1.0f;  
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame (horizontal movement)
    void Update()
    {
        //Making the Travelator move left and right
        Vector3 v = startPos;
        v.x += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}
