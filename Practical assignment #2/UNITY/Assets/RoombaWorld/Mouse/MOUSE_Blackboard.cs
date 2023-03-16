using UnityEngine;

public class MOUSE_Blackboard : MonoBehaviour {

    private GameObject[] exitPoints;
    private GameObject theDestiny;
    public GameObject pooPrefab;
    public float roombaDetectionRadius = 50;
    public float crapTimeDuration = 1.0f;

    void Awake() {
        // let's get all the exit&entry points
        exitPoints = GameObject.FindGameObjectsWithTag("EXIT");
        pooPrefab = Resources.Load<GameObject>("POO");
    }

    public GameObject RandomExitPoint() {
        return exitPoints[Random.Range(0, exitPoints.Length)];
    }

    public GameObject RandomDestination() {
        if (theDestiny == null) {
            theDestiny = new GameObject("MouseDestinationHandler");
            theDestiny.transform.parent = gameObject.transform.parent;
        }
        theDestiny.transform.position = RandomLocationGenerator.RandomWalkableLocationOnScreen();
        return theDestiny;
    }

    public void DestroyMe() {
        if (theDestiny != null)
            GameObject.Destroy(theDestiny);

        GameObject.Destroy(gameObject);
    }

    public GameObject NearestExitPoint() {
        GameObject nearest = exitPoints[0];
        float best = SensingUtils.DistanceToTarget(gameObject, nearest);
        float current;
        // process all exit points. Retain the nearest
        for (int i = 1; i < exitPoints.Length; i++) {
            current = SensingUtils.DistanceToTarget(gameObject, exitPoints[i]);
            if (current < best) {
                best = current;
                nearest = exitPoints[i];
            }
        }

        return nearest;
    }

    public void Crap(Vector3 position) {
        GameObject poo = Instantiate(pooPrefab, GameObject.Find("Poo").transform);
        poo.transform.position = position;
    }

}
