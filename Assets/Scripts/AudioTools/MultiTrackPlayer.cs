using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioTools
{
    public class MultiTrackPlayer : MonoBehaviour
    {
        public AudioTrack[] tracks;
        public bool loop;
        public bool playOnAwake;
        public AudioMixerGroup output;
        private AudioMixer mixer;
        // Start is called before the first frame update
        void Start()
        {

        }

        void Awake()
        {
            AttachAudioSources();
        }

        private void AttachAudioSources()
        {
            foreach (AudioTrack track in tracks)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.clip = track.clip;
                source.loop = loop;
                source.outputAudioMixerGroup = output;
                if (playOnAwake) source.Play();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

