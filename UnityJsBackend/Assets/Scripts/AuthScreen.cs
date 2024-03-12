using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AuthScreen : MonoBehaviour
{
    [Header("Login")]
    [SerializeField] private TMP_InputField loginUsername;
    [SerializeField] private TMP_InputField loginPassword;
    [SerializeField] private Button loginButton;
    
    [Header("Register")]
    [SerializeField] private TMP_InputField registerUsername;
    [SerializeField] private TMP_InputField registerEmail;
    [SerializeField] private TMP_InputField registerPassword;
    [SerializeField] private Button registerButton;

    private void Start()
    {
        loginButton.onClick.AddListener(Login);
        registerButton.onClick.AddListener(Register);
    }

    public void Login()
    {

    }

    public void Register()
    {

    }
}
