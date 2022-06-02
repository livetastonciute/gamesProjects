using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRoll : MonoBehaviour
{
    public float speed = 1f;
    public float timeToEnd = 1f;
    public Transform target;
    private bool start = false;

    private void Start()
    {
    }

    void Update()
    {
        if (start == true)
        {
            float step = speed * Time.deltaTime;
            //transform.Translate (0,Time.deltaTime,0,Space.World);
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }

    public void startRolling()
    {
        start = true;
    }
}
