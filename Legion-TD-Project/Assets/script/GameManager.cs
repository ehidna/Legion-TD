using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	[System.Serializable]
	public class Team {
		public string teamName;
		public string tag;
		public Player[] player;

		[System.Serializable]
		public class Player{
			public string playerName;
			public Transform spawnPoint;
			public ResourceController resource;
		}
	}
	public Team[] team;

	[Header("StartStats")]
	public int startMoney;
	public int startLumber;
	public int startIncome;
	public int startKingKills;

	[Header("Prefabs")]
	public GameObject numberEffect;

	public static GameManager instance;
	void Awake (){
		if (instance != null){
			Debug.Log("More than one GameManager in scene!");
			return;
		}
		instance = this;
	}

	[HideInInspector]
	public bool building;

	void OnEnable(){// Creating Team and players with resourceManager
		for (int i = 0; i < instance.team.Length; i++) {
			GameObject empty = new GameObject ();
			empty.name = instance.team[i].teamName;
			empty.tag = instance.team [i].tag;
			for (int j = 0; j < instance.team[i].player.Length; j++) {
				CreatePlayer (instance.team[i].player[j], empty.transform);
			}
		}
	}

	void CreatePlayer(Team.Player _player, Transform parent){
		GameObject empty = new GameObject ();
		empty.name = _player.playerName;
		empty.transform.SetParent (parent);
		_player.resource = empty.AddComponent<ResourceController> ();
		ResetResource (_player.resource);
		SetParent (empty.transform, empty.name);
	}

	void ResetResource(ResourceController rc){
		rc.numberEffect = this.numberEffect;
		rc.Money = startMoney;
		rc.Lumber = startLumber;
		rc.Income = startIncome;
		rc.kingKills = startKingKills;
	}

	void SetParent(Transform parent, string name){
		GameObject MercenaryPlace = GameObject.Find ("MercenariesPlace_"+name);
		GameObject MainPlace = GameObject.Find ("MainPlace_"+name);
		GameObject ActionPlace = GameObject.Find ("ActionPlace_"+name);
		MercenaryPlace.transform.SetParent (parent);
		MainPlace.transform.SetParent (parent);
		ActionPlace.transform.SetParent (parent);
	}

}
