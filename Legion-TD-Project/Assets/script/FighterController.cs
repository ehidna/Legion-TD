using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FighterController : MonoBehaviour {

	//	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	[HideInInspector]
	public NavMeshAgent ag;

	public enum enemyStatus{Move, Fight, Idle};
	public enemyStatus currentStatus;

	[SerializeField]
	private string targetTag;

	EnemySeeker seek;
	FighterStats stat;

	[Header("PATH")]
	[SerializeField]
	private Transform[] pathPoints;
	private int currentPathPoint;
	private Transform nextPathPoint;
	private float nextWaypointDistance = 1;

	void OnDisable(){
		currentStatus = enemyStatus.Idle;
		Reset (); //position, stat vb.
	}

	void Reset(){
		if (stat == null)
			return;
		transform.position = stat.getPosition ();
		stat.setHealth (stat.getMaxHealth ());
	}

	void Start () {
		stat = transform.GetComponent<FighterStats> ();
		stat.setHealth (stat.getMaxHealth ());

		ag = transform.GetComponent<NavMeshAgent> ();
		currentPathPoint = 0;
		nextPathPoint = pathPoints [currentPathPoint];

		seek = transform.GetComponent<EnemySeeker> ();

	}

	void Update () {
		if (currentPathPoint < pathPoints.Length && currentStatus == enemyStatus.Move)
			Move ();
	}

	private void Move (){
		if (Vector3.Distance (transform.position, nextPathPoint.position) < nextWaypointDistance) {
			currentPathPoint++;
			if (currentPathPoint == pathPoints.Length) {
				return;
			}
		}
		//			FindNextPoint ();
		nextPathPoint = pathPoints [currentPathPoint];
		if (visibleTargets.Count == 0) {
			ag.SetDestination (nextPathPoint.position);
		} 
	}

	public void OnChildTriggerEnter(Collider other){
		if (other.CompareTag (targetTag)) {
			if (visibleTargets.Count == 0) {
				currentStatus = enemyStatus.Fight;
				ag.Stop ();
				seek.enabled = true;
			}
			visibleTargets.Add (other.gameObject.transform);
		}
	}

	public void OnChildTriggerExit(Collider other){
		ColliderExit (other);
	}

	public void ColliderExit(Collider other){
		if (!visibleTargets.Find(r=> r.transform == other.transform))
			return;

		if (other.CompareTag (targetTag)) {
			visibleTargets.Remove(other.transform);
			if (visibleTargets.Count > 0) {
				if(ag != null)
					ag.Stop ();
				seek.enabled = true;
			}
			else {
				seek.enabled = false;
				currentStatus = enemyStatus.Move;
				ag.Resume ();
			}
		}
	}
}
