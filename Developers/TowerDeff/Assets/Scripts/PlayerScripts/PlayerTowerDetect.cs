using UnityEngine;
using System.Collections;

public class PlayerTowerDetect : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		//when player gets near to a tower
		if(col.gameObject.tag == "Structure")
		{
			//make tower Clickeble
			col.gameObject.GetComponent<BaseStructureScript>().makeClickeble();
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		//when player moves away from a tower
		if(col.gameObject.tag == "Structure")
		{
			//make tower UnClickeble
			col.gameObject.GetComponent<BaseStructureScript>().makeUnClickeble(); 
		}
	}
}
