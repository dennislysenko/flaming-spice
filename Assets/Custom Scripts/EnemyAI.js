#pragma strict

public var patrolSpeed : float = 2f;
public var chaseSpeed : float = 3f;
public var chaseWaitTime : float = 5f;
public var patrolWaitTime : float = 1f;
public var patrolWayPoints : Transform[];


private var enemySight : EnemySight;

private var destination : Vector3;
private var speed : float;
//private var nav : NavMeshAgent;
private var player : Transform;
private var lastPlayerSighting : LastPlayerSighting;
private var chaseTimer : float;
private var patrolTimer : float;
private var wayPointIndex : int;


function Awake() {
	enemySight = GetComponent(EnemySight);
	//nav = GetComponent(NavMeshAgent);
	player = GameObject.FindGameObjectWithTag("Player").transform;
	lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent(LastPlayerSighting);
}


function Start () {

}

function Update () {
	if(enemySight.playerInSight || enemySight.personalLastSighting != lastPlayerSighting.resetPosition) {
		Chasing();
	} else {
		Patrolling();
	}
}

function Chasing() {
	var sightingDeltaPos : Vector3 = enemySight.personalLastSighting - transform.position;
	
	destination = enemySight.personalLastSighting;
	speed = chaseSpeed;
	
	
	chaseTimer += Time.deltaTime;
	if(chaseTimer >= chaseWaitTime) {
		lastPlayerSighting.position = lastPlayerSighting.resetPosition;
		enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
		chaseTimer = 0f;
	}
}

function Patrolling() {
	speed= patrolSpeed;
	if(destination == lastPlayerSighting.resetPosition) {
		patrolTimer += Time.deltaTime;
		
		if(patrolTimer >= patrolWaitTime) {
			if(wayPointIndex == patrolWayPoints.Length - 1)
                wayPointIndex = 0;
            else
                wayPointIndex++;
		
			patrolTimer = 0;
		}
		
	} else {
		patrolTimer = 0;
	}
	
	destination = patrolWayPoints[wayPointIndex].position;
}