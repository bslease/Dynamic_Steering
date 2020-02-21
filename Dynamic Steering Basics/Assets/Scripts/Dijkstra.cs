using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dijkstra
{
    // This structure is used to keep track of the information we need
    // for each node.
    class NodeRecord : IComparable<NodeRecord>
    {
        public Node node;
        public Connection connection;
        public float costSoFar;

        public int CompareTo(NodeRecord other)
        {
            // CompareTo returns a value that indicates the relative order of the objects being compared.
            // The return value has these meanings:
            //   negative - this instance precedes the other in sort order
            //   zero     - this instance occurs in the same position in the sort order as other
            //   positive - this instance follows other in the sort order

            // This is a standard implementation feature I couldn't find an explanation for
            if (other == null)
            {
                return 1;
            }

            // We want to sort lowest costsofar to highest, so:
            //   if our costsofar is lower than other, return a negative value
            //   if we're exactly the same, return 0
            //   if our costsofar is larger than other, return a positive value
            return (int)(costSoFar - other.costSoFar);
        }
    }

    class PathfindingList
    {
        // based on Millington section 4.2.4, pp. 211-212
        // the pathfindinglist data structure provides four critical operations
        // 1. add an entry to the list
        // 2. remove an entry from the list
        // 3. find the smallest element
        // 4. find an entry in the list corresponding to a particular node (contains and find)
        List<NodeRecord> nodeRecords = new List<NodeRecord>();

        public void add(NodeRecord n)
        {
            nodeRecords.Add(n);
        }

        public void remove(NodeRecord n)
        {
            nodeRecords.Remove(n);
        }

        public NodeRecord smallestElement()
        {
            nodeRecords.Sort();
            return nodeRecords[0]; // depends on list sorted, lowest cost to highest
        }

        public int length()
        {
            return nodeRecords.Count;
        }

        public bool contains(Node node)
        {
            foreach (NodeRecord n in nodeRecords)
            {
                if (n.node == node)
                {
                    return true;
                }
            }

            return false;
        }

        public NodeRecord find(Node node)
        {
            foreach (NodeRecord n in nodeRecords)
            {
                if (n.node == node)
                {
                    return n;
                }
            }

            return null;
        }

    }

    public static List<Connection> pathfind(Graph graph, Node start, Node goal)
    {
        // Initialize the record for the start node.
        NodeRecord startRecord = new NodeRecord();
        startRecord.node = start;
        startRecord.connection = null;
        startRecord.costSoFar = 0;

        // Initialize the open and closed lists
        PathfindingList open = new PathfindingList();
        open.add(startRecord);
        PathfindingList closed = new PathfindingList();

        // Iterate through processing each node
        NodeRecord current = new NodeRecord();
        while (open.length() > 0)
        {
            // Find the smallest element in the open list
            current = open.smallestElement();

            // If it is the goal node, then terminate
            if (current.node == goal)
            {
                break;
            }

            // Otherwise get its outgoing connections.
            List<Connection> connections = graph.getConnections(current.node);

            // Loop through each connection in turn.
            foreach (Connection connection in connections)
            {
                // Get the cost estimate for the end node
                Node endNode = connection.getToNode();
                float endNodeCost = current.costSoFar + connection.getCost();

                NodeRecord endNodeRecord = new NodeRecord();

                // Skip if the node is closed
                if (closed.contains(endNode))
                {
                    continue;
                }
                // ... or if it is open and we've found a worse route
                else if (open.contains(endNode))
                {
                    // Here we find the record in the open list
                    // corresponding to the endNode
                    endNodeRecord = open.find(endNode);
                    if (endNodeRecord != null && endNodeRecord.costSoFar < endNodeCost)
                    {
                        continue;
                    }
                }
                // Otherwise we know we've got an unvisited node, so make a
                // record for it
                else
                {
                    endNodeRecord = new NodeRecord();
                    endNodeRecord.node = endNode;
                }

                // We're here if we need to update the node. Update the
                // cost and connection.
                endNodeRecord.costSoFar = endNodeCost;
                endNodeRecord.connection = connection;

                // And add it to the open list
                if (!open.contains(endNode))
                {
                    open.add(endNodeRecord);
                }
            }

            // We've finished looking at the connections for the current
            // node, so add it to the closed list and remove it from the 
            // open list.
            open.remove(current);
            closed.add(current);
        }

        // We're here if we've either found the goal, or if we've no more
        // nodes to search. Find which.
        if (current.node != goal)
        {
            // We've run out of nodes without finding the goal, so there's
            // no solution
            return null;
        }
        else
        {
            // Compile the list of connections in the path.
            List<Connection> path = new List<Connection>();

            // Work back along the path, accumulating connections
            while (current.node != start)
            {
                path.Add(current.connection);
                //current = current.connection.getFromNode(); // << This is Millington. This doesn't work because current needs to be a full NodeRecord.
                Node fromNode = current.connection.getFromNode();
                current = closed.find(fromNode);
            }

            // Reverse the path and return it.
            path.Reverse();
            return path;
        }

    }
}
