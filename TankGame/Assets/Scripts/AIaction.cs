using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIaction : ScriptableObject
{
    public abstract void UpdateActionGizmos(AIController controller);

    public abstract void UpdateAction(AIController controller);
}
