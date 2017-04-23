using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public List<Axle> axles;
    public float maxMotorTorque;
    public float maxSteeringAngle;

	float minPitch = 0.5f;
	float maxPitch = 4f;

	float maxSpeed = 10f;

	void Start () {

	}

	void FixedUpdate () {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (Axle axle in axles)
        {
            if (axle.steering)
            {
                axle.leftWheel.steerAngle = steering;
                axle.rightWheel.steerAngle = steering;
            }
            if (axle.motor)
            {
                axle.leftWheel.motorTorque = motor;
                axle.rightWheel.motorTorque = motor;
            }

            ApplyLocalPositionToVisuals(axle.leftWheelMesh, axle.leftWheel);
            ApplyLocalPositionToVisuals(axle.rightWheelMesh, axle.rightWheel);
        }

		if (CompareTag ("Player")) {
			float speed = GetComponent<Rigidbody> ().velocity.magnitude;

			float newPitch = Mathf.Clamp (((speed / maxSpeed) * maxPitch), minPitch, maxPitch);
			transform.GetComponent<AudioSource> ().pitch = newPitch;
		}
	}

    void ApplyLocalPositionToVisuals(GameObject wheelMesh, WheelCollider collider)
    {
        if (wheelMesh.transform == null)
        {
            return;
        }


        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        wheelMesh.transform.position = position;
		wheelMesh.transform.rotation = rotation;
	}
}

[System.Serializable]
public class Axle
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public GameObject leftWheelMesh;
    public GameObject rightWheelMesh;
    public bool motor;
    public bool steering;
}
