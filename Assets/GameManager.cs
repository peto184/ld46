using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private List<GameObject> waypoints = new List<GameObject>();
    public GameObject waypointPrefab;
    public GameObject childPrefab;

    public int nWaypoints = 5;
    public float period = 5.0f;
    private float passedTime = 0.0f;

    public int nHealthyChildren = 10;
    public int nSickChildren = 2;

    public List<GameObject> children = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        RegenerateWaypoints();
        SpawnChildren();
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        if (passedTime > period) {
            passedTime = 0.0f;
            // Regenerate
            RegenerateWaypoints();
        }
    }

    public void SpawnChildren() {
        for (int i = 0; i < nHealthyChildren; i++) {
            float x = Random.Range(1.0f, 5f);
            if (Random.value > .5f) 
                x *= -1.0f;
            float y = Random.Range(1.0f, 5f);
            if (Random.value > .5f)
                y *= -1.0f;
                
            GameObject child = (GameObject) Instantiate(childPrefab, new Vector3 (x, y, 0f), Quaternion.identity);
            children.Add(child);            
        }

        for (int i = 0; i < nSickChildren; i++) {
            float x = Random.Range(1.0f, 5f);
            if (Random.value > .5f) 
                x *= -1.0f;
            float y = Random.Range(1.0f, 5f);
            if (Random.value > .5f)
                y *= -1.0f;
                
            GameObject child = (GameObject) Instantiate(childPrefab, new Vector3 (x, y, 0f), Quaternion.identity);
            children.Add(child);

            ((Child)child.GetComponent(typeof(Child))).isSick = true;           
        }
    }

    public List<GameObject> GetWayPoints() {
        return waypoints;
    }

    private void RegenerateWaypoints() {
        // Remove old waypoints
        for (int i = 0; i < waypoints.Count; i++) {
            Destroy(waypoints[i]);
        }
        waypoints.Clear();

        // Init new waypoints
        for (int i = 0; i < nWaypoints; i++) {
            float x = Random.Range(1.0f, 5f);
            if (Random.value > .5f) 
                x *= -1.0f;
            float y = Random.Range(1.0f, 5f);
            if (Random.value > .5f)
                y *= -1.0f;
                
            GameObject wp = (GameObject) Instantiate(waypointPrefab, new Vector3 (x, y, 0f), Quaternion.identity);
            waypoints.Add(wp);
        }
    }

}
