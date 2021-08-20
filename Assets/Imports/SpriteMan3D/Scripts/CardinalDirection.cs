namespace SpriteMan3D
{
    /// <summary>
    /// Cardinal directions available for directional sprite management.
    /// </summary>
    public enum CardinalDirection
    {
        /// <summary>
        /// The direction is not set.
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// North
        /// </summary>
        N = 1,
        /// <summary>
        /// North-Northeast
        /// </summary>
        NNE = 2,
        /// <summary>
        /// Northeast
        /// </summary>
        NE = 3,
        /// <summary>
        /// East-Northeast
        /// </summary>
        ENE = 4,
        /// <summary>
        /// East
        /// </summary>
        E = 5,
        /// <summary>
        /// East-Southeast
        /// </summary>
        ESE = 6,
        /// <summary>
        /// Southeast
        /// </summary>
        SE = 7,
        /// <summary>
        /// South-Southeast
        /// </summary>
        SSE = 8,
        /// <summary>
        /// South
        /// </summary>
        S = 9,
        /// <summary>
        /// South-Southwest
        /// </summary>
        SSW = 10,
        /// <summary>
        /// Southwest
        /// </summary>
        SW = 11,
        /// <summary>
        /// West-Southwest
        /// </summary>
        WSW = 12,
        /// <summary>
        /// West
        /// </summary>
        W = 13,
        /// <summary>
        /// West-Northwest
        /// </summary>
        WNW = 14,
        /// <summary>
        /// Northwest
        /// </summary>
        NW = 15,
        /// <summary>
        /// North-Northwest
        /// </summary>
        NNW = 16,

        #region 32 ways

        /// <summary>
        /// North by East
        /// </summary>
        NbE = 17,
        /// <summary>
        /// Northeast by North
        /// </summary>
        NEbN = 18,
        /// <summary>
        /// Northeast by East
        /// </summary>
        NEbE = 19,
        /// <summary>
        /// East by North
        /// </summary>
        EbN = 20,
        /// <summary>
        /// East by South
        /// </summary>
        EbS = 21,
        /// <summary>
        /// Southeast by East
        /// </summary>
        SEbE = 22,
        /// <summary>
        /// Southeast by South
        /// </summary>
        SEbS = 23,
        /// <summary>
        /// South by East
        /// </summary>
        SbE = 24,
        /// <summary>
        /// South by West
        /// </summary>
        SbW = 25,
        /// <summary>
        /// Southwest by South
        /// </summary>
        SWbS = 26,
        /// <summary>
        /// Southwest by West
        /// </summary>
        SWbW = 27,
        /// <summary>
        /// West by South
        /// </summary>
        WbS = 28,
        /// <summary>
        /// West by North
        /// </summary>
        WbN = 29,
        /// <summary>
        /// Northwest by West
        /// </summary>
        NWbW = 30,
        /// <summary>
        /// Northwest by North
        /// </summary>
        NWbN = 31,
        /// <summary>
        /// North by West
        /// </summary>
        NbW = 32

        #endregion
    }
}
