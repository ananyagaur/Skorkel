<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="forum-detail.aspx.cs"
    Inherits="forum_detail" %>

<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
    <style type="text/css">
        a
        {
            color: #0254EB;
        }
        a:visited
        {
            color: #0254EB;
        }
        #header h2
        {
            color: white;
            background-color: #00A1E6;
            margin: 0px;
            padding: 5px;
        }
        .comment
        {
            width: 660px;
            margin: 10px;
        }
        a.morelink
        {
            text-decoration: none;
            outline: none;
            color: #40BFC4;
        }
        .morecontent span
        {
            display: none;
        }
        .breakword
        {
            word-break: break-all;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnrply" runat="server" ClientIDMode="Static" />
    <div class="container" style="padding-top: 25px">
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
                                                CssClass="joinBtn" OnClick="lnkDeleteConfirm_Click" OnClientClick="divCancels();"></asp:LinkButton>
                                        </td>
                                        <td style="padding-right: 20px;">
                                            <asp:LinkButton ID="lnkDeleteCancel" runat="server" ClientIDMode="Static" Text="Cancel"
                                                Style="float: left; text-align: center; text-decoration: none; width: 82px;
                                                margin-top: -5px; color: #000; cursor: pointer;" OnClick="lnkDeleteCancel_Click"
                                                OnClientClick="divCancels();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <asp:UpdatePanel ID="up1" runat="server">
                <ContentTemplate>
                    <div class="innerContainer">
                        <!--groups top box starts-->
                        <Group:GroupDetails ID="grpDetails" runat="server" />
                        <!--groups top box ends-->
                        <div class="clsFooter" style="border: 0">
                        </div>
                        <!--left box starts-->
                        <div class="innerGroupBoxnew" style="margin-top: -30px;">
                            <div class="innerContainerLeft" style="width: 900px">
                                <div class="tagContainer">
                                    <div class="forumsTabs" style="margin-top: -15px; height: 75px;">
                                        <ul>
                                            <li>
                                                <asp:LinkButton ID="lnkProfile" runat="server" Text="Profile" ClientIDMode="Static"
                                                    OnClick="lnkProfile_Click"></asp:LinkButton></li>
                                            <li>
                                                <div id="DivHome" runat="server" style="display: block;">
                                                    <asp:LinkButton ID="lnkHome" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li>
                                                <div id="DivForumTab" runat="server" clientidmode="Static">
                                                    <asp:LinkButton ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static"
                                                        class="forumstabAcitve" OnClick="lnkForumTab_Click"></asp:LinkButton></div>
                                            </li>
                                            <li>
                                                <div id="DivUploadTab" runat="server" clientidmode="Static" style="display: block">
                                                    <asp:LinkButton ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static"
                                                        OnClick="lnkUploadTab_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li>
                                                <div id="DivPollTab" runat="server" clientidmode="Static" style="display: block">
                                                    <asp:LinkButton ID="lnkPollTab" runat="server" Text="Poll" ClientIDMode="Static"
                                                        OnClick="lnkPollTab_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li>
                                                <div id="DivEventTab" runat="server" clientidmode="Static" style="display: block">
                                                    <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                                                        OnClick="lnkEventTab_Click"></asp:LinkButton></div>
                                            </li>
                                            <li>
                                                <div id="DivMemberTab" runat="server" clientidmode="Static" style="display: block">
                                                    <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                                        OnClick="lnkEventMemberTab_Click"></asp:LinkButton></div>
                                            </li>
                                        </ul>
                                        <div class="cls">
                                        </div>
                                        <div style="margin-left: 0px;">
                                            <img src="images/recentBlogs.png" align="absmiddle" style="margin-right: 8px; margin-left: -7%;
                                                margin-top: -8px;" />
                                            <asp:LinkButton ID="lnkAllForum" runat="server" class="recentPoll" OnClick="lnkAllForum_Click"
                                                Style="margin-top: -6px;" Text="Back"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkcreateForum" Text="Create Forum" runat="server" ClientIDMode="Static"
                                                Style="margin-top: -8px; margin-right: 45px;" OnClick="lnkcreateForum_Click"
                                                CssClass="createForum"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div style="height: 99%; width: 938px; margin-top: 39px; border: #c6c8ca solid 1px;">
                                    <div class="cls">
                                        <br />
                                    </div>
                                    <div class="cls">
                                    </div>
                                    <div class="pollBox forumArea" style="margin-top: 25px; float: none;">
                                        <div class="pollArea">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:ListView ID="lstForumDetails" runat="server" OnItemCommand="lstForumDetails_ItemCommand"
                                                        OnItemDataBound="lstForumDetails_ItemDatabound">
                                                        <LayoutTemplate>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr id="itemPlaceholder" runat="server">
                                                                </tr>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:HiddenField ID="hdnForumPostId" Value='<%#Eval("intForumPostingId")%>' ClientIDMode="Static"
                                                                        runat="server" />
                                                                    <asp:HiddenField ID="hdnRegid" Value='<%#Eval("intRegistrationId")%>' ClientIDMode="Static"
                                                                        runat="server" />
                                                                    <div class="cls">
                                                                    </div>
                                                                    <div class="forumListN">
                                                                        <div class="forumBoxTxt" style="width: 865px; border: none;">
                                                                            <p>
                                                                                <span style="display: none;">
                                                                                    <asp:LinkButton CommandName="Details" ID="Label1" Style="color: #40bfc4; font-size: 14px;
                                                                                        text-decoration: none;" runat="server" Text='<%#Eval("Name") %>'></asp:LinkButton>
                                                                                    .</span>
                                                                                <div class="breakword">
                                                                                    <asp:LinkButton ID="lnkTitle" CssClass="forumsTitle" Style="color: #666666; text-decoration: none!important;"
                                                                                        Enabled="false" Text='<%#Eval("strTitle")%>' CommandName="Forum" runat="server"></asp:LinkButton>
                                                                                </div>
                                                                                <div style="text-align: right; margin-left: 653px;" class="posted">
                                                                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label>
                                                                                </div>
                                                                                <br />
                                                                            </p>
                                                                            <p>
                                                                                <div class="breakword">
                                                                                    <asp:LinkButton ID="LinkButton1" Style="color: #9c9c9c; text-decoration: none!important;"
                                                                                        Enabled="false" Text='<%#Eval("strDescription") %>' CommandName="Forum" runat="server"></asp:LinkButton>
                                                                                </div>
                                                                            </p>
                                                                            <div class="editDeleteMore">
                                                                                <asp:UpdatePanel ID="up3" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:LinkButton ID="lnkEdit1" Font-Underline="false" Visible="false" class="edit"
                                                                                            ClientIDMode="Static" ToolTip="Edit" Text="Edit" CommandName="Edit forum" CausesValidation="false"
                                                                                            runat="server"> </asp:LinkButton>
                                                                                        <asp:LinkButton ID="lnkDelete1" Font-Underline="false" Visible="false" class="edit"
                                                                                            ClientIDMode="Static" ToolTip="Delete" Text="Delete" CommandName="Delete forum"
                                                                                            CausesValidation="false" runat="server" OnClientClick="docdelete();"> </asp:LinkButton>
                                                                                        <br />
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:AsyncPostBackTrigger ControlID="lnkDelete1" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>
                                                                        <div style="margin-top: 1%; margin-left: 10px; width: 800px; display: none;" class="forumLike">
                                                                            <div style="text-align: left;">
                                                                                <img src="images/like-gray.png" />
                                                                                <asp:Label ID="lblTotallike" runat="server" Text=""></asp:Label>
                                                                                .
                                                                                <asp:LinkButton ID="btnLike" Style="text-decoration: none; color: #666666;" Text="Like"
                                                                                    runat="server" CommandName="LikeForum" ToolTip="Like"></asp:LinkButton>
                                                                            </div>
                                                                            <div style="text-align: left; margin-left: 75px; margin-top: -18px;">
                                                                                <img src="images/reply-gray.png" />
                                                                                <asp:Label ID="lblreply" runat="server" Text=""></asp:Label>.
                                                                                <asp:LinkButton ID="btnReply" Style="text-decoration: none; color: #666666;" Text="Reply"
                                                                                    runat="server" CommandName="ReplyPost" ToolTip="Add Comment"></asp:LinkButton>
                                                                            </div>
                                                                            <div style="text-align: left; margin-left: 150px; margin-top: -18px;">
                                                                                <img src="images/share-gray.png" />
                                                                                <asp:Label ID="lblShare" runat="server" Text=""></asp:Label>
                                                                                .
                                                                                <asp:LinkButton ID="btnShare" Style="text-decoration: none; color: #666666;" Text="Share"
                                                                                    runat="server" CommandName="ShareForum" ToolTip="Share"></asp:LinkButton></div>
                                                                        </div>
                                                                        <b>
                                                                            <asp:Label ID="lbShareTitle" Visible="false" runat="server" Text='<%#Eval("shareTitle") %>'></asp:Label>
                                                                            <asp:Label ID="lbShareDesc" Visible="false" runat="server" Text='<%#Eval("shareDesc") %>'></asp:Label>
                                                                    </div>
                                                                    <div class="cls">
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="lstForumDetails" EventName="ItemCommand" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="bgLine" style="width: 100%; margin-left: 4px;" id="Div1">
                                            &nbsp;</div>
                                        <div class="cls">
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div class="voteCommentsTxt" style="margin-top: 7px;">
                                                    <asp:Panel ID="pnlReply" runat="server">
                                                        <div style="width: 850px; padding: 0px 0px 0px 20px;">
                                                            <div style="margin-left: 0px;">
                                                                <div class="voteCommentsTxt">
                                                                    <asp:TextBox ID="txtPostForum" placeholder="Reply" runat="server" CssClass="eventTitleField commentA"
                                                                        ValidationGroup="txt" Style="border: 1px solid #e7e7e7;" ClientIDMode="Static"></asp:TextBox>
                                                                    <p class="commentspost">
                                                                        <asp:LinkButton ID="lnkPostForum" CssClass="viewAllTxt" runat="server" Text="Post"
                                                                            Style="margin: 0px;" ClientIDMode="Static" ValidationGroup="txt" OnClick="lnkPostForum_Click"></asp:LinkButton>
                                                                    </p>
                                                                    <div style="display:none;">
                                                                    <asp:Button ID="btnSAve" runat="server" ClientIDMode="Static" OnClick="lnkPostForum_Click" />
                                                                    </div>
                                                                </div>
                                                                <div class="fieldTxt" style="margin: 10px 0px 0px 20px;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPostForum"
                                                                        Display="Dynamic" ValidationGroup="txt" ErrorMessage="Please enter reply."
                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Style="font-size: 15px;" Text="Please enter comment."
                                                                        Visible="false"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <div class="forumList" style="border-bottom: none; margin-top: 20px;">
                                                    <asp:ListView ID="lstReplyForum" runat="server" OnItemCommand="lstReplyForum_ItemCommand"
                                                        OnItemDataBound="lstReplyForum_ItemDatabound">
                                                        <LayoutTemplate>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr id="itemPlaceholder" runat="server">
                                                                </tr>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:HiddenField ID="hdnForumPostId" Value='<%#Eval("intForumReplyLikeShareId")%>'
                                                                        ClientIDMode="Static" runat="server" />
                                                                    <asp:HiddenField ID="hdnRegid" Value='<%#Eval("intRegistrationId")%>' ClientIDMode="Static"
                                                                        runat="server" />
                                                                    <div class="forumList" style="border-bottom: 1px solid #bcbcbb; margin-top: 10px;
                                                                        width: 103%; margin-left: 44px;">
                                                                        <div class="thumbnail" style="width: 45px;">
                                                                            <img id="imgprofile" runat="server" style="width: 46px; height: 52px; float: left"
                                                                                src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' /></div>
                                                                        <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath")%>' />
                                                                        <div class="searchListTxt" style="width: 800px;">
                                                                            <p>
                                                                                <span>
                                                                                    <asp:LinkButton Font-Underline="false" CommandName="Details" ID="Label1" CssClass="forumsTitle"
                                                                                        Style="color: #666666; font-size: 20px;" runat="server" Text='<%#Eval("Name") %>'></asp:LinkButton>
                                                                                    <div style="margin-left: 700px; margin-top: -15px;" class="posted">
                                                                                        <asp:Label ID="lblPostedDate" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label>
                                                                                    </div>
                                                                                </span>
                                                                                <br />
                                                                                <div style="margin-left: 10px; margin-top: 12px;">
                                                                                    <asp:Label ID="lblReplyComment" CssClass="comment more" Style="color: #9c9c9c; font-size: 16px;
                                                                                        line-height: 16px; margin: 0;" runat="server" Text='<%#Eval("strComment") %>'></asp:Label>
                                                                                </div>
                                                                            </p>
                                                                            <div class="editDeleteMore" style="margin-right: 30px;">
                                                                                <asp:UpdatePanel ID="up2" runat="server">
                                                                                    <ContentTemplate>
                                                                                    <span class="spEditForum">
                                                                                        <asp:LinkButton ID="lnkEdit" Font-Underline="false" Visible="false" class="edit"
                                                                                            ClientIDMode="Static" ToolTip="Edit" Text="Edit" CommandName="Edits" CausesValidation="false"
                                                                                            runat="server"> </asp:LinkButton>
                                                                                            </span>
                                                                                        <span class="spDeleteForum">
                                                                                        <asp:LinkButton ID="lnkDelete" Font-Underline="false" Visible="false" class="edit"
                                                                                            ClientIDMode="Static" ToolTip="Delete" Text="Delete" CommandName="Deletes" CausesValidation="false"
                                                                                            runat="server" OnClientClick=" docdelete(); ">
                                                                                        </asp:LinkButton>
                                                                                        </span>
                                                                                        <br />
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:AsyncPostBackTrigger ControlID="lnkEdit" />
                                                                                        <asp:AsyncPostBackTrigger ControlID="lnkDelete" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                            <div style="margin-left: 10px; display: none;" class="forumLike">
                                                                                <img src="images/like-gray.png" />
                                                                                <asp:Label ID="lblTotallike" runat="server" Text=""></asp:Label>.
                                                                                <asp:LinkButton ID="btnLike" Style="text-decoration: none; color: #666666;" Text="Like"
                                                                                    runat="server" CommandName="LikeForum" ToolTip="Like"></asp:LinkButton></div>
                                                                        </div>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="cls">
                                        </div>
                                        <div class="cls">
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                            <!--left box ends-->
                        </div>
                        <!--left verticle search list ends-->
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkPostForum" />
                    <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
                    <asp:AsyncPostBackTrigger ControlID="lnkDeleteCancel" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="hdnForumrRefresh" runat="server" ClientIDMode="Static" />
        <script type="text/javascript">
            $(document).ready(function () {
                var showChar = 150;
                var ellipsestext = "...";
                var moretext = "More";
                var lesstext = "Less";
                $('.more').each(function () {
                    var content = $(this).html();
                    if (content.length > showChar) {
                        var c = content.substr(0, showChar);
                        var h = content.substr(showChar - 1, content.length - showChar);
                        var html = c + '<span class="moreelipses">' + ellipsestext + '</span>&nbsp;<span class="morecontent"><span>' + h + '</span>&nbsp;&nbsp;<a href="" class="morelink" style="float: right;">' + moretext + '</a></span>';
                        $(this).html(html);
                    }
                });
                $(".morelink").click(function () {
                    if ($(this).hasClass("less")) {
                        $(this).removeClass("less");
                        $(this).html(moretext);
                    } else {
                        $(this).addClass("less");
                        $(this).html(lesstext);
                    }
                    $(this).parent().prev().toggle();
                    $(this).prev().toggle();
                    return false;
                });
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                    $('.more').each(function () {
                        var content = $(this).html();
                        if (content.length > showChar) {
                            var c = content.substr(0, showChar);
                            var h = content.substr(showChar - 1, content.length - showChar);
                            var html = c + '<span class="moreelipses">' + ellipsestext + '</span>&nbsp;<span class="morecontent"><span>' + h + '</span>&nbsp;&nbsp;<a href="" class="morelink" style="float: right;">' + moretext + '</a></span>';
                            $(this).html(html);
                        }
                    });
                    $(".morelink").click(function () {
                        if ($(this).hasClass("less")) {
                            $(this).removeClass("less");
                            $(this).html(moretext);
                        } else {
                            $(this).addClass("less");
                            $(this).html(lesstext);
                        }
                        $(this).parent().prev().toggle();
                        $(this).prev().toggle();
                        return false;
                    });
                });
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
                $("#txtPostForum").keypress(function (e) {
                    if (e.keyCode == 13) {
                        $('#btnSAve').click();
                        return false; 
                    }
                });
                $('#lnkDeleteConfirm').click(function (e) {
                    $(this).css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $('#lnkDelete1').click(function (e) {
                    $(this).css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $('#lnkEdit1').click(function (e) {
                    $(this).css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $("span.spEditForum").click(function () {
                    $(this).children('#lnkEdit').css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $("span.spDeleteForum").click(function () {
                    $(this).children('#lnkDelete').css("box-shadow", "0px 0px 5px #00B7E5");
                });

                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                    $("#txtPostForum").keydown(function (e) {
                        if (e.keyCode == 13) {
                            $('#btnSAve').click();
                            return false; 
                        }
                    });
                    $('#lnkDeleteConfirm').click(function (e) {
                        $(this).css("box-shadow", "0px 0px 5px #00B7E5");
                    });
                    $('#lnkDelete1').click(function (e) {
                        $(this).css("box-shadow", "0px 0px 5px #00B7E5");
                    });
                    $('#lnkEdit1').click(function (e) {
                        $(this).css("box-shadow", "0px 0px 5px #00B7E5");
                    });
                    $("span.spEditForum").click(function () {
                        $(this).children('#lnkEdit').css("box-shadow", "0px 0px 5px #00B7E5");
                    });
                    $("span.spDeleteForum").click(function () {
                        $(this).children('#lnkDelete').css("box-shadow", "0px 0px 5px #00B7E5");
                    });

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
    </div>
</asp:Content>
