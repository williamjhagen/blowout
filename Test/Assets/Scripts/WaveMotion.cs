using UnityEngine;
using System.Collections;

public class WaveMotion : MonoBehaviour
{
	//private Vector3 pointB;
	
	/*IEnumerator Start()
	{
		var pointA = new Vector3(transform.position.x + 2.0f, transform.position.y, transform.position.z);
		var pointB = new Vector3(transform.position.x - 2.0f, transform.position.y, transform.position.z);

		while(true)
		{
			yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
			yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
		}
	}
	
	IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		var i= 0.0f;
		var rate= Random.Range (1.0f, 3.0f)/time;
		var direction = Random.Range (-1, 2);
		if(direction == 0)
			direction++;

		while(i < 1.0f)
		{
			i += direction * Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null;
		}
	}*/

	public float min=2f;
	public float max=3f;

	public float minY;
	public float maxY;

	private float speed;
	private float speedY;
	// Use this for initialization
	void Start () {
		
		min=transform.position.x-2;
		max=transform.position.x+2;	

		minY = transform.position.y - 0.3f;
		maxY = transform.position.y + 0.3f;

		speed  = Random.Range (-2.0f,2.0f);
		speedY  = Random.Range (-0.5f,0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
		
		transform.position =new Vector3(Mathf.PingPong(Time.time*speed*2,max-min)+min, Mathf.PingPong(Time.time*speedY*2,maxY-minY)+minY, transform.position.z);
		
	}
}

