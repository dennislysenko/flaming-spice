using UnityEngine;
using System.Collections;

public class FireBehaviour : MonoBehaviour {
	Collider player;

	IEnumerator Burn() {
		Debug.Log ("Starting to burn player...");
		yield return new WaitForSeconds(0.3f);

		if (player) {
			player.GetComponent<EgoSystem> ().Reset ();
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.name == "Alterego Player") {
			player = collider;
			StartCoroutine(Burn ());
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.name == "Alterego Player") {
			player = null;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
