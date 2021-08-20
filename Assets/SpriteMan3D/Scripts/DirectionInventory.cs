using System.Collections.Generic;

namespace SpriteMan3D
{
    /// <summary>
    /// Defines all angle ranges for all directions of 4, 8 and 16 direction modes.
    /// </summary>
    internal static class DirectionInventory
    {
        /// <summary>
        /// Reflection map for all directions.
        /// </summary>
        public static Dictionary<CardinalDirection, CardinalDirection> EWReflectionMap;
        /// <summary>
        /// Reflection map for all directions.
        /// </summary>
        public static Dictionary<CardinalDirection, CardinalDirection> WEReflectionMap;

        /// <summary>
        /// Gets the 2-direction angle range definitions.
        /// </summary>
        public static List<AngleRange> TwoDirections { get; private set; }
        /// <summary>
        /// Gets the 4-direction angle range definitions.
        /// </summary>
        public static List<AngleRange> FourDirections { get; private set; }
        /// <summary>
        /// Gets the 8-direction angle range definitions.
        /// </summary>
        public static List<AngleRange> EightDirections { get; private set; }
        /// <summary>
        /// Gets the 16-direction angle range definitions.
        /// </summary>
        public static List<AngleRange> SixteenDirections { get; private set; }
        /// <summary>
        /// Gets the 32-direction angle range definitions.
        /// </summary>
        public static List<AngleRange> ThirtyTwoDirections { get; private set; }

