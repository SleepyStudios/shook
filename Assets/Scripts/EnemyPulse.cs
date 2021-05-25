using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPulse : MonoBehaviour {
    public float targetSize = 1.4f, scaleSpeed = 6f, alphaSpeed = 4f;
    public float delay = 2f;

    bool visible, animate;

    SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        SpriteRenderer parentSr = transform.parent.GetComponentInParent<SpriteRenderer>();
        sr.sprite = parentSr.sprite;
        sr.flipX = parentSr.flipX;
        sr.flipY = parentSr.flipY;
    }

    void Update() {
        if (sr.isVisible && !visible) {
            visible = true;
            Invoke("StartAnimate", delay);
        }

        if (animate) {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(targetSize, targetSize), Time.deltaTime * scaleSpeed);
            sr.color = Color.Lerp(sr.color, new Color(1f, 1f, 1f, 0f), Time.deltaTime * alphaSpeed);
        }
    }

    void StartAnimate() {
        animate = true;
    }
}
