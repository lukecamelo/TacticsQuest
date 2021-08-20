using SpriteMan3D.UnityEditor.Data;
using SpriteMan3D.UnityEditor.Utilities;
using SpriteMan3D.UnityEditor.Viewers;
using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Tools
{
    /// <summary>
    /// Converts a sprite sheet to a <see cref="Assets.SpriteManagerState"/> using a source <see cref="Assets.SpriteManagerState"/>.
    /// </summary>
    internal class ConvertSheetToState : IEditorTool
    {
        private bool fold;
        private SpriteManagerStateViewer stateViewer;
        private SpriteSheetViewer sheetViewer;
        private DefaultAsset folder;

        public ConvertSheetToState()
        {
            stateViewer = new SpriteManagerStateViewer();
            sheetViewer = new SpriteSheetViewer();
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
                "Convert Sprite Sheet to SpriteMan State",
                "Creates a new SpriteMan State using a SpriteMan State as a template and combining it with a specified sprite sheet.",
                ref fold);
        }

        void ShowOptions()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.BeginVertical();

                EditorDisplayHelper.ShowObjectInputField(
                    "Source SpriteMan State",
                    "Drag to a Sprite Manager State from the Project view",
                    ref stateViewer.state,
                    false);

                if (stateViewer.state)
                {
                    EditorGUILayout.LabelField("Source Template", EditorStyles.boldLabel);
                    stateViewer.ShowSpriteManScrollView(265);
                }

                EditorGUILayout.EndVertical();
            }

            {
                EditorGUILayout.BeginVertical();
                EditorDisplayHelper.ShowObjectInputField(
                    "Sprite Sheet To Use",
                    "Drag a sprite sheet from the Project view",
                    ref sheetViewer.image,
                    false,
                    () => sheetViewer.Loader.Clear());

                EditorDisplayHelper.ShowObjectInputField(
                    "Output Folder",
                    "Drag a folder from the Project view",
                    ref folder,
                    false);

                if (sheetViewer.image)
                {
                    EditorGUILayout.LabelField("Destination Image Names", EditorStyles.boldLabel);
                    sheetViewer.ShowSpriteScrollView();
                }

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndHorizontal();


            var canGenerate = stateViewer.state && sheetViewer.image && folder;
            EditorGUI.BeginDisabledGroup(!canGenerate);
            if (GUILayout.Button("Generate SpriteMan State"))
            {
                CopyMap();
            }
            EditorGUI.EndDisabledGroup();
        }

        void CopyMap()
        {
            var asset = SpriteManStateUtility.CreateStateFromTemplate(stateViewer.state, sheetViewer.Loader.Sprites);
            AssetDbRepo.CreateAssetInFolder(asset, sheetViewer.image.name, folder);
        }
    }
}
