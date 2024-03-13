using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json.Linq;

public class SelectionScreen : MonoBehaviour
{
    [SerializeField] private Transform characterRoster;
    [SerializeField] private GameObject prefab;

    private readonly string CHARACTERS_ENDPOINT = "/api/characters";

    private void Start()
    {
        StartCoroutine(PopulateCharacters());
    }

    private IEnumerator PopulateCharacters()
    {
        var request = UnityWebRequest.Get(SocketManager.ConnectionUrl + CHARACTERS_ENDPOINT);
        yield return request.SendWebRequest();
        
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Failed to fetch characters");
            yield break;
        }

        var response = Encoding.UTF8.GetString(request.downloadHandler.data);
        var charactersList = JObject.Parse(response);
    }
}
