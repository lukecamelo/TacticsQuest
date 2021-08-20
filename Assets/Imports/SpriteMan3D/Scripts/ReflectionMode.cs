using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteMan3D
{
    /// <summary>
    /// Determines reflection mode.
    /// </summary>
    public enum ReflectionMode
    {
        /// <summary>
        /// Don't reflect sprites
        /// </summary>
        None = 0,
        /// <summary>
        /// reflect east sprites to west sprites
        /// </summary>
        EastToWest = 1,
        /// <summary>
        /// reflect west sprites to east sprites
        /// </summary>
        WestToEast = 2
    }
}
