using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleScreen : MonoBehaviour
{
    [Header("Turn info")]
    [SerializeField] private TMP_Text turnTimeText;

    [Header("Players")]
    [SerializeField] private BattleCharacter localPlayer;
    [SerializeField] private BattleCharacter remotePlayer;

    [Header("Skills")]
    [SerializeField] private GameObject skillPrefab;
    [SerializeField] private Transform skillRoster;

    [Header("Battle Results")]
    [SerializeField] private GameObject battleResultPanel;
    [SerializeField] private Button confirmBattleResultButton;
    [SerializeField] private TMP_Text battleResultText;

    private Coroutine timerCoroutine = null;

    private void Start()
    {
        confirmBattleResultButton.onClick.AddListener(() => { GameManager.Instance.OpenSelectScreen(); });
    }

    public void InitScreen()
    {
        battleResultPanel.SetActive(false);
    }

    public void StartTurn(int turnTime)
    {
        StopTimer();
        timerCoroutine = StartCoroutine(UpdateTurnTimer(turnTime));
    }

    private IEnumerator UpdateTurnTimer(int turnTime)
    {
        turnTimeText.text = "New Turn";
        for (int i = turnTime - 1; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            turnTimeText.text = "" + i;
        }
    }

    public void EndBattle(string result)
    {
        StopTimer();
        turnTimeText.text = "";
        battleResultText.text = "You " + result;
        battleResultPanel.SetActive(true);
    }

    private void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
    }

    public void UpdateLocalPlayerSkills(List<SkillData> skills)
    {
        foreach (Transform child in skillRoster)
        {
            Destroy(child.gameObject);
        }

        foreach (SkillData skill in skills)
        {
            GameObject skillObj = Instantiate(skillPrefab, skillRoster);
            skillObj.GetComponent<Skill>().Populate(skill);
        }
    }

    public void SetLocalPlayer(BattleCharacterData characterData)
    {
        localPlayer.Populate(characterData);
    }

    public void SetRemotePlayer(BattleCharacterData characterData)
    {
        remotePlayer.Populate(characterData);
    }
}
