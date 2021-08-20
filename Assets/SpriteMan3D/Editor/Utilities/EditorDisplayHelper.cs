using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Utilities
{
    internal static class EditorDisplayHelper
    {
        public static string Tabs(int tabCount)
        {
            return "".PadLeft(tabCount * 3);
        }

        public static void ShowHeader(string title, string desc, ref bool fold)
        {
            EditorGUILayout.BeginVertical(EditorStyles.toolbar);
            fold = EditorGUILayout.Foldout(fold, title, true, SpriteManStyles.boldFoldout);
            EditorGUILayout.EndVertical();


            EditorGUILayout.LabelField(desc, SpriteManStyles.description);
        }

        public static void ShowObjectInputField<T>(string label, string desc, ref T obj, bool allowSceneObjects, Action changeHandler = null)
            where T : UnityEngine.Object
        {
            EditorGUILayout.Space();

            // add label and description
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);

            // add field and X button
            EditorGUILayout.BeginHorizontal();

            if (changeHandler == null)
            {
                obj = (T)EditorGUILayout.ObjectField(obj, typeof(T), allowSceneObjects);
            }
            else
            {
                EditorGUI.BeginChangeCheck();
                obj = (T)EditorGUILayout.ObjectField(obj, typeof(T), allowSceneObjects);
                if (EditorGUI.EndChangeCheck())
                {
                    changeHandler();
                }
            }

            if (GUILayout.Button("X", GUILayout.Width(35f)))
            {
                obj = null;
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField(desc, SpriteManStyles.description);
        }

        public static void ShowScrollView<T>(IEnumerable<T> items, Func<T, string> getLabel, ref Vector2 scrollPos, float maxHeight = 200)
        {
            ShowHierarchyScrollView(items, getLabel, null, ref scrollPos, maxHeight);
        }

        public static void ShowHierarchyScrollView<T>(IEnumerable<T> items, Func<T, string> getLabel, Func<T, IEnumerable<T>> getInnerList, ref Vector2 scrollPos, float maxHeight = 200)
        {
            EditorGUILayout.BeginVertical();

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.MaxHeight(maxHeight));

            ShowScrollViewRecursive(items, getLabel, 0, getInnerList);

            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndVertical();
        }

        private static void ShowScrollViewRecursive<T>(IEnumerable<T> items, Func<T, string> getLabel, int tabIndex = 0, Func<T, IEnumerable<T>> getInnerList = null)
        {
            foreach (var item in items)
            {
                if (item != null)
                {
                    EditorGUILayout.LabelField(string.Format("{0}{1}", Tabs(tabIndex), getLabel(item)));

                    if (getInnerList != null)
                    {
                        var innerList = getInnerList(item);
                        if (innerList != null)
                        {
                            ShowScrollViewRecursive(innerList, getLabel, tabIndex + 1, getInnerList);
                        }
                    }
                }
            }
        }
    }
}
