using UnityEngine;
using System.Collections;

public class EnemyDrops : MonoBehaviour {

	private int value;
	public float timeToDespawn;
	public float warningTime;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		timeToDespawn -= Time.deltaTime;

		if(timeToDespawn < warningTime)
		{
			renderer.material.color = Color.red;

			if(timeToDespawn < 0)
			{
				Destroy(gameObject);
			}
		}
	}

	public void setValue(int v)
	{
		value = v;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<PlayerControll>().changeCredits(value);
			Destroy(gameObject);
		}
	}
}
