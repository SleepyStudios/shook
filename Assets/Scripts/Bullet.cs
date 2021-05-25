using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletOwner {
    Player,
    Fish
}

public class Bullet : MonoBehaviour {
    public BulletOwner owner;

    private void OnBecameInvisible() {
        Destroy(gameObject, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Fish") && owner == BulletOwner.Player) {
            collision.GetComponent<FishBase>().OnDeath();

            var sfx = GetComponent<AudioSource>();
            sfx.pitch = Random.Range(0.9f, 1.1f);
            sfx.Play();

            Destroy(gameObject, 0.35f);
        } else if (collision.CompareTag("Rope") && owner == BulletOwner.Fish) {
            if (collision.GetComponent<Bone>().visible) {
                FindObjectOfType<Rope>().OnHit();
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Cage") && owner == BulletOwner.Fish) {
            collision.gameObject.GetComponent<AudioSource>().Play();
            owner = BulletOwner.Player;
        }
    }
}
