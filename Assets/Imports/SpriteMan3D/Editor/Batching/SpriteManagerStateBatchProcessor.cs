using SpriteMan3D.UnityEditor.Data;
using SpriteMan3D.Templates;
using SpriteMan3D.UnityEditor.Utilities;
using System.Collections.Generic;

namespace SpriteMan3D.UnityEditor.Batching
{
    /// <summary>
    /// Processes a SpriteManagerStateBatch generating new SpriteManagerStates and display results.
    /// </summary>
    internal class SpriteManagerStateBatchProcessor : IBatchProcessor
    {
        /// <summary>
        /// The batch to process assignable by a custom editor.
        /// </summary>
        public SpriteManagerStateBatch batch;

        /// <summary>
        /// Func to call to get a loader for a sprite sheet.
        /// </summary>
        private ISpriteSheetLoader spriteSheetLoader;

        /// <summary>
        /// Stored results after processing.
        /// </summary>
        public List<StateBatchResult> BatchResults { get; private set; }

        /// <summary>
        /// Stored display output to show users after processing.
        /// </summary>
        public List<DisplayResult> Results { get; private set; }

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="spriteSheetLoader">loader that loads sprite sheets</param>
        public SpriteManagerStateBatchProcessor(ISpriteSheetLoader spriteSheetLoader)
        {
            Results = new List<DisplayResult>();
            BatchResults = new List<StateBatchResult>();

            this.spriteSheetLoader = spriteSheetLoader;
        }

        /// <summary>
        /// Generates a new SpriteManagerState for each sprite sheet in batch targets list.
        /// </summary>
        public void Process()
        {
            if (batch != null)
            {
                var source = batch.sourceState;

                foreach (var target in batch.targets)
                {
                    if (target)
                    {
                        var sheetResult = new DisplayResult(string.Format("Processing spritesheet '{0}'", target.name));
                        Results.Add(sheetResult);

                        spriteSheetLoader.Load(target);

                        var asset = SpriteManStateUtility.CreateStateFromTemplate(source, spriteSheetLoader.Sprites);
                        BatchResults.Add(new StateBatchResult(target, asset));
                    }
                }
            }
        }
    }
}
