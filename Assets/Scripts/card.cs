using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public Animator anim;

    public AudioClip flip; //플레이할 음악 파일
    public AudioSource audioSource; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //카드 open시 front를 active로 하고 back을 false 상태로 바꿈
    //첫번째 선택한 카드인지 아닌지 구분하여 저장 및 2번째 선택한 카드면 isMatched 호출
    public void OpenCard()
    {
        anim.SetBool("isOpen", true);

        transform.Find("front").gameObject.SetActive(true);

        transform.Find("back").gameObject.SetActive(false);


        if(gameManager.I.firstCard == null)
        {
            gameManager.I.firstCard = gameObject;
        }
        else
        {
            gameManager.I.secondCard = gameObject;
            gameManager.I.isMatched();
        }

        //openCard 함수 호출 될 때마다 flip sound 재생
        audioSource.PlayOneShot(flip);

    }

    //public 함수 내에서 다시 Invoke로 Destroy 사용하는 이유는 
    //gameObject를 파괴하기 때문에 중요한 함수여서 외부에서 접근 막기 위해
    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 0.1f);
    }

    void destroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void closeCard()
    {
        Invoke("closeCardInvoke", 1.0f);
    }

    void closeCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);
    }

}
