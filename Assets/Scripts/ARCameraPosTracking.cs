using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCameraPosTracking : MonoBehaviour
{

    private Vector3 ARCameraPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ARCameraPos = transform.position;
        Debug.Log("X coordinate: [" + ARCameraPos.x + "] " + "Y coordinate: [" + ARCameraPos.y + "] " + "Z coordinate: [" + ARCameraPos.z + "]" );
    }
}
