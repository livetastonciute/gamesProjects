using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public GameObject Character;


    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey("a") || Input.GetKey("left") ||
             Input.GetKey("s") || Input.GetKey("down") ||
             Input.GetKey("w") || Input.GetKey("up") ||
             Input.GetKey("d") || Input.GetKey("right"))
             && !Input.GetKey("space"))
        {
            Character.GetComponent<Animator>().Play("WalkingCycle");
        }

        if (Input.GetKey("space"))
        {
            Character.GetComponent<Animator>().Play("Jump");
        }

        if (!Input.GetKey("a") && !Input.GetKey("left") &&
            !Input.GetKey("s") && !Input.GetKey("down") &&
            !Input.GetKey("w") && !Input.GetKey("up") &&
            !Input.GetKey("d") && !Input.GetKey("right") &&
            !Input.GetKey("space"))
        {
            Character.GetComponent<Animator>().Play("Standing");
        }
    }
}
