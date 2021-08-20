using System.Collections.Generic;

namespace SpriteMan3D
{
    /// <summary>
    /// Defines the valid cardinal directions for each direction mode.
    /// </summary>
    public static class DirectionModeCardinalSets
    {
        /// <summary>
        /// A dictionary containing the cardinal directions for each direction mode.
        /// </summary>
        public static Dictionary<DirectionMode, CardinalDirection[]> ModeSets;

        /// <summary>
        /// static c'tor
        /// </summary>
        static DirectionModeCardinalSets()
        {
            ModeSets = new Dictionary<DirectionMode, CardinalDirection[]>();
            ModeSets.Add(DirectionMode.TwoWay,
                new CardinalDirection[]
                {
                    CardinalDirection.E,
                    CardinalDirection.W
                });

            ModeSets.Add(
                DirectionMode.FourWay,
                new CardinalDirection[] 
                    { 
                        CardinalDirection.N, 
                        CardinalDirection.E, 
                        CardinalDirection.S, 
                        CardinalDirection.W 
                    });

            ModeSets.Add(
                DirectionMode.EightWay,
                new CardinalDirection[] 
                    { 
                        CardinalDirection.N, 
                        CardinalDirection.NE, 
                        CardinalDirection.E,
                        CardinalDirection.SE, 
                        CardinalDirection.S,
                        CardinalDirection.SW, 
                        CardinalDirection.W,
                        CardinalDirection.NW
                    });

            ModeSets.Add(
                DirectionMode.SixteenWay,
                new CardinalDirection[] 
                    { 
                        CardinalDirection.N,
                        CardinalDirection.NNE, 
                        CardinalDirection.NE, 
                        CardinalDirection.ENE, 
                        CardinalDirection.E,
                        CardinalDirection.ESE, 
                        CardinalDirection.SE,
                        CardinalDirection.SSE, 
                        CardinalDirection.S,
                        CardinalDirection.SSW, 
                        CardinalDirection.SW,
                        CardinalDirection.WSW, 
                        CardinalDirection.W,
                        CardinalDirection.WNW, 
                        CardinalDirection.NW,
                        CardinalDirection.NNW 
                    });

            ModeSets.Add(DirectionMode.ThirtyTwoWay,
                new CardinalDirection[]
                    {
                        CardinalDirection.N,
                        CardinalDirection.NbE,
                        CardinalDirection.NNE,
                        CardinalDirection.NEbN,
                        CardinalDirection.NE,
                        CardinalDirection.NEbE,
                        CardinalDirection.ENE,
                        CardinalDirection.EbN,
                        CardinalDirection.E,
                        CardinalDirection.EbS,
                        CardinalDirection.ESE,
                        CardinalDirection.SEbE,
                        CardinalDirection.SE,
                        CardinalDirection.SEbS,
                        CardinalDirection.SSE,
                        CardinalDirection.SbE,
                        CardinalDirection.S,
                        CardinalDirection.SbW,
                        CardinalDirection.SSW,
                        CardinalDirection.SWbS,
                        CardinalDirection.SW,
                        CardinalDirection.SWbW,
                        CardinalDirection.WSW,
                        CardinalDirection.WbS,
                        CardinalDirection.W,
                        CardinalDirection.WbN,
                        CardinalDirection.WNW,
                        CardinalDirection.NWbW,
                        CardinalDirection.NW,
                        CardinalDirection.NWbN,
                        CardinalDirection.NNW,
                        CardinalDirection.NbW
                    });
        }
    }
}
