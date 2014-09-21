using UnityEngine;
using System.Collections;
public class Walker : Behaviour {

	public float jumpHeight;
	private char dir = '.';
	public float maxSpeed = 2f;
	public float noChangeProb = .97f;
	// Update is called once per frame
	void Update () {
	}

	void LateUpdate(){

		}
	void Awake(){
	}
	public override void Init(){
		rigidbody2D.gravityScale = 1;
		state = Behaviour.States.Idle;
		this.enabled = true;
		target = GameObject.FindGameObjectWithTag ("Player");
		StartCoroutine ("UpdateState");
	}
#region Idle
	public override bool Idle(){
		//walk left and right based on MARKOV CHAIN algorithm
		float r = Random.value;
		Vector2 f = new Vector2 ();
		//a: Left
		//.: Still
		//d: Right
		switch (dir) {
		case 'a':
			f.x = -speed;
			if(r < noChangeProb){
				dir = 'a';
			}
			else if(r < noChangeProb + (1 - (noChangeProb)) / 2){
				dir = 'd';
			}
			else{
				dir = '.';
			}
			break;
		case '.':
			if(r < noChangeProb){
				dir = '.';
			}
		else if(r < noChangeProb + (1 - (noChangeProb)) / 2){
				dir = 'a';
			}
			else{
				dir = 'd';
			}
			break;
		case 'd':
			f.x = speed;
			if(r < noChangeProb){
				dir = 'd';
		}
		else if(r < noChangeProb + (1 - (noChangeProb)) / 2){
				dir = '.';
			}
			else{
				dir = 'a';
			}
			break;
		}
		f *= Time.deltaTime * 100;
		if(rigidbody2D.velocity.magnitude < maxSpeed/2)
			rigidbody2D.AddForce (f);
		return true;
	}
#endregion

	public override bool Attacking(){

		if(target != null){
			Vector2 f = new Vector2();
			float dy = rigidbody2D.position.y - target.rigidbody2D.position.y;
			float dx = rigidbody2D.position.x - target.rigidbody2D.position.x;

			//horizontal speed
			if(dx < 0){
				f.x = maxSpeed;
			}
			if(dx > 0){
				f.x = -maxSpeed;
			}
			if(dy < -1.5 && Mathf.Abs(dx) > 2f && Mathf.Abs(dx) < 4f) Jump();
			f *= Time.deltaTime * 100;
			if(rigidbody2D.velocity.magnitude < maxSpeed)
				rigidbody2D.AddForce (f);
		}
		return true;
	}
	public override bool Boasting(){
		return true;
	}
	public override bool Running(){
		return true;
	}

	private void Jump(){

		if(IsGrounded()) rigidbody2D.AddForce (Vector2.up * jumpHeight * Time.deltaTime * 1000 + rigidbody2D.velocity);
	}

	//if the npc is grounded, return true;
	public bool IsGrounded(){
		return Physics2D.Raycast (rigidbody2D.position, -Vector2.up, .65f);
	}

	//returns the float max of a given arraylist
	private float ArrMax(ArrayList l){
		float max = 0;
		int len = l.Count;
		for (int i = 0; i < len; ++i) {
			max = Mathf.Max((float)l[i], max);
		}
		print (max);
		return max;
	}
}
