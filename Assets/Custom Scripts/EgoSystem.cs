using UnityEngine;
using System.Collections;

public abstract class CharacterEgo {
	public abstract void Init(EgoSystem parent);
	public abstract void DeInit(EgoSystem parent);
}

public class StandardEgo : CharacterEgo {
	public override void Init(EgoSystem parent) {
		parent.setCurrentlyChangingEgo (false);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		CharacterMotor mtr = player.GetComponent<CharacterMotor>();
		mtr.isStandard = true;

	}
	
	public override void DeInit(EgoSystem parent) {
		// do nothing.
		// IMPORTANT: do NOT setCurrentlyChangingEgo(false).
		// That would mean that you can switch to a new ego before the current ego finishes computing 
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		CharacterMotor mtr = player.GetComponent<CharacterMotor>();
		mtr.isStandard = false;

	}
}

public class ThiefEgo : CharacterEgo {

	public override void Init(EgoSystem parent) {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		CharacterMotor mtr = player.GetComponent<CharacterMotor>();
		mtr.isThief = true;

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
					doorRotateyPart.RotateAround (leftHinge.position, leftHinge.up, 90);
				}
			}
		}

		parent.setCurrentlyChangingEgo (false);
	}

	public override void DeInit(EgoSystem parent) {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		CharacterMotor mtr = player.GetComponent<CharacterMotor>();
		mtr.isThief = false;
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
					doorRotateyPart.RotateAround (leftHinge.position, leftHinge.up, -90);
				}
			}
		}
	}
}

public class BirdmanEgo : CharacterEgo {
	public override void Init(EgoSystem parent) {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		CharacterMotor mtr = player.GetComponent<CharacterMotor>();
		mtr.jumping.baseHeight = 0.5f;
		mtr.jumping.extraHeight = 0.5f;
		mtr.isBirdman = true;

		GameObject[] drafts = GameObject.FindGameObjectsWithTag("Draft");
		foreach (GameObject draft in drafts) {
			draft.GetComponent<MeshRenderer>().enabled = true;
		}

		parent.setCurrentlyChangingEgo (false);
	}

	public override void DeInit(EgoSystem parent) {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		CharacterMotor mtr = player.GetComponent<CharacterMotor>();
		mtr.jumping.baseHeight = 1.0f;
		mtr.jumping.extraHeight = 1.0f;
		mtr.isBirdman = false;

		GameObject[] drafts = GameObject.FindGameObjectsWithTag("Draft");
		foreach (GameObject draft in drafts) {
			MeshRenderer jesus = draft.GetComponent<MeshRenderer>();
			if (jesus) {
				jesus.enabled = false;
			}
		}
	}
}

public class InventorEgo : CharacterEgo {
	public override void Init(EgoSystem parent) {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		CharacterMotor mtr = player.GetComponent<CharacterMotor>();
		mtr.isInventor = true;
		GameObject[] debrises = GameObject.FindGameObjectsWithTag("Debris");
		foreach (GameObject debris in debrises) {
			foreach (Transform child in debris.transform) {
				MeshRenderer jesus = child.GetComponent<MeshRenderer>();
				if (jesus) {
					if (child.tag.Equals("InnerDebris")) {
						child.gameObject.SetActive (false);
					} else if (child.tag.Equals("HiddenObject")) {
						child.gameObject.SetActive (true);
					}
				}
			}
		}

		if (mtr != null && mtr.movement != null) {
			float newSpeed = 3.5f;

			mtr.movement.maxForwardSpeed = newSpeed;
			mtr.movement.maxSidewaysSpeed = newSpeed;
			mtr.movement.maxBackwardsSpeed = newSpeed;
		}

		parent.setCurrentlyChangingEgo (false);
	}

	public override void DeInit(EgoSystem parent) {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		CharacterMotor mtr = player.GetComponent<CharacterMotor>();
		mtr.isInventor = false;
		GameObject[] debrises = GameObject.FindGameObjectsWithTag("Debris");
		foreach (GameObject debris in debrises) {
			foreach (Transform child in debris.transform) {
				MeshRenderer jesus = child.GetComponent<MeshRenderer>();
				if (jesus) {
					if (child.tag.Equals("InnerDebris")) {
						child.gameObject.SetActive (true);
					} else if (child.tag.Equals("HiddenObject")) {
						child.gameObject.SetActive (false);
					}
				}
			}
		}

		if (mtr != null && mtr.movement != null) {
			float newSpeed = 6.0f;
			
			mtr.movement.maxForwardSpeed = newSpeed;
			mtr.movement.maxSidewaysSpeed = newSpeed;
			mtr.movement.maxBackwardsSpeed = newSpeed;
		}
	}
}

