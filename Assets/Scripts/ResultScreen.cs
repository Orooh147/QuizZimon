using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{

    internal GameManager _gameManager;
    [SerializeField] string WinnerDecleration;
    [SerializeField] Text WinnerField;
    // Start is called before the first frame update
    public void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
      
    }
    void Start()
    {

        AnalizeResults();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AnalizeResults()
    {

        string Win = "you!";
        string Lose = _gameManager.WinnerUserName;
        string Tie = "Nobody...";
        


        switch (_gameManager.winstate)
        {
            case Winstate.Lose:
                WinnerField.text = WinnerDecleration + Lose;
                break;
            case Winstate.Win:
                WinnerField.text = WinnerDecleration + Win;
                break;
            case Winstate.Tie:
                WinnerField.text = WinnerDecleration + Tie;
                break;
        }
        
    }
    public void ExitGame()
    {
        _gameManager.QuitGame();
    }


}
