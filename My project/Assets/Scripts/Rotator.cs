using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 50f, 0f) * Time.deltaTime, Space.Self);
        transform.Rotate(new Vector3(0f, 70f, 0f) * Time.deltaTime, Space.World);


    }
}
