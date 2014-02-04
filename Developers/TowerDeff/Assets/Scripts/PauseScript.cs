using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	private bool gamePaused = false;


	void Update () {
		if(gamePaused == false)
		{
			if(Input.GetKeyUp(KeyCode.P))
			{
				Debug.Log("Pause Pressed");
				gamePaused = true;
				Time.timeScale = 0;
			}
		}

		else if(gamePaused == true)
		{
			if(Input.GetKeyUp(KeyCode.P))
			{
				gamePaused = false;
				Time.timeScale = 1;
				Debug.Log("Unpause Pressed");
			}
		}
	}
}
