using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeFish : MonoBehaviour {
    public float speed = 2f;
    public float minTimeBetweenAttacks = 0.5f, maxTimeBetweenAttacks = 2f;
    public float explodeDistance;
    public GameObject explosionPrefab;
    public float stopOffset = 5f;

    Rigidbody2D rb;
    Rope r;
    bool isExploding, attacked;
    SpriteRenderer sr;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        r = FindObjectOfType<Rope>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Attack() {
        Vector3 ropePosOffset = r.transform.position;
        ropePosOffset.y -= stopOffset;

        Vector2 dir = ropePosOffset - transform.position;

        rb.AddForce(dir.normalized * speed);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 180f, Vector3.forward);
    }

    void Update() {
        if (sr.isVisible) {
            Attack();
        }

        if (Vector2.Distance(transform.position, r.transform.position) <= explodeDistance && !isExploding) {
            Explode();
        }
    }

    void Explode() {
        rb.velocity = Vector2.zero;
        isExploding = true;
        GetComponent<Animator>().SetTrigger("Explode");
    }

    public void SpawnExplosion() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
