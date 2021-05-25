using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catchable : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Cage")) {
            transform.parent = collision.transform.Find("Attach Point");
            float attachOffset = 0.25f;
            transform.localPosition = new Vector2(-Random.Range(-attachOffset, attachOffset), Random.Range(-attachOffset, attachOffset));

            HandleOnCaught();
        }
    }

    void HandleOnCaught() {
        SendMessage("OnCaught");
        GetComponent<AudioSource>().Play();
    }
}
