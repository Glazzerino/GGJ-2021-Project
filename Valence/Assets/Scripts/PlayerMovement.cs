using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;

    public float speed = 30f;
    public CharacterController2D controller;
    bool jump = false;
    GameObject grappable = null;
    float horizontalInput = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Player script start");
    }

    // Update is called once per frame
    void Update() {
        grappable = GetClosestGrappable();
        horizontalInput = Input.GetAxisRaw("Horizontal") *speed;
        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            rb.velocity = new Vector2(0f, 0f);
            rb.gravityScale = 0;
        }
         else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            rb.gravityScale = 2;
        }
        
        if (grappable != null)
        {
            //Debug.Log(grappable.transform.position);
            Grapple(grappable);
        }
    }

    void FixedUpdate() {
        controller.Move((horizontalInput * Time.fixedDeltaTime), false, jump);
        jump = false;
    }

    GameObject GetClosestGrappable() {
        Vector3 pos = this.gameObject.transform.position;
        float dist = float.PositiveInfinity;
        GameObject target = null;

        foreach (GrappableEntity go in GrappableEntity.Entities) {
            if ((go.gameObject.transform.position - pos).sqrMagnitude < dist) { 
                dist = (go.gameObject.transform.position - pos).sqrMagnitude;
                target = go.gameObject;
            }
        }
        return target;
    }

    void Grapple(GameObject target) {
        float y_delta = target.transform.position.y - this.transform.position.y;
        Vector2 hypothenuse = (target.transform.position - this.transform.position);

        //while(hypothenuse != 3f) {

        //}
        float angle = Vector2.Angle(target.transform.position, this.transform.position);

        Debug.Log(angle);
    }
}


