using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Data
{
    /// <summary>
    /// Loads a sprite sheet using the <see cref="AssetDatabase"/>.
    /// </summary>
    internal class AssetDbSpriteSheetLoader : ISpriteSheetLoader
    {
        public string SpriteSheetPath { get; private set; }
        public Sprite[] Sprites { get; private set; }
        public bool IsLoaded { get { return Sprites != null && Sprites.Length > 1; } }

        public void Load(Texture2D texture)
        {
            SpriteSheetPath = AssetDatabase.GetAssetPath(texture);
            Sprites = AssetDbRepo.LoadSpriteSheet(texture);
        }

        public void Clear()
        {
            SpriteSheetPath = null;
            Sprites = null;
        }
    }
}
