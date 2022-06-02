using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    float m_Speed;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Speed = 2f;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, 0, z);
        transform.Translate(movement * m_Speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Rigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
        
     
    }

}
