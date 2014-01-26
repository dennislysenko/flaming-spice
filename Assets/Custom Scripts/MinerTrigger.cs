using UnityEngine;
using System.Collections;
public class MinerTrigger : MonoBehaviour {

	private bool inCube = false;
	void OnTriggerEnter(Collider col) {
		Debug.Log (col.gameObject.name + "");
		if(col.gameObject.name == "Alterego Player") {
			//col.gameObject.GetComponent(EgoSystem).SendMessage("SetInDark", true);
			//col.gameObject.SendMessage("SetInDark", true);
			Debug.Log ("LAWL ENTERING");
			inCube = true;
			EgoSystem.SetInDark(true);
		}
	}
	void OnTriggerExit(Collider col) {
		if(col.gameObject.name == "Alterego Player") {
			//col.gameObject.GetComponent(EgoSystem).SendMessage("SetInDark", false);
			//col.gameObject.SendMessage("SetInDark", false);
			//col.gameObject.GetComponent(EgoSystem).inDark = false;
			inCube = false;
			EgoSystem.SetInDark(false);
		}
	}

	void Update() {
		if (inCube) {
			EgoSystem.SetInDark(true);
		}

	}
}