using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndText : MonoBehaviour
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
        int dead = gameManager.GetDead();
        int sick = gameManager.GetSick();
        text.text = $"Despire your valiant effort you did not manage to keep the village alive and {dead + sick} people died ...\n\nHowever, you reached level: "; 
    }
}
