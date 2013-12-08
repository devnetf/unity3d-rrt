using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar
{

	private SortedList<double, SearchNode> PQ;
	public IDictionary<Node, SearchNode> VisitedStates;
	public Stack<SearchNode> Path;
	
	public AStar(Node start)
	{	   
		PQ = new SortedList<double, SearchNode>();//initialize a priority queue
		VisitedStates = new Dictionary<Node, SearchNode>();
		Path = new Stack<SearchNode>();
		PQ.Add(0.0, new SearchNode(start, 0.0, 0.0, null));
	}
	
	public SearchNode search(Graph g)
	{
		while(PQ.Count > 0)
		{
			SearchNode lowest =  PQ.Values[0];
			PQ.RemoveAt(0);

			if(g.isGoal(lowest.node)){return lowest;}
			
			foreach(Node neighbour in lowest.node.neighbours)
			{
				if(!g.isStart(neighbour))
				{
					double cost = lowest.curr_cost + Vector3.Distance(neighbour.point, lowest.node.point);
					double heuristic = Vector3.Distance(neighbour.point, g.Goal.point);
					double f = cost + heuristic;

					SearchNode visited_node;

					if(VisitedStates.TryGetValue(neighbour, out visited_node))
					{
						
						if(f <= visited_node.F_value )
						{
							visited_node.F_value = f;
							visited_node.curr_cost = cost;
							visited_node.came_from = lowest;
							SearchNode NewNode = new SearchNode(neighbour, f, cost, lowest);
							PQ.Add(f, NewNode);
						}
					}
					else
					{
						SearchNode NewNode = new SearchNode(neighbour, f, cost, lowest);
						PQ.Add(f, NewNode);
						VisitedStates.Add(NewNode.node, NewNode);
					}

				}

			}      
			
		}	   
		
		return null; 
	}

	public SearchNode buildpathfrom(SearchNode s)
	{
		if(s.came_from != null)
		{		
			Debug.DrawLine(s.node.point, s.came_from.node.point, Color.red, 100);
			Path.Push(s);
			buildpathfrom(s.came_from);
		}
		
		return null;
	} 

}

public class SearchNode
{
	//node store in PQ
	public double F_value;
	public SearchNode came_from;
	public Node node; 
	public double curr_cost;
	
	public SearchNode(Node n, double f, double c ,SearchNode cf)
	{
		this.node = n;
		this.F_value = f;
		this.curr_cost = c;
		this.came_from = cf;
	}
}
