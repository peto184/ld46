using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour
{
    public Vector3 dir;
    public float speed = 250.0f;
    public float rotationSpeed = 100f;
    private Rigidbody2D rb;

    public float disinfectRange = 1.0f;

    private ParticleSystem[] particleSystems;

    private GameManager gameManager;

    private float disinfectCharge = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.FindWithTag("GameManager");
        gameManager = (GameManager)go.GetComponent(typeof(GameManager));  
        rb = GetComponent<Rigidbody2D>();
        particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        dir = new Vector3(x, y, 0);

        HandleDisinfectant();

        if (dir != Vector3.zero)
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation,
            //    Quaternion.FromToRotation(Vector3.right, dir), rotationSpeed * Time.deltaTime);
        }

    }

    void HandleDisinfectant()
    {
        //if (Input.GetMouseButtonDown(0))
        if (Input.GetMouseButton(0) && !gameManager.gameOver && disinfectCharge > 0.0f)
        {
            disinfectCharge -= Time.deltaTime;

            Vector3 sprayTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 tmp = sprayTarget - transform.position;
            tmp.z = 0.0f;
            Vector3 sprayDir = tmp / tmp.magnitude;

            particleSystems[0].gameObject.SetActive(true);

            // move to the position
            particleSystems[0].transform.position = (transform.position + sprayDir);
            particleSystems[0].transform.localPosition *= 0.25f;
            Vector3 sprayRot = new Vector3(-90, sprayTarget.y, sprayTarget.z);
            
            //Quaternion sprayRot = Quaternion.LookRotation(particleSystems[0].transform.position, sprayTarget);
            // particleSystems[0].transform.rotation = sprayRot;
            
            Vector3 diff = sprayTarget - particleSystems[0].transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(tmp.y, tmp.x) * Mathf.Rad2Deg;
            particleSystems[0].transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
        else {
            particleSystems[0].gameObject.SetActive(false);
            disinfectCharge += Time.deltaTime;
        }

        disinfectCharge = Mathf.Clamp(disinfectCharge, 0, 1);
    }

    void FixedUpdate()
    {
        rb.velocity = dir * speed * Time.fixedDeltaTime;
    }

    public float GetDisinfectantCharge(){
        return disinfectCharge;
    }

}
