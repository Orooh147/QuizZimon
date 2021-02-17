using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] int minNameLenght=1;
    [SerializeField] int minPassLenght=6;
    // Start is called before the first frame update
    public InputField nameField;
    public InputField passwordField;
    WebHandler _webHandler;
    SceneTransistor _sceneTransistor;
    public Button submitButton;

    private void Start()
    {
        _webHandler = WebHandler.Instance;
        _sceneTransistor = SceneTransistor.Instance;
    }

    public void TryLogin()
    {
        LoginRequest();
    }

    public void LoginRequest()
    {

        _webHandler.LoginRequest(nameField.text,passwordField.text);  
        
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= minNameLenght && passwordField.text.Length >= minPassLenght);
    }
}
