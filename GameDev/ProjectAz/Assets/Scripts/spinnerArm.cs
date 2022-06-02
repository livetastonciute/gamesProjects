using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinnerArm : MonoBehaviour
{
    [SerializeField]
    private string playertag = "Player";
    [SerializeField]
    private int force = 200;
    CharacterController character;

    private void Awake()
    {
        character = FindObjectOfType<CharacterController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == playertag)
        {
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
