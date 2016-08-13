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

	public float timeBetweenWaves;

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

		InvokeRepeating ("WaveTracker", 0f, 1f);
	}

	void WaveTracker () {
		if (waveCountdown == 0 && !spawning) {
			if (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0) {
				enemiesAlive = false;
				waveCountdown = timeBetweenWaves;
			}	
		}
	}

	void Update () {
		waveCountdown -= Time.deltaTime;

		if (waveCountdown == 0 && enemiesAlive == false) {
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
