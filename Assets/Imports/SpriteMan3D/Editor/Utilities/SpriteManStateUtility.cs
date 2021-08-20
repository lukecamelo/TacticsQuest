using SpriteMan3D.Templates;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Utilities
{
    internal class SpriteManStateUtility
    {
        /// <summary>
        /// Creates a <see cref="SpriteManagerState"/> combining the given template <see cref="SpriteManagerState"/> and <see cref="Sprite"/> array. 
        /// </summary>
        /// <remarks>
        /// The new <see cref="SpriteManagerState"/> will have the layout of the template, and be loaded with the sprites of the sprite array.
        /// </remarks>
        /// <param name="template">a <see cref="SpriteManagerState"/>to use as a template</param>
        /// <param name="sprites">sprites to load the new <see cref="SpriteManagerState"/> with</param>
        /// <returns></returns>
        public static SpriteManagerState CreateStateFromTemplate(SpriteManagerState template, Sprite[] sprites)
        {
            var result = (SpriteManagerState)template.Clone();

            result.ReplaceSprites(sprites);

            return result;
        }
    }
}
