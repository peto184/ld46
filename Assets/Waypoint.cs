using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public bool isSick = false;
    public bool isDisinfected = false;

    private float maxHealth = 5.0f; // stay desifected 5s
    private float currentHealth = 0.0f;
    
    private float minHealth = -2.5f;
    private float disinfectSpeed = 5f;
    private SpriteRenderer sr;

    public float spreadRadius = 0.5f; 
    public float spreadChance = 0.01f;

    private float spreadDelay = 1.0f;
    private float currentSpreadTime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        sr = (SpriteRenderer) GetComponent<SpriteRenderer>();
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, spreadRadius);
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth); 

        if (isSick) {
            currentHealth = Mathf.Clamp(currentHealth - Time.deltaTime, minHealth, 0f);
            sr.color = Color.Lerp(Color.white, Color.red, currentHealth / (minHealth));
            Spread();
        }
        else if (isDisinfected && currentHealth > 0.0f) {
            currentHealth -= Time.deltaTime;
            sr.color = Color.Lerp(Color.white, Color.green, currentHealth / maxHealth);
        }
        else {
            // currentHealth = 0.0f;
            isDisinfected = false;
        }

    }

    public void Infect() {
        if (isDisinfected)
            return;
        isSick = true;
    }

    public void Disinfect(float deltaTime) {
        isSick = false;
        isDisinfected = true;
        // currentDisifected = disinfectedCooldown;
        currentHealth += disinfectSpeed*deltaTime;

        if (currentHealth < 0.0f) {
            sr.color = Color.Lerp(Color.white, Color.red, currentHealth / (minHealth));
        }
        else {
            sr.color = Color.Lerp(Color.white, Color.green, currentHealth / maxHealth);
        }
    }

    void Spread() {
        if (currentSpreadTime < spreadDelay){
            currentSpreadTime += Time.deltaTime;
            return;
        }
        currentSpreadTime = 0.0f;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, spreadRadius);
        for (int i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i].gameObject.tag == "Child"){
                if (Random.value < spreadChance){
                    ((Child) hitColliders[i].gameObject.GetComponent(typeof(Child))).isSick = true;
                }
            }
        }
    }
}
