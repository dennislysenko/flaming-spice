#pragma strict

var inCube : boolean = false;
var textManager : GameObject;;

function Start () {
	textManager = GameObject.FindGameObjectWithTag("TextManager");
	textManager.guiText.text = "Welcome to Alterego!";
	yield WaitForSeconds(3);
	textManager.guiText.text = "";
}

function Update () {

	if (inCube && Input.GetKey("e")) {
		Application.LoadLevel("map_tutorial");
	}

}

function OnGUI() {

}

function OnTriggerEnter (col : Collider) {
	if (col.gameObject.name == "Alterego Player") {
		inCube = true;
		textManager.guiText.text = "Press E to enter the level...";
	}
}

function OnTriggerExit (col : Collider) {
	if (col.gameObject.name == "Alterego Player") {
		inCube = false; 
		textManager.guiText.text = "";
	}
}