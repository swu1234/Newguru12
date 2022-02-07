using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    //build setting에 scene 추가해주기
    public string transferMapName; // 이동할 맵의 이름

    //public Transform target;
    //public BoxCollider2D targetBound; //?

    private MovingObject thePlayer;
    //private CameraManager theCamera; // ?

    // Start is called before the first frame update
    void Start()
    {
        //theCamera = FindObjectOfType<CameraManager>(); // ? 
        thePlayer = FindObjectOfType<MovingObject>();
    }


    private void OnTriggerEnter2D(Collider2D collision) // 이동하고자 하는 부분에 box collider을 설치하고, 그 부분에 닿을 시 맵 이동, is trigger 활성화 필요
    {
        if(collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName;
            //theCamera.SetBound(targetBound); // ?
            SceneManager.LoadScene(transferMapName);
        }
    }
}
