using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioManager : MonoBehaviour
{
    private static audioManager Audio;

    // ���� ���۰� ���ÿ� �̱����� ����
    private void Awake()
    {
        // �̱��� ���� instance�� ����ִ°�?
        if (Audio == null)
        {
            // Audio�� ����ִٸ�(null) �װ��� �ڱ� �ڽ��� �Ҵ�
            Audio = this;

            //sceneLoaded �� ���� OnSceneLoaded �Լ��� Scene Load ��, ����
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        // Audio�� �̹� �ٸ� GameManager ������Ʈ�� �Ҵ�Ǿ� �ִ� ���
        else if (Audio != this)
        {
            // ���� �ΰ� �̻��� GameManager ������Ʈ�� �����Ѵٴ� �ǹ�.
            // �̱��� ������Ʈ�� �ϳ��� �����ؾ� �ϹǷ� �ڽ��� ���� ������Ʈ�� �ı�
            Destroy(gameObject);
        }

        //Scene�� ��ȯ�Ǿ object�� �������� �ʵ��� ��
        DontDestroyOnLoad(gameObject);
    }

    public AudioClip[] bgmList;
    public AudioSource audioSource;

    //Scene �� �ε����� �� �ش� Scene �̸��� ���� �̸��� bgm ���
    //SceneManager.sceneLoaded += OnSceneLoaded;�� ���� Scene ��ȯ ��, �� Scene���� ȣ���
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
