using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttachGreenLvl2 : MonoBehaviour
{
    public GameObject Player;

    private PlayerSwitch skinNumber;

    Collider m_Collider;

    private void OnTriggerEnter(Collider other)
    {
        // getting player's info from gameobjecta
        skinNumber = Player.GetComponent<PlayerSwitch>();
        m_Collider = GetComponent<Collider>();


        if (other.gameObject == Player)
        {
            //Debug.Log(skinNumber.whichAvatarIsOn);
            if (skinNumber.whichAvatarIsOn == 2)
            {
                //m_Collider.enabled = false;
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
