using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Rigidbody))]
public class HoveringController : MonoBehaviour
{
    [Header("Hovering")]
    [Min(0f)]
    [SerializeField]
    private float hoverDistance = 1.2f;

    [Min(0f)]
    [SerializeField]
    private float hoverForce = 9f;

    [SerializeField]
    private KeyCode hoverKey = KeyCode.Space;

    [SerializeField]
    private Transform localCenterOfMass;

    [Header("PID Controller")]
    [SerializeField]
    [Tooltip("Counters current error")]
    private float proportionalConstant = 0.5f;

    [SerializeField]
    [Tooltip("Counters cumulated error")]
    private float integralConstant = 0.01f;

    [SerializeField]
    [Tooltip("Figths oscillation")]
    private float derivativeConstant = 0.17f;

    [Header("Events")]
    [SerializeField]
    private UnityEvent onHoverStart;

    [SerializeField]
    private UnityEvent onHoverStop;

    private List<HoverPoint> hoverPoints;
    private new Rigidbody rigidbody;
    private bool hovering;

    private float currentHoverDuration;

    private void OnDrawGizmos()
    {
        if(hoverPoints == null)
        {
            hoverPoints = GetHoverPoints();
        }

        DrawHoverPointGizmos();
    }

    private void OnDisable()
    {
        hovering = false;
    }

    private void Awake()
    {
        hoverPoints = GetHoverPoints();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = localCenterOfMass.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        var newHovering = IsHover();
        if (newHovering)
        {
            UpdateHoverStart();
        }
        else
        {
            UpdateHoverStop();
        }
    }
    private void FixedUpdate()
    {
        if (hovering)
        {
            AddHoverPointForce();
        }
    }
    private void DrawHoverPointGizmos()
    {
        Gizmos.color = Color.red;
        foreach(var hoverPoint in hoverPoints)
        {
            DrawHoverPointGizmos(hoverPoint);
        }
    }

    private void DrawHoverPointGizmos(HoverPoint hoverPoint)
    {
        var hoverTransform = hoverPoint.transform;
        var hoverPosition = hoverTransform.position;
        var hoverDirection = hoverTransform.forward;

        Gizmos.DrawRay(hoverPosition, hoverDirection * hoverDistance);
    }

    private List<HoverPoint> GetHoverPoints()
    {
        return GetComponentsInChildren<HoverPoint>().ToList();
    }

    private void UpdateHoverStart()
    {
        if (!hovering)
        {
            onHoverStart.Invoke();
        }

        hovering = true;
    }

    private void UpdateHoverStop()
    {
        if (hovering)
        {
            onHoverStop.Invoke();
        }

        hovering = false;
    }

    private bool IsHover()
    {
        return Input.GetKey(hoverKey);
    }

    private void AddHoverPointForce()
    {
        foreach(var hoverPoint in hoverPoints)
        {
            AddHoverPointForce(hoverPoint);
        }
    }

    private void AddHoverPointForce(HoverPoint hoverPoint)
    {
        var hoverTransform = hoverPoint.transform;
        var hoverPosition = hoverTransform.position;
        var hoverDirection = hoverTransform.forward;

        if(IsSurface(hoverPosition, hoverDirection, out var hit))
        {
            var distanceError = hoverDistance - hit.distance;
            var forceMultiplier = GetForce(hoverPoint, distanceError);
            var force = -hoverDirection * (hoverForce * forceMultiplier);
            rigidbody.AddForceAtPosition(force, hoverPosition, ForceMode.Acceleration);
        }
    }

    private bool IsSurface(Vector3 hoverPosition, Vector3 hoverDirection, out RaycastHit hit)
    {
        return Physics.Raycast(hoverPosition, hoverDirection, out hit, hoverDistance);
    }

    private float GetForce(HoverPoint hoverPoint, float distanceError)
    {
        return GetForce(hoverPoint, distanceError, Time.deltaTime);
    }

    private float GetForce(HoverPoint hoverPoint, float distanceError, float dt)
    {
        var derivative = (distanceError - hoverPoint.LastError) / dt;
        hoverPoint.Integral += distanceError * dt;
        hoverPoint.LastError = distanceError;

        var rawValue = proportionalConstant * distanceError + integralConstant *
            hoverPoint.Integral + derivativeConstant * derivative;

        hoverPoint.Value = Mathf.Clamp01(rawValue);

        return hoverPoint.Value;
    }
}
