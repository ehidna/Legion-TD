using UnityEngine;
using System.Collections;

public class Town : MonoBehaviour {

	[SerializeField]
	Transform spawn;
	[SerializeField]
	GameObject worker;
	GameObject[] trees;

	[SerializeField]
	private float workerCooldown;
	private float countdown;

	private int availableTreeIndex;
	private int workerNumber;

	private bool spawning;

	ResourceController resource;

	// Use this for initialization
	void Start () {
		trees = GameObject.FindGameObjectsWithTag ("Tree");
		resource = GameObject.Find ("ResourceManager").GetComponent<ResourceController> ();
		availableTreeIndex = 0;
		StartCoroutine ("InitiateWorker", 1);
		StartCoroutine ("InitiateWorker", 1);
	}

	IEnumerator InitiateWorker(float wait){
		yield return new WaitForSeconds (wait);
		GameObject work = Instantiate (worker, spawn.position, spawn.rotation) as GameObject;
		work.GetComponent<WorkerMotor> ().setTown (spawn);
		work.GetComponent<WorkerMotor> ().setTree (trees[availableTreeIndex].transform);
		Vector3 pos = work.transform.position;
		pos.y = 0.5f;
		work.transform.position = pos;
		availableTreeIndex++;
		workerNumber++;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawning) {
			countdown -= 1.0f * Time.deltaTime;
			Debug.Log (countdown.ToString ());
			if(countdown <= 0)
				spawning = false;
		}

	}

	void OnMouseDown(){
		if (resource.Money < 50)
			return;
			

		if (workerNumber < trees.Length) {
			Debug.Log ("Isci geliyorr!");
			StartCoroutine ("InitiateWorker", workerCooldown);
			resource.BuyWorker (50);
			countdown = workerCooldown;
			spawning = true;
		}
	}
}
