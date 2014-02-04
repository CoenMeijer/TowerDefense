using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControll: MonoBehaviour {

	public float moveSpeed;

	public int maxHealth;
	private int currentHealth;
	public float immuneTime;
	private float immuneTimeLeft;
	private bool immune = false;

	public float respawnTime;
	private float respawnTimeLeft;

	public int credits;

	public bool lookingRight;
	public bool lookingLeft;
	public bool lookingUp;
	public bool lookingDown;

	private Animator animator;
	public GUIText scrapGui;

	private Vector3 spawnPoint;
	private bool isRespawning = false;
	private Leveler spriteLeveler;

	public GUITexture[] playerHealth;

	private float  standardResolutionX = 1024;
	private float  standardResolutionY = 768;

	// Use this for initialization
	void Start () {

		//Debug.Log(renderer.material.color);

		currentHealth = maxHealth;

		scrapGui.text = "$ " + credits;
		animator = GetComponent<Animator>();
		spriteLeveler = GetComponent<Leveler> ();
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		spriteLeveler.levelWithYas (-0.9f);

		if(!isRespawning){
			if(immune){
				immuneTimeLeft -= Time.deltaTime;

				if(immuneTimeLeft <= 0){
					renderer.material.color = Color.white;
					immune = false;
				}
			}

			//.........move player(non-diagonal).....\\
			Vector2 movement = new Vector2();
			rigidbody2D.velocity = Vector2.zero;
			if(Input.GetButton("Vertical"))
			{
				animator.SetInteger("Horizontal", 0);

				if(Input.GetAxis("Vertical") > 0)
				{
					movement.y = moveSpeed;
					clearLookDirection();
					lookingUp = true;
					animator.SetInteger("Vertical", 1);
				}
				else
				{
					movement.y = -moveSpeed;
					clearLookDirection();
					lookingDown = true;
					animator.SetInteger("Vertical", -1);
				}
			}
			else if(Input.GetButton("Horizontal"))
			{
				animator.SetInteger("Vertical", 0);

				if(Input.GetAxis("Horizontal") > 0)
				{
					movement.x = moveSpeed;
					clearLookDirection();
					lookingRight = true;
					animator.SetInteger("Horizontal", 1);
				}
				else
				{
					movement.x = -moveSpeed;
					clearLookDirection();
					lookingLeft = true;
					animator.SetInteger("Horizontal", -1);
				}
			}
			else{
				
				animator.SetInteger("Vertical", 0);
				animator.SetInteger("Horizontal", 0);
			}
			rigidbody2D.AddForce(movement);

		}

		else{
			audio.Play();
			respawnTimeLeft -= Time.deltaTime;				
			if(respawnTimeLeft <= 0){
				changeHealth(maxHealth);
				animator.SetBool("Respawning",false);
				transform.position = spawnPoint;
				isRespawning = false;
				renderer.material.color = Color.white;
			}
		}
	}

	void Update(){
		Rect newInset = new Rect(Screen.width * 0.0525f, Screen.height * 0.076f,(20 / standardResolutionX) * Screen.width, (150 / standardResolutionY) * Screen.height);
		playerHealth[currentHealth].pixelInset = newInset;

		scrapGui.fontSize = (int)((Screen.width / standardResolutionX) * 17);
	}

	private void clearLookDirection()
	{
		lookingDown = false;
		lookingUp = false;
		lookingRight = false;
		lookingLeft = false;

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(!isRespawning){
			if(!immune){
				if(col.gameObject.tag == "Creep")
				{
					changeHealth(-1);
				}
			}
		}
	}

	void changeHealth(int change)
	{
		Debug.Log(currentHealth);
		playerHealth[currentHealth].enabled = false;

		//Player takes dmg

		currentHealth += change;
		playerHealth[currentHealth].enabled = true;
		//change hp indicator
	
		
		if(currentHealth > 0){
			//activate immume timer
			immune = true;
			immuneTimeLeft = immuneTime;
			renderer.material.color = Color.red;
			
		}
		
		else {
			Debug.Log("Death");
			changeCredits(-(int)Mathf.Ceil(credits/2));
			respawnTimeLeft = respawnTime;
			isRespawning = true;
			animator.SetBool("Respawning",true);
			//renderer.material.color = new Color(0,0,0,0);
		}
	}

	public void setRespawnPoint(Vector3 setSpawn){

		spawnPoint = setSpawn;
		transform.position = spawnPoint;
	}

	public bool changeCredits(int cAdd)
	{
		if(credits + cAdd >= 0)
		{
			credits += cAdd;
			scrapGui.text = "$ " + credits;

			Debug.Log("NewCredits = " + credits);
			return true;
		}
		else
		{	
			return false;
		}

	}
}
