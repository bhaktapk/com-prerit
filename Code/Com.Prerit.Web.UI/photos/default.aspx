<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="photos_default" %>

<asp:Content ID="sidebar" ContentPlaceHolderID="sidebarPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="mainbar" ContentPlaceHolderID="mainbarPlaceHolder" runat="server">
    <h1><span>Choose An Album</span></h1>
    <ul>
        <li><a href="?albumid=0">Pithi Photos</a></li>
        <li><a href="?albumid=1">Wedding Photos</a></li>
        <li><a href="?albumid=2">Honeymoon Photos</a></li>
        <li><a href="?albumid=3">Christmas Photos</a></li>
    </ul>
    <asp:MultiView ID="photoAlbumViewMultiView" ActiveViewIndex="0" runat="server">
        <asp:View ID="pithiAlbum" runat="server">
            <h1><span>Pithi Photos (2006-12-14 to 2006-12-15)</span></h1>
        </asp:View>
        <asp:View ID="weddingAlbum" runat="server">
            <h1><span>Wedding Photos (2006-12-16)</span></h1>
        </asp:View>
        <asp:View ID="honeymoonAlbum" runat="server">
            <h1><span>Honeymoon Photos (2006-12-17 to 2006-12-24)</span></h1>
        </asp:View>
        <asp:View ID="christmasAlbum" runat="server">
            <h1><span>Christmas Photos (2006-12-25 to 2006-12-27)</span></h1>
        </asp:View>
    </asp:MultiView>
    <div class="photos">
        <asp:Repeater ID="photoRepeater" runat="server">
            <ItemTemplate>
                <p>
                    <a href="<%# ((string) Container.DataItem) + ".jpg" %>" onclick="window.open('<%# ((string) Container.DataItem) + ".jpg" %>'); return false;">
                    <img alt="" src="<%# ((string) Container.DataItem) + "_thumb.jpg" %>" /> </a>
                </p>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
