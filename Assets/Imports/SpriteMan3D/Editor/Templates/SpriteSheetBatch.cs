using UnityEngine;

namespace SpriteMan3D.Templates
{
    /// <summary>
    /// Editor Only: Batch class for sprite sheet renaming.
    /// </summary>
    public class SpriteSheetBatch : ScriptableObject
    {
        /// <summary>
        /// The source map containing good names of sprites.
        /// </summary>
        public SpriteSheetMap sourceSpriteSheetMap;
        /// <summary>
        /// A group of sprite sheets whose names to replace.
        /// </summary>
        public Texture2D[] targets;
    }
}
