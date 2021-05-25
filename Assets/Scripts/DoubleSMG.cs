using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSMG : MonoBehaviour {
    void Start() {
        transform.GetChild(0).GetChild(0).GetComponent<HingeJoint2D>().connectedBody = transform.parent.GetComponent<Rigidbody2D>();
        transform.GetChild(1).GetChild(0).GetComponent<HingeJoint2D>().connectedBody = transform.parent.GetComponent<Rigidbody2D>();
    }
}
