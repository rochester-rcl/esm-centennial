using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioTools
{
    [System.Serializable]
    public class AudioTrack : ScriptableObject
    {
        public string title;
        public AudioClip clip;
    }
}

