using UnityEngine;
using System.Collections;

public class MainActor : MonoBehaviour {


	bool leftClickFlag = true;
	RaycastHit hit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {



		if(Input.GetKey(KeyCode.Mouse0) && leftClickFlag)
			leftClickFlag = false;
		
		if(!Input.GetKey(KeyCode.Mouse0) && !leftClickFlag)
		{
			leftClickFlag = true;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit, 100))
			{
				if (hit.transform.name == "Plane")
				{
					float X = hit.point.x;
					float Z = hit.point.z;

					RRTScript RRT = new RRTScript(new Vector3(0.0f,0.5f,0.0f), new Vector3(X,0.5f,Z));
					RRT.buildRRT();

				}
			}

		}
	}
}
