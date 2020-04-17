using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If attached to an object, object will move in accordance with the 
        //tilt of the mobile device

        transform.Translate(Input.acceleration.x, 0, -Input.acceleration.z);
        //Debug.Log("x acceleration: " + Input.acceleration.x);
        //Debug.Log("y acceleration: " + Input.acceleration.y);
        //Debug.Log("z acceleration: " + Input.acceleration.z);

        
    }
}
