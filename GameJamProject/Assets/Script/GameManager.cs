using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

    #endregion

    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            while (true)
            {
                int cardToSpawn = Random.Range(0, 10);

                if (card[cardToSpawn] != 2)
                {
                    Instantiate(cardPrefabs[cardToSpawn], grid);
                    card[cardToSpawn]++;
                    break;
                }
            }
        }
    }

    public void NotMatch()
    {
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

        canClick = true;

        targetID1 = -1;
        targetID2 = -1;
    }

    public void Match()
    {
        chosenCard1 = -1;
        chosenCard2 = -1;
        canClick = true;

        DestroyObjectWithCardID();

        targetID1 = -1;
        targetID2 = -1;
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

}
