using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransistor :MonoBehaviour
{
    private static SceneTransistor _instance;
    public static SceneTransistor Instance { get { return _instance; } }


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
    public void GoToScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}
