using SpriteMan3D.UnityEditor.Utilities;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Data
{
    /// <summary>
    /// Repo for workin with the <see cref="AssetDatabase"/>.
    /// </summary>
    internal static class AssetDbRepo
    {
        /// <summary>
        /// Loads a sprite sheet from the disk.
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        public static Sprite[] LoadSpriteSheet(Object userObj)
        {
            // load the multi sprites
            var path = AssetDatabase.GetAssetPath(userObj);
            var assetObjs = AssetDatabase.LoadAllAssetsAtPath(path);

            var sprites = SpriteSheetUtility.ToSortedSpriteSheet(assetObjs);
            
            return sprites;
        }

        /// <summary>
        /// Saves an asset to disk and optionally focuses on the new asset.
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="assetName"></param>
        /// <param name="folder"></param>
        /// <param name="focusOnNewAsset"></param>
        public static void CreateAssetInFolder(Object asset, string assetName, DefaultAsset folder, bool focusOnNewAsset = true)
        {
            var folderPath = AssetDatabase.GetAssetPath(folder);
            var filePath = string.Format("{0}/{1}.asset", folderPath, assetName);
            CreateAssetInFolder(asset, filePath, focusOnNewAsset);
        }

        /// <summary>
        /// Saves an asset to disk and optionally focuses on the new asset.
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="filePath"></param>
        /// <param name="focusOnNewAsset"></param>
        public static void CreateAssetInFolder(Object asset, string filePath, bool focusOnNewAsset = true)
        {
            var uniquePath = AssetDatabase.GenerateUniqueAssetPath(filePath);
            AssetDatabase.CreateAsset(asset, uniquePath);

            AssetDatabase.SaveAssets();

            if (focusOnNewAsset)
            {
                EditorUtility.FocusProjectWindow();

                Selection.activeObject = asset;
            }
        }

        /// <summary>
        /// Creates a directory if it doesn't exist.
        /// </summary>
        /// <param name="dir"></param>
        public static void EnsureDirectoryExists(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}
