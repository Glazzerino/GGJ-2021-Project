using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grappleReactionTest : MonoBehaviour
{
    public GrappableEntity grappableEntity;   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if (grappableEntity.GetGrappled()) {
            Debug.Log(grappableEntity.GetGrappled());
        }   
    }
}
