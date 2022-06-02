using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField]
    private List<BallSpawner> ballSpawner;
    private Animator myAnimation;
    public bool locked = true;

    private void OnDestroy()
    {
        foreach (var bs in ballSpawner)
        {
            bs.spawn = true;
            
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
