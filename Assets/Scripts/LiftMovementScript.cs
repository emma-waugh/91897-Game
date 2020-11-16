using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftMovementScript : MonoBehaviour
{
    //Amount to move up and down from the start point
    public float delta = 3.0f;
    //The speed it moves at
    public float speed = 1.0f;  
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame (Virtical Movement)
    void Update()
    {
        //Making the Lift move up and down
        Vector3 v = startPos;
        v.y += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}
