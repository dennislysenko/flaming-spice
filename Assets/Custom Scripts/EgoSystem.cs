using UnityEngine;
using System.Collections;
using System.Threading;

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
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		CharacterMotor mtr = player.GetComponent<CharacterMotor>();
		mtr.isNinja = true;

		parent.setCurrentlyChangingEgo (false);
	}

	public override void DeInit(EgoSystem parent) {
		// Activate all guards
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		CharacterMotor mtr = player.GetComponent<CharacterMotor>();
		mtr.isNinja = false;
	}
}

public class ElectricianEgo : CharacterEgo {
	public override void Init(EgoSystem parent) {
		// Open all laser control panels
		GameObject[] caps = GameObject.FindGameObjectsWithTag("LaserCPCap");
		foreach (GameObject cap in caps) {
			cap.SetActive (false);
		}
		GameObject[] lcaps = GameObject.FindGameObjectsWithTag ("LightCPCap");
		foreach (GameObject cap in lcaps) {
			cap.SetActive(false);
		}
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		CharacterMotor mtr = player.GetComponent<CharacterMotor>();
		mtr.isElectrician = true;
		
		parent.setCurrentlyChangingEgo (false);
	}
	
	public override void DeInit(EgoSystem parent) {
		// Activate all guards
		GameObject[] panels = GameObject.FindGameObjectsWithTag("LaserCP");
		foreach (GameObject panel in panels) {
			Transform cap = panel.transform.GetChild(0);
			if (cap) {
				cap.gameObject.SetActive(true);
			}
		}
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		CharacterMotor mtr = player.GetComponent<CharacterMotor>();
		mtr.isElectrician = false;
	}
}

public class GhostEgo : CharacterEgo {
	public override void Init(EgoSystem parent) {
		// Open all laser control panels
		GameObject[] caps = GameObject.FindGameObjectsWithTag("GhostBlock");
		foreach (GameObject cap in caps) {
			//cap.collider.isTrigger = true;
			cap.gameObject.SetActive(false);
		}

		GameObject[] panels = GameObject.FindGameObjectsWithTag("GhostHallucinationContainer");
		foreach (GameObject panel in panels) {
			Transform cap = panel.transform.GetChild(0);
			if (cap) {
				cap.gameObject.SetActive(true);
			}
		}
		
		parent.setCurrentlyChangingEgo (false);
	}
	
	public override void DeInit(EgoSystem parent) {
		// Activate all guards
		GameObject[] panels = GameObject.FindGameObjectsWithTag("GhostBlockContainer");
		foreach (GameObject panel in panels) {
			Transform cap = panel.transform.GetChild(0);
			if (cap) {
				//cap.collider.isTrigger = false;
				cap.gameObject.SetActive(true);
			}
		}

		GameObject[] caps = GameObject.FindGameObjectsWithTag("GhostHallucination");
		foreach (GameObject cap in caps) {
			cap.SetActive (false);
		}


	}
}


public class EgoSystem : MonoBehaviour {
	public int maxSwitches = 5;
	int switchesLeft;
	bool hasZipline = false;
	bool hasSuperShoes = false;
	bool hasTrap = false;

	bool usingZipline = false;
	Vector3 deltaPosition = Vector3.zero;

	float timeWithShoesLeft = 0.0f;

	public static bool inDark = false;

	static bool shouldDieFromGuard = false;

	float timeSinceLastAction = 0.3f;

	public Texture2D standard;
	public Texture2D thief; 
	public Texture2D inventor;
	public Texture2D birdman;
	public Texture2D ninja;
	public Texture2D miner;
	public Texture2D electrician;
	GUITexture egoDisplay;

	GUIText switchesLeftText;

	GUITexture minerLight;

	public Transform trapPrefab;

	int maxEgo = 1;

	bool currentlyChangingEgo;
	CharacterEgo currentEgo;
	CharacterEgo standardEgo;
	CharacterEgo thiefEgo;
	CharacterEgo birdmanEgo;
	CharacterEgo inventorEgo;
	CharacterEgo minerEgo;
	CharacterEgo ninjaEgo;
	CharacterEgo electricianEgo;
	CharacterEgo ghostEgo;

	public static void SetInDark(bool update) {
		inDark = update;
	}

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

