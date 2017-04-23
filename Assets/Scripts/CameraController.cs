using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject target;

	public float cameraRotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	void LateUpdate () {
		if (target == null) {
			// The game is over
			return;
		}
		transform.LookAt (target.transform);
		transform.Translate (Vector3.right * cameraRotationSpeed * Time.deltaTime);
	}
}
