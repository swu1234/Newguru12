using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagerment;

public class title : MonoBehaviour
{
    public string click_sound;
    private MovingObject thePlayer;
    //private GameManager theGM;



    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<MovingObject>();
        //theGM = FindObjectOfType<GameManager>();
    }

    public void StartGame()
    {
        StartCoroutine(GameStartCoroutine());
    }

    IEnumerator GameStartCoroutine()
    {
        yield return new WaitForSeconds(2f);
        Color color = thePlayer.GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        thePlayer.GetComponent<SpriteRenderer>().color = color;
        thePlayer.currentMapName = "forest";
        //thePlayer.currentSceneName = "start";

        //the GM.LoadStart();
        //SceneManager.LoadScene("start");
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
