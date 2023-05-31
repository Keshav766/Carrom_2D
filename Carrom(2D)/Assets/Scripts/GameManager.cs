using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerStrikerRef;
    [SerializeField] GameObject enemyStrikerRef;


    public bool playerTurn = true;

    void Update()
    {
        if(playerTurn)
        {
            playerStrikerRef.SetActive(true);
            enemyStrikerRef.SetActive(false);
        }
        else if(!playerTurn)
        {
            enemyStrikerRef.SetActive(true);
            playerStrikerRef.SetActive(false);
        }
    }
}
