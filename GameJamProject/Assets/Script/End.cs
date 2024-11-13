using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class End : MonoBehaviour
{
    public TextMeshProUGUI winner;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    
    void Start()
    {
        if (GameManager.player1Score == GameManager.player2Score)
        {
            winner.text = "TIE!";
        }
        else if (GameManager.player1Score > GameManager.player2Score)
        {
            winner.text = "Player 1 Win!!";
        }
        else
        {
            winner.text = "Player 2 Win!!";
        }

        player1ScoreText.text = "Player 1: " + GameManager.player1Score;
        player2ScoreText.text = "Player 2: " + GameManager.player2Score;
    }
}
