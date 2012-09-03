﻿<%@ Page Title="Crickhowell High School - IT - Home Access Plus+" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HAP.Web.Default" %>
<%@ Register TagName="announcement" TagPrefix="hap" Src="~/Controls/Announcement.ascx" %>
<%@ Register TagName="version" TagPrefix="hap" Src="~/Controls/UpdateChecker.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" type="text/css" href="style/jquery.wysiwyg.css" />
    <script type="text/javascript" src="Scripts/jquery.mousewheel.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div id="hapHomeContainer">
        <div id="hapHomeSide" class="tile-color"></div>
        <div id="hapHome">
            <hap:announcement runat="server" />
            <hap:version runat="server" />
            <div id="title">
                <h1><a href="http://hap.codeplex.com" target="_blank"><hap:LocalResource StringPath="homeaccessplus" runat="server" /></a></h1>
                <h2>Access your School from Home</h2>
            </div>
            <div id="HomeButtonsHeader">
                <asp:Repeater ID="homepageheaders" runat="server">
                    <ItemTemplate><h1 id="header-<%#Eval("Name").ToString().Replace(" ", "").Replace("'", "").Replace("+", "").Replace(".", "").Replace(",", "").Replace("&", "").Replace("/", "").Replace("\\", "") %>"><a href="#" title="Show the <%#Eval("Name") %> Tab"><%#Eval("Name") %></a></h1></ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="HomeButtonsContainer">
                <div id="HomeButtonsOutter">
                    <div id="HomeButtons" class="tiles">
                        <asp:Repeater ID="homepagelinks" runat="server">
                            <ItemTemplate>
                                <div class="panel" id="panel-<%#Eval("Name").ToString().Replace(" ", "").Replace("'", "").Replace("+", "").Replace(".", "").Replace(",", "").Replace("&", "").Replace("/", "").Replace("\\", "") %>">
                                    <%#Eval("SubTitle").ToString() == "#me" ? "" : Eval("SubTitle").ToString() %>
                                    <div <%#Eval("SubTitle").ToString() == "#me" ? "class=\"me\" " : "" %>id="<%#((string)Eval("Name")).Replace(" ", "").Replace("'", "").Replace(",", "").Replace(".", "").Replace("*", "").Replace("&", "").Replace("/", "").Replace("\\", "") %>">
                                        <script type="text/javascript">
                                            $(document).ready(function () {
                                                hap.livetiles.Init([
                                                <%#gettiles((HAP.Web.Configuration.LinkGroup)Container.DataItem)%>
                                                ]);
                                            });
                                        </script>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <a id="rightscoll" href="#right"></a>
                <a id="leftscroll" href="#left"></a>
            </div>
            <script type="text/javascript">
                var scrollpos = 0;
                hap.load = hap.loadtypes.help;
                $(document).ready(function () {
                    $("#HomeButtons").mousewheel(function(event, delta) {
                        if (delta > 0) {
                            if (scrollpos < ($("#HomeButtons > div").length - 1)) {
                                scrollpos++;
                                $("#HomeButtonsOutter").animate({ scrollLeft: (scrollpos * ($("#HomeButtonsOutter").width() - 20) + (scrollpos * 20)) });
                                $("#HomeButtonsHeader h1").removeClass("active");
                                $("#HomeButtonsHeader h1")[scrollpos].className = "active";
                            }
                        }
                        else {
                            if (scrollpos > 0) {
                                scrollpos--;
                                $("#HomeButtonsOutter").animate({ scrollLeft: (scrollpos * ($("#HomeButtonsOutter").width() - 20) + (scrollpos * 20)) });
                                $("#HomeButtonsHeader h1").removeClass("active");
                                $("#HomeButtonsHeader h1")[scrollpos].className = "active";
                            }
                        }
                        return false;
                    });
                    $("#HomeButtons").css("width", (($("#HomeButtons > div").length * $("#HomeButtonsOutter").width()) + 200) + "px");
                    if ($("#HomeButtons > div").length == 1) $("#rightscoll, #leftscroll").hide();
                    $("#HomeButtons .panel").css("width", $("#HomeButtonsOutter").width() + "px");
                    $("#leftscroll").click(function () {
                        if (scrollpos > 0) {
                            scrollpos--;
                            $("#HomeButtonsOutter").animate({ scrollLeft: (scrollpos * ($("#HomeButtonsOutter").width() - 20) + (scrollpos * 20)) });
                            $("#HomeButtonsHeader h1").removeClass("active");
                            $("#HomeButtonsHeader h1")[scrollpos].className = "active";
                        }
                        return false;
                    });
                    $("#rightscoll").click(function () {
                        if (scrollpos < ($("#HomeButtons > div").length - 1)) {
                            scrollpos++;
                            $("#HomeButtonsOutter").animate({ scrollLeft: (scrollpos * ($("#HomeButtonsOutter").width() - 20) + (scrollpos * 20)) });
                            $("#HomeButtonsHeader h1").removeClass("active");
                            $("#HomeButtonsHeader h1")[scrollpos].className = "active";
                        }
                        return false;
                    });
                    $(window).resize(function() {
                        $("#HomeButtons").css("width", (($("#HomeButtons > div").length * $("#HomeButtonsOutter").width()) + 200) + "px");
                        $("#HomeButtons .panel").css("width", $("#HomeButtonsOutter").width() + "px");
                        $("#HomeButtonsOutter").animate({ scrollLeft: (scrollpos * ($("#HomeButtonsOutter").width() - 20) + (scrollpos * 20)) });
                        $("#rightscoll, #leftscroll").css("height", $("#HomeButtonsOutter").height() + "px").css("line-height", $("#HomeButtonsOutter").height() + "px");
                    });
                    $("#HomeButtonsHeader h1 a").click(function () {
                        scrollpos = $(this).parent().index();
                        $("#HomeButtonsOutter").animate({ scrollLeft: (scrollpos * ($("#HomeButtonsOutter").width() - 20) + (scrollpos * 20)) });
                        $("#HomeButtonsHeader h1").removeClass("active");
                        this.parentNode.className = "active";
                        return false;
                    });
                    $("#HomeButtonsOutter").animate({ scrollLeft: (scrollpos * ($("#HomeButtonsOutter").width() - 20) + (scrollpos * 20)) });
                    $("#HomeButtonsHeader h1:first").addClass("active");
                    $('input[type=submit]').button();
                    $("#rightscoll, #leftscroll").css("height", $("#HomeButtonsOutter").height() + "px").css("line-height", $("#HomeButtonsOutter").height() + "px");
                });
            </script>
        </div>
    </div>
</asp:Content>
