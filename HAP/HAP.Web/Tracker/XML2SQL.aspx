﻿<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true" CodeBehind="XML2SQL.aspx.cs" Inherits="HAP.Web.Tracker.XML2SQL" %>
<asp:Content ContentPlaceHolderID="title" runat="server"><asp:HyperLink runat="server" NavigateUrl="~/tracker/weblogs.aspx"><hap:LocalResource runat="server" StringPath="tracker/logontracker" /></asp:HyperLink></asp:Content>
<asp:Content ContentPlaceHolderID="header" runat="server">
    <asp:HyperLink runat="server" NavigateUrl="~/tracker/live.aspx"><hap:LocalResource runat="server" StringPath="tracker/livelogons" /></asp:HyperLink>
    <asp:HyperLink runat="server" NavigateUrl="~/tracker/logs.aspx"><hap:LocalResource runat="server" StringPath="tracker/historiclogs" /></asp:HyperLink>
    <asp:HyperLink runat="server" NavigateUrl="~/tracker/weblogs.aspx"><hap:LocalResource runat="server" StringPath="tracker/weblogs" /></asp:HyperLink>
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="server">
    <header class="commonheader">
		<div>
			<a href="<%:ResolveClientUrl("~/tracker") %>"><hap:LocalResource StringPath="tracker/logontracker" runat="server" /></a>
		</div>
	</header>
    <div>
        <h1>XML2SQL Upgrade</h1>
        <p>Warning, this page will move the logon tracker events from the XML Store to a SQL Server you have Set in the Web.Config</p>
        <asp:Literal runat="server" ID="upgraded" />
        <asp:PlaceHolder runat="server" ID="cantupgrade" Visible="true">
            <p>You have not set the Tracker's Provider in the web.config to the SQL Provider you have defined.  It is still set to 'XML'.</p>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="canupgrade" Visible="false">
            <p>This process may take some time, click the button to begin</p>
            <input type="checkbox" id="iagree" onchange="document.getElementById('<%=upgrade.ClientID %>').style.display = this.checked ? '' : 'none';" /><label for="iagree">I Agreee</label>
            <br />
            <asp:Button runat="server" ID="upgrade" Text="Upgrade" 
                OnClientClick="this.value='Upgrading...';" 
                style="width: 200px; height: 70px; display: none;" onclick="upgrade_Click" />
        </asp:PlaceHolder>
    </div>
</asp:Content>
