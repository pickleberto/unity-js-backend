using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Text;

public class AuthScreen : MonoBehaviour
{
    [Header("Login")]
    [SerializeField] private TMP_InputField loginUsername;
    [SerializeField] private TMP_InputField loginPassword;
    [SerializeField] private Button loginButton;
    
    [Header("Register")]
    [SerializeField] private TMP_InputField registerUsername;
    [SerializeField] private TMP_InputField registerEmail;
    [SerializeField] private TMP_InputField registerPassword;
    [SerializeField] private Button registerButton;

    private readonly string REGISTER_ENDPOINT = "/api/auth/local/register";
    private readonly string LOGIN_ENDPOINT = "/api/auth/local";

    private void Start()
    {
        loginButton.onClick.AddListener(Login);
        registerButton.onClick.AddListener(Register);
    }

    public void Login()
    {
        StartCoroutine(SendLoginRequest());
    }

    private IEnumerator SendLoginRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("identifier", loginUsername.text);
        form.AddField("password", loginPassword.text);

        var request = UnityWebRequest.Post(SocketManager.ConnectionUrl + LOGIN_ENDPOINT, form);
        yield return request.SendWebRequest();

        if(request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Login failed");
            yield break;
        }

        var res = Encoding.UTF8.GetString(request.downloadHandler.data);
        var resObj = JObject.Parse(res);
        Debug.Log(resObj);

        GameManager.Instance.StartSession(resObj["jwt"].ToString());
    }

    public void Register()
    {
        var request = new UnityWebRequest(SocketManager.ConnectionUrl + REGISTER_ENDPOINT, "POST");
        
        string json = 
            "{"
            +"\"username\":\""+registerUsername.text+"\","
            + "\"email\":\"" + registerEmail.text + "\","
            + "\"password\":\"" + registerPassword.text + "\""
            + "}";
        Debug.Log(json);

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");
        request.SendWebRequest();

        //TODO: handle errors
    }
}
