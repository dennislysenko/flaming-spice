#pragma strict

public var speed   : float = 1;
public var chaseDistance = 20;
public var fieldOfViewAngle : float = 60f;
public var playerInSight : boolean;
public var personalLastSighting : Vector3;

private var lastPlayerSighting : LastPlayerSighting;
private var player : GameObject;
private var previousSighting : Vector3;

function Start () {
	player = GameObject.FindGameObjectWithTag("Player");
	//lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent(LastPlayerSighting);
	personalLastSighting = new Vector3(1000f, 1000f, 1000f);
	
}


 
function Update()
{
/************************Enemy Sight AI****************************/
var ray : RaycastHit;
var sightVec : Vector3 = transform.forward;
var heading : Vector3 = (player.transform.position - transform.position).normalized;
var dot : float = Vector3.Dot(sightVec, heading);
//Dot the two together to see

if(personalLastSighting != player.transform.position && personalLastSighting != Vector3(1000f, 1000f, 1000f)){
	transform.LookAt(personalLastSighting);
	transform.position += transform.forward*speed*Time.deltaTime;
}
//If the player is in a ~120 degree FOV
if(dot > .5) {
	//Use raycast to check that it is in his chasing range.
	if(Physics.Raycast(transform.position, heading, ray, 20)){
		personalLastSighting = ray.point;
		
		
     	//transform.position += transform.forward*speed/36000000000*Time.deltaTime;
		var pos : Vector3 = (personalLastSighting - transform.position).normalized;
		pos.y = 0;
		transform.LookAt(pos);
		transform.position += pos * Time.deltaTime;
	}

}


 
/****************************** Enemy Movement AI **************************/
 

  /*  if(Vector3.Distance(transform.position,personalLastSighting) <= chaseDistance)
       {
       transform.LookAt(personalLastSighting);
     transform.position += transform.forward*speed*Time.deltaTime;
 		//print("MOVING");
 
 
        }
 */
 
/**************************************************************************/
 
/****************************** Lose Conditions ***************************/
     var distToPlayer = (transform.position - player.transform.position).sqrMagnitude;
 
     if( distToPlayer < 1.0 ) {
        print ("You lost");
     }
 
/**************************************************************************/
}