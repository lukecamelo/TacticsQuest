using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Utilities
{
    internal static class SpriteManStyles
    {
        public static GUIStyle boldFoldout;
        public static GUIStyle description;

        static SpriteManStyles()
        {
            boldFoldout = new GUIStyle(EditorStyles.foldout);
            boldFoldout.fontStyle = FontStyle.Bold;

            description = new GUIStyle();
            description.fontStyle = FontStyle.Italic;
            description.padding = new RectOffset(5, 5, 0, 0);
            description.wordWrap = true;
        }
    }
}
