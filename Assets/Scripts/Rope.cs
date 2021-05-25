using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {
    public float speed = 2f, upSpeed = 4f;
    public int maxHealth = 200, health;
    public int healthAnimFrames = 2;
    public bool goingBackUp;
    public AudioClip inWater, outWater, onHit;

    Rigidbody2D rb;
    Animator animator;
    Animator[] childAnimators;

    float initialY;
    bool disabled = true;
    float initialDrag;
    DragTarget dt;
    bool dead;
    bool repairing;
    GameObject logo;

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        animator.speed = 0f;
        childAnimators = GetComponentsInChildren<Animator>();
        foreach (Animator a in childAnimators) {
            a.speed = 0f;
        }

        initialY = transform.position.y;

        dt = FindObjectOfType<DragTarget>();
        initialDrag = dt.m_Frequency;
        dt.m_Frequency = 0f;

        health = maxHealth;
        logo = GameObject.Find("Logo");
    }

    void Update() {
        if (disabled && Input.GetKeyDown(KeyCode.Space) && !logo.activeSelf) {
            disabled = false;
            dt.m_Frequency = initialDrag;
            GetComponent<CinemachineSwitcher>().OnGoingDown();

            HandleInWaterSound();
            return;
        }

        if (!goingBackUp && !disabled && Input.GetKeyDown(KeyCode.Space)) {
            goingBackUp = true;
        }
    }

    void FixedUpdate()  {
        animator.Play("Rope", 0, health / (float)maxHealth);
        foreach (Animator a in childAnimators) {
            a.Play("Rope", 0, health / (float)maxHealth);
        }

        if (disabled) {
            return;
        }

        if (goingBackUp) {
            rb.MovePosition(new Vector2(transform.position.x, transform.position.y + upSpeed * Time.deltaTime));
            if (transform.position.y >= initialY) {
                transform.position = new Vector2(transform.position.x, initialY);
                disabled = true;
                dt.m_Frequency = 0f;
                goingBackUp = false;

                GetComponent<CinemachineSwitcher>().OnBackUp();
                EmptyCage();
                HandleOutWaterSound();
            }
        } else {
            rb.MovePosition(new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime));
        }
    }

    public void OnHit() {
        if (dead) return;

        AudioSource sfx = GetComponent<AudioSource>();
        sfx.PlayOneShot(onHit);

        health -= 10;
        if (health <= 0) {
            transform.Find("Net rope").GetChild(0).GetComponent<HingeJoint2D>().enabled = false;
            transform.Find("Gun rope").GetChild(0).GetComponent<HingeJoint2D>().enabled = false;
            Invoke("OnDeath", 2f);

            dead = true;
        }
    }

    public void OnRepair() {
        repairing = true;
        for (int i = 0; i < (maxHealth - health) / 10; i++) {
            Invoke("DelayedRepair", 0.3f * i);
        }
    }

    void DelayedRepair() {
        health += 10;
        if (health == maxHealth) repairing = false;
    }

    public bool CanRepair() {
        return !repairing && health < maxHealth;
    }

    void OnDeath() {
        FindObjectOfType<GameStateManager>().FadeIn();
    }

    public bool IsDisabled() {
        return disabled;
    }

    void EmptyCage() {
        Transform t = GameObject.Find("Attach Point").transform;
        for (int i = 0; i < t.childCount; i++) {
            Destroy(t.GetChild(i).gameObject, i * 0.5f);
        }
    }

    void HandleInWaterSound() {
        Ambience a = FindObjectOfType<Ambience>();
        a.targetReverbZoneMix = 1f;
        a.targetPitch = -0.08f;

        AudioSource sfx = GetComponent<AudioSource>();
        sfx.PlayOneShot(inWater);
    }

    void HandleOutWaterSound() {
        Ambience a = FindObjectOfType<Ambience>();
        a.targetReverbZoneMix = 0f;
        a.targetPitch = 1f;

        AudioSource sfx = GetComponent<AudioSource>();
        sfx.PlayOneShot(outWater);
    }
}
