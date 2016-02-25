<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp2.aspx.cs" Inherits="SignUp2"
    MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <asp:UpdatePanel ID="upanel" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
        <ContentTemplate>
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
                                <div class="innerSignUp" id="signup2">
                                    <div class="signupDtl">
                                        <span style="margin-left: -40px;">1</span><span style="margin-left: 40px; font-size: 19px;
                                            margin-top: 2px;"> Sign Up - Your Details</span>
                                    </div>
                                    <div class="dtlforUpdateProfile">
                                        <span style="margin-left: -45px;">2</span><span style="margin-left: 0px; font-size: 19px;
                                            margin-top: 2px;"> Details for an updated Skorkel Profile</span>
                                    </div>
                                    <div class="cls">
                                    </div>
                                    <div class="signupDtl dts rightImg">
                                        <div class="signUp2Steps">
                                            <p class="two" style="padding-bottom: 6px;">
                                                Sign up in 2 steps</p>
                                        </div>
                                    </div>
                                    <div class="signupDtl dts grayBg">
                                        <p>
                                            <asp:TextBox ID="txtInstituteName" runat="server" placeholder="Institute name" class="signInput"
                                                Style="font-size: 15px;"></asp:TextBox>
                                            <ajax:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtInstituteName"
                                                ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                            </ajax:AutoCompleteExtender>
                                        </p>
                                        <p>
                                            <asp:TextBox ID="txtDegree" runat="server" placeholder="Degree" class="signInput"
                                                Style="font-size: 15px;"></asp:TextBox>
                                            <ajax:AutoCompleteExtender ServiceMethod="GetDegreeList" MinimumPrefixLength="1"
                                                CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtDegree"
                                                ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                            </ajax:AutoCompleteExtender>
                                        </p>
                                        <asp:UpdatePanel ID="updates" runat="server">
                                            <ContentTemplate>
                                                <p>
                                                    <select name="select" id="fromMonth" runat="server" style="font-size: 15px;">
                                                        <option>Month</option>
                                                        <option>Jan</option>
                                                        <option>Feb</option>
                                                        <option>Mar</option>
                                                        <option>Apr</option>
                                                        <option>May</option>
                                                        <option>Jun</option>
                                                        <option>Jul</option>
                                                        <option>Aug</option>
                                                        <option>Sep</option>
                                                        <option>Oct</option>
                                                        <option>Nov</option>
                                                        <option>Dec</option>
                                                    </select>
                                                    <asp:DropDownList ID="txtFromYear" runat="server" CssClass="signInputY" ClientIDMode="Static">
                                                    </asp:DropDownList>
                                                    <span class="spanDash">- </span>
                                                    <asp:DropDownList ID="toMonth" runat="server" ClientIDMode="Static">
                                                        <asp:ListItem Selected="True">Month</asp:ListItem>
                                                        <asp:ListItem>Jan</asp:ListItem>
                                                        <asp:ListItem>Feb</asp:ListItem>
                                                        <asp:ListItem>Mar</asp:ListItem>
                                                        <asp:ListItem>Apr</asp:ListItem>
                                                        <asp:ListItem>May</asp:ListItem>
                                                        <asp:ListItem>Jun</asp:ListItem>
                                                        <asp:ListItem>Jul</asp:ListItem>
                                                        <asp:ListItem>Aug</asp:ListItem>
                                                        <asp:ListItem>Sep</asp:ListItem>
                                                        <asp:ListItem>Oct</asp:ListItem>
                                                        <asp:ListItem>Nov</asp:ListItem>
                                                        <asp:ListItem>Dec</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="txtToYear" runat="server" CssClass="signInputY" ClientIDMode="Static">
                                                    </asp:DropDownList>
                                                </p>
                                                <p class="currentlyStd">
                                                    <asp:CheckBox ID="chkAtPresent" ClientIDMode="Static" AutoPostBack="true" onclick="changeCheckboxText(this);"
                                                        runat="server" />
                                                    Currently studying
                                                    <asp:Button ID="BtnCheckbox" ClientIDMode="Static" Style="display: none" OnClick="chkAtPresent_CheckedChanged"
                                                        runat="server" />
                                                </p>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="BtnCheckbox" />
                                                <asp:AsyncPostBackTrigger ControlID="chkAtPresent" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <p>
                                            <textarea class="txtArea" id="txtDescription" runat="server" placeholder="Description"
                                                style="font-size: 15px; color: #9c9c9c;"></textarea></p>
                                        <p>
                                            <asp:TextBox ID="txtAreaExpert" runat="server" placeholder="Add your area of expertise"
                                                class="signInput" ClientIDMode="Static"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAreaExpert"
                                                Display="Dynamic" ValidationGroup="Regist" ErrorMessage="Area of expertise is required."
                                                Style="font-size: 15px; margin-top: 2px;" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:Button ID="btnAreaExpSave" runat="server" ClientIDMode="Static" Style="display: none;"
                                                Text="Add" OnClick="btnAreaExpSave_Click" ValidationGroup="Regist" />
                                        </p>
                                        <div style="margin: 0px 0px 7px 10px;">
                                            <asp:ListView ID="lstAreaExpert" runat="server" OnItemCommand="lstAreaExpert_ItemCommand"
                                                GroupItemCount="3" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">
                                                <GroupTemplate>
                                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                </GroupTemplate>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnintUserSkillId" runat="server" Value='<%#Eval("intUserSkillId")%>' />
                                                    <div>
                                                        <span class="firstTxt">
                                                            <%#Eval("strSkillName")%>
                                                        </span><span class="secondTxt">
                                                            <asp:ImageButton ID="lnkDelete" runat="server" CommandName="DeleteExp" ImageUrl="images/close.png">
                                                            </asp:ImageButton>
                                                        </span>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                        <div class="cls">
                                            &nbsp;</div>
                                        <div class="cannextBtn" style="margin-top: 5px;">
                                            <asp:LinkButton runat="server" ID="lnkSkipbtn" Text="Skip" CssClass="cancelBtn" ValidationGroup="Registration"
                                                ClientIDMode="Static" OnClientClick="javascript:lnlSkip_Click();return false;"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton runat="server" ID="lnlNext" Text="Finish" CssClass="nextBtn" ValidationGroup="Registration"
                                                Style="margin-left: 2px;" ClientIDMode="Static" OnClientClick="javascript:lnlNext_Click();return false;"></asp:LinkButton>
                                            <asp:Button runat="server" ID="BtnSkip" Text="Skip" Style="display: none;" ValidationGroup="Registration"
                                                ClientIDMode="Static" OnClick="lnlSkip_Click"></asp:Button>&nbsp;
                                            <asp:Button runat="server" ID="BtnFinish" Text="Finish" Style="display: none;" ValidationGroup="Registration"
                                                ClientIDMode="Static" OnClick="lnlNext_Click"></asp:Button>
                                        </div>
                                        <div class="cannextBtn">
                                            <asp:LinkButton runat="server" ID="lnkSkipbtn2" Text="Skip" CssClass="cancelBtn"
                                                ValidationGroup="Registration" Visible="false" OnClientClick="return false"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton runat="server" ID="lnlNext2" Text="Finish" CssClass="nextBtn" ValidationGroup="Registration"
                                                Visible="false" OnClientClick="return false"></asp:LinkButton>
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
            <asp:UpdateProgress ID="updateProgress" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                        right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/loadingImage.gif"
                            AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; margin-top: 19%;"
                            class="divProgress" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAreaExpSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lstAreaExpert" />
            <asp:AsyncPostBackTrigger ControlID="txtAreaExpert" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
