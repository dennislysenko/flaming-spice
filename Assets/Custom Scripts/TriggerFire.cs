using UnityEngine;
using System.Collections;

public class TriggerFire : MonoBehaviour {
	public GameObject fire;

	void OnTriggerEnter (Collider collider) {
		fire.SetActive (true);
	}
}
