using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float movementSpeed = 15f;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();		
	}
	
	void Update () {
		Vector3 movement = new Vector3 (
			Input.GetAxis("Vertical") * movementSpeed,
			Physics.gravity.y,
			-Input.GetAxis("Horizontal") * movementSpeed
        );

		rb.AddForce (movement);
	}
}
