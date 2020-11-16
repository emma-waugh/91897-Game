using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovementScriptVirtical : MonoBehaviour
{
    //Amount to move up and down from the start point
    public float delta = 1.1f;
    //The speed it moves at
    public float speed = 1.0f;  
    private Vector3 startPos;

    //This sets the degrees to rotate 
    public int rotateDeg = -3;

    //Start is called before the first frame update
    void Start()
    {
        //This sets the speed to be random but still between 1 and 3
        speed = Random.Range(1.0f, 3.0f);
        startPos = transform.position;
    }

    //Update is called once per frame
    void Update()
    {
        //Rotating the saw
        Vector3 v = startPos;
        v.y += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
        transform.Rotate(Vector3.forward * rotateDeg);
    }
}
