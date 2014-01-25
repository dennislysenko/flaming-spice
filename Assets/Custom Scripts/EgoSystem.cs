﻿using UnityEngine;
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

public class NinjaEgo : CharacterEgo {
	public override void Init(EgoSystem parent) {
		// Deactivate all guards

		parent.setCurrentlyChangingEgo (false);
	}

	public override void DeInit(EgoSystem parent) {
		// Activate all guards
	}
}


public class EgoSystem : MonoBehaviour {
	public int maxSwitches = 5;
	public static int switchesLeft;

	float timeSinceLastDoorChange = 0.3f;

	public Texture2D standard;
	public Texture2D thief; 
	public Texture2D inventor;
	public Texture2D birdman;
	public Texture2D ninja;
	public Texture2D miner;
	public Texture2D electrician;
	public GUITexture egoDisplay;

	public GUIText switchesLeftText;

	bool currentlyChangingEgo;
	CharacterEgo currentEgo;
	CharacterEgo standardEgo;
	CharacterEgo thiefEgo;
	CharacterEgo birdmanEgo;
	CharacterEgo inventorEgo;
	CharacterEgo minerEgo;
	CharacterEgo ninjaEgo;

	public void setCurrentlyChangingEgo(bool changing) {
		currentlyChangingEgo = changing;
	}

	public CharacterEgo GetCurrentEgo() {
		return currentEgo;
	}

	public void setCurrentEgo(CharacterEgo changeEgo) {
		if (changeEgo == currentEgo) {
			return;
		}

		setCurrentlyChangingEgo(true);
		
		currentEgo.DeInit (this);
		changeEgo.Init (this);

		currentEgo = changeEgo;
		switchesLeft--;
		switchesLeftText.text = "Ego Switches Left: " + switchesLeft;
	}

	// Use this for initialization
	void Start () {
		switchesLeft = maxSwitches;
		currentlyChangingEgo = false;

		standardEgo = new StandardEgo();
		thiefEgo = new ThiefEgo();
		birdmanEgo = new BirdmanEgo();
		inventorEgo = new InventorEgo ();
		minerEgo = new MinerEgo ();
		ninjaEgo = new NinjaEgo ();

		// Standard Ego should NOT deinit
		// Thief Ego should NOT deinit
		birdmanEgo.DeInit (this);
		inventorEgo.DeInit (this);
		// Miner Ego should NOT deinit
		// Ninja Ego should NOT deinit
		
		currentEgo = standardEgo;

		switchesLeftText.text = "Ego Switches Left: " + switchesLeft;

		//standard = (Texture2D)Resources.Load ("Images/Standard.png");
		//thief = (Texture2D)Resources.Load ("Images/Thief.png"); 
		//inventor = (Texture2D)Resources.Load ("Images/Inventor.png");
		//birdman = (Texture2D)Resources.Load ("Images/Birdman.png");
		//ninja = (Texture2D)Resources.Load ("Images/Ninja.png");
		//miner = (Texture2D)Resources.Load ("Images/Miner.png");
		//electrician = (Texture2D)Resources.Load ("Images/Electrician.png");

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
		timeSinceLastDoorChange += Time.deltaTime;

		// Handle ego-changing button presses
		if (!currentlyChangingEgo && switchesLeft > 0) {
			//GUITexture guiTexture = GUITexture.FindObjectOfType<GUITexture>();
			//GameObject guiTexture = GameObject.FindGameObjectWithTag ("EgoDisplay");
			CharacterEgo changeEgo = null;
			if (Input.GetKey ("1")) {
				changeEgo = standardEgo;
				egoDisplay.texture = standard;
			} else if (Input.GetKey ("2")) {
				changeEgo = thiefEgo;
				egoDisplay.texture = thief;
			} else if (Input.GetKey ("3")) {
				changeEgo = ninjaEgo;
				egoDisplay.texture = ninja;
			} else if (Input.GetKey ("4")) {
				changeEgo = birdmanEgo;
				egoDisplay.texture = birdman;
			} else if (Input.GetKey ("6")) {
				changeEgo = inventorEgo;
				egoDisplay.texture = inventor;
			} else if (Input.GetKey ("5")) {
				changeEgo = minerEgo;
				egoDisplay.texture = miner;
			}



			if (changeEgo != null) {
				
				setCurrentEgo (changeEgo);
			}
		}

		if (Input.GetKey ("e") && timeSinceLastDoorChange >= 0.3f) {
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
						timeSinceLastDoorChange = 0;
					}
				}
			}
		}

		if (GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMotor>().isDead)
			Reset ();

	}
}
