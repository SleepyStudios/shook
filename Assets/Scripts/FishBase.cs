using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBase : MonoBehaviour {
    public float destroyIfOverDistance = 40f;
    public GameObject deathPrefab;
    public FishData data;
    public bool isCaught;

    Rope r;

    void Start() {
        r = FindObjectOfType<Rope>();
    }

    public void OnDeath() {
        Destroy(gameObject);
        InitDeathParticles();
    }

    void Update() {
        if (Vector2.Distance(transform.position, r.transform.position) >= destroyIfOverDistance) {
            Destroy(gameObject);
        }
    }

    void InitDeathParticles() {
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
    }

    void OnCaught() {
        isCaught = true;
        Score.instance.AddPoints(data.score);
        InitDeathParticles();
    }
}
