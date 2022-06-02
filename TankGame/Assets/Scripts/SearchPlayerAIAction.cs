using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SearchPlayerAIAction", menuName = "Lec_11/Search Player AI Action")]
public class SearchPlayerAIAction : AIaction
{
    [Min(0f)]
    [SerializeField]
    private float searchDistance = 10f;

    [SerializeField]
    private AIState playerFoundState;

    public override void UpdateActionGizmos(AIController controller)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(controller.transform.position, searchDistance);
    }

    public override void UpdateAction(AIController controller)
    {
        var player = GetPlayer(controller);
        if(player == null)
        {
            return;
        }

        var playerPosition = player.transform.position;
        var distance = Vector3.Distance(controller.transform.position, playerPosition);

        if(distance > searchDistance)
        {
            return;
        }

        controller.ChangeState(playerFoundState);
    }

    private static Player GetPlayer(AIController controller)
    {
        return controller.Players.FirstOrDefault();
    }
}
