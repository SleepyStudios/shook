using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour {
    public static Score instance;
    public int score;
    public float displayScore;
    public Color positiveColour, negativeColour;

    TextMeshProUGUI text;
    Color targetColor;

    void Start() {
        instance = this;
        text = GetComponent<TextMeshProUGUI>();
        targetColor = text.color;
    }

    public void AddPoints(int points) {
        score += points;
        if (points > 0) {
            targetColor = positiveColour;
        } else {
            targetColor = negativeColour;
        }
    }

    void Update() {
        displayScore = Mathf.Lerp(displayScore, score, Time.deltaTime * 4f);
        float textScore = score > displayScore ? Mathf.Ceil(displayScore) : Mathf.Floor(displayScore);
        if ((int) textScore == score) targetColor = Color.white;

        string sign = score >= 0 ? "" : "-";
        text.text = $"{sign}${Mathf.Abs(textScore)}";

        text.color = Color.Lerp(text.color, targetColor, Time.deltaTime * 4f);
    }
}
