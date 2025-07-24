using ModdableWebServer.Attributes;
using PlayFab.ClientModels;
using Plugin.PlayFab.Managers;
using Plugin.PlayFab.Models;

namespace Plugin.PlayFab;

internal partial class Client
{
    [HTTP("POST", "/Client/LoginWithSteam?{!args}")]
    public static bool LoginWithSteam(ServerSender server)
    {
        var steam = JsonSerializer.Deserialize<LoginWithSteamRequest>(server.Request.Body);
        if (server.ReturnIfNull(steam))
            return true;
        var ticket = AppTickets.GetTicket(Convert.FromHexString(steam.SteamTicket));
        var steam_id = ticket.SteamID.ToString();

        var user = DBFabUser.GetOne(x=>x.TitleId == steam.TitleId && x.PlatformId == steam_id && x.PlatformType == "Steam");
        if (user == null || user.PlayFabId == FabId.Empty)
        {
            // if no user found create one.
            DBFabUser.Create(user = new()
            { 
                PlayFabId = FabId.RandomId,
                GameId = FabId.RandomId,
                PlatformId = steam_id,
                PlatformType = "Steam",
                RandomId = FabId.RandomId,
                TitleAccountId = FabId.RandomId,
                TitleId = steam.TitleId
            });
        }
        LoginResult loginResult = new()
        {
            EntityToken = new()
            {
                Entity = new()
                {
                    Id = user.TitleAccountId,
                    Type = "title_player_account"
                },
                EntityToken = user.CreateSerializedEntityToken(),
                TokenExpiration = DateTime.Now.AddDays(1)
            },
            SessionTicket = user.GenerateSessionTicket(),
            SettingsForUser = new()
            {
                NeedsAttribution = false,
                GatherDeviceInfo = false,
                GatherFocusInfo = false
            },
            LastLoginTime = DateTime.Now,
            TreatmentAssignment = new()
            {
                Variables = [],
                Variants = [],
            },
            NewlyCreated = false,
            PlayFabId = user.PlayFabId,
            InfoResultPayload = new()
        };
        if (steam.InfoRequestParameters.GetUserAccountInfo)
        {
            loginResult.InfoResultPayload.AccountInfo = new()
            { 
                PlayFabId = user.PlayFabId,
                Created = user.CreatedAt,
                PrivateInfo = new(),
                TitleInfo = new()
                { 
                    isBanned = false,
                    DisplayName = user.DisplayName,
                    Created = user.CreatedAt,
                    FirstLogin = user.CreatedAt,
                    LastLogin = DateTime.UtcNow,
                    Origination = UserOrigination.Steam,
                    TitlePlayerAccount = new()
                    { 
                        Id = user.TitleAccountId,
                        Type = "title_player_account",
                    },
                },
                SteamInfo = new()
                { 
                    SteamActivationStatus = TitleActivationStatus.ActivatedSteam,
                    SteamCountry = "US",
                    SteamCurrency  = Currency.USD,
                    SteamId = steam_id,
                    SteamName = user.DisplayName
                },
                Username = user.DisplayName,
            };
        }
        if (steam.InfoRequestParameters.GetUserData)
        {
            loginResult.InfoResultPayload.UserData = user.CustomData;
            loginResult.InfoResultPayload.UserDataVersion = user.DataVersion;
        }

        if (steam.InfoRequestParameters.GetPlayerProfile)
        {
            loginResult.InfoResultPayload.PlayerProfile = new()
            { 
                PublisherId = user.GameId,
                TitleId = user.TitleId,
                PlayerId = user.PlayFabId,
            };
        }

        if (steam.InfoRequestParameters.GetTitleData)
        {
            var title = DBFabTitle.GetOne(x=> x.TitleId == steam.TitleId);
            if (title == null)
            {
                title = new()
                { 
                    TitleId = steam.TitleId,
                    TitleData = [],
                };
                DBFabTitle.Create(title);
            }
            loginResult.InfoResultPayload.TitleData = title.TitleData;
        }

        if (steam.InfoRequestParameters.GetUserVirtualCurrency)
        {
            loginResult.InfoResultPayload.UserVirtualCurrency = user.VirtualCurrency;
        }

        return server.SendSuccess(loginResult);
    }
}
