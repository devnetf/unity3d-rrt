using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Node
{
	public Vector3 point;
	private List<Node> neighbours;

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

	public Graph()
	{
		this.vertices = new List<Node>();
	}

	public Graph(Node firstNode)
	{
		this.vertices = new List<Node>();
		this.vertices.Add(firstNode);

	}
}
