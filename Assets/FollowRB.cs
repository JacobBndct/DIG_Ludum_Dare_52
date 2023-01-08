using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRB : MonoBehaviour {
    [SerializeField] Transform alienParent;
    // Start is called before the first frame update
    private void Update() {
        transform.localRotation = Quaternion.identity;
        transform.position = alienParent.position;
    }
}
