using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusText : MonoBehaviour
{

    private GameManager gameManager;

    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.FindWithTag("GameManager");
        gameManager = (GameManager)go.GetComponent(typeof(GameManager));    
        text = (Text) GetComponent(typeof(Text));
    }

    // Update is called once per frame
    void Update()
    {
        int healthy = 0;
        int sick = 0;
        int dead = 0;

        foreach (GameObject go in gameManager.children) {
            Child c = (Child) go.GetComponent(typeof(Child));
            if (c.isDead){
                dead++;
                continue;
            }
            if (c.isSick) {
                sick++;
            } else {
                healthy++;
            }
        }

        text.text = $"Healthy: {healthy}\nSick: {sick}\nDead: {dead}";
    }
}
