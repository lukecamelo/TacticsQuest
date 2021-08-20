using SpriteMan3D.UnityEditor.Data;
using SpriteMan3D.UnityEditor.Utilities;
using UnityEngine;

namespace SpriteMan3D.UnityEditor.Viewers
{
    internal class SpriteSheetViewer
    {
        public Texture2D image;

        private AssetDbSpriteSheetLoader loader;
        private Vector2 scrollPos;

        public ISpriteSheetLoader Loader
        {
            get
            {
                if (!image && loader.IsLoaded)
                {
                    loader.Clear();
                }
                else if (!loader.IsLoaded && image)
                {
                    loader.Load(image);
                }

                return loader;
            }
        }

        public SpriteSheetViewer()
        {
            loader = new AssetDbSpriteSheetLoader();
        }

        public void ShowSpriteScrollView()
        {
            if (Loader.IsLoaded)
            {
                var sprites = loader.Sprites;
                EditorDisplayHelper.ShowScrollView(
                    sprites,
                    (sprite) => !sprite ? "" : string.Format("{0} {1}", sprite.name, sprite.rect.position),
                    ref scrollPos);
            }
        }
    }
}
