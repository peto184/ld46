using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelReachedEnd : MonoBehaviour
{
    // Start is called before the first frame update
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
    void LateUpdate()
    {
        int level = gameManager.GetLevel() + 1;
        text.text = $"{level}";
    }
}
