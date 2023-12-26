using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public Text timeText;
    public GameObject card;



    float time;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        for(int i = 0; i<16; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = time.ToString("N2");
    }
}
