using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class GameLogic : MonoBehaviour
{
    public int currentLife;
    public int maxLifes = 2;
    public float timer = 200;
    public int botCount = 4;
    public bool isCreated = false;

    [Header("Score")]
    private int score = 0;
    public int squidScore = 100;

    private void Awake()
    {
        currentLife = maxLifes;
        if (!isCreated)
        {
            DontDestroyOnLoad(gameObject);
            isCreated = true;
        }
    }

    private void Update()
    {
 
        timer -= Time.deltaTime;
    }

    public void AddScore(int plusScore)
    {
        score += plusScore;
    }

    public void DisplayTime(Text timeText)
    {
        int minute = Mathf.FloorToInt(timer / 60f);
        int second = Mathf.FloorToInt(timer -  minute * 60);

        timeText.text = string.Format("{0:00}:{1:00}", minute, second);
    }

    public void DisplayLife(Text lifeText)
    {
        lifeText.text = string.Format("x{0:00}", currentLife); 
    }

    public void DisplayScore(Text scoreText)
    {
        scoreText.text = score.ToString();
    }

    public void CheckGameOver(GameObject player)
    {
        
        if (!player.activeSelf || timer == 0)
        {
            currentLife--;
            if (currentLife < 0)
            {
                currentLife = maxLifes;
            }
            Invoke(nameof(NewRound), 0.5f);
        }
    }

    public void NewRound()
    {
        timer = 200;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
