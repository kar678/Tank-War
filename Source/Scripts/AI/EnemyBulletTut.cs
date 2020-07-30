using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletTut : MonoBehaviour
{
    public int damage = 10;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    Task3 taskObjective;

    void Start()
    {
        StartCoroutine(BulletLife());
        taskObjective = GameObject.FindGameObjectWithTag("Task3").GetComponent<Task3>();
    }

    IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(1.2f);

        if (impactEffect)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }

        taskObjective.bulletsDodged++;
        UnityEngine.Object.Destroy(gameObject);
    }



    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Enemy")
        {

            if (impactEffect)
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
            }

            taskObjective.bulletsDodged++;
            UnityEngine.Object.Destroy(gameObject);
        }

        if (col.gameObject.tag == "Player")
        {
            TankController player = col.gameObject.GetComponent<TankController>();
            player.TakeDamage(damage);
        }
    }
}
