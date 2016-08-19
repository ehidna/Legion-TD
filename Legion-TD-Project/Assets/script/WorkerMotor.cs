using UnityEngine;
using System.Collections;

public class WorkerMotor : MonoBehaviour {

	private Transform tree;
	private Transform town;

	private bool full;
	private bool cutting;

	public float speed;
	public int lumberIncome;

	ResourceController resource;

	public void setTree(Transform _tree){
		tree = _tree;
	}
	public void setTown(Transform _town){
		town = _town;
	}
	// Use this for initialization
	void Start () {
		full = false;
		cutting = false;
		resource = GameObject.Find ("ResourceManager").GetComponent<ResourceController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (cutting)
			return;
		if (full) { // move back to town for to unspike trees
			MoveToTarget (town, false);
		} else { // move to cut tree
			MoveToTarget (tree, true);
		}
	}
	void MoveToTarget(Transform target, bool state){
		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame){
			if (state) {// for tree
				StartCoroutine ("Cutting");
				return;
			}
			resource.EmptyResource (gameObject);
			full = false;// for town
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}
	IEnumerator Cutting(){
		cutting = true;
		Debug.Log ("Cutting Tree!"); // cutting animation start
		yield return new WaitForSeconds(5.0f);
		full = true;
		cutting = false;
	}
}
