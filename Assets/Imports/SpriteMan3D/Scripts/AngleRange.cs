namespace SpriteMan3D
{
    /// <summary>
    /// Class defining a range of angles that a cardinal direction is visible when facing a camera.
    /// </summary>
    internal class AngleRange
    {
        /// <summary>
        /// Gets or sets the cardinal direction for this angle range.
        /// </summary>
        public CardinalDirection Direction { get; set; }
        /// <summary>
        /// Gets or sets the min angle of this range.
        /// </summary>
        public float MinAngle { get; set; }
        /// <summary>
        /// Gets or sets the max angle of this range.
        /// </summary>
        public float MaxAngle { get; set; }

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public AngleRange(CardinalDirection dir, float min, float max)
        {
            Direction = dir;
            MinAngle = min;
            MaxAngle = max;
        }
    }
}
