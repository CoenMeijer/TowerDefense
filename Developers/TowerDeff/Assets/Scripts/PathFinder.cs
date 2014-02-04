using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PathFinder : MonoBehaviour {

	private GameControllScript gridScript;
	public Vector3 endPoint;
	List<Vector3> usedPoints;
	List<Tpoint> pendingPoints;

	void Start()
	{
		gridScript = GetComponent<GameControllScript>();
	}

	public Vector3[] makePath(Vector3 enemyPos)
	{
		usedPoints = new List<Vector3>();
		pendingPoints = new List<Tpoint>();

		pendingPoints.Add(new Tpoint());
		//ScriptableObject.CreateInstance("Tpoint");
		pendingPoints[0].position = enemyPos;

		while(pendingPoints.Count > 0)
		{
			if(checkSurrounding(pendingPoints[0]))
			{
				List<Vector3>  finalList = new List<Vector3>();
				Tpoint checkingPoint = pendingPoints[0];

				finalList.Add(endPoint);

				while(checkingPoint.parent != null)
				{
					finalList.Add(checkingPoint.position);
					checkingPoint = checkingPoint.parent;
				}
				
				finalList.Add(checkingPoint.position);

				finalList.Reverse();
				return finalList.ToArray();
			}
			usedPoints.Add(pendingPoints[0].position);
			pendingPoints.RemoveAt(0);

			//sort array when new point are added
			pendingPoints.Sort(delegate(Tpoint x, Tpoint y)
			{
				return (x.F.CompareTo(y.F));
			});
		}

		return null;
	}



	bool checkSurrounding(Tpoint point)
	{
		Vector3 pointToCheck;

		pointToCheck = point.position;
		pointToCheck.y += 1;
		if(checkPoint(pointToCheck, point))
		{
			return true;
		}
		
		pointToCheck = point.position;
		pointToCheck.y -= 1;
		if(checkPoint(pointToCheck, point))
		{
			return true;
		}
		
		pointToCheck = point.position;
		pointToCheck.x -= 1;
		if(checkPoint(pointToCheck, point))
		{
			return true;
		}
		
		pointToCheck = point.position;
		pointToCheck.x += 1;
		if(checkPoint(pointToCheck, point))
		{
			return true;
		}

		return false;
	}

	bool checkPoint(Vector3 point, Tpoint parentPoint)
	{
		//check if the point being checked is the endpoint
		if(point == endPoint)
		{
			//break out of function, return that the endpoint is found via bool
			return true;
		}
		//check if the point being checked is already occupied
		if(gridScript.getGridTile((int)point.x,(int)point.y) != 0)
		{
			//break out of function
			return false;
		}

		for(int i = 0; i < usedPoints.Count; i++)
		{
			if(point == usedPoints[i])
			{
				//break out of function
				return false;
			}
		}
		for(int j = 0; j < pendingPoints.Count; j++)
		{
			if(point == pendingPoints[j].position)
			{
				//break out of function
				return false;
			}
		}


		Tpoint newTpoint = new Tpoint();
		newTpoint.position = point;

		newTpoint.parent = parentPoint;

		newTpoint.G = parentPoint.G + 1;
		newTpoint.H = Mathf.Abs((int)(newTpoint.position.x - endPoint.x)) + Mathf.Abs((int)(newTpoint.position.y - endPoint.y));
		newTpoint.F = newTpoint.G + newTpoint.H;

		pendingPoints.Add (newTpoint);

		return false;

	}
}
