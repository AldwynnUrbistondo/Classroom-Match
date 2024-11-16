using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void PauseGame()
    {
        gameManager.audioSource.clip = gameManager.cardClickSound;
        gameManager.audioSource.Play();

        gameManager.canClick = false;
        gameManager.timeIsRunning = false;
    }

    public void PlayGame()
    {
        gameManager.audioSource.clip = gameManager.cardClickSound;
        gameManager.audioSource.Play();

        gameManager.canClick = true;
        gameManager.timeIsRunning = true;
    }

    public void BackScene()
    {
        gameManager.audioSource.clip = gameManager.cardClickSound;
        gameManager.audioSource.Play();

        SceneManager.LoadScene("Start");
    }
}
