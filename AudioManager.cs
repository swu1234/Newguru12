using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class Sound
{
    public string name; // ���� �̸�

    public AudioClip clip; // ��������
    private AudiSource source; // ���� �÷��̾�

    public float Volumn;
    public bool loop;

    public void SetSource(AudioSource_source)
    {

        source = _source;
        source.clip = clip;
        source.loop = loop;

    }

    public void SetVolumn()
    {
        source.volume = SetVolumn();

    }
    public void Play()
    {

        source.Play();
    }

    public void Stop()
    {

        source.Stop(); 
    }
    public void SetLoop()
    {
        source.loop = true;
    }

    public void SetLoopCancel()
    {

        source.loop = false;
    }
}


public class AudioManager : MonoBehaviour


    static public AudioManager instance;
    [Serializable]
    public Sound[] sounds;




    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i =0;i<sounds.Length;i++)
        {

            GameObject soundObject = new GameObject("���� ���� �̸� : " + i + "=" + sounds[i].name);
            sounds[i].SetSource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }
    }

    public void Play(string _name)
    {

        for (int i =0; i <sounds.Length; i++ )
        {
            if(_name == sounds[i].name)
            {

                sounds[i].Play();
                return;
            }
        }

    }


    public void Stop(string _name)
    {

        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].Stop)
            {

                sounds[i].Play();
                return;
            }
        }

    }


    public void SetLoop(string _name)
    {

        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {

                sounds[i].SetLoop();
                return;
            }
        }

    }



    public void SetLoopCencel(string _name)
    {

        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {

                sounds[i].SetLoopCancel();
                return;
            }
        }

    }

    public void SetVolumn(string _name,float _ Volumn)
    {

        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {

                sounds[i].SetVolumn = _Volumn
                sounds[i].SetVolumn();
                return;
            }
        }

    }


}
