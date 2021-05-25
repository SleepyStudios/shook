using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour {
    int clicks;

    private void OnMouseDown() {
        if (!Debug.isDebugBuild) return;

        clicks++;
        if (clicks == 3) {
            Score.instance.AddPoints(999);
            clicks = 0;
        }
    }
}
