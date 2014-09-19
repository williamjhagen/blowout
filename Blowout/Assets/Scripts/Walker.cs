using UnityEngine;
using System.Collections;
public class Walker : Behaviour {

	public float jumpHeight;
	private char dir = '.';
	public float maxSpeed = 2f;
	public float noChangeProb = .97f;
	ArrayList DYPerFrames;
	private bool grounded;
	// Update is called once per frame
	void Update () {
		DYPerFrames.Add (0.0f);
	}

	void LateUpdate(){

		}
	void Start(){
		DYPerFrames = new ArrayList ();
		state = Behaviour.States.Idle;
		grounded = true;
	}
	public override void Tick(){
		//update grounded information
		DYPerFrames.Insert (0, Mathf.Abs(rigidbody2D.velocity.y));
		if (DYPerFrames.Count > 3)
						DYPerFrames.RemoveAt (3);
		if (!grounded) {
			grounded = (ArrSum(DYPerFrames) < .00001f);
		}
		//update based on state
		if (state == Behaviour.States.Idle) {
			Idle ();
		}
		else if (state == Behaviour.States.Attacking) {
			Attacking ();
		}
		else if (state == Behaviour.States.Boasting) {
			Boasting ();
		}
		else if (state == Behaviour.States.Running) {
			Running ();
		}
	}
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
		Jump ();
		f *= Time.deltaTime * 100;
		if(rigidbody2D.velocity.magnitude < maxSpeed/2)
			rigidbody2D.AddForce (f);
		return true;
	}
	public override bool Attacking(){
		if(target != null){
			Vector2 f = new Vector2();
			float dy = rigidbody2D.position.y - target.rigidbody2D.position.y;
			float dx = rigidbody2D.position.x - target.rigidbody2D.position.x;

			if(dx < 0){

			}
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

		if(grounded) rigidbody2D.AddForce (Vector2.up * jumpHeight * Time.deltaTime * 300);
		grounded = false;
	}

	//if the npc is grounded, return true;
	public bool IsGrounded(){
		return grounded;
	}

	//returns the float sum of a given arraylist
	private float ArrSum(ArrayList l){
		float sum = 0;
		int len = l.Count;
		for (int i = 0; i < len; ++i) {
			sum += (float)l[i];
		}
		return sum / len;
	}
}
