using System.Collections.Generic;
using System.Linq;

namespace SpriteMan3D
{
    /// <summary>
    /// Class defining a collection of angle ranges to use for a given sprite manager model.
    /// </summary>
    internal class AngleRanges
    {
        /// <summary>
        /// Gets or sets the valid angle ranges of this collections.
        /// </summary>
        public List<AngleRange> Ranges { get; set; }

        /// <summary>
        /// Gets the range for a given look direction angle.
        /// </summary>
        /// <param name="directionAngle"></param>
        /// <returns></returns>
        public CardinalDirection GetDirection(float directionAngle)
        {
            var result = CardinalDirection.N;

            var angleRange = Ranges.FirstOrDefault(o => o.MinAngle < directionAngle && directionAngle <= o.MaxAngle);
            if (angleRange != null)
            {
                result = angleRange.Direction;
            }
            return result;
        }
    }
}
