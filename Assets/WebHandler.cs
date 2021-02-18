using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebHandler : MonoBehaviour
{
    GameManager _gameManager;
    private static WebHandler _instance;
    public static WebHandler Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            _instance = this;
        }
    }

    void Start()
    {

        _gameManager = GetComponent<GameManager>();
    }
    public void LoginRequest(string username, string password)
    {
        StartCoroutine(Login(username, password));

    }
    public void SetBattleScoreRequest(string id, string newscore)
    {
        StartCoroutine(SetBattleScore(id,newscore));
    }
    public void SetScoreRequest(string id, string newscore)
    {
        StartCoroutine(SetScore(id, newscore));
    }
    public void InsertQuestionsList(List<Question> questionList)
    {
        StartCoroutine(InsertQuestions(questionList));
    }
    public void RegisterRequest(string username, string password)
    {
        StartCoroutine(RegisterUser(username, password));
    }


    IEnumerator GetUsers()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost/UnityBackend/GetUsers.php"))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

           // string[] pages = uri.Split('/');
          //  int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
 
    IEnumerator Login(string username,string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                _gameManager.CreateCharacter(www.downloadHandler.text);
                StartCoroutine(AssignBattle(www.downloadHandler.text));
                
            }
        }
    }
   
        
   
    IEnumerator AssignBattle(string id)
    {
        
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/AssignBattle.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                
                   _gameManager.GoToLoadingScreen();
                


            }
        }
    }
    public void BattleRequest()
    {
        StartCoroutine(StartCheck());
    }
    IEnumerator StartCheck()
    {
        
        WWWForm form = new WWWForm();
       


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/StartCheck.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == "'2'")
                {
                    _gameManager.gameReady = true;
                }
                else Debug.Log("waiting");


            }
        }
                
    }
    IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                _gameManager.GoToScene(2);
                
            }
        }
    }
    IEnumerator SetBattleScore(string id, string newScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerid", id);
        form.AddField("scorevalue", newScore);
        


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/UpdateBattleScore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

            }
        }
    }
    IEnumerator SetScore(string id,string newScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerid", id);
        form.AddField("scorevalue", newScore);
     

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/UpdateScore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

            }
        }
    }
    
    public void CheckPlayerAnswer(string id,string rightanswer,string questionNumber,bool isLast)
    {
        StartCoroutine(AnswerCheck(id,rightanswer,questionNumber,isLast));
    }
    public void CheckServerGameCondition()
    {
        StartCoroutine(GameCheck());
    }
    IEnumerator GameCheck()
    {
        WWWForm form = new WWWForm();
     

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/GameCheck.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == "1")
                {
                    _gameManager.RecieveAnswer(true);
                }
                _gameManager.RecieveAnswer(false);

            }
        }
    }
    IEnumerator AnswerCheck(string id, string rightanswer,string questionNumber ,bool lastMoveDone)
    {
        
        WWWForm form = new WWWForm();
        form.AddField("playerid", id);
        form.AddField("rightanswer", rightanswer);
        form.AddField("number", questionNumber);
        if (lastMoveDone)
        {
            
            form.AddField("true", "1");
        }
        else
        {
           
            form.AddField("true", "0");
        }


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/CheckAnswer.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == "1")
                {
                    _gameManager.RecieveAnswer(true);
                }
                else
                {
                _gameManager.RecieveAnswer(false);

                }

            }
        }
    }

    public void DeleteQuestionsRequest()
    {
        StartCoroutine(DeleteQuestions());
    }
    IEnumerator DeleteQuestions()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/DeleteQuestions.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

            }
        }

    }


    
    IEnumerator InsertQuestions(List<Question> questionList)
    {
     
        foreach(Question found in questionList)
        {
    
            WWWForm form = new WWWForm();
           form.AddField("question",found.question);
           form.AddField("answer1", found.answer1);
           form.AddField("answer2", found.answer2);
           form.AddField("answer3", found.answer3);
           form.AddField("answer4", found.answer4);
           form.AddField("number", _gameManager.IntToString(found.questionNumber));
            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/InsertQuestions.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);

                }
            }

        }

    }
}

