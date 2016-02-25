<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp1.aspx.cs" Inherits="SignUp1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" type="image/x-icon" href="logoSkorkel.ico" />
    <title>:: Skorkel ::</title>
    <link href="<%=ResolveUrl("css/style.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("css/stylever-2.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("Styles/MyStyle.css")%>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="<%=ResolveUrl("js/jquery-1.8.2.min.js")%>"></script>
    <script src="<%=ResolveUrl("css/placeholders.min.js")%>" type="text/javascript"></script>
    <style type="text/css">
        input[type=text]::-ms-clear
        {
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
</head>
<body>
    <!--[if IE 9]>  :root .somebox{height:100%  ;} <![endif]-->
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
                        <div class="innerSignUp" id="signup1">
                            <div class="signupDtl">
                                <span style="margin-left: -40px;">1</span><span style="margin-left: 56px; font-size: 21px;
                                    margin-top: 2px;"> Sign Up - Your Details</span>
                            </div>
                            <div class="dtlforUpdateProfile">
                                <span style="margin-left: -45px;">2</span><span style="margin-left: 0px; font-size: 21px;
                                    color: #97999b; margin-top: 0px;"> Details for an updated Skorkel Profile</span>
                            </div>
                            <div class="cls">
                            </div>
                            <div class="signupDtl dts rightImg">
                                <div class="signUp2Steps">
                                    <p class="two" style="padding-bottom: 6px;">
                                        Sign up in 2 steps</p>
                                    <p class="centerp">
                                        Or</p>
                                    <p>
                                        <asp:LinkButton ID="btnLinkedInLogin" runat="server" OnClientClick="javascript:btnLinkedInLogin_Click();return false;"> <img src="images/sign-linkedin.png" /></asp:LinkButton>
                                        <asp:Button ID="btnLinkdin" runat="server" ClientIDMode="Static" OnClick="btnLinkedInLogin_Click"
                                            Style="display: none;" />
                                    </p>
                                    <p>
                                        <asp:LinkButton ID="btnGoogleLogin" runat="server" OnClientClick="javascript:btnGoogleLogin_Click();return false;"
                                            CommandArgument="https://www.googleapis.com/plus/v1/people/me/openIdConnect"><img src="images/sign-google.png" /></asp:LinkButton>
                                        <asp:Button ID="btnGmail" runat="server" ClientIDMode="Static" OnClick="btnGoogleLogin_Click"
                                            Style="display: none;" />
                                    </p>
                                </div>
                            </div>
                            <div class="signupDtl dts grayBg">
                                <p>
                                    <asp:TextBox ID="txtFirstName" ClientIDMode="Static" runat="server" placeholder="First Name"
                                        class="signInput" Style="font-size: 15px;"></asp:TextBox>
                                </p>
                                <p>
                                    <asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name" class="signInput"
                                        Style="font-size: 15px;"></asp:TextBox>
                                </p>
                                <p>
                                    <asp:TextBox ID="txtUname" runat="server" placeholder="Email" class="signInput" Style="font-size: 15px;"></asp:TextBox>
                                </p>
                                <p>
                                    <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password"
                                        Style="font-size: 15px; border: none; width: 200px; color: #9c9c9c;" class="signInput"></asp:TextBox>
                                </p>
                                <p>
                                    <asp:TextBox ID="txtConPassword" runat="server" TextMode="Password" placeholder="Confirm Password"
                                        ClientIDMode="Static" Style="font-size: 15px; font-family: Arial; border: none;
                                        width: 200px; color: #9c9c9c;" class="signInput"></asp:TextBox>
                                </p>
                                <p style="text-align: center; font-family: Arial;">
                                    <asp:CheckBox ID="chkIAgree" runat="server" ClientIDMode="Static" Checked="false"
                                        Text="" />&nbsp;&nbsp;I agree to the <a href="TermsAndConditions.aspx" target="_blank">Terms & Conditions</a>
                                </p>
                                <p>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName"
                                        Display="Dynamic" ValidationGroup="Registration" ErrorMessage="First name is required."
                                        Style="font-size: 15px; width: 265px;" ForeColor="Red"></asp:RequiredFieldValidator></p>
                                <p>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLastName"
                                        Display="Dynamic" ValidationGroup="Registration" ErrorMessage="Last name is required."
                                        Style="font-size: 15px; width: 265px;" ForeColor="Red"></asp:RequiredFieldValidator></p>
                                <p>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUname"
                                        Style="font-size: 15px; width: 265px;" Display="Dynamic" ValidationGroup="Registration"
                                        ErrorMessage="Email id is required." ForeColor="Red"></asp:RequiredFieldValidator></p>
                                <p>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid email address."
                                        ControlToValidate="txtUname" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        Display="Dynamic" Style="font-size: 15px; width: 265px;" ValidationGroup="Registration">
                                    </asp:RegularExpressionValidator>
                                </p>
                                <p>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Style="font-size: 15px;
                                        width: 265px;" ControlToValidate="txtPassword" ErrorMessage="Password&nbsp;must&nbsp;be&nbsp;atleast&nbsp;6&nbsp;characters."
                                        Text="Password&nbsp;must&nbsp;be&nbsp;atleast&nbsp;6&nbsp;characters." ToolTip="New password must be atleast 6 characters."
                                        Display="Dynamic" ValidationExpression=".{6}.*" ValidationGroup="Registration" />
                                </p>
                                <p>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassword"
                                        Style="font-size: 15px; width: 265px;" Display="Dynamic" ValidationGroup="Registration"
                                        ErrorMessage="Password is required." ForeColor="Red"></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:CustomValidator ID="CustomValidator7" runat="server" ClientValidationFunction="validateTermAgree"
                                        Style="font-size: 15px; width: 265px;" CssClass="failureNotification" ErrorMessage="Confirm password is required."
                                        Text="Please check Terms & Conditions." ForeColor="Red" Display="Dynamic" ValidationGroup="Registration"></asp:CustomValidator>
                                </p>
                                <p>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Passwords do not match."
                                        Display="Dynamic" ControlToCompare="txtPassword" ControlToValidate="txtConPassword"
                                        Style="font-size: 15px; width: 265px;" ValidationGroup="Registration">
                                    </asp:CompareValidator>
                                </p>
                                <p>
                                    <asp:RegularExpressionValidator ID="Regex2" runat="server" ControlToValidate="txtPassword"
                                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{6,}$"
                                        Style="font-size: 15px; width: 450px; line-height: 0.9em; margin-top: 1px;" ErrorMessage="Password must be minimum 6 characters contain at least one special symbol and one digit."
                                        ForeColor="Red" ValidationGroup="Registration" />
                                </p>
                                <p>
                                    <asp:Label ID="lblMsgs" runat="server" Style="font-size: 15px; width: 265px; color: Red;"></asp:Label>
                                    &nbsp;</p>
                                <div class="cannextBtn">
                                    <a href="Landing.aspx" class="cancelBtn" id="aCancel" style="color: Black; font-family: Arial;
                                        border: 1px solid #bcbdc0; font-size: 15px;" onclick="callCancel();">Cancel</a>
                                    <asp:LinkButton runat="server" ID="lnlNext" Text="Next" ClientIDMode="Static" Style="font-family: Arial;
                                        font-size: 15px;" CssClass="nextBtn" ValidationGroup="Registration" OnClientClick="javascript:CallFunc();return false;"></asp:LinkButton>
                                    <asp:Button runat="server" ID="Btnnext" Text="Next" ClientIDMode="Static" Style="font-family: Arial;
                                        display: none; font-size: 15px;" CssClass="nextBtn" ValidationGroup="Registration"
                                        OnClick="lnlNext_Click"></asp:Button>
                                </div>
                                <p>
                                    &nbsp;</p>
                            </div>
                        </div>
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
                <a href="TermsAndConditions.aspx">Privacy Policy</a><br />
                <a href="#">Terms of Service</a>
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
    <!--footer ends-->
    </form>
</body>
<head>
    <script type="text/javascript" language="javascript">
        function validateTermAgree(source, args) {
            args.IsValid = true;
            var CuuEmail = $("#chkIAgree").val();
            if ($("#chkIAgree").is(':checked'))
                args.IsValid = true; // checked
            else
                args.IsValid = false;  // unchecked
        }
        function InvalidEmailID() {
            alert("Email id already exist.");
        }
    </script>
    <script type="text/javascript">
        function ValidateCheckBox(sender, args) {
            if (document.getElementById("<%=chkIAgree.ClientID %>").checked == true) {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }       
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtUname").keydown(function () {
                $("#lblMsgs").text('');
            });
            $('#lnlNext').on('click', function () {
                if ($.browser.msie) {
                    $('input').each(function () {
                        var theAttribute = $(this).data('placeholder');
                        if (theAttribute == this.value) {// check placeholder and value
                            $(this).val('');
                            alert('Please enter value - IE');
                        }
                    });
                }
            });

        });
        function CallFunc() {
            $('#lnlNext').css("background", "#00A5AA");
            $('#lnlNext').css("border", "2px solid #3EBFE5");
            if ($('#txtFirstName').text() == '' || $('#txtLastName').text() == '' || $('#txtUname').text() == ''
            || $('#txtPassword').text() == '' || $('#txtConPassword').text() == '') {
                setTimeout(
                function () {
                    $('#lnlNext').css("background", "#0096a1");
                    $('#lnlNext').css("border", "none");
                }, 1000);
            }
            if ($("#chkIAgree").is(':checked'))
            { }
            else {   // unchecked
                setTimeout(
                function () {
                    $('#lnlNext').css("background", "#0096a1");
                    $('#lnlNext').css("border", "none");
                }, 1000);
            }

            document.getElementById("Btnnext").click();
        }
        function callCancel() {
            $('#aCancel').css("border", "2px solid #BCBDCE");
        }
        function btnLinkedInLogin_Click() {
            document.getElementById("btnLinkdin").click();
        }
        function btnGoogleLogin_Click() {
            document.getElementById("btnGmail").click();
        }
        $(document).ready(function () {
            var ie_version = getIEVersion();
            var is_ie9 = ie_version.major == 9;
            if (is_ie9 == true) {
                document.getElementById("CompareValidator1").style.display = "none";
                document.getElementById("CompareValidator1").style.color = "#E0E2E2";
                $("#txtConPassword").click(function () {
                    document.getElementById("CompareValidator1").style.display = "none";
                    document.getElementById("CompareValidator1").style.color = "Red";
                });
            }
        });
        function getIEVersion() {
            var agent = navigator.userAgent;
            var reg = /MSIE\s?(\d+)(?:\.(\d+))?/i;
            var matches = agent.match(reg);
            if (matches != null) {
                return { major: matches[1], minor: matches[2] };
            }
            return { major: "-1", minor: "-1" };
        }

    </script>
</head>
</html>
<script>
    $("#commnetBtn1").click(function () {
        $("#commentTxt1").slideToggle("slow");
        $("#commnetBtn1 img").toggle();

    });
    $("#commnetBtn2").click(function () {
        $("#commentTxt2").slideToggle("slow");
        $("#commnetBtn2 img").toggle();

    });
</script>
