using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    private string question;
    private List<string> answerList; // 답변모음

    public GameObject go; // 평소에 비활성화시킴

    public Text QuizText;

    public Text[] AnswerText;
    public GameObject[] AnswerPanel;

    public Animator anim;

    public bool quizing; // 대기
    private bool keyInput; // 키처리 활/비활성화

    private int count; // 배열 크기
    private int result; // 선택 결과

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    // Start is called before the first frame update
    void Start()
    {
        answerList = new List<string>();
        for(int i = 0; i<=1; i++) // answerlist 길이만큼, 우리는 2가지 답을 사용하므로 1로 설정.
        {
            AnswerText[i].text = "";
            AnswerPanel[i].SetActive(false);
        }
        QuizText.text = "";
    }

    public void ShowQuiz(Quiz _quiz)
    {
        quizing = true;
        go.SetActive(true);
        result = 0;
        question = _quiz.question;
        for (int i = 0; i < _quiz.answers.Length; i++)
        {
            answerList.Add(_quiz.answers[i]);
            AnswerPanel[i].SetActive(true);
            count = i;
        }

        anim.SetBool("Appear", true);
        Selection();
        StartCoroutine(QuizCoroutine());
    }

    public int GetResult() // 이 부분 수정을 통해 quiz 답변 조절 가능
    {
        if(result == 1)
        {
            print("땡!");
        }
        else
        {
            print("정답이야~");
        }
        return result;
    }

    public void ExitQuiz()
    {
        QuizText.text = "";
        for (int i= 0; i<= count; i++)
        {
            AnswerText[i].text = "";
            AnswerPanel[i].SetActive(false);
        }
        answerList.Clear();
        anim.SetBool("Appear", false);
        quizing = false;
        go.SetActive(false);
    }

    IEnumerator QuizCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(TypingQuestion());
        StartCoroutine(TypingAnswer1());
        if (count >= 1)
        {
            StartCoroutine(TypingAnswer2());
        }

        yield return new WaitForSeconds(0.4f);
        keyInput = true;
    }

    IEnumerator TypingQuestion()
    {
        for(int i = 0; i < question.Length; i++)
        {
            QuizText.text += question[i];
            yield return waitTime;
        }
    }

    IEnumerator TypingAnswer1()
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < answerList[0].Length; i++)
        {
            AnswerText[0].text += answerList[0][i];
            yield return waitTime;
        }
    }

    IEnumerator TypingAnswer2()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < answerList[1].Length; i++)
        {
            AnswerText[1].text += answerList[1][i];
            yield return waitTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(keyInput)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(result > 0)
                {
                    result--;
                }
                else
                {
                    result = count;
                }
                Selection();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(result < count)
                {
                    result++;
                }
                else
                {
                    result = 0;
                }
                Selection();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                keyInput = false;
                ExitQuiz();
            }
        }
    }

    public void Selection()
    {
        Color color = AnswerPanel[0].GetComponent<Image>().color;
        color.a = 0.75f;
        for(int i = 0; i <= count; i++)
        {
            AnswerPanel[i].GetComponent<Image>().color = color;
        }
        color.a = 1f;
        AnswerPanel[result].GetComponent<Image>().color = color;
    }
}
