using UnityEngine;
using System.Collections;

public class WaypointScript : MonoBehaviour {
	
	private int framesToTarget;

	private Vector3[] pointArray;
	private int waypointIndex = 0;
	private Vector3 startDist;
	private Animator animator;

	void Start () {
		animator = GetComponent<Animator>();

		//define array using pathfinding
		pointArray = GameObject.FindGameObjectWithTag("GameController").GetComponent<PathFinder>().makePath(transform.position);
		startDist = (pointArray[waypointIndex] - transform.position) / framesToTarget;
	}

	public void SetFramesToTarget(int frms)
	{
		framesToTarget = frms;


		//attempt to change speed of animation(failed)
		//float aS = 60/frms;
		//animation["MoveUp"].speed = aS;
		//animation["MoveLeft"].speed = aS;
		//animation["MoveRight"].speed = aS;
		//animation["MoveDown"].speed = aS;
		//animator.speed = 0.5f;
	}

	void Update () {
	
		transform.Translate(startDist);

		if(Vector3.Distance(pointArray[waypointIndex], transform.position) < 0.05){
			waypointIndex ++;
			if(waypointIndex > pointArray.Length-1){
				Destroy(gameObject);
				GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelLives>().changeLives(-1);
			}

			else 
			{
				startDist = (pointArray[waypointIndex] - transform.position) / framesToTarget;

				if(Mathf.Abs(startDist.x) > Mathf.Abs(startDist.y))
				{
					animator.SetInteger("Horizontal", (int)(startDist.x / Mathf.Abs(startDist.x)));
					animator.SetInteger("Vertical", 0);
				}
				else
				{
					animator.SetInteger("Vertical", (int)(startDist.y / Mathf.Abs(startDist.y)));
					animator.SetInteger("Horizontal", 0);
				}
			}
		}

	}
}
