using SpriteMan3D.Templates;
using SpriteMan3D.UnityEditor.Data;
using SpriteMan3D.UnityEditor.Utilities;
using SpriteMan3D.UnityEditor.Viewers;
using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Tools
{
    /// <summary>
    /// Saves a <see cref="SpriteManager"/> to a <see cref="SpriteManagerState"/>.
    /// </summary>
    internal class SaveSpriteManState : IEditorTool
    {
        private bool fold;
        private SpriteManagerViewer viewer;
        private DefaultAsset folder;

        public SaveSpriteManState()
        {
            viewer = new SpriteManagerViewer();
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
                "Save SpriteMan State",
                "Saves the state of a SpriteManager in your scene to a reloadable template: SpriteMan State.",
                ref fold);
        }

        void ShowOptions()
        {
            EditorDisplayHelper.ShowObjectInputField(
                "Sprite Manager",
                "Drag a Sprite Manager from the Scene Hierarchy",
                ref viewer.state,
                true);

            EditorDisplayHelper.ShowObjectInputField(
                "Output Folder",
                "Drag a folder from the Project view",
                ref folder,
                false);

            var manager = viewer.state;
            var canSave = manager && folder;
            EditorGUI.BeginDisabledGroup(!canSave);
            if (GUILayout.Button("Save State"))
            {
                SaveMap();
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space();

            if (manager)
            {
                EditorGUILayout.LabelField("Preview");
                viewer.ShowSpriteManScrollView();
            }
        }

        void SaveMap()
        {
            var manager = viewer.state;

            if (manager != null)
            {
                var path = AssetDatabase.GetAssetPath(folder);

                var state = ScriptableObject.CreateInstance<SpriteManagerState>();
                state.CopyFrom(manager);

                AssetDbRepo.CreateAssetInFolder(state, manager.name, folder);
            }
        }
    }
}
