using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioManager : MonoBehaviour
{
    //sound 를 한 곳에서 관리할 수 있도록 singleton 화
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

        //Scene이 전환되어도 object가 없어지지 않도록 함
        DontDestroyOnLoad(gameObject);
    }

    public AudioClip[] bgmList;
    public AudioSource audioSource;

    //Scene 이 로딩됐을 때 해당 Scene 이름과 같은 이름의 bgm 재생
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
    
    //배경음 플레이 함수
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
