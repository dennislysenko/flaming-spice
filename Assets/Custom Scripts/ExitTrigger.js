#pragma strict

function Start () {
}

function Update () {
}

function OnTriggerEnter (col : Collider) {
	if (col.gameObject.name == "Alterego Player") {
		Application.LoadLevel("LevelSelect");
	}
}