using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor
{
    [CustomEditor(typeof(SpriteManager))]
    internal class SpriteManagerEditor : Editor
    {
        public static GUIContent
        addButtonContent = new GUIContent("Add", "Add"),
        editButtonContent = new GUIContent("Edit", "Edit"),
        deleteButtonContent = new GUIContent("x", "Delete");
        SpriteManagerStateWindow window;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var rootManager = serializedObject.FindProperty("rootManager");
            EditorGUILayout.PropertyField(rootManager, new GUIContent("Root Sprite Manager"));

            var billboard = serializedObject.FindProperty("billboard");
            EditorGUILayout.PropertyField(billboard, new GUIContent("Billboard"));

            var dirMode = serializedObject.FindProperty("directionMode");
            EditorGUILayout.PropertyField(dirMode, new GUIContent("Mode"));

            var reflectMode = serializedObject.FindProperty("reflectionMode");
            EditorGUILayout.PropertyField(reflectMode, new GUIContent("Reflection Mode"));

            var states = serializedObject.FindProperty("stateMapping");
            EditorGUILayout.PropertyField(states, new GUIContent("States"));

            UpdateStateDirections(states);

            if (states.isExpanded)
            {
                for (int x = 0; x < states.arraySize; x++)
                {
                    EditorGUILayout.BeginHorizontal();
                    var state = states.GetArrayElementAtIndex(x);
                    EditorGUILayout.LabelField(x.ToString(), GUILayout.Width(15));
                    EditorGUILayout.PropertyField(state.FindPropertyRelative("stateName"), GUIContent.none);

                    if (GUILayout.Button(editButtonContent))
                    {
                        window = (SpriteManagerStateWindow)EditorWindow.GetWindow(typeof(SpriteManagerStateWindow), false, "State Editor");
                        window.serializedObject = serializedObject;
                        window.states = states;
                        window.state = state;
                        window.stateIndex = x;
                        window.Show();
                    }

                    if (GUILayout.Button(deleteButtonContent))
                    {
                        states.DeleteArrayElementAtIndex(x);
                    }

                    EditorGUILayout.EndHorizontal();
                }
                
                if(GUILayout.Button(addButtonContent))
                {
                    states.arraySize++;
                }
            }

            if (window != null)
            {
                window.Repaint();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void UpdateStateDirections(SerializedProperty states)
        {
            if (states.arraySize > 0)
            {
                var state = states.GetArrayElementAtIndex(0);

                var directions = state.FindPropertyRelative("directions");
                var mode = (DirectionMode)serializedObject.FindProperty("directionMode").enumValueIndex;

                var changed = false;
                var newArraySize = 0;

                if (mode == DirectionMode.TwoWay && directions.arraySize != 2)
                {
                    newArraySize = 2;
                    changed = true;
                }
                else if (mode == DirectionMode.FourWay && directions.arraySize != 4)
                {
                    newArraySize = 4;
                    changed = true;
                }
                else if (mode == DirectionMode.EightWay && directions.arraySize != 8)
                {
                    newArraySize = 8;
                    changed = true;
                }
                else if (mode == DirectionMode.SixteenWay && directions.arraySize != 16)
                {
                    newArraySize = 16;
                    changed = true;
                }
                else if (mode == DirectionMode.ThirtyTwoWay && directions.arraySize != 32)
                {
                    newArraySize = 32;
                    changed = true;
                }

                if (changed)
                {
                    for (int i = 0; i < states.arraySize; i++)
                    {
                        state = states.GetArrayElementAtIndex(i);

                        var frameCount = state.FindPropertyRelative("frameCount").intValue;

                        directions.ClearArray();
                        directions.arraySize = newArraySize;

                        var set = DirectionModeCardinalSets.ModeSets[mode];

                        for (int x = 0; x < directions.arraySize; x++)
                        {
                            var direction = directions.GetArrayElementAtIndex(x);
                            direction.FindPropertyRelative("direction").enumValueIndex = (int)set[x];
                            direction.FindPropertyRelative("frames").arraySize = frameCount;
                        }
                    }

                    serializedObject.ApplyModifiedProperties();
                }
            }
        }

        void OnDestroy()
        {
            if(window != null)
            {
                window.serializedObject = null;
                window.state = null;

                window.Repaint();
            }
        }
    }
}