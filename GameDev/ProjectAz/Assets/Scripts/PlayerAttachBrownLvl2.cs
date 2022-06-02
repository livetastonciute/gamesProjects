using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttachBrownLvl2 : MonoBehaviour
{
    public GameObject Player;

    private PlayerSwitch skinNumber;

    Collider m_Collider;

    private void OnTriggerEnter(Collider other)
    {
        // getting player's info from gameobject
        skinNumber = Player.GetComponent<PlayerSwitch>();
        m_Collider = GetComponent<Collider>();

        if (other.gameObject == Player)
        {
            //Debug.Log(skinNumber.whichAvatarIsOn);
            if (skinNumber.whichAvatarIsOn == 1)
            {
                Debug.Log(skinNumber.whichAvatarIsOn);
                Player.GetComponent<ThridPersonMovement>().ResetScene();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_Collider = GetComponent<Collider>();

        if (other.gameObject == Player)
        {
            m_Collider.enabled = true;
        }
    }
}
