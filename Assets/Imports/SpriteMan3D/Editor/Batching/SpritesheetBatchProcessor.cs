using SpriteMan3D.UnityEditor.Data;
using SpriteMan3D.Templates;
using System.Collections.Generic;

namespace SpriteMan3D.UnityEditor.Batching
{
    /// <summary>
    /// Processes a SpriteSheetBatch, renaming frames in multiple sprite sheets.
    /// </summary>
    internal class SpriteSheetBatchProcessor : IBatchProcessor
    {
        /// <summary>
        /// The sprite sheet batch to process.
        /// </summary>
        public SpriteSheetBatch batch;

        /// <summary>
        /// Sprite Sheet loader to use during processing.
        /// </summary>
        private ISpriteSheetLoader spriteSheetLoader;

        /// <summary>
        /// Results to show a user.
        /// </summary>
        public List<DisplayResult> Results { get; private set; }

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="spriteSheetLoader">loader to use for loading sprite sheets</param>
        public SpriteSheetBatchProcessor(ISpriteSheetLoader spriteSheetLoader)
        {
            Results = new List<DisplayResult>();
            this.spriteSheetLoader = spriteSheetLoader;
        }

        /// <summary>
        /// Renames all sprites in batch's target sprite sheets using the batch's source map.
        /// </summary>
        public void Process()
        {
            if (batch != null)
            {
                var source = batch.sourceSpriteSheetMap;

                foreach (var target in batch.targets)
                {
                    if (target)
                    {
                        var sheetResult = new DisplayResult(string.Format("Processing spritesheet '{0}'", target.name));
                        Results.Add(sheetResult);

                        spriteSheetLoader.Load(target);

                        var changer = new SpriteSheetNameChanger(source, spriteSheetLoader);
                        if (changer.CanChange)
                        {
                            changer.Process();
                            spriteSheetLoader.Clear();
                            sheetResult.InnerResults = changer.Results;
                        }
                    }
                }
            }
        }
    }
}
