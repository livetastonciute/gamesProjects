using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;

public class HoveringController : MonoBehaviour
{
    [SerializeField]
    private float hoverDistance = 1.2f;

    [SerializeField]
    private float hoverForce = 9f;

    [SerializeField]
    private KeyCode hoverKey = KeyCode.H;

    [SerializeField]
    private Transform localCenterOfMass;

    [SerializeField]
    [Tooltip("Counters current error")]
    private float proportionalConstant = 0.5f;

    [SerializeField]
    [Tooltip("Counters cumulated error")]
    private float integralConstant = 0.01f;

    [SerializeField]
    [Tooltip("Fights oscillaction")]
    private float derivaticeConstant = 0.17f;

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
        foreach(var point in hoverPoints)
        {
            DrawHoverPointGizmos(point);
        }
    }
    private void DrawHoverPointGizmos(HoverPoint point)
    {
        var hoverTransform = point.transform;
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
        foreach(var point in hoverPoints)
        {
            AddHoverPointForce(point);
        }
    }

    private void AddHoverPointForce(HoverPoint point)
    {
        var hoverTransform = point.transform;
        var hoverPosition = hoverTransform.position;
        var hoverDirection = hoverTransform.forward;

        if(IsSurface(hoverPosition, hoverDirection, out var hit))
        {
            var distanceError = hoverDistance - hit.distance;
            var forceMultiplier = GetForce(point, distanceError);
            var force = -hoverDirection * (hoverForce * forceMultiplier);
            rigidbody.AddForceAtPosition(force, hoverPosition, ForceMode.Acceleration);
        }
    }

    private bool IsSurface(Vector3 hoverPosition, Vector3 hoverDirection, out RaycastHit hit)
    {
        return Physics.Raycast(hoverPosition, hoverDirection, out hit, hoverDistance);
    }

    private float GetForce(HoverPoint point, float distanceError, float dt)
    {
        var derivative = (distanceError - point.LastError) / dt;
        point.Integral += distanceError * dt;
        point.LastError = distanceError;

        var rawValue = proportionalConstant * distanceError + integralConstant * point.Integral + derivaticeConstant * derivative;

        point.Value = Mathf.Clamp01(rawValue);

        return point.Value;
    }

    private float GetForce(HoverPoint point, float distanceError)
    {
        return GetForce(point, distanceError, Time.deltaTime);
    }
}
