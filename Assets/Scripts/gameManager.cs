using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class gameManager : MonoBehaviour
{
    public static gameManager I;

    private void Awake()
    {
        I = this;
    }


    public Text timeText;
    public GameObject card;

    public GameObject firstCard;
    public GameObject secondCard;


    float time;

   
    int card_type = 16; //카드 종류
    int card_count = 16; //게임에 사용되는 카드 전체 개수

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        int[] card_N = new int[card_count];

        for (int i = 0; i < card_count; i++)
        {
            card_N[i] = i;
        }

        card_N = card_N.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for(int i = 0; i < card_count; i++)
        {
            card_N[i] = card_N[i % (card_count / 2)];
        }

        card_N = card_N.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();


        for (int i = 0; i< card_count; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform;

            float x = (i % 4) * 1.4f - 2.1f;
            float y = -(i / 4) * 1.9f + 2.5f;
            newCard.transform.position = new Vector3(x, y, 0);

            string cardName = "card" + card_N[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = time.ToString("N2");
    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if(firstCardImage == secondCardImage)
        {
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();
        }
        else 
        {
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
        }

        firstCard = null;
        secondCard = null;
    }
}
