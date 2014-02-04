using UnityEngine;
using System.Collections;

public class UIScaler : MonoBehaviour {

	public GUITexture FullGUI;

	void Update () {
		Rect newInset = new Rect(-(Screen.width / 2), -(Screen.height / 2), Screen.width, Screen.height);
		//Rect (x, y, width, height) from the bottom left corner.
		
		FullGUI.pixelInset = newInset;
	}
}