		minerLight = GameObject.FindGameObjectWithTag ("BlindingLight").guiTexture;
		egoDisplay = GameObject.FindGameObjectWithTag ("EgoDisplay").guiTexture;
		switchesLeftText = GameObject.FindGameObjectWithTag ("SwitchesLeft").guiText;

		standardEgo = new StandardEgo();
		thiefEgo = new ThiefEgo();
		birdmanEgo = new BirdmanEgo();
		inventorEgo = new InventorEgo ();
		minerEgo = new MinerEgo ();
		ninjaEgo = new NinjaEgo ();
		electricianEgo = new ElectricianEgo ();
		ghostEgo = new GhostEgo ();

		// Standard Ego should NOT deinit
		// Thief Ego should NOT deinit
		birdmanEgo.DeInit (this);
		inventorEgo.DeInit (this);
		// Miner Ego should NOT deinit
		// Ninja Ego should NOT deinit
		ghostEgo.DeInit (this);
		
		currentEgo = standardEgo;

		switchesLeftText.text = "Ego Switches Left: " + switchesLeft;

		int mapsCompleted = PersitentLevelManager.GetMapsCompleted ();
		maxEgo = 10;
			if (mapsCompleted >= 0)
				if (PersitentLevelManager.thiefIsUnlocked ())
						maxEgo = 2;

