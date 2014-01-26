using UnityEngine;
using System.Collections;

public class LaserBehaviour : MonoBehaviour {
	void OnTriggerEnter(Collider collider) {
		if (collider.tag == "Player") {
			collider.GetComponent<EgoSystem> ().Reset ();
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
