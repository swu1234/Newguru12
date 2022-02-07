using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eding : MonoBehaviour
{
    public Gameobject go;

    private void OnTriggerStay2D(Collide2D collision)
    {


        if (Input.GetKeyDown(KeyCode.Z))
        {


            go.SetActive(true);
        }
    }
}
