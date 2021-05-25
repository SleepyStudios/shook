using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Explosion : MonoBehaviour {
    private HashSet<Collider2D> colliders = new HashSet<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Rope") && collision.GetComponent<Bone>().visible) colliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Rope")) colliders.Remove(collision);
    }

    void GoBoom() {
        ScreenShake.instance.Shake(0.3f, 4f, 3f);
        GetComponent<AudioSource>().Play();
        if (colliders.Count > 0) FindObjectOfType<Rope>().OnHit(); 
    }

    void OnAnimFinished() {
        Destroy(gameObject);
    }
}
