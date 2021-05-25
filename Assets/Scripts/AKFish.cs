using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKFish : MonoBehaviour {
    public float aggroRange = 8f;
    public float shootSpeed = 0.5f;
    public GameObject bullet;
    public GameObject muzzle;
    public int bulletCount = 3;
    public float spreadAmount = 4f, bulletVelocity = 10f;
    public float nextPosInterval = 4f;
    public float speed = 3f;

    Rope r;
    GameObject cage;
    float tmrShoot, tmrNextPos;
    bool hasAggro, canShoot;
    SpriteRenderer sr;
    Rigidbody2D rb;
    bool visible;

    void Start() {
        r = FindObjectOfType<Rope>();
        cage = GameObject.Find("Cage");
        nextPosInterval += bulletCount * shootSpeed;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        float xScreenMiddle = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0)).x;

        if (transform.position.x < xScreenMiddle) {
            GetComponent<SpriteRenderer>().flipY = true;
        }
    }

    void OnBecameVisible() {
        if (Vector2.Distance(transform.position, r.transform.position) <= aggroRange) {
            hasAggro = true;
        }
        visible = true;
    }

    void OnBecameInvisible() {
        hasAggro = false;
        canShoot = false;
        visible = false;
    }

    void Update() {
        if (visible && !hasAggro) {
            if (Vector2.Distance(transform.position, r.transform.position) <= aggroRange) {
                hasAggro = true;
            }
        }

        Vector3 dir = cage.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 180f, Vector3.forward);

        if (hasAggro) {
            if (transform.position.y > cage.transform.position.y) {
                rb.AddForce(Vector2.down * speed);
            }
        }

        if (canShoot) {
            tmrShoot += Time.deltaTime;
            if (tmrShoot >= shootSpeed) {
                for (int i = 0; i < bulletCount; i++) {
                    Invoke("Fire", shootSpeed * i);
                }
                canShoot = false;
                tmrShoot = 0;
            }
        }

        if (!canShoot && hasAggro) {
            tmrNextPos += Time.deltaTime;
            if (tmrNextPos >= nextPosInterval) {
                canShoot = true;
                tmrNextPos = 0;
            }
        }
    }

    void Fire() {
        GetComponent<AudioSource>().Play();

        float spread = Random.Range(-spreadAmount, spreadAmount);
        GameObject b = Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation * Quaternion.Euler(0, 0, -180f + spread));
        b.GetComponent<Bullet>().owner = BulletOwner.Fish;
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.AddForce(b.transform.right * bulletVelocity, ForceMode2D.Impulse);
    }
}
