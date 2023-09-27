using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int totalCoins = 0;
    public int lives = 3;
    public Transform spawnPoint;
    public PlayerController player;
    public float timeToRespawn = 2f;
    private float timer = 0;
    public bool isGameOver = false;
    public bool isLevelFinished = false;
    public Text lifesText;
    public GameObject levelEndPanel;
    public Text LevelEndText;
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = spawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isAlive)
        {
            if (timer < timeToRespawn)
            {
                timer = timer + Time.deltaTime;
            }
            else
            {
                if (lives > 0)
                {
                    lives--;
                    player.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    player.transform.position = spawnPoint.transform.position;
                    timer = 0f;
                    player.isAlive = true;
                }
                else
                {
                    isGameOver = true;
                }
            }

        }
        if (isGameOver == true || isLevelFinished == true)
        {
            levelEndPanel.SetActive(true);
            if (isGameOver)
            {
                LevelEndText.text = "GAME OVER";
            }
            if (isLevelFinished)
            {
                LevelEndText.text = "LEVEL FINISHED";
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
        lifesText.text = "x" + lives;
    }

    public void FinishLevel()
    {
        isLevelFinished = true;
    }
}
