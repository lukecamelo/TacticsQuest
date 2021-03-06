using UnityEngine;

namespace SpriteMan3D
{
    /// <summary>
    /// Default predefined set of state names and hashes that come with SpriteMan3D.
    /// </summary>
    internal static class IdleState
    {
        /// <summary>
        /// Idle state string.
        /// </summary>
        public const string Idle = "Idle";

        /// <summary>
        /// Gets the hash of the Idle state as generated by Animator.StringToHash().
        /// </summary>
        public static int IdleHash { get; private set; }
        
        /// <summary>
        /// static c'tor
        /// </summary>
        static IdleState()
        {
            IdleHash = Animator.StringToHash(Idle);
        }

        /// <summary>
        /// Creates a new array of CharacterStateMapping with an empty 8-direction Idle state.
        /// </summary>
        /// <returns></returns>
        internal static CharacterStateMapping[] GetNewDefaultIdleStateSet()
        {
            var cardinalDirectionSet = DirectionModeCardinalSets.ModeSets[DirectionMode.EightWay];
            
            var directions = new DirectionSpriteMapping[8];

            for (int x = 0; x < 8; x++)
            {
                directions[x] = new DirectionSpriteMapping
                {
                    direction = cardinalDirectionSet[x],
                    frames = new Sprite[1]
                };
            }

            return new CharacterStateMapping[]
            {
                new CharacterStateMapping
                {
                    stateName = Idle,
                    frameCount = 1,
                    directions = directions
                }
            };
        }
    }
}
