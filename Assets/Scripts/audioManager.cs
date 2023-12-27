using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioManager : MonoBehaviour
{
    //sound �� �� ������ ������ �� �ֵ��� singleton ȭ
    private static audioManager _instance;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }

        //Scene�� ��ȯ�Ǿ object�� �������� �ʵ��� ��
        DontDestroyOnLoad(gameObject);
    }

    public AudioClip[] bgmList;
    public AudioSource audioSource;

    //Scene �� �ε����� �� �ش� Scene �̸��� ���� �̸��� bgm ���
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for(int i = 0; i < bgmList.Length; i++)
        {
            if(arg0.name == bgmList[i].name)
            {
                BgSoundPlay(bgmList[i]);
            }
        }
    }
    
    //����� �÷��� �Լ�
    public void BgSoundPlay(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.volume = 0.1f;
        audioSource.Play();
    }





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
