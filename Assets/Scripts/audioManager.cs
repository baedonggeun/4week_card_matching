using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioManager : MonoBehaviour
{
    private static audioManager Audio;

    // 게임 시작과 동시에 싱글톤을 구성
    private void Awake()
    {
        // 싱글톤 변수 instance가 비어있는가?
        if (Audio == null)
        {
            // Audio가 비어있다면(null) 그곳에 자기 자신을 할당
            Audio = this;

            //sceneLoaded 를 통해 OnSceneLoaded 함수를 Scene Load 시, 실행
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        // Audio에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우
        else if (Audio != this)
        {
            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Destroy(gameObject);
        }

        //Scene이 전환되어도 object가 없어지지 않도록 함
        DontDestroyOnLoad(gameObject);
    }

    public AudioClip[] bgmList;
    public AudioSource audioSource;

    //Scene 이 로딩됐을 때 해당 Scene 이름과 같은 이름의 bgm 재생
    //SceneManager.sceneLoaded += OnSceneLoaded;로 인해 Scene 전환 시, 매 Scene마다 호출됨
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
