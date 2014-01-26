#pragma strict

public var fieldOfViewAngle : float = 110f;
public var playerInSight : boolean;
public var personalLastSighting : Vector3;

private var nav : NavMeshAgent;
private var col : SphereCollider;
private var anim : Animator;
private var lastPlayerSighting : LastPlayerSighting;
private var player : GameObject;
private var playerAnim : Animator;
//private var hash : HashIDs;
private var previousSighting : Vector3;

function Awake() { 
	col = GetComponent(SphereCollider);
	anim = GetComponent(Animator);
	lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent(LastPlayerSighting);
	player = GameObject.FindGameObjectWithTag(Tags.player);
	playerAnim = player.GetComponent(Animator);
	//hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent(Tags.HashIDs);

	personalLastSighting = lastPlayerSighting.resetPosition;
	previousSighting = lastPlayerSighting.resetPosition;
	
}	


function Start () {

}

function Update () {
	if(lastPlayerSighting.position != previousSighting) {
		personalLastSighting = lastPlayerSighting.position;
	}
	
	previousSighting = lastPlayerSighting.position;
	//Maybe add flag for if player is alive
	//For animation
	
}

function OnTriggerStay(other : Collider) {
	if( other.gameObject == player ){
		playerInSight = false;
		
		var direction : Vector3 = other.transform.position - transform.position;
		var angle : float = Vector3.Angle(direction, transform.forward);
	
		if(angle < fieldOfViewAngle * 0.5f) {
			var hit : RaycastHit;
			
			if(Physics.Raycast(transform.position + transform.up, direction.normalized, hit, col.radius)) {
				if(hit.collider.gameObject == player){
					playerInSight = true;
					
					lastPlayerSighting.position = player.transform.position;
				}
			
			}
			
		}
	
//	var playerLayerZeroStateHash : int = playerAnim.GetCurrentAnimatorStateInfo(0).nameHash;
 //   var playerLayerOneStateHash : int = playerAnim.GetCurrentAnimatorStateInfo(1).nameHash;
	
	//if(playerLayerZeroStateHash == hash.locomotionState || playerLayerOneStateHash == hash.shoutState){
		if(CalculatePathLength(player.transform.position) <= col.radius) {
			personalLastSighting = player.transform.position;
		}
	//}
	
	
	}
	}
	
	function OnTriggerExit(other : Collider) {
		if(other.gameObject == player)
			playerInSight = false;
	}
	
	function CalculatePathLength(targetPosition : Vector3) {
		var path : NavMeshPath = new NavMeshPath();
		if(nav.enabled) {
			nav.CalculatePath(targetPosition, path);
		}
		var allWayPoints : Vector3[] = new Vector3[path.corners.Length + 2];
		allWayPoints[0] = transform.position;
		
		allWayPoints[allWayPoints.Length - 1] = targetPosition;
		
		for(var i = 0; i < path.corners.Length; i++) {
			allWayPoints[i + 1] = path.corners[i];
		}
		
		var pathLength : float = 0;
		
		for(var j = 0; j < allWayPoints.Length - 1; j++){
			pathLength += Vector3.Distance(allWayPoints[j], allWayPoints[j+1]);
		}
		return pathLength;
		
	}
