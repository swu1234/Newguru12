using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakManager : MonoBehaviour
{
    //스크립트
    public TalkManager talkManager;

    //대화창
    public GameObject talkPanel;
    public Text talkText;
    public Text NameText;
    public Image portraitImg;
    public GameObject scanObject;
    public bool isAction; //대화창 활성화 상태
    public int talkIndex;

    void Start()
    {

    }

    void Update()
    {

    }


    //오브젝트 설명창, PC/NPC 이름창
    //public string name;

    public void Action(GameObject scanObj)
    {
        /*
        scanObject = scanObj;
        talkText.text = "이것의 이름은" + scanObject.name + "이라고 한다.";

        if (isAction) // 실행중 아닐때 ->대화창 없애기 
        {
            isAction = false;
        }
        else //실행중 ->대화창 띄우기 
        {
            isAction = true;
            */

        scanObject = scanObj;
        name = scanObject.name;
        Objdata objData = scanObject.GetComponent<Objdata>();
        //TalkText.text = "이것은 "+scanObject.name+"이다."
        Talk(objData.id, objData.isNPC);

        talkPanel.SetActive(isAction); //대화창 활성화 상태에 따라 대화창 활성화 변경
    }


    void Talk(int id, bool isNPC)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null) //반환된 것이 null이면 더이상 남은 대사가 없으므로 action상태변수를 false로 설정 
        {
            isAction = false;
            talkIndex = 0; //talk인덱스는 다음에 또 사용되므로 초기화
            return; //void에서의 return 함수 강제종료 (밑의 코드는 실행되지 않음)
        }

        if (isNPC)
        {
            talkText.text = talkData.Split(':')[0]; //구분자로 문장을 나눠줌  0: 대사 1:portraitIndex
            //portraitImg.sprite = GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);

            if (int.Parse(talkData.Split(':')[1]) == 1)
            {
                NameText.text = "선배슌";
            }
            else
            {
                NameText.text = name; //나머지일 때는 이름 UI에 미리 저장해둔 name출력 
            }
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        //다음 문장을 가져오기 위해 talkData의 인덱스를 늘림
        isAction = true; //대사가 남아있으므로 계속 진행되어야함
        talkIndex++;
    }
}