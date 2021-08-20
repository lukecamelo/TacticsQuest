using SpriteMan3D.UnityEditor.Tools;
using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor
{
    /// <summary>
    /// Window containing all SpriteMan tools.
    /// </summary>
    internal class SpriteManToolsWindow : EditorWindow
    {
        //sprite sheet tools
        IEditorTool saveSpriteNames;
        IEditorTool replaceSpriteNames;

        //sprite manager tools
        IEditorTool saveManagerState;
        IEditorTool convertSheetToState;
        IEditorTool loadManagerState;

        Vector2 scrollPos;

        public SpriteManToolsWindow()
        {
            saveSpriteNames = new SaveSpriteSheetNames();
            replaceSpriteNames = new ReplaceSpriteSheetNames();
            saveManagerState = new SaveSpriteManState();
            convertSheetToState = new ConvertSheetToState();
            loadManagerState = new LoadSpriteManState();
        }

        private void OnGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            
            saveSpriteNames.ShowGUI();
            EditorGUILayout.Separator();

            replaceSpriteNames.ShowGUI();
            EditorGUILayout.Separator();

            saveManagerState.ShowGUI();
            EditorGUILayout.Separator();

            convertSheetToState.ShowGUI();
            EditorGUILayout.Separator();

            loadManagerState.ShowGUI();

            EditorGUILayout.EndScrollView();
        }

        private void ShowSection(IEditorTool tool)
        {
            tool.ShowGUI();
        }
    }
}
