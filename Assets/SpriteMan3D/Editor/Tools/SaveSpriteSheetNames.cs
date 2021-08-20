using SpriteMan3D.UnityEditor.Data;
using SpriteMan3D.UnityEditor.Utilities;
using SpriteMan3D.UnityEditor.Viewers;
using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Tools
{
    /// <summary>
    /// Saves the frame names of a sprite sheet to a <see cref="Assets.SpriteSheetMap"/>.
    /// </summary>
    internal class SaveSpriteSheetNames : IEditorTool
    {
        private bool fold;
        private SpriteSheetViewer viewer;
        private DefaultAsset folder;

        public SaveSpriteSheetNames()
        {
            viewer = new SpriteSheetViewer();
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
                "Save Sprite Sheet Names",
                "Saves all the names in a sprite sheet to a reloadable template: Sprite Sheet Map.", 
                ref fold);

        }

        void ShowOptions()
        {
            EditorDisplayHelper.ShowObjectInputField(
                "Texture to Save",
                "Drag a sprite sheet from the Project view",
                ref viewer.image,
                false,
                () => viewer.Loader.Clear());

            EditorDisplayHelper.ShowObjectInputField(
                "Output Folder",
                "Drag a folder from the Project view",
                ref folder,
                false);

            var canSave = viewer.image && folder;
            EditorGUI.BeginDisabledGroup(!canSave);
            if (GUILayout.Button("Save Map"))
            {
                SaveMap();
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space();

            var loader = viewer.Loader;
            if (loader.IsLoaded)
            {
                EditorGUILayout.LabelField("Preview");
                EditorGUILayout.LabelField(string.Format("Slices Count: {0}", loader.Sprites.Length));
                viewer.ShowSpriteScrollView();
            }
        }

        void SaveMap()
        {
            var builder = new SpriteSheetMapBuilder(viewer.Loader);
            var spriteMap = builder.Build();
            
            AssetDbRepo.CreateAssetInFolder(spriteMap, viewer.image.name, folder);
        }
    }
}
