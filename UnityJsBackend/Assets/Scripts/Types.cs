using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class CharacterData
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("attributes")]
    public CharacterAttributes Attributes { get; set; }
}

public class CharacterAttributes
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("imageUrl")]
    public string ImageUrl { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}

public class CharacterResponse
{
    [JsonProperty("data")]
    public List<CharacterData> Data { get; set; }

    [JsonProperty("meta")]
    public PaginationMeta Meta { get; set; }
}

public class PaginationMeta
{
    [JsonProperty("page")]
    public int Page { get; set; }

    [JsonProperty("pageSize")]
    public int PageSize { get; set; }

    [JsonProperty("pageCount")]
    public int PageCount { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }
}

public class UserInfo
{
    [JsonProperty("username")]
    public string UserName { get; set; }
    [JsonProperty("experience")]
    public string Experience { get; set; }
    [JsonProperty("wins")]
    public int Wins { get; set; }
    [JsonProperty("losses")]
    public int Losses { get; set; }
}

public class SkillData
{

}