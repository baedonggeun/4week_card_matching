using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class gameManager : MonoBehaviour
{
    public Text timeText;
    public GameObject card;



    float time;

    int card_count = 16; //���ӿ� ���Ǵ� ī�� ��ü ����
    int card_type = 16; //ī�� ����

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        int[] image_N = new int[card_type];

        for (int j = 0; j < card_count; j++)
        {
            image_N[j] = j % (card_count / 2);
        }

        image_N = image_N.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();


        for (int i = 0; i< card_count; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform;

            float x = (i % 4) * 1.4f - 2.1f;
            float y = -(i / 4) * 1.9f + 2.5f;
            newCard.transform.position = new Vector3(x, y, 0);

            string cardName = "card" + image_N[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = time.ToString("N2");
    }
}
