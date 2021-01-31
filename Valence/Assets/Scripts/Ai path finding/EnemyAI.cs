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

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    public PlayerDetection enemyArea;
    public DeathDetection deathDetection;
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
        if (!enemyArea.playerInside || deathDetection.isDead == true)
        {
            if (seeker.IsDone())
                seeker.StartPath(rb.position, defaultPosition, OnPathComplete);
        } else
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

        if (grappableEntity.isGrappled)
        {
            spriteRenderer.sprite = grappledSprite;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
        } else
        {
            spriteRenderer.sprite = scoutingSprite;
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
