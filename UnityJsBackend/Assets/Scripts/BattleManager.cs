using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIOClient;
using Newtonsoft.Json.Linq;

public class BattleManager : MonoBehaviour
{
    
    [SerializeField] private GameObject skillPrefab;
    [SerializeField] private Transform skillRoster;
    
    [SerializeField] private BattleCharacter localPlayer;
    [SerializeField] private BattleCharacter remotePlayer;

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

        JObject outerData = JObject.Parse(resp.GetValue<JObject>().ToString());

        var localPlayerData = JObject.Parse(outerData["left"].Value<string>());
        localPlayer.Populate(localPlayerData);
        remotePlayer.Populate(JObject.Parse(outerData["right"].Value<string>()));

        UpdateLocalPlayerSkills((JArray) localPlayerData["character"]["skills"], (JArray) localPlayerData["character"]["skillVars"]);
    }

    void UpdateLocalPlayerSkills(JArray skills, JArray skillsVars)
    {
        foreach(Transform child in skillRoster)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < skills.Count; i++)
        {
            JObject skill = (JObject)skills[i];
            JObject vars = (JObject)skillsVars[i];
            GameObject skillObj = Instantiate(skillPrefab, skillRoster);
            skillObj.GetComponent<Skill>().Populate(skill, vars, i);
        }
    }
}
