using SpriteMan3D.Templates;
using SpriteMan3D.UnityEditor.Utilities;
using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Viewers
{
    internal class SpriteManagerViewer : SpriteManStateViewer<SpriteManager> { }
    internal class SpriteManagerStateViewer : SpriteManStateViewer<SpriteManagerState> { }

    internal class SpriteManStateViewer<T>
        where T : ISpriteManagerState
    {
        public T state;
        private Vector2 scrollPos;

        public void ShowSpriteManScrollView(float maxHeight = 200)
        {
            if (state != null)
            {
                EditorGUILayout.BeginVertical();

                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.MaxHeight(maxHeight));

                var states = state.StateMapping;
                foreach (var state in states)
                {
                    EditorGUILayout.LabelField(state.stateName);

                    foreach (var dir in state.directions)
                    {
                        var dirStr = string.Format("{0}{1}", EditorDisplayHelper.Tabs(1), dir.direction.ToString());
                        EditorGUILayout.LabelField(dirStr);

                        foreach (var frame in dir.frames)
                        {
                            var name = string.Format("{0}{1}", EditorDisplayHelper.Tabs(2), frame != null ? frame.name : "-empty-");
                            EditorGUILayout.LabelField(name);
                        }
                    }
                }

                EditorGUILayout.EndScrollView();

                EditorGUILayout.EndVertical();
            }
        }
    }
}
