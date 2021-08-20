using SpriteMan3D.Templates;
using SpriteMan3D.UnityEditor.Batching;
using SpriteMan3D.UnityEditor.Data;
using SpriteMan3D.UnityEditor.Utilities;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor
{
    [CustomEditor(typeof(SpriteManagerStateBatch))]
    internal class SpriteManagerStateBatchEditor : Editor
    {
        private List<DisplayResult> results;
        private Vector2 scrollPos;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var batch = target as SpriteManagerStateBatch;

            var canSave = batch.sourceState && batch.targets.Length > 0 && batch.outputFolder;
            EditorGUI.BeginDisabledGroup(!canSave);
            if (GUILayout.Button("Process Batch"))
            {
                ProcessBatch();
            }
            EditorGUI.EndDisabledGroup();

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
            var batchProc = new SpriteManagerStateBatchProcessor(loader);
            batchProc.batch = target as SpriteManagerStateBatch;

            batchProc.Process();
            results = batchProc.Results;

            foreach(var result in batchProc.BatchResults)
            {
                SaveAsset(result);
            }
        }

        private void SaveAsset(StateBatchResult result)
        {
            var batch = target as SpriteManagerStateBatch;

            var asset = result.asset as SpriteManagerState;
            AssetDbRepo.CreateAssetInFolder(asset, result.source.name, batch.outputFolder, false);
        }
    }
}
