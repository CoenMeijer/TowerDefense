using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private float health;
	private int lootValue;


	public void takeDmg(float dmg)
	{
		health -= dmg;

		if(health <= 0f)
		{
			GameObject loot = Instantiate(Resources.Load("Coins"),transform.position, transform.rotation) as GameObject;
			loot.GetComponent<EnemyDrops>().setValue(lootValue);


			
			Instantiate (Resources.Load ("EnemyDeath"), transform.position, transform.rotation);
			Destroy(gameObject);

		}
	}

	public void setStats(int value,int hlth,int frms)
	{
		lootValue = value;
		health = hlth;

		GetComponent<WaypointScript> ().SetFramesToTarget (frms);
	}
	
}
