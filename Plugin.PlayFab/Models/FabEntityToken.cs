using System.Text.Json.Serialization;

namespace Plugin.PlayFab.Models;

public class FabEntityToken
{
    [JsonPropertyName("i")]
    public string CreatedAt { get; set; } = string.Empty;

    [JsonPropertyName("idp")]
    public string IdentityPlatform { get; set; } = string.Empty;

    [JsonPropertyName("e")]
    public string ExpAt { get; set; } = string.Empty;

    [JsonPropertyName("fi")]
    public string CreatedAt2 { get; set; } = string.Empty;

    // 11 Char long.
    [JsonPropertyName("tid")]
    public string UnkownId { get; set; } = string.Empty;

    [JsonPropertyName("idi")]
    public string IdentityId { get; set; } = string.Empty;

    // No idea if real name
    [JsonPropertyName("h")]
    public string Host { get; set; } = "internal";

    // No idea if real name
    [JsonPropertyName("ec")]
    public string EntityCredentials { get; set; } = string.Empty;

    // No idea if real name | AKA Title Acount Id
    [JsonPropertyName("ei")]
    public string EntityId { get; set; } = string.Empty;

    // No idea if real name
    [JsonPropertyName("et")]
    public string EntityType { get; set; } = "title_player_account";
}
