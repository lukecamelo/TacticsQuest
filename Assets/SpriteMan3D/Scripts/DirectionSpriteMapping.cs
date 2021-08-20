using System;
using System.Linq;
using UnityEngine;

namespace SpriteMan3D
{
    /// <summary>
    /// Class that maps a cardinal direction to an array of sprite frames.
    /// </summary>
    [Serializable]
    public class DirectionSpriteMapping : ICloneable
    {
        /// <summary>
        /// The cardinal direction for this mapping.
        /// </summary>
        public CardinalDirection direction;
        /// <summary>
        /// The sprite frames used for animation when this direction is visible.
        /// </summary>
        public Sprite[] frames;

        public DirectionSpriteMapping() { }

        protected DirectionSpriteMapping(DirectionSpriteMapping obj)
        {
            direction = obj.direction;
            frames = obj.frames
                .Select(sprite => 
                    sprite != null ? 
                    sprite // NOTE: this is a reference, not a clone!
                    : default(Sprite))
                .ToArray();
        }

        public object Clone()
        {
            return new DirectionSpriteMapping(this);
        }
    }
}
