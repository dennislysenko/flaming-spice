#pragma strict

function Start () {

}

var inCube : boolean = false;

function Update () {

	if (inCube && Input.GetKey("e")) {
		Application.LoadLevel("map_thief1");
	}

}

function OnTriggerEnter (col : Collider) {
	if (col.gameObject.name == "Alterego Player") {
		inCube = true;
	}
}

function OnTriggerExit (col : Collider) {
	if (col.gameObject.name == "Alterego Player") {
		inCube = false; 
	}
}