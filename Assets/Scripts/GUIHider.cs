using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHider : MonoBehaviour {
    
    public bool hidden;
    public List<GameObject> elements = new List<GameObject>();

    void Update() {
        if (!Debug.isDebugBuild) return;

        if (Input.GetKeyDown(KeyCode.H)) {
            Toggle();
        }
    }

    void Toggle() {
        hidden = !hidden;
        foreach (GameObject elem in elements) {
            elem.SetActive(!hidden);
        }
    }
}
