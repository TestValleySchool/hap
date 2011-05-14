﻿using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.DirectoryServices;
using System.Web.Security;
using HAP.Web.Configuration;

namespace HAP.AD
{
	public sealed class ActiveDirectoryRoleProvider : RoleProvider
	{
		public string ConnUsername;
		public string ConnPassword;
		public string ConnStringName;
		public string DomainName;
		public DirectoryEntry DirectoryRoot;
		public bool Cache;

		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
				throw new ArgumentNullException(ActiveDirectoryHelper.ERROR_CONFIG_NOT_FOUND);

			if (string.IsNullOrEmpty(name))
				name = "ActiveDirectoryRoleProvider";

			if (string.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", "Active Directory Role Provider");
			}
			base.Initialize(name, config);

			hapConfig c = hapConfig.Current;
			ConnUsername = c.ADSettings.ADUsername;
			ConnPassword = c.ADSettings.ADPassword;
			ConnStringName = ConfigurationManager.ConnectionStrings[c.ADSettings.ADConnectionString].ConnectionString;

			DirectoryRoot = new DirectoryEntry(ConnStringName, ConnUsername, ConnPassword, AuthenticationTypes.ServerBind);
			DomainName = ActiveDirectoryHelper.GetDomainName(ConnStringName);

			ApplicationName = config["applicationName"];

			Cache = bool.Parse(config["cache"] ?? "false");
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			return ActiveDirectoryHelper.IsUserInRole(DirectoryRoot, DomainName, username, roleName, Cache);
		}

		public override string[] GetRolesForUser(string username)
		{
			return ActiveDirectoryHelper.GetRolesForUser(DirectoryRoot, username);
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
			return ActiveDirectoryHelper.RoleExists(DirectoryRoot, roleName);
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
			return ActiveDirectoryHelper.GetUsersInRole(DirectoryRoot, DomainName, roleName, Cache);
		}

		public override string[] GetAllRoles()
		{
			return ActiveDirectoryHelper.GetAllRoles(DirectoryRoot);
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			return ActiveDirectoryHelper.FindUsersInRole(DirectoryRoot, DomainName, roleName, usernameToMatch);
		}

		public override string ApplicationName { get; set; }
	}
}
