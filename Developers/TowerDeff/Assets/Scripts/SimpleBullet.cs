using UnityEngine;
using System.Collections;

public class SimpleBullet : MonoBehaviour {

	private GameObject target;
	private float bulletSpeed;
	private float bulletDmg;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(target)
		{
			Vector3 relative = transform.InverseTransformPoint(target.transform.position);
			float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
			transform.Rotate( 0, 0,angle);
			//transform.LookAt(target.transform.position);
			transform.Translate(new Vector3(bulletSpeed,0f,0f));
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void setStats(GameObject tar,float speed,float dmg)
	{
		target = tar;
		bulletDmg = dmg;
		bulletSpeed = speed;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject == target)
		{
			target.GetComponent<Enemy>().takeDmg(bulletDmg);
			Destroy(gameObject);
		}
	}
}
