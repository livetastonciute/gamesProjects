#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

TEXTURE2D(_CameraDepthTexture);
SAMPLER(sampler_CameraDepthTexture);

float sampleDepth(float2 uv)
{
    // @formatter:off
#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
    return SAMPLE_TEXTURE2D_ARRAY(
        _CameraDepthTexture,
        sampler_CameraDepthTexture,
        uv, unity_StereoEyeIndex
    ).r;
#else
    return SAMPLE_DEPTH_TEXTURE(
        _CameraDepthTexture,
        sampler_CameraDepthTexture,
        uv
    );
#endif
    // @formatter:on
}

void sobel_float(float2 uv, float thickness, out float output)
{
    float2 delta = float2(thickness, thickness) * 0.0001;

    float hr = 0;
    float vt = 0;

    // @formatter:off
    hr += sampleDepth(uv + float2(-1.0, -1.0) * delta) *  1.0;
    hr += sampleDepth(uv + float2( 1.0, -1.0) * delta) * -1.0;
    hr += sampleDepth(uv + float2(-1.0,  0.0) * delta) *  2.0;
    hr += sampleDepth(uv + float2( 1.0,  0.0) * delta) * -2.0;
    hr += sampleDepth(uv + float2(-1.0,  1.0) * delta) *  1.0;
    hr += sampleDepth(uv + float2( 1.0,  1.0) * delta) * -1.0;

    vt += sampleDepth(uv + float2(-1.0, -1.0) * delta) *  1.0;
    vt += sampleDepth(uv + float2( 0.0, -1.0) * delta) *  2.0;
    vt += sampleDepth(uv + float2( 1.0, -1.0) * delta) *  1.0;
    vt += sampleDepth(uv + float2(-1.0,  1.0) * delta) * -1.0;
    vt += sampleDepth(uv + float2( 0.0,  1.0) * delta) * -2.0;
    vt += sampleDepth(uv + float2( 1.0,  1.0) * delta) * -1.0;
    // @formatter:on

    output = pow(1 - saturate(sqrt(hr * hr + vt * vt)), 50);
}
