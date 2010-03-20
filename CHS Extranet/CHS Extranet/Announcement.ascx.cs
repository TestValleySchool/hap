﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices.AccountManagement;
using CHS_Extranet.Configuration;
using System.Configuration;
using System.Xml;

namespace CHS_Extranet
{
    public partial class Announcement : System.Web.UI.UserControl
    {

        private String _DomainDN;
        private String _ActiveDirectoryConnectionString;
        private PrincipalContext pcontext;
        private UserPrincipal up;
        private extranetConfig config;

        public string Username
        {
            get
            {
                if (Page.User.Identity.Name.Contains('\\'))
                    return Page.User.Identity.Name.Remove(0, Page.User.Identity.Name.IndexOf('\\') + 1);
                else return Page.User.Identity.Name;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            config = ConfigurationManager.GetSection("extranetConfig") as extranetConfig;
            ConnectionStringSettings connObj = ConfigurationManager.ConnectionStrings[config.ADSettings.ADConnectionString];
            if (connObj != null) _ActiveDirectoryConnectionString = connObj.ConnectionString;
            if (string.IsNullOrEmpty(_ActiveDirectoryConnectionString))
                throw new Exception("The connection name 'activeDirectoryConnectionString' was not found in the applications configuration or the connection string is empty.");
            if (_ActiveDirectoryConnectionString.StartsWith("LDAP://"))
                _DomainDN = _ActiveDirectoryConnectionString.Remove(0, _ActiveDirectoryConnectionString.IndexOf("DC="));
            else throw new Exception("The connection string specified in 'activeDirectoryConnectionString' does not appear to be a valid LDAP connection string.");
            pcontext = new PrincipalContext(ContextType.Domain, null, _DomainDN, config.ADSettings.ADUsername, config.ADSettings.ADPassword);
            up = UserPrincipal.FindByIdentity(pcontext, IdentityType.SamAccountName, Username);
            GroupPrincipal da = GroupPrincipal.FindByIdentity(pcontext, "Domain Admins");
            EditAnnouncement.Visible = up.IsMemberOf(da);
            if (!Page.IsPostBack)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("~/App_Data/Announcement.xml"));
                XmlNode node = doc.SelectSingleNode("/announcement");
                ShowAnnouncement.Checked = bool.Parse(node.Attributes[0].Value);
                Editor1.Content = node.InnerXml.Replace("<![CDATA[ ", "").Replace(" ]]>", "");
                Editor1.DataBind();
                if (ShowAnnouncement.Checked)
                    AnnouncementText.Text = string.Format("<h1 class=\"Announcement\"><b>Announcement</b><br />{0}</h1>", Editor1.Content);
            }
        }

        protected void saveann_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/App_Data/Announcement.xml"));
            XmlNode node = doc.SelectSingleNode("/announcement");
            node.Attributes[0].Value = ShowAnnouncement.Checked.ToString();
            node.InnerXml = string.Format("<![CDATA[ {0} ]]>", Editor1.Content);
            doc.Save(Server.MapPath("~/App_Data/Announcement.xml"));
            if (ShowAnnouncement.Checked)
                AnnouncementText.Text = string.Format("<h1 class=\"Announcement\"><b>Announcement</b><br />{0}</h1>", Editor1.Content);
            else AnnouncementText.Text = "";
        }
    }
}