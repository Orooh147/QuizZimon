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

    public void Start()
    {
        _webHandler = WebHandler.Instance;
        InitQuiz();

    }
    public void InitQuiz()
    {
        _quizManager = QuizManager.Instance;
        if (_quizManager != null)
        {
        _quizManager._gameManager = this;
        }
        _quizManager.LoadQuestions();
       
    }
    public void CreateCharacter(string id)
    {
        sessionPlayer = new Player(id);
    }
  

    public void ReloadRound()
    {
        _quizManager.LoadQuestions();
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
        _webHandler.SetScoreRequest(sessionPlayer.id,IntToString(sessionPlayer.score));
    }
    public string IntToString(int intsource)
    {
        return intsource.ToString();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(3);
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
