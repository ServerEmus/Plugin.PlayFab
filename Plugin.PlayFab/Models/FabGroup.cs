namespace Plugin.PlayFab.Models;

public class FabGroup
{
    public readonly static Dictionary<string, string> MainRoles = new()
    {
        ["admins"] = "Administrators",
        ["members"] = "Members",
    };

    [LiteDB.BsonId]
    public int DataBaseId { get; set; }

    public FabId Id { get; set; }

    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Key: ID, Value: Name
    /// </summary>
    public Dictionary<string, string> Roles { get; set; } = [];

    public Dictionary<FabId, string> MembersAndRoles { get; set; } = [];
    public Dictionary<FabId, DateTime> Applications { get; set; } = [];
    public List<Invitiation> Invitations { get; set; } = [];
    public List<FabId> Blocked { get; set; } = [];
    public string AdminId { get; set; } = "admins";
    public string MemberId { get; set; } = "members";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public class Invitiation
    {
        public FabId Inviter { get; set; }
        public FabId Invited { get; set; }
        public string RoleId { get; set; }
        public DateTime Exp { get; set; }
    }
}
