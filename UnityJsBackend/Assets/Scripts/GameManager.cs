using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public string JwtToken { get; private set; }

    [SerializeField] private GameObject authScreen;
    [SerializeField] private GameObject selectScreen;
    [SerializeField] private GameObject battleScreen;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        OpenAuthScreen();
    }

    public void SetJwtToken(string token)
    {
        JwtToken = token;
    }

    public void OpenAuthScreen()
    {
        selectScreen.SetActive(false);
        battleScreen.SetActive(false);

        authScreen.SetActive(true);
    }

    public void OpenSelectScreen()
    {
        authScreen.SetActive(false);
        battleScreen.SetActive(false);

        selectScreen.SetActive(true);
    }

    public void OpenBattleScreen()
    {
        authScreen.SetActive(false);
        selectScreen.SetActive(false);

        battleScreen.SetActive(true);
    }
}