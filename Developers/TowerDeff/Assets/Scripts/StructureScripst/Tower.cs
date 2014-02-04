using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

	public float fireDelay;
	private float fireDelayLeft;
	public float bulletDamage;
	public float bulletSpeed;
	private List<GameObject> targetList = new List<GameObject>();
	public string bulletName;


	// Update is called once per frame
	void Update () {

		fireDelayLeft -= Time.deltaTime;


		if(targetList.Count > 0)
		{
			if(!targetList[0])
			{
				targetList.RemoveAt(0);
			}
			
			if(targetList.Count > 0)
			{
				//transform.LookAt(targetList[0].transform.position);
				Vector3 relative = transform.InverseTransformPoint(targetList[0].transform.position);
				float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
				transform.Rotate( 0, 0,angle);
				
				if(fireDelayLeft < 0f)
				{
					fireDelayLeft = fireDelay;
					GameObject bullet = Instantiate(Resources.Load("Bullets/"+ bulletName),transform.position, transform.rotation) as GameObject;
					bullet.GetComponent<SimpleBullet>().setStats(targetList[0],bulletSpeed,bulletDamage);
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{

		if(col.gameObject.tag == "Creep")
		{
			targetList.Add(col.gameObject);
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.tag == "Creep")
		{
			targetList.Remove(col.gameObject);
		}
	}
}
