using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class Utils 
{
    private static Dictionary<string, Sprite> spriteChache = new Dictionary<string, Sprite>();
    public static IEnumerator LoadTexture(string imgUrl, Image imgComponent)
    {
        if(spriteChache.TryGetValue(imgUrl, out Sprite cachedSprite))
        {
            imgComponent.sprite = cachedSprite;
            yield break;
        }

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
            Sprite downloadedSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
            imgComponent.sprite = downloadedSprite;
            spriteChache.Add(imgUrl, downloadedSprite);
        }
        else
        {
            Debug.Log("Could not load texture: " + imgUrl);
        }
    }

    public static SkillData ParseSkill(JObject data, JObject vars, int index)
    {
        SkillData skill = new SkillData();

        skill.SkillSlot = index;
        skill.Name = data["name"].ToString();
        skill.Description = data["description"].ToString();
        skill.ManaCost = (int)data["manaCost"];
        skill.Cooldown = (int)data["cooldown"];
        skill.CurrentCooldown = (int)vars["currentCooldown"];
        skill.ImageUrl = data["imageUrl"].ToString();

        return skill;
    }

    public static BattleCharacterData ParseCharacter(JObject data)
    {
        BattleCharacterData characterData = new BattleCharacterData();
        
        characterData.Health = (int)data["character"]["health"];
        characterData.Mana = (int)data["character"]["mana"];
        characterData.ImageUrl = data["character"]["imageUrl"].ToString();
        characterData.CharacterName = data["character"]["name"].ToString();
        characterData.PlayerName = data["username"].ToString();
        
        return characterData;
    }
}
