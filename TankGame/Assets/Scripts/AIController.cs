using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(TankController))]
[RequireComponent(typeof(ProjectileController))]
[RequireComponent(typeof(NavMeshAgent))]

public class AIController : MonoBehaviour
{
    [SerializeField]
    private AIState initialState;

    private ProjectileController projectileController;
    private TankController tankController;
    private NavMeshAgent navMeshAgent;

    private AIState currentAIState;
    private List<Player> players;

    private AIState CurrentAIState
    {
        get
        {
            if(currentAIState == null)
            {
                currentAIState = initialState;
            }

            return currentAIState;
        }
    }

    public NavMeshAgent NavMeshAgent
    {
        get
        {
            if(navMeshAgent == null)
            {
                SetupNavMeshAgent();
            }

            return navMeshAgent;
        }
    }

    public float NextFireTime { get; set; }
    public Vector2 TargetAxis { get; set; }

    public IReadOnlyList<Player> Players => players;

    private void OnDrawGizmos()
    {
        CurrentAIState.UpdateStateGizmos(this);
    }

    private void Awake()
    {
        projectileController = GetComponent<ProjectileController>();
        tankController = GetComponent<TankController>();
        players = FindObjectsOfType<Player>().ToList();
    }

    private void FixedUpdate()
    {
        CurrentAIState.UpdateState(this);
        tankController.Move(TargetAxis);
        UpdatePlayers();
    }

    public void ChangeState(AIState newAIState)
    {
        currentAIState = newAIState;
    }

    public void Fire()
    {
        //infinite ammo
        projectileController.UpdateAmmo(1);
        projectileController.Fire();
    }

    private void SetupNavMeshAgent()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void UpdatePlayers()
    {
        for(var index = players.Count - 1; index >= 0; index--)
        {
            var player = players[index];
            if (!player.enabled)
            {
                players.Remove(player);
            }
        }
    }


}
