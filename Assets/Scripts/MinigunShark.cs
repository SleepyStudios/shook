using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunShark : MonoBehaviour {
    public float aggroRange = 20f;
    public float shootSpeed = 0.5f;
    public GameObject bullet;

    public int bulletCount;
    public float spreadAmount, bulletVelocity;

    public GameObject muzzle;

    Rope r;
    float tmrShoot;
    bool hasAggro;

    void Start() {
        r = FindObjectOfType<Rope>();

        float xScreenMiddle = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0)).x;

        if (transform.position.x >= xScreenMiddle) {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-50, 50)));
        } else {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-220, -120)));
            GetComponent<SpriteRenderer>().flipY = true;
        }
    }

    void Update() {
        float distance = Vector2.Distance(r.transform.position, transform.position);
        if (distance <= aggroRange) {
            hasAggro = true;
        } else {
            hasAggro = false;
        }

        if (hasAggro) {
            tmrShoot += Time.deltaTime;
            if (tmrShoot >= shootSpeed) {
                Fire();
                tmrShoot = 0;
            }
        }
    }

    void Fire() {
        GetComponent<AudioSource>().Play();

        for (int i = 0; i < bulletCount; i++) {
            float spread = Random.Range(-spreadAmount, spreadAmount);
            GameObject b = Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation * Quaternion.Euler(0, 0, -180f + spread));
            b.GetComponent<Bullet>().owner = BulletOwner.Fish;
            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            rb.AddForce(b.transform.right * bulletVelocity, ForceMode2D.Impulse);
        }
    }
}
