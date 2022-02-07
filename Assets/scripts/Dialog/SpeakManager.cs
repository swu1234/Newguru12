using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakManager : MonoBehaviour
{
    //��ũ��Ʈ
    public TalkManager talkManager;

    //��ȭâ
    public GameObject talkPanel;
    public Text talkText;
    public Text NameText;
    public Image portraitImg;
    public GameObject scanObject;
    public bool isAction; //��ȭâ Ȱ��ȭ ����
    public int talkIndex;

    void Start()
    {

    }

    void Update()
    {

    }


    //������Ʈ ����â, PC/NPC �̸�â
    //public string name;

    public void Action(GameObject scanObj)
    {
        /*
        scanObject = scanObj;
        talkText.text = "�̰��� �̸���" + scanObject.name + "�̶�� �Ѵ�.";

        if (isAction) // ������ �ƴҶ� ->��ȭâ ���ֱ� 
        {
            isAction = false;
        }
        else //������ ->��ȭâ ���� 
        {
            isAction = true;
            */

        scanObject = scanObj;
        name = scanObject.name;
        Objdata objData = scanObject.GetComponent<Objdata>();
        //TalkText.text = "�̰��� "+scanObject.name+"�̴�."
        Talk(objData.id, objData.isNPC);

        talkPanel.SetActive(isAction); //��ȭâ Ȱ��ȭ ���¿� ���� ��ȭâ Ȱ��ȭ ����
    }


    void Talk(int id, bool isNPC)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null) //��ȯ�� ���� null�̸� ���̻� ���� ��簡 �����Ƿ� action���º����� false�� ���� 
        {
            isAction = false;
            talkIndex = 0; //talk�ε����� ������ �� ���ǹǷ� �ʱ�ȭ
            return; //void������ return �Լ� �������� (���� �ڵ�� ������� ����)
        }

        if (isNPC)
        {
            talkText.text = talkData.Split(':')[0]; //�����ڷ� ������ ������  0: ��� 1:portraitIndex
            //portraitImg.sprite = GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);

            if (int.Parse(talkData.Split(':')[1]) == 1)
            {
                NameText.text = "���蚜";
            }
            else
            {
                NameText.text = name; //�������� ���� �̸� UI�� �̸� �����ص� name��� 
            }
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        //���� ������ �������� ���� talkData�� �ε����� �ø�
        isAction = true; //��簡 ���������Ƿ� ��� ����Ǿ����
        talkIndex++;
    }
}