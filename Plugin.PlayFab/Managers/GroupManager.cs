using Plugin.PlayFab.Models;
using ServerShared.Database;

namespace Plugin.PlayFab.Managers;

internal static class GroupManager
{
    public static bool AddMembers(FabId groupId, List<FabId> enityIds, string RoleId)
    {
        var group = DBFabGroup.GetOne(x => x.Id == groupId);
        if (group == null)
            return false;
        if (!group.Roles.ContainsKey(RoleId))
           return false;
        foreach (var item in enityIds)
            group.MembersAndRoles.Add(item, RoleId);
        DBFabGroup.Update(group);
        return true;
    }

    public static bool IsMember(FabId groupId, FabId enityId, string RoleId)
    {
        var group = DBFabGroup.GetOne(x => x.Id == groupId);
        if (group == null)
            return false;
        bool contains = group.MembersAndRoles.ContainsKey(enityId);
        if (contains && !string.IsNullOrEmpty(RoleId))
            contains = group.MembersAndRoles.TryGetValue(RoleId, out var role) && role == RoleId;
        return contains;
    }

    public static bool ChangeMemberRole(FabId groupId, FabId enityId, string newRoleId)
    {
        var group = DBFabGroup.GetOne(x => x.Id == groupId);
        if (group == null)
            return false;
        if (group.Roles.ContainsKey(newRoleId))
            return false;
        group.MembersAndRoles[enityId] = newRoleId;
        DBFabGroup.Update(group);
        return true;
    }

    public static bool BlockEntity(FabId groupId, FabId enityId)
    {
        var group = DBFabGroup.GetOne(x => x.Id == groupId);
        if (group == null)
            return false;
        group.Blocked.Add(enityId);
        DBFabGroup.Update(group);
        return true;
    }

    public static bool TryGetGroup(FabId groupId, out FabGroup? group)
    {
        group = DBFabGroup.GetOne(x => x.Id == groupId);
        return group != null;
    }

    public static FabGroup? CreateGroup(string name)
    {
        if (DBFabGroup.GetOne(x=>x.Name == name) != null)
            return null;
        FabGroup fabGroup = new()
        { 
            Id = FabId.RandomId,
            Name = name,
            Roles = FabGroup.MainRoles,
        };
        DBFabGroup.Create(fabGroup);
        return fabGroup;
    }

    public static int CreateRole(FabId groupId, string roleId, string RoleName)
    {
        var group = DBFabGroup.GetOne(x => x.Id == groupId);
        if (group == null)
            return 1;
        if (group.Roles.ContainsKey(roleId))
            return 2;
        if (group.Roles.ContainsValue(RoleName))
            return 1;
        if (RoleName.Length is <1 or >100)
            return 1;
        // TODO more checks.
        group.Roles.Add(roleId, RoleName);
        DBFabGroup.Update(group);
        return 0;
    }

    public static void DeleteGroup(FabId groupId)
    {
        DBFabGroup.Delete(x=>x.Id == groupId);
    }

    public static int DeleteRole(FabId groupId, string roleId)
    {
        var group = DBFabGroup.GetOne(x => x.Id == groupId);
        if (group == null)
            return 1;
        if (!group.Roles.ContainsKey(roleId))
            return 1;
        group.Roles.Remove(roleId);
        for (int i = 0; i < group.MembersAndRoles.Count; i++)
        {
            var member = group.MembersAndRoles.ElementAt(i);
            group.MembersAndRoles[member.Key] = group.MemberId;
        }
        DBFabGroup.Update(group);
        return 0;
    }

    public static int ApplyToGroup(FabId groupId, FabId userId, bool acceptInvite, out DateTime time)
    {
        time = DateTime.UtcNow;
        var group = DBFabGroup.GetOne(x => x.Id == groupId);
        if (group == null)
            return 1;
        if (group.Blocked.Contains(userId))
            return 1;
        if (group.MembersAndRoles.ContainsKey(userId))
            return 2;
        if (acceptInvite && TryGetValidInvitation(groupId, userId, out var invitation) && invitation != null)
        {
            if (group.Roles.ContainsKey(invitation.RoleId))
                return 3;
            group.MembersAndRoles.Add(userId, invitation.RoleId);
            group.Invitations.Remove(invitation);
            DBFabGroup.Update(group);
            return 4;
        }
        time = DateTime.UtcNow.AddDays(7);
        group.Applications.Add(userId, time);
        DBFabGroup.Update(group);
        return 0;
    }

    public static bool TryGetValidInvitation(FabId groupId, FabId userId, out FabGroup.Invitiation? invitation)
    {
        invitation = null;
        var group = DBFabGroup.GetOne(x => x.Id == groupId);
        if (group == null)
            return false;
        invitation = group.Invitations.FirstOrDefault(x => x.Invited == userId);
        if (invitation == null)
            return false;
        return invitation.Exp < DateTime.UtcNow;
    }

    public static bool TryGetInvitation(FabId groupId, FabId userId, out FabGroup.Invitiation? invitation)
    {
        invitation = null;
        var group = DBFabGroup.GetOne(x => x.Id == groupId);
        if (group == null)
            return false;
        invitation = group.Invitations.FirstOrDefault(x => x.Invited == userId);
        return invitation != null;
    }
}
