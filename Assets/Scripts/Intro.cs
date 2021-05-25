using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {
    public float speed = 3f;

    void Update() {
        transform.position = Vector3.Lerp(transform.position, Vector3.zero, Time.deltaTime * speed);
    }
}
