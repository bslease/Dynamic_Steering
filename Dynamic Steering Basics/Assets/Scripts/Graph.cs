using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    // an array of connections outgoing from the given node
    public Connection[] getConnections(GameObject fromNode)
    {
        return null;
    }
}

public class Connection
{
    float cost;
    GameObject fromNode;
    GameObject toNode;

    public float getCost()
    {
        return cost;
    }

    public GameObject getFromNode()
    {
        return fromNode;
    }

    public GameObject getToNode()
    {
        return toNode;
    }
}
