using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurretController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int rotationOffset = 0;    // This will offset the rotation of the object as it tracks the mouse. This is required to correctly set rotation values
    public int rotationSpeed = 200;
    public int maxAmmo = 1;
    private int currentAmmo;
    public float reloadTime = 4.0f;
    public float bulletVelocity = 8.0f;
    public int bulletDamage = 10;
    private bool isReloading = false;
    private bool isPlayerWithinDistance;
    private GameObject playerToFollow;
    public bool dummyAI = false;
    public LayerMask myLayerMask;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void FixedUpdate()
    {
        if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }

        if (!dummyAI)
        {
            if (isPlayerWithinDistance)
            {
                RaycastHit2D hit = Physics2D.Linecast(transform.position, playerToFollow.transform.position, myLayerMask);

                if (hit)
                {
                    float distanceToPlayer = Vector2.Distance(playerToFollow.transform.position, transform.position);

                    if (hit.collider.gameObject.tag == "Player" && distanceToPlayer < 9)
                    {
                        Vector3 difference = playerToFollow.transform.position - transform.position; // This will calculate the distance between the mouse in the game and the position of the tank turret
                        difference.Normalize();    // This returns simplified values which makes it easier to work with


                        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;    // This calculates the angle between the mouse and the turret by using the values derives from the difference calculation.

                        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, angle + rotationOffset), rotationSpeed * Time.deltaTime); // This will rotate the turret towards the calculated angle over time. Tweaking the multiplication value will state how quickly or slowly it will rotate.

                        if (!isReloading && Time.timeScale == 1)
                        {
                            Shoot();
                        }
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            playerToFollow = hitInfo.gameObject;
            isPlayerWithinDistance = true;
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            isPlayerWithinDistance = false;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        currentAmmo--;

        //Shooting Logic
        GameObject activeBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as GameObject;

        Rigidbody2D rb = activeBullet.GetComponent<Rigidbody2D>();
        rb.velocity = activeBullet.transform.right * bulletVelocity;
        EnemyBullet script = activeBullet.GetComponent<EnemyBullet>();
        script.damage = bulletDamage;
    }
}
