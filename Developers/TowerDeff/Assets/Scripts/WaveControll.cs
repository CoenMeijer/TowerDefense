using UnityEngine;
using System.Collections;
using System.Xml;

public class WaveControll : MonoBehaviour {

	private int enemyCount;
	private int waveIndex = 0;
	private float spawnDelay;
	private float spawndelayLeft;
	private bool waveActive;
	public TextAsset waveInfoXml;
	private XmlNode waveInfo;
	private Vector3[] spawnPoints;
	private int spawnPointNum;

	public float waveDelay;
	private float waveDelayLeft;

	public GUIText waveTimeGUI;

	private float  standardResolutionX = 1024;

	// Use this for initialization
	void Start () {
		//load xml document
		XmlDocument xml = new XmlDocument();//create a new Xml doc
		xml.LoadXml(waveInfoXml.text);//
		
		//Read Xml File
		waveInfo = xml.FirstChild;

		waveDelayLeft = waveDelay + 20;
	}
	
	// Update is called once per frame
	void Update () 
	{

		waveTimeGUI.fontSize = (int)((Screen.width / standardResolutionX) * 24);

		if(waveActive)
		{
			//spawn timer counts down
			spawndelayLeft -= Time.deltaTime;

			//when timer for single enemy runs out
			if(spawndelayLeft < 0)
			{
				
				//---end wave here if no enemys are left in the wave---\\
				if(enemyCount <= 0)
				{
					//waveIndex++;


					//check if all waves of the stage have been used
					if(waveIndex > waveInfo.ChildNodes.Count-2)
					{
						//after all waves are done, this waits untill the stage is clear of creeps
						if(GameObject.FindGameObjectsWithTag("Creep").Length == 0)
						{
							//end stage here
							Debug.Log("Stage has ended!!");
							waveActive = false;

							if (GameObject.FindGameObjectWithTag("GameController").GetComponent<EndScreen>().haveWon != false) {
								GameObject.FindGameObjectWithTag("GameController").GetComponent<EndScreen>().enabled = true;
								GameObject.FindGameObjectWithTag("GameController").GetComponent<EndScreen>().haveWon = true;
							}

						}
					}
					else 
					{
						if(GameObject.FindGameObjectsWithTag("Creep").Length == 0)
						{
							waveIndex++;

							waveActive = false;
							waveDelayLeft = waveDelay;
						}
					}
				}
				//--spawn enemy here--\\
				else
				{
					//reset timer
					spawndelayLeft = float.Parse(waveInfo.ChildNodes[waveIndex]["spawndelay"].InnerText);
					//cycle through spawnpoints with "spawnPointNum"
					if(spawnPointNum > spawnPoints.Length -1)
					{
						spawnPointNum = 0;
					}
					//spawn enemy
					GameObject newCreep = Instantiate(Resources.Load("Creep"),spawnPoints[spawnPointNum], Quaternion.identity) as GameObject;
					//calls a function in the enemy to set its stats defined in the XML(lootvalue,health,speed)
					newCreep.GetComponent<Enemy>().setStats(int.Parse(waveInfo.ChildNodes[waveIndex]["dropvalue"].InnerText),int.Parse(waveInfo.ChildNodes[waveIndex]["health"].InnerText),int.Parse(waveInfo.ChildNodes[waveIndex]["framesperwaypoint"].InnerText));
					//count down enemys left in wave
					enemyCount--;
					spawnPointNum ++;
				}
			}
		}
		else
		{
			waveDelayLeft -= Time.deltaTime;
			if(Mathf.Round(waveDelayLeft) > 10)
			{
				waveTimeGUI.text = "00:" + Mathf.Round(waveDelayLeft);
			}
			else
			{
				waveTimeGUI.text = "00:0" + Mathf.Round(waveDelayLeft);
			}
		
			if(waveDelayLeft < 0)
			{
				makeActive();
			}

		}
	}

	public void makeActive()
	{
		if(!waveActive)
		{
			waveTimeGUI.text = "Active";
			spawnPointNum = 0;
			waveActive = true;
			enemyCount = int.Parse(waveInfo.ChildNodes[waveIndex]["creepcount"].InnerText);
		}
	}
	public bool isActive()
	{
		return waveActive;
	}
	public void setSpawnPoints(Vector3[] points)
	{
		spawnPoints = points;
	}
	public Vector3[] getSpawnPoints()
	{
		return spawnPoints;
	}
}
