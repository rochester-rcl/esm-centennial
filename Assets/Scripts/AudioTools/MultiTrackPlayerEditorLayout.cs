using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace AudioTools
{
#if UNITY_EDITOR
    [CustomEditor(typeof(MultiTrackPlayer))]
    public class MultiTrackPlayerEditorLayout : Editor
    {
        public SerializedProperty tracks;
        public SerializedProperty trackArraySize;
        public SerializedProperty loop;
        public SerializedProperty playOnAwake;
        public SerializedProperty output;

        private int buttonWidth = 100;
        private Rect lastRect;
        private List<AudioTrack> serializedTracks;
        private Color defaultColor = new Color(0.9f, 0.9f, 0.9f);
        private Color darkColor = new Color(0.4f, 0.4f, 0.4f);
        public void OnEnable()
        {
            tracks = serializedObject.FindProperty("tracks");
            trackArraySize = tracks.FindPropertyRelative("Array.size");
            loop = serializedObject.FindProperty("loop");
            playOnAwake = serializedObject.FindProperty("playOnAwake");
            output = serializedObject.FindProperty("output");
        }

        private void UpdateSerializedAudioTrack(ref SerializedProperty track)
        {
            SerializedObject so = new SerializedObject(track.objectReferenceValue);
            SerializedProperty title = so.FindProperty("title");
            SerializedProperty clip = so.FindProperty("clip");
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Title");
                    EditorGUILayout.LabelField("Audio Clip");
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PropertyField(title, GUIContent.none);
                    EditorGUILayout.PropertyField(clip, GUIContent.none);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            GUILayout.Space(5);
            so.ApplyModifiedProperties();
        }

        private void SetDefaultAudioTrack(ref SerializedProperty track)
        {
            AudioTrack at = ScriptableObject.CreateInstance<AudioTrack>();
            track.objectReferenceValue = at;
        }

        private void DrawTracks()
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Audio Tracks", EditorStyles.largeLabel);
                if (GUILayout.Button("Add", GUILayout.Width(buttonWidth)))
                {
                    trackArraySize.intValue++;
                    SerializedProperty t = tracks.GetArrayElementAtIndex(trackArraySize.intValue - 1);
                    SetDefaultAudioTrack(ref t);
                }
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUI.backgroundColor = darkColor;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUI.backgroundColor = defaultColor;
            for (int i = 0; i < trackArraySize.intValue; i++)
            {
                // GUI.backgroundColor = defaultColor;
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                {
                    SerializedProperty track = tracks.GetArrayElementAtIndex(i);
                    UpdateSerializedAudioTrack(ref track);
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("Remove", GUILayout.Width(buttonWidth)))
                        {
                            track.objectReferenceValue = null;
                            tracks.DeleteArrayElementAtIndex(i);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
                GUILayout.Space(10);
            }
            EditorGUILayout.EndVertical();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(loop);
            EditorGUILayout.PropertyField(playOnAwake);
            EditorGUILayout.PropertyField(output);
            DrawTracks();
            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif