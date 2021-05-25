using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour {
    public bool visible;
    Renderer r;

    private void Start() {
        r = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        visible = r.isVisible;
    }
}
