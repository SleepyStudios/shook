using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour {
    public List<string> messages = new List<string>();
    public float newMessageInterval = 3f, fadeInterval = 1f;
    public TextMeshProUGUI text;
    public string startMessage = "Press [SPACE] to descend";
    public float targetAlpha = 1f;

    int currIndex = -1;
    float tmrMessage, tmrFade;
    Rope r;

    private void Start() {
        text.text = startMessage;
        r = FindObjectOfType<Rope>();
        tmrFade = fadeInterval;
    }

    private void Update() {
        if (r.IsDisabled()) {
            text.text = startMessage;
            text.color = Color.white;
            return;
        }

        tmrMessage += Time.deltaTime;
        if (tmrMessage >= newMessageInterval && currIndex < messages.Count - 1) {
            targetAlpha = 1f;
            currIndex++;
            text.text = messages[currIndex];
            tmrMessage = 0;
            tmrFade = 0;
        }

        tmrFade += Time.deltaTime;
        if (tmrFade >= fadeInterval) {
            targetAlpha = 0f;
            tmrFade = 0;
        }

        text.color = Color.Lerp(text.color, new Color(1f, 1f, 1f, targetAlpha), Time.deltaTime * 6f);
    }
}
