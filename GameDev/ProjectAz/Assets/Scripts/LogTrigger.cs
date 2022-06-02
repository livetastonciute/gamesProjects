using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTrigger : MonoBehaviour
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
        if (other.gameObject.tag == "Player")
        {
            //this is all I need to call the method
            GameObject go = GameObject.Find("first log");
            go.GetComponent<LogRoll>().startRolling();
            Debug.Log("The button clicked, raising the wall");

        }
    }
}
