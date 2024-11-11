using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Button button;
    private bool isCouro = false;
    public int cardIdentification;

    private GameManager gameManager;

    void Start()
    {
        button = GetComponentInChildren<Button>();
        gameManager = FindObjectOfType<GameManager>();

        for (int i = 0; i < 20; i++)
        {
            if (ArrayCard.cardId[i] == -1)
            {
                ArrayCard.cardId[i] = i;
                cardIdentification = ArrayCard.cardId[i];
                break;
            }
        }
    }

    private void Update()
    {
        if (!isCouro)
        {
            if (gameManager.chosenCard1 != -1 && gameManager.chosenCard2 != -1)
            {
                StartCoroutine(DelaySeconds());
                isCouro = true;
            }
        }
    }

    IEnumerator DelaySeconds()
    {
        if (gameManager.chosenCard1 != gameManager.chosenCard2)
        {
            yield return new WaitForSeconds(2);

            gameManager.NotMatch();

            isCouro = false;
        }
        else
        {
            yield return new WaitForSeconds(2);

            gameManager.Match(); 

            isCouro = false;
        }
    }
}
