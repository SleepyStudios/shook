using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float magnitude = 1f, speed = 0f;

    Vector2 initialPos;
    Rigidbody2D rb;

    void Start()
    {
        initialPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rb.MovePosition(initialPos + new Vector2(0f, magnitude*Mathf.Sin(Time.time * speed)));
    }
}
