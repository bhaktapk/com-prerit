<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="photo_albums_default" %>

<%@ Import Namespace="Com.Prerit.Web" %>
<%@ Import Namespace="System.Collections.Generic" %>
<asp:Content ID="mainbar" ContentPlaceHolderID="mainbarPlaceHolder" runat="server">
    <asp:MultiView ID="photoAlbumViews" ActiveViewIndex="0" runat="server">
        <asp:View ID="albumView" runat="server">
            <asp:Repeater ID="albumYearRepeater" runat="server">
                <ItemTemplate>
                    <h1><span>
                        <%# ((KeyValuePair<int, Album[]>) Container.DataItem).Key %>
                        Albums</span></h1>
                    <asp:Repeater ID="albumRepeater" DataSource="<%# ((KeyValuePair<int, Album[]>) Container.DataItem).Value %>" runat="server">
                        <HeaderTemplate>
                            <div class="albums">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <p>
                                <a id="albumLink" href="<%# ((Album) Container.DataItem).VirtualPath %>" title="<%# ((Album) Container.DataItem).AlbumName %>"
                                runat="server"><img id="coverImage" alt="<%# ((Album) Container.DataItem).AlbumName %>" height="<%# ((Album) Container.DataItem).AlbumCover.Height %>"
                                width="<%# ((Album) Container.DataItem).AlbumCover.Width %>" src="<%# ((Album) Container.DataItem).AlbumCover.VirtualPath %>"
                                runat="server" />
                                <br />
                                <span>
                                    <%# ((Album) Container.DataItem).AlbumName %></span></a>
                            </p>
                        </ItemTemplate>
                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
        </asp:View>
        <asp:View ID="photoView" runat="server">
            <asp:Repeater ID="photoRepeater" runat="server">
                <HeaderTemplate>
                    <h1><span>Photo Album of
                        <%# GetAlbumNameAsTitle() %></span></h1>
                    <div class="photos">
                </HeaderTemplate>
                <ItemTemplate>
                    <p>
                        <a href="<%# ((Photo) Container.DataItem).VirtualPath %>" rel="<%# GetLightboxIdentifier() %>" title="<%# ((Photo) Container.DataItem).Caption %>"
                        runat="server"><img alt="<%# ((Photo) Container.DataItem).Caption %>" height="<%# ((Photo) Container.DataItem).Height %>"
                        width="<%# ((Photo) Container.DataItem).Width %>" src="<%# ((Photo) Container.DataItem).VirtualPath %>" runat="server" /></a>
                    </p>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </asp:View>
    </asp:MultiView>
</asp:Content>
