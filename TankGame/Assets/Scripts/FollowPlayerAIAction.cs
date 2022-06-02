using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FollowPlayerAIAction", menuName = "Lec_11/Follow Player AI Action")]
public class FollowPlayerAIAction : AIaction
{
    [Min(0f)]
    [SerializeField]
    private float followDistance = 15f;

    [SerializeField]
    private AIState playerMissingState;

    public override void UpdateActionGizmos(AIController controller)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(controller.transform.position, followDistance);
    }

    public override void UpdateAction(AIController controller)
    {
        var player = GetPlayer(controller);
        if (player == null)
        {
            return;
        }

        var playerPosition = player.transform.position;
        var distance = Vector3.Distance(controller.transform.position, playerPosition);

        if (distance > followDistance)
        {
            controller.ChangeState(playerMissingState);
            return;
        }

        var agent = controller.NavMeshAgent;
        agent.SetDestination(playerPosition);
    }

    private static Player GetPlayer(AIController controller)
    {
        return controller.Players.FirstOrDefault();
    }
}
