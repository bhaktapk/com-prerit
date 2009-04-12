<%@ Import Namespace="Com.Prerit.Web.Controllers"%>
<%@ Import Namespace="Com.Prerit.Web.Models.About"%>
<%@ Page Language="C#" MasterPageFile="~/views/shared/default.master" Inherits="System.Web.Mvc.ViewPage<IndexModel>" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server">About the Website &amp; It's Author</asp:Content>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
    <meta name="description" content="A brief description about <%= Model.SiteName %> and it's author" />
    <meta name="keywords" content="about, <%= Model.SiteName %>, Prerit Bhakta" />
    <meta name="verify-v1" content="Zh9YODQtJIOw8hV64WsjasfjjqakXRWrCtzfe0XD/3Q=" >
</asp:Content>

<asp:Content ContentPlaceHolderID="mainBarContent" runat="server">
    <h1><span>About this Website</span></h1>
    <h2><span>Where am I?</span></h2>
    <p>
        "<%= Model.SiteName %>" is a virtual home away from home for Prerit Bhakta. It's where he can play with new technology and try to build something that
        maybe useful for both himself and family members.
    </p>
    <h2><span>Who's this site intended for?</span></h2>
    <p>
        The intent of "<%= Model.SiteName %>" is to amuse myself, keep my skills sharp by playing with new technologies, build an online hub for people that know
        me and always have an up-to-date resume online.
    </p>
    <h2><span>What do you plan on doing with this site?</span></h2>
    <p>
        Currently, the main goal is to create a photo hub for family members to upload pictures. That should take awhile because I have a lot of things
        going on in my personal life but am determined to accomplish it so that my family can keep in touch with each other better. A long term goal
        will be to gauge them to see if they would be interested in blogging together and if not, then just creating a personal blog.
    </p>
    <h1><span>About the Author</span></h1>
    <p>
        <img alt="Headshot of Prerit Bhakta" class="headshot" height="100" src="<%= Url.Content("~/content/images/about/headshot.jpg") %>" width="75" /> Prerit Bhakta
        is a programmer in the St. Louis metropolitan area who currently develops in the <a href="http://en.wikipedia.org/wiki/.NET_Framework" title=".NET Framework">
        .NET Framework</a> to fulfill his clients' needs. He's a geek and like all geeks, he likes to learn things, especially when it comes to the
        web. Why? Well, because the web changes so fast that it takes dedicated people who love it to learn it's complexities. He didn't always started
        out that way but he's always had a love of technology and science. He may have started late compared to some of my peers but once he learned
        about the underling structure and it's mathematical nature, he fell in love with it and took his education into overdrive. Now, he is one of
        the top developers in the area and gives constant feedback on improving systems. Feel free to check out his
        <%= Html.ActionLink("resume", ResumeController.Action.Index, null, new { title = "Resume of Prerit Bhakta" }) %>
        for more details on what he has accomplished and what he knows.
    </p>
</asp:Content>