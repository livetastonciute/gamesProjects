using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavyWallBrown : MonoBehaviour
{
    public GameObject Player;
    private PlayerSwitch skinNumber;

    // Start is called before the first frame update
    void Start()
    {
        var animator = GetComponent<Animator>();
        animator.speed = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        var animator = GetComponent<Animator>();
        skinNumber = Player.GetComponent<PlayerSwitch>();

        if (other.gameObject == Player)
        {
            //Debug.Log(skinNumber.whichAvatarIsOn);
            if (skinNumber.whichAvatarIsOn == 2)
            {
                animator.speed = 1;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var animator = GetComponent<Animator>();
        animator.speed = 0;
    }
}
