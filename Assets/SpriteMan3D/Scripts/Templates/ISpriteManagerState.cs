namespace SpriteMan3D
{
    /// <summary>
    /// Interface for SpriteMan State.
    /// </summary>
    public interface ISpriteManagerState
    {
        /// <summary>
        /// When implemented in a derived class, gets/sets whether to billboard a sprite.
        /// </summary>
        bool Billboard { get; set; }
        /// <summary>
        /// When implemented in a derived class, gets/sets the direction mode of this state.
        /// </summary>
        DirectionMode DirectionMode { get; set; }
        /// <summary>
        /// When implemented in a derived class, gets/sets the reflection direction.
        /// </summary>
        ReflectionMode ReflectionMode { get; set; }
        /// <summary>
        /// When implemented in a derived class, gets/sets all character state mappings for this spriteman state.
        /// </summary>
        CharacterStateMapping[] StateMapping { get; set; }
    }
}
