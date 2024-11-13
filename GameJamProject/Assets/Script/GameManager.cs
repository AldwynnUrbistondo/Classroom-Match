using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables

    public GameObject[] cardPrefabs;
    public Transform grid;

    public bool canClick = true;

    public int chosenCard1 = -1;
    public int chosenCard2 = -1;

    public int[] card = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

    public int targetID1 = -1;
    public int targetID2 = -1;

    int cardToSpawn;

    [Header("UI")]
    public TextMeshProUGUI player1ScoreText;
    public static int player1Score = 0;
    public TextMeshProUGUI player2ScoreText;
    public static int player2Score = 0;

    public bool player1Turn = false;
    public bool player2Turn = false;

    public int matches = 0;

    public TextMeshProUGUI timeText;
    public float time = 5;
    public bool timeIsRunning;
    public bool timeOut = false;


    [Header("PopUp")]
    public Image popUpBackground;
    public TextMeshProUGUI p1Text;
    public TextMeshProUGUI p2Text;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI textMatch;

    public Animator popUpAnim;
    public Animator textMatchAnim;
    #endregion

    void Start()
    {
        player1Score = 0;
        player2Score = 0;

        timeIsRunning = false;

        canClick = false;

        popUpBackground.enabled = false;
        p1Text.enabled = false;
        p2Text.enabled = false;
        gameOverText.enabled = false;

        StartCoroutine(SpawnAnim());
    }

    private void Update()
    {
        timeText.text = time.ToString("0.0");

        if (timeIsRunning)
        {
            time -= Time.deltaTime;
            
            if (time <= 0)
            {
                timeIsRunning = false;
                timeOut = true;
                NotMatch();
            }
        }
    }

    IEnumerator SpawnAnim()
    {
        
        for (int i = 0; i < 20; i++)
        {
            while (true)
            {
                cardToSpawn = Random.Range(0, 10);

                if (card[cardToSpawn] != 2)
                {
                    yield return new WaitForSeconds(0.1f);
                    Instantiate(cardPrefabs[cardToSpawn], grid);

                    card[cardToSpawn]++;

                    break;
                }
            }
        }

        p1Text.enabled = true;

        popUpBackground.enabled = true;

        popUpAnim.SetTrigger("pop");

        yield return new WaitForSeconds(2f);

        popUpBackground.enabled = false;
        p1Text.enabled = false;

        popUpAnim.SetTrigger("default");

        player1Turn = true;

        canClick = true;

        TimeStart();
    }

    #region Matching

    public void NotMatch()
    {
        timeIsRunning = false;

        Card[] allCards = FindObjectsOfType<Card>();
        chosenCard1 = -1;
        chosenCard2 = -1;


        foreach (Card card in allCards)
        {
            if (card.cardIdentification == targetID1 || card.cardIdentification == targetID2)
            {

                if (card.transform.childCount > 1)
                {

                    Transform secondChild = card.transform.GetChild(1);
                    secondChild.gameObject.SetActive(true);
                }
            }
        }

        targetID1 = -1;
        targetID2 = -1;

        textMatch.color = Color.red;

        if (timeOut == true)
        {
            textMatch.text = "Time Out!";
            timeOut = false;
        }
        else
        {
            textMatch.text = "Failed!";
        }
      
        textMatchAnim.SetTrigger("pop");

        NextTurn();
    }

    public void Match()
    {
        timeIsRunning = false;

        chosenCard1 = -1;
        chosenCard2 = -1;
        
        DestroyObjectWithCardID();

        targetID1 = -1;
        targetID2 = -1;

        textMatch.color = Color.green;
        textMatch.text = "Match!";
        textMatchAnim.SetTrigger("pop");

        StartCoroutine(AfterAnim());

        if (player1Turn)
        {
            player1Score++;
            player1ScoreText.text = $"Player 1: {player1Score}";
        }
        else
        {
            player2Score++;
            player2ScoreText.text = $"Player 2: {player2Score}";
        }

        matches++;
        if (matches == 10)
        {
            gameOverText.enabled = true;
            StartCoroutine(EndGame());
        }
        else
        {
            TimeStart();
        }

    }

    IEnumerator AfterAnim()
    {
        yield return new WaitForSeconds(1);
        canClick = true;

        Card.isCouro = false;
    }

    public void NextTurn()
    {
        if (player1Turn == true)
        {
            player1Turn = false;
            player2Turn = true;

            p1Text.enabled = false;
            p2Text.enabled = true;
            Debug.Log("Player 2 Turn");

        }
        else
        {
            player1Turn= true;
            player2Turn= false;

            p1Text.enabled = true;
            p2Text.enabled = false;
            Debug.Log("Player 1 Turn");
        }

        StartCoroutine(Turn());
    }

    IEnumerator Turn()
    {
        yield return new WaitForSeconds(1);

        popUpBackground.enabled = true;

        popUpAnim.SetTrigger("pop");

        yield return new WaitForSeconds(2f);

        popUpBackground.enabled = false;

        p1Text.enabled = false;
        p2Text.enabled = false;

        popUpAnim.SetTrigger("default");

        canClick = true;

        Card.isCouro = false;

        TimeStart();
    }
    #endregion

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1);

        popUpBackground.enabled = true;

        popUpAnim.SetTrigger("pop");

        yield return new WaitForSeconds(1.5f);

        popUpBackground.enabled = false;

        SceneManager.LoadScene("End");
    }

    public void DestroyObjectWithCardID()
    {

        Card[] allCards = FindObjectsOfType<Card>();

        foreach (Card card in allCards)
        {

            if (card.cardIdentification == targetID1 || card.cardIdentification == targetID2)
            {
                foreach (Transform child in card.transform)
                {
                    child.gameObject.SetActive(false);
                }
                
            }
        }
    }

    public void TimeStart()
    {
        time = 5;
        timeIsRunning = true;
    }

}
