using System.Linq;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Utilities
{
    internal class SpriteSheetUtility
    {
        /// <summary>
        /// Returns a sorted list of sprites from top-to-bottom left-to-right in sprite sheet.
        /// </summary>
        /// <param name="assetObjs">the loaded contents of a multi-texture</param>
        /// <returns></returns>
        public static Sprite[] ToSortedSpriteSheet(Object[] assetObjs)
        {
            var sprites = assetObjs
                    .Where(o => o is Sprite)
                    .Cast<Sprite>()
                    .ToArray();

            var result = SortSpriteSheet(sprites);

            return result;
        }

        /// <summary>
        /// Orders a list of sprites from top-to-bottom left-to-right
        /// </summary>
        /// <param name="unsortedSprites"></param>
        /// <returns></returns>
        private static Sprite[] SortSpriteSheet(Sprite[] unsortedSprites)
        {
            var result = unsortedSprites;

            // only process multi sprite textures
            if (unsortedSprites.Length > 1)
            {
                // order the sprites top-to-bottom left-to-right
                result =
                    unsortedSprites
                        .OrderByDescending(o => o.textureRect.y)
                        .ThenBy(o => o.textureRect.x)
                        .ToArray();
            }

            return result;
        }
    }
}
