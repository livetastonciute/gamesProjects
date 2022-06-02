using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OutlineFeature : ScriptableRendererFeature
{
    [SerializeField]
    private Material outlineMaterial;

    private const string TextureName = "_OutlineTexture";

    private RenderTargetHandle outlineTexture;
    private OutlinePass outlinePass;

    public override void Create()
    {
        outlineTexture.Init(TextureName);
        outlinePass = new OutlinePass(outlineMaterial)
        {
            renderPassEvent = RenderPassEvent.AfterRenderingTransparents
        };
    }
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if(outlineMaterial == null)
        {
            return;
        }

        outlinePass.Setup(renderer.cameraColorTarget, RenderTargetHandle.CameraTarget);
        renderer.EnqueuePass(outlinePass);
    }
}
