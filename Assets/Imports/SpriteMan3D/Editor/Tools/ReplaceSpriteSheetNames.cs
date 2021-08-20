using SpriteMan3D.UnityEditor.Batching;
using SpriteMan3D.UnityEditor.Data;
using SpriteMan3D.UnityEditor.Utilities;
using SpriteMan3D.UnityEditor.Viewers;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Tools
{
    /// <summary>
    /// Replaces the frame names of a sprite sheet with the names stored in a <see cref="Assets.SpriteSheetMap"/>.
    /// </summary>
    internal class ReplaceSpriteSheetNames : IEditorTool
    {
        private bool fold;
        private SpriteSheetViewer sheetViewer;
        private SpriteMapViewer mapViewer;

        private List<DisplayResult> Results;
        private Vector2 scrollPos;

        public ReplaceSpriteSheetNames()
        {
            sheetViewer = new SpriteSheetViewer();
            mapViewer = new SpriteMapViewer();
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
                "Replace Sprite Sheet Names", 
                "Replaces names in a sprite sheet with those from a Sprite Sheet Map - created in the previous tool.", 
                ref fold);
        }

        void ShowOptions()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.BeginVertical();

                EditorDisplayHelper.ShowObjectInputField(
                    "Source Sprite Sheet Map",
                    "Drag a Sprite Sheet Map from the Project view",
                    ref mapViewer.map,
                    false,
                    () => Results = null);

                if (mapViewer.map)
                {
                    EditorGUILayout.LabelField("Source Map Names", EditorStyles.boldLabel);
                    mapViewer.ShowSpriteMapScrollView();
                }

                EditorGUILayout.EndVertical();
            }

            {
                EditorGUILayout.BeginVertical();
                EditorDisplayHelper.ShowObjectInputField(
                    "Target Sprite Sheet",
                    "Drag a sprite sheet in the Project view",
                    ref sheetViewer.image,
                    false,
                    () =>
                    {
                        sheetViewer.Loader.Clear();
                        Results = null;
                    });

                var loader = sheetViewer.Loader;
                if (loader.IsLoaded)
                {
                    EditorGUILayout.LabelField("Destination Image Names", EditorStyles.boldLabel);
                    sheetViewer.ShowSpriteScrollView();
                }

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndHorizontal();

            var canSave = mapViewer.map && sheetViewer.image;
            EditorGUI.BeginDisabledGroup(!canSave);
            if (GUILayout.Button("=> Replace Names =>"))
            {
                ReplaceNames();
            }
            EditorGUI.EndDisabledGroup();

            if (Results != null && Results.Count > 0)
            {
                EditorGUILayout.LabelField("Results", EditorStyles.boldLabel);
                EditorDisplayHelper.ShowScrollView(Results, (stat) => stat.Message, ref scrollPos);
            }
        }

        private void ReplaceNames()
        {
            var changer = new SpriteSheetNameChanger(mapViewer.map, sheetViewer.Loader);
            if (changer.CanChange)
            {
                changer.Process();
                sheetViewer.Loader.Clear();
                Results = changer.Results;
            }
        }
    }
}
