using UnityEngine;
using System.Collections;

public class DoorState : MonoBehaviour {
	bool open = false;

	public void Toggle() {
		int angle = 90;
		if (open) {
			angle *= -1;
		}

		Transform leftFrame = transform.root.Find ("Door Container/Door Frame/Door Left Frame");
		Transform doorRotateyPart = transform.root.Find ("Door Container/Door");

		if (!(doorRotateyPart == null || leftFrame == null)) {
			Debug.Log ("Rotating door!");
			doorRotateyPart.RotateAround (leftFrame.position, leftFrame.up, angle);
		}

		open = !open;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
