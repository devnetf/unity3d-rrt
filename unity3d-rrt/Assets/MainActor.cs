using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class MainActor : MonoBehaviour {


	bool leftClickFlag = true;
	RaycastHit hit;
	float startTime;
	float duration = 1.0f;
	bool Walk = false;
	Stack<SearchNode> path = null;
	Node from = null, to = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Walk)
		{
		
			if((to == null || transform.position.Equals(to.point)) && path.Count > 0)
			{
				if(from == null)
				{
					from = path.Pop().node;
				}
				else
				{
					from = to;
				}
					
				to = path.Pop().node;
				startTime = Time.time;
			}


			float journeyLength = Vector3.Distance(from.point, to.point);
			transform.position = Vector3.Lerp(from.point, to.point, (Time.time - startTime)*2.0f/journeyLength);
				
			if(transform.position.Equals(to.point) && path.Count == 0)
			{
				Walk = false;
			}


		}


		if(Input.GetKey(KeyCode.Mouse0) && leftClickFlag)
			leftClickFlag = false;
		
		if(!Input.GetKey(KeyCode.Mouse0) && !leftClickFlag)
		{
			leftClickFlag = true;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.name == "Plane")
				{
					float X = hit.point.x;
					float Z = hit.point.z;
					float Y = hit.point.y;
					//transform.Translate(Vector3.forward);

					if(Physics.OverlapSphere(new Vector3(X,Y+0.7f,Z), 0.6f).Length == 0)
					{
						RRTScript RRT = new RRTScript(transform.position, new Vector3(X,Y+0.7f,Z));
						path = RRT.buildRRT();
					
						if(path != null)
						{
							Walk = true;

						}
						startTime = Time.time;


					}
					else
					{
						Debug.Log("Invalid ending point!");
					}

				}
			}

		}
	}
}
