using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankWeaponController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private InGameUIController playerHUD;
	private PlayerStats stats;
    public int maxAmmo = 1;
    private int currentAmmo;
    public float reloadTime = 4.0f;
    public float reloadTimeLeft;
    public float bulletVelocity = 8.0f;
    public int bulletDamage = 10;
    private bool isReloading = false;
    public bool disableControls = false;
    public bool inGarage = false;
    public float timeBetweenShots = 1.0f;
    private float timeSinceLastShot;
    AudioSource sourceSound;
    public AudioClip weaponSound;
    public float soundPitch = 1.0f;

    void Start()
    {
        if (!inGarage)
        {
            currentAmmo = maxAmmo;

            playerHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameUIController>();
            stats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        }

        sourceSound = GetComponent<AudioSource>();
        sourceSound.clip = weaponSound;
        sourceSound.pitch = soundPitch;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAmmo <= 0 && !isReloading && !disableControls)
        {
            StartCoroutine(Reload());
        }

        if(isReloading)
        {
            reloadTimeLeft -= Time.deltaTime;
            playerHUD.reloadText.text = string.Format("{0}: {1}", "Reloading", (reloadTimeLeft).ToString("0.0"));
        }

        if(Input.GetButtonDown("Fire1") && !isReloading && Time.timeScale == 1 && !disableControls && timeSinceLastShot < Time.time)
        {
            Shoot();
            sourceSound.Play();
            timeSinceLastShot = Time.time + timeBetweenShots;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        reloadTimeLeft = reloadTime;
        playerHUD.SetReloading(true);
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        playerHUD.SetReloading(false);
    }

    void Shoot()
    {
		stats.shotsFired++;
		
        currentAmmo--;

        //Shooting Logic
        GameObject activeBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as GameObject;

        Rigidbody2D rb = activeBullet.GetComponent<Rigidbody2D>();
        rb.velocity = activeBullet.transform.right * bulletVelocity;
        Bullet script = activeBullet.GetComponent<Bullet>();
        script.damage = bulletDamage;
    }
}
