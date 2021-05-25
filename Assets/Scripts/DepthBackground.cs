using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public struct DepthColour {
    public float depth;
    public Color colour;
}

public class DepthBackground : MonoBehaviour {
    public List<DepthColour> depthColours = new List<DepthColour>();
    public float bgChangeSpeed = 3f;

    Rope r;
    Camera cam;

    void Start() {
        r = FindObjectOfType<Rope>();
        cam = GetComponent<Camera>();
        cam.backgroundColor = depthColours[0].colour;
    }

    void Update() {
        var available = depthColours.Where((c) => r.transform.position.y < -c.depth).ToList();

        if (available.Count > 0) {
            cam.backgroundColor = Color.Lerp(cam.backgroundColor, available.Last().colour, Time.deltaTime * bgChangeSpeed);
        } else {
            cam.backgroundColor = Color.Lerp(cam.backgroundColor, depthColours[0].colour, Time.deltaTime * bgChangeSpeed);
        }
    }
}
