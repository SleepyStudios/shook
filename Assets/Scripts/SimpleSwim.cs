using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSwim : MonoBehaviour {
    public float minSpeed = 0.5f, maxSpeed = 2f;
    public float frequency = 20.0f;  // Speed of sine movement
    public float magnitude = 0.5f;   // Size of sine movement
    public float rotation = 10f, rotateSpeed = 2f;

    Vector3 pos;
    int direction = 0;

    SpriteRenderer sr;
    Rigidbody2D rb;

    bool caught;

    float speed;

    void Start() {
        pos = transform.position;

        if (Random.Range(0, 2) == 0) {
            direction = -1;
        } else {
            direction = 1;
        }

        sr = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();

        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update() {
        if (caught) return;

        pos += transform.right * direction * Time.deltaTime * speed;

        float sine = Mathf.Sin(Time.time * frequency) * magnitude;
        rb.MovePosition(pos + transform.up * sine);

        float flipMultiplier = sr.flipX ? -1 : 1;

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.AngleAxis(Mathf.Sign(sine) * rotation * flipMultiplier, Vector3.forward),
            Time.deltaTime * rotateSpeed
        );

        if (transform.position.x <= Camera.main.ScreenToWorldPoint(Vector2.zero).x) {
            direction = 1;
        } else if (transform.position.x >= Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x) {
            direction = -1;
        }

        sr.flipX = direction == 1;
    }

    void OnCaught() {
        caught = true;
        GetComponent<TrailRenderer>().emitting = false;
    }
}
