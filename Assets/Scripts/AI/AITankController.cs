using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AITankController : MonoBehaviour
{
    Transform target;

    public float speed = 200;
    public float nextWaypointDistance = 3f;
	public float rotationSpeed = 200;
	public int rotationOffset = 0;
	public float maxHitPoints = 100;
    public float currentHitPoints = 100;
    public int aiLogicNum = 1;

    Path path;
    int currentWaypoint;
    bool reachedEndOfPath;
	bool isPlayerWithinDistance = false;
    bool blowingUp = false;
	GameObject playerToFollow;
    bool destroyingEnemy = false;
    public LayerMask myLayerMask;
    public GameObject explosionEffect;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        if(aiLogicNum == 1 || aiLogicNum == 2)
        {
            playerToFollow = GameObject.FindGameObjectWithTag("Player");
            target = playerToFollow.transform;
        }
    }

    void UpdatePath()
    {
        if(seeker.IsDone() && aiLogicNum != 0)
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        if (currentHitPoints <= 0 && !blowingUp)
        {
            StartCoroutine(BlowUp());
            destroyingEnemy = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
			return;

        switch (aiLogicNum)
        {
            case 0:
                break;
            case 1:
                if (isPlayerWithinDistance && !destroyingEnemy)
                {

                    RaycastHit2D hit = Physics2D.Linecast(transform.position, playerToFollow.transform.position, myLayerMask);

                    if (hit)
                    {
                        float distanceToPlayer2 = Vector2.Distance(playerToFollow.transform.position, transform.position);

                        if (hit.collider.gameObject.tag != "Player" || distanceToPlayer2 > 6)
                        {
                            if (currentWaypoint >= path.vectorPath.Count)
                            {
                                reachedEndOfPath = true;
                                return;
                            }
                            else
                            {
                                reachedEndOfPath = false;
                            }

                            if (path != null)
                            {
                                Vector3 difference = path.vectorPath[currentWaypoint] - transform.position; // This will calculate the distance between the mouse in the game and the position of the tank turret
                                difference.Normalize();    // This returns simplified values which makes it easier to work with


                                float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;    // This calculates the angle between the mouse and the turret by using the values derives from the difference calculation.

                                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, angle + rotationOffset), rotationSpeed * Time.deltaTime); // This will rotate the turret towards the calculated angle over time. Tweaking the multiplication value will state how quickly or slowly it will rotate.
                            }

                            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                            Vector2 force = direction * speed * Time.deltaTime;

                            rb.AddForce(force);

                            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

                            if (distance < nextWaypointDistance)
                            {
                                currentWaypoint++;
                            }
                        }
                    }

                }
                break;
            case 2:
                float distanceToPlayer = Vector2.Distance(playerToFollow.transform.position, transform.position);
                RaycastHit2D hit2 = Physics2D.Linecast(transform.position, playerToFollow.transform.position, myLayerMask);

                if (distanceToPlayer > 6 || hit2.collider.gameObject.tag != "Player" && !destroyingEnemy)
                {
                    if (currentWaypoint >= path.vectorPath.Count)
                    {
                        reachedEndOfPath = true;
                        return;
                    }
                    else
                    {
                        reachedEndOfPath = false;
                    }

                    if (path != null)
                    {
                        Vector3 difference = path.vectorPath[currentWaypoint] - transform.position; // This will calculate the distance between the mouse in the game and the position of the tank turret
                        difference.Normalize();    // This returns simplified values which makes it easier to work with


                        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;    // This calculates the angle between the mouse and the turret by using the values derives from the difference calculation.

                        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, angle + rotationOffset), rotationSpeed * Time.deltaTime); // This will rotate the turret towards the calculated angle over time. Tweaking the multiplication value will state how quickly or slowly it will rotate.
                    }

                    Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                    Vector2 force = direction * speed * Time.deltaTime;

                    rb.AddForce(force);

                    float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

                    if (distance < nextWaypointDistance)
                    {
                        currentWaypoint++;
                    }
                }
                break;
            default:
                Debug.Log("No Logic ID passed");
                break;
        }
    }

    IEnumerator BlowUp()
    {
        blowingUp = true;

        if (explosionEffect)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        yield return new WaitForSeconds(1);
        UnityEngine.Object.Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player" && aiLogicNum == 1)
        {
            playerToFollow = hitInfo.gameObject;
            isPlayerWithinDistance = true;
            target = hitInfo.gameObject.transform;
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player" && aiLogicNum == 1)
        {
            isPlayerWithinDistance = false;
        }
    }

    public void TakeDamage(float Damage)
    {
        currentHitPoints = (Damage > 0) ? currentHitPoints - Damage : 0;
    }
}
