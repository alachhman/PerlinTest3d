using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
    public GameObject cameraReference;
    public Text textReference;
    public Enemy enemy;
    
    void Start() {
        cameraReference = GameObject.Find("Main Camera");
        textReference = GameObject.Find("EntityText").GetComponent<Text>();
    }
    
    void Update() {
        gameObject.transform.rotation = cameraReference.transform.rotation;
    }
}