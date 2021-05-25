using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour {
    AudioSource sfx;
    public float targetReverbZoneMix, targetPitch;
    public float speed = 3f;

    void Start() {
        sfx = GetComponent<AudioSource>();
        targetReverbZoneMix = 0f;
        targetPitch = 1f;
    }

    void Update()  {
        sfx.reverbZoneMix = Mathf.Lerp(sfx.reverbZoneMix, targetReverbZoneMix, Time.deltaTime * speed);
        sfx.pitch = Mathf.Lerp(sfx.pitch, targetPitch, Time.deltaTime * speed);
    }
}
