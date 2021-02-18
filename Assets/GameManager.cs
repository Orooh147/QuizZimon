using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    QuizManager _quizManager;
    WebHandler _webHandler;
    internal Player sessionPlayer;

    [SerializeField] float RoundTime;
    internal bool gameReady;

    public void Start()
    {
        _webHandler = WebHandler.Instance;
       
        
    }
    public void InitQuiz()
    {
        _quizManager = QuizManager.Instance;
        if (_quizManager != null)
        {
        _quizManager._gameManager = this;
        }

        _webHandler.DeleteQuestionsRequest();
        _quizManager.LoadGame();
       // _quizManager.StartQuiz();
    }
    public void CreateCharacter(string id)
    {
        sessionPlayer = new Player(id);
    }
  

    public void ReloadRound()
    {
        
    }
    public void CheckAnsWithServer(int ans,int question,bool isLast)
    {
        _webHandler.CheckPlayerAnswer(sessionPlayer.id, IntToString(ans),IntToString(question),isLast);
        
    }
    public void CheckGameCondition()
    {
        _webHandler.CheckServerGameCondition();

    }
    public void RecieveAnswer(bool rightAnswer)
    {
        if (rightAnswer)
        {
            sessionPlayer.score += 1;
            SetScore();
        }
        else
        {
            Debug.Log("wrong answer");
        }
    }
        
    public IEnumerator RoundTimer()
    {
        yield return new WaitForSeconds(RoundTime);
        RoundOver();

    }
    public void SendNewQuestion()
    {

    }
    public void RoundOver()
    {

    } 
    public void SetRegistered()
    {

    }
    public void SetScore()
    {
        Debug.Log("setting score!!!");
        _webHandler.SetBattleScoreRequest(sessionPlayer.id,IntToString(sessionPlayer.score));
    }
    public string IntToString(int intsource)
    {
        return intsource.ToString();
    }
    public void GoToLoadingScreen()
    {
        SceneManager.LoadScene(3);
        StartCoroutine(WaitForPlayers());

    }
    public IEnumerator WaitForPlayers()
    {

        while (!gameReady)
        {


            _webHandler.BattleRequest();


            yield return new WaitForSeconds(0.5f);
        }

        StartGame();
     


    }
    public void StartGame()
    {
        SceneManager.LoadScene(4);
        InitQuiz();

    }
    public void InsertQuestionList(List<Question> questionList)
    {
        _webHandler.InsertQuestionsList(questionList);
    }
    public void GoToScene(int index)
    {
        SceneManager.LoadScene(index);
    }
  


    



}
