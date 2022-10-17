using System;
using UnityEngine;

namespace Assets.Scripts.Sensors.Lidar
{
    [Serializable]
    public sealed class Faces : IDisposable
    {
        public RenderTexture Right, Left, Top, Bottom, Forward, Back;

        public void Dispose()
        {
            Right?.Release();
            Left?.Release();
            Top?.Release();
            Bottom?.Release();
            Forward?.Release();
            Back?.Release();
        }

        public void SetTextures(ComputeShader shader, int kernelIndex)
        {
            shader.SetTexture(kernelIndex, "RightFace", Right);
            shader.SetTexture(kernelIndex, "LeftFace", Left);
            shader.SetTexture(kernelIndex, "TopFace", Top);
            shader.SetTexture(kernelIndex, "BottomFace", Bottom);
            shader.SetTexture(kernelIndex, "ForwardFace", Forward);
            shader.SetTexture(kernelIndex, "BackFace", Back);
        }
    }
}
