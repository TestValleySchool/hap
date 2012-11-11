﻿<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true" CodeBehind="logs.aspx.cs" Inherits="HAP.Web.Tracker.logs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link href="tracker.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]><script language="javascript" type="text/javascript" src="../scripts/excanvas.js"></script><![endif]-->
    <script type="text/javascript" src="../scripts/jquery.jqplot.min.js"></script>
    <script type="text/javascript" src="../Scripts/jqplot.dateAxisRenderer.min.js"></script>
    <script type="text/javascript" src="../Scripts/jqplot.highlighter.min.js"></script>
    <script type="text/javascript" src="../Scripts/jqplot.cursor.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../style/jquery.jqplot.css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="title" runat="server"><asp:HyperLink runat="server" NavigateUrl="~/tracker/logs.aspx"><hap:LocalResource runat="server" StringPath="tracker/historiclogs" /></asp:HyperLink></asp:Content>
<asp:Content ContentPlaceHolderID="header" runat="server">
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/tracker/live.aspx"><hap:LocalResource runat="server" StringPath="tracker/livelogons" /></asp:HyperLink>
    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/tracker/logs.aspx"><hap:LocalResource runat="server" StringPath="tracker/historiclogs" /></asp:HyperLink>
    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/tracker/weblogs.aspx"><hap:LocalResource runat="server" StringPath="tracker/weblogs" /></asp:HyperLink>
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="server">
    <header class="commonheader">
		<div>
			<a href="<%:ResolveClientUrl("~/tracker") %>"><hap:LocalResource StringPath="tracker/historiclogs" runat="server" /></a>
		</div>
	</header>
    <asp:Repeater ID="dates" runat="server">
        <HeaderTemplate><ul></HeaderTemplate>
        <ItemTemplate><li><a href="<%#Eval("Year")%>/<%#Eval("Month") %>/"><%#((DateTime)Container.DataItem).ToString("MMMM yyyy") %></a></li></ItemTemplate>
        <FooterTemplate></ul></FooterTemplate>
    </asp:Repeater>
    <div id="chartdiv" style="height:300px;width:99%; "></div>
    <button onclick="plot1.resetZoom(); return false;">Reset Zoom</button>
    <script type="text/javascript">
        var line1 = [<%=Data%>];
    </script>
    <script type="text/javascript">
        var plot1;
        $(document).ready(function(){
            plot1 = $.jqplot('chartdiv', [line1], {
                title:'Overview Tracker Results',
                axes:{yaxis: { min: 0 }, xaxis:{ renderer:$.jqplot.DateAxisRenderer}},
                series:[{lineWidth:2, markerOptions:{ style: 'filledCircle', lineWidth: 1, size: 4 }}],
                highlighter: { show: true }, 
                cursor: { show: true, zoom: true }
            });
        });
    </script>
</asp:Content>
