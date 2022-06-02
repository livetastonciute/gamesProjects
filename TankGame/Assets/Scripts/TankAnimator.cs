using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TankAnimator : MonoBehaviour
{
    [SerializeField]
    private string hoveringParameter = "Hovering";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetHovering()
    {
        animator.SetBool(hoveringParameter, true);
    }

    public void SetDriving()
    {
        animator.SetBool(hoveringParameter, false);
    }
}
