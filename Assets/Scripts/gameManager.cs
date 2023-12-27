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
    public Text matchText;
    public Text scoreText;
    public Text setText;
    public Text bestScoreTxt;

    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject endPanel;

    public AudioSource audioSource;
    public AudioClip tada;
    public AudioClip fail;
    public AudioClip urgent;

    float time = 60.0f; //�ð�
    float score = 10.00f; //���� ����
    
    int matchCount = 0; //��Ī Ƚ��
    int card_type = 16; //ī�� ����
    int card_count = 16; //���ӿ� ���Ǵ� ī�� ��ü ����


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        int[] card_N = new int[card_count];

        //��ü ī�� ����ŭ �迭 ���� �� �� �Է�
        for (int i = 0; i < card_count; i++)
        {
            card_N[i] = i;
        }

        //���� ����
        card_N = card_N.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        //ī�� ¦�� ���߱� ���� ī�� ���� �������� ������ �� ���� �Է�
        for(int i = 0; i < card_count; i++)
        {
            card_N[i] = card_N[i % (card_count / 2)];
        }

        //���� ���� << 2���� ������ �ϴ� ������ ¦�� �� ���� �� �ֱ� ����
        card_N = card_N.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();


        //��ü ī�� ������ŭ instance ����
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
        
        //15�� ���ϸ� timeText ���� ���������� ���� + urgent sound ���
        if (time <= 15.00f)
        {
            timeText.text = "<color=#960707>" + time.ToString("N2") + "</color>";

            if(time >= 14.90f)
            {
                audioSource.PlayOneShot(urgent);
            }
        }

        //0.00�� ������ ��� ���� ����
        if (time <= 0.00f)
        {
            Invoke("GameEnd", 0.001f);

            time = 0.00f;

            score += time;
        }

        matchText.text = matchCount.ToString();
        scoreText.text = score.ToString("N2");
    }

    //��Ī �Լ�
    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        //ī�� ��Ī ���� ��
        if(firstCardImage == secondCardImage)
        {
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("cards").transform.childCount;

            audioSource.PlayOneShot(tada);

            score += 6;

            //������ ī�� ��Ī���� �� ���� ����
            if (cardsLeft == 2)
            {
                Invoke("GameEnd", 0.001f);

                score += time;
            }
        }
        //ī�� ��Ī ���� ��
        else 
        {
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();

            audioSource.PlayOneShot(fail);

            time -= 1.0f;
        }

        matchCount++;

        score -= 1;

        if(score <= 0.00f)
        {
            score = 0;
        }

        //�ʱ�ȭ
        firstCard = null;
        secondCard = null;
    }

    void GameEnd()
    {
        Time.timeScale = 0f;

        endPanel.SetActive(true);

        if (PlayerPrefs.HasKey("bestscore") == false)
        {
            PlayerPrefs.SetFloat("bestscore", score);
        }
        else
        {
            if (score > PlayerPrefs.GetFloat("bestscore"))
            {
                PlayerPrefs.SetFloat("bestscore", score);
            }
        }

        float maxScore = PlayerPrefs.GetFloat("bestscore");

        setText.text = scoreText.text;
        bestScoreTxt.text = maxScore.ToString("N2");
    }
}
