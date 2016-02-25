<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Notifications_Details_2.aspx.cs" Inherits="Notifications_Details_2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--heading ends-->
    <div class="cls">
    </div>
    <!--inner container starts-->
    <div class="cls">
    </div>
    <!--inner container ends-->
    <div class="innerContainer" style="background: #fff; float: left">
        <!--middle container starts-->
        <div class="NmiddleContainer">
            <asp:UpdatePanel ID="upnotification" runat="server">
                <ContentTemplate>
                    <div id="divSuccess" runat="server" style="border: 20px solid rgba(0,0,0,0.5); width: 40%;
                        display: none; position: fixed; margin: 8% 0% 0% 12%" clientidmode="Static">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                            <tr>
                                <td>
                                    <strong>&nbsp;&nbsp;
                                        <asp:Label ID="lblSuccess" runat="server" Text="Do you want to Decline?" Font-Size="Small"></asp:Label>
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td class="popBgLineGray">
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <table width="100" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnkCancels" CssClass="message" CausesValidation="false" Style="background: url(images/Nmessage-btn.png) no-repeat scroll 0 0 transparent;
                                                    color: #FFFFFF; height: 21px; margin: 0 72px -24px 0; padding: 3px 0 0 3px; text-align: center;
                                                    text-decoration: none; width: 60px; text-decoration: none; float: right" runat="server"
                                                    Text="Ok" OnClick="lnkCancels_Click"></asp:LinkButton>
                                                <a href="#" clientidmode="Static" causesvalidation="false" style="float: left; text-align: center;
                                                    text-decoration: none; width: 82px; margin-left: 50px; padding-top: 5px; color: #000;"
                                                    onclick="javascript:Cancel();return false;">Close </a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divheight" runat="server">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        <asp:ListView ID="lstMainMyTag" runat="server" OnItemDataBound="lstMainMyTag_ItemDataBound"
                            OnItemCreated="lstMainMyTag_ItemCreated">
                            <ItemTemplate>
                                <div class="progessList" style="width: 890px;">
                                    <div class="workList">
                                        <span class="subheading">
                                            <asp:Label ID="lblAddedOn" Text='<%#Eval("dtAddedOn") %>' runat="server"></asp:Label></span>
                                        <div style="height: 10px;">
                                        </div>
                                    </div>
                                </div>
                                <asp:ListView ID="lstChildMyTag" runat="server" OnItemCommand="lstChildMyTag_ItemCommand"
                                    OnItemDeleting="lstChildMyTag_ItemDeleting" DataKeyNames="Id">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnPkId" Value='<%# Eval("Id") %>' runat="server" />
                                        <asp:HiddenField ID="hdnRegID" Value='<%# Eval("intRegistrationId") %>' runat="server" />
                                        <asp:HiddenField ID="hdnintUserTypeId" runat="server" Value='<%#Eval("intUserTypeId") %>' />
                                        <asp:HiddenField ID="hdnrequserid" Value='<%# Eval("intInvitedUserId") %>' runat="server" />
                                        <asp:HiddenField ID="hdnTableName" Value='<%# Eval("strTableName") %>' runat="server" />
                                        <asp:HiddenField ID="hdnShareInvitee" Value='<%# Eval("strInvitee") %>' runat="server" />
                                        <asp:HiddenField ID="hdnStrRecommendation" Value='<%# Eval("StrRecommendation") %>'
                                            runat="server" />
                                        <asp:HiddenField ID="hdnIsAccept" Value='<%# Eval("IsAccept") %>' runat="server" />
                                        <asp:HiddenField ID="hdnintIsAccept" Value='<%# Eval("intIsAccept") %>' runat="server" />
                                        <div id="SearchRept" runat="server">
                                            <p>
                                                <asp:UpdatePanel ID="updele" runat="server">
                                                    <ContentTemplate>
                                                        <div style="width: 90%;" class="breakallwords">
                                                            <asp:Label ID="lnkName" Text='<%# Eval("Name") %>' Font-Bold="true" CommandName="Details"
                                                                runat="server"></asp:Label>
                                                            <asp:Label ID="lblnotificationname" runat="server"></asp:Label>&nbsp;
                                                            <asp:Label ID="lblComment" Text="" Font-Bold="true" Visible="false" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="lnkShareDetail" CommandName="Details" Style="color: #00B6BD;
                                                                font-size: 14px; text-decoration: none; margin-left: 0px;" runat="server"></asp:LinkButton>
                                                            &nbsp;
                                                            <asp:Label ID="lblTime" Text='<%# Eval("AddedTime") %>' Style="color: Gray; font-size: 11px;
                                                                text-decoration: none;" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="lnkCancel" CssClass="message" CommandName="Delete" CausesValidation="false"
                                                                Style="background: url(images/Nmessage-btn.png) no-repeat scroll 0 0 transparent;
                                                                color: #FFFFFF; height: 21px; margin: 0 -68px 0 0; padding: 4px 0 0 8px; text-align: left;
                                                                text-decoration: none; width: 52px; text-decoration: none; float: right" runat="server"
                                                                Text="Decline" OnClientClick="javascript:OpenPopup(); "></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkConfirm" CssClass="message" CommandName="Confirm" CausesValidation="false"
                                                                Style="background: url(images/Nmessage-btn.png) no-repeat scroll 0 0 transparent;
                                                                color: #FFFFFF; height: 21px; margin: 0 0 0 0; padding: 4px 20px 0 10px; text-align: left;
                                                                text-decoration: none; width: 43px; text-decoration: none; float: right" runat="server"
                                                                Text="Accept"></asp:LinkButton>
                                                            <asp:LinkButton Visible="false" ID="lnkConnected" OnClientClick="return false;" CausesValidation="false"
                                                                Style="color: #40CF8F; float: right; height: 25px; margin: 0px -45px 0 0; text-align: center;
                                                                text-decoration: none; width: 82px; text-decoration: none; border: none;" runat="server"
                                                                Text="Accepted"></asp:LinkButton>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkCancel" />
                                                        <asp:AsyncPostBackTrigger ControlID="lnkConfirm" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <div>
                                                    <asp:Label ID="lblstrMessage" Text='<%# Eval("strMessage") %>' CommandName="Details"
                                                        runat="server"></asp:Label>
                                                    <br />
                                                    <asp:LinkButton ID="lnkstrLink" Text='<%# Eval("strLink") %>' CommandName="Details"
                                                        Style="color: #00B6BD; font-size: 12px; text-decoration: none; display: none;"
                                                        runat="server"></asp:LinkButton>
                                                </div>
                                            </p>
                                        </div>
                                        <div style="display: none;">
                                            <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("vchrUserName") %>'></asp:Label>
                                            <asp:Label ID="lblUserType" runat="server" Text='<%#Eval("intUserTypeID") %>'></asp:Label>
                                            <asp:Label ID="lblGroupName" Font-Italic="true" runat="server" Text='<%#Eval("strGroupName") %>'></asp:Label>
                                        </div>
                                        <!--comments box ends-->
                                        <!--tag ends-->
                                    </ItemTemplate>
                                </asp:ListView>
                                <div class="bgLine">
                                    <img src="images/spacer.gif" height="2" width="500" /></div>
                            </ItemTemplate>
                        </asp:ListView>
                         <p id="pLoadMore" runat="server" align="center">
                            <asp:ImageButton ID="imgLoadMore" runat="server" ClientIDMode="Static" ImageUrl="~/images/loadingIcon.gif"
                                OnClick="imgLoadMore_OnClick" style="display:none;" />
                        </p>
                        <p align="center">
                            <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server"
                                ClientIDMode="Static" ForeColor="Red" Visible="false"></asp:Label>
                        </p>
                    </div>
                    <asp:UpdateProgress id="updateProgress" runat="server">
                            <ProgressTemplate>
                              <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999;  opacity: 0.7;">
                                 <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/Loadgif.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;margin-top:20%;" class="divProgress" />
                              </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkCancels" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <!--middle container ends-->
    </div>
    <!--pagination starts-->
    <!--pagination ends-->
     <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
    <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
    <div class="cls">
    </div>
    <script type="text/javascript">
            $(document).ready(function () {
                $('#divSuccess').center();
            });
    </script>
    <script type="text/jscript">
        function Cancel() {
            document.getElementById("divSuccess").style.display = 'none';
            return true;
        }
        function Submit() {
            document.getElementById("divSuccess").style.display = 'none';
            return true;
        }
        function OpenPopup() {
            return true;
        }
    </script>
    <script type="text/javascript">
            $(document).ready(function () {
                $(window).scroll(function () {
                    if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                        $("#imgLoadMore").css("display", "block");
                        $(".divProgress").css("display", "none");
                        var v = $("#lblNoMoreRslt").text();
                        var maxCount = $("#hdnMaxcount").val();
                        if (maxCount <= 10) {
                            $("#lblNoMoreRslt").css("display", "none");
                            $("#imgLoadMore").css("display", "none");
                        } else {
                            if (v != "No more results available...") {
                                document.getElementById('<%= imgLoadMore.ClientID %>').click();
                            }
                        }
                    }
                });
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                    $(window).scroll(function () {
                        if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                            $("#imgLoadMore").css("display", "block");
                            $(".divProgress").css("display", "none");
                            var v = $("#lblNoMoreRslt").text();
                            var maxCount = $("#hdnMaxcount").val();
                            if (maxCount <= 10) {
                                $("#lblNoMoreRslt").css("display", "none");
                                $("#imgLoadMore").css("display", "none");
                            } else {
                                if (v != "No more results available...") {
                                    document.getElementById('<%= imgLoadMore.ClientID %>').click();
                                }
                            }
                        }
                    });
                });
            });
    </script>
</asp:Content>
