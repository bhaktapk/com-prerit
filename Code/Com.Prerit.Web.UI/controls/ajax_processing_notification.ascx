<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ajax_processing_notification.ascx.cs" Inherits="controls_ajax_processing_notification" %>
<ajaxtoolkit:AlwaysVisibleControlExtender TargetControlID="updateProgress"
    VerticalSide="Middle" VerticalOffset="50" HorizontalSide="Center" HorizontalOffset="50"
    runat="server" />
<asp:UpdateProgress ID="updateProgress" runat="server">
    <ProgressTemplate>
        <div id="processingNotification">
            Processing...
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
