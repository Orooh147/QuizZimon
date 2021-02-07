using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class QuizManager : MonoBehaviour
{

        internal GameManager _gameManager;




        private static QuizManager _instance;

        public static QuizManager Instance { get { return _instance; } }


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
    }





