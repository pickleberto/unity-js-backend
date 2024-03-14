using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Utils 
{
    public static IEnumerator LoadTexture(string imgUrl, Image imgComponent)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imgUrl);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Failed to request image: " + request.error);
            yield break;
        }

        Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        if (texture != null)
        {
            imgComponent.sprite = Sprite.Create(texture, new Rect(0,0,texture.width, texture.height), new Vector2(.5f,.5f));
        }
        else
        {
            Debug.Log("Could not load texture: " + imgUrl);
        }
    }
}
