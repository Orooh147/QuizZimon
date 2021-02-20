using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour
{
    QuizManager _quizManager;
    [SerializeField] Text textquestion;
    [SerializeField] Text textanswer1;
    [SerializeField] Text textanswer2;
    [SerializeField] Text textanswer3;
    [SerializeField] Text textanswer4;
    [SerializeField] Text TimerText;
    [SerializeField] Text TimerText2;
    bool lastQuestion=false;
    bool moveDone=true;




    private void Start()
    {
       
        _quizManager = QuizManager.Instance;
        _quizManager._gameBoard = this;
    }
    public void GetPoint()
    {

        _quizManager.ChangeScore();
    }
    
  
    public void PresentQuestion(Question question,bool isLast)
    {
        moveDone = false;
        StartCoroutine(VisualTimer());
        lastQuestion = isLast;
        textquestion.text = question.question;
        textanswer1.text = question.answer1;
        textanswer2.text = question.answer2;
        textanswer3.text = question.answer3;
        textanswer4.text = question.answer4;
        
    }
    public void answer1()
    {
        if (!moveDone)
        {
            _quizManager.ReciveAnswer(0, lastQuestion);
            moveDone = true;
        }
        
    }
    public void answer2()
    {
        if (!moveDone)
        {
            _quizManager.ReciveAnswer(1, lastQuestion);
            moveDone = true;
        }
    }
    public void answer3()
    {
        if (!moveDone)
        {
            _quizManager.ReciveAnswer(2, lastQuestion);
            moveDone = true;
        }
    }
    public void answer4()
    {
        if (!moveDone)
        {
            _quizManager.ReciveAnswer(3, lastQuestion);
            moveDone = true;
        }
    }
    
    public IEnumerator VisualTimer()
    {
        int roundTime = _quizManager.TimeBetweenRounds;
        for (int i = 0; i < roundTime; i++)
        {
            TimerText.text = (roundTime - i).ToString();
            TimerText2.text = (roundTime - i).ToString();
            yield return new WaitForSeconds(1);
        }

    }
}
