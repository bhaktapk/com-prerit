<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="photo_albums_default" %>

<%@ Import Namespace="Com.Prerit.Web" %>
<%@ Import Namespace="System.Collections.Generic" %>
<asp:Content ID="sidebar" ContentPlaceHolderID="sidebarPlaceHolder" runat="server">
    <h2><span>Photo Viewing Tips</span></h2>
    <ul>
        <li>Roll over the image</li>
        <li>Press the right arrow key</li>
        <li>Press Esc to close</li>
    </ul>
</asp:Content>
<asp:Content ID="mainbar" ContentPlaceHolderID="mainbarPlaceHolder" runat="server">
    <asp:MultiView ID="photoAlbumViews" ActiveViewIndex="0" runat="server">
        <asp:View ID="albumView" runat="server">
            <asp:Repeater ID="albumYearRepeater" OnItemDataBound="AlbumYearRepeater_ItemDataBound" runat="server">
                <ItemTemplate>
                    <asp:PlaceHolder ID="albumYearPlaceHolder" runat="server">
                        <h1><span>
                            <%# ((AlbumYear) Container.DataItem).Year %>
                            Albums</span></h1>
                        <asp:Repeater ID="albumRepeater" DataSource="<%# ((AlbumYear) Container.DataItem).Albums %>" runat="server">
                            <HeaderTemplate>
                                <div class="albums">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <a id="albumLink" href="<%# ((Album) Container.DataItem).VirtualPath %>" title="<%# ConvertNameToTitle(((Album) Container.DataItem).AlbumName) %>"
                                    runat="server"><img id="coverImage" alt="<%# ((Album) Container.DataItem).AlbumCover.Caption %>" height="<%# ((Album) Container.DataItem).AlbumCover.Height %>"
                                    width="<%# ((Album) Container.DataItem).AlbumCover.Width %>" src="<%# ((Album) Container.DataItem).AlbumCover.VirtualPath %>"
                                    runat="server" />
                                    <br />
                                    <span>
                                        <%# ConvertNameToTitle(((Album) Container.DataItem).AlbumName) %>
                                        (<%# ((Album) Container.DataItem).Photos.Length %>)</span></a>
                                </p>
                            </ItemTemplate>
                            <FooterTemplate>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>
                    </asp:PlaceHolder>
                </ItemTemplate>
            </asp:Repeater>
        </asp:View>
        <asp:View ID="photoAlbumView" runat="server">
            <h1><span>Photo Album of
                <%= ConvertNameToTitle(AlbumNameQueryStringValue) %></span></h1>
            <asp:MultiView ID="photoViews" ActiveViewIndex="0" runat="server">
                <asp:View ID="noPhotosView" runat="server">
                    <p>
                        There are no photos for this album.
                    </p>
                </asp:View>
                <asp:View ID="somePhotosView" runat="server">
                    <asp:Repeater ID="photoRepeater" runat="server">
                        <HeaderTemplate>
                            <div class="photos">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <p>
                                <a href="<%# ((Photo) Container.DataItem).ResizedImage.VirtualPath %>" rel="lightbox[<%# GetLightboxImageSetIdentifier() %>]"
                                title="Download" runat="server"><img alt="<%# ((Photo) Container.DataItem).Thumbnail.Caption %>" height="<%# ((Photo) Container.DataItem).Thumbnail.Height %>"
                                width="<%# ((Photo) Container.DataItem).Thumbnail.Width %>" src="<%# ((Photo) Container.DataItem).Thumbnail.VirtualPath %>"
                                runat="server" /></a>
                            </p>
                        </ItemTemplate>
                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:View>
            </asp:MultiView>
        </asp:View>
    </asp:MultiView>
</asp:Content>
