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
	NavMeshObstacle obs;

	[Header("PATH")]
	[SerializeField]
	private Transform[] pathPoints;
	private int currentPathPoint;
	private Transform nextPathPoint;
	private float nextWaypointDistance = 1;

	void Start () {
		stat = GetComponent<FighterStats> ();
		stat.setHealth (stat.getMaxHealth ());

		ag = GetComponent<NavMeshAgent> ();
		ag.speed = stat.getSpeed ()/2;
		ag.acceleration = Mathf.Pow (ag.speed, 2);

		currentPathPoint = 0;
		nextPathPoint = pathPoints [currentPathPoint];

		seek = GetComponent<EnemySeeker> ();
		obs = GetComponent<NavMeshObstacle> ();
	}

	void Update () {
		if (currentPathPoint < pathPoints.Length && currentStatus == enemyStatus.Move)
			Move ();
	}

	private void Move (){
		obs.enabled = false;

		if (!ag.isActiveAndEnabled)
			ag.enabled = true;

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

	public void SetDestination(Transform target){
		ag.SetDestination (target.position);
		ag.Resume ();
	}

	public void OnChildTriggerEnter(Collider other){
		if (other.CompareTag (targetTag)) {
			if (visibleTargets.Count == 0) {
				currentStatus = enemyStatus.Fight;
				if(ag.isActiveAndEnabled)
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
				if(ag.isActiveAndEnabled)
					ag.Stop ();
				seek.enabled = true;
			}
			else {
				seek.enabled = false;
				currentStatus = enemyStatus.Move;
				if(ag.isActiveAndEnabled)
					ag.Resume ();
			}
		}
	}
}
