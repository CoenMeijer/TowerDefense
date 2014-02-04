using UnityEngine;
using System.Collections;

public class GuiControll : MonoBehaviour {

	private float  standardResolutionX = 1024;
	private float  standardResolutionY = 768;

	private WaveControll waveScript;
	public GUITexture waveButton;


	void Start()
	{
		waveScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<WaveControll>();

		float relativeScreenSize = Screen.width/standardResolutionX;
		
		Rect newInset = new Rect(154*relativeScreenSize,726*relativeScreenSize,125*relativeScreenSize,33*relativeScreenSize);
		waveButton.pixelInset = newInset;
	}


	void OnGUI () {


		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.width / standardResolutionX, Screen.height /standardResolutionY, 1));
	



		// Make the next wave button.
		if(!waveScript.isActive())
		{
			waveButton.enabled = true;
			if(GUI.Button(new Rect(153,9,125,33), new GUIContent ("New wave","nextWave"),GUIStyle.none)) 
			{
				waveScript.makeActive();
			}
		}
		else{
			waveButton.enabled = false;

		}
	}

}
