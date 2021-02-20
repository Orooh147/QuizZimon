using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    QuizManager _quizManager;
    WebHandler _webHandler;
    internal Player sessionPlayer;
    internal string CompetitorName;
    internal string WinnerUserName;
    internal Winstate winstate;
    internal ResultScreen _resultScreen;
    internal WaitingRoom _WaitingRoom;
    internal bool GameEnd;
 
    internal bool gameReady;
    public int timeInCaulculatingScreen;

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
    public void CreateCharacter(string id,string userName)
    {
        sessionPlayer = new Player(id,userName);

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
    public void DeclareWinner(string winner)
    {
        _quizManager.winningCondition = winner;
        GameOver();
    }
    public void GameOver()
    {
        _quizManager.gameOn = false;
    }
    public void QuitGame()
    {
        _webHandler.DeleteBattle();
        StartCoroutine(EndGame());
    }
    IEnumerator EndGame()
    {
        while (!GameEnd)
        {
            yield return new WaitForFixedUpdate();
        }
        Application.Quit();
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
        
 
    public void SendNewQuestion()
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
    public void GoToQuizResultScreen()
    {

        SceneManager.LoadScene(5);
    
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public IEnumerator WaitForPlayers()
    {

        while (!gameReady)
        {


            _webHandler.BattleRequest();


            yield return new WaitForSeconds(0.5f);
        }
        _WaitingRoom.gameReady = true;
        yield return new WaitForSeconds(_WaitingRoom.TimeFromReadyToStart + 1);
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
