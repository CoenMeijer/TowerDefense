using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
	void Start()
	{

	}

	void OnMouseDown()
	{
		if(gameObject.tag == "ExitButton")
		{
			Application.Quit();
		}
		
		if(gameObject.tag == "PlayButton")
		{
			Application.LoadLevel("TestArea");
		}

		if(gameObject.tag == "HowToButton")
		{
			Application.LoadLevel("TestHowTo");
		}

		//if(gameObject.tag == "CreditsButton")
		//{
			//Application.LoadLevel("");
		//}
	}
}
