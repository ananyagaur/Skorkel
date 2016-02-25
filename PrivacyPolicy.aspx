<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrivacyPolicy.aspx.cs" Inherits="PrivacyPolicy" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    </form>
</body>
</html>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" type="image/x-icon" href="logoSkorkel.ico" />
    <title>:: Skorkel ::</title>
    <link href="<%=ResolveUrl("css/style.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("css/stylever-2.css")%>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="<%=ResolveUrl("js/jquery-1.8.2.min.js")%>"></script>
    <script src="<%=ResolveUrl("css/placeholders.min.js")%>" type="text/javascript"></script>
    <style type="text/css">
        input[type=text]::-ms-clear
        {
            display: none;
        }
        input[type=textarea]::-ms-clear
        {
            color: #9c9c9c;
            display: none;
        }
        input[type=password]::-ms-reveal
        {
            display: none;
        }
        .signInput::-ms-clear
        {
            display: none;
        }
        .signInput::-ms-reveal
        {
            display: none;
        }
    </style>
    <style type="text/css">
        *
        {
            font-family: Arial;
            font-size: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="outterContainer" id="topHeader">
        <div class="container">
            <div class="topHeaderRight">
                &nbsp;
            </div>
        </div>
    </div>
    <!--header starts-->
    <div class="outterContainerResearch" id="documents">
        <div class="container">
            <div class="topContainer">
                <div class="logo">
                    <a href="Landing.aspx" title="Go to login page.">
                        <img src="images/logo.png" alt="Skorkel" /></a></div>
                <!--header ends-->
            </div>
            <!--heading ends-->
            <div class="cls">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </div>
            <!--inner container starts-->
            <div class="innerContainer" id="documentContainer">
                <div class="headingNew">
                </div>
            </div>
            <div class="cls">
            </div>
            <!--inner container ends-->
            <div class="innerDocumentContainerSpc">
                <div class="innerContainer">
                    <div class="innerGroupBox">
                        <div style="margin-top: 50px; font-family: Arial; font-size: 14px; font-weight: normal;
                            padding: 0px 20px 20px 20px; line-height: 1.5em; text-align: justify;">
                            <p style="color: #666666; font-size: 16px; font-weight: bold; text-decoration: underline;
                                text-align: center;">
                                Privacy Policy</p>
                            Skorkel values the privacy of its users. We are grateful for the immense trust put
                            in us by our users by providing their information to us online. Hence, we make the
                            protection of your privacy an utmost priority to ensure security and confidentiality
                            of any personal information provided. We assure you your information shall be used
                            only in ways that are compatible with this Privacy Policy.<br />
                            <br />
                            In this Privacy Policy, the words “you” and “your” refer to each registered user
                            of Skorkel and “we”, “us” and “our” refer to Skorkel.
                            <br />
                            <br />
                            <b>Information Collected from Users</b>
                            <br />
                            <br />
                            We receive and store any information you provide on our website or give us in any
                            other way. This includes information that can identify you (“personal information”),
                            including your e-mail address, user name and password. On various parts of Skorkel.com,
                            you may also choose to disclose more information about yourself such as educational
                            qualifications, professional experience, etc. Any comments, tags, bookmarks and
                            uploaded documents are also retained on our servers. If you choose to create a profile
                            on Skorkel, certain information in your profile will be publicly viewable and identifiable
                            by clicking your screen name. We also compile certain information about the visits
                            to skorkel.com. For example, we compile statistics that show the numbers and frequency
                            of visitors to the Web Site and its individual pages and time spent by each visitor
                            on each page.<br />
                            <br />
                            Please keep in mind that whenever you voluntarily disclose personal information
                            anywhere on Skorkel.com - for example through articles, blogs, case comments, discussion
                            lists, or elsewhere - that information can be collected and possibly used by other
                            registered members. If you post personal information online that is accessible to
                            other users, you may receive unsolicited messages from said users in return. Ultimately,
                            the responsibility to maintain the secrecy of your personal information rests with
                            you.
                            <br />
                            <br />
                            <b>Information from Third Party Sources</b>
                            <br />
                            <br />
                            In relation to users who registered on our website through Google+, we retain the
                            right to collect information such as your profile picture, username, email and password
                            and other information made available to us through those services in order to improve
                            and personalize your use of our website.
                            <br />
                            <br />
                            <b>Usage of Information and Analytics</b>
                            <br />
                            <br />
                            Skorkel uses your personal information for specific purposes only. The personal
                            information you provide to us when using the Web Site, such as your name and email
                            address will be kept confidential and used to support your relationship with Skorkel,
                            to notify you of updated information and relevant content from Skorkel, or information
                            from third parties that we think may be of interest to you. Skorkel retains the
                            right to enhance or merge your information collected at its Web Site with data from
                            third parties for purposes of marketing products or services to you.
                            <br />
                            <br />
                            For everyone who visits our website, we automatically collect some information about
                            the computer when you visit our website. This information includes inter alia session
                            data such as time spent, content viewed and pages visited; your IP address for geographical
                            data, web browser, device and referring website. We collect this information for
                            the purpose of making helium.travel a better experience for our visitors and enabling
                            customization to meet our visitors’ preferences and interests.
                            <br />
                            <br />
                            Aggregated statistics showing the numbers and frequency of visitors to our website
                            are generally used internally to improve the Web Site and for product development
                            and marketing purposes. We also retain the right to provide such data to advertisers
                            and other third parties, but the statistics shall not contain personal information
                            of any of our users.
                            <br />
                            <br />
                            <b>Cookies</b><br />
                            <br />
                            Cookies are small text files that are stored on the hard drive of the user’s computer
                            and are used for record-keeping purposes. The use of cookies is considered to be
                            an industry standard and makes web surfing easier by performing certain functions
                            such as saving your passwords and your personal preferences. If you choose to have
                            your browser refuse cookies, it is possible that some areas of our Site will not
                            function properly. Please note that when you visit parts of Skorkel.com where you
                            are prompted to log in or that are customizable, you may be required to accept cookies.
                            <br />
                            <br />
                            Advertisers and partners may also use cookies. We do not control use of these cookies
                            and disclaim responsibility for information collected through them. You can however
                            choose to accept or refuse the cookies by changing the settings of your browser.
                            You can reset your browser to refuse all cookies or allow your browser to show you
                            when a cookie is being sent. Please note that if use of cookies is disabled, certain
                            portions of Skorkel.com may not function as desired.
                            <br />
                            <br />
                            <b>External links</b>
                            <br />
                            <br />
                            You may find links on Skorkel.com that lead you to content on external websites.
                            Please not that such external websites are not governed by this privacy policy.
                            We suggest that you go through the privacy policy of these websites to understand
                            their processes for collecting and disseminating your personal information.
                            <br />
                            <br />
                            <b>Our Commitment to Data Security</b>
                            <br />
                            <br />
                            To prevent unauthorized access, maintain data accuracy, and ensure the correct use
                            of information, we have put in place appropriate electronic and managerial procedures
                            to safeguard and secure the information we collect online.
                            <br />
                            <br />
                            <b>Storing your data </b>
                            <br />
                            <br />
                            If you provide personal information, you acknowledge and agree that such personal
                            information may be transferred from your current location to the offices and servers
                            of Skorkel in India and any third parties servers authorized by Skorkel.
                            <br />
                            <br />
                            <b>Updates to this Privacy Policy</b><br />
                            <br />
                            Skorkel may update or amend this Privacy Policy at any time in the future. We will
                            notify our members about substantive changes to this Privacy Policy by sending a
                            notice to the e-mail address you provided to us.<br />
                            <br />
                            <b>Governing Law</b>
                            <br />
                            <br />
                            We collect, store and use your data in accordance with the applicable laws of India.
                            <br />
                            <br />
                            <b>Contact Us</b>
                            <br />
                            <br />
                            In case you have any questions or queries regarding this Privacy Policy, do not
                            hesitate to contact us on our “Contact Us” page or writing in to us at <a href="mailto:contact@skorkel.com"
                                style="font-size: 14px;">contact@skorkel.com</a> We assure you that your concerns
                            will be addressed at the earliest.
                        </div>
                        <div class="cls">
                        </div>
                        <!--left verticle search list ends-->
                    </div>
                </div>
            </div>
        </div>
        <div class="cls">
        </div>
        <!--footer starts-->
        <div class="outterContainer" id="footer">
            <div class="container">
                <div class="leftFooter">
                    <a href="AboutUs.aspx">About Us</a><br />
                    <a href="#">Skorkel Team</a>
                </div>
                <div class="leftFooter">
                    <a href="PrivacyPolicy.aspx">Privacy Policy</a><br />
                    <a href="TermsAndConditions.aspx">Terms of Service</a>
                </div>
                <div class="leftFooter">
                    <a href="#">Contact</a><br />
                    &copy; Skorkel.com
                </div>
                <div class="rightFooter">
                    <a href="https://www.facebook.com" target="_blank">
                        <img src="images/fb.png" /></a> <a href="https://www.twitter.com" target="_blank">
                            <img src="images/twitter.png" /></a> <a href="https://www.linkedin.com" target="_blank">
                                <img src="images/linked-in.png" /></a>
                </div>
            </div>
        </div>
        <div class="grayBoxSign" id="divWelcome" runat="server" style="display: none;">
            <div class="wlcBox">
                <img src="images/ltr.jpg" class="ltr" />
                <div class="cls">
                </div>
                <p class="wlc">
                    WELCOME TO SKORKEL!</p>
                <p>
                    A confirmation email has been sent to
                    <asp:Label ID="lblEmail" runat="server" Style="color: #3ebfc5"></asp:Label>. </br>
                    Please click on the confirmation link in the email to activate your account.</p>
            </div>
            <hr />
            <div class="wlcBox">
                <center>
                    <a href="Landing.aspx" id="aProceed" onclick="CallProceed()" class="nextBtn">Proceed</a></center>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
