using PlayFab.Internal;
using PlayFab.MultiplayerModels;

namespace Plugin.PlayFab;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
/// <summary>
/// Requests a party session from a particular build in any of the given preferred regions.
/// </summary>
internal class RequestPartyRequest : PlayFabRequestCommon
{
    /// <summary>
    /// A guid string party ID created track the party session over its life.
    /// </summary>
    public string PartyId { get; set; }
    /// <summary>
    /// The preferred regions to request a party session from. The party service will iterate through the regions in the
    /// specified order and allocate a party session from the first one that is available.
    /// </summary>
    public List<AzureRegion> PreferredRegions { get; set; }
    /// <summary>
    /// Data encoded as a string that is passed to the party when requested. This can be used to to communicate information such
    /// as party type through the request flow.
    /// </summary>
    public string SessionCookie { get; set; }
    /// <summary>
    /// The client version for the party being requested.
    /// </summary>
    public string Version { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.