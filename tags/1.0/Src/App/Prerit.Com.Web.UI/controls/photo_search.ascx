<%@ Control Language="C#" AutoEventWireup="true" CodeFile="photo_search.ascx.cs" Inherits="controls_photo_search" %>
<h2><span>Photo Search</span></h2>
<fieldset>
	<asp:Label ID="photoSearchLabel" AssociatedControlID="photoSearchInputText" runat="server">Search:</asp:Label>
	<input id="photoSearchInputText" type="text" runat="server" />
	<br />
	<asp:Button ID="photoSearchButton" CssClass="button" OnClientClick="alert('Hold your horses. It is still under construction!'); return false;"
		OnClick="PhotoSearchButton_Click" Text="Search" runat="server" />
</fieldset>
