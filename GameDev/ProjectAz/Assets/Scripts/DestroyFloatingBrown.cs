using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFloatingBrown : MonoBehaviour
{
    public GameObject obj;

    public GameObject Player;

    private PlayerSwitch skinNumber;

    Collider m_Collider;

    void OnTriggerEnter(Collider other)
    {
        skinNumber = Player.GetComponent<PlayerSwitch>();

        if (other.tag == "Player" && skinNumber.whichAvatarIsOn == 1)
        {
            Destroy(obj);
            m_Collider.enabled = false;
        }
    }
}
