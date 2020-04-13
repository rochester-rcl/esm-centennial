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

        public void OnEnable()
        {
            tracks = serializedObject.FindProperty("tracks");
            trackArraySize = tracks.FindPropertyRelative("Array.size");
            loop = serializedObject.FindProperty("loop");
            playOnAwake = serializedObject.FindProperty("playOnAwake");
            output = serializedObject.FindProperty("output");
        }

        private void DrawTracks()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            {
                EditorGUILayout.LabelField("Audio Tracks", EditorStyles.largeLabel);
                if (GUILayout.Button("Add", GUILayout.Width(buttonWidth)))
                {
                    trackArraySize.intValue++;
                    tracks.GetArrayElementAtIndex(trackArraySize.intValue - 1).objectReferenceValue = null;
                }
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                {
                    for (int i = 0; i < trackArraySize.intValue; i++)
                    {
                        SerializedProperty track = tracks.GetArrayElementAtIndex(i);
                        EditorGUILayout.BeginHorizontal();
                        {
                            string label = string.Format("Track {0}", i.ToString());
                            EditorGUILayout.PropertyField(track, new GUIContent(label));
                            if (GUILayout.Button("Remove", GUILayout.Width(buttonWidth)))
                            {
                                track.objectReferenceValue = null;
                                tracks.DeleteArrayElementAtIndex(i);
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
                EditorGUILayout.EndVertical();
                EditorGUI.indentLevel--;
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