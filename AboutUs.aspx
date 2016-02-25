<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs" Inherits="AboutUs" %>

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
                    <div class="innerGroupBox" style="height: 500px;">
                        <div style="margin-top: 50px; font-family: Arial; font-size: 14px; font-weight: normal;padding: 0px 20px 20px 20px; line-height: 1.5em;
                            text-align: justify;">
                             <p style="color: #666666;font-size: 16px; font-weight:bold; text-decoration: underline; text-align: center; ">
                                About Us</p>
                            Skorkel is a product of Knowledge Cloud Private Limited and our core team is based
                            out of New Delhi, India. At Skorkel, our mission is to empower the research facilities
                            available with lawyers. We want to build a one true smart research engine that showcases
                            the most relevant results in the least possible time. We have a wonderful team working
                            in the background, improving and growing Skorkel every day to ensure that you only
                            have to go through research material that’s worth your time. If you have any feedback
                            or suggestions, do get into touch with us at <a href="mailto:support@skorkel.com" style="font-size:14px;">support@skorkel.com</a> .
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
