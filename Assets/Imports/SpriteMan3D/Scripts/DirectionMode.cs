namespace SpriteMan3D
{
    /// <summary>
    /// The number of directions a sprite manager should use.
    /// </summary>
    public enum DirectionMode
    {
        /// <summary>
        /// Denotes a model with 2 directions: East (E), West (W)
        /// </summary>
        TwoWay = 0,
        /// <summary>
        /// Denotes a model with 4 directions: North (N), East (E), South (S), West (W)
        /// </summary>
        FourWay = 1,
        /// <summary>
        /// Denotes a model with 8 directions: North (N), Northeast (NE), East (E), Southeast (SE), South (S), Southwest (SW), West (W), Northwest (NW)
        /// </summary>
        EightWay = 2,
        /// <summary>
        /// Denotes a model with 16 directions: North (N), North-northeast (NNE), Northeast (NE), East-northeast (ENE), East (E), East-southeast (ESE), Southeast (SE), South-southeast (SSE), South (S), South-southwest (SSW), Southwest (SW), West-southwest (WSW), West (W), West-northwest (WNW), Northwest (NW), North-northwest (NNW)
        /// </summary>
        SixteenWay = 3,
        /// <summary>
        /// Denotes a model with 32 directions.
        /// </summary>
        ThirtyTwoWay = 4
    }
}
