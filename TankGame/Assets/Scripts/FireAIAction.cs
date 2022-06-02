using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FireAIAction", menuName = "Lec_11/Fire AI Action")]
public class FireAIAction : AIaction
{
    [Min(0f)]
    [SerializeField]
    private float firingCooldown = 5f;

    [Min(0f)]
    [SerializeField]
    private float firingAngle = 5f;

    public override void UpdateActionGizmos(AIController controller)
    {
        var player = GetPlayer(controller);
        if (player == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controller.transform.position, player.transform.position);
    }

    public override void UpdateAction(AIController controller)
    {
        var player = GetPlayer(controller);
        if (player == null)
        {
            return;
        }

        var controllerTransform = controller.transform;
        var currentDirection = controllerTransform.forward;
        var desiredDirection = GetDirection(
            controllerTransform.position, player.transform.position);

        var angle = GetAngle(currentDirection, desiredDirection);

       if(IsFire(controller, angle))
        {
            Fire(controller);
        }
    }

    private static Player GetPlayer(AIController controller)
    {
        return controller.Players.FirstOrDefault();
    }

    private static Vector3 GetDirection(Vector3 positionA, Vector3 positionB)
    {
        return Vector3.ProjectOnPlane(positionB - positionA, Vector3.up).normalized;
    }

    private static float GetAngle(Vector3 directionA, Vector3 directionB)
    {
        return Vector3.Angle(directionA, directionB);
    }

    private bool IsFire(AIController controller, float angle)
    {
        return controller.NextFireTime < Time.time && angle <= firingAngle;
    }

    private void Fire(AIController controller)
    {
        controller.Fire();
        controller.NextFireTime = Time.time + firingCooldown;
    }
}
