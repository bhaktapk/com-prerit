<%@ Import Namespace="Com.Prerit.Models.Resume" %>
<%@ Page Language="C#" MasterPageFile="~/views/shared/default.master" Inherits="System.Web.Mvc.ViewPage<IndexModel>" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server">Resume of Prerit Bhakta</asp:Content>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
    <meta name="description" content="Resume of Prerit Bhakta" />
    <meta name="keywords" content="resume, Prerit Bhakta" />
    <link rel="canonical" href="<%= Url.Action(MVC.Resume.Index()) %>" />
    <link rel="stylesheet" type="text/css" href="<%= Url.Content(Links.content.styles.resume.print_css) %>" media="print" />
    <link rel="stylesheet" type="text/css" href="<%= Url.Content(Links.content.styles.resume.screen_css) %>" media="projection, screen, tv" />
</asp:Content>

<asp:Content ContentPlaceHolderID="mainBarContent" runat="server">
    <h1><span>Technical Skills</span></h1>
    <h2><span>Languages</span></h2>
    <ul id="languageList" class="long3">
        <li class="group1first">C</li>
        <li class="group1">C#</li>
        <li class="group1">C++</li>
        <li class="group1">CSS</li>
        <li class="group1">HTML</li>
        <li class="group2first">JavaScript</li>
        <li class="group2">SQL</li>
        <li class="group2">VB.NET</li>
        <li class="group2">VBScript</li>
        <li class="group2">XHTML</li>
        <li class="group3first">XML</li>
        <li class="group3">XML Schema</li>
        <li class="group3">XPATH</li>
        <li class="group3">XSL</li>
        <li class="group3">XSLT</li>
    </ul>
    <h2><span>Concepts/Technologies</span></h2>
    <ul id="techList" class="long3">
        <li class="group1first">.NET Framework</li>
        <li class="group1">ADO</li>
        <li class="group1">ADO.NET</li>
        <li class="group1">ASP.NET</li>
        <li class="group2first">Design Patterns</li>
        <li class="group2">DTS</li>
        <li class="group2">NAnt</li>
        <li class="group2">NUnit</li>
        <li class="group3first">OOP</li>
        <li class="group3">SOAP</li>
        <li class="group3">Web Services</li>
        <li class="group3">WinForms</li>
    </ul>
    <h2><span>Databases</span></h2>
    <ul id="dbList" class="long3">
        <li class="group1first">MS Access</li>
        <li class="group2first">Oracle</li>
        <li class="group3first">SQL Server</li>
    </ul>
    <h1><span>Experience</span></h1>
    <h2><span><a href="http://www.perficient.com/" title="Website of the employer">Perficient</a> (04/2007 - present)</span></h2>
    <h3><span>Lead Developer of "Watershed" for <a href="http://www.msd.st-louis.mo.us/" title="Website of the client">Metropolitan
    Sewer District</a></span></h3>
    <p>
        The Watershed project at Metropolitan Sewer District is a multi-decade water conservation project for the greater St. Louis
        area. Organization is a key component in ensuring that the hundreds of engineers and planners communicate effectively over
        a long period of time. SharePoint 2007 was chosen as the most effective mechanism for this communication and as the lead
        consultant of this SharePoint implementation, my accomplishments consisted of the following:
    </p>
    <ul>
        <li>Architected and implemented site hierarchy which included Active Directory and SharePoint role based authentication for
            various SharePoint sites and sub-sites</li>
        <li>Facilitated in the migration of hundreds of gigabytes of data into various SharePoint document libraries</li>
        <li>Created reusable site templates for consistency between similar sites and sub-sites</li>
    </ul>
    <p>
        <strong>Environment:</strong> Active Directory, WSS 3.0
    </p>
    <h3><span>Lead UI Developer of "eNAC" for <a href="http://www.agedwards.com/" title="Website of the client">A.G. Edwards</a></span></h3>
    <p>
        Capturing customers' account information for a large financial company is a large undertaking. At A. G. Edwards, the goal
        was to rewrite an electronic New Account Card (eNAC) application that tied into their existing infrastructure utilizing
        SOA methodology. A large team of developers and testers were assembled to accomplish this task and as the lead consultant
        of the user interface, my accomplishments in this application consisted of the following:
    </p>
    <ul>
        <li>Lead a team of 4-5 developers to design and develop the user interface for a window application using the model view presenter
            and adapter design patterns</li>
        <li>Organized and delegated tasks during daily status update meetings and continually tracked progress to ensure milestones
            where reached</li>
        <li>Developed application wide error handling and validation of complex rules</li>
        <li>Created example units tests for other team members to follow</li>
    </ul>
    <p>
        <strong>Environment:</strong> C#, Design Patterns, OOP, Web Services, WinForms
    </p>
    <h2><span><a href="http://www.tech-partners.com/" title="Website of the employer">Technology Partners</a> (08/2006 - 04/2007)</span></h2>
    <h3><span>Developer of "Creo Framework" for <a href="http://www.maritz.com/" title="Website of the client">Maritz</a></span></h3>
    <p>
        To fulfill the training needs of Maritz Learning's clients, a scalable and robust architecture was needed as a foundation
        to rapidly develop custom applications. The Creo Framework, originally built upon .NET 1.1, was the solution to creating
        customized pluggable components to train people via different methods such as podcasts, email, voicemail messages, etc.
        As one of the developers, my accomplishments in this application consisted of the following:
    </p>
    <ul>
        <li>Introduced .NET 2.0 and SQL Server 2005 to the team members by successfully lobbying to management about the new features
            and enhancements associated to the new version</li>
        <li>Mentored team members on the new features of .NET 2.0 and SQL Server 2005 and upgraded the .NET project files</li>
        <li>Helped test Creo in terms of performance, pointed out bottlenecks and helped move the framework from a .NET remoting based
            framework to an inprocess one</li>
        <li>Finished implementing a previously unfinished privilege handling based on user accounts for business objects</li>
        <li>Created a list of enhancements and code optimizations and shared it with fellow team members</li>
    </ul>
    <p>
        <strong>Environment:</strong> ASP.NET, C#, Design Patterns, OOP, SQL Server, UML
    </p>
    <h2><span><a href="http://www.advancedresources.net/" title="Website of the employer">Advanced Resources</a> (10/2005 - 08/2006)</span></h2>
    <h3><span>Developer of "Reuters.com" for <a href="http://www.reuters.com/" title="Website of the client">Reuters</a></span></h3>
    <p>
        Reuters, one of the most respected and trustworthy news organizations, is world renown for it's ability to deliver up to
        the minute news from around the world. One key component of this core business requirement is a massive, yet robust website
        that is efficient and scalable. As one of the developers, my accomplishments in this application consisted of the following:
    </p>
    <ul>
        <li>Designed and developed a multilingual online hub for Reuters.com users to catch up on current events by watching free online
            news casts by using custom Reuters.com Design Patterns and ASP.NET caching</li>
        <li>Architected the ASP.NET error handling to better inform the developers and support staff of the root causes of reproducible
            .NET errors in every stage of the development lifecycle</li>
        <li>Mentored other developers by creating code samples of .NET best practices by finding mistakes and/or inefficiencies in the
            source code and sharing them at the weekly developers meeting</li>
        <li>Retroactively implemented .NET best practices gained from analyzing the source code in order to optimize the system</li>
    </ul>
    <p>
        <strong>Environment:</strong> ASP.NET, C#, CSS, Design Patterns, HTML, JavaScript, NAnt, OOP, XML
    </p>
    <h2><span><a href="http://www.stockell.com/" title="Website of the employer">Stockell</a> (03/2005 - 09/2005)</span></h2>
    <h3><span>Developer of "Digecenter" for <a href="http://www.jeromegroup.com/" title="Website of the client">Jerome Group</a></span></h3>
    <p>
        The ability for marketing departments to reach their customers quickly and efficiently via mass mailing is a problem that
        the Jerome Group provides a solution for. The Digecenter application is one of the tools that were created to expedite this
        process by allowing the Jerome Group's clients to customize their marketing needs. As one of the developers, my accomplishments
        in this application consisted of the following:
    </p>
    <ul>
        <li>Analyzed, architected (via UML static structure diagrams) and implemented "Gang of Four" Design Patterns in VB.NET to simplify
            object hierarchy, promote object reuse and improve overall efficiency of core business functionality</li>
        <li>Responsible for the research, design, and implementation of Opus online proofing Web Services integration into current infrastructure
            as well as producing impact analysis documentation and flow chart diagrams</li>
        <li>Optimized ASP.NET and WinForms code to perform significantly faster by utilizing a combination of lightweight data structures,
            .NET best practices, object oriented design principles and SQL Server performance enhancement techniques such as utilizing
            stored procedures, efficient query writing and indexes by using SQL Server Index Tuning Wizard</li>
        <li>Mentored other developers in object oriented programming techniques, .NET and SQL Server best practices, guidelines to follow
            and general advice on efficient .NET and SQL Server coding as well as the .NET object hierarchy</li>
    </ul>
    <p>
        <strong>Environment:</strong> ASP.NET, Design Patterns, OOP, SOAP, SQL Server, UML, VB.NET, Web Services, WinForms, XML,
        XSLT
    </p>
    <h2><span><a href="http://www.tech-partners.com/" title="Website of the employer">Technology Partners</a> (04/2004 - 04/2005)</span></h2>
    <h3><span>Developer of "Household Rewards" for <a href="http://www.maritz.com/" title="Website of the client">Maritz</a></span></h3>
    <p>
        As one of Maritz's many financial clients, Household International charged Maritz to create a rewards fulfillment system
        to create a unique customer experience. In the program, thousands of orders are created and fulfilled on a daily basis by
        Household International's customers. These products represent tens of thousands of dollars in merchandise and an enormous
        amount of money spent to acquire the necessary points to redeem them. As one of the developers, my accomplishments in this
        application consisted of the following:
    </p>
    <ul>
        <li>Developed and maintained an object oriented C# based ASP.NET e-commerce web application which utilizes SSL to secure sensitive
            credit card information transmitted over the internet</li>
        <li>Designed, developed and implemented the migration of customer history from a similar but distinctly different SQL Server
            database model of an older ASP based e-commerce web application</li>
        <li>Architected and developed SQL Server DTS packages to seamlessly integrate 2 independent systems on a daily basis in order
            for customers to receive their most up-to-date account information</li>
        <li>Implemented SQL Server optimizations to speed up the transaction times of orders on the e-commerce web application in order
            to enhance the user's customer experience and to allow more concurrent users</li>
        <li>Leveraged CSS and the database to create themes in order for Household to have many different programs associated with the
            various credit cards and differing functionality for each credit card</li>
    </ul>
    <p>
        <strong>Environment:</strong> ASP.NET, C#, CSS, Design Patterns, DTS, OOP, SQL Server, UML
    </p>
    <h2><span><a href="http://www.envision.com/" title="Website of the employer">Envision</a> (07/2000 - 04/2004)</span></h2>
    <h3><span>Developer of "Market Development Tool" for <a href="http://www.anheuser-busch.com/" title="Website of the client">
    Anheuser-Busch</a></span></h3>
    <p>
        The suite of applications known as the Market Development Tool (MDT) written under the direction of the Anheuser-Busch Sales
        Department consists of 3 separate applications which when linked together, provide invaluable market data to both upper
        management and field sales personnel. MDT exists in three forms in order to serve each of its three distinct user bases
        by targeting their specific needs. The total user base of MDT is well over a thousand users. In its first year of inception,
        MDT has been tremendously successful and has scored over 4 out of 5 on recent surveys. Originally, MDT began as a project
        with only myself and two other developers, but its importance and functionality quickly expanded and a total of seven developers
        were required to execute the business needs of Anheuser-Busch. As one of the developers, my accomplishments in this application
        consisted of the following:
    </p>
    <ul>
        <li>Instrumental in introducing the first .NET project written in C# at Anheuser-Busch because of my prior experience with the
            technology (self taught)</li>
        <li>Architected a multi-tier suite of 3 applications (2 web based ASP.NET applications and a Windows application) that reuses
            common business and ADO.NET data access components to reduce development time and cost against both Oracle and SQL Server</li>
        <li>Solely designed, developed and implemented one of the web applications to utilize SOAP, XML and Web Services as the data
            source and transportation, XSLT as the transformation source, and XPATH as the bridge between both sources to produce an
            XHTML interface</li>
        <li>Implemented a marketing initiative in 2 of the applications which annually manages over one hundred million dollars annually
            by tracking promotional expenditures</li>
        <li>Created the look and feel of the first application and leveraged it in the other applications to create a seamless suite
            of applications to reduce the learning curve of all end users</li>
        <li>Leveraged CSS to create brand based themes and the ability to change font sizes to the application to enhance the user's
            experience</li>
        <li>Mentored the less experienced developers in both C#, ASP.NET and the .NET framework</li>
    </ul>
    <p>
        <strong>Environment:</strong> ASP.NET, C#, CSS, Design Patterns, Oracle, OOP, SOAP, SQL Server, UML, Web Services, WinForms,
        XHTML, XML, XSLT
    </p>
    <h3><span>Developer of "SOTD Evaluations" for <a href="http://www.anheuser-busch.com/" title="Website of the client">Anheuser-Busch</a></span></h3>
    <p>
        Sales Operations Training and Development (SOTD) department required an application that could both generate and evaluate
        dynamic surveys taken by members of each training session. In this way, each member of the SOTD group could track his or
        her effectiveness in training other Anheuser-Busch employees. This web-based tool featured many rollup capabilities such
        as the ability to generate graphical reports on the status of an individual trainer or survey type. As the sole developer
        of SOTD Evaluations, my accomplishments in this application consisted of the following:
    </p>
    <ul>
        <li>Architected and implemented an ASP driven web based application written using CSS, XHTML and XML to build and maintain dynamic
            surveys</li>
        <li>Created a secure administration section for team members to perform complex administration tasks such creating, editing
            and deleting surveys as well as running complex reports on the effectiveness of Anheuser-Busch trainers</li>
        <li>Utilized CSS to graphically display individual survey results or group survey results to administrators of the application</li>
        <li>Used the MSXML component of SQL Server to automatically produce XML as its native output in order to use XSLT as the transformation
            source</li>
        <li>Leveraged CSS to create brand based themes and the ability to change font sizes to the application to enhance the user's
            experience</li>
    </ul>
    <p>
        <strong>Environment:</strong> ASP, CSS, SQL Server, XHTML, XML, XSLT
    </p>
    <h3><span>Developer of "Contact Gatherer" for <a href="http://www.marketingdirect.com/" title="Website of the client">Marketing
    Direct</a></span></h3>
    <p>
        In order to further reach consumers for Marketing Direct's clients, a database application was required to harvest the consumers'
        information. Marketing Direct's clients consists of many direct to consumer agencies around the United States who required
        an automated application to dynamically generate lists of customers on a daily basis. As the sole developer, my accomplishments
        in this application consisted of the following:
    </p>
    <ul>
        <li>Designed and implemented a SQL Server database tool for data extraction of potential clients from 3rd party documents</li>
        <li>Convinced the 3rd parties to utilize XML documents instead of transferring tab delimited text files in order for simplicity,
            data integrity and future upgradeability</li>
        <li>Architected complex AI logic into the tool to eliminate duplicate contacts thus saving Marketing Direct money by having
            a much more focused customer base</li>
        <li>Leveraged SQL Server's features to both expedite development time and transform the tool from a manual process to an automatic
            one</li>
    </ul>
    <p>
        <strong>Environment:</strong> SQL Server, XML, XSLT
    </p>
    <h3><span>Developer of "Webport-Ecom" for <a href="http://www.dhl.com/" title="Website of the client">DHL</a></span></h3>
    <p>
        As a global logistical company, DHL required a secure web-based application to track both air and ocean shipments made by
        their client companies. A complete list of ports and countries around the world as well as the ability to dynamically create
        PDFs of shipping documents is just a small part of the application known as Webport-Ecom. As a developer, my accomplishments
        in this application consisted of the following:
    </p>
    <ul>
        <li>Worked in a team of 3 developers to design and implement an ASP and COM/COM+ based solution to track global shipments</li>
        <li>Implemented the administration section to add, modify and delete system wide data such as shipping ports and countries</li>
        <li>Designed a security model around SSL to prevent unauthorized entry into the application</li>
        <li>Organized and used Acrobat FDF template documents as the basis for the dynamic generation of PDF shipping documents</li>
    </ul>
    <p>
        <strong>Environment:</strong> ASP, COM/COM+, SQL Server
    </p>
    <h1><span>Education</span></h1>
    <p>
        <strong>Bachelors in Computer Science</strong> at Saint Louis University
        <br />
        Minor in Mathematics
        <br />
        Graduated with Honors (Magna Cum Laude)
    </p>
    <h1><span>References</span></h1>
    <p>
        Available Upon Request
    </p>
</asp:Content>

<asp:Content ContentPlaceHolderID="sidebarContent" runat="server">
    <h2><span>Other Formats</span></h2>
    <ul>
        <li><a href="<%= Url.Action(MVC.ResumeFormats.AdobePdf()) %>" title="Resume of Prerit Bhakta in Adobe PDF format">Adobe PDF</a></li>
        <li><a href="<%= Url.Action(MVC.ResumeFormats.MicrosoftWord()) %>" title="Resume of Prerit Bhakta in Microsoft Word format">Microsoft Word</a></li>
    </ul>
</asp:Content>