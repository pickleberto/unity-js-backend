using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;

public class Skill : MonoBehaviour
{
    [SerializeField] private Image skillImage;
    [SerializeField] private TMP_Text skillName;
    [SerializeField] private TMP_Text skillDescription;
    [SerializeField] private TMP_Text manaCostText;
    [SerializeField] private TMP_Text cooldownText;

    private int slotPosition = -1;

    private void Start()
    {
        GetComponent<Button>()?.onClick.AddListener(CastSkill);
    }

    public void Populate(JObject data, int index)
    {
        slotPosition = index;

        skillName.text = data["name"].ToString();
        skillDescription.text = data["description"].ToString();
        manaCostText.text = data["manaCost"].ToString();
        cooldownText.text = data["cooldown"].ToString();

        StartCoroutine(Utils.LoadTexture(data["imageUrl"].ToString(), skillImage));
    }

    private void CastSkill()
    {
        SocketManager.Instance.SendTurn(slotPosition);
    }
}
