using SpriteMan3D.UnityEditor.Data;
using System.Collections.Generic;

namespace SpriteMan3D.UnityEditor.Batching
{
    /// <summary>
    /// Interface for batch processing.
    /// </summary>
    internal interface IBatchProcessor
    {
        /// <summary>
        /// When implemented in a derived class, gets a list of result messages to display to a user.
        /// </summary>
        List<DisplayResult> Results { get; }
        /// <summary>
        /// When implemented in a derived class, processes a batch.
        /// </summary>
        void Process();
    }
}
