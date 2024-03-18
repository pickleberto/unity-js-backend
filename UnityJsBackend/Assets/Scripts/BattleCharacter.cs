using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class BattleCharacter : MonoBehaviour
{
    [SerializeField] private Image facePic;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;

    private readonly int MAX_HEALTH = 100;
    private readonly int MAX_MANA = 100;

    public void Populate(JObject data)
    {
        healthBar.fillAmount = (float)data["character"]["health"] / MAX_HEALTH;
        manaBar.fillAmount = (float)data["character"]["mana"] / MAX_MANA;

        StartCoroutine(Utils.LoadTexture(data["character"]["imageUrl"].ToString(), facePic));
    }
}
