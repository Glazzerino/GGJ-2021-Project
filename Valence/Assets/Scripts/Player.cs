﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    public Animator animator;

    public int RADIUS = 5;

    public float speed = 30f;
    public LineRenderer beamRenderer;
    public CharacterController2D controller;
    bool jump = false;

    public Transform beampoint;

    //variable stores nearest grappaple entity, if any
    GrappableEntity grappable = null;

    float horizontalInput = 0f;
    public float rotSpeedFactor = 2f;
    public bool isGrappled = false;
    public DeathDetection deathDetection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Debug.Log("Player script start");
    }

    // Update is called once per frame
    void Update() {

        
            //ANIMATION CONTROL

            if (!controller.isGrounded())
            {
                animator.SetBool("Jump", true);
            }
            else
            {
                animator.SetBool("Jump", false);
            }

            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

            horizontalInput = Input.GetAxisRaw("Horizontal") * speed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("Jump", true);
            }
            // BEHAVIOR

            // keydown event. keeps reads and writes low

            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                
                grappable = GetClosestGrappable();
                if (grappable != null) {
                    
                    if (Vector2.Distance(transform.position, grappable.transform.position) < RADIUS) {
                           beamRenderer.enabled = true;
                           grappable.SetGrappled(true);
                           
                           isGrappled = true; 
                    }
                }
                
            }

            if (Input.GetKey(KeyCode.LeftShift) && grappable != null && isGrappled) {
                rb.velocity = new Vector2(0f, 0f);
                rb.gravityScale = 0;
                
                beamRenderer.SetPosition(0, beampoint.position);
                beamRenderer.SetPosition(1, grappable.gameObject.transform.position);  
                  
                

            }
           

            
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                beamRenderer.enabled = false;
                rb.gravityScale = 2.5f;
                isGrappled = false;
                this.gameObject.transform.rotation = Quaternion.identity;
                
                grappable.SetGrappled(false);
            }
    }

    void FixedUpdate() {
        if (!isGrappled) {
            controller.Move((horizontalInput * Time.fixedDeltaTime), false, jump);
        } else {
            transform.RotateAround(grappable.gameObject.transform.position, Vector3.forward, horizontalInput * Time.fixedDeltaTime * rotSpeedFactor);
        }
        
            jump = false;
        if (isGrappled) {
            //NearCircularEdge(grappable);
        }
        
    }

    GrappableEntity GetClosestGrappable() {
        Vector3 pos = this.gameObject.transform.position;
        float dist = float.PositiveInfinity;
        GrappableEntity target = null;

        foreach (GrappableEntity go in GrappableEntity.Entities) {
            if ((go.gameObject.transform.position - pos).sqrMagnitude < dist) { 
                dist = (go.gameObject.transform.position - pos).sqrMagnitude;
                target = go;
            }
        }
        return target;
    }

    void DrawBeam(GameObject target) {
        Debug.Log("Beam");
        beamRenderer.enabled = true;
        beamRenderer.SetPosition(0, beampoint.position);
        beamRenderer.SetPosition(1, target.transform.position);
    }

    //void Grapple(GameObject target) {
    //    float y_delta = target.transform.position.y - this.transform.position.y;
    //    Vector2 distance = (target.transform.position - this.transform.position);

    //    float angle = Vector2.Angle(target.transform.position, this.transform.position);
        
    //}

    //void NearCircularEdge(GameObject target) {
    //    float angle = Vector2.Angle(this.transform.position, target.transform.position);
    //    float distance = (target.transform.position - this.gameObject.transform.position).sqrMagnitude - RADIUS;
    //    float x_component = distance * Mathf.Sin(angle);
    //    float y_component = distance * Mathf.Cos(angle);
    //    float x = this.gameObject.transform.position.x;
    //    float y = this.gameObject.transform.position.y;


    //    this.transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, 10f * Time.fixedDeltaTime);
        
    //}

    public void OnLanding() {
        animator.SetBool("Jump", false);
        //Debug.Log("Landed");
    }
}


