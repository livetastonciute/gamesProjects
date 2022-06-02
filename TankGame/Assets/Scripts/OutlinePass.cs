using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OutlinePass : ScriptableRenderPass
{
    private const string BufferName = "_OutlinePass";

    private readonly Material outlineMaterial;
    private RenderTargetHandle temporaryColorTexture;

    private RenderTargetIdentifier source;
    private RenderTargetHandle destination;

    public OutlinePass(Material outlineMaterial)
    {
        this.outlineMaterial = outlineMaterial;
    }

    public void Setup(RenderTargetIdentifier sourceId, RenderTargetHandle destinationHandle)
    {
        source = sourceId;
        destination = destinationHandle;
    }

    public override void Execute(
        ScriptableRenderContext context, 
        ref RenderingData renderingData)
    {
        var cmd = CommandBufferPool.Get(BufferName);

        var opaqueDescriptor = renderingData.cameraData.cameraTargetDescriptor;
        opaqueDescriptor.depthBufferBits = 0;

        if (destination == RenderTargetHandle.CameraTarget)
        {
            cmd.GetTemporaryRT(temporaryColorTexture.id, opaqueDescriptor,
                FilterMode.Point);
            Blit(cmd, source, temporaryColorTexture.Identifier(), outlineMaterial);
            Blit(cmd, temporaryColorTexture.Identifier(), source);
        }
        else
        {
            Blit(cmd, source, destination.Identifier(), outlineMaterial);
        }

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
    public override void FrameCleanup(CommandBuffer cmd)
    {
        if(destination == RenderTargetHandle.CameraTarget)
        {
            cmd.ReleaseTemporaryRT(temporaryColorTexture.id);
        }
    }
}
