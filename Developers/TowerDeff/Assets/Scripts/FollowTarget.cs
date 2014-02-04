using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public GameObject target;
	public int followDrag;
	
	// Update is called once per frame
	void Update () {
		Vector3 distance = target.transform.position - transform.position;
		distance.z = 0;
		
		transform.Translate (distance / followDrag);
	}
}
