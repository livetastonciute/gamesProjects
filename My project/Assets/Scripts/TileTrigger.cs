using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTrigger : MonoBehaviour
{

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("White"))
        {
            Debug.Log("Saugu");
        }
        if (other.gameObject.CompareTag("Red"))
        {
            Debug.Log("Mirtis");
           // gameObject.SetActive(false);
          //  Destroy(gameObject);
          //  Instantiate(gameObject, new Vector3(0, 1, 0), transform.rotation);
        }
    }
}
