using UnityEngine;
using System.Collections;

public class LevelLives : MonoBehaviour {

	public int lives;
	public GUITexture[] HealthTextures;
	public AudioClip loseSound;
	public AudioClip coreHit;

	private float  standardResolutionX = 1024;
	private float  standardResolutionY = 768;

	public void changeLives(int chng)
	{
		AudioSource.PlayClipAtPoint(coreHit, transform.position);

		HealthTextures[lives].enabled = false;
		lives += chng;

		Rect newInset = new Rect(Screen.width * 0.0165f, Screen.height * 0.053f,(20 / standardResolutionX) * Screen.width, (302 / standardResolutionY) * Screen.height);
		HealthTextures[lives].pixelInset = newInset;

		HealthTextures[lives].enabled = true;

		if(lives <= 0)
		{
			audio.Stop();
			AudioSource.PlayClipAtPoint(loseSound, transform.position);
			GetComponent<EndScreen>().enabled = true;
			GetComponent<EndScreen>().haveWon = false;


			//GameObject[] allEnemys = GameObject.FindGameObjectsWithTag("Creep");
			foreach(GameObject enem in GameObject.FindGameObjectsWithTag("Creep"))
			{
				enem.GetComponent<Enemy>().takeDmg(5000);
			}
		}
	}
	void Update()
	{
		Rect newInset = new Rect(Screen.width * 0.0165f, Screen.height * 0.053f,(20 / standardResolutionX) * Screen.width, (302 / standardResolutionY) * Screen.height);
		HealthTextures[lives].pixelInset = newInset;
	}
}
