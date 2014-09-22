using UnityEngine;
using System.Collections;

public class GroundScript : MonoBehaviour {
	public int hp;
	Vector2 iniposition;
	Quaternion _rotation;
	// Use this for initialization
	void Start () {
		hp = 10;
		iniposition = new Vector2 (transform.position.x, transform.position.y);
		_rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = iniposition;
		transform.rotation = _rotation;
	}

	/*void OnCollisionEnter2D(Collision2D c) {
		print(hp);
		hp--;
		if(hp <= 0) {
			Destroy(gameObject);
		}
	}*/
	void OnCollisionEnter2D(Collision2D c) {
		print ("destroy");
		if(c.gameObject.tag == "fireball"){
			Destroy(c.gameObject);
			print ("destroy");
		}
	}
}
