﻿using Plugin.PlayFab.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Plugin.PlayFab.Extensions;

internal static class FabUserExt
{
    public static (FabId PlayFabId, FabId GameId, FabId TitleAccountId, string TitleId) GetSessionInfo(string sessionTicket)
    {
        FabId fabId = FabId.Empty;
        FabId gameId = FabId.Empty;
        FabId titleAccountId = FabId.Empty;
        string titleId = string.Empty;
        string randomid = string.Empty;
        string somehash = string.Empty;
        InterpolatedParsing.InterpolatedParser.Parse(sessionTicket, $"{fabId}-{gameId}-{titleAccountId}-{titleId}-{randomid}-{somehash}");
        return (fabId, gameId, titleAccountId, titleId);
    }

    public static string GenerateSessionTicket(this FabUser user)
    {
        return $"{user.PlayFabId}-{user.GameId}-{user.TitleAccountId}-{user.TitleId}-{user.RandomId}-{Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))}";
    }

    public static string CreateEntityCredential(this FabUser user)
    {
        return $"{user.GameId}/{user.TitleId}/{user.PlayFabId}/{user.TitleId}/{user.TitleAccountId}/";
    }

    public static string CreateEntityToken(this FabUser user)
    {
        return $"4|{RandomNumberGenerator.GetString("0123456789qwertzuiopasdfghjklyxcvbnm", 44)}|{JsonSerializer.Serialize(user.CreateFabEntityToken())}";
    }

    public static string CreateSerializedEntityToken(this FabUser user)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(user.CreateEntityToken()));
    }

    public static FabEntityToken CreateFabEntityToken(this FabUser user)
    {
        return new()
        { 
            CreatedAt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            IdentityPlatform = user.PlatformType,
            IdentityId = user.PlatformId,
            CreatedAt2 = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            EntityType = "title_player_account",
            EntityCredentials = "title_player_account!" + user.CreateEntityCredential(),
            EntityId = user.TitleAccountId,
            ExpAt = DateTime.Now.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ssZ"),
            Host = "internal",
            UnkownId = RandomNumberGenerator.GetString("0123456789qwertzuiopasdfghjklyxcvbnm", 11)
        };
    }
}
