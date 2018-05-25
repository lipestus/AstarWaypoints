using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public struct Line
{
    public enum Direction {SOLO, TWO};
    public GameObject node1;
    public GameObject node2;
    public Direction direction;
}


public class AstarManager : MonoBehaviour {

    public GameObject[] waypoints; // spheres placed on the map
    public Line[] lines; // these are the lines to link into nodes
    public Graph graph = new Graph();

    // Use this for initialization
    void Start () {
		if(waypoints.Length > 0)
        {
            foreach(GameObject wp in waypoints)
            {
                graph.AddNode(wp);
            }

            foreach(Line l in lines)
            {
                graph.AddEdge(l.node1, l.node2);
                if (l.direction == Line.Direction.TWO)
                    graph.AddEdge(l.node2, l.node1);
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
        graph.debugDraw();
	}
}
