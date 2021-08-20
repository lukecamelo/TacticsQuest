using SpriteMan3D.Templates;
using SpriteMan3D.UnityEditor.Utilities;
using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Tools
{
    /// <summary>
    /// Loads a <see cref="SpriteManagerState"/> to a <see cref="SpriteManager"/> in your scene. 
    /// </summary>
    internal class LoadSpriteManState : IEditorTool
    {
        private bool fold;
        private SpriteManagerState source;
        private SpriteManager dest;

        public LoadSpriteManState()
        {
        }

        public void ShowGUI()
        {
            ShowHeader();
            if (fold)
            {
                ShowOptions();
            }
            EditorGUILayout.Space();
        }

        void ShowHeader()
        {
            EditorDisplayHelper.ShowHeader(
                "Load SpriteMan State", 
                "Loads a SpriteMan State to a SpriteManager in your scene.",
                ref fold);
        }

        void ShowOptions()
        {
            EditorDisplayHelper.ShowObjectInputField(
                "Source SpriteMan State",
                "Drag a Sprite Manager State from the Project view",
                ref source,
                false);

            EditorDisplayHelper.ShowObjectInputField(
                "Target Sprite Manager",
                "Drag a Sprite Manager from the Scene Hierarchy",
                ref dest,
                true);

            EditorGUILayout.Space();

            var canSave = source && dest;
            EditorGUI.BeginDisabledGroup(!canSave);
            if (GUILayout.Button("Load State"))
            {
                CopyMap();
            }
            EditorGUI.EndDisabledGroup();
        }

        void CopyMap()
        {
            dest.CopyFrom(source);
            SceneView.RepaintAll();
            AssetDatabase.Refresh();
        }
    }
}
