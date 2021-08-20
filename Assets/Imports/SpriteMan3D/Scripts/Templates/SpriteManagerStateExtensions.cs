using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpriteMan3D
{
    /// <summary>
    /// Extensions for ISprietManagerState.
    /// </summary>
    public static class SpriteManagerStateExtensions
    {
        /// <summary>
        /// Copies from a source <see cref="ISpriteManagerState"/> to a target <see cref="ISpriteManagerState"/>.
        /// </summary>
        /// <remarks>
        /// This utility is used to generate new SpriteManagerState objects and to copy SpriteManagerState to <see cref="SpriteManager"/> instances.
        /// Both those classes implement <see cref="ISpriteManagerState"/>.
        /// </remarks>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void CopyFrom(this ISpriteManagerState target, ISpriteManagerState source)
        {
            if (source != null)
            {
                target.Billboard = source.Billboard;
                target.DirectionMode = source.DirectionMode;
                target.ReflectionMode = source.ReflectionMode;

                var destStates = CopyStateMapping(source.StateMapping);

                target.StateMapping = destStates;
            }
        }

        /// <summary>
        /// Copies an array of state mappings.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static CharacterStateMapping[] CopyStateMapping(CharacterStateMapping[] source)
        {
            var result = new List<CharacterStateMapping>();
            foreach (var sourceState in source)
            {
                var destState = (CharacterStateMapping)sourceState.Clone();
                result.Add(destState);
            }

            return result.ToArray();
        }

        /// <summary>
        /// Replaces sprites in a given state with those in a sprite array matched by name or null if not found.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="sprites"></param>
        public static void ReplaceSprites(this ISpriteManagerState state, Sprite[] sprites)
        {
            foreach (var mapState in state.StateMapping)
            {
                foreach (var mapDir in mapState.directions)
                {
                    for (int x = 0; x < mapDir.frames.Length; x++)
                    {
                        var frame = mapDir.frames[x];

                        if (frame != null)
                        {
                            mapDir.frames[x] = sprites.FirstOrDefault(sprite => sprite.name == frame.name);
                        }
                    }
                }
            }
        }
    }
}
