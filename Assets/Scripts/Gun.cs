using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public Transform muzzle;
    public GameObject bullet;
    public int bulletCount = 6;
    public float spreadAmount = 12f;
    public float bulletVelocity = 20f;
    public float kickbackForce = 10f, kickbackUpForce = 2f;
    public float shootSpeed = 0.75f;
    public GameObject prefabToSpawnOnShoot;

    float tmrShoot;
    bool canShoot = true;
    Rope r;

    void Start() {
        Kickback();
        Invoke("Untrigger", 2);
        r = FindObjectOfType<Rope>();
    }

    void Untrigger() {
        GetComponent<Collider2D>().isTrigger = false;
    }

    void Update() {
        if (Input.GetMouseButton(0) && canShoot && !r.IsDisabled()) {
            Fire();
        }

        if (!canShoot) {
            tmrShoot += Time.deltaTime;
            if (tmrShoot >= shootSpeed) {
                canShoot = true;
                tmrShoot = 0;
            }
        }
    }

    void Fire() {
        canShoot = false;
        ScreenShake.instance.Shake();

        AudioSource sfx = GetComponent<AudioSource>();
        sfx.pitch = Random.Range(0.9f, 1.1f);
        sfx.Play();

        Instantiate(prefabToSpawnOnShoot, muzzle.position, Quaternion.identity);

        for (int i = 0; i < bulletCount; i++) {
            float spread = Random.Range(-spreadAmount, spreadAmount);
            GameObject b = Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation * Quaternion.Euler(0, 0, 90f + spread));
            b.GetComponent<Bullet>().owner = BulletOwner.Player;
            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            rb.AddForce(b.transform.right * bulletVelocity, ForceMode2D.Impulse);

            Kickback();
        }
    }

    void Kickback() {
        transform.parent.GetComponent<Rigidbody2D>().AddForceAtPosition(muzzle.right * kickbackForce, transform.position, ForceMode2D.Impulse);
        transform.parent.GetComponent<Rigidbody2D>().AddForceAtPosition(muzzle.up * kickbackUpForce, transform.position, ForceMode2D.Impulse);
    }
}
