using UnityEngine;
using System.Collections;

public abstract class Behaviour : MonoBehaviour {
	public bool active = false;
	public float speed;
	protected GameObject target;
	public float threatRange;
	public float lodRange = 50.0f;
	
	public Sprite flySprite;
	public Sprite groundSprite;
	public enum States{
		Idle,
		Attacking, 
		Boasting,
		Running,
		Dead
	}
	public States state;
	
	public void Tick(){
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

	public void Deactivate(){
		Vector2 temp = new Vector2 (9001, 9001);
		rigidbody2D.position = temp;
		this.enabled = false;
	}

	public abstract void Init();
	//public abstract void CleanUp();
	public abstract bool Idle();
	public abstract bool Attacking();
	public abstract bool Boasting();
	public abstract bool Running();

	public IEnumerator UpdateState(){
		while (true) {
			Vector3 t = transform.localScale;
			if(rigidbody2D.velocity.x > 0 && transform.localScale.x > 0){
				t.x = transform.localScale.x * -1;
				transform.localScale = t;
			}
			else if(rigidbody2D.velocity.x < 0 && transform.localScale.x < 0){
				t.x = transform.localScale.x * -1;
				transform.localScale = t;			}
			Vector2 d = rigidbody2D.position - target.rigidbody2D.position;
			if(state == States.Idle){
				if(d.magnitude < threatRange){
					state = States.Attacking;
				}
			}
			else if(state == States.Attacking){
				if(d.magnitude > threatRange * 2){
					state = States.Idle;
				}
			}
			if(Mathf.Abs(d.x) > lodRange)
			{
				state = States.Dead;
			}
			yield return state;
		}
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			//coll.gameObject.renderer.material.color = Color.red;
			this.state = States.Dead;
		}
	}
}