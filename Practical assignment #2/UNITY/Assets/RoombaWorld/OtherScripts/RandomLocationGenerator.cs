using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class RandomLocationGenerator {

    private static List<GraphNode> allNodes;
    private static List<GameObject> patrolPoints;

    private static RandomLocationGenerator instance = null;

    static RandomLocationGenerator() {
        // get all the nodes in the gridgraph and save the walkable ones in allNodes list.
        allNodes = new List<GraphNode>();
        GridGraph gg = AstarPath.active.data.gridGraph;
        gg.GetNodes(nod => { if (nod.Walkable) allNodes.Add(nod); });

        // get all the patrol points
        patrolPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("PATROLPOINT"));
    }

    public static Vector3 RandomWalkableLocation() {
        if (instance == null) instance = new RandomLocationGenerator();

        GraphNode node = allNodes[Random.Range(0, allNodes.Count)];
        // return its position as a vector 3
        return (Vector3)node.position;
    }

    public static Vector2 RandomWalkableLocationOnScreen() {
        if (instance == null) instance = new RandomLocationGenerator();

        Vector3 pos = Vector3.zero;
        bool onScreen = false;

        do {
            pos = RandomLocationGenerator.RandomWalkableLocation();

            Vector3 screenPoint = Camera.main.WorldToViewportPoint(pos);
            onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        } while (!onScreen);

        return (Vector2)pos;
    }

    public static Vector3 RandomPatrolLocation() {
        if (instance == null) instance = new RandomLocationGenerator();

        return patrolPoints[Random.Range(0, patrolPoints.Count)].transform.position;
    }

    public static GameObject RandomPatrolPoint() {
        return patrolPoints[Random.Range(0, patrolPoints.Count)];
    }

}
