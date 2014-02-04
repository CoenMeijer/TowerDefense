using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class GameControllScript : MonoBehaviour {

	private List<List<int>> mainGrid = new List<List<int>>();
	public TextAsset gridXml;
	// Use this for initialization
	void Start () {

		//loat xml document
		XmlDocument xml = new XmlDocument();//create a new Xml doc
		xml.LoadXml(gridXml.text);//
		
		//Read Xml File
		XmlNode root = xml.FirstChild;
		//list to sent to the wave controll later
		List<Vector3> startPoints = new List<Vector3> ();

		int i = 0;//workaround var to make foreach act as for loop
		foreach(XmlNode row in root.ChildNodes)
		{
			int j = 0;
			mainGrid.Insert(0,new List<int>());
			foreach(XmlNode tile in row.ChildNodes)
			{
				//make empty tile
				if(tile.Name == "o")
				{
					mainGrid[0].Add(0);
				}
				//make tile with terrain
				else if(tile.Name == "r")
				{
					mainGrid[0].Add(4);

					bool ajcUp = false;
					bool ajcDown = false;
					bool ajcLeft = false;
					bool ajcRight = false;
					string terrainName = "";

					if(i > 0)
					{
						//set ajcUp
						if(root.ChildNodes[i-1].ChildNodes[j].Name == "r")
						{
							ajcUp = true;
						}
					}
					if(i < root.ChildNodes.Count-1)
					{
						//set ajcDown
						if(root.ChildNodes[i+1].ChildNodes[j].Name == "r")
						{
							ajcDown = true;
						}
					}
					if(j > 0)
					{
						//set ajcLeft
						if(row.ChildNodes[j-1].Name == "r")
						{
							ajcLeft = true;
						}
					}
					if(j < row.ChildNodes.Count-1)
					{
						//set ajcRight
						if(row.ChildNodes[j+1].Name == "r")
						{
							ajcRight = true;
						}
					}

					if(ajcUp)
					{
						if(ajcDown)
						{
							if(ajcRight)
							{
								if(ajcLeft)
								{
									//make crossection
									terrainName = "Terrain#1";
								}
								else
								{
									//make t-junction(right)
									terrainName = "Corner_RightDown";
								}
							}
							else if(ajcLeft)
							{
								//make t-junction(left)
								terrainName = "Corner_LeftDown";
							}
							else
							{
								//make vertical wall
								terrainName = "Wall_Ver";
							}
						}
						else if(ajcRight)
						{
							if(ajcLeft)
							{
								//make t-junction(up)
								terrainName = "Corner_LeftUp";
							}
							else
							{
								//make corner(up,right)
								terrainName = "Corner_RightUp";
							}
						}
						else if(ajcLeft)
						{
							//make corner(up,left)
							terrainName = "Corner_LeftUp";
						}
						else
						{
							//make end(down)
							terrainName = "Wall_Ver";
						}
					}
					else if(ajcDown)
					{
						if(ajcRight)
						{
							if(ajcLeft)
							{
								//make t-junction(down)
								terrainName = "Corner_LeftDown";
							}
							else
							{
								//make corner(down,right)
								terrainName = "Corner_RightDown";
							}
						}
						else if(ajcLeft)
						{
							//make corner(down,left)
							terrainName = "Corner_LeftDown";
						}
						else
						{
							//make end(up)
							terrainName = "Corner_LeftDown";
						}
					}
					else if(ajcRight)
					{
						if(ajcLeft)
						{
							//make horizontal wall
							terrainName = "Wall_Hor";
						}
						else
						{
							//make end(left)
							terrainName = "Corner_RightDown";
						}
					}
					else if(ajcLeft)
					{
						//make end(right)
						terrainName = "Corner_LeftDown";
					}
					else
					{


						terrainName = "TerrainProp" + Mathf.Floor(Random.Range(1,4));
						Debug.Log(terrainName);


					}

					
					Instantiate(Resources.Load("Terrain/" + terrainName),new Vector3(j,root.ChildNodes.Count - (1+i),0f), Quaternion.identity);



				}
				//make tile with startpoint
				else if(tile.Name == "s")
				{
					mainGrid[0].Add(1);
					//add a startpoint to the list
					startPoints.Add(new Vector3(j,root.ChildNodes.Count - (1+i),0f));
				}
				//make tile with endpoint(one per stage)
				else if(tile.Name == "e")
				{
					Instantiate(Resources.Load("Terrain/Core"),new Vector3(j,root.ChildNodes.Count - (1+i),0f), Quaternion.identity);
					mainGrid[0].Add(1);
					GetComponent<PathFinder>().endPoint = new Vector3(j,root.ChildNodes.Count - (1+i),0f);
				}
				//make tile with player spawnpoint
				else if(tile.Name == "p")
				{
					Instantiate(Resources.Load("Terrain/PlayerSpawn"),new Vector3(j,root.ChildNodes.Count - (1+i),0f), Quaternion.identity);
					mainGrid[0].Add(1);
					GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>().setRespawnPoint(new Vector3(j,root.ChildNodes.Count - (1+i),0f));
				}
				
				j++;
			}
			i++;//workaround var to make foreach act as for loop
		}
		//sends startpoints to the wave controll
		GetComponent<WaveControll>().setSpawnPoints(startPoints.ToArray());

	}

	public void setGridTile(int xPos, int yPos, int type)
	{
		mainGrid[yPos][xPos] = type;
	}

	public int getGridTile(int xPos,int yPos)
	{
		if(xPos < mainGrid[0].Count-1 && yPos < mainGrid.Count-1 && xPos >= 0 && yPos >= 0)
		{
			return mainGrid[yPos][xPos];
		}
		else
		{
			return 1;
		}
	}
}