        /// <summary>
        /// static c'tor
        /// </summary>
        static DirectionInventory()
        {
            TwoDirections = new List<AngleRange>
            {
                new AngleRange(CardinalDirection.E, 0f, 180f),
                new AngleRange(CardinalDirection.W, 180f, 360f),
            };

            FourDirections = new List<AngleRange>
		    {
			    new AngleRange(CardinalDirection.N, 315f, 360f),
			    new AngleRange(CardinalDirection.N, 0f, 45f),
			    new AngleRange(CardinalDirection.E, 45f, 135f),
			    new AngleRange(CardinalDirection.S, 135f, 225f),
			    new AngleRange(CardinalDirection.W, 225f, 315f)
		    };

            EightDirections = new List<AngleRange>
		    {
			    new AngleRange(CardinalDirection.N, 337.5f, 360f),
			    new AngleRange(CardinalDirection.N, 0f, 22.5f),
			    new AngleRange(CardinalDirection.NE, 22.5f, 67.5f),
			    new AngleRange(CardinalDirection.E, 67.5f, 112.5f),
			    new AngleRange(CardinalDirection.SE, 112.5f, 157.5f),
			    new AngleRange(CardinalDirection.S, 157.5f, 202.5f),
			    new AngleRange(CardinalDirection.SW, 202.5f, 247.5f),
			    new AngleRange(CardinalDirection.W, 247.5f, 292.5f),
			    new AngleRange(CardinalDirection.NW, 292.5f, 337.5f)
		    };
            
            SixteenDirections = new List<AngleRange>
		    {
			    new AngleRange(CardinalDirection.N, 348.75f, 360f),
			    new AngleRange(CardinalDirection.N, 0f, 11.25f),
                new AngleRange(CardinalDirection.NNE, 11.25f, 33.75f),
			    new AngleRange(CardinalDirection.NE, 33.75f, 56.25f),
                new AngleRange(CardinalDirection.ENE, 56.25f, 78.75f),
			    new AngleRange(CardinalDirection.E, 78.75f, 101.25f),
                new AngleRange(CardinalDirection.ESE, 101.25f, 123.75f),
			    new AngleRange(CardinalDirection.SE, 123.75f, 146.25f),
                new AngleRange(CardinalDirection.SSE, 146.25f, 168.75f),
			    new AngleRange(CardinalDirection.S, 168.75f, 191.25f),
                new AngleRange(CardinalDirection.SSW, 191.25f, 213.75f),
			    new AngleRange(CardinalDirection.SW, 213.75f, 226.25f),
                new AngleRange(CardinalDirection.WSW, 226.25f, 258.75f),
			    new AngleRange(CardinalDirection.W, 258.75f, 281.25f),
                new AngleRange(CardinalDirection.WNW, 281.25f, 303.75f),
			    new AngleRange(CardinalDirection.NW, 303.75f, 326.25f),
                new AngleRange(CardinalDirection.NNW, 326.25f, 348.75f)
		    };

            ThirtyTwoDirections = new List<AngleRange>
            {
                new AngleRange(CardinalDirection.N, 354.375f, 360f),
                new AngleRange(CardinalDirection.N, 0f, 5.625f),
                new AngleRange(CardinalDirection.NbE, 5.625f, 16.875f),
                new AngleRange(CardinalDirection.NNE, 16.875f, 28.125f),
                new AngleRange(CardinalDirection.NEbN, 28.125f, 39.375f),
                new AngleRange(CardinalDirection.NE, 39.375f, 50.625f),
                new AngleRange(CardinalDirection.NEbE, 50.625f, 61.875f),
                new AngleRange(CardinalDirection.ENE, 61.875f, 73.125f),
                new AngleRange(CardinalDirection.EbN, 73.125f, 84.375f),
                new AngleRange(CardinalDirection.E, 84.375f, 95.625f),
                new AngleRange(CardinalDirection.EbS, 95.625f, 106.875f),
                new AngleRange(CardinalDirection.ESE, 106.875f, 118.125f),
                new AngleRange(CardinalDirection.SEbE, 118.125f, 129.375f),
                new AngleRange(CardinalDirection.SE, 129.375f, 140.625f),
                new AngleRange(CardinalDirection.SEbS, 140.625f, 151.875f),
                new AngleRange(CardinalDirection.SSE, 151.875f, 163.125f),
                new AngleRange(CardinalDirection.SbE, 163.125f, 174.375f),
                new AngleRange(CardinalDirection.S, 174.375f, 185.625f),
                new AngleRange(CardinalDirection.SbW, 185.625f, 196.875f),
                new AngleRange(CardinalDirection.SSW, 196.875f, 208.125f),
                new AngleRange(CardinalDirection.SWbS, 208.125f, 219.375f),
                new AngleRange(CardinalDirection.SW, 219.375f, 230.625f),
                new AngleRange(CardinalDirection.SWbW, 230.625f, 241.875f),
                new AngleRange(CardinalDirection.WSW, 241.875f, 253.125f),
                new AngleRange(CardinalDirection.WbS, 253.125f, 264.375f),
                new AngleRange(CardinalDirection.W, 264.375f, 275.625f),
                new AngleRange(CardinalDirection.WbN, 275.625f, 286.875f),
                new AngleRange(CardinalDirection.WNW, 286.875f, 298.125f),
                new AngleRange(CardinalDirection.NWbW, 298.125f, 309.375f),
                new AngleRange(CardinalDirection.NW, 309.375f, 320.625f),
                new AngleRange(CardinalDirection.NWbN, 320.625f, 331.875f),
                new AngleRange(CardinalDirection.NNW, 331.875f, 343.125f),
                new AngleRange(CardinalDirection.NbW, 343.125f, 354.375f)
            };

            EWReflectionMap = new Dictionary<CardinalDirection, CardinalDirection>();
            EWReflectionMap.Add(CardinalDirection.NNW, CardinalDirection.NNE);
            EWReflectionMap.Add(CardinalDirection.NW, CardinalDirection.NE);
            EWReflectionMap.Add(CardinalDirection.WNW, CardinalDirection.ENE);
            EWReflectionMap.Add(CardinalDirection.W, CardinalDirection.E);
            EWReflectionMap.Add(CardinalDirection.WSW, CardinalDirection.ESE);
            EWReflectionMap.Add(CardinalDirection.SW, CardinalDirection.SE);
            EWReflectionMap.Add(CardinalDirection.SSW, CardinalDirection.SSE);

            EWReflectionMap.Add(CardinalDirection.NbW, CardinalDirection.NbE);
            EWReflectionMap.Add(CardinalDirection.NWbN, CardinalDirection.NEbN);
            EWReflectionMap.Add(CardinalDirection.NWbW, CardinalDirection.NEbE);
            EWReflectionMap.Add(CardinalDirection.WbN, CardinalDirection.EbN);
            EWReflectionMap.Add(CardinalDirection.WbS, CardinalDirection.EbS);
            EWReflectionMap.Add(CardinalDirection.SWbW, CardinalDirection.SEbE);
            EWReflectionMap.Add(CardinalDirection.SWbS, CardinalDirection.SEbS);
            EWReflectionMap.Add(CardinalDirection.SbW, CardinalDirection.SbE);


            WEReflectionMap = new Dictionary<CardinalDirection, CardinalDirection>();
            WEReflectionMap.Add(CardinalDirection.NNE, CardinalDirection.NNW);
            WEReflectionMap.Add(CardinalDirection.NE, CardinalDirection.NW);
            WEReflectionMap.Add(CardinalDirection.ENE, CardinalDirection.WNW);
            WEReflectionMap.Add(CardinalDirection.E, CardinalDirection.W);
            WEReflectionMap.Add(CardinalDirection.ESE, CardinalDirection.WSW);
            WEReflectionMap.Add(CardinalDirection.SE, CardinalDirection.SW);
            WEReflectionMap.Add(CardinalDirection.SSE, CardinalDirection.SSW);

            WEReflectionMap.Add(CardinalDirection.NbE, CardinalDirection.NbW);
            WEReflectionMap.Add(CardinalDirection.NEbN, CardinalDirection.NWbN);
            WEReflectionMap.Add(CardinalDirection.NEbE, CardinalDirection.NWbW);
            WEReflectionMap.Add(CardinalDirection.EbN, CardinalDirection.WbN);
            WEReflectionMap.Add(CardinalDirection.EbS, CardinalDirection.WbS);
            WEReflectionMap.Add(CardinalDirection.SEbE, CardinalDirection.SWbW);
            WEReflectionMap.Add(CardinalDirection.SEbS, CardinalDirection.SWbS);
            WEReflectionMap.Add(CardinalDirection.SbE, CardinalDirection.SbW);
        }

        /// <summary>
        /// Gets angle range definitions for a given direction mode.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static AngleRanges GetStateOptions(DirectionMode mode)
        {
            List<AngleRange> ranges = null;
            if(mode == DirectionMode.TwoWay)
            {
                ranges = TwoDirections;
            }
            else if (mode == DirectionMode.FourWay)
            {
                ranges = FourDirections;
            }
            else if (mode == DirectionMode.EightWay)
            {
                ranges = EightDirections;
            }
            else if (mode == DirectionMode.SixteenWay)
            {
                ranges = SixteenDirections;
            }
            else if (mode == DirectionMode.ThirtyTwoWay)
            {
                ranges = ThirtyTwoDirections;
            }

            return new AngleRanges { Ranges = ranges };
        }
    }
}
