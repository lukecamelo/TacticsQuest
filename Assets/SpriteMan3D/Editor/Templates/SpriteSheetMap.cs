using System;
using UnityEngine;

namespace SpriteMan3D.Templates
{
    /// <summary>
    /// Editor Only: Contains names and positions of sprites in a sprite sheet.
    /// </summary>
    public class SpriteSheetMap : ScriptableObject
    {
        /// <summary>
        /// The array of sprite names and their positions.
        /// </summary>
        public SpriteInfo[] sprites;
    }

    /// <summary>
    /// Editor Only: Contains the name and position of a single sprite.
    /// </summary>
    [Serializable]
    public class SpriteInfo
    {
        /// <summary>
        /// The name of a sprite.
        /// </summary>
        public string name;
        /// <summary>
        /// The top-left corner position in the sprite sheet.
        /// </summary>
        public Vector2 position;
    }
}
