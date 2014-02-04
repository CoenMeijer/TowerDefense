using UnityEngine;
using System.Collections;

public class Leveler : MonoBehaviour {



	public void levelWithYas (float yOffset) 
	{
		renderer.sortingOrder = 100 - (int)(Mathf.Round(transform.position.y) + yOffset);
	}
}