public class MinerEgo : CharacterEgo {
	public override void Init(EgoSystem parent) {
		//GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject.FindWithTag ("MinerLight").gameObject.light.intensity = 0.2f;

		parent.setCurrentlyChangingEgo (false);
	}

	public override void DeInit(EgoSystem parent) {
		//GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject.FindWithTag ("MinerLight").gameObject.light.intensity = 0;
	}
}



public class EgoSystem : MonoBehaviour {
	public static int maxSwitches = 5;
	public static int switchesLeft = maxSwitches;

	bool currentlyChangingEgo;
	CharacterEgo currentEgo;
	CharacterEgo standardEgo;
	CharacterEgo thiefEgo;
	CharacterEgo birdmanEgo;
	CharacterEgo inventorEgo;
	CharacterEgo minerEgo;

	public void setCurrentlyChangingEgo(bool changing) {
		currentlyChangingEgo = changing;
	}

	public CharacterEgo GetCurrentEgo() {
		return currentEgo;
	}

	public void setCurrentEgo(CharacterEgo changeEgo) {
		if (changeEgo == currentEgo || switchesLeft <= 0) {
			return;
		}

		setCurrentlyChangingEgo(true);
		
		currentEgo.DeInit (this);
		changeEgo.Init (this);

		currentEgo = changeEgo;
		switchesLeft--;
	}

	// Use this for initialization
	void Start () {
		currentlyChangingEgo = false;

		standardEgo = new StandardEgo();
		thiefEgo = new ThiefEgo();
		birdmanEgo = new BirdmanEgo();
		inventorEgo = new InventorEgo ();
		minerEgo = new MinerEgo ();
		// Standard Ego should NOT deinit
		// Thief Ego should NOT deinit
		birdmanEgo.DeInit (this);
		inventorEgo.DeInit (this);
		// Miner Ego should NOT deinit
		
		currentEgo = standardEgo;
	}

	void Reset () {
		switchesLeft = maxSwitches;
		currentEgo.DeInit (this);
		standardEgo.Init (this);
		currentEgo = standardEgo;

		GameObject spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.transform.position = spawnPoint.transform.position;
		player.transform.rotation = spawnPoint.transform.rotation;
		player.GetComponent<CharacterMotor> ().isDead = false;

	}

	// Update is called once per frame
	void Update () {
		// Handle ego-changing button presses
		if (!currentlyChangingEgo) {
			CharacterEgo changeEgo = null;
			if (Input.GetKey ("1")) {
				changeEgo = standardEgo;
			} else if (Input.GetKey ("2")) {
				changeEgo = thiefEgo;
			} else if (Input.GetKey ("3")) {
				changeEgo = birdmanEgo;
			} else if (Input.GetKey ("4")) {
				changeEgo = inventorEgo;
			} else if (Input.GetKey ("5")) {
				changeEgo = minerEgo;
			}

			if (changeEgo != null) {
				
				setCurrentEgo (changeEgo);
			}
		}

		if (Input.GetKey ("e")) {
			RaycastHit forwardLookHit;
			if (Camera.current) {
				// Debug.DrawRay (transform.position + Vector3.up * 0.5f, Camera.current.transform.forward * 200, Color.black);
				Ray forwardRay = new Ray (transform.position + Vector3.up * 0.5f, Camera.current.transform.forward);
				if (Physics.Raycast (forwardRay, out forwardLookHit, 2)) {
					Collider collider = forwardLookHit.collider;
					if (forwardLookHit.collider.tag == "HiddenObject") {
						Debug.Log ("Colliding with hidden object!!");
					} else if (collider.transform.root.gameObject.tag == "UnlockedDoor") {
						Transform tmpRoot = collider.transform.root;
						tmpRoot.gameObject.GetComponent<DoorState>().Toggle ();
					}
				}
			}
		}
		if (GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMotor>().isDead)
			Reset ();

	}
}
