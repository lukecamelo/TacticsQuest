using SpriteMan3D.Templates;
using SpriteMan3D.UnityEditor.Batching;
using SpriteMan3D.UnityEditor.Data;
using SpriteMan3D.UnityEditor.Utilities;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor
{
    [CustomEditor(typeof(SpriteSheetBatch))]
    internal class SpriteSheetBatchEditor : Editor
    {
        private List<DisplayResult> results;
        private Vector2 scrollPos;
        public DefaultAsset folder;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Process Batch"))
            {
                ProcessBatch();
            }

            if (results != null && results.Count > 0)
            {
                EditorGUILayout.LabelField("Results", EditorStyles.boldLabel);
                EditorDisplayHelper.ShowHierarchyScrollView(
                    results,
                    (stat) => stat.Message,
                    (stat) => stat.InnerResults,
                    ref scrollPos);
            }
        }

        private void ProcessBatch()
        {
            var loader = new AssetDbSpriteSheetLoader();
            var batchProc = new SpriteSheetBatchProcessor(loader);
            batchProc.batch = target as SpriteSheetBatch;

            batchProc.Process();
            results = batchProc.Results;
        }
    } 
}
