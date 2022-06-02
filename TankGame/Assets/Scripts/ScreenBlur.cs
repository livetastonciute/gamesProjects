using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenBlur : MonoBehaviour
{
    //private PlayerController playerController;
    //private DepthOfField depthOfField;

    // private bool currentPlayerControllerState;

    //private DepthOfFieldMode initialMode;

    [Min(0f)]
    [SerializeField]
    private float blurGaussianStart;

    [Min(0f)]
    [SerializeField]
    private float blurGaussianEnd;

    private DepthOfField depthOfField;

    private float initialGaussianStart;
    private float initialGaussianEnd;

    private void Awake()
    {
       // playerController = GetComponent<PlayerController>();
       // currentPlayerControllerState = playerController.enabled;

        depthOfField = GetDepthOfField();
        if (depthOfField == null)
        {
            Debug.LogError("Could not find DephOfField effect", this);
            enabled = false;
           // return;
        }

      //  initialMode = depthOfField.mode.value;
        initialGaussianStart = depthOfField.gaussianStart.value;
        initialGaussianEnd = depthOfField.gaussianEnd.value;
    }

    public void Blur()
    {
        SetBlur(blurGaussianStart, blurGaussianEnd);
    }

    public void UnBlur()
    {
        SetBlur(initialGaussianStart, initialGaussianEnd);
    }

    private void SetBlur(float start, float end)
    {
        depthOfField.mode.value = DepthOfFieldMode.Gaussian;
        depthOfField.gaussianStart.value = start;
        depthOfField.gaussianEnd.value = end;
        
    }
    private static DepthOfField GetDepthOfField()
    {
        var volume = FindObjectOfType<Volume>();
        if (volume == null)
        {
            return null;
        }

        volume.profile.TryGet<DepthOfField>(out var dephOfField);
        return dephOfField;

    }
    //private bool IsPlayerControllerStateChanged()
    //{
    //    return currentPlayerControllerState != playerController.enabled;
    //}

    //private void UpdateDephOfField()
    //{
    //    if (playerController.enabled)
    //    {
    //        RestoreDepthOfField();
    //    }
    //    else
    //    {
    //        BlurDepthOfField();
    //    }
    //}

    //private void RestoreDepthOfField()
    //{
    //    depthOfField.mode.value = initialMode;
    //    depthOfField.gaussianStart.value = initialGaussianStart;
    //    depthOfField.gaussianEnd.value = initialGaussianEnd;
    //}

    //private void BlurDepthOfField()
    //{
    //    depthOfField.mode.value = DepthOfFieldMode.Gaussian;
    //    depthOfField.gaussianStart.value = 0f;
    //    depthOfField.gaussianEnd.value = 5f;
    //}

    //private void UpdateCurrentPlayerControllerState()
    //{
    //    currentPlayerControllerState = playerController.enabled;
    //}
}
