using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{

    [SerializeField] float minutes = 2;
    [SerializeField] float seconds = 0;
    [SerializeField] GameObject GameManagerRef;
    [SerializeField] GameObject playerStrikerRef;
    [SerializeField] GameObject enemyStrikerRef;

    [SerializeField] TMP_Text playerScoreText;
    [SerializeField] TMP_Text aiScoreText;
    [SerializeField] TMP_Text minutesText;
    [SerializeField] TMP_Text secondsText;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text playerWonText;
    [SerializeField] TMP_Text aiWonText;
    [SerializeField] TMP_Text tieText;

    GameManager gameManagerScriptRef;
    int playerScore = 0;
    int aiScore = 0;

     void Start()
    {
        gameManagerScriptRef = GameManagerRef.GetComponent<GameManager>();
    }

    void Update()
    {
       
        seconds = seconds - Time.deltaTime;
        if(seconds <= 0)
        {
            if(minutes <= 0)
            {
                minutes = 0;
                seconds = 0;
                GameOverBro();
            }
            else
            {
                minutes = minutes - 1;
                seconds = 60;
            }
        }
        minutesText.text = minutes.ToString("0");
        secondsText.text = seconds.ToString("0");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Black")
        {
            playerScore = playerScore + 1;
            playerScoreText.text = playerScore.ToString();
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "White")
        {
            aiScore = aiScore + 1;
            aiScoreText.text = aiScore.ToString();
            Destroy(collision.gameObject);
        }
        else if( collision.tag == "Queen")
        {
            Destroy(collision.gameObject);
            if(gameManagerScriptRef.playerTurn)
            {
                playerScore = playerScore + 2;
                playerScoreText.text = playerScore.ToString();
            }
            else if(!gameManagerScriptRef.playerTurn)
            {
                aiScore = aiScore + 2;
                aiScoreText.text = aiScore.ToString();
            }
        }
    }

    void GameOverBro()
    {
        if(playerScore < aiScore)
        {
            gameOverText.gameObject.SetActive(true);
            aiWonText.gameObject.SetActive(true);
        }
        else if (playerScore  > aiScore) 
        {
            gameOverText.gameObject.SetActive(true);
            playerWonText.gameObject.SetActive(true);
        }
        else
        {
            gameOverText.gameObject.SetActive(true);
            tieText.gameObject.SetActive(true);
        }
        enemyStrikerRef.SetActive(false);
        playerStrikerRef.SetActive(false);
    }
}
