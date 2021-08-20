using SpriteMan3D.Templates;
using SpriteMan3D.UnityEditor.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace SpriteMan3D.UnityEditor.Batching
{
    /// <summary>
    /// Changes the names of a sprite sheet with the names found in a given sprite sheet map.
    /// </summary>
    internal class SpriteSheetNameChanger : IBatchProcessor
    {
        /// <summary>
        /// Gets/sets the loader of the target sprite sheet.
        /// </summary>
        public ISpriteSheetLoader TargetLoader { get; set; }
        /// <summary>
        /// Gets/sets the source sprite sheet map.
        /// </summary>
        public SpriteSheetMap Source { get; set; }
        /// <summary>
        /// Gets/sets the display results to show to users.
        /// </summary>
        public List<DisplayResult> Results { get; set; }

        /// <summary>
        /// Determines if target loader is loaded and source is set.
        /// </summary>
        public bool CanChange
        {
            get
            {
                return
                    TargetLoader.IsLoaded
                    && Source;
            }
        }

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destLoader"></param>
        public SpriteSheetNameChanger(SpriteSheetMap source, ISpriteSheetLoader destLoader)
        {
            TargetLoader = destLoader;
            Source = source;
        }

        /// <summary>
        /// Changes the names of a sprite sheet with the names found in a given sprite sheet map.
        /// </summary>
        public void Process()
        {
            var mapSprites = Source.sprites;
            var sprites = TargetLoader.Sprites;

            var mismatches = new List<string>();
            var importer = AssetImporter.GetAtPath(TargetLoader.SpriteSheetPath) as TextureImporter;
            var spritesheet = importer.spritesheet.ToList();

            for (int x = 0; x < mapSprites.Length; x++)
            {
                var mapSprite = mapSprites[x];
                var match = spritesheet.FirstOrDefault(o => o.rect.position == mapSprite.position);

                var def = default(SpriteMetaData);

                if (!match.Equals(def))
                {
                    if (match.name != mapSprite.name)
                    {
                        spritesheet.Remove(match);

                        match.name = mapSprite.name;
                        spritesheet.Add(match);
                    }
                }
                else
                {
                    mismatches.Add(mapSprite.name);
                }
            }

            importer.spritesheet = spritesheet.ToArray();
            EditorUtility.SetDirty(importer);
            importer.SaveAndReimport();

            AssetDatabase.ImportAsset(TargetLoader.SpriteSheetPath, ImportAssetOptions.ForceUpdate);

            Results = mismatches
                .Select(o => 
                    new DisplayResult(string.Format("no match found for '{0}'", o)))
                .ToList();
        }
    }
}
