using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingPlacer : MonoBehaviour {
	
	public float placeRange;
	public List<string> pLacebleObjects;
	public List<int> placedObjectPrices;

	private bool buildModeOn = false;
	private GameObject heldObject;
	private int heldObjectNum;
	private BaseStructureScript heldObjectScript;


	// Update is called once per frame
	void Update () 
	{
		//toggles buildmode on and off
		if(Input.GetButtonDown("BuildmodeToggle"))
		{
			//buildModeOn turns off
			if(buildModeOn)
			{
				buildModeOn = false;
				Destroy(heldObject);
			}
			//buildmode turns on
			else
			{
				if(GameObject.FindGameObjectsWithTag("Creep").Length == 0)
				{
					buildModeOn = true;
					setHeldObject();
				}
			}
		}

		//when in buildmode
		if(buildModeOn)
		{
			if(GameObject.FindGameObjectsWithTag("Creep").Length == 0)
			{
				//when scolling up with the mouseWheel
				if(Input.GetAxis("CycleBuildObject") > 0)
				{
					heldObjectNum ++;
					ModifyHeldNum();
					
				}
				//when scolling down with the mouseWheel
				else if(Input.GetAxis("CycleBuildObject") < 0)
				{
					heldObjectNum --;
					ModifyHeldNum();
				}

				/*******Tower placement**********\\
				 *Placing tower via mouse position 
				 */
				//get position of the mouse
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				Vector3 pos = ray.GetPoint(5);
				//snap new position to grid
				pos = new Vector3(Mathf.Round(pos.x),Mathf.Round(pos.y),0f);
				pos -= heldObject.transform.position;
				//move tower to new position
				heldObject.transform.Translate(pos);

				
				// place tower/disconnect tower from player
				if(Input.GetButtonDown("PlaceTower"))
				{
					if(gameObject.GetComponent<PlayerControll>().changeCredits(-placedObjectPrices[heldObjectNum]))
					{
						//check if the tower is in a valid position to be placed, if so, it will also place the tower
						if(heldObjectScript.makeActive())
						{
							
							//heldObject.transform.GetChild(1).gameObject.renderer.material.color = Color.white;

							//disconnect structure from player & turn on buildmode
							heldObject = null;
							buildModeOn = false;

						}
						else
						{
							gameObject.GetComponent<PlayerControll>().changeCredits(placedObjectPrices[heldObjectNum]);
						}
					}
				}
			}
			else
			{
				buildModeOn = false;
				Destroy(heldObject);
			}
		}

	}

	private void ModifyHeldNum()
	{
		if(heldObjectNum > pLacebleObjects.Count-1)
		{
			heldObjectNum = 0;
		}
		else if(heldObjectNum < 0)
		{
			heldObjectNum = pLacebleObjects.Count-1;
		}
		Destroy(heldObject);
		setHeldObject();
	}


	//sets heldObject to the object with the name in pLacebleObjects at position 'heldObjectNum'
	void setHeldObject()
	{
		heldObject = Instantiate(Resources.Load("Structures/" + pLacebleObjects[heldObjectNum]),transform.position, Quaternion.identity) as GameObject;
		heldObject.transform.GetChild(1).gameObject.renderer.material.color = new Color(0.4f,0f,0f,0.5f);
		heldObjectScript = heldObject.gameObject.GetComponent<BaseStructureScript> ();
		heldObjectScript.controllScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllScript>();
	}
}
