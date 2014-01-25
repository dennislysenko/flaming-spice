﻿using UnityEngine;
using System.Collections;

public class DraftCollision : MonoBehaviour {
	bool playerInside = false;
	GameObject player;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Player")) {
			playerInside = true;
			player = other.gameObject;

			CharacterMotor mtr = other.GetComponent<CharacterMotor>();
			mtr.inDraft = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag.Equals ("Player")) {
			playerInside = false;
			player = null;

			CharacterMotor mtr = other.GetComponent<CharacterMotor>();
			mtr.inDraft = false;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
