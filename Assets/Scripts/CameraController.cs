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
	private bool isGoingToSkyView;
	private Vector3 skyViewCoords;
	private Quaternion skyViewRotation;
	
	void Start() {
		originalPos = transform.position;
		originalRot = transform.localRotation;
		isResetingCamera = false;
		isGoingToSkyView = false;
		skyViewCoords = new Vector3(12, 10, 12);
		skyViewRotation = Quaternion.Euler(
			new Vector3(
				90,
				transform.eulerAngles.y,
				transform.eulerAngles.z));
	}

	void Update() {
		if (isResetingCamera && !isGoingToSkyView) {
			resetCamera();
			if (transform.position == originalPos) {
				isResetingCamera = false;
			}
		}
		else if (isGoingToSkyView && !isResetingCamera) {
			switchToSkyView();
			if (transform.position == skyViewCoords) {
				isGoingToSkyView = false;
			}
		}
		else if(!isResetingCamera && !isGoingToSkyView) {
			if (Input.GetKey("left")) {
				rotateCamera(Vector3.up);
			}

			if (Input.GetKey("right")) {
				rotateCamera(Vector3.down);
			}

			if (Input.GetKey("up")) {
				isGoingToSkyView = true;
			}

			if (Input.GetKey("down")) {
				isResetingCamera = true;
			}

			if (Input.GetKeyDown("c")) {
				isResetingCamera = true;
			}
		}
	}

	void rotateCamera(Vector3 direction) {
		transform.RotateAround(target.transform.position, direction, rotationSpeed * Time.deltaTime);
	}

	void switchToSkyView() {
		transform.position = Vector3.MoveTowards(transform.position, skyViewCoords, Time.deltaTime * 20f);
		transform.rotation = Quaternion.Slerp(
			transform.rotation,
			skyViewRotation,
			Time.deltaTime * 20f);
	}

	private void resetCamera() {
		transform.position = Vector3.MoveTowards(transform.position, originalPos, Time.deltaTime * 20f);
		transform.localRotation = originalRot;
		transform.LookAt(target.transform);
	}
}