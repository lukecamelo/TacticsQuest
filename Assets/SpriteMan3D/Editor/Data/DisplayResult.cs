using System.Collections.Generic;

namespace SpriteMan3D.UnityEditor.Data
{
    /// <summary>
    /// Result message to display to users.
    /// </summary>
    internal class DisplayResult
    {
        /// <summary>
        /// Gets/sets the message to show.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Gets/sets child results.
        /// </summary>
        public List<DisplayResult> InnerResults { get; set; }

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="message"></param>
        public DisplayResult(string message)
        {
            Message = message;
            InnerResults = new List<DisplayResult>();
        }
    }
}
