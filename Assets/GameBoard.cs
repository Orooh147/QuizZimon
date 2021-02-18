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
    bool lastQuestion=false;




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
        lastQuestion = isLast;
        textquestion.text = question.question;
        textanswer1.text = question.answer1;
        textanswer2.text = question.answer2;
        textanswer3.text = question.answer3;
        textanswer4.text = question.answer4;
        
    }
    public void answer1()
    {
        _quizManager.ReciveAnswer(0,lastQuestion);
    }
    public void answer2()
    {
        _quizManager.ReciveAnswer(1,lastQuestion);

    }
    public void answer3()
    {
        _quizManager.ReciveAnswer(2,lastQuestion);

    }
    public void answer4()
    {
        _quizManager.ReciveAnswer(3,lastQuestion);

    }
}
