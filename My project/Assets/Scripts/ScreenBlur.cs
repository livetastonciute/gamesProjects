using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenBlur : MonoBehaviour
{
    private PlayerController playerController;
    private DepthOfField depthOfField;

    private bool currentPlayerControllerState;

    private DepthOfFieldMode initialMode;
    private float initialGaussianStart;
    private float initialGaussianEnd;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        currentPlayerControllerState = playerController.enabled;

        depthOfField = GetDepthOfField();
        if(depthOfField == null)
        {
            Debug.LogError("Could not find depthOfField effect", this);
            enabled = false;
            return;
        }

        initialMode = depthOfField.mode.value;
        initialGaussianStart = depthOfField.gaussianStart.value;
        initialGaussianEnd = depthOfField.gaussianEnd.value;

        Debug.Log("Testuoju");
    }

    private static DepthOfField GetDepthOfField()
    {
        var volume = FindObjectOfType<Volume>();
        if(volume == null)
        {
            return null;
        }

        volume.profile.TryGet<DepthOfField>(out var depthOfField);
        return depthOfField;
    }

    private bool IsPlayerControllerStateChanged()
    {
        return currentPlayerControllerState != playerController.enabled;
    }

    private void UpdateDepthOfFied()
    {
        if (playerController.enabled)
        {
            RestoreDepthOfField();
        }
        else
        {
            BlurDepthOfField();
        }
    }

    private void RestoreDepthOfField()
    {
        depthOfField.mode.value = initialMode;
        depthOfField.gaussianStart.value = initialGaussianStart;
        depthOfField.gaussianEnd.value = initialGaussianEnd;
    }

    private void BlurDepthOfField()
    {
        depthOfField.mode.value = DepthOfFieldMode.Gaussian;
        depthOfField.gaussianStart.value = 0f;
        depthOfField.gaussianEnd.value = 5f;
    }

    private void UpdateCurrentPlayerControllerState()
    {
        currentPlayerControllerState = playerController.enabled;
    }
}


