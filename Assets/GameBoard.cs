using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    QuizManager _quizManager;
    private void Start()
    {
        _quizManager = QuizManager.Instance;
    }
    public void GetPoint()
    {

        _quizManager.ChangeScore();
    }
    
}
