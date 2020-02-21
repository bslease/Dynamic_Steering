using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public Node start;
    public Node goal;
    Graph myGraph;

    // Start is called before the first frame update
    void Start()
    {
        Graph myGraph = new Graph();
        myGraph.Build();
        List<Connection> path = Dijkstra.pathfind(myGraph, start, goal);
        foreach (Connection c in path)
        {
            Debug.Log("from " + c.getFromNode() + " to " + c.getToNode() + " @" + c.getCost());
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
