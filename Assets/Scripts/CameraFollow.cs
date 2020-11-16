using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform Player;
    public float cameraDistance = 50.0f;

    //Keep Camera Inbounds - unable to move off the screen
    public float xRangeLeft = 1f;
    public float xRangeRight = 56f;

    void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(Player.position.x, transform.position.y, transform.position.z);
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Keep Player Inbounds - unable to leave the screen
        if (transform.position.x < -xRangeLeft)
        {
            transform.position = new Vector3(-xRangeLeft, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRangeRight)
        {
            transform.position = new Vector3(xRangeRight, transform.position.y, transform.position.z);
        }
    }
    
}
