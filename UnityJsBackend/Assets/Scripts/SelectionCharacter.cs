using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class SelectionCharacter : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text characterDescription;

    public UnityAction<CharacterData, Sprite> OnSelected;
    private CharacterData characterData = null;

    public void Populate(CharacterData data)
    {
        characterData = data;
        characterName.text = data.Attributes.Name;
        characterDescription.text = data.Attributes.Description;
        transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(Utils.LoadTexture(data.Attributes.ImageUrl, characterImage));
    }

    public void Choose()
    {
        OnSelected?.Invoke(characterData, characterImage.sprite);
    }
}
