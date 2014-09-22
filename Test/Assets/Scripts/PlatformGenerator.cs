using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {
	
	public GameObject platform;
	//public GameObject platformSpike;
	private GameObject player;
	private GameObject cam;
	
	//private int levelsCleared = 0;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find ("rainbowMan_v6");
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		Invoke ("SpawnPlatforms", 5.0f);
	}

	
	public void SpawnPlatforms()
	{
		//levelsCleared++;
		
		float xPos = 31f;
		float playerHeight = player.collider2D.bounds.size.y;
		for(int i = 0; i < Random.Range (0,3); i++)
		//for(int i = 0; i < 3; i++)
		{
			//introduce some variance in x value of placement
			xPos = cam.transform.position.x + 31f + (i*7) + Random.Range (-1.5f,1.5f);
			
			//y value ensures player has space to fly above or below platforms
			float yPos = Random.Range (-8.0f, 9.0f);
			
			GameObject newPlatform;
			
			newPlatform = Instantiate(platform, new Vector2(xPos, yPos), Quaternion.identity) as GameObject;
			
			/*float offset = 0.2f;
			
			//50% chance of spikes
			if(Random.Range (0,2) == 0)
			{
				GameObject spike;
				int sideOfSpike = Random.Range (0,4);
				
				if(sideOfSpike == 0)
					spike = Instantiate(platformSpike, new Vector2(xPos, yPos + newPlatform.collider2D.bounds.size.y/2 + offset), Quaternion.Euler(0,0,0)) as GameObject;
				else if(sideOfSpike == 1)
					spike = Instantiate(platformSpike, new Vector2(xPos, yPos - newPlatform.collider2D.bounds.size.y/2 - offset), Quaternion.Euler(0,0,180)) as GameObject;
				else if(sideOfSpike == 2)
					spike = Instantiate(platformSpike, new Vector2(xPos - newPlatform.collider2D.bounds.size.x/2 - offset, yPos), Quaternion.Euler(0,0,90)) as GameObject;
				else if(sideOfSpike == 3)
					spike = Instantiate(platformSpike, new Vector2(xPos + newPlatform.collider2D.bounds.size.x/2 + offset, yPos), Quaternion.Euler(0,0,-90)) as GameObject;
				else
					return;
				
				spike.transform.parent = pBG.transform;*/
				//if(Random.Range (0,2) == 1)
				//newPlatform.transform.rotation = Quaternion.Euler(0,0,180);
			}
			
			//newPlatform.transform.parent = pBG.transform;
			//print ("added to parent : " + pBG);
		//}

		Invoke ("SpawnPlatforms", 5.0f);
	}
}
