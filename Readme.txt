REQUIREMENTS
============



Build Server
------------

ASP.NET MVC 1.0
CruiseControl.NET (1.5)
  * configured using BuildScripts\CCNet.config
  * dashboard deployed to ccnet.localhost
  * managed pipeline mode set to classic
IIS URL Rewrite Module 2.0
Microsoft .NET Framework 3.5 SP1 Runtime
SVN Command Line Client (1.6)
Web Deployment Project 2008
Windows SDK for Windows Server 2008 and .NET Framework 3.5

Host File Entries
.................

127.0.0.1	ccnet.localhost
127.0.0.1	ccnet.prerit.com
127.0.0.1	www.ccnet.prerit.com

IIS Sites
.........

ccnet.localhost
ccnet.prerit.com / www.ccnet.prerit.com



Development Machine
-------------------

ASP.NET MVC 1.0
IIS URL Rewrite Module 2.0
TortoiseSVN (1.6)
Visual Studio 2008 SP1 (C#)
Web Deployment Project 2008

Host File Entries
.................

127.0.0.1	dev.prerit.com
127.0.0.1	staging.prerit.com
127.0.0.1	www.dev.prerit.com
127.0.0.1	www.staging.prerit.com

IIS Sites
.........

dev.prerit.com / www.dev.prerit.com
staging.prerit.com / www.staging.prerit.com



BUILDING
========



For Development
---------------

Before opening Visual Studio, execute BuildScripts\BuildCommands\SetupDevEnvironment.cmd

Via Command Line
----------------

Common NAnt commands have been encapsulated in files in the BuildScripts\BuildCommands\ directory. Alternatively, NAnt commands can be executed if the current directory of a command prompt is set to the same directory.