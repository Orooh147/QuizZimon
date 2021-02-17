using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    private QuestionSource questionSource;
    internal GameManager _gameManager;
    private static QuizManager _instance;
    List<string> questionsList = new List<string>();
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
        foreach(string found in questionSource.Questions)
        {
            questionsList.Add(found);
        }
        
    }
    public void ShowNext()
    {

    }
    

}





