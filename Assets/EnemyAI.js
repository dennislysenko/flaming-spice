#pragma strict

public var patrolSpeed : float = 2f;
public var chaseSpeed : float = 3f;
public var chaseWaitTime : float = 5f;
public var patrolWaitTime : float = 1f;
public var patrolWayPoint : Transform[];

//private var enemySight : EnemySight;
private var nav : NavMeshAgent;
private var player : Transform;
//private var lastPlayerSighting : LastPlayerSighting;
private var chaseTimer : float;
private var patrolTimer : float;
private var wayPointIndex : int;

function Start () {

}

function Update () {

}