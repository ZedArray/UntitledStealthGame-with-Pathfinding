using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{
    private PathNodes[] vertices; //array to store the vertices
    public PathNodes startNode; // storing the point of origin for the algorithm
    public PathNodes endNode; // storing the end point
    private List<PathNodes> unvisited;
    private List<PathNodes> visited;

    // variables for testing purposes
    private List<PathNodes> testPath;
    [SerializeField] Transform testStartLoc, testEndLoc;

    // Start is called before the first frame update
    void Start()
    {
        vertices = FindObjectsOfType<PathNodes>(); //Finds all the nodes and stores it in the vertices variable
    }

    private void Update()
    {
        //testing
/*
        testPath = findPath(testStartLoc.position, testEndLoc.position);
        foreach (PathNodes v in testPath)
        {
            if (v.previousNode == null) continue;
            Debug.DrawLine(v.transform.position, v.previousNode.transform.position);
        }
*/
    }

    public List<PathNodes> findPath(Vector3 src, Vector3 dest)
    {
        //testPath = new List<PathNodes>();
        visited = new List<PathNodes>(); //clears out the visited list
        foreach (PathNodes v in vertices)
        {
            v.distanceCost = float.MaxValue;
            unvisited.Add(v);
        }

        //sets startnode and endnode to the first node in the unvisited list 
        startNode = unvisited[0];
        endNode = unvisited[0];

        //creates list to be returned to the function caller
        List<PathNodes> gotPath = new List<PathNodes>();

        // Finds the node closest to the function caller (the enemy) as the startnode and the node closest to the player as the end node
        float startD = Vector3.Distance(startNode.transform.position, src);
        float endD = Vector3.Distance(endNode.transform.position, dest);
        foreach (PathNodes v in vertices)
        {
            //Debug.Log("Start: " + startTest.name + " Distance: " + startD + " Pos: " + startTest.transform.position);
            //Debug.Log("End: " + endTest.name + " Distance: " + endD + " Pos: " + endTest.transform.position);
            if (Vector3.Distance(v.transform.position, src) < startD)
            {
                startNode = v;
                startD = Vector3.Distance(startNode.transform.position, src);
            }
            if (Vector3.Distance(v.transform.position, dest) < endD)
            {
                endNode = v;
                endD = Vector3.Distance(endNode.transform.position, dest);
            }
        }

        
        //Dijkstra's algorithm to find the shortest path from the start node to all nodes
        startNode.distanceCost = 0;
        startNode.previousNode = null;
        PathNodes checkNode = startNode;
        //Debug.Log(unvisited.Count);
        while (unvisited.Count > 0)
        {
            //Debug.Log(unvisited.Count);
            foreach (PathNodes n in checkNode.neighbors)
            {
                float distCostCheck = checkNode.distanceCost + Vector3.Distance(checkNode.transform.position, n.transform.position);
                //Debug.Log(distCostCheck);
                //Debug.Log(n.distanceCost);
                if (distCostCheck < n.distanceCost)
                {
                    n.distanceCost = distCostCheck;
                    n.previousNode = checkNode;
                }
            }
            visited.Add(checkNode);
            unvisited.Remove(checkNode);
            if (unvisited.Count == 0) break;
            PathNodes nextNode = unvisited[0];
            foreach (PathNodes v in unvisited)
            {
                if (v.distanceCost < nextNode.distanceCost)
                {
                    nextNode = v;
                }
            }
            checkNode = nextNode;
        }

        //Finds the path from end node to start node
        checkNode = endNode;
        bool pathFound = false;
        while (!pathFound)
        {
            //Debug.Log(checkNode.name);
            gotPath.Add(checkNode);
            if (checkNode == startNode) break;
            if (checkNode.previousNode == null) break;
            checkNode = checkNode.previousNode;
        }
        gotPath.Reverse();

        return gotPath;
    }

    public PathNodes getStartNode()
    {
        return startNode;
    }

    public PathNodes getEndNode()
    {
        return endNode;
    }
}
