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

    public void Populate(SkillData data)
    {

    }
}
