using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public Animator anim;

    public AudioClip flip; //�÷����� ���� ����
    public AudioSource audioSource; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ī�� open�� front�� active�� �ϰ� back�� false ���·� �ٲ�
    //ù��° ������ ī������ �ƴ��� �����Ͽ� ���� �� 2��° ������ ī��� isMatched ȣ��
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

        //openCard �Լ� ȣ�� �� ������ flip sound ���
        audioSource.PlayOneShot(flip);

    }

    //public �Լ� ������ �ٽ� Invoke�� Destroy ����ϴ� ������ 
    //gameObject�� �ı��ϱ� ������ �߿��� �Լ����� �ܺο��� ���� ���� ����
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
