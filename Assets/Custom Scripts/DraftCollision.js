#pragma strict

function Start () {

}

function Update () {

}

function onCollisionEnter(theCollision : Collision) {
	if (theCollision.gameObject.tag == "Player") {
		Debug.Log("Hit the player.");
	}
}