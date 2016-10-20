using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {

	[System.Serializable]
	public class Wave {
		public string name;
		public Transform enemy;
		public int count;
		public float spawnRate;
	}
	public Wave[] Waves;

	ResourceController resource;
	ResetFighters reset;

	private GameObject dragDropPlatform;
	public GameObject waveInfo;

	public bool enemiesAlive = false;
	public bool spawning = false;
	public float timeBetweenWaves;
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
		resource = GameManager.instance.team[0].player[0].resource;
		reset = GetComponent<ResetFighters> ();
		dragDropPlatform = GameObject.Find ("Platform");
		InvokeRepeating ("WaveTracker", 0f, 1f);
	}

	void WaveTracker () {
		if (waveCountdown == 0 && !spawning) {
			if (GameObject.FindGameObjectsWithTag ("Enemy_Player").Length == 0 && GameObject.FindGameObjectsWithTag ("Enemy_AI").Length == 0) {
				waveNumber += 1;
				enemiesAlive = false;

				waveInfo.SetActive (true);
				waveInfo.GetComponent<WaveInfo> ().WaveCleared (resource.Money, waveIndex + 1, timeBetweenWaves, resource.Income, resource.kingKills);
				waveCountdown = timeBetweenWaves;

				for (int i = 0; i < GameManager.instance.team.Length; i++) {
					for (int j = 0; j < GameManager.instance.team [i].player.Length; j++) {
						GameManager.instance.team[i].player[j].resource.AddIncomeToMoney ();
						GameManager.instance.team[i].player[j].resource.AddKingRewardToMoney ();
					}
				}
			}	
		}
		if (!spawning && waveCountdown > 0) {
			GameManager.instance.building = true;
			reset.enabled = true;
			dragDropPlatform.SetActive (true);
			BuildManager.instance.enabled = true;
		}
	}

	void Update () {
		waveCountdown -= Time.deltaTime;

		if (waveCountdown == 0 && enemiesAlive == false) {
			GameManager.instance.building = false;

			dragDropPlatform.SetActive (false);
			GameObject circlePlatform = GameObject.Find ("CirclePlatform(Clone)");
			Destroy (circlePlatform);
			Touch.instance.busy = false;
			BuildManager.instance.enabled = false;

			reset.enabled = false;
			StartCoroutine ( SpawnWave ( Waves[waveIndex] ) );
			return;
		}
	}

	public IEnumerator SpawnWave (Wave wave) {

		enemiesAlive = true;
		spawning = true;

		yield return new WaitForSeconds (0.5f);

		for (int i = 0; i < GameManager.instance.team.Length; i++) {
			for (int j = 0; j < GameManager.instance.team[i].player.Length; j++) {
				StartCoroutine (InstantiateWave(wave, GameManager.instance.team[i].player[j]));		
			}
		}

		if (waveIndex < Waves.Length - 1) {
			waveIndex++;
			spawning = false;
		}
		else {
			Debug.Log("Game Over");
		}
	}

	IEnumerator InstantiateWave(Wave wave, GameManager.Team.Player player){
		for (int i = 0; i < wave.count; i++) {
			Transform enemy = Instantiate (wave.enemy, player.spawnPoint.position + Random.insideUnitSphere * 2, player.spawnPoint.transform.rotation)as Transform;
			enemy.tag = "Enemy_" + player.playerName; 
			enemy.GetComponent<Fighter>().playerName = player.playerName; 
			SphereCollider radiusCollider = enemy.GetComponentInChildren<SphereCollider> ();
			FighterStats stats = enemy.GetComponent<FighterStats> ();
			radiusCollider.radius = stats.viewRadius;
			yield return new WaitForSeconds (1f/wave.spawnRate);
		}
		yield return new WaitForSeconds (1.0f);

		InstantiateMercenary (player.spawnPoint, player.playerName);// Mercenaries coming
	}

	void InstantiateMercenary(Transform pos, string name){
		GameObject[] mercenaries = GameObject.FindGameObjectsWithTag ("Mercenary_" + name);
		for (int i = 0; i < mercenaries.Length; i++) {
			mercenaries[i].transform.position = pos.position + Random.insideUnitSphere * 2;
			mercenaries [i].tag = "Enemy_" + name; 
			mercenaries[i].GetComponent<NavMeshAgent> ().enabled = true;
			mercenaries[i].GetComponent<FighterController> ().currentStatus = FighterController.enemyStatus.Move;
			mercenaries[i].GetComponent<Collider> ().enabled = true;

			SphereCollider radiusCollider = mercenaries[i].GetComponentInChildren<SphereCollider> ();
			FighterStats stats = mercenaries[i].GetComponent<FighterStats> ();
			radiusCollider.radius = stats.viewRadius;
			radiusCollider.enabled = true;
		}
	}

}
