using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "RoamAIAction", menuName = "Lec_11/Roam AI Action")]
public class RoamAIAction : AIaction
{
    [Min(0f)]
    [SerializeField]
    private float newDestinationDistance = 0.1f;

    [Min(0f)]
    [SerializeField]
    private float roamRadius = 20f;

    public override void UpdateActionGizmos(AIController controller)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(controller.transform.position, roamRadius);
    }

    public override void UpdateAction(AIController controller)
    {
        var agent = controller.NavMeshAgent;
        if(agent.remainingDistance <= newDestinationDistance)
        {
            SetRandomDestination(controller, agent);
        }
    }

    private void SetRandomDestination(AIController controller, NavMeshAgent agent)
    {
        var randomPosition = GetRandomPosition(controller.transform.position);
        var agentPosition = GetAgentPosition(agent, randomPosition);

        agent.SetDestination(agentPosition);
    }

    private Vector3 GetRandomPosition(Vector3 offset)
    {
        var randomPosition = offset + Random.insideUnitSphere * roamRadius;
        return randomPosition;
    }

    private Vector3 GetAgentPosition(NavMeshAgent agent, Vector3 position)
    {
        NavMesh.SamplePosition(position, out var hit, roamRadius, agent.areaMask);
        return hit.position;
    }

}
