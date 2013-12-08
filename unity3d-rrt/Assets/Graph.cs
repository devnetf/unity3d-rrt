using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Node
{
	public Vector3 point;
	public List<Node> neighbours;

	public Node()
	{
		this.point = new Vector3();
		neighbours = new List<Node>();
	}

	public Node(Vector3 pt)
	{
		this.point = pt;
		neighbours = new List<Node>();
	}

	public void addNeighbour(Node node)
	{
		this.neighbours.Add(node);
	}
}


public class Graph{

	public List<Node> vertices;
	public Node startNode; 
	public Node Goal;

	public Graph()
	{
		this.vertices = new List<Node>();
	}

	public Graph(Node firstNode, Node endNode)
	{
		this.vertices = new List<Node>();
		this.startNode = firstNode;
		this.Goal = endNode;
		this.vertices.Add(firstNode);

	}

	public bool isGoal(Node n)
	{
		return n.point.x == this.Goal.point.x && n.point.y == this.Goal.point.y && n.point.z == this.Goal.point.z;
	}

	public bool isStart(Node n)
	{
		return n.point.x == this.startNode.point.x && n.point.y == this.startNode.point.y && n.point.z == this.startNode.point.z;
	}
}
