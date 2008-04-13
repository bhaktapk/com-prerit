<%@ Control Language="C#" AutoEventWireup="true" CodeFile="random_photo.ascx.cs" Inherits="controls_random_photo" %>
<h2><span>Random Photo</span></h2>
<div class="photo">
	<p>
		<a id="photoLink" href="" onclick="window.open('{0}'); return false;" runat="server">
			<img id="photoImage" alt="Random photo" src="" runat="server" />
		</a>
	</p>
</div>
