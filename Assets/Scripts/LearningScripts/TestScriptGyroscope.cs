﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeTest : MonoBehaviour
{

    // Faces for 6 sides of the cube
    private GameObject[] quads = new GameObject[6];

    // Textures for each quad, should be +X, +Y etc
    // with appropriate colors, red, green, blue, etc
    public Texture[] labels;


    // Start is called before the first frame update
    void Start()
    {
        // Make camera solid colour and based at the origin 
        GetComponent<Camera>().backgroundColor = new Color(49.0f / 255.0f, 77.0f / 255.0f, 121.0f / 255.0f);
        GetComponent<Camera>().transform.position = new Vector3(0, 0, 0);
        GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;

        // Create the six quads forming the sides of a cube
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);

        quads[0] = CreateQuad(quad, new Vector3(1, 0, 0), new Vector3(0, 90, 0), "plus x", 
            new Color(0.90f, 0.10f, 0.10f, 1), labels[0]);
        quads[1] = CreateQuad(quad, new Vector3(0, 1, 0), new Vector3(-90, 0, 0), "plus y",
            new Color(0.10f, 0.90f, 0.10f, 1), labels[1]);
        quads[2] = CreateQuad(quad, new Vector3(0, 0, 1), new Vector3(0, 0, 0), "plus z",
            new Color(0.10f, 0.10f, 0.90f, 1), labels[2]);
        quads[3] = CreateQuad(quad, new Vector3(-1, 0, 0), new Vector3(0, -90, 0), "neg x",
            new Color(0.90f, 0.50f, 0.50f, 1), labels[3]);
        quads[4] = CreateQuad(quad, new Vector3(0, -1, 0), new Vector3(90, 0, 0), "neg y",
            new Color(0.50f, 0.90f, 0.50f, 1), labels[4]);
        quads[5] = CreateQuad(quad, new Vector3(0, 0, -1), new Vector3(0, 180, 0), "neg z",
            new Color(0.50f, 0.50f, 0.90f, 1), labels[5]);

        GameObject.Destroy(quad);
    }

    // make a quad for one side of the cube
    GameObject CreateQuad(GameObject quad, Vector3 pos, Vector3 rot, string name, Color col, Texture t)
    {
        Quaternion quat = Quaternion.Euler(rot);
        GameObject GO = Instantiate(quad, pos, quat);
        GO.name = name;
        GO.GetComponent<Renderer>().material.color = col;
        GO.GetComponent<Renderer>().material.mainTexture = t;
        GO.transform.localScale += new Vector3(0.25f, 0.25f, 0.25f);
        return GO;
    }

    // Update is called once per frame
    void Update()
    {
        GyroModifyCamera();
    }

    protected void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("Orientation: " + Screen.orientation);
        GUILayout.Label("input.gyro.attitude: " + Input.gyro.attitude);
        GUILayout.Label("iphone width/font: " + Screen.width + " : " + GUI.skin.label.fontSize);
    }

    /********************************************/

    // The Gyroscope is right-handed.  Unity is left handed.
    // Make the necessary change to the camera.
    void GyroModifyCamera()
    {
        transform.rotation = GyroToUnity(Input.gyro.attitude);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

}
