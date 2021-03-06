# Change Log
* [release:75158](release_75158) Released 16/10/11
	* New Booking System (out of Beta)
	* New Help Desk (out of Beta)
	* New My Files (Developer Preview)
	* Token now saved into Cookie so the system doesn't TIMEOUT as much
	* File Changes:
		* ~/bin/hap.ad.dll
		* ~/bin/hap.web.dll
		* ~/bin/hap.data.dll
		* ~/bin/hap.web.configuration.dll
		* ~/bookingsystem/admin/default.aspx
		* ~/bookingsystem/default.aspx
		* REMOVED ~/bookingsystem/bookingpopup.ascx
		* REMOVED ~/bookingsystem/daylist.ascx
		* REMOVED ~/bookingsystem/new.aspx
		* ~/helpdesk/default.aspx
		* ~/helpdesk/*.htm
		* REMOVED ~/images/headbg.png
		* ~/images/myfiles.png
		* REMOVED ~/helpdesk/new.aspx
		* REMOVED ~/images/staticb.png
		* REMOVED ~/images/swish.png
		* ~/scripts/jquery.dataTable.js
		* ~/style/basestyle.css
		* ~/style/bookingsystem.css
		* ~/style/helpdesk.css
		* ~/style/jquery-ui.css
		* ~/style/myfiles.css
		* ~/tracker/default.aspx
* [release:74741](release_74741) Released 9/10/11
	* Some bug fixes from v7.2
	* New Homepage Style
	* New Tracker Index Page Style
	* BETA Booking System Page (new.aspx)
	* New API's
		* ~/API/BookingSystem
		* ~/API/HelpDesk
	* Updated jQuery UI 1.8.16
	* File Changes:
		* ~/bin/hap.web.dll
		* ~/bin/hap.web.config.dll
		* ~/bin/hap.ad.dll
		* ~/bin/hap.data.dll
		* ~/bookingsystem/new.aspx
		* ~/images/icons/*
		* ~/scripts/jquery-ui-1.8.16-custom.min.js
		* ~/style/basestyle.css
		* ~/style/bookingsystem.css
		* ~/style/helpdesk.css
		* ~/style/jquery-ui.css
		* ~/tracker/default.aspx
		* ~/tracker/tracker.css
		* ~/default.aspx
		* ~/masterpage.master
	* Changes from v7.4.0910.2200:
		* ~/bin/hap.web.dll
		* ~/bin/hap.data.dll
		* ~/bin/hap.web.configuration.dll
		* ~/bookingsystem/new.aspx
		* ~/helpdesk/new.aspx
		* ~/style/basestyles.css
		* ~/style/helpdesk.css
	* Changes from v7.4.1010.1640:
		* ~/bin/hap.web.dll
		* ~/bin/hap.web.configuration.dll
		* ~/bookingsystem/new.aspx
		* ~/helpdesk/new.aspx
		* ~/images/bookingsystem.png
		* ~/images/helpdesk.png
		* ~/tracker/default.aspx
* [release:73712](release_73712) Released 21/9/11
	* FIXED: Position of the Logout Link and the Booking System
	* FIXED: Changing the day on the booking system in older browsers (<=IE8)
	* ADDED: Sorted the Subjects List A-Z
	* FIXED: The Setup page js (AJAX) issues 
	* FIXED: If no email address is detected or SMTP is disabled, no email will be sent
	* FIXED: Issue with the Help Desk
	* File Changes:
		* ~/bin/hap.web.dll
		* ~/bin/hap.web.configuration.dll
		* ~/bookingsystem/bookingpopup.ascx
		* ~/bookingsystem/bookingsystem.css
		* ~/bookingsystem/default.aspx
		* ~/helpdesk/default.aspx
		* ~/scripts/jquery.ba-hashchange.min.js
		* ~/style/basestyle.css
		* ~/style/setup.css
		* ~/setup.aspx
* [release:73598](release_73598) Released 19/9/11
	* Fixed any bugs from v7.0
	* FIXED MSCB Basic Silverlight Upload Filter Issue
	* FIXED MSCB Extended Silverlight Upload Check Issue
	* FIXED MSCB Hidden Files issue
	* ADDED Code to hide HIDDEN and SYSTEM files/folders
	* FIXED Homepage showing extra tabs that didn't do anything
	* File Changes:
		* ~/bin/hap.web.dll
		* ~/bin/hap.web.configuration.dll
* [release:71361](release_71361) Released 18/9/11
	* ! There is updated [installation video](installation-video) and [Documentation](Documentation) for this release!
	* First Release of the v7 Branch, release includes:
	* New Configuration Utility (Built in)
	* New Logon System
	* Improved Security
	* Additional Booking System Features
	* File Changes:
		* No Changes to HAP+ User Card/Logon Tracker/Quota Service Setup
		* !!This is a MAJOR RELEASE, and as such, all WEB need to be replaced!
* [release:68687](release_68687) Released 20/6/11
	* Attempt at fixing Chrome/Safari/Opera issue with downloading
	* Added File Size/Last Write meta data to downloads (When you download your browser can give you and ETA)
	* Fix for the Term Dates in the Booking System
	* Moved iCal generation to write to the ~/App_Data/ folder
	* Increased the buffer size in the User Card
	* File Changes from v6.3
		* HAP User Card.exe.config
		* ~/bin/hap.data.dll
		* ~/bin/hap.web.dll
		* ~/images/icons/knownicons.xml
		* ~/mycomputersl.aspx
* [release:67263](release_67263) Released 29/5/11
	* Fix file locking of picture files in MSC
	* Support Resource Admin Emails
	* Fix issue with ICAL files and Exchange 2010
	* Support for Booking System Email Templates
	* Fix issue when moving files they loose file extension
	* Fix issue when removing a booking it would not email a cancel to admins
	* Another Ampersands fix 
	* File Changes:
		* ~/App_Data/BookingTemplates.xml
		* ~/Bin/HAP.Data.dll
		* ~/Bin/HAP.Web.dll
		* ~/Bin/HAP.Web.Configuration.dll
		* ~/BookingSystem/Admin/Default.aspx
		* ~/BookingSystem/BookingPopup.ascx
		* ~/ClientBin/HAP.Silverlight.xap
		* ~/ClientBin/HAP.Silverlight.Browser.xap
		* ~/web.config
* [release:66301](release_66301) Released 14/5/11
	* Fixed the booking system overview calendar
	* Removed 1 instance of duplicate data in the web.config
	* Enabled the home page to show no tabs
	* Added a FOCUS call to the User Card to make closing it easier
	* Updated the Thanks.rtf file
	* Modified some of the layout structure for the My Computer browsers
	* Added a basic search ability to My Computer Extended Edition
	* File Changes:
		* ~/bin/hap.ad.dll
		* ~/bin/hap.web.dll
		* ~/bin/hap.web.configuration.dll
		* ~/clientbin/hap.silverlight.browser.xap
		* ~/scripts/viewmode.js
		* ~/mycomputer.aspx
		* ~/mycomputersl.aspx
		* ~/thanks.rtf
		* ~/web.config
* [release:65318](release_65318) Released 28/4/11
	* Removed the ADUserName + ADPassword from the User Card API
	* User Card API Lock down Video: [http://www.youtube.com/watch?v=di_QnHjcWYg](http://www.youtube.com/watch?v=di_QnHjcWYg)
	* File Changes
		* ~/bin/hap.web.dll
		* ~/bin/hap.data.dll
		* HAP User Card Installer.msi
* [release:64750](release_64750) Released 19/4/11
	* Contains 2 files to fix an issue with v6.0. - See [workitem:8673](workitem_8673)
	* File Changes:
		* ~/bin/hap.web.dll
		* ~/clientbin/hap.silverlight.browser.xap
* [release:64562](release_64562) Released 16/4/11
	* New My Computer Browser API (Using ASP.net Web Services)
	* My Computer Silverlight Browser updated to use the New API
	* Added quota information service to retrieve quota data
	* Added a User Card Application
	* New Logon Tracker API
	* Moved Logon Tracker to the new API
	* Added a MS SQL Provider for the logon tracker
	* Updated the Documentation Section
	* New Videos
		* !! Notes:
		* HAP+ Logon Tracker's API is now API.asmx instead of API.ashx, you will need to update your IIS authentication settings!
		* Don't overwrite the files in the App_Data folder when performing an upgrade, as it will overwrite them
		* MAJOR WEB.CONFIG CHANGES!
* [release:58103](release_58103) Released 25/12/10
	* Added caching support to the base role provider
	* Deleted the redundant OU Selector dialogue in the config tool
	* Made the Home Page Links Config Step "Edit Web.Config" ONLY!
	* Added Groups to the Home Page Links!!!!
	* Updated the change my password control to pick up it's text off the description field in the config
	* Logon Tracker's API will wait for the database file to be released before processing (before it would wait try to write the file twice, and then not write the info)
	* File Changes:
		* ~/bin/hap.ad.dll
		* ~/bin/hap.web.dll
		* ~/bin/hap config.exe
		* ~/default.aspx
		* ~/mycomputer.css
		* **~/web.config**
* [release:56085](release_56085) Released 20/11/10
	* Added logic to the My Computer Browsers to allow for users with no home directories (set in ad anyhow)
	* Renamed the My School Computer Enhanced page to My School Computer Extended Edition
	* Removed MessageBox from the Basic Uploader
	* File Changes:
		* ~/bin/hap.web.dll
		* ~/clientbin/hap.silverlight.xap
		* ~/mycomputersl.aspx
* [release:56080](release_56080) Released 20/11/10
	* Improved the Silverlight My Computer Browser's speed at loading a large folder
	* Added Cancellation iCal Emails (When bookings are cancelled, the booking system sends a cancellation email for outlook to process)
	* Added auto BookingSystem cleaning (bookings that are older that 1 week from the current day are pruned from the XML database (can be disabled via config))
	* Updated ConfigTool to support above item
	* Changed a bit of javascript on the bookingsystem to avoid any naming issues (found out it was causing some in IE)
	* File Changes:
		* ~/bin/hap.web.dll
		* ~/bin/hap config.exe
		* ~/clientbin/hap.silverlight.browser.xap
		* ~/bookingsystem/bookingpopup.ascx
		* ~/bookingsystem/default.aspx
		* ~/web.config  _(note this is only partially true)_
* [release:54747](release_54747) Released 28/10/10
	* Fixed the problem with renaming a file with no extension in the My Computer API
	* Fixed the problem with moving files between folders in the My Computer API
	* File Change:
		* ~/bin/hap.web.dll
* [release:54350](release_54350) Released 21/10/10
	* Changed CHS.dll to HAP.AD.dll
	* Updated Web.config as above
	* Added a Week View Calendar (Very basic ATM) to the booking system
	* Added a client side API call to change the day to the booking system
	* Changed the way My Computer handles Dates (uses server date format instead of EN-GB Hardcode
	* File Changes
		* ~/bin/hap.ad.dll  <- Remove CHS.dll
		* ~/bin/hap.dll
		* ~/bookingsystem/bookingsystem.css
		* ~/bookingsystem/default.aspx
			* ~/bookingsystem/weekview.aspx
		* ~/web.config:
			* Line 144: <add name="DomainLoginMembershipProvider" attributeMapUsername="sAMAccountName" type="**HAP.AD**.ActiveDirectoryMembershipProvider,HAP.AD" connectionStringName="ADConnectionString" connectionUsername="domain\Administrator" connectionPassword="" />
			* Line 150: <add name="ActiveDirectoryRoleProvider" type="**HAP.AD**.ActiveDirectoryRoleProvider,HAP.AD" connectionStringName="ADConnectionString" connectionUsername="domain\Administrator" connectionPassword="" />
* [release:53528](release_53528) Released 6/10/10
	* Changed the way the silverlight browser downloads (to avoid the IE yellow bar)
	* Updates to the logon tracker UI
	* New IIS 7.5 Settings Package
	* File Changes:
		* HAP Logon Tracker.exe/msi
		* ~/bin/hap.web.dll
		* ~/clientbin/hap.silverlight.browser.xap
* [release:52938](release_52938) Released 26/9/10
	* Fixed the logon tracker UI
	* Added an override code (CTRL + Click on the CANCEL button (my require 2 clicks))
	* Added Log all off Button to Live Tracker
	* Updated Archive button
	* File Changes:
		* ~/bin/hap.web.dll
		* ~/bin/hap config.exe
		* ~/tracker/live.aspx
		* ~/tracker/log.aspx
		* ~/web.config
		* HAP Logon Tracker.exe
		* HAP Logon Tracker.msi
* [release:52231](release_52231) Released 12/9/10
	* Fixed Basic Silverlight Uploader
	* Added Right Click Download Context Option in Enhanced My Computer Browser
	* File Changes:
		* ~/bin/hap.web.dll
		* ~/clientbin/hap.silverlight.xap
		* ~/clientbin/hap.silverlight.browser.xap
	* **Note:** The Update file updates versions 5.2.2 through 5.2.9 to 5.2.10.
* [release:51876](release_51876) Released 5/9/10
	* Changed the logout button to use the browsers alert popup rather than HTML
	* File Changes:
		* ~/bin/hap.web.dll
		* ~/chs.master
* [release:51804](release_51804) Released 3/9/10
	* Added Change My Password
	* Added Logoff Button
	* Changed the rename message when nothing is entered
	* File Changes:
		* ~/bin/hap config.exe
		* ~/bin/hap.web.dll
		* ~/bookingsystem/admin/default.aspx
		* ~/bookingsystem/default.aspx
		* ~/clientbin/hap.silverlight.browser.xap
		* ~/controls/ChangePassword.ascx
		* ~/helpdesk/default.aspx
		* ~/basestyle.css
		* ~/chs.master
		* ~/default.aspx
		* ~/mycomputer.aspx
		* ~/web.config
* [release:51721](release_51721) Released 2/9/10
	* Fixed Right Click Issues in Silverlight Browser
	* File Changes:
		* ~/bin/hap.web.dll
		* ~/clientbin/hap.silverlight.browser.xap
* [release:51649](release_51649) Released 1/9/10
	* Fixed problem with renaming
	* Fixed problem with auto selecting the last folder when you refresh the page
	* File Changes:
		* ~/bin/HAP.web.dll
		* ~/clientbin/HAP.Silverlight.Browser.xap
* [release:51408](release_51408) Released 28/8/10
	* Fixed some caching issues in the booking system
	* File Changes:
		* ~/bin/hap.web.dll
* [release:51345](release_51345) Released 27/8/10
	* Fixed the HTML Uploader
	* Fixed some of the caching on the booking system
	* Fixed the position of the Calendar in the booking system
	* Added a file type error message in the HTML Uploader
	* Fixed the Basic Silverlight Uploader when the browser doesn't allow drag file from outside (Firefox 3.6.4 ->)
	* File Changes:
		* ~/bin/hap.web.dll
		* ~/clientbin/hap.silverlight.dll
		* ~/bookingsystem/default.aspx
		* ~/uploadh.aspx
* [release:51301](release_51301) Released 27/8/10
	* Updated the Logon Tracker UI to avoid the user cancelling the tracker prompt
	* Added an MSI for the Tracker
	* File Changes:
		* ~/bin/HAP.Web.dll (no real changes but the version number has been updated)
		* HAP Logon Tracker.exe
		* HAP Logon Tracker Installer.msi
* [release:50493](release_50493) Released 12/8/10
	* **Please DELETE: ~/bin/System.Web.Ajax.dll, and the sub folders in ~/bin/**
	* Added Subjects Drop Down to the Booking System
	* Added Day Drop Down to the Booking System Admin Page
	* Added the Username Drop Down to the Booking System Admin Page
	* Added a Basic Mode button to the Silverlight Browser in My Computer
	* Fixed the CSS issue with the booking system 
	* Added Caching to the Booking System Admin User List
	* Changed the Loading Animation on the Booking System Admin Page
	* Removed Duplicate Code on the Booking System main page (javascript) 
	* Removed hardcoded references to /extranet/ in the url 
	* Fixed CSS on hap folder chance
	* Added more try statements to the logon tracker 
	* File Changes:
		* hap logon tracker.exe
		* ~/bin/ajaxcontroltoolkit.dll
		* ~/bin/hap config.exe
		* ~/bin/hap.web.dll
		* ~/bookingsystem/admin/default.aspx
		* ~/bookingsystem/BookingPopup.ascx
		* ~/bookingsystem/bookingsystem.css
		* ~/bookingsystem/default.aspx
		* ~/bookingsystem/display.aspx
		* ~/bookingsystem/overviewcalendar.aspx
		* ~/clientbin/hap.web.silverlight.xap
		* ~/controls/*.ascx
		* ~/helpdesk/default.aspx
		* ~/helpdesk/*.htm
		* ~/helpdesk/helpdesk.css
		* ~/scripts/rightclick.js
		* ~/scripts/viewmode.js
		* ~/tracker/default.aspx
		* ~/tracker/tracker.css
		* ~/basestyles.css
		* ~/chs.master
		* ~/default.aspx
		* ~/move.aspx
		* ~/mycomputer.aspx
		* ~/mycomputersl.aspx
		* ~/mycomputer.css
		* ~/uploadh.aspx
		* ~/web.config
		* ~/xls.aspx
* [release:50258](release_50258) Released 8/8/10
	* Added Logon Tracker
	* Added Drag and Drop Upload to the Tree#
	* Updated the Config Tool
	* Some Bug Fixes in the API
	* Changed the way the My Computer Version Selector prompts the user
	* Added a better messaging system for the silverlight version with regards to firefox 3.6.4.
	* Added some indication of locked folders (NTFS permissions) in the silverlight browser
	* File Changes:
		* ~/App_Data/Tracker.xml
		* ~/Bin/HAP.Web.dll
		* ~/Bin/Hap Config.exe
		* ~/ClientBin/HAP.Silverlight.Browser.xap
		* ~/images/table-head-bg.gif
		* ~/images/icons/tracker.png
		* ~/images/icons/tracker-historic.png
		* ~/images/icons/tracker-live.png
		* ~/scripts/viewmode.js
		* ~/Tracker/*
		* ~/mycomputer.aspx
		* ~/web.config
* [release:49841](release_49841) Released 30/7/10
	* Added Treeview Icons in the Silverlight Browser
	* Set the API to use '|' instead of ',' as seperators.
	* Added my favourites icon
	* Set the Maximize button to use the default full screen routines
	* File Changes:
		* ~/bin/HAP.Web.dll
		* ~/bin/HAP Config.exe
		* ~/clientbin/HAP.Silverlight.Browser.xap
		* ~/images/icons/myfavs.png
* [release:49544](release_49544) Released 24/7/10
	* Fixed a problem with the silverlight browser uploading
	* File Changes:
		* ~/bin/HAP.Web.dll
		* ~/clientbin/HAP.Silverlight.Browser.xap
* [release:49249](release_49249) Released 19/7/10
	* Fixed HAP Config.exe so it saves
	* Removed the fixed mapping of N\ to be the users home directory, this is now set in the web.config
	* Removed hard coded mappings for the drive letter N
	* Fixed the Finish button in HAP Config.exe
	* Updated the API to represent these changes
	* Added Enable HTML move for Drives
	* File Changes:
		* ~/bin/hap.web.dll
		* ~/bin/hap config.exe
		* ~/bookingsystem/SIMS.ascx
		* **~/web.config**
* [release:49190](release_49190) Released 18/7/10
	* Removed Hard Coded CC3 OU Paths
	* Redesigned the HAP Config tool
	* Made the HAP Config Tool Launch after running the MSI
	* If a user is entered in the booking system admin, and the user doesn't exist in AD, it'll display unknown user rather than throwing an exception
	* A popup box will be shown if silverlight is installed, asking which my computer version to use
	* The update checker has been changed to use VersionInfo.CompareTo (rather than comparing the individual parts of the version string)
	* Changed the SIMS import box to not require validation
	* Moved the Overview Calendar into an iFrame, this improves the loading speeds of the booking system
	* Set release mode to RELEASE (rather than DEBUG)
	* File Changes:
		* ~/bin/hap.web.dll
		* ~/bin/hap config.exe
		* ~/bin/AeroWizard.dll
		* ~/bookingsystem/default.aspx
		* ~/bookingsystem/overviewcalendar.ascx
		* ~/bookingsystem/overviewcalendar.aspx
		* ~/bookingsystem/SIMS.ascx
		* ~/clientbin/HAP.Silverlight.xap
		* ~/clientbin/HAP.Silverlight.Browser.xap
		* ~/scripts/viewmode.js
		* ~/scripts/silverlight.js
		* ~/mycomputer.aspx
		* ~/mycomputersl.aspx
		* ~/mycomputer.css
		* ~/web.config
* [release:48359](release_48359) Released 5/7/10
	* Reverted the download handler to v4.2
	* File Changes:
		* ~/bin/hap.web.dll
		* ~/bin/hap.web.pdb
* [release:48317](release_48317) Released 4/7/10
	* Fixed the API so it works in IIS 6 (previous required IIS7+)
	* File Changes
		* ~/bin/HAP.Web.dll
		* ~/bin/HAP.Web.pdb
* [release:48296](release_48296) Released 4/7/10
	* Alpha API for Booking System
	* Bug fixes in HAP.Silverlight.Browser
	* Some other fixes
	* Fixed HAP.Config.exe
	* Updated Icons
	* Additional Icons
	* Favicon
	* File Changes:
		* ~/bin/HAP.Config.exe
		* ~/bin/HAP.Web.dll
		* ~/bin/HAP.Web.PDB
		* ~/bookingsystem/api.ashx
		* ~/ClientBin/HAP.Silverlight.Browser.xap
		* ~/Images/icons/cs.png
		* ~/Images/icons/config.png
		* ~/Images/icons/newfolder.png
		* ~/Images/icons/xaml.png
		* ~/Images/icons/xml.png
* [release:47874](release_47874) Released 26/6/10
	* SIMS Import on the Booking System
	* Silverlight My Computer Browser
	* My Computer API
	* Update Checker
	* version tags (Meta)
	* File Changes:
		* ~/bin/hap.web.dll
		* ~/bin/hap.web.pdb
		* ~/bin/hap.config.exe
		* ~/bookingsystem/bookingsystem.css
		* ~/bookingsystem/default.aspx
		* ~/bookingsystem/sims.ascx
		* ~/clientbin/hap.silverlight.browser.xap
		* ~/controls/updatechecker.ascx
		* ~/images/logo.png
		* ~/chs.master
		* ~/default.aspx
		* ~/mycomputer.aspx
		* ~/mycomputer.css
		* ~/mycomputersl.aspx
		* ~/mycomputerslsplash.xmal
* [release:45441](release_45441) Released 17/5/10
* Changed how my computer handles NTFS permission exceptions
	* File Changes:
		* ~/Bin/Hap.Web.dll
		* ~/bin/Hap.Web.pdb
* [release:44710](release_44710) Released 3/5/10
	* Added Overrides into the Booking System
	* Some slight CSS changes to the Help Desk
	* Updated the config tool to work anywhere on the LAN
	* File Changees:
		* ~/Bin/HAP.Config.exe
		* ~/Bin/Hap.Web.dll
		* ~/bin/Hap.Web.pdb
		* ~/bookingsystem/bookingsystem.css
		* ~/bookingsystem/OverviewCalendar.ascx
		* ~/helpdesk/helpdesk.css
		* ~/images/browser-a-override-hover.gif
* [release:44497](release_44497) Released 29/4/10
	* Added Disable Code for the Booking System Resources
	* Updated HAP.Config to include above changes
	* File Changes:
		* ~/Bin/HAP.Config.exe
		* ~/Bin/HAP.Web.dll
		* ~/Bin/HAP.web.pdb
* [release:44397](release_44397) Released 28/4/10
	* booking system fixes/additions
	* Uploader fixes
	* Added excluded extensions in my computer
	* Updated Config Tool
	* Fixed an issue with users who have been given Network/Delegate network rights
	* Added 2 new icons
	* File Changes:
		* ~/bin/chs.dll
		* ~/bin/HAP.Web.dll
		* ~/bin/HAP.Web.pdb
		* ~/bin/HAP.Config.exe
		* ~/BookingSystem/bookingsystem.css
		* ~/BookingSystem/daylist.ascx
		* ~/BookingSystem/Default.aspx
		* ~/BookingSystem/OverviewCalendar.ascx
		* ~/images/icons/rdp.png
		* ~/images/icons/xml.png
		* ~/web.config
* [release:44206](release_44206) Released 23/4/10
	* Fixed an issue with laptops and the booking system (CSS and code fixes)
	* Moved filters to top
	* Added some Javascript to position the calendar on opening
	* Recoded the Help Desk/Booking System user load process (it much quicker, and works with exchange on the domain)
	* File Changes:
		* ~/bin/HAP.Web.dll
		* ~/bin/HAP.Web.pdb
		* ~/BookingSystem/bookingsystem.css
		* ~/BookingSystem/daylist.ascx
		* ~/BookingSystem/Default.aspx
* [release:43849](release_43849) Released 18/4/10
	* Moved to using .net 4.0
	* New Silverlight Uploader
	* Various .net 4 fixes and tweaks
	* File Changes:
		* All fixes have changed
* [release:43461](release_43461) Released 12/4/10
	* Fixed the wrong date in the iCal Generator
	* Fixed the admin booking posting logging it as being booked by the admin
	* Fixed the problem of 6 or more lessons not showing those lessons
	* File Changes:
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
		* ~/BookingSystem/BookingPopup.ascx
		* ~/BookingSystem/BookingSystem.css
		* ~/BookingSystem/DayList.ascx
		* ~/BookingSystem/Default.aspx
* [release:43379](release_43379) Released 11/4/10
	* Add lesson naming
	* Fixed a bug in the help desk which was rendering the wrong URL for tickets
	* Planning has started for the new silverlight uploader
	* File Changes:
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
		* ~/BookingSystem/*
		* ~/helpdesk/default.aspx
		* ~/images/web.config
		* ~/scripts/web.config
		* ~/web.config
* [release:43154](release_43154) Released 6/4/10
	* Attempt to fix Domain Admin Lookup box
	* File Changes:
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
* [release:43086](release_43086) Released 4/4/10
	* Fixed booking system where it doesn't load for non Domain Admins
	* File Changes:
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
		* ~/images/icons/bookingsystem.png
		* ~/web.config
* [release:43067](release_43067) Released 4/4/10
	* Added the booking system
	* File Changes:
		* ~/app_data/*
		* ~/bin/CHS.dll
		* ~/bin/CHS.pdb
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
		* ~/BookingSystem/*
		* ~/images/*
		* ~/helpdesk/Default.aspx
		* ~/web.config
* [release:42953](release_42953) Released 1/4/10
	* Fixed: Issue with & ampersand
	* File Changes:
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
		* ~/Scripts/viewmode.js
* [release:42665](release_42665) Released 29/3/10
	* More AJAX to reduce page refreshes (Deleting, New Folder, Rename moved from browser popups)
	* Only 3 Browser Popups (1 for Moving, 1 for Previewing & 1 for Uploading)
	* ASP.net URL Routing (Nicer looking urls: ~/mycomputer/n/my pictures, ~/download/n/some file.ext, ~/preview/n/someworddoc.docx, ~/helpdesk/ticket/123)
	* Various CSS fixes
	* Fixed HTML upload problem
	* File Changes:
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
		* ~/Controls/*
		* ~/HelpDesk/*
		* ~/Scripts/*
		* ~/basestyle.css
		* ~/Default.aspx
		* ~/Global.asax
		* ~/Move.aspx
		* ~/MyComputer.aspx
		* ~/MyComputer.css
		* ~/Web.config:  Additions: 
			* <extranetConfig><announcementbox showto="All" enableeditto="Domain Admins" /></extranetConfig>
			* <system.web><httpModules><add name="RoutingModule" type="System.Web.Routing.UrlRoutingModule, System.Web.Routing, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" /></httpModules></system.web>
			* <system.webServer></system.webServer> (please update whole section)
* [release:42293](release_42293) Released 20/3/10
	* Fixed CSS for My Computer in List View
	* Ability to remember which view mode you have selected
	* Added HA+ home button to the Help Desk
	* Moved the announcement box to a user control (see below)
	* If the GivenName attribute in AD isn't set, it'll use the display name
	* Fixed the Unauthorised title (so it actually puts the school name in)
	* File Changes:
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
		* ~/helpdesk/default.aspx
		* ~/helpdesk/helpdesksheet.css
		* ~/scripts/viewmode.js
		* ~/announcement.aspx
		* ~/default.aspx
		* ~/mycomputer.css
		* ~/mycomputer.aspx
* [release:41936](release_41936) Released 14/3/10
	* Added Breadcrumbs to My Computer
	* File Changes:
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
		* ~/images/arrow-right.gif
		* ~/images/breadcrumbover.gif
		* ~/mycomputer.aspx
		* ~/mycomputer.css
* [release:41883](release_41883) Released 13/3/10
	* Fixed Help Desk
	* File Changes:
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
		* ~/helpdesk/*.htm
* v3.1.3.0 Released 13/3/10
	* Added Templates for Help Desk Emails
	* File Changes:
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
		* ~/helpdesk/*.htm
* [release:41871](release_41871) Released 13/3/10
	* Added SSL Support to Help Desk SMTP
	* Added Authentication Support to Help Desk SMTP
	* File Changes:
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
		* ~/web.config
* v3.1.2.0 Released 13/3/10
	* Added Upload File Filters
	* File Changes:
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
		* ~/web.config
* [release:41846](release_41846) Released 12/3/10
	* Fixed the Help Desk
	* File Changes:
		* ~/App_Data/Tickets.xml
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
* [release:41840](release_41840) Released 12/3/10
	* Fixed ampersand issue
	* Added Unzip Features
	* Added Help Desk
	* Added No Silverlight Prompt
	* File Changes:
		* ~/App_Data/Tickets.xml
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
		* ~/HelpDesk/*
		* ~/Images/*
		* ~/Scripts/*
		* ~/basestyle.css
		* ~/Default.aspx
		* ~/MyComputer.aspx
		* ~/mycomputer.css
		* ~/unzip.aspx
		* ~/upload.aspx
		* ~/web.config
* [release:41514](release_41514) Released 6/3/10
	* Added Announcement Box
	* Removed script files that aren't needed
	* Fixed & issue in directory path
	* Stylesheet Changes
	* File Changes
		* ~/App_Data/Announcement.xml
		* ~/bin/CHS Extranet.dll
		* ~/bin/CHS Extranet.pdb
		* ~/Scripts/
		* ~/basestyle.css
		* ~/Default.aspx
		* ~/mycomputer.css
* [release:41188](release_41188) Released 1/3/10
	* Fixed Stylesheet in MyComputer.aspx
	* File Changed:
		* ~/chs.master
* [release:41179](release_41179) Released 28/2/10
	* Fixed Move page
	* Files Changed:
		* ~/Bin/CHS Extranet.dll
		* ~/Bin/CHS Extranet.pdb
* [release:41165](release_41165) Released 28/2/10
	* Reconfiguration of the web.config
	* Ability to add additional links to homepage via web.config
	* Ability to add additional drives (remove drives as well) via the web.config
	* Improved speed
	* Custom Redirection page for unauthorised access
	* Files Changed:
		* All Apart from:
			* ~/Images/
			* ~/App_Data/
			* ~/Client_Bin/
* [release:40949](release_40949) Released 23/2/10
	* Added HTML preview options for XLS, XLSX, DOCX
* [release:40755](release_40755) Released 19/2/10
	* Fixed the update my details not updating the department/form
	* Tried to fix the issue when the ampersand (&) is in the filename, something which any kid caught doing should be taken out back and shot
	* Files Changed:
		* bin/CHS Extranet.dll
		* bin/CHS Extranet.pdb
* Beta 2.2 - Released 18/2/10
	* Moved to Basic Authentication
	* Using ASP.net Identity Impersonate for added security
	* Tweaked the code to support basic authentication
	* Recoded the file download handler, it now has a lot more MIME Types, uses asp.net 2 TransferFile instead of WriteFile (asp.net 1.1), it also should work with spaces in the file name
	* Files Changed:
		* bin/CHS Extranet.dll
		* bin/CHS Extranet.pdb
		* web.config
* Beta 2 - Released 17/2/10
	* UNC paths instead of just server name for the shared areas
	* Fixed learning resources link
	* Pupil photos (will require a SIMS export in XML format, containing UPN, and Photo)
	* Swapping buttons on the default.aspx screen with links (like in the my computer view)
	* Additional tweaks