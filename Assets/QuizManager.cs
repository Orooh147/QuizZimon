using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] QuestionSource questionSource;
    internal GameManager _gameManager;
    private static QuizManager _instance;
    List<Question> questionsList = new List<Question>();
    int currentQuestion;
  



    public static QuizManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


 
    public void LoadQuestions()
    {
        Question[] questionSheet = new Question[questionSource.questions.Length];
        for (int i = 0; i < questionSheet.Length; i++)
        {
            questionSheet[i] = new Question();
        }


        for (int i = 0; i < questionSource.questions.Length; i++)
        {

            questionSheet[i].insertData(questionSource.questions[i].question, questionSource.questions[i].answer1, questionSource.questions[i].answer2,
                questionSource.questions[i].answer3, questionSource.questions[i].answer4, questionSource.questions[i].rightAns);
     
        }
        for (int i = 0; i < questionSheet.Length; i++)
        {
            questionsList.Add(questionSheet[i]);
            questionSheet[i].questionNumber = i;
            //adjust for computer numbers
            questionSheet[i].rightAns --;
        }

            Debug.Log(questionsList.Count);
        _gameManager.InsertQuestionList(questionsList);
       
           
       
    }
    public void ShowNext()
    {

    }
    public void ChangeScore()
    {
        _gameManager.SetScore();
    }
    

}





