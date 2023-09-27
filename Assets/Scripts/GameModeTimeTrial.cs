using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameModeTimeTrial : MonoBehaviour
{
    public GameManager manager;
    public float levelTimer = 300f;
    public Text timeText;

    // Update is called once per frame
    void Update()
    {
        if (levelTimer > 0)
        {
            levelTimer -= Time.deltaTime; 
        }else
        {
            if (manager.isGameOver == false)
            {
                manager.isGameOver = true;
                manager.player.Die();
            }
        }
        timeText.text = "Tiempo: " + levelTimer.ToString("F0")+"s";
    }
}
