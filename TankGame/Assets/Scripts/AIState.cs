using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIState", menuName = "Lec_11/AI State")]
public class AIState : ScriptableObject
{
    [SerializeField]
    private List<AIaction> actions;

    public void UpdateStateGizmos(AIController controller)
    {
        foreach(var action in actions)
        {
            action.UpdateActionGizmos(controller);
        }
    }

    public void UpdateState(AIController controller)
    {
        foreach (var action in actions)
        {
            action.UpdateAction(controller);
        }
    }
}
