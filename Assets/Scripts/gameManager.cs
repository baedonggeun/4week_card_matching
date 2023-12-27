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
    public GameObject endText;
    public Text scoreText;
    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;

    public AudioClip tada;
    public AudioClip fail;
    public AudioClip urgent;
    public AudioSource audioSource;


    float time = 60.0f;

   
    int card_type = 16; //카드 종류
    int card_count = 16; //게임에 사용되는 카드 전체 개수

    float score = 10.00f;
    float matchCount = 0.00f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        int[] card_N = new int[card_count];

        //전체 카드 수만큼 배열 생성 및 값 입력
        for (int i = 0; i < card_count; i++)
        {
            card_N[i] = i;
        }

        //랜덤 섞기
        card_N = card_N.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        //카드 짝을 맞추기 위해 카드 수의 절반으로 나누고 그 값을 입력
        for(int i = 0; i < card_count; i++)
        {
            card_N[i] = card_N[i % (card_count / 2)];
        }

        //랜덤 섞기 << 2번에 나눠서 하는 이유는 짝이 안 맞을 수 있기 때문
        card_N = card_N.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();


        //전체 카드 개수만큼 instance 생성
        for (int i = 0; i< card_count; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform;

            float x = (i % 4) * 1.4f - 2.1f;
            float y = -(i / 4) * 1.9f + 2.9f;
            newCard.transform.position = new Vector3(x, y, 0);

            string cardName = "card" + card_N[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeText.text = time.ToString("N2");
        scoreText.text = (score - matchCount).ToString("N2");

        //15초 이하면 timeText 색상 붉은색으로 변경 + urgent sound 재생
        if (time <= 15.0f)
        {
            timeText.text = "<color=#960707>" + time.ToString("N2") + "</color>";

            if(time >= 14.0f)
            {
                audioSource.PlayOneShot(urgent);
            }
        }

        //0.00초 이하일 경우 게임 종료
        if (time <= 0.00f)
        {
            Invoke("GameEnd", 0.0f);
        }
    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if(firstCardImage == secondCardImage)
        {
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("cards").transform.childCount;

            if(cardsLeft == 2)
            {
                Invoke("GameEnd", 0.1f);
            }

            audioSource.PlayOneShot(tada);

            matchCount++;

            score += 5;
        }
        else 
        {
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();

            audioSource.PlayOneShot(fail);

            time -= 1.0f;

            matchCount++;
        }

        firstCard = null;
        secondCard = null;
    }

    void GameEnd()
    {
        score += time;
        
        Time.timeScale = 0f;

        endText.SetActive(true);
    }
}
