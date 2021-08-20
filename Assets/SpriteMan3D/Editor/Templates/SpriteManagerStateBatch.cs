using SpriteMan3D.Templates;
using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.Templates
{
    /// <summary>
    /// Editor Only: Batch class for SpriteManagerStates.
    /// </summary>
    public class SpriteManagerStateBatch : ScriptableObject
    {
        /// <summary>
        /// The source state to use for this batch.
        /// </summary>
        public SpriteManagerState sourceState;
        /// <summary>
        /// A group of sprite sheets to use for generation.
        /// </summary>
        public Texture2D[] targets;
        /// <summary>
        /// A folder to output generated SpriteManagerStates to.
        /// </summary>
        public DefaultAsset outputFolder;
    }
}
