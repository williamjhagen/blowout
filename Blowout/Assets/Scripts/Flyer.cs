using UnityEngine;
using System.Collections;

public class Flyer : Behaviour {
	private float startElevation;
	private bool airborne = true;
	private float airTimer = 0;
	public float airResetTime = 1f;
	private float deltaStart = 0;
	private float timer = 0;
	public float recoverTime;
	private Vector2 swoopTarget;
	private enum AttackStates{
		SwoopAttack,
		SwoopRecover,
		Positioning
	}
	public float swoopHorF;
	public float swoopVerF;
	public Vector2 maxSwoop;
	public Vector2 minSwoop;
	private enum IdleStates{
		hover,
		goUp,
		stabilizing
	}
	private AttackStates attackState = AttackStates.Positioning;
	private IdleStates idleState = IdleStates.hover;
	// Use this for initialization
	void Awake () {
	}

	// Update is called once per frame
	void Update () {
	}
	
	public override void Init(){
		state = Behaviour.States.Idle;
		target = GameObject.FindGameObjectWithTag ("Player");
		rigidbody2D.gravityScale = 0;
		startElevation = rigidbody2D.position.y;
		rigidbody2D.AddForce (Vector2.up * 15f);
		StartCoroutine ("CheckOffset");
		StartCoroutine ("UpdateState");
		this.enabled = true;

	}

	public override bool Idle(){
		switch (idleState) {
		case IdleStates.hover:
			rigidbody2D.AddForce(Vector2.up * deltaStart);
			break;
		case IdleStates.goUp:
			if(deltaStart < .35f) {
				//rigidbody2D.velocity = Vector2.zero;
				idleState = IdleStates.stabilizing;
			}
			   
			rigidbody2D.AddForce(Vector2.up * deltaStart * 2);
			break;
		case IdleStates.stabilizing:
			rigidbody2D.AddForce(Vector2.up * -2f);
			rigidbody2D.velocity *= .91f;
			if(rigidbody2D.velocity.y < .3f) idleState = IdleStates.hover;
			break;
		}
		return true;
	}
	public override bool Attacking(){
		print (attackState);
		Vector2 distFromTarget = target.rigidbody2D.position - rigidbody2D.position;
		switch (attackState) {
		case AttackStates.Positioning:
			//try to get to attack range
			bool swoopTime = true;
			if(Mathf.Abs(distFromTarget.x) < minSwoop.x){
				swoopTime = false;
				Vector2 t = rigidbody2D.velocity;
				t.x *= .99f;
				rigidbody2D.velocity = t;

			}
			else if(Mathf.Abs(distFromTarget.x) > maxSwoop.x ){
				swoopTime = false;
				rigidbody2D.AddForce(Vector2.right * ((swoopHorF/2) * distFromTarget.x * Time.deltaTime));
			}
			if(Mathf.Abs(distFromTarget.y) < minSwoop.y){
				swoopTime = false;
				Vector2 t = rigidbody2D.velocity;
				t.y *= .95f;
				rigidbody2D.velocity = t;
			}
			else if(Mathf.Abs(distFromTarget.y) > maxSwoop.y){
				swoopTime = false;
				rigidbody2D.AddForce(Vector2.up * ((swoopVerF/2) * distFromTarget.y * Time.deltaTime));
			}
			//GET EM
			RaycastHit2D rh2;
			if(rh2 = Physics2D.Raycast(rigidbody2D.position, distFromTarget, distFromTarget.magnitude)){
				if(rh2 != target) swoopTime = false;
			}
			if(swoopTime){
				attackState = AttackStates.SwoopAttack;
				swoopTarget = target.rigidbody2D.position;
				rigidbody2D.velocity *= .75f;
			}

			RaycastHit2D rh;
			Debug.DrawRay(rigidbody2D.position, Vector2.right * (distFromTarget.x>0? 2:-2), Color.red);
			//object in the way, avoid it
			if(Physics2D.Raycast(rigidbody2D.position, Vector2.right * (distFromTarget.x>0? 1f:-1f), 2f) != target){
					rigidbody2D.AddForce(Vector2.up * -3 * Time.deltaTime);
			}
			break;
		case AttackStates.SwoopAttack:
			Vector2 distFromSwoopTarget = swoopTarget - rigidbody2D.position;
			if(Mathf.Abs(distFromSwoopTarget.x) < .1f){
				attackState = AttackStates.SwoopRecover;
				break;
			}
			Vector2 sV = distFromSwoopTarget;
			sV.y *= swoopVerF;
			sV.x = swoopHorF * 3;
			rigidbody2D.AddForce(sV * Time.deltaTime);
			break;
		case AttackStates.SwoopRecover:
			if(timer > recoverTime){
				attackState = AttackStates.Positioning;
			}
			else{
				timer += Time.deltaTime;
				Vector2 t = rigidbody2D.velocity;
				t.x *= .95f;
				rigidbody2D.velocity = t;
				rigidbody2D.AddForce(Vector2.up * swoopVerF * 5 * Time.deltaTime);
			}
			break;
		}
		return true;
	}
	public override bool Boasting(){
		return true;
	}
	public override bool Running(){
		return true;
	}

	IEnumerator CheckOffset(){
		while (true) {
			deltaStart = (startElevation -rigidbody2D.position.y);
			if(deltaStart > .35f){
				idleState = IdleStates.goUp;
			}
			yield return null;
		}
	}
}
