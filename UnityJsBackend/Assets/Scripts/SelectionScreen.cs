using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    [Header("User info")]
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private TMP_Text xpText;
    [SerializeField] private TMP_Text winLossText;

    [Header("Roster")]
    [SerializeField] private Transform characterRoster;
    [SerializeField] private GameObject characterSelectionPrefab;

    [Header("Selected character")]
    [SerializeField] private TMP_Text nameIdText;
    [SerializeField] private Image selectedPic;

    [Header("Search")]
    [SerializeField] private Button searchBattleBtn;

    private readonly string CHARACTERS_ENDPOINT = "/api/characters";

    private CharacterData[] characters;
    private int selectedCharID = 0;

    private void Start()
    {
        searchBattleBtn.onClick.AddListener(SearchBattle);
    }

    private void OnEnable()
    {
        SetUserInfoValues();
        StartCoroutine(PopulateCharacters());
    }

    private IEnumerator PopulateCharacters()
    {
        var request = UnityWebRequest.Get(SocketManager.ConnectionUrl + CHARACTERS_ENDPOINT);
        yield return request.SendWebRequest();
        
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Failed to fetch characters: " + request.error);
            yield break;
        }

        var response = Encoding.UTF8.GetString(request.downloadHandler.data);
        var responseObj = JObject.Parse(response);

        characters = responseObj["data"].ToObject<CharacterData[]>();

        foreach(var character in characters)
        {
            var obj = Instantiate(characterSelectionPrefab);
            obj.transform.SetParent(characterRoster);
            obj.GetComponent<SelectionCharacter>().Populate(character);
            obj.GetComponent<SelectionCharacter>().OnSelected += SetSelectedCharacter;
        }
    }

    private void SetUserInfoValues()
    {
        usernameText.text = GameManager.Instance.User.UserName;
        xpText.text = GameManager.Instance.User.Experience + " XP";
        winLossText.text = GameManager.Instance.User.Wins + ":" + GameManager.Instance.User.Losses;
    }

    private void SetSelectedCharacter(CharacterData selected, Sprite img)
    {
        if (selected == null) return;

        selectedCharID = selected.Id;
        nameIdText.text = selected.Id + ": " + selected.Attributes.Name;
        selectedPic.sprite = img;
    }


    private void SearchBattle()
    {
        SocketManager.Instance.SearchBattle(selectedCharID);
    }

}
