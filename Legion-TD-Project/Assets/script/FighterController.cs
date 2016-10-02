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
	FighterAnimator anim;
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
		anim = GetComponent<FighterAnimator> ();
		obs = GetComponent<NavMeshObstacle> ();
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
		WalkAnim (nextPathPoint);
	}

	public void SetDestination(Transform target){
		ag.SetDestination (target.position);
		ag.Resume ();
	}

	public void WalkAnim(Transform position){
		obs.enabled = false;
		ag.enabled = true;
		SetDestination (position);
		if(anim != null)
			anim.SetAnimBool ("walk", 1);
		ag.avoidancePriority = 50;
	}

	public void FightAnimPrepare(){ 
		currentStatus = enemyStatus.Fight;
		ag.avoidancePriority = 30;
		ag.enabled = false;
		obs.enabled = true;
	}

	public void IdleAnim(){
		if(anim != null)
			anim.Idle ();
	}

	public void OnChildTriggerEnter(Collider other){
		if (other.CompareTag (targetTag)) {
			if (visibleTargets.Count == 0) {
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
				seek.enabled = true;
			}
			else {
				seek.enabled = false;
				currentStatus = enemyStatus.Move;
			}
		}
	}
}
