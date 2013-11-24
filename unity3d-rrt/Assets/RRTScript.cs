using UnityEngine;
using System.Collections;
using System;

public class RRTScript : MonoBehaviour {

	GameObject actor;
	Random rand;
	List<Vector3> points;
	Vector3 dest;

	public RRTScript(Vector3 start, Vector3 end)
	{
		this.points = new List<Vector3>();
		this.points.Add(start);
		this.dest = end;
	}

	void addPoints()
	{
		Vector3 randPoint = new Vector3();
		randPoint.x = rand.Next(-10.0, 10.0);
		randPoint.z = rand.Next(-10.0, 10.0);
		randPoint.y = 1.0;

		/*while (Physics.Raycast(randPoint, dest -randPoint , exitDistance))
		{
			List<Vector3> path = new List<Vector3>();
			path.Add(startPos);
			path.Add(targetPos);
			return path;
		}*/

		double smallestD = 0;
		double tmpD = 0;

		foreach(var point in points)
		{
			tmpD = Vector3.Distance(point, randPoint);
		}

	}



	// Use this for initialization
	void Start () { 

		actor = GameObject.Find("Actor");
		Debug.Log("Start!!!");

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(actor.transform.position + " " + actor.transform.position.z);
	}
}
