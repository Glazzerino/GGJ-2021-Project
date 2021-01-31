using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    private Vector3 defaultPosition;

    private Vector3 targetPos;
    private Vector3 thisPos;
    public float offset;
    private float angle;


    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    public PlayerDetection enemyArea;
    GrappableEntity grappableEntity;

    public Sprite grappledSprite;
    public Sprite scoutingSprite;
    public Sprite seekingSprite;

    Seeker seeker;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        grappableEntity = GetComponent<GrappableEntity>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        defaultPosition = transform.position;

    }

    void UpdatePath()
    {       
        if (!enemyArea.playerInside)
        {
            if (seeker.IsDone())
                seeker.StartPath(rb.position, defaultPosition, OnPathComplete);
        }
        else
        {
            if (seeker.IsDone())
                seeker.StartPath(rb.position, target.position, OnPathComplete);
        }

    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (grappableEntity.grappled)
        {
            
            spriteRenderer.sprite = grappledSprite;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
        } else
        {

            if (!enemyArea.playerInside)
            {
                spriteRenderer.sprite = scoutingSprite;
            } else
            {
                spriteRenderer.sprite = seekingSprite;
                targetPos = target.position;
                thisPos = transform.position;
                targetPos.x = targetPos.x - thisPos.x;
                targetPos.y = targetPos.y - thisPos.y;
                angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
            }

            if (path == null)
                return;
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            rb.AddForce(force);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }
    
    }
}
