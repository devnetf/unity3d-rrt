using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RRTScript{
	 
	GameObject actor;
	System.Random rand = new System.Random();
	Vector3 dest;
	Graph graph;
	double EPISILON = 1.0;

	public RRTScript(Vector3 start, Vector3 end)
	{
		this.graph = new Graph(new Node(start));
		this.dest = end;
	}

	public void buildRRT()
	{
		bool isOver = false;

		int i = 0;
		while(!isOver)
		{
			addPoint();

			if( i % 5 == 0)
			{
				isOver = ConnectEnd();
			}

			i++;
		}

	}

	public void addPoint()
	{
		Vector3 randPoint = new Vector3();
		bool inObj = true;

		while(inObj)
		{
			inObj = false;
			randPoint.x = (float)(rand.NextDouble()*20.0 - 10.0);
			randPoint.z = (float)(rand.NextDouble()*20.0 - 10.0);
			randPoint.y = 0.5f;

			//actually we don't need these I think...
			/*if(Physics.SphereCast(randPoint, 0.1f, new Vector3(0,0,0), 0.0f))
			{
				inObj = true;
				break;
			}*/
			/*GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Untagged");

			foreach(var obj in gameObjects)
			{
				if(obj.collider.bounds.Contains(randPoint))
				{
					inObj = true;
					break;
				}
			}*/

		}


		/*while (Physics.Raycast(randPoint, dest -randPoint , exitDistance))
		{
			List<Vector3> path = new List<Vector3>();j
			path.Add(startPos);
			path.Add(targetPos);
			return path;
		}*/

		double smallestD = 999999.0;
		double tmpD = 0;
		Node chosenNode = new Node();

		foreach(var node in graph.vertices)
		{
			tmpD = Vector3.Distance(node.point, randPoint);

			if(tmpD < smallestD)
			{
				smallestD = tmpD;
				chosenNode = node;
			}
		}

		if(Physics.Raycast(randPoint, chosenNode.point - randPoint, (float)smallestD))
		{
			return;//not working
		}

		Vector3 newPoint = randPoint;

		if(smallestD > EPISILON)
		{
			float scalar  = (float)(EPISILON/smallestD);
			Vector3 tmp = (randPoint - chosenNode.point);
			tmp.Scale(new Vector3(scalar,scalar,scalar));
			newPoint = chosenNode.point + tmp;
		}

		Debug.DrawLine(newPoint, chosenNode.point, Color.white, 100);
		//Debug.DrawRay(newPoint, chosenNode.point, Color.white, 100);
		Debug.Log(newPoint.x + " " + newPoint.y + " " + newPoint.z);
		Debug.Log(  "D: " + smallestD);
		Debug.Log(  "ND: " + Vector3.Distance(newPoint, chosenNode.point));

		/*check if the point has close obstables*/
		Node newNode = new Node(newPoint);
		newNode.addNeighbour(chosenNode);
		this.graph.vertices.Add(newNode);
		chosenNode.addNeighbour(newNode);
		Debug.Log(  "G size " + this.graph.vertices.Count);


	}

	public bool ConnectEnd()
	{
		List<Node> ChosenNodes = new List<Node>();
		double tmpD = 0.0;

		foreach(var node in this.graph.vertices)
		{
			tmpD = Vector3.Distance(node.point, this.dest);

			if(!Physics.Raycast(node.point, this.dest-node.point, (float)tmpD))
			{
				ChosenNodes.Add(node);
			}

		}

		double smallestD = 99999.0;
		Node smallestNode = null;

		foreach(var node in ChosenNodes)
		{
			tmpD = Vector3.Distance(node.point, this.dest);
			
			if(tmpD < smallestD)
			{
				smallestD = tmpD;
				smallestNode = node;
			}
			
		}

		if(smallestNode == null)
		{
			return false;
		}

		if(smallestD <= EPISILON)
		{
			Node newNode = new Node(this.dest);
			newNode.addNeighbour(smallestNode);
			this.graph.vertices.Add(newNode);
			smallestNode.addNeighbour(newNode);
			Debug.DrawLine(dest, smallestNode.point, Color.white, 100);
			return true;
		}

		return false;
	}
	
}
