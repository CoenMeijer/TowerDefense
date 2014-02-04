using UnityEngine;
using System.Collections;

public class BaseStructureScript : MonoBehaviour {

	public GameControllScript controllScript;
	public int structureType;
	public bool isInRange = false;
	public bool isActive = false;
	public GameObject secondObject;


	public bool makeActive()
	{
		if(controllScript.getGridTile((int)transform.position.x,(int)transform.position.y) != 0 || !isInRange)
		{
			//return that the tower could not be placed (here due to ether there already being something at this position
			//or it being to far away from the player and outside of placing range
			return false;
		}
		else
		{

			//change grid as if the tower is now there
			controllScript.setGridTile ((int)transform.position.x, (int)transform.position.y, structureType);

			//---check to see if placing this tower whould block a path compleatly---\\
			Vector3[] spawnPoints = GameObject.FindGameObjectWithTag("GameController").GetComponent<WaveControll>().getSpawnPoints();
			for(int i = 0; i < spawnPoints.Length; i++)
			{
				//check if a path can be found between the endpoint and startpoint "i" or not
				if(GameObject.FindGameObjectWithTag("GameController").GetComponent<PathFinder>().makePath(spawnPoints[i]) == null)
				{

					//revert change to grid so this structure is no longer there
					controllScript.setGridTile ((int)transform.position.x, (int)transform.position.y, 0);

					//return that the tower could not be placed (here due to it blocking a path)
					return false;
				}
			}
			//make colliders active
			gameObject.collider2D.isTrigger = false;
			if(secondObject)
			{
				secondObject.collider2D.enabled = true;
			}

			transform.GetChild(1).gameObject.GetComponent<Leveler>().levelWithYas(-1.2f);

			//return that the tower has been placed
			isActive = true;
			return true;
		}
	}

	//.....Will allow the tower to be clickable.....\\
	public void makeClickeble()
	{
		isInRange = true;
		if(isActive)
		{
			transform.GetChild(1).gameObject.renderer.material.color = new Color(0.9f,0.9f,1f);
		}
		else
		{
			transform.GetChild(1).gameObject.renderer.material.color = new Color(0f,0f,0f,0.5f);
		}
	}

	//.....Will make the tower unclickeble.....\\
	public void makeUnClickeble()
	{
		isInRange = false;
		if(isActive)
		{
			transform.GetChild(1).gameObject.renderer.material.color = Color.white;
		}
		else
		{
			transform.GetChild(1).gameObject.renderer.material.color = new Color(0.4f,0f,0f,0.5f);
		}
	}
}
