using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Text")]
    public Text lifeText;
    public Text timeText;
    public Text scoreText;

    private GameLogic gameLogic;

    private void Start()
    {
        gameLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }

    private void Update()
    {
        gameLogic.DisplayLife(lifeText);
        gameLogic.DisplayTime(timeText);    
        gameLogic.DisplayScore(scoreText);
    }
}
