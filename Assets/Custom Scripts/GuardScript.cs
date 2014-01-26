using UnityEngine;
using System.Collections;

/**
 * The array should always be >= 2 in size. For !stationary, at least 2 points to patrol. For stationary, size is fixed at 2. [0] is guarding point, [1] is looking at point.
 */
public class GuardScript : MonoBehaviour {

	public bool isStationary = false;
	bool shouldMove = true;
	float speed = 5.0f;
	float chaseDistance = 10.0f;
	float maxAngleOfVision = 30.0f;
	int state;//1 for patrolling, 3 for chasing
	public Transform[] waypoints;
	int wayPointToWalkTo = 0;
	float wayPointTolerance = 2.0f;
	float instakillRadius = 2.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit forwardLookHit;
		Vector3 direction;
		Ray forwardRay;
		Vector3 walkTowards = waypoints[wayPointToWalkTo].position;
		state = 1;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (!player.GetComponent<CharacterMotor> ().isNinja) {
						for (float yShift = 0.0f; yShift <= 2.0f; yShift+=1.0f) {
								for (float angle = -maxAngleOfVision; angle <= 0; angle += -maxAngleOfVision/(3*angle)) {
										direction = (Quaternion.AngleAxis (angle, Vector3.up) * transform.forward).normalized;
										Debug.DrawRay (transform.position + Vector3.up * yShift, direction * chaseDistance * 1, Color.black);
										forwardRay = new Ray (transform.position + Vector3.up * yShift, direction);
										if (Physics.Raycast (forwardRay, out forwardLookHit, chaseDistance)) {
												if (forwardLookHit.collider.tag == "Player") {
														walkTowards = forwardLookHit.collider.transform.position;
														state = 3;
												}
										}
								}
								//make sure to check at 0
								direction = transform.forward.normalized;
								Debug.DrawRay (transform.position + Vector3.up * yShift, direction * chaseDistance * 1, Color.black);
								forwardRay = new Ray (transform.position + Vector3.up * yShift, direction);
								if (Physics.Raycast (forwardRay, out forwardLookHit, chaseDistance)) {
										if (forwardLookHit.collider.tag == "Player") {
												walkTowards = forwardLookHit.collider.transform.position;
												state = 3;
										}
								}
								for (float angle = maxAngleOfVision; angle > 0; angle -= maxAngleOfVision/(3*angle)) {
										direction = (Quaternion.AngleAxis (angle, Vector3.up) * transform.forward).normalized;
										Debug.DrawRay (transform.position + Vector3.up * yShift, direction * chaseDistance * 1, Color.black);
										forwardRay = new Ray (transform.position + Vector3.up * yShift, direction);
										if (Physics.Raycast (forwardRay, out forwardLookHit, chaseDistance)) {
												if (forwardLookHit.collider.tag == "Player") {
														walkTowards = forwardLookHit.collider.transform.position;
														state = 3;
												}
										}
								}
						}
				}
		if ((player.transform.position - transform.position).magnitude < instakillRadius)
						EgoSystem.interactWithGuard (true);

			//walk towards the current waypoint
		if (state == 3) {
			EgoSystem.interactWithGuard(false);
			//Debug.Log ("SEEN");
			if((transform.position - walkTowards).magnitude < wayPointTolerance) {
				EgoSystem.interactWithGuard(true);
				//Debug.Log ("DEAD DEAD DEAD");
			}
		}

		if (state == 1) {
						//walkTowards = waypoints[wayPointToWalkTo].position;
						if ((transform.position - walkTowards).magnitude < wayPointTolerance) {
								if (!isStationary)
										wayPointToWalkTo = (1 + wayPointToWalkTo) % waypoints.Length;
								else
										shouldMove = false;
						} else if (isStationary)
								shouldMove = true;
				}
		if(state == 3) {
			shouldMove = true;
		}
		Vector3 deltaPosition;
			if (!shouldMove) {
				deltaPosition = Vector3.zero;
				walkTowards = waypoints [1].position;
			} else {
				deltaPosition = (walkTowards - transform.position).normalized * speed * Time.deltaTime;
				deltaPosition.y = 0;
				walkTowards.y = 6.0f;
			}
			transform.LookAt (walkTowards);
			transform.position += deltaPosition;
	}

}