		//standard = (Texture2D)Resources.Load ("Images/Standard.png");
		//thief = (Texture2D)Resources.Load ("Images/Thief.png"); 
		//inventor = (Texture2D)Resources.Load ("Images/Inventor.png");
		//birdman = (Texture2D)Resources.Load ("Images/Birdman.png");
		//ninja = (Texture2D)Resources.Load ("Images/Ninja.png");
		//miner = (Texture2D)Resources.Load ("Images/Miner.png");
		//electrician = (Texture2D)Resources.Load ("Images/Electrician.png");

	}
	
	bool dying = false;
	Vector3 placeOfDeath;
	public void Reset() {
		if (dying) {
			return;
		}
		dying = true;
		placeOfDeath = transform.position;
		StartCoroutine (ResetHelper ());
	}

	public IEnumerator ResetHelper () {
		int iters = 50;
		float delay = 0.9f / iters;
		float increment = 30f / iters;
		// Show death mask
		for (int i = 0; i < iters; i++) {
			transform.position = placeOfDeath;
			transform.RotateAround (transform.position - (Vector3.up * 0.5f), transform.forward, Mathf.Pow(i / 20f, 2) * increment);
			yield return new WaitForSeconds(delay);
		}

		yield return new WaitForSeconds (0.5f);
	
		switchesLeft = maxSwitches;
		currentEgo.DeInit (this);
		standardEgo.Init (this);
		currentEgo = standardEgo;
		egoDisplay.texture = standard;

		hasZipline = false;
		hasSuperShoes = false;
		hasTrap = false;

		shouldDieFromGuard = false;

		GameObject spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.transform.position = spawnPoint.transform.position;
		player.transform.rotation = spawnPoint.transform.rotation;
		player.GetComponent<CharacterMotor> ().isDead = false;

		GuardScript[] guards = GameObject.FindObjectsOfType<GuardScript> ();
		foreach (GuardScript guard in guards) {
			guard.gameObject.GetComponent<GuardScript>().inLight = false;
		}

		dying = false;

		switchesLeftText.text = "Ego Switches Left: " + switchesLeft;
	}

	public static void interactWithGuard(bool withinKillRange) {
		if(!shouldDieFromGuard)
			shouldDieFromGuard = (withinKillRange || GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>().isThief);
	}

	// Update is called once per frame
	void Update () {
		timeSinceLastAction += Time.deltaTime;

		if (!dying) {
			if (timeWithShoesLeft >= 0)
				timeWithShoesLeft -= Time.deltaTime;
			else if(hasSuperShoes) {
				timeWithShoesLeft = 0;
				hasSuperShoes = false;
				CharacterMotor mtr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>();
				mtr.jumping.baseHeight = 1.0f;
				mtr.jumping.extraHeight = 1.0f;
			}

			if (usingZipline) {
				GameObject player = GameObject.FindGameObjectWithTag("Player");
				GameObject target = GameObject.FindGameObjectWithTag("ZiplineTarget");
				CharacterMotor mtr = player.GetComponent<CharacterMotor>();
				if((target.transform.position - player.transform.position).magnitude > deltaPosition.magnitude) {
					player.transform.position += deltaPosition;
				}
				else {
					hasZipline = false;
					usingZipline = false;
					deltaPosition = Vector3.zero;
					mtr.ziplining = false;
				}
				return;
			}

			if (!inDark && currentEgo == minerEgo)
				minerLight.enabled = true;
			else
				minerLight.enabled = false;

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
					changeEgo = inventorEgo;
					egoDisplay.texture = inventor;
				} else if (Input.GetKey ("4")) {
					changeEgo = birdmanEgo;
					egoDisplay.texture = birdman;
				} else if (Input.GetKey ("5")) {
					changeEgo = ninjaEgo;
					egoDisplay.texture = ninja;
				} else if (Input.GetKey ("6")) {
					changeEgo = minerEgo;
					egoDisplay.texture = miner;
				} else if (Input.GetKey ("7")) {
					changeEgo = electricianEgo;
					egoDisplay.texture = electrician;
				} else if (Input.GetKey ("8")) {
					changeEgo = ghostEgo;
					// TODO add texture
				}



				if (changeEgo != null) {
					
					setCurrentEgo (changeEgo);
				}
			}

			if (Input.GetKey ("e") && timeSinceLastAction >= 0.3f) {
				RaycastHit forwardLookHit;
				if (Camera.current) {
					Debug.DrawRay (Camera.current.transform.position - Vector3.up * 0.3f + transform.forward * 0.4f,
					               Camera.current.transform.forward * 200, Color.black);
					Ray forwardRay = new Ray (Camera.current.transform.position - Vector3.up * 0.3f + transform.forward * 0.4f,
					                          Camera.current.transform.forward);
					if (Physics.Raycast (forwardRay, out forwardLookHit, 3)) {
						Collider collider = forwardLookHit.collider;
						Debug.Log (collider.name);
						print (collider.tag);
						if (collider.tag == "HiddenObject") {
							switch(forwardLookHit.collider.name) {
								case "ZiplineDebris":
									//Debug.Log ("Picked up a zipline!");
									hasZipline = true;
								break;
								case "SuperShoesDebris":
									hasSuperShoes = true;
									CharacterMotor mtr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>();
									mtr.jumping.baseHeight = 4.5f;
									mtr.jumping.extraHeight = 4.5f;
									timeWithShoesLeft = 10.0f;//10 seconds to use shoes. MAYBE add timer?
								break;
								case "TrapDebris":
									hasTrap = true;
								break;
							}

							collider.gameObject.SetActive (false);
							//Debug.Log ("Colliding with hidden object!!");

						} else if (collider.transform.root.gameObject.tag == "UnlockedDoor") {
							Transform tmpRoot = collider.transform.root;
							tmpRoot.gameObject.GetComponent<DoorState>().Toggle ();
						} else if(hasZipline && forwardLookHit.collider.name == "Zipline") { 
							Debug.Log ("Trying to use zipline");
							//if raycast collides with zipline base, loop(transform, thread.sleep) till you get there 
							GameObject player = GameObject.FindGameObjectWithTag("Player");
							GameObject target = GameObject.FindGameObjectWithTag("ZiplineTarget");
							deltaPosition = (target.transform.position - player.transform.position)/100;
							//deltaPosition /= 100;
							usingZipline = true;
							player.GetComponent<CharacterMotor>().ziplining = true;
						} else if (collider.tag == "LaserCP" && currentEgo == electricianEgo) {
							collider.GetComponent<LaserCPBehaviour> ().DisableLasers ();
						} else if (collider.tag == "LightCP" && currentEgo == electricianEgo) {
							collider.GetComponent<LightCPBehaviour>().DisableLights();
							print ("lights");
						}
					} else if(hasTrap) { 
						Instantiate (trapPrefab,
						             gameObject.transform.position + gameObject.transform.forward * 1.2f + gameObject.transform.up * 1.0f,
						             gameObject.transform.rotation);
						hasTrap = false;
					}

					timeSinceLastAction = 0;
				}
			}

			if (GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMotor>().isDead || (Input.GetKey ("r") && timeSinceLastAction >= 0.3f))
				Reset ();
			//Debug.Log (shouldDieFromGuard + "");
			if (shouldDieFromGuard) {
				//Debug.Log ("about to reset");
				Reset ();
				
			}
		}
	}
}
