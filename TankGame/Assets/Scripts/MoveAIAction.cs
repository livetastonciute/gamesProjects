using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "MoveAIAction", menuName = "Lec_11/Move AI Action")]
public class MoveAIAction : AIaction
{
    [Header("Agent")]
    [Min(0f)]
    [SerializeField]
    private float agentStoppingDistance = 2f;

    [Header("Controller")]
    [Min(0f)]
    [SerializeField]
    private float controllerStoppingDistance = 1f;

    [Range(0f, 180f)]
    [SerializeField]
    private float angleThreshold = 20f;

    [Range(0f, 1f)]
    [SerializeField]
    private float moveSmoothing = 0.5f;

    [Range(0f, 1f)]
    [SerializeField]
    private float rotationSmoothing = 0.1f;

    public override void UpdateActionGizmos(AIController controller)
    {
        var agent = controller.NavMeshAgent;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(agent.nextPosition, 0.25f);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(agent.destination, 0.25f);
    }

    public override void UpdateAction(AIController controller)
    {
        var controllerTransform = controller.transform;
        var controllerDirection = controllerTransform.forward;
        var controllerPosition = controllerTransform.position;

        var agent = controller.NavMeshAgent;
        var agentPosition = agent.nextPosition;

        var directionToAgent = GetDirection(controllerPosition, agentPosition);
        var distanceToAgent = GetDistance(controllerPosition, agentPosition);
        var angleToAgent = GetAngle(controllerDirection, directionToAgent);

        UpdateStopped(agent, distanceToAgent);
        UpdateMovement(controller, distanceToAgent, angleToAgent);
        UpdateRotation(controller, angleToAgent, directionToAgent);
    }

    private void UpdateStopped(NavMeshAgent agent, float distance)
    {
        agent.isStopped = distance > agentStoppingDistance;
    }

    private void UpdateMovement(AIController controller, float distance, float angle)
    {
        var targetAxis = controller.TargetAxis;
        targetAxis.y = 0f;

        if(distance > controllerStoppingDistance && angle <= angleThreshold)
        {
            targetAxis.y = GetYAxis(distance);
        }

        controller.TargetAxis = targetAxis;
    }

    private void UpdateRotation(AIController controller, float angle, Vector3 direction)
    {
        var targetAxis = controller.TargetAxis;
        var dot = Vector3.Dot(controller.transform.right, direction);

        targetAxis.x = Mathf.Sign(dot) * GetXAxis(angle);

        controller.TargetAxis = targetAxis;
    }

    private static Vector3 GetDirection(Vector3 positionA, Vector3 positionB)
    {
        return Vector3.ProjectOnPlane(positionB - positionA, Vector3.up).normalized;
    }

    private static float GetDistance(Vector3 positionA, Vector3 positionB)
    {
        return Vector3.Distance(positionA, positionB);
    }

    private static float GetAngle(Vector3 directionA, Vector3 directionB)
    {
        return Vector3.Angle(directionA, directionB);
    }

    private float GetYAxis(float distanceToAgent)
    {
        return Mathf.Min(distanceToAgent * moveSmoothing, 1);
    }

    private float GetXAxis(float angleToAgent)
    {
        return Mathf.Min(angleToAgent * rotationSmoothing, 1);
    }

}
