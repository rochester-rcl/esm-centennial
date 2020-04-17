using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageRecognition : MonoBehaviour
{

    //To implement in Unity Scene:

    //(1) For this script to work, you must add the component: `AR Tracked Image Manger` to the `AR Session Origin` Compoent 
    //(2) Public variable `Serilaized Library` needs an XR `Reference Image Library`
    //(3) Public variable `Tracked Image Prefab` needs any prefab that you wwant to appear when an image is detected

    private ARTrackedImageManager _arTrackedImageManager;

    private void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    public void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;    
    }

    public void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }


    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            Debug.Log(trackedImage.name);
        }
    }
}
