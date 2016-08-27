using UnityEngine;
using System.Collections;

public class NumberEffect : MonoBehaviour {
	
	[SerializeField]
	float speed;
			
	void Update () {
		transform.rotation = Quaternion.Euler (0, 0, 0);
		Vector3 pos = transform.position;
		pos.y += speed * Time.deltaTime;
		transform.position = pos;
	}
}
