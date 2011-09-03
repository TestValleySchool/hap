﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.DirectoryServices;
using System.Collections.Specialized;
using System.DirectoryServices.AccountManagement;

namespace HAP.AD
{
    /// <summary>
    /// Summary description for RoleProvider
    /// </summary>
    public sealed class RoleProvider : System.Web.Security.RoleProvider
    {
        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);

            ApplicationName = config["applicationName"];
        }

        System.Web.Security.WindowsTokenRoleProvider wtrp = new System.Web.Security.WindowsTokenRoleProvider();

        public override bool IsUserInRole(string username, string roleName)
        {
            return wtrp.IsUserInRole(HAP.Web.Configuration.hapConfig.Current.AD.UPN.Remove(HAP.Web.Configuration.hapConfig.Current.AD.UPN.IndexOf('.')) + '\\' + username, roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            PrincipalContext pcontext = new PrincipalContext(ContextType.Domain, HAP.Web.Configuration.hapConfig.Current.AD.UPN, HAP.Web.Configuration.hapConfig.Current.AD.User, HAP.Web.Configuration.hapConfig.Current.AD.Password);
            UserPrincipal userp = UserPrincipal.FindByIdentity(pcontext, username);
            List<string> roles = new List<string>();
            foreach (Principal p in userp.GetGroups()) roles.Add(p.SamAccountName);
            return roles.ToArray();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return wtrp.RoleExists(roleName);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return wtrp.GetUsersInRole(roleName);
        }

        public override string[] GetAllRoles()
        {
            return wtrp.GetAllRoles();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return wtrp.FindUsersInRole(roleName, usernameToMatch);
        }

        public override string ApplicationName { get; set; }
    }
}