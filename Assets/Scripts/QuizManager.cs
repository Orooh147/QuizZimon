using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Winstate { Tie, Win, Lose };
public class QuizManager : MonoBehaviour
{
    [SerializeField] QuestionSource questionSource;
    internal GameManager _gameManager;
    internal GameBoard _gameBoard;
    private static QuizManager _instance;
    internal List<Question> questionsList = new List<Question>();
    int currentQuestion = 0;
    public int TimeBetweenRounds;
    internal bool gameOn = true;
    internal string winningCondition;
    
    private Winstate winstate;

   
    //string winnerName;
  



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
    public void StartQuiz()
    {
        
        StartCoroutine(Quiz());
    }
    public IEnumerator Quiz()
    {
        for (int i = 0; i < questionsList.Count; i++)
        {
            currentQuestion = i;
            if (i == questionsList.Count - 1)
            {
            PostQuestion(currentQuestion,true);
            }
            else
            {

            PostQuestion(currentQuestion,false);
            }
        yield return new WaitForSeconds(TimeBetweenRounds);
            Debug.Log("roundover");
        }
        while (gameOn)
        {

            _gameManager.CheckGameCondition();

            yield return new WaitForSeconds(0.5f);
            if (!gameOn)
            {

                if (winningCondition == "tie")
                {
                    winstate = Winstate.Tie;
                }
                else
                {
                    if (winningCondition == _gameManager.sessionPlayer.userName)
                    {
                        winstate = Winstate.Win;
                    }
                    else { winstate = Winstate.Lose; }
                    _gameManager.WinnerUserName = winningCondition;
                }
            }
        }
        _gameManager.winstate = winstate;
        _gameManager.GoToQuizResultScreen();
        
       
        

      
    }


    public void LoadGame()
    {
        StartCoroutine(LoadQuestions());
    }

    public IEnumerator LoadQuestions()
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
           
            
        }

            Debug.Log(questionsList.Count);
        _gameManager.InsertQuestionList(questionsList);
        yield return new WaitForSeconds(3);
        StartQuiz();
       
    }
    public void PostQuestion(int questionNumber,bool isLast)
    {
        _gameBoard.PresentQuestion(questionsList[questionNumber],isLast);
    }
    public void ChangeScore()
    {
        _gameManager.SetScore();
    }
    public void ReciveAnswer(int ansNumber,bool isLast)
    {
        _gameManager.CheckAnsWithServer(ansNumber,questionsList[currentQuestion].questionNumber,isLast);
    }
    

}





