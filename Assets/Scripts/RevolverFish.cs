using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverFish : MonoBehaviour {
    public float shootSpeed = 0.5f;
    public GameObject bullet;
    public float spreadAmount, bulletVelocity;

    public GameObject muzzle;

    public float kickBackAngle = 20f, rotateSpeed = 4f;

    float tmrShoot;
    float targetRotation;
    Quaternion startRot;

    void Start() {
        float xScreenMiddle = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0)).x;

        if (transform.position.x < xScreenMiddle) {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180f));
            GetComponent<SpriteRenderer>().flipY = true;
            kickBackAngle = transform.rotation.eulerAngles.z + kickBackAngle;
        } else {
            kickBackAngle = 360 - kickBackAngle;
        }

        startRot = transform.rotation;
    }

    void Update() {
        tmrShoot += Time.deltaTime;
        if (tmrShoot >= shootSpeed) {
            GetComponent<AudioSource>().Play();

            float spread = Random.Range(-spreadAmount, spreadAmount);
            GameObject b = Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation * Quaternion.Euler(0, 0, -180f + spread));
            b.GetComponent<Bullet>().owner = BulletOwner.Fish;
            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            rb.AddForce(b.transform.right * bulletVelocity, ForceMode2D.Impulse);

            targetRotation = kickBackAngle;

            tmrShoot = 0;
        }

        if (Mathf.Approximately(transform.rotation.eulerAngles.z, targetRotation)) {
            targetRotation = startRot.eulerAngles.z;
        }

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(0, 0, targetRotation),
            Time.deltaTime * rotateSpeed
        );
    }
}
