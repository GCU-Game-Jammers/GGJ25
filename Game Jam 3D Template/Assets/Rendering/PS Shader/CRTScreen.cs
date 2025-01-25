using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRTScreen : MonoBehaviour
{
    public Vector2 resolution;
    public Material crtMaterial;
    public Camera camToRender;

    private RenderTexture rt;

    void Start()
    {
        rt = new RenderTexture(256, 224, 16, RenderTextureFormat.ARGB32);
        rt.Create();

        crtMaterial.mainTexture = rt;

        camToRender.targetTexture = rt;
    }

    private void OnDestroy()
    {
        rt.Release();
    }
}
