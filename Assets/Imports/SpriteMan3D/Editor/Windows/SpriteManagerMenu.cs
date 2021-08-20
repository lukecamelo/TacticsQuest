using SpriteMan3D.UnityEditor.Data;
using SpriteMan3D.Templates;
using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor
{
    /// <summary>
    /// Contains menu items for the Tools\SpriteMan 3D menu.
    /// </summary>
    internal class SpriteManagerMenu : MonoBehaviour
    {
        private const string genFolderPath = "Assets/SpriteMan3D/Generated/";

        /// <summary>
        /// Creates a new <see cref="SpriteSheetBatch"/>.
        /// </summary>
        [MenuItem("Tools/SpriteMan 3D/Create/Sprite Sheet Batch", priority = 10)]
        static void CreateSpritesheetBatch()
        {
            AssetDbRepo.EnsureDirectoryExists(genFolderPath);

            var batch = ScriptableObject.CreateInstance<SpriteSheetBatch>();
            var path = AssetDatabase.GenerateUniqueAssetPath(genFolderPath + "SpritesheetBatch.asset");

            AssetDbRepo.CreateAssetInFolder(batch, path);
        }

        /// <summary>
        /// Creates a new <see cref="SpriteManagerState"/>.
        /// </summary>
        [MenuItem("Tools/SpriteMan 3D/Create/SpriteMan State Batch", priority = 11)]
        static void CreateSpriteManStateBatch()
        {
            AssetDbRepo.EnsureDirectoryExists(genFolderPath);

            var batch = ScriptableObject.CreateInstance<SpriteManagerStateBatch>();
            var path = AssetDatabase.GenerateUniqueAssetPath(genFolderPath + "SpriteManStateBatch.asset");

            AssetDbRepo.CreateAssetInFolder(batch, path);
        }

        /// <summary>
        /// Opens the SpriteMan tools window.
        /// </summary>
        [MenuItem("Tools/SpriteMan 3D/SpriteMan Tools Window", priority = 12)]
        static void OpenSpriteManToolsWindow()
        {
            var window = EditorWindow.GetWindow<SpriteManToolsWindow>(false, "SpriteMan");
            window.Show();
        }
    }
}
