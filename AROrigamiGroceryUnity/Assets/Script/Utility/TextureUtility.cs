﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureUtility
{
    TextureStructure _textureStructure = new TextureStructure();

    private const float degreeToRadian = Mathf.PI / 180;

    public TextureStructure GrabTextureRadius(int p_width, int p_height, float ratio)
    {
        int criteria = (p_width > p_height) ? p_height : p_width;
        int radius = (int)((criteria * ratio) / 2f);
        int size = radius * 2;
        Vector2Int center = new Vector2Int((int)(p_width / 2f), (int)(p_height / 2f));

        _textureStructure.width = size;
        _textureStructure.height = size;
        _textureStructure.x = center.x - radius;
        _textureStructure.y = center.y - radius;

        _textureStructure.xRatio = (float)size / p_width;
        _textureStructure.yRatio = (float)size / p_height;

        _textureStructure.xResidualRatio = 1 - _textureStructure.xRatio;
        _textureStructure.yResidualRatio = 1 - _textureStructure.yRatio;

        return _textureStructure;
    }

    public struct TextureStructure
    {
        public int width;
        public int height;
        public int x;
        public int y;

        public float xRatio;
        public float yRatio;
        public float xResidualRatio;
        public float yResidualRatio;
    }

    public static RenderTexture GetRenderTexture(int size)
    {
        return GetRenderTexture(size, size, depth:0);
    }

    public static RenderTexture GetRenderTexture(int width, int height, int depth)
    {
        var rt = new RenderTexture(width, height, depth, RenderTextureFormat.ARGB32);
        
        rt.Create();
        return rt;
    }


    public static RenderTexture RotateAndScaleImage(Texture p_texture, RenderTexture renderer, Material rotateMat, TextureUtility.TextureStructure textureSetting, int degree)
    {
        float radian = degreeToRadian * degree;

        rotateMat.SetFloat("_EnlargeX", textureSetting.xRatio);
        rotateMat.SetFloat("_EnlargeY", textureSetting.yRatio);
        rotateMat.SetFloat("_Rotation", radian);

        Graphics.Blit(p_texture, renderer, rotateMat, 0);

        return renderer;
    }

    public struct RaycastResult
    {
        public Vector3 hitPoint;
        public Quaternion hitRotation;
        public bool hasHit;
    }
}
