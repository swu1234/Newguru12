using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiztest : MonoBehaviour
{
    [SerializeField]
    public Quiz quiz;

    private QuizManager theQuiz;
    private MovingObject theMove;

    public bool flag;
    public bool mmove;

    // Start is called before the first frame update
    void Start()
    {
        theQuiz = FindObjectOfType<QuizManager>();
        theMove = FindObjectOfType<MovingObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!flag)
        {
            StartCoroutine(ACoroutine());
        }
    }

    IEnumerator ACoroutine()
    {
        flag = true;
        mmove = false;
        theMove.notMove();
        theQuiz.ShowQuiz(quiz);
        yield return new WaitUntil(() => !theQuiz.quizing);

        Debug.Log(theQuiz.GetResult());
    }
}
