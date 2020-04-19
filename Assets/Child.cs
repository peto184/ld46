using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    public float maxSpeed = 50.0f;

    private float currentSpeed = 50.0f;
    public float rotationSpeed = 10f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public GameObject target;

    public Vector3 dir;
    private bool targetReached = false;
    public float talkingTime = 2.0f;
    public float currentTime = 0.0f;
    private GameManager gameManager;


    private float spreadDelay = 1.0f;
    private float currentSpreadTime = 0.0f;

    public bool isSick = false;
    public float spreadRadius = 1.0f;
    public float spreadChance = 0.25f;

    public bool isDead = false;
    private float timeToDie = 7.5f;
    public float currentTimeToDie = 0.0f;
    private ParticleSystem[] particleSystems;

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, spreadRadius);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject go = GameObject.FindWithTag("GameManager");
        gameManager = (GameManager)go.GetComponent(typeof(GameManager));
        target = GetRandomWaypoint();
        sr = (SpriteRenderer) GetComponent(typeof(SpriteRenderer));
        particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
        particleSystems[0].gameObject.SetActive(false);

        timeToDie = timeToDie + Random.Range(-2.5f, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead){
            dir = Vector3.zero;
            rb.mass = 0.05f;
            particleSystems[0].gameObject.SetActive(false);
            return;
        }

        UpdatePosition();
        if (isSick){
            particleSystems[0].gameObject.SetActive(true);
            UpdateSpread();
            currentTimeToDie += Time.deltaTime;
        }

        if (isSick) {
           sr.color = Color.red;
           currentSpeed = maxSpeed * (1 - 0.20f * (currentTimeToDie / timeToDie));
        }
        else {
            sr.color = Color.white;
        }

        if (currentTimeToDie > timeToDie) {
            sr.color = Color.black;
            isDead = true;
        }
    }

    void UpdateSpread()
    {
        if (currentSpreadTime < spreadDelay){
            currentSpreadTime += Time.deltaTime;
            return;
        }
        currentSpreadTime = 0.0f;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, spreadRadius);
        for (int i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i].gameObject.tag == "Waypoint"){
                ((Waypoint) hitColliders[i].gameObject.GetComponent(typeof(Waypoint))).Infect();
            }

            // if (hitColliders[i].gameObject.tag == "Child"){
            //     if (Random.value < spreadChance) {
            //         ((Child) hitColliders[i].gameObject.GetComponent(typeof(Child))).isSick = true;
            //     }
            // }
        }

    }

    void UpdatePosition()
    {
        if (targetReached)
        {
            currentTime += Time.deltaTime;
            if (currentTime > talkingTime)
            {
                currentTime = 0.0f;
                targetReached = false;
                target = GetRandomWaypoint();
            }
        }

        // Move to target
        if (target == null)
        {
            dir = Vector3.zero;
            target = GetRandomWaypoint();
        }
        else
        {
            dir = target.transform.position - transform.position;
            dir.z = 0.0f;
        }

        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.FromToRotation(Vector3.right, dir), rotationSpeed * Time.deltaTime);
        }

        // target reached
        if (dir.magnitude < 0.5f)
        {
            targetReached = true;
            dir = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = dir * currentSpeed * Time.fixedDeltaTime;
    }

    GameObject GetRandomWaypoint()
    {
        if (gameManager == null)
        {
            Debug.Log("game manager is null");
        }

        List<GameObject> waypoints = gameManager.GetWayPoints();

        if (waypoints.Count == 0)
        {
            return null;
        }

        return waypoints[Random.Range(0, waypoints.Count)];
    }

    GameObject FindClosestWaypoint()
    {
        if (gameManager == null)
        {
            Debug.Log("game manager is null");
        }

        List<GameObject> waypoints = gameManager.GetWayPoints();

        float minimum = 999999f;
        GameObject closest = null;
        for (int i = 0; i < waypoints.Count; i++)
        {
            float diff = (waypoints[i].transform.position - transform.position).magnitude;
            if (diff < minimum)
            {
                minimum = diff;
                closest = waypoints[i];
            }
        }

        return closest;
    }

}
