using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDrawer
{
    static readonly int DrawCircleRadius = 20;

    Color[] pixels = null;
    int width;
    int height;

    public void Init(Texture2D src, ref Texture2D dist)
    {
        // 元絵のPixel情報取得
        pixels = src.GetPixels();
        width = src.width;
        height = src.height;

        // 書き換え用テクスチャの生成
        dist = new Texture2D(width, height, TextureFormat.RGBA32, false);
        dist.filterMode = FilterMode.Point;
        dist.SetPixels(pixels);
        dist.Apply();
    }

    public void DrawCircle(Vector3 pos)
    {
        DrawCircle((int)Math.Floor(pos.x), (int)Math.Floor(pos.y));
    }

    public void Apply(ref Texture2D dist)
    {
        // 書き換え用テクスチャの生成
        dist.SetPixels(pixels);
        dist.Apply();
    }

    /* 中心(x0,y0)とする半径rの真円を描く */
    void DrawCircle(int x0, int y0)
    {
        int radius, x, y, F;

        for (int i = DrawCircleRadius; i > 0; i--)
        {
            radius = i;
            x = radius;
            y = 0;
            F = -2 * radius + 3;

            while (x >= y)
            {
                SetDraw(x0 + x, y0 + y);
                SetDraw(x0 - x, y0 + y);
                SetDraw(x0 + x, y0 - y);
                SetDraw(x0 - x, y0 - y);
                SetDraw(x0 + y, y0 + x);
                SetDraw(x0 - y, y0 + x);
                SetDraw(x0 + y, y0 - x);
                SetDraw(x0 - y, y0 - x);

                if (F >= 0)
                {
                    x--;
                    F -= 4 * x;
                }

                y++;
                F += 4 * y + 2;
            }
        }
    }

    void SetDraw(int x, int y)
    {
        pixels[x + (y * width)] = Color.white;
    }
}
