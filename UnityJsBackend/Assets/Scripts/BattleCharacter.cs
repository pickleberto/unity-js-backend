using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using TMPro;

public class BattleCharacter : MonoBehaviour
{
    [SerializeField] private Image facePic;
    
    [Header("Health")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text healthText;
    [Header("Mana")]
    [SerializeField] private Image manaBar;
    [SerializeField] private TMP_Text manaText;

    private readonly int MAX_HEALTH = 100;
    private readonly int MAX_MANA = 100;

    public void Populate(JObject data)
    {
        healthBar.fillAmount = (float)data["character"]["health"] / MAX_HEALTH;
        manaBar.fillAmount = (float)data["character"]["mana"] / MAX_MANA;

        healthText.text = data["character"]["health"].ToString();
        manaText.text = data["character"]["mana"].ToString();

        StartCoroutine(Utils.LoadTexture(data["character"]["imageUrl"].ToString(), facePic));
    }
}
