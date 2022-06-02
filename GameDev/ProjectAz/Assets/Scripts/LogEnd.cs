using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        var otherGameObject = other.gameObject;
        if (isLog(otherGameObject))
        {
            otherGameObject.SetActive(false);
            Destroy(otherGameObject);
        }
    }

    private bool isLog(GameObject obj)
    {
        return obj.CompareTag("DmgLog");
    }
   
}
