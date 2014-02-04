using UnityEngine;
using System.Collections;

public class EndScreen : MonoBehaviour {

	public bool haveWon;

	void OnGUI()
	{
		
		GUI.Box(new Rect((Screen.width/2) -80,(Screen.height/2)-50,160,180), new GUIContent ("TowerGUI", ""));
		//GUI.Box(new Rect(Screen.width - 170,Screen.height - 170,150,150), new GUIContent ("TowerGUI", ""));

		if(GUI.Button(new Rect((Screen.width/2) -60,(Screen.height/2)-20,120,40), new GUIContent ("Restart")))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		if(GUI.Button(new Rect((Screen.width/2) -60,(Screen.height/2)+25,120,40), new GUIContent ("Back To Title")))
		{
			Application.LoadLevel("TestMenu");
		}

		if(haveWon)
		{
			if(GUI.Button(new Rect((Screen.width/2) -60,(Screen.height/2)+70,120,40), new GUIContent ("Next Level")))
			{
				Application.LoadLevel(Application.loadedLevel + 1);
			}
		}
	}
}
