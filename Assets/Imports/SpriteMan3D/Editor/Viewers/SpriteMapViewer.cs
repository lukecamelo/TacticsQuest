using SpriteMan3D.Templates;
using SpriteMan3D.UnityEditor.Utilities;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Viewers
{
    internal class SpriteMapViewer
    {
        public SpriteSheetMap map;
        private Vector2 scrollPos;

        public void ShowSpriteMapScrollView()
        {
            if (map)
            {
                var sprites = map.sprites;
                EditorDisplayHelper.ShowScrollView(
                    sprites,
                    (sprite) => string.Format("{0} {1}", sprite.name, sprite.position),
                    ref scrollPos);
            }
        }
    }
}
