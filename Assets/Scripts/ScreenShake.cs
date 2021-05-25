using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour {
    private float amplitude, frequency;
    private float timeLeft;
    public float defaultDuration = 0.2f, defaultAmplitude = 1.2f, defaultFrequency = 2f;

    private CinemachineBasicMultiChannelPerlin noise;

    public static ScreenShake instance;

    private void Start() {
        CinemachineVirtualCamera vCam = GetComponent<CinemachineVirtualCamera>();
        noise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        instance = this;
    }

    public void Shake() {
        Shake(defaultDuration, defaultAmplitude, defaultFrequency);
    }

    public void Shake(float duration) {
        Shake(duration, defaultAmplitude, defaultFrequency);
    }

    public void Shake(float duration, float amplitude) {
        Shake(duration, amplitude, defaultFrequency);
    }

    public void Shake(float duration, float amplitude, float frequency) {
        this.amplitude = amplitude;
        this.frequency = frequency;
        timeLeft = duration;
    }

    private void Update() {
        if (timeLeft > 0) {
            noise.m_AmplitudeGain = amplitude;
            noise.m_FrequencyGain = frequency;
            timeLeft -= Time.deltaTime;
        } else {
            noise.m_AmplitudeGain = 0;
            noise.m_FrequencyGain = 0;
        }
    }
}
