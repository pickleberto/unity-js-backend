using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIOClient;

public class BattleManager : MonoBehaviour
{
    
    [SerializeField] private GameObject skillPrefab;
    [SerializeField] private Transform skillRoster;
    
    [SerializeField] private Image player1Image;
    [SerializeField] private Image player2Image;

    public static BattleManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartBattle(SocketIOResponse resp)
    {
        GameManager.Instance.OpenBattleScreen();

    }
}
