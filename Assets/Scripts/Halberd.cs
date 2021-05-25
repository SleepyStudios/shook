using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halberd : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Fish") && !collision.GetComponent<FishBase>().isCaught) {
            collision.GetComponent<FishBase>().OnDeath();
            ScreenShake.instance.Shake();
            PlaySound();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bullet")) {
            ScreenShake.instance.Shake();
            PlaySound();

            Destroy(collision.gameObject);
        }
    }

    void PlaySound() {
        AudioSource sfx = GetComponent<AudioSource>();
        sfx.pitch = Random.Range(0.7f, 0.9f);
        sfx.Play();
    }
}
