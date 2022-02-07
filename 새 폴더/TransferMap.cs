using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    //build setting�� scene �߰����ֱ�
    public string transferMapName; // �̵��� ���� �̸�

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


    private void OnTriggerEnter2D(Collider2D collision) // �̵��ϰ��� �ϴ� �κп� box collider�� ��ġ�ϰ�, �� �κп� ���� �� �� �̵�, is trigger Ȱ��ȭ �ʿ�
    {
        if(collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName;
            //theCamera.SetBound(targetBound); // ?
            SceneManager.LoadScene(transferMapName);
        }
    }
}
