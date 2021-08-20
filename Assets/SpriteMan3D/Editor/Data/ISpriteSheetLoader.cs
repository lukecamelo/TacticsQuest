using UnityEngine;

namespace SpriteMan3D.UnityEditor.Data
{
    /// <summary>
    /// Interface for loading a sprite sheet into memory.
    /// </summary>
    internal interface ISpriteSheetLoader
    {
        /// <summary>
        /// When implemented in a derived class, gets the path of the sprite sheet to load.
        /// </summary>
        string SpriteSheetPath { get; }
        /// <summary>
        /// When implemented in a derived class, gets whether loading has been run.
        /// </summary>
        bool IsLoaded { get; }
        /// <summary>
        /// When implemented in a derived class, gets the loaded sprites.
        /// </summary>
        Sprite[] Sprites { get; }
        /// <summary>
        /// When implemented in a derived class, loads sprites of a sprite sheet from a given texture to the Sprites array.
        /// </summary>
        /// <param name="texture">the texture to load.</param>
        void Load(Texture2D texture);
        /// <summary>
        /// When implemented in a derived class, clears the sprites and loaded state.
        /// </summary>
        void Clear();
    }
}
