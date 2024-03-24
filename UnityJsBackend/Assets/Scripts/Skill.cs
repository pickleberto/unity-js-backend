using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill : MonoBehaviour
{
    [SerializeField] private Image skillImage;
    [SerializeField] private TMP_Text skillName;
    [SerializeField] private TMP_Text skillDescription;
    [SerializeField] private TMP_Text manaCostText;
    [SerializeField] private TMP_Text cooldownText;
    [SerializeField] private Button button;

    private int slotPosition = -1;

    private void Start()
    {
        button.onClick.AddListener(CastSkill);
    }

    public void Populate(SkillData data)
    {
        slotPosition = data.SkillSlot;

        skillName.text = data.Name;
        skillDescription.text = data.Description;
        manaCostText.text = data.ManaCost.ToString();
        cooldownText.text = data.CurrentCooldown.ToString();

        int skillCooldown = data.Cooldown;
        int currentCooldown = data.CurrentCooldown;

        GetComponent<Button>().interactable = (skillCooldown == currentCooldown);
        
        StartCoroutine(Utils.LoadTexture(data.ImageUrl, skillImage));
    }

    private void CastSkill()
    {
        SocketManager.Instance.SendTurn(slotPosition);
    }
}
