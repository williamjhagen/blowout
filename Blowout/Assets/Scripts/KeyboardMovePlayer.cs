using UnityEngine;
using System.Collections;

public class KeyboardMovePlayer : MonoBehaviour {

	private float speed = 1.5f;
	private float spacing = 1.0f;
	private Vector2 pos;
	
	void Start() 
	{
		pos.x = transform.position.x;
		pos.y = transform.position.y;
	}
	
	void Update() 
	{
		if (Input.GetKeyDown(KeyCode.W))
			rigidbody2D.AddForce(Vector3.up * 100 * Time.deltaTime);
		/*if (Input.GetKeyDown(KeyCode.S))
			pos.y -= spacing;
		if (Input.GetKeyDown(KeyCode.A))
			pos.x -= spacing;
		if (Input.GetKeyDown(KeyCode.D))
			pos.x += spacing;
		
		transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);*/
	}
}
