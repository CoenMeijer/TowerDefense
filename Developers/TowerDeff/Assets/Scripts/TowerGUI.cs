using UnityEngine;
using System.Collections;

public class TowerGUI : MonoBehaviour {
	private GameObject guiTarget;
	private BaseStructureScript BaseScript;
	private GameControllScript controlScript;
	private PlayerControll playerScript;
	public Sprite markerSprite;

	private float  standardResolutionX = 1024;
	private float  standardResolutionY = 768;

	public GUITexture heavyButtonTexture;
	public GUITexture sniperButtonTexture;
	public GUITexture rapidButtonTexture;
	public GUITexture sellButtonTexture;
	private GUIStyle style = GUIStyle.none;

	public GameObject selectArrow;
	
	void Start()
	{
		style.fontSize = 20;
		style.padding = new RectOffset(25, 0,5, 0);

		controlScript = gameObject.GetComponent<GameControllScript>();
		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>();

		
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.width / standardResolutionX, Screen.height /standardResolutionY, 1));
		
		float relativeScreenSize = Screen.width/standardResolutionX;
		
		
		Rect newInset = new Rect(885*relativeScreenSize,129*relativeScreenSize,125*relativeScreenSize,33*relativeScreenSize);
		rapidButtonTexture.pixelInset =newInset;
		
		newInset = new Rect(885*relativeScreenSize,95*relativeScreenSize,125*relativeScreenSize,33*relativeScreenSize);
		sniperButtonTexture.pixelInset =newInset;
		
		newInset = new Rect(885*relativeScreenSize,62*relativeScreenSize,125*relativeScreenSize,33*relativeScreenSize);
		heavyButtonTexture.pixelInset =newInset;
		
		newInset = new Rect(885*relativeScreenSize,16*relativeScreenSize,125*relativeScreenSize,33*relativeScreenSize);
		sellButtonTexture.pixelInset =newInset;
	}
	
	public void ShowGUI(GameObject thing)
	{
		guiTarget = thing;
		
		if(thing)
		{
			BaseScript = guiTarget.GetComponent<BaseStructureScript>();

			if(guiTarget.name == "BasicTower(Clone)")
			{
				rapidButtonTexture.enabled = true;
				sniperButtonTexture.enabled = true;
				heavyButtonTexture.enabled = true;
			}
			else{
				rapidButtonTexture.enabled = false;
				sniperButtonTexture.enabled = false;
				heavyButtonTexture.enabled = false;
			}
			sellButtonTexture.enabled = true;
			selectArrow.transform.position = new Vector3(guiTarget.transform.position.x,guiTarget.transform.position.y+0.8f,0);
			//guiTarget.transform.position;
			selectArrow.renderer.enabled = true;
		}

		else
		{
			BaseScript = null;
			rapidButtonTexture.enabled = false;
			sniperButtonTexture.enabled = false;
			heavyButtonTexture.enabled = false;
			sellButtonTexture.enabled = false;
			selectArrow.renderer.enabled = false;
		}
	}

	void FixedUpdate()
	{
		if(Input.GetMouseButtonDown(0))
		{
			RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			for(int i = 0; i < hit.Length ;i++)
			{
				if(hit[i].collider != null)
				{
					if(hit[i].transform.gameObject.tag == "Structure")
					{
						BaseStructureScript towScr = hit[i].transform.gameObject.GetComponent<BaseStructureScript>();

						if(towScr.isActive && towScr.isInRange)
						{
							ShowGUI(hit[i].transform.gameObject);
						}
					}
				}
			}
		}


	}
	
	void OnGUI()
	{
		if(BaseScript)
		{
			if(BaseScript.isInRange)
			{
				GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.width / standardResolutionX, Screen.height /standardResolutionY, 1));

				// Making the buttons
				Vector3 pos = guiTarget.transform.position;


				// When buttons are clicked, replace with sniper tower
				if(guiTarget.name == "BasicTower(Clone)")
				{
					if(GUI.Button(new Rect(885,605,125,33), new GUIContent ("Sniper", "-$30"), GUIStyle.none))
					{
						if(playerScript.changeCredits(-30))
						{
							Destroy(guiTarget);
							ShowGUI(Instantiate(Resources.Load("Structures/Snipe"),pos,  Quaternion.identity) as GameObject);
							guiTarget.GetComponent<BaseStructureScript>().makeClickeble();
							guiTarget.transform.GetChild(1).GetComponent<Leveler>().levelWithYas(-1.2f);

						}
					}
					// When buttons are clicked, replace with rapid tower
					if(GUI.Button(new Rect(885,639,125,33), new GUIContent ("Rapid", "-$35"), GUIStyle.none))
					{
						if(playerScript.changeCredits(-35))
						{
							Destroy(guiTarget);
							ShowGUI(Instantiate(Resources.Load("Structures/Rapid"),pos,  Quaternion.identity) as GameObject);
							guiTarget.GetComponent<BaseStructureScript>().makeClickeble();
							guiTarget.transform.GetChild(1).GetComponent<Leveler>().levelWithYas(-1.2f);
						}
					}
					// When buttons are clicked, replace with heavy tower
					if(GUI.Button(new Rect(885,673,125,33), new GUIContent ("Heavy", "-$30"), GUIStyle.none))
					{
						if(playerScript.changeCredits(-30))
						{
							Destroy(guiTarget);
							ShowGUI(Instantiate(Resources.Load("Structures/Heavy"),pos,  Quaternion.identity) as GameObject);
							guiTarget.GetComponent<BaseStructureScript>().makeClickeble();
							guiTarget.transform.GetChild(1).GetComponent<Leveler>().levelWithYas(-1.2f);
						}
					}
				}
				// When buttons are clicked, sell tower
				if(GUI.Button(new Rect(885,720,125,33), new GUIContent ("Sell", "+$10"), GUIStyle.none))
				{
					controlScript.setGridTile ((int)guiTarget.transform.position.x, (int)guiTarget.transform.position.y, 0);
					playerScript.changeCredits(10);
					Destroy(guiTarget);
					ShowGUI(null);
				}

				GUI.Label (new Rect (Screen.width + 55,Screen.height - 40,150,20), GUI.tooltip,style);
			}
			else
			{
				ShowGUI(null);
			}
		}
	}
}