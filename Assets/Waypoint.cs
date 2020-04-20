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
    private float disinfectSpeed = 15f;
    private SpriteRenderer sr;

    public float spreadRadius = 0.5f; 
    private float spreadChance = 0.5f;

    private float spreadDelay = 1.0f;
    private float currentSpreadTime = 0.0f;

    private Color disinfectantColor = new Color(82/255f, 151/255f, 17/255f, 1f);
    private Color sickColor = new Color(182/255f, 56/255f, 50/255f, 1f);

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
            sr.color = Color.Lerp(Color.white, sickColor, currentHealth / (minHealth));
            Spread();
        }
        else if (isDisinfected && currentHealth > 0.0f) {
            currentHealth -= Time.deltaTime;
            sr.color = Color.Lerp(Color.white, disinfectantColor, currentHealth / maxHealth);
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
            sr.color = Color.Lerp(Color.white, sickColor, currentHealth / (minHealth));
        }
        else {
            sr.color = Color.Lerp(Color.white, disinfectantColor, currentHealth / maxHealth);
        }
    }

    void Spread() {
        if (currentSpreadTime < spreadDelay){
            //currentSpreadTime += Time.deltaTime;
            currentSpreadTime += Random.Range(0.0f, 2.0f) * Time.deltaTime;
            return;
        }
        currentSpreadTime = 0.0f;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, spreadRadius);
        for (int i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i].gameObject.tag == "Child"){
                if (Random.value < spreadChance){
                    //((Child) hitColliders[i].gameObject.GetComponent(typeof(Child))).isSick = true;
                    ((Child) hitColliders[i].gameObject.GetComponent(typeof(Child))).GetSick();
                }
            }
        }
    }
}
