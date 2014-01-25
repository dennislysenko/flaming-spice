using UnityEngine;
using System.Collections;

public abstract class CharacterEgo {
	public abstract void Init(EgoSystem parent);
	public abstract void DeInit(EgoSystem parent);
}

public class StandardEgo : CharacterEgo {
	public override void Init(EgoSystem parent) {
		parent.setCurrentlyChangingEgo (false);
	}
	
	public override void DeInit(EgoSystem parent) {
		 // do nothing. do NOT setCurrentlyChangingEgo(false).
		// That would mean that you can switch to a new ego before the current ego finishes computing 
	}
}

public class ThiefEgo : CharacterEgo {
	public override void Init(EgoSystem parent) {
		GameObject[] doors = GameObject.FindGameObjectsWithTag("LockedDoor");
		foreach (GameObject door in doors) {
			if (door.activeInHierarchy) {
				Debug.Log ("Active door");
				Transform doorRotateyPart = null;
				Transform leftHinge = null;

				// Find and assign the moving part of the door and the left part of the door frame 
				foreach (Transform child in door.transform) {
					if (child.name == "Door") {
						doorRotateyPart = child;
					} else if (child.name == "Door Frame") {
						foreach (Transform frameChild in child.transform) {
							if (frameChild.name == "Door Left Frame") {
								leftHinge = frameChild;
							}
						}
					}
				}

				if (!(doorRotateyPart == null || leftHinge == null)) {
					Debug.Log ("Rotating door!");
					doorRotateyPart.RotateAround (leftHinge.position, leftHinge.up, 90);
				}
			}
		}

		parent.setCurrentlyChangingEgo (false);
	}

	public override void DeInit(EgoSystem parent) {
		GameObject[] doors = GameObject.FindGameObjectsWithTag("LockedDoor");
		foreach (GameObject door in doors) {
			if (door.activeInHierarchy) {
				Debug.Log ("Active door");
				Transform doorRotateyPart = null;
				Transform leftHinge = null;
				
				// Find and assign the moving part of the door and the left part of the door frame 
				foreach (Transform child in door.transform) {
					if (child.name == "Door") {
						doorRotateyPart = child;
					} else if (child.name == "Door Frame") {
						foreach (Transform frameChild in child.transform) {
							if (frameChild.name == "Door Left Frame") {
								leftHinge = frameChild;
							}
						}
					}
				}
				
				if (!(doorRotateyPart == null || leftHinge == null)) {
					Debug.Log ("Rotating door!");
					doorRotateyPart.RotateAround (leftHinge.position, leftHinge.up, -90);
				}
			}
		}
	}
}

public class BirdmanEgo : CharacterEgo {
	public override void Init(EgoSystem parent) {


		parent.setCurrentlyChangingEgo (false);
	}

	public override void DeInit(EgoSystem parent) {

	}
}

public class EgoSystem : MonoBehaviour {
	bool currentlyChangingEgo;
	CharacterEgo currentEgo;
	CharacterEgo standardEgo;
	CharacterEgo thiefEgo;
	CharacterEgo birdmanEgo;

	public void setCurrentlyChangingEgo(bool changing) {
		currentlyChangingEgo = changing;
	}

	public void setCurrentEgo(CharacterEgo changeEgo) {
		Debug.Log ("In setCurrentEgo");

		if (changeEgo == currentEgo) {
			Debug.Log ("No ego change");
			return;
		}

		setCurrentlyChangingEgo(true);

		Debug.Log ("Deiniting current ego");
		currentEgo.DeInit (this);

		Debug.Log ("Initing new ego");
		changeEgo.Init (this);

		currentEgo = changeEgo;

		Debug.Log ("Done changing ego!");
	}

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		//player.

		currentlyChangingEgo = false;

		standardEgo = new StandardEgo();
		thiefEgo = new ThiefEgo();
		
		currentEgo = standardEgo;
	}
	
	// Update is called once per frame
	void Update () {
		if (!currentlyChangingEgo) {
			CharacterEgo changeEgo = null;
			if (Input.GetKey ("1")) {
				changeEgo = standardEgo;
				Debug.Log ("Pressing 1");
			} else if (Input.GetKey ("2")) {
				changeEgo = thiefEgo;
				Debug.Log ("Pressing 2");
			}

			if (changeEgo != null) {
				Debug.Log ("Calling setCurrentEgo");
				
				setCurrentEgo (changeEgo);
			}
		}

		if (currentEgo == birdmanEgo) {
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			//player.gameObject.
		}
	}
}
