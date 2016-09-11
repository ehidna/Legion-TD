using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {

	[System.Serializable]
	public class Wave {
		public string name;
		public Transform enemy;
		public int count;
		public float spawnRate;
		public GameObject SpawnPoint;
	}

	ResourceController resource;
	ResetFighters reset;

	public float timeBetweenWaves;
	public GameObject waveInfo;

	public Wave[] Waves;

	public bool enemiesAlive = false;
	public bool spawning = false;
	private static float _waveCountdown = 0f;
	public static float waveCountdown {
		get { return _waveCountdown; }
		set { _waveCountdown = Mathf.Clamp (value, 0, Mathf.Infinity); }
	}

	public static int waveNumber = 1;

	int waveIndex = 0;

	void Start () {
		waveNumber = 1;
		waveCountdown = timeBetweenWaves;
		resource = GameObject.Find ("ResourceManager").GetComponent<ResourceController> ();
		reset = GetComponent<ResetFighters> ();
		InvokeRepeating ("WaveTracker", 0f, 1f);
	}

	void WaveTracker () {
		if (waveCountdown == 0 && !spawning) {
			if (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0) {
				enemiesAlive = false;

				waveInfo.SetActive (true);
				waveInfo.GetComponent<WaveInfo> ().WaveCleared (resource.Money, waveIndex + 1, timeBetweenWaves, resource.Income);

				waveCountdown = timeBetweenWaves;
				resource.AddIncomeToMoney ();
			}	
		}
		if (!spawning && waveCountdown > 0) {
			GameManager.instance.building = true;
			reset.enabled = true;
		}
	}

	void Update () {
		waveCountdown -= Time.deltaTime;

		if (waveCountdown == 0 && enemiesAlive == false) {
			GameManager.instance.building = false;
			reset.enabled = false;
			StartCoroutine ( SpawnWave ( Waves[waveIndex] ) );
			return;
		}
	}

	public IEnumerator SpawnWave (Wave wave) {

		enemiesAlive = true;
		spawning = true;

		yield return new WaitForSeconds (0.5f);

		for (int i = 0; i < wave.count; i++) {
			Transform enemy = Instantiate (wave.enemy, wave.SpawnPoint.transform.position + Random.insideUnitSphere * 2, wave.SpawnPoint.transform.rotation)as Transform;
			enemy.name = "Enemy" + i; 
			yield return new WaitForSeconds (1f/wave.spawnRate);
		}
		yield return new WaitForSeconds (1.0f);

		GameObject[] mercenaries = GameObject.FindGameObjectsWithTag ("Mercenary");
		for (int i = 0; i < mercenaries.Length; i++) {
			mercenaries[i].transform.position = wave.SpawnPoint.transform.position + Random.insideUnitSphere * 2;
			mercenaries[i].tag = "Enemy"; 
			mercenaries[i].GetComponent<NavMeshAgent> ().enabled = true;
			mercenaries[i].GetComponent<FighterController> ().currentStatus = FighterController.enemyStatus.Move;
			mercenaries[i].GetComponent<Collider> ().enabled = true;
		}

		waveNumber += 1;

		if (waveIndex < Waves.Length - 1) {
			waveIndex++;
			spawning = false;
		}
		else {
			Debug.Log("Game Over");
		}
	}

}
