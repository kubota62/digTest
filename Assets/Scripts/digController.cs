using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class digController : MonoBehaviour
{
    [SerializeField]
    Camera camera = null;

    [SerializeField]
    Sprite ground = null;

    [SerializeField]
    Sprite dig = null;

    [SerializeField]
    SpriteRenderer spriteRenderer = null;

    Texture2D drawTarget;
    SpriteDrawer spriteDrawer = new SpriteDrawer();
    Material mat;
    Vector3 prevPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        mat = spriteRenderer.material;
        mat.SetTexture("_ground", ground.texture);


        spriteDrawer.Init(dig.texture, ref drawTarget);
        mat.SetTexture("_dig", drawTarget);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Update is called once per frame
    void OnDestroy()
    {
        GameObject.Destroy(mat);
    }

    // Update is called once per frame
    public void OnEnter(BaseEventData data)
    {
        var pointer = (data as PointerEventData);

        // Œ»Ý’l‚ð“h‚é
        spriteDrawer.DrawCircle(pointer.position);

        // ˆÈ‘O‚ÌÀ•W‚©‚ç‚Ì•âŠ®
        if (prevPos != Vector3.zero)
        {
            var diff = Vector3.Distance(pointer.position, prevPos);
            if (diff > 1)
            {
                var leapCount = (int)Math.Ceiling(diff) / 2;
                for (int i = 0; i < leapCount; i++)
                {
                    var t = (float)i / (float)leapCount;
                    var leapPos = Vector3.Lerp(pointer.position, prevPos, t);
                    spriteDrawer.DrawCircle(leapPos);
                }
            }
        }
        prevPos = pointer.position;

        spriteDrawer.Apply(ref drawTarget);

        mat.SetTexture("_dig", drawTarget);
    }

    Vector2 Diff(Vector2 tmp1, Vector3 tmp2)
    {
        return new Vector2(tmp1.x - tmp2.x, tmp1.y - tmp2.y);
    }
}
