using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagerment;
public class title : MonoBehaviour
{
    private FadeManager theFade;
    private AudioManager theAudio;

    public string click_sound;
    private PlayerManager the Player;
    private GameManager theGM;



    // Start is called before the first frame update
    void Start()
    {
        the Fade = FindObjectOfType<FadeManager>();
        theAudio = FindObjectOfType<AudioManager>();
        thePlyer = FindObjectOfType<PlayerManager>();
        theGM = FindObjectOfType<GameManager>();
    }

    public void StartGame()
    {

        StartCorouotine(GameStartCoroutine());
    }



    IEnumerator GamestartCoroutine()
    {
        theFade.Fadeout();
        theAudio.Play(click_sound);
        yield return new WaitForEndSeconds(2f);
        Color color = thePlayer.GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        thePlayer.GetComponent<SpriteRenderer>().color = color;
        thePlayer.currentMapName = "forest";
        thePlayer.currentSceneName = "start";

        the GM.LoadStart();
        SceneManager.LoadScene("start");

    }


    public void ExitGame()
    {
        theAudio.Play(click_sound);
        Application.Quit();

    }
}
