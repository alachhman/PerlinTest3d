using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour {
    public GameObject target;
    public float rotationSpeed = 1f;
    public Vector3 originalPos;
    public Quaternion originalRot;
    private bool isResetingCamera;

    void Start() {
        originalPos = transform.position;
        originalRot = transform.localRotation;
        isResetingCamera = false;
    }
    void Update() {
        if (isResetingCamera) {
            resetCamera();
            if (transform.position == originalPos) {
                isResetingCamera = false;
            }
        }
        else {
            if (Input.GetKey("left")) {
                rotateCamera(Vector3.up);
            }

            if (Input.GetKey("right")) {
                rotateCamera(Vector3.down);
            }

            if (Input.GetKeyDown("c")) {
                isResetingCamera = true;
            }
        }
    }

    void rotateCamera(Vector3 direction) {
        transform.RotateAround(target.transform.position, direction, rotationSpeed * Time.deltaTime);
    }

    private void resetCamera() {
        transform.position = Vector3.MoveTowards(transform.position, originalPos, Time.deltaTime * 20f);
        transform.localRotation = originalRot;
        transform.LookAt(target.transform);
    }
}