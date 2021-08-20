using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpriteMan3D.Templates
{
    /// <summary>
    /// SpriteManagerState scriptable object can be saved to disk and loaded at runtime.
    /// </summary>
    public class SpriteManagerState : ScriptableObject, ISpriteManagerState, ICloneable
    {
        /// <summary>
        /// Whether to billboard a sprite.
        /// </summary>
        public bool billboard;
        /// <summary>
        /// The direction mode of a SpriteManager.
        /// </summary>
        public DirectionMode directionMode;
        /// <summary>
        /// Reflect east to west, or west to east.
        /// </summary>
        public ReflectionMode reflectionMode;
        /// <summary>
        /// All state mappings of a SpriteManager.
        /// </summary>
        public CharacterStateMapping[] stateMapping;

        /// <summary>
        /// ISpriteManagerState.Billboard
        /// </summary>
        public bool Billboard
        {
            get { return billboard; }
            set { billboard = value; }
        }

        /// <summary>
        /// ISpriteManagerState.DirectionMode
        /// </summary>
        public DirectionMode DirectionMode
        {
            get { return directionMode; }
            set { directionMode = value; }
        }

        /// <summary>
        /// ISpriteManagerState.ReflectionMode
        /// </summary>
        public ReflectionMode ReflectionMode
        {
            get { return reflectionMode; }
            set { reflectionMode = value; }
        }

        /// <summary>
        /// ISpriteManagerState.StateMapping
        /// </summary>
        public CharacterStateMapping[] StateMapping
        {
            get { return stateMapping; }
            set { stateMapping = value; }
        }

        /// <summary>
        /// Deep clones this SpriteManagerState
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var result = CreateInstance<SpriteManagerState>();
            result.name = name;
            result.billboard = billboard;
            result.directionMode = directionMode;
            result.reflectionMode = reflectionMode;

            var states = new List<CharacterStateMapping>();

            foreach (var state in stateMapping)
            {
                states.Add((CharacterStateMapping)state.Clone());
            }

            result.stateMapping = states.ToArray();

            return result;
        }
    }
}
