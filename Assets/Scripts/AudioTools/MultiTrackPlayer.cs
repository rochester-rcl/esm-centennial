using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
namespace AudioTools
{
    public class MultiTrackPlayer : MonoBehaviour
    {
        public AudioTrack[] tracks;
        public bool loop;
        public bool playOnAwake;
        public AudioMixerGroup output;
        public AudioSource SelectedSource
        {
            get
            {
                return selectedSource;
            }
        }

        private AudioSource selectedSource;
        private Dictionary<string, AudioSource> sources;

        private AudioMixer mixer;
        private OutputMapping mapper;

        // Start is called before the first frame update
        void Start()
        {
            mapper = gameObject.GetComponent<OutputMapping>();
        }

        public void Awake()
        {
            AttachAudioSources();
        }

        public void SelectSource(string trackName)
        {
            if (string.IsNullOrEmpty(trackName))
            {
                selectedSource = null;
            }
            else
            {
                AudioSource src = sources[trackName];
                if (src != null)
                {
                    selectedSource = src;
                }
                else
                {
                    Debug.LogWarning(string.Format("Audio Source with Title {0} not found!", trackName));
                }
            }
        }

        public void BypassMixerGroup()
        {
            if (selectedSource)
            {
                selectedSource.outputAudioMixerGroup = null;
            }
        }

        public void Mute()
        {
            if (selectedSource)
            {
                selectedSource.mute = true;
            }
        }

        public void Mute(string trackName)
        {
            SelectSource(trackName);
            if (selectedSource)
            {
                Mute();
            }
        }

        public void BypassMixerGroup(string trackName)
        {
            SelectSource(trackName);
            if (selectedSource)
            {
                BypassMixerGroup();
            }
        }

        private void AttachAudioSources()
        {
            sources = new Dictionary<string, AudioSource>();
            foreach (AudioTrack track in tracks)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.clip = track.clip;
                source.loop = loop;
                source.outputAudioMixerGroup = output;
                if (playOnAwake) source.Play();
                sources[track.title] = source;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

