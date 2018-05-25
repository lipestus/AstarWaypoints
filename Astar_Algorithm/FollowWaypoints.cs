using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour {

    #region Private Variables
    private Transform goal;
    private float speed = 3f;
    private float accuracy = 1f;
    private float rotationSpeed = 2f;
    private GameObject[] wps;
    private int currentWpTracker = 0;
    private GameObject currentNode;
    private Graph g;
    private Animator anim;
    #endregion

    public GameObject aStartManager;

    // Use this for initialization
    void Start () {
        wps = aStartManager.GetComponent<AstarManager>().waypoints;
        g = aStartManager.GetComponent<AstarManager>().graph;
        currentNode = wps[0];
        anim = GetComponent<Animator>();
	}

    public void GoToDestination()
    {
        g.AStar(currentNode, wps[3]);
        currentWpTracker = 0;
    }

    public void GoBackToStart()
    {
        g.AStar(currentNode, wps[0]);
        currentWpTracker = 0;
    }
	
	// Update is called once per frame
	void LateUpdate () {

        if (g.getPathLength() == 0 || currentWpTracker == g.getPathLength())
        {
            return;
        }
           
        // node that it's closest to the player at this moment
        currentNode = g.getPathPoint(currentWpTracker);

        // if we reach close enough to the current WP we move towards the next one
        if(Vector3.Distance(g.getPathPoint(currentWpTracker).transform.position, transform.position) < accuracy)
        {
            currentWpTracker++;
        }

        //if we did not reach the end of the line
        if(currentWpTracker < g.getPathLength())
        {
            goal = g.getPathPoint(currentWpTracker).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

            anim.SetFloat("speed", speed);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}
