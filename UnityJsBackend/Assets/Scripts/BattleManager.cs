using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIOClient;
using Newtonsoft.Json.Linq;

public class BattleManager : MonoBehaviour
{    
    [SerializeField] private BattleScreen battleScreen;

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
        battleScreen.InitScreen();
        StartTurn(resp);
    }

    public void StartTurn(SocketIOResponse resp)
    {
        JObject outerData = JObject.Parse(resp.GetValue<JObject>().ToString());

        var localPlayerData = JObject.Parse(outerData["left"].Value<string>());
        var remotePlayerData = JObject.Parse(outerData["right"].Value<string>());

        battleScreen.SetLocalPlayer(Utils.ParseCharacter(localPlayerData));
        battleScreen.SetRemotePlayer(Utils.ParseCharacter(remotePlayerData));

        UpdateLocalPlayerSkills((JArray)localPlayerData["character"]["skills"], (JArray)localPlayerData["character"]["skillVars"]);

        var turnTime = outerData["turnTime"].Value<int>();
        battleScreen.StartTurn(turnTime);
    }

    private void UpdateLocalPlayerSkills(JArray skills, JArray skillsVars)
    {
        List<SkillData> skillsList = new List<SkillData>();

        for (int i = 0; i < skills.Count; i++)
        {
            JObject skill = (JObject)skills[i];
            JObject vars = (JObject)skillsVars[i];
            skillsList.Add(Utils.ParseSkill(skill, vars, i));
        }

        battleScreen.UpdateLocalPlayerSkills(skillsList);
    }

    public void BattleEnded(SocketIOResponse resp)
    {
        JObject outerData = JObject.Parse(resp.GetValue<JObject>().ToString());

        GameManager.Instance.UpdateUserHistory(outerData["userWins"].Value<int>(), outerData["userLosses"].Value<int>());

        battleScreen.EndBattle(outerData["battleResult"].Value<string>());
    }

}
