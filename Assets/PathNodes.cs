using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNodes : MonoBehaviour
{
    public List<PathNodes> neighbors; //list of neighboring vertices
    public float distanceCost = float.MaxValue; //distance cost using max value float
    public PathNodes previousNode = null; //stores the previous node

    void Awake()
    {
        distanceCost = float.MaxValue; //makes the distance cost max float again just in case unity overwrites it
    }
}
