<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Reset-Password.aspx.cs" Inherits="Reset_Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <link href="<%=ResolveUrl("css/stylever-2.css")%>" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            if ($("#lblNotifyCount").text() == '0') {
                document.getElementById("divNotification1").style.display = "none";
            }
            if ($("#lblInboxCount").text() == '0') {
                document.getElementById("divInbox").style.display = "none";
            } 
        });   
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#lnkResetPassword').focus();
            $("#txtConfPassword").keydown(function (e) {
                if (e.keyCode == 13) {
                    $('#btnsave').focus();
                    $('#btnsave').trigger('click');
                }
            });
            $("#txtConfPassword").css('margin-top', '-15');    //margin-top: -15px;
            $("#txtNewPassword").focusout(function () {
                if ($('#ctl00_ContentPlaceHolder1_Regex2').is(":hidden")) {
                    $("#txtConfPassword").css('margin-top', '-15');
                } else {
                    $("#txtConfPassword").css('margin-top', '0');
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="cls">
        </div>
        <div class="innerDocumentContainer">
            <div class="innerContainer">
                <div class="leftDocumentPanelNew">
                    <!--left container starts-->
                    <div style="margin-top: 35px;" class="leftContainer">
                        <img src="images/cloud-image.png" style="margin-left: 120px;" alt="cloud" />
                    </div>
                    <!--left container ends-->
                    <!--right container starts-->
                    <div style="margin-top: 35px;" class="rightContainer">
                        <div class="loginContainer">
                            <%-- <asp:UpdatePanel ID="ipreset" runat="server"><ContentTemplate> --%>
                                <strong style="font-size: medium">Change&nbsp;your&nbsp;password.</strong>
                            </div>
                            <div style="background: url(images/line-bg.png) repeat-x; float: left; margin: -15px 0 10px; display:none;">
                                <img src="images/spacer.gif" height="2" width="320" />
                            </div>
                            <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" placeholder="Old password"
                                ClientIDMode="Static" CssClass="loginPasswordField"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ClientIDMode="Static"
                                ValidationGroup="Login1" Display="Dynamic" ToolTip="Old password required." CssClass="errorTxt"
                                ErrorMessage="Please&nbsp;enter&nbsp;valid&nbsp;old&nbsp;password." ControlToValidate="txtOldPassword"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" placeholder="New password"
                                ClientIDMode="Static" MaxLength="15" Style="margin-top: 13px;" CssClass="loginPasswordField"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Login1"
                                Display="Dynamic" ToolTip="New password required." CssClass="errorTxt" ErrorMessage="Please&nbsp;enter&nbsp;valid&nbsp;new&nbsp;password."
                                ControlToValidate="txtNewPassword"></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="Regex2" runat="server" ControlToValidate="txtNewPassword" Font-Bold="false"
                                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{6,}$" Font-Size="Small" 
                                    ErrorMessage="Password must be minimum 6 characters contain at least one special symbol and one digit." 
                                    ForeColor="Red" ValidationGroup="Login1" />
                            <asp:TextBox ID="txtConfPassword" runat="server" TextMode="Password"  placeholder="Confirm new password"
                                ClientIDMode="Static" MaxLength="15" Style="margin-top: -15px;" CssClass="loginPasswordField"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Login1"
                                Display="Dynamic" ToolTip="Confirem new password required." CssClass="errorTxt"
                                ErrorMessage="Please&nbsp;enter&nbsp;valid&nbsp;confirm&nbsp;new&nbsp;password."
                                ControlToValidate="txtConfPassword"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ValidationGroup="Login1" ID="CompareValidator1" runat="server"
                                ControlToCompare="txtNewPassword" ClientIDMode="Static" ControlToValidate="txtConfPassword"
                                Display="Dynamic" ErrorMessage="Password&nbsp;does&nbsp;not&nbsp;matched"></asp:CompareValidator><br />
                            <asp:Label ID="lblMessage" runat="server"></asp:Label><br />
                            <asp:Label ID="lblPwdLenMsg" ClientIDMode="Static" ForeColor="Red" runat="server"></asp:Label>
                            <p style="font-size: smaller">
                                <strong>Hint: </strong>Passwords are case-sensitive and must be at least 6 characters.
                                A good password should contain a mix of capital and lower-case letters, numbers
                                and symbols.
                            </p>
                            <p style="font-size: smaller">
                            </p>
                            <p style="font-size: smaller">
                            </p>
                            <div class="cls">
                            </div>
                            <div class="btn" style="width: 195px;">
                                <asp:LinkButton ID="lnkResetPassword" Text="Reset Password" Font-Bold="false" runat="server"
                                    ClientIDMode="Static" OnClick="lnkResetPassword_Click" ValidationGroup="Login1"
                                    class="resetPasswords"></asp:LinkButton>
                                <div style="display: block;">
                                    <asp:Button ID="btnsave" Text="" Font-Bold="true" runat="server" Style="margin-top: 0px;
                                        background: #EBEBEB; border: none; margin-left: 3px; cursor: pointer;" ClientIDMode="Static"
                                        OnClick="lnkResetPassword_Click" ValidationGroup="Login1" />
                                </div>
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
                </div>
            </div>
            <!--left verticle search list ends-->
        </div>
    </div>
</asp:Content>
