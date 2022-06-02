using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAttach : MonoBehaviour
{
    public GameObject Player;

    private PlayerSwitch skinNumber;
    private ThridPersonMovement vel;
    private Vector3 velocity;
    float prevSpeed;

    private void OnTriggerEnter(Collider other)
    {
        // getting player's info from gameobjecta
        skinNumber = Player.GetComponent<PlayerSwitch>();

        //var animator = GetComponent<Animator>();

        if (other.gameObject == Player)
        {
            //Debug.Log(skinNumber.whichAvatarIsOn);
            if (skinNumber.whichAvatarIsOn == 2)
            {
                Player.transform.parent = transform;
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;

            //var animator = GetComponent<Animator>();
            //animator.speed = 1;
        }
    }
}
