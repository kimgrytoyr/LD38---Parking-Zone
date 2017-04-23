using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkingController : MonoBehaviour {

	public GameObject car;
	public GameObject trailer;
	public GameObject gameController;
	public Text stopText;

	Bounds parkingZoneBounds;

	void Start() {
		// Get the bounds of the parking zone
		parkingZoneBounds = GetComponent<MeshRenderer>().bounds;
		GetComponent<MeshRenderer> ().material.color = Color.red;
	}

	void Update () {
		Bounds carBounds = car.GetComponent<MeshRenderer> ().bounds;

		Bounds trailerBounds = trailer.GetComponent<MeshRenderer> ().bounds;

		if (BoundsContains (parkingZoneBounds, carBounds) && BoundsContains (parkingZoneBounds, trailerBounds)) {
			float rot = car.transform.rotation.eulerAngles.y;
			if ((rot < 360 && rot > 340) || rot > 0 && rot < 20) {
				// Success!
				GetComponent<MeshRenderer> ().material.color = Color.green;
				float speed = car.transform.parent.gameObject.GetComponent<Rigidbody> ().velocity.magnitude;
				Debug.Log (speed);
				if (speed <= 0.5f) {
					gameController.GetComponent<GameController> ().Parked ();
					stopText.text = "";
				} else {
					stopText.text = "Great! Now, come to a stop.";
				}
			} else {
				GetComponent<MeshRenderer> ().material.color = Color.red;
				stopText.text = "";
			}
		} else {
			GetComponent<MeshRenderer> ().material.color = Color.red;
			stopText.text = "";
		}
	}

	bool BoundsContains (Bounds container, Bounds obj) {
		Vector3 minBounds = obj.min;
		minBounds.y = container.min.y;

		Vector3 maxBounds = obj.max;
		maxBounds.y = container.max.y;

		return container.Contains(minBounds) && container.Contains(maxBounds);
	}
}
