﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Web;
public class Registration : MonoBehaviour
{

    [SerializeField] int minNameLeght;
    [SerializeField] int minPassLeght;
    WebHandler _webHandler;
    // Start is called before the first frame update
    public InputField nameField;
    public InputField passwordField;
    GameManager _gameManager;

    public Button submitButton;
    public void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
       
    }
    private void Start()
    {
        _webHandler = WebHandler.Instance;
    }

    public void TryRegister()
    {
        _webHandler.RegisterRequest(nameField.text, passwordField.text);
    }
    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= minNameLeght && passwordField.text.Length >= minPassLeght);
    }
    public void Back()
    {
        _gameManager.GoToMainMenu();
    }
}
