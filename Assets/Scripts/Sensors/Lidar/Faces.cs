using System;
using UnityEngine;

namespace Assets.Scripts.Sensors.Lidar
{
    public sealed class Faces : IDisposable
    {
        public int Resolution { get; private set; }
        public RenderTexture Right { get; private set; }
        public RenderTexture Left { get; private set; }
        public RenderTexture Top { get; private set; }
        public RenderTexture Bottom { get; private set; }
        public RenderTexture Forward { get; private set; }
        public RenderTexture Back { get; private set; }

        public Faces(int resolution)
        {
            Resolution = resolution;
            Right = CreateFace();
            Left = CreateFace();
            Top = CreateFace();
            Bottom = CreateFace();
            Forward = CreateFace();
            Back = CreateFace();
        }

        public void Dispose()
        {
            Right.Release();
            Left.Release();
            Top.Release();
            Bottom.Release();
            Forward.Release();
            Back.Release();
        }

        public void SetFaceParametersToShader(ComputeShader shader, int kernelIndex)
        {
            shader.SetInt("FaceResolution", Resolution);
            shader.SetTexture(kernelIndex, "RightFace", Right);
            shader.SetTexture(kernelIndex, "LeftFace", Left);
            shader.SetTexture(kernelIndex, "TopFace", Top);
            shader.SetTexture(kernelIndex, "BottomFace", Bottom);
            shader.SetTexture(kernelIndex, "ForwardFace", Forward);
            shader.SetTexture(kernelIndex, "BackFace", Back);
        }

        private RenderTexture CreateFace()
        {
            RenderTexture texture = new(Resolution, Resolution, 24);
            texture.Create();
            return texture;
        }
    }
}
