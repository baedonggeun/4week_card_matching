using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCard()
    {
        anim.SetBool("isOpen", true);

        transform.Find("front").gameObject.SetActive(true);

        transform.Find("back").gameObject.SetActive(false);
    }

}
