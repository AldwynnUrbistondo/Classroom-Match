using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour
{
    public Button button;
    public int cardValue;
    public bool isChosen = false;
    public int cardPlacement;
    

    private GameManager gameManager;

    private void Start()
    {
        button = GetComponentInParent<Button>();
        gameManager = FindObjectOfType<GameManager>();

        Card parentScript = GetComponentInParent<Card>();
        cardPlacement = parentScript.cardIdentification;
    }

    public void buttonClick()
    {
        if (gameManager.canClick)
        {
            if (gameManager.chosenCard2 == -1)
            {
                if (gameManager.chosenCard1 == -1)
                {
                    isChosen = true;
                    gameManager.chosenCard1 = cardValue;
                    button.gameObject.SetActive(false);
                }
                else
                {
                    isChosen = true;
                    gameManager.chosenCard2 = cardValue;
                    button.gameObject.SetActive(false);
                    gameManager.canClick = false;
                }
            }

            if (gameManager.targetID1 == -1)
            {
                gameManager.targetID1 = cardPlacement;
            }
            else
            {
                gameManager.targetID2 = cardPlacement;
            }
        }

        
    }
}
