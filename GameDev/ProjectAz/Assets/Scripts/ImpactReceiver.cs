using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ImpactReceiver : MonoBehaviour
{
    [SerializeField]
    private static float mass = 3.0f;
    private static Vector3 impact = Vector3.zero;
    private CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(impact.magnitude > 0.2f)
        {
            character.Move(impact * Time.deltaTime);
        }
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }


    public static void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0)
        {
            //dir.y = -dir.y;
        }
        impact += dir.normalized * force / mass;
    }
}
