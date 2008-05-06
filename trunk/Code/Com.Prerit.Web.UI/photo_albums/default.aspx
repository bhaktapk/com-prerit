<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="photo_albums_default" %>
<%@ Import namespace="Com.Prerit.Web"%>
<%@ Import Namespace="System.Collections.Generic" %>

<asp:Content ID="mainbar" ContentPlaceHolderID="mainbarPlaceHolder" runat="server">
    <asp:Repeater ID="albumYearRepeater" runat="server">
        <ItemTemplate>
            <h1><span>
                <%# ((List<Album>) Container.DataItem)[0].AlbumYear %>
                Albums</span></h1>
            <asp:Repeater ID="albumRepeater" DataSource="<%# Container.DataItem %>" runat="server">
                <HeaderTemplate>
                    <div class="albums">
                </HeaderTemplate>
                <ItemTemplate>
                    <p>
                        <a id="albumLink" href="<%# ((Album) Container.DataItem).VirtualPath %>" title="<%# ((Album) Container.DataItem).AlbumName %>"
                        runat="server"><img id="coverImage" alt="<%# ((Album) Container.DataItem).AlbumName %>" height="<%# ((Album) Container.DataItem).CoverPhoto.Height %>"
                        width="<%# ((Album) Container.DataItem).CoverPhoto.Width %>" src="<%# ((Album) Container.DataItem).CoverPhoto.VirtualPath %>"
                        runat="server" />
                        <br />
                        <span><%# ((Album) Container.DataItem).AlbumName %></span></a>
                    </p>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
