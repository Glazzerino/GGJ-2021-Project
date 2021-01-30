using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // Start is called before the first frame update
    GameObject[] grappables;
    Vector3 grappable_position_holder;
    public CharacterController2D controller;
    float horizontalInput = 0f;
    public float speed = 40f;

    private Rigidbody2D rb;

    //Dummy radius val
    float grapple_radius = 2f;

    bool isGrappled = false;

    private SpriteRenderer renderer;
    bool jump = false;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        Debug.Log("Character script start");
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        GameObject closest = GetClosest("Grappable");
        if (closest != null) {
            //Debug.Log("Found grappable with position: " + closest.transform.position.x + " " + closest.transform.position.y);
        }

       if (Input.GetKeyDown(KeyCode.LeftShift)) {
            isGrappled = true;

            rb.velocity = new Vector2(0f, 0f);
            //renderer.color = Color.blue;
            rb.gravityScale = 0;
       } 

       if (Input.GetKeyUp(KeyCode.LeftShift)) {
            //rb.velocity = new Vector2(0f, 0f);
            //renderer.color = Color.white;

            rb.gravityScale = 2;
       }

       //if (isGrappled) {
       //     GameObject grappleTarget = GetClosest("Grappable");
       //     if (grappleTarget != null) {
       //         if ((grappleTarget.transform.position - gameObject.transform.position).sqrMagnitude != grapple_radius) {
                    
                    
       //         }
       //     }
       // }
    }
    void FixedUpdate()
    {
        controller.Move((horizontalInput * Time.fixedDeltaTime), false, jump);
        jump = false;
    }

    //tag represents the tag of gameobjects to look for
    GameObject GetClosest(string tag)
    {
        grappables = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        bool started = false;
        foreach (GameObject go in grappables) {

            if (!started) {
                closest = go;
                started = true;
                continue;
            }

            grappable_position_holder = go.transform.position;
            Vector2 diff = grappable_position_holder - this.gameObject.transform.position;
            if ((closest.transform.position - this.gameObject.transform.position).sqrMagnitude > diff.sqrMagnitude) {
                closest = go;
            }
        }

        return closest;
    }
}

