using UnityEngine;

namespace Assets.Scripts.Sensors
{
    public static class RenderTextureToTexture2DConverter
    {
        public static Texture2D Convert(RenderTexture renderTexture, TextureFormat format = TextureFormat.RGB24)
        {
            Texture2D texture = new(renderTexture.width, renderTexture.height, format, false);
            Rect rect = new(0, 0, texture.width, texture.height);
            RenderTexture.active = renderTexture;
            texture.ReadPixels(rect, 0, 0);
            texture.Apply();
            RenderTexture.active = null;

            return texture;
        }
    }
}
