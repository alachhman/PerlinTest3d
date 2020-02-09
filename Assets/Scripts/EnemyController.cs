using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public GameObject cameraReference;

    void Start() {
        cameraReference = GameObject.Find("Main Camera");
    }
    
    void Update() {
        gameObject.transform.rotation = cameraReference.transform.rotation;
    }
}