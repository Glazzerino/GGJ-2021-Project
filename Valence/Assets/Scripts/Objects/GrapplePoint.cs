using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    public GrappableEntity grappableEntity;
    public Animator animator;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        animator.SetBool("Grappled", grappableEntity.isGrappled());
    }
}
