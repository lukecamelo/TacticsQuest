using SpriteMan3D.Templates;
using SpriteMan3D.UnityEditor.Data;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Utilities
{
    /// <summary>
    /// Creates a SpriteSheetMap from a given sprite sheet.
    /// </summary>
    internal class SpriteSheetMapBuilder
    {
        /// <summary>
        /// The source sprite sheet loader.
        /// </summary>
        public ISpriteSheetLoader Loader { get; set; }

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="loader"></param>
        public SpriteSheetMapBuilder(ISpriteSheetLoader loader)
        {
            Loader = loader;
        }

        /// <summary>
        /// Creates a SpriteSheetMap from a given sprite sheet.
        /// </summary>
        /// <returns></returns>
        public SpriteSheetMap Build()
        {
            var result = ScriptableObject.CreateInstance<SpriteSheetMap>();

            if (Loader.IsLoaded)
            {
                var sprites = new List<SpriteInfo>();

                foreach (var sprite in Loader.Sprites)
                {
                    var info = new SpriteInfo();
                    info.name = sprite.name;
                    info.position = sprite.rect.position;

                    sprites.Add(info);
                }

                result.sprites = sprites.ToArray();
            }

            return result;
        }
    }
}
