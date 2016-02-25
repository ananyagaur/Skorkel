<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="forum-landing-page.aspx.cs"
    Inherits="forum_landing_page" %>
<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updtae" runat="server">
        <ContentTemplate>
            <div class="container" style="padding-top: 40px">
                <div class="cls">
                </div>
                <div class="innerDocumentContainerGroup" style="margin-top: 0px;">
                    <div id="divDeletesucess" clientidmode="Static" runat="server" class="EditProfilepopupHome"
                        style="display: none;">
                        <div id="divDeleteConfirm" runat="server" class="confirmDeletes" clientidmode="Static">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                <tr>
                                    <td>
                                        <b>
                                            <asp:Label ID="Label2" runat="server"></asp:Label>
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>&nbsp;&nbsp;
                                            <asp:Label ID="Label3" runat="server" Text="Do you want to Delete ?" Font-Size="Small"></asp:Label>
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
                                                    <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes"
                                                        CssClass="joinBtn" OnClick="lnkDeleteConfirm_Click"></asp:LinkButton>
                                                </td>
                                                <td style="padding-right: 20px;">
                                                    <asp:LinkButton ID="lnkDeleteCancel" runat="server" ClientIDMode="Static" Text="Cancel"
                                                        Style="float: left; text-align: center; text-decoration: none; width: 82px;
                                                        margin-top: -5px; color: #000; cursor: pointer;" OnClick="lnkDeleteCancel_Click"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="innerContainer">
                        <asp:UpdatePanel ID="upGroups" runat="server">
                            <ContentTemplate>
                                <!--groups top box starts-->
                                <Group:GroupDetails ID="grpDetails" runat="server" />
                                <!--groups top box ends-->
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!--left box starts-->
                        <div class="innerGroupBoxnew" style="width: 960px">
                            <div class="innerContainerLeft" style="width: 900px; border: none;">
                                <div class="tagContainer" style="width: 900px;">
                                    <div class="forumsTabs" style="height: 77px; margin: 2px 0px 0;">
                                        <ul>
                                            <li>
                                                <asp:LinkButton ID="lnkProfile" runat="server" Text="Profile" ClientIDMode="Static"
                                                    OnClick="lnkProfile_Click"></asp:LinkButton></li>
                                            <li id="DivHome" runat="server" style="display: block;">
                                                <div>
                                                    <asp:LinkButton ID="lnkHome" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li id="DivForumTab" runat="server" clientidmode="Static">
                                                <div>
                                                    <asp:LinkButton ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static"
                                                        class="forumstabAcitve" OnClick="lnkForumTab_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li id="DivUploadTab" runat="server" clientidmode="Static" style="display: block">
                                                <div>
                                                    <asp:LinkButton ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static"
                                                        OnClick="lnkUploadTab_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li id="DivPollTab" runat="server" clientidmode="Static" style="display: block">
                                                <div>
                                                    <asp:LinkButton ID="lnkPollTab" runat="server" Text="Polls" ClientIDMode="Static"
                                                        OnClick="lnkPollTab_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li id="DivEventTab" runat="server" clientidmode="Static" style="display: block">
                                                <div>
                                                    <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                                                        OnClick="lnkEventTab_Click"></asp:LinkButton></div>
                                            </li>
                                            <li id="DivMemberTab" runat="server" clientidmode="Static" style="display: block">
                                                <div>
                                                    <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                                        OnClick="lnkEventMemberTab_Click"></asp:LinkButton></div>
                                            </li>
                                        </ul>
                                        <div class="cls">
                                        </div>
                                        <asp:LinkButton ID="lnkcreateForum" Text="Create Forum" runat="server" ClientIDMode="Static"
                                            Style="margin-top: -10px; margin-right: 3px;" OnClick="lnkcreateForum_Click"
                                            CssClass="createForum"></asp:LinkButton>
                                    </div>
                                </div>
                                <div id="divheight" runat="server" style="width: 938px; margin-top: 56px; border: #e7e7e7 solid 1px;
                                    height: 99%;">
                                    <div id="divCancelPoll" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                                        width: 39%; z-index: 100; display: none;" clientidmode="Static">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                            <tr>
                                                <td>
                                                    <b>
                                                        <asp:Label ID="lbl" runat="server"></asp:Label>
                                                    </b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>&nbsp;&nbsp;
                                                        <asp:Label ID="lblConnDisconn" runat="server" Text="" Font-Size="Small"></asp:Label>
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
                                                                <asp:LinkButton ID="lnkConnDisconn" runat="server" ClientIDMode="Static" Text="Yes"
                                                                    CssClass="joinBtn" OnClick="lnkConnDisconn_Click"></asp:LinkButton>
                                                            </td>
                                                            <td style="padding-right: 20px;">
                                                                <a href="#" clientidmode="Static" causesvalidation="false" style="float: left; text-align: center;
                                                                    text-decoration: none; width: 82px; padding-top: 5px; padding-bottom: 8px; color: #000;"
                                                                    onclick="Cancel();">Cancel </a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="cls">
                                    </div>
                                    <div style="margin-top: 6%;">
                                        <asp:UpdatePanel ID="update" runat="server">
                                            <ContentTemplate>
                                                <asp:ListView ID="lstForumDetails" runat="server" OnItemDataBound="lstForumDetails_ItemDatabound"
                                                    OnItemCommand="lstForumDetails_ItemCommand">
                                                    <LayoutTemplate>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:HiddenField ID="hdnForumPostId" Value='<%#Eval("intForumPostingId") %>' ClientIDMode="Static"
                                                                    runat="server" />
                                                                <asp:HiddenField ID="hdnRegistrationId" Value='<%#Eval("intRegistrationId") %>' runat="server" />
                                                                <div class="cls">
                                                                </div>
                                                                <div class="forumBoxTxt" style="margin-top: 0%; margin-left: 30px;">
                                                                    <div style="margin-left: 0%;">
                                                                        <div class="">
                                                                            <div id="divshare" style="display: none" runat="server">
                                                                                <asp:LinkButton Style="color: #40bfc4; font-size: 14px; padding-left: 12px; text-decoration: none;"
                                                                                    ID="LinkButton1" Font-Underline="false" CommandName="Details" runat="server"
                                                                                    Text='<%#Eval("SharedName") %>'></asp:LinkButton>
                                                                                <br />
                                                                                <asp:Label ID="Label1" ForeColor="#40bfc4" Style="padding-left: 12px" runat="server"
                                                                                    Text="Shared"></asp:Label>&nbsp;
                                                                            </div>
                                                                            <p>
                                                                                <div class="breakwordTitle">
                                                                                    <asp:LinkButton ID="lnkTitle" Text='<%#Eval("strTitle") %>' CommandName="Forum" Style="color: #000000;
                                                                                        width: 900px; font-size: 26px; text-decoration: none !important;" runat="server"></asp:LinkButton>
                                                                                </div>
                                                                                <div class="forumsDate dateforum" style="float: right;">
                                                                                    <asp:Label ID="lblDate" Style="font-size: 14px;" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label></div>
                                                                            </p>
                                                                            <div class="breakwordTitle" style="margin-top: 0px;">
                                                                                <asp:Label ID="lblDescrption" Style="color: #9c9c9c; text-decoration: none; font-size: 16px;"
                                                                                    runat="server" Text='<%#Eval("strDescription") %>'></asp:Label>
                                                                            </div>
                                                                            <p class="replies" style="margin-top: 7px;">
                                                                                Replies :
                                                                                <asp:Label ID="lblreply" Style="font-size: 12px;" runat="server" Text=""></asp:Label>
                                                                            </p>
                                                                            <asp:UpdatePanel ID="updatedelet" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <div class="editDeleteMore" style="margin: 0px -3px 0 0;">
                                                                                    <span class="spEditForum">
                                                                                        <asp:LinkButton ID="lnkEdit" Font-Underline="false" Visible="false" class="edit"
                                                                                            ClientIDMode="Static" ToolTip="Edit" Text="Edit" CommandName="EditForum" CausesValidation="false"
                                                                                            runat="server">
                                                                                        </asp:LinkButton>
                                                                                        </span>
                                                                                        <span class="spDeleteForum">
                                                                                        <asp:LinkButton ID="lnkDelete" Font-Underline="false" Visible="false" class="edit"
                                                                                            ClientIDMode="Static" ToolTip="Delete" Text="Delete" CommandName="DeleteForum"
                                                                                            CausesValidation="false" runat="server">
                                                                                        </asp:LinkButton>
                                                                                        </span>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="lnkDelete" EventName="Click" />
                                                                                    <asp:AsyncPostBackTrigger ControlID="lnkEdit" EventName="Click" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="cls">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                                        <asp:HiddenField ID="hdnLoader" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnLoadernew" runat="server" ClientIDMode="Static" />
                                        <p id="pLoadMore" runat="server" align="center">
                                            <asp:ImageButton ID="imgLoadMore" runat="server" ClientIDMode="Static" ImageUrl="~/images/loadingIcon.gif"
                                                OnClick="imgLoadMore_OnClick" Height="100px" Width="100px" />
                                        </p>
                                        <p align="center">
                                            <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server"
                                                ClientIDMode="Static" ForeColor="Red" Visible="false"></asp:Label>.
                                        </p>
                                        <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
                                    </div>
                                </div>
                                <div class="cls">
                                    <p>
                                        &nbsp;</p>
                                </div>
                            </div>
                            <!--left box ends-->
                        </div>
                        <!--left verticle search list ends-->
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                var config = {
                    '.chosen-select': {},
                    '.chosen-select-deselect': { allow_single_deselect: true },
                    '.chosen-select-no-single': { disable_search_threshold: 10 },
                    '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                    '.chosen-select-width': { width: "95%" }
                }
                for (var selector in config) {
                    $(selector).chosen(config[selector]);
                }
                $(document).ready(function () {
                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                    prm.add_endRequest(function () {
                        var config = {
                            '.chosen-select': {},
                            '.chosen-select-deselect': { allow_single_deselect: true },
                            '.chosen-select-no-single': { disable_search_threshold: 10 },
                            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                            '.chosen-select-width': { width: "95%" }
                        }
                        for (var selector in config) {
                            $(selector).chosen(config[selector]);
                        }
                    });
                });
            </script>
            <script type="text/javascript">
                $(document).ready(function () {
                    var ID = "#" + $("#hdnLoader").val();
                    $(ID).focus();
                });
                $(document).ready(function () {
                    var ID = "#" + $("#hdnLoadernew").val();
                    $(ID).focus();
                });
                function Cancel() {
                    document.getElementById("divCancelPoll").style.display = 'none';
                    return false;
                }
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
            <asp:AsyncPostBackTrigger ControlID="lnkDeleteCancel" />
        </Triggers>
    </asp:UpdatePanel>
        <script type="text/javascript" language="javascript">
            $(document).ready(function () {
                $("#lblNoMoreRslt").css("display", "none");
            });
    </script>
    <script type="text/javascript">
        $(window).scroll(function () {
            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                var v = $("#lblNoMoreRslt").text();
                setTimeout(1500);
                var maxCount = $("#hdnMaxcount").val();
                if (maxCount <= 10) {
                    $("#lblNoMoreRslt").css("display", "none");
                } else {
                    if (v != "No more results available...") {
                        document.getElementById('<%= imgLoadMore.ClientID %>').click();
                    }
                }
            }
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("span.spEditForum").click(function () {
                $(this).children('#lnkEdit').css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $("span.spDeleteForum").click(function () {
                $(this).children('#lnkDelete').css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $('#lnkDeleteConfirm').click(function (e) {
                $(this).css("box-shadow", "0px 0px 5px #00B7E5");
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("span.spEditForum").click(function () {
                    $(this).children('#lnkEdit').css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $("span.spDeleteForum").click(function () {
                    $(this).children('#lnkDelete').css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $('#lnkDeleteConfirm').click(function (e) {
                    $(this).css("box-shadow", "0px 0px 5px #00B7E5");
                });
            });

            $('#divCancelPoll').center();
        });

    </script>
    <script type="text/javascript">
        function docdelete() {
            $('#divDeletesucess').css("display", "block");
        }
        function divCancels() {
            $('#divDeletesucess').css("display", "none");
        }
    </script>
</asp:Content>
