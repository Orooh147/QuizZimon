using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class QuestionSource : ScriptableObject
{
   
    public Question[] questions;


}
[System.Serializable]
public class Question 
{
    public void insertData(string _question , string _answer1, string _answer2, string _answer3, string _answer4,  int _rightAns)
    {
        question = _question;
        answer1 = _answer1;
        answer2 = _answer2;
        answer3 = _answer3;
        answer4 = _answer4;
        rightAns = _rightAns; 
    }
    public string question;
    public string answer1;
    public string answer2;
    public string answer3;
    public string answer4;
    public int rightAns;
    internal int questionNumber=1;



}
