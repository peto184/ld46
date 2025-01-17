﻿using System.Collections;
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
    void LateUpdate()
    {
        int healthy = gameManager.GetHealthy();
        int sick = gameManager.GetSick();
        int dead = gameManager.GetDead();

        text.text = $"Healthy: {healthy}\nSick: {sick}\nDead: {dead}";
    }
}
