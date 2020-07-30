using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public Rigidbody2D rb;
    public GameObject impactEffect;

	void Start()
	{
		StartCoroutine(BulletLife());
	}
	
	IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(1.2f);

        if (impactEffect)
		{
			Instantiate(impactEffect, transform.position, transform.rotation);
		}

		UnityEngine.Object.Destroy(gameObject);
    }
	
	
	
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")
        {

            if (impactEffect)
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
            }

            UnityEngine.Object.Destroy(gameObject);
        }

        if (col.gameObject.tag == "Enemy")
        {
            AITankController ai = col.gameObject.GetComponent<AITankController>();
            ai.currentHitPoints = ai.currentHitPoints - damage;
        }
    }

}
