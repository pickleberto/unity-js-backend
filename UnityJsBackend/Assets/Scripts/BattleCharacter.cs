using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleCharacter : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private TMP_Text username;

    [Header("Character")]
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private Image facePic;
    
    [Header("Health")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text healthText;

    [Header("Mana")]
    [SerializeField] private Image manaBar;
    [SerializeField] private TMP_Text manaText;

    private readonly int MAX_HEALTH = 100;
    private readonly int MAX_MANA = 100;

    public void Populate(BattleCharacterData data) 
    {
        username.text = data.PlayerName;
        characterName.text = data.CharacterName;

        healthBar.fillAmount = (float) data.Health / MAX_HEALTH;
        manaBar.fillAmount = (float) data.Mana / MAX_MANA;

        healthText.text = data.Health.ToString();
        manaText.text = data.Mana.ToString();

        StartCoroutine(Utils.LoadTexture(data.ImageUrl, facePic));
    }
}
