using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    QuizManager _quizManager;

    [SerializeField] float RoundTime;

    public void Start()
    {
        InitQuiz();

    }
    

    public void InitQuiz()
    {
        _quizManager = QuizManager.Instance;
        _quizManager._gameManager = this;
    }

    public void ReloadRound()
    {

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
    


    



}
