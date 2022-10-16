using UnityEngine;

namespace Assets.Scripts.Sensors
{
    public static class RenderTextureToTexture2DConverter
    {
        public static Texture2D Convert(RenderTexture renderTexture)
        {
            Texture2D texture = new(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
            Rect rect = new(0, 0, texture.width, texture.height);
            RenderTexture.active = renderTexture;
            texture.ReadPixels(rect, 0, 0);
            texture.Apply();
            RenderTexture.active = null;

            return texture;
        }
    }
}
