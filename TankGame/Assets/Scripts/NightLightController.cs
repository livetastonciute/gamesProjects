using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightLightController : MonoBehaviour
{
    [Min(0f)]
    [SerializeField]
    private float intensityTransitionSpeed = 3f;

    [SerializeField]
    private bool smoothIntensity;

    private DayTimeController dayTimeController;
    private new Light light;

    private float initialIntensity;
    private float targetIntensity;

    private void Awake()
    {
        dayTimeController = FindObjectOfType<DayTimeController>();
        light = GetComponent<Light>();

        initialIntensity = light.intensity;
    }

    private void Start()
    {
        UpdateTargetIntensity();
        UpdateLightIntensity();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTargetIntensity();

        if (smoothIntensity)
        {
            UpdateLightIntensitySmooth();
        }
        else
        {
            UpdateLightIntensity();
        }
    }

    private void UpdateTargetIntensity()
    {
        targetIntensity = dayTimeController.IsDay
            ? 0f
            : initialIntensity;
    }

    private void UpdateLightIntensitySmooth()
    {
        light.intensity = Mathf.Lerp(
            light.intensity,
            targetIntensity,
            Time.deltaTime * intensityTransitionSpeed);
    }

    private void UpdateLightIntensity()
    {
        light.intensity = targetIntensity;
    }
}
