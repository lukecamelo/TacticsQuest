using SpriteMan3D.Templates;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Batching
{
    /// <summary>
    /// Stores SpriteManState batch processing result in memory.
    /// </summary>
    internal class StateBatchResult
    {
        /// <summary>
        /// The texture used to generate the state.
        /// </summary>
        public Texture2D source;
        /// <summary>
        /// The generated state.
        /// </summary>
        public ISpriteManagerState asset;

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="source"></param>
        /// <param name="asset"></param>
        public StateBatchResult(Texture2D source, ISpriteManagerState asset)
        {
            this.source = source;
            this.asset = asset;
        }
    }
}
