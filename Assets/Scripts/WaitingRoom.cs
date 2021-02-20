using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaitingRoom : MonoBehaviour
{
    GameManager _gameManager;
    [SerializeField] Text GameStatus;
    internal bool gameReady;
    [SerializeField] string gameReadyCount;
    [SerializeField] string GameReadyText;
    [SerializeField] string[] waitingTexts;
    public int TimeFromReadyToStart;
    

    // Start is called before the first frame update
    private void Awake()
    {      
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameManager._WaitingRoom = this;
    }
    void Start()
    {
        StartCoroutine(WaitForPlayers());
    }
    IEnumerator WaitForPlayers()
    {
        int count=0;
        while (!gameReady)
        {
            GameStatus.text = waitingTexts[count];


            
            yield return new WaitForSeconds(1);
            if (count == waitingTexts.Length - 1)
            {
                count = 0;
            }else
            {
                count++;
            }
        }
        for (int i = 0; i < TimeFromReadyToStart; i++)
        {
            GameStatus.text = gameReadyCount+(TimeFromReadyToStart-i);
            yield return new WaitForSeconds(1);
        }
        GameStatus.text = GameReadyText;
        
    }

}
