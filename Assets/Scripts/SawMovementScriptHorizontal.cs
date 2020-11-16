using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovementScriptHorizontal : MonoBehaviour
{
    //Amount to move left and right from the start point
    public float delta = 3.7f;
    //The speed it moves at
    public float speed = 1.0f;  
    private Vector3 startPos;

    //This sets the degrees to rotate 
    public int rotateDeg = -3;

    //Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    //Update is called once per frame
    void Update()
    {
        //Rotating the saw
        Vector3 v = startPos;
        v.x += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
        transform.Rotate(Vector3.forward * rotateDeg);
    }
}
