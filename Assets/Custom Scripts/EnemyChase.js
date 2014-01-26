#pragma strict

public var speed   : float = 6f;
public var chaseDistance = 20;
public var fieldOfViewAngle : float = 60f;
public var playerInSight : boolean;
public var personalLastSighting : Vector3;
public var chaseWaitTime : float = 5f;
public var patrolWaitTime : float = 3f;
public var patrolWayPoints : Transform[];

private var returning : boolean = false;
private var lastPlayerSighting : LastPlayerSighting;
private var player : GameObject;
private var previousSighting : Vector3;
private var wayPointIndex : int = 0;
private var chaseTimer : float;
private var patrolTimer : float;

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
//Dot the two together


//If the player is in a ~120 degree FOV
if(dot > .5 && Physics.Raycast(transform.position, heading, ray, 30)) {
	//Use raycast to check that it is in his chasing range.
	//if(ray.collider.gameObject.tag == "Player") {
	print("maybe a person");
		personalLastSighting = ray.point;
		returning = false;
		
     	//transform.position += transform.forward*speed/36000000000*Time.deltaTime;
		var pos : Vector3 = (personalLastSighting - transform.position).normalized;
		pos.y = 0;
		transform.LookAt(pos);
		transform.position += pos * Time.deltaTime * speed;
	//}

}
//If you saw them before... but not anymore.
else if(/*(personalLastSighting - player.transform.position).sqrMagnitude > .01 && */(personalLastSighting - transform.position).sqrMagnitude < .1 ){
	if( chaseTimer <= chaseWaitTime ){
		chaseTimer += Time.deltaTime;
		print("going to old point");
		var oldPos : Vector3 = (personalLastSighting - transform.position).normalized;
		oldPos.y = 0;
		returning = false;
		transform.LookAt(personalLastSighting);
		transform.position += oldPos * Time.deltaTime * speed;	
	} else {
		returning = true;
		chaseTimer = 0f;
	}
}
else if(returning) {
	if((transform.position - patrolWayPoints[wayPointIndex].position).sqrMagnitude >= .1 ){
		print("returning");
		transform.LookAt(patrolWayPoints[wayPointIndex].position);
		transform.position += transform.forward * Time.deltaTime * speed;
		returning = true;
	} else {
		returning = false;
	}
} else {
	//Patrol
	print("Potentially going to waypoint");
	returning = false;
	if(patrolWayPoints.Length > 0) {
		patrolTimer += Time.deltaTime;
		if((transform.position - patrolWayPoints[wayPointIndex].position).sqrMagnitude < .1 || patrolTimer >= patrolWaitTime) {
			if(wayPointIndex >= patrolWayPoints.Length - 1)
                wayPointIndex = 0;
            else
                wayPointIndex++;
			patrolTimer = 0;
		} else {
			patrolTimer = 0;
		}
		transform.LookAt(patrolWayPoints[wayPointIndex].position);
		transform.position += transform.forward * Time.deltaTime * speed;
	}
}


 
/**************************************************************************/
 
/****************************** Lose Conditions ***************************/
     var distToPlayer = (transform.position - player.transform.position).sqrMagnitude;
 
     if( distToPlayer < 1.0 ) {
        print ("You lost");
     }
 
/**************************************************************************/
}