<head>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtFromYear').find('option[value=2016], option[value=2025]').remove();
            $('#txtToYear').find('option[value=2025]').remove();
            $("#txtAreaExpert").keydown(function (e) {
                if (e.keyCode == 13) {
                    debugger;
                    document.getElementById("btnAreaExpSave").click();
                    return false;
                }
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#txtFromYear').find('option[value=2016], option[value=2025]').remove();
                $('#txtToYear').find('option[value=2025]').remove();
                var v = "";
                $("#txtAreaExpert").keydown(function (e) {
                    if (e.keyCode == 13) {
                        document.getElementById("btnAreaExpSave").click();
                        return false;
                    }
                });
            });
        });
    </script>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(beginRequest);
        function beginRequest() {
            prm._scrollPosition = null;
        }
    </script>
    <script type="text/javascript">
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler() {
            yPos = $get('#upanel').scrollTop;
            alert(yPos);
        }
        function EndRequestHandler() {
            $get('#upanel').scrollTop = yPos;
        }
        function fnAddElement() {
            BeginRequestHandler();
            EndRequestHandler();
        }
        function callClose() {
            $('#Button1').click();
        }

        function lnlSkip_Click() {
            $('#lnkSkipbtn').css("box-shadow", "0px 0px 10px #BCBDCE");
            $('#lnlNext').css("border", "1px solid #3EBFE5");
            document.getElementById("BtnSkip").click();
        }
        function lnlNext_Click() {
            $('#lnkSkipbtn').css("border", "1px solid #BCBDCE");
            $('#lnlNext').css("background", "#00A5AA");
            $('#lnlNext').css("box-shadow", "0px 0px 2px #00B7E5");
            document.getElementById("BtnFinish").click();
        }
        function changeCheckboxText(checkbox) {
            if (checkbox.checked)
                document.getElementById("BtnCheckbox").click();
            else
                document.getElementById("BtnCheckbox").click();
        }
        function CallProceed() {
            $('#aProceed').css("background", "#00A5AA");
            $('#aProceed').css("box-shadow", "0px 0px 2px #00B7E5");
        }
    </script>
</head>
</html>
