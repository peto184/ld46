using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private List<GameObject> waypoints = new List<GameObject>();
    public GameObject waypointPrefab;
    public GameObject childPrefab;

    public int nWaypoints = 5;
    public float period = 100.0f;
    private float passedTime = 0.0f;

    private int nInitHealthyChildren = 30;
    private int nInitSickChildren = 3;

    public List<GameObject> children = new List<GameObject>();

    private int level = 0;

    // Start is called before the first frame update
    void Start()
    {
        //RegenerateWaypoints();
        float r = 2.5f;
        AddWaypointOnLocation(r, r);
        AddWaypointOnLocation(-r, -r);
        AddWaypointOnLocation(r, -r);
        AddWaypointOnLocation(-r, r);
        AddWaypoint(2);
        SpawnChildren(nInitHealthyChildren, nInitSickChildren);
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        if (passedTime > period) {
            passedTime = 0.0f;
            // Regenerate
            // RegenerateWaypoints();
        }


        if (GetHealthy() == 0) {
            // Gameover
            Debug.Log($"you lose.");
        }
        else if (GetSick() == 0) {
            Debug.Log($"you win level");
            // Game win
            LevelUp();
        }


    }

    private void LevelUp() {
        level += 1;
        nInitSickChildren += 2;
        // nInitHealthyChildren += 1;
        
        nWaypoints += 1;
        if (nWaypoints < 3)
            nWaypoints = 3;
        
        AddWaypoint(1);
        int healthy = GetHealthy();
        SpawnChildren(20-healthy, nInitSickChildren);
    }

    public int GetLevel() {
        return level;
    }

    public int GetHealthy() {
        int count = 0;
        foreach (GameObject go in children) {
            Child c = (Child) go.GetComponent(typeof(Child));
            if (!c.isSick && !c.isDead)
                count++;
        }
        return count;
    }

    public int GetSick() {
        int count = 0;
        foreach (GameObject go in children) {
            Child c = (Child) go.GetComponent(typeof(Child));
            if (c.isSick && !c.isDead)
                count++;
        }
        return count;
    }

    public int GetDead() {
        int count = 0;
        foreach (GameObject go in children) {
            Child c = (Child) go.GetComponent(typeof(Child));
            if (c.isDead)
                count++;
        }
        return count;
    }

    void AddWaypoint(int n) {

        Vector2 topRightCorner = new Vector2(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);

        float max_x = edgeVector.x;
        float max_y = edgeVector.y;
        Debug.Log($"{max_x} - {max_y}");

        for (int i = 0; i < n; i++) {
            float x = Random.Range(0.0f, max_x - 0.75f);
            if (Random.value > .5f) 
                x *= -1.0f;
            float y = Random.Range(0.0f, max_y - 0.75f);
            if (Random.value > .5f)
                y *= -1.0f;
                
            GameObject wp = (GameObject) Instantiate(waypointPrefab, new Vector3 (x, y, 0f), Quaternion.identity);
            waypoints.Add(wp);
        }
    }

    void AddWaypointOnLocation(float x, float y) {
        GameObject wp = (GameObject) Instantiate(waypointPrefab, new Vector3 (x, y, 0f), Quaternion.identity);
        waypoints.Add(wp);
    }


    public void SpawnChildren(int healthy, int sick) {
        for (int i = 0; i < healthy; i++) {
            float x = Random.Range(8.0f, 15f);
            if (Random.value > .5f) 
                x *= -1.0f;
            float y = Random.Range(8.0f, 15f);
            if (Random.value > .5f)
                y *= -1.0f;
                
            GameObject child = (GameObject) Instantiate(childPrefab, new Vector3 (x, y, 0f), Quaternion.identity);
            children.Add(child);            
        }

        for (int i = 0; i < sick; i++) {
            float x = Random.Range(8.0f, 15f);
            if (Random.value > .5f) 
                x *= -1.0f;
            float y = Random.Range(8.0f, 15f);
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

        Vector2 topRightCorner = new Vector2(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);

        float max_x = edgeVector.x;
        float max_y = edgeVector.y;
        Debug.Log($"{max_x} - {max_y}");

        for (int i = 0; i < nWaypoints; i++) {
            float x = Random.Range(0.0f, max_x - 0.75f);
            if (Random.value > .5f) 
                x *= -1.0f;
            float y = Random.Range(0.0f, max_y - 0.75f);
            if (Random.value > .5f)
                y *= -1.0f;
                
            GameObject wp = (GameObject) Instantiate(waypointPrefab, new Vector3 (x, y, 0f), Quaternion.identity);
            waypoints.Add(wp);
        }
    }

}
