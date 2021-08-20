namespace SpriteMan3D.UnityEditor.Tools
{
    /// <summary>
    /// Interface for custom editor tools.
    /// </summary>
    /// <remarks>
    /// This interface is implemented to standardize tool display for multiple tools..
    /// </remarks>
    internal interface IEditorTool
    {
        /// <summary>
        /// When implemented in a derived class, it show's the entire GUI for a tool.
        /// </summary>
        void ShowGUI();
    } 
}
