<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Landing.aspx.cs" Inherits="Landing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="shortcut icon" type="image/x-icon" href="logoSkorkel.ico" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
    <meta http-equiv="Content-Script-Type" content="text/javascript" />
    <title>:: Skorkel ::</title>
    <link href="<%=ResolveUrl("css/style.css")%>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=ResolveUrl("js/jquery-1.8.2.min.js")%>"></script>
    <script src="<%=ResolveUrl("js/jquery.iosslider.min.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("js/hex_md5.js")%>" type="text/javascript"></script>
</head>
<body>
    <form id="form2" runat="server">
    <asp:HiddenField ID="hdnEncpass" ClientIDMode="Static" runat="server" Value="" />
    <div class="topHeaderContainer landingPage">
        <div class="innerContainer">
            <div class="logo">
                <img src="images/logo-home.png" alt="Skorkel" /></div>
            <div class="navigationInner">
                <ul class="landingMenu">
                    <li><a href="SignUp1.aspx" style="font-family: Arial;">SIGN UP</a></li>
                    <li>
                        <asp:LinkButton ID="lnklogin" Text="LOGIN" runat="server" Style="font-family: Arial;"
                            ClientIDMode="Static" OnClientClick="javascript:lnklog();return false;"></asp:LinkButton>
                    </li>
                </ul>
            </div>
        </div>
        <div class="cls">
        </div>
        <!-- slider starts-->
        <div class="sliderContainer">
            <div class='iosSlider' style="width: 100%">
                <div class='slider' style="width: 100%">
                    <div class='item' id='item1'>
                    </div>
                    <div class='item' id='item2'>
                    </div>
                    <div class='item' id='item3'>
                    </div>
                    <div class='item' id='item4'>
                    </div>
                </div>
                <div class='iosSliderButtons'>
                    <div class="dots">
                        <div class='button'>
                        </div>
                        <div class='button'>
                        </div>
                        <div class='button'>
                        </div>
                        <div class='button'>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--slider ends-->
    </div>
    <div class="cls">
    </div>
    <div class="fullWidth" id="fullWidth">
        <div class="container">
            <div class="boxHome" style="text-align: justify;">
                <p class="heading">
                    Research</p>
                Skorkel has been designed from the ground up to present you with the most relevant
                results. Every user interaction feeds into our machine-learning algorithm to ensure
                that research can be done in a smart and efficient way. Further, research is made
                easier with a suite of tools that help you document your thoughts as you go through
                a case law.
            </div>
            <div class="boxHome" style="text-align: justify;">
                <p class="heading">
                    Collaborate</p>
                Skorkel provides you with an expansive set of tools to ensure that you can harness
                the power of your network to the fullest. You can use Skorkel for running your own
                blog, polling in everyone’s views, starting Q&As, setting up events, building toping
                specific forums, and almost any activity that requires true collaboration between
                friends or teams.
            </div>
            <div class="boxHome" style="text-align: justify;">
                <p class="heading">
                    Opportunities</p>
                Researching on Skorkel will never be futile. Build up points by using Skorkel for
                research and at the end of semester compete with your peers to get internships and
                interviews at some of India’s top law firms.
            </div>
        </div>
    </div>
    <div class="cls">
    </div>
    <div class="cls">
    </div>
    <!--footer starts-->
    <div class="outterContainer" id="footer">
        <div class="container">
            <div class="logo footerLogo">
                <img src="images/logo-home-footer.png" alt="Skorkel" /></div>
            <div class="rightFooter">
                <a href="https://www.facebook.com" target="_blank" class="facebook"></a><a href="https://www.linkedin.com"
                    target="_blank" class="linkedIn"></a><a href="https://www.twitter.com" target="_blank"
                        class="twitter"></a>
            </div>
            <div class="leftFooter">
                <a href="AboutUs.aspx">About Us</a><br />
                <a href="#">Contact</a>
            </div>
            <div class="leftFooter">
                <a href="#">Skorkel Team</a><br />
                <a href="PrivacyPolicy.aspx">Privacy Policy</a>
            </div>
            <div class="leftFooter" style="display: none">
                <a href="TermsAndConditions.aspx">Terms of Service</a><br />
                &copy; Skorkel.com
            </div>
        </div>
    </div>
    <!--footer ends-->
    <!--login pop up starts-->
    <div class="popupLoginSocialOuterBox" id="divLogin" runat="server" clientidmode="Static"
        style="display: none">
        <div class="popupLoginSocialInerBox">
            <div class="popupLoginSocialInerBoxF">
                <p class="closePopIconH">
                    <asp:LinkButton ID="lnkCloseBtn" runat="server" ClientIDMode="Static" OnClientClick="javascript:lnkCloseBtns();return false;">
                        <img src="images/close-pop.jpg" alt="" /></asp:LinkButton></p>
                <p class="logoPopH">
                    <img src="images/logo-home.png" alt="" /></p>
                <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate" BorderStyle="None"
                    RememberMeSet="false" Width="100%">
                    <LayoutTemplate>
                        <p>
                            <asp:TextBox ID="UserName" placeholder="USERNAME" ClientIDMode="Static" runat="server"
                                CssClass="popInpTxtH"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ValidationGroup="Login1"
                                Display="Dynamic" ToolTip="Please enter valid user name." CssClass="errorTxt"
                                ErrorMessage="Please enter valid user name." ControlToValidate="UserName">
                            </asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:TextBox ID="Password" runat="server" TextMode="Password" ClientIDMode="Static"
                                placeholder="PASSWORD" CssClass="popInpTxtH"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ValidationGroup="Login1"
                                Display="Dynamic" ToolTip="Password is required." CssClass="errorTxt" ErrorMessage="Please enter valid password."
                                ControlToValidate="Password"></asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label Style="color: red; float: left;" ID="FailureText" runat="server" EnableViewState="False"></asp:Label>
                        </p>
                        <div class="cls">
                        </div>
                        <p class="rememberMeH">
                            <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me" Checked="false" />
                        </p>
                        <p class="forgotPassH">
                            <a href="ForgetPassword.aspx">Forgot password?</a></p>
                        <div class="cls">
                        </div>
                        <p class="loginHomeTxtLink">
                            <%-- <asp:LinkButton ID="button1" runat="server" ValidationGroup="Login1" ClientIDMode="Static"
                                Text="LOGIN" OnClientClick="javascript:LoginButton_Click();"></asp:LinkButton>--%>
                            <input type="button" class="submitButton" id="button" onclick="LoginButton_Click();"
                                value="LOGIN" validationgroup="Login1" font-bold="True" tabindex="2" style="background: #e62925;
                                color: #fff; text-decoration: none !important; width: 100%; text-align: center;
                                float: left; padding: 6px 0; font-size: 16px;" />
                        </p>
                    </LayoutTemplate>
                </asp:Login>
                <asp:Button ID="btnLogins" runat="server" ValidationGroup="Login1" CommandName="Login"
                    ClientIDMode="Static" Style="display: none;" Text="LOGIN" OnClick="LoginButton_Click">
                </asp:Button>
                <div class="cls">
                </div>
                <p class="signLinkGoogle">
                    <asp:ImageButton ID="btnLinkedInLogin" ImageUrl="images/signin-linkedin.jpg" runat="server"
                        OnCommand="btnLinkedInLogin_Click" />
                    <asp:ImageButton ID="btnGoogleLogin" ImageUrl="images/signin-google.jpg" runat="server"
                        OnCommand="btnGoogleLogin_Click" CommandArgument="https://www.google.com/accounts/o8/id" />
                </p>
                <div class="cls">
                </div>
                <p class="doyouAcc">
                    Do not have an account? <a href="SignUp1.aspx">Sign Up Now</a></p>
                <div class="cls">
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnInFirstName" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnInLastName" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnInEmailId" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnInimgProfile" runat="server" ClientIDMode="Static" />
    <!--login pop up ends-->
    </form>
    <script language="javascript" type="text/javascript">
        function getPass() {
            var pass = $('#Password').val();
            calc(pass);
        }
        function OnSuccess(response, userContext, methodName) {
            calc(response);
        }
        function replaceAll(find, replace, str) {
            return str.replace(new RegExp(find, 'g'), replace);
        }
        function calc(strTxt1) {
            var strTxt = strTxt1;
            if (strTxt.length == 0) {
                document.getElementById('hdnEncpass').value = "";
                return;
            }
            if (strTxt.search("\r") > 0) strTxt = replaceAll("\r", "", strTxt);
            var strHash = hex_md5(strTxt);
            strHash = strHash.toUpperCase();
            document.getElementById('hdnEncpass').value = strHash;
            $('#Password').val(strHash);
            document.getElementById("btnLogins").click();
        }
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('.iosSlider').iosSlider({
                scrollbar: true,
                snapToChildren: true,
                desktopClickDrag: true,
                autoSlideTransTimer: 100,
                scrollbarLocation: 'top',
                scrollbarMargin: '10px 10px 0 10px',
                scrollbarBorderRadius: '0',
                responsiveSlideWidth: true,
                navSlideSelector: $('.iosSliderButtons .button'),
                infiniteSlider: true,
                startAtSlide: '1',
                onSlideChange: slideContentChange,
                onSlideComplete: slideContentComplete,
                onSliderLoaded: slideContentLoaded,
                autoSlide: true
            });
            function slideContentChange(args) {
                /* indicator */
                $(args.sliderObject).parent().find('.iosSliderButtons .button').removeClass('selected');
                $(args.sliderObject).parent().find('.iosSliderButtons .button:eq(' + args.currentSlideNumber + ')').addClass('selected');
            }
            function slideContentComplete(args) {
                /* animation */
                $(args.sliderObject).find('.text1, .text2').attr('style', '');

                $(args.currentSlideObject).children('.text1').animate({
                    right: '100px',
                    opacity: '1'
                }, 100, 'easeOutQuint');

                $(args.currentSlideObject).children('.text2').delay(100).animate({
                    right: '50px',
                    opacity: '1'
                }, 100, 'easeOutQuint');
            }
            function slideContentLoaded(args) {
                /* animation */
                $(args.sliderObject).find('.text1, .text2').attr('style', '');
                $(args.currentSlideObject).children('.text1').animate({
                    right: '100px',
                    opacity: '1'
                }, 100, 'easeOutQuint');

                $(args.currentSlideObject).children('.text2').delay(100).animate({
                    right: '50px',
                    opacity: '1'
                }, 100, 'easeOutQuint');
                /* indicator */
                $(args.sliderObject).parent().find('.iosSliderButtons .button').removeClass('selected');
                $(args.sliderObject).parent().find('.iosSliderButtons .button:eq(' + args.currentSlideNumber + ')').addClass('selected');
            }
        });
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#UserName").keypress(function (e) {
                if (e.keyCode == 13)
                    return false;
            });
            $("#Password").keypress(function (e) {
                if (e.keyCode == 13) {
                    document.getElementById("button").click(); return false;
                }
            });
        });
        function lnklog() {
            $("#divLogin").css("display", "block");
        }
        function lnkCloseBtns() {
            $("#divLogin").css("display", "none");
        }
        function LoginButton_Click() {
            $('#button').css("box-shadow", "0px 0px 5px #BCBDCE");
            $("#button").css("background", "#EB5451");
            getPass();
        }
    </script>
</body>
</html>
