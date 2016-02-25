<%@ Page Title="" Language="C#" MasterPageFile="~/BeforeLogin.master" AutoEventWireup="true"
    CodeFile="ForgetPassword.aspx.cs" Inherits="ForgetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headBefore" runat="Server">
    <link href="Styles/MyStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="js/jquery-1.8.2.min.js"></script>
    <script src="js/WaterMark.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            if (document.getElementById("UserName").value == "")
                $("[id*=UserName]").WaterMark();

            //To change the color of Watermark
            $("[id*=Email]").WaterMark(
            {
                WaterMarkTextColor: 'Black'
            });
        });
    </script>
    <link href="css/stylever-2.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBefore" runat="Server">
    <div class="cls">
    </div>
    <br />
    <!--heading starts-->
    <div class="heading">
        <div class="container">
            <div class="">
                Forgot Password
            </div>
        </div>
    </div>
    <!--heading ends-->
    <div class="cls">
    </div>
    <!--left container starts-->
    <div style="margin-top: 35px;" class="leftContainer">
        <img src="images/cloud-image.png" style="margin-left: 100px;" alt="cloud" />
    </div>
    <!--left container ends-->
    <!--right container starts-->
    <div style="margin-top: 35px;" class="rightContainer">
        <div class="loginContainer">
            <div class="documentHeading" style="font-size: 20pt">
                <strong>Forgot your Password?</strong>
            </div>
            <div style="background: url(images/line-bg.png) repeat-x; float: left; margin: -15px 0 10px;">
                <img src="images/spacer.gif" height="2" width="320" />
            </div>
            <p id="pMsg" runat="server" style="font-size: 12pt; display: none">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </p>
            <p id="ptext1" runat="server" style="font-size: 12pt; display: none">
                Please enter your username or email address.</p>
            <p id="ptext2" runat="server" style="font-size: 12pt; display: none">
                You will receive a link to create a new password via email</p>
            <br />
            <asp:TextBox ID="UserName" ToolTip="Enter your email address" autocomplete="off"
                ClientIDMode="Static" runat="server" CssClass="loginField"></asp:TextBox>
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            <div class="cls">
            </div>
            <div class="btn">
                <asp:LinkButton ID="lnkGetPassword" Text="Continue" runat="server" ClientIDMode="Static"
                    Style="font-size: 16px; margin-left: 25px;" OnClick="lnkGetPassword_Click" class="createGroup"></asp:LinkButton>
            </div>
            <div style="background: url(images/line-bg.png) repeat-x; float: left; margin: 5px 0 5px;">
                <img src="images/spacer.gif" height="2" width="320" />
            </div>
            <div class="signup">
                <a style="font-size: 12pt; color: Gray; font-weight: bold" href='<%= ResolveUrl("Landing.aspx") %>'>
                    Back to login</a>
            </div>
            <div class="cls">
            </div>
        </div>
    </div>
    <div class="cls">
    </div>
    <!--right container ends-->
    <div id="profiles">
    </div>
</asp:Content>
