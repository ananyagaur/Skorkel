<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="Polls-Details.aspx.cs" Inherits="Polls_Details" %>

<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("js/PopupCenter.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="uppolldetails" UpdateMode="Conditional" ClientIDMode="Static"
        runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="cls">
                </div>
                <div class="innerDocumentContainerGroup" style="margin-top: 0px;">
                <div id="divDeletesucess" clientidmode="Static" runat="server" class="EditProfilepopupHome" style="display: none;">
                     <div id="divDeleteConfirm" runat="server" class="confirmDeletes" clientidmode="Static">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                            <tr>
                                <td>
                                    <b>
                                        <asp:Label ID="Label3" runat="server"></asp:Label>
                                    </b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>&nbsp;&nbsp;
                                        <asp:Label ID="Label4" runat="server" Text="Do you want to Delete ?" Font-Size="Small"></asp:Label>
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
                                                    CssClass="joinBtn" OnClick="lnkDeleteConfirm_Click" ></asp:LinkButton>
                                            </td>
                                            <td style="padding-right: 20px;">
                                                    <asp:LinkButton ID="lnkDeleteCancel" runat="server" ClientIDMode="Static" Text="Cancel"
                                                    style="float: left; text-align: center;text-decoration: none; width: 82px;   margin-top: -5px; color: #000;
                                                    cursor: pointer;" OnClick="lnkDeleteCancel_Click" ></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    </div>
                    <div class="innerContainer">
                        <!--groups top box starts-->
                         <asp:UpdatePanel ID="updatedoc" runat="server" UpdateMode="Conditional"> 
                                <ContentTemplate>
                 <Group:GroupDetails ID="GroupDetails1" runat="server" />
                </ContentTemplate></asp:UpdatePanel>
                        <!--groups top box ends-->
                        <!--left box starts-->
                        <div class="innerGroupBoxnew">
                            <div class="innerContainerLeft" style="width: 900px; padding-left: 20px;">
                                <div class="tagContainer" style="width: 900px">
                                    <div class="forumsTabs" style="margin:2px -19px 0px;height: 75px;">
                                        <ul style="width:885px;">
                                            <li>
                                                <asp:LinkButton ID="lnkProfile" runat="server" Text="Profile" ClientIDMode="Static"
                                                    OnClick="lnkProfile_Click"></asp:LinkButton></li>
                                            <li id="DivHome" runat="server" style="display: block;">
                                                <div>
                                                    <asp:LinkButton ID="lnkHome" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li id="DivForumTab" runat="server" clientidmode="Static" style="display: block">
                                                <div>
                                                    <asp:LinkButton ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static"
                                                        OnClick="lnkForumTab_Click"></asp:LinkButton></div>
                                            </li>
                                            <li id="DivUploadTab" runat="server" clientidmode="Static" style="display: block">
                                                <div>
                                                    <asp:LinkButton ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static"
                                                        OnClick="lnkUploadTab_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li id="DivPollTab" runat="server" clientidmode="Static">
                                                <div>
                                                    <asp:LinkButton ID="lnkPollTab" runat="server" Text="Polls" ClientIDMode="Static"
                                                        class="forumstabAcitve" OnClick="lnkPollTab_Click"></asp:LinkButton>
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
                                        <br />
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Recent" ClientIDMode="Static" 
                                            Style="margin: -13px 7px 0px 38px;;" CssClass="recentPoll recentActive" OnClick="chkRecent_CheckedChanged"></asp:LinkButton>
                                        <span class="pipeBlack" style="margin-top: -11px;">|</span>
                                        <asp:LinkButton ID="LinkButton2" runat="server" Text="Active" ClientIDMode="Static" 
                                            Style="margin:-12px 0px 0px 10px;" CssClass="recentPoll" OnClick="chkActive_CheckedChanged"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkcreatePoll" Text="Create Poll" runat="server" ClientIDMode="Static" 
                                            Style="margin:-11px 10px 0px 0px;" OnClick="lnkcreatePoll_Click" CssClass="createForum"></asp:LinkButton>
                                        <div class="cls">
                                        </div>
                                    </div>
                                </div>
                                <div id="divheight" runat="server" style="height: 99%; margin-top: 56px; border: #e7e7e7 solid 1px;
                                    width: 938px; margin-left: -20px;">
                                    <div class="cls">
                                        <br />
                                    </div>
                                    <div id="divSuccessPolls" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                                        float: left; width: 500px; padding-top: 0px; position: relative; margin: -200px 0 0 50px;
                                        z-index: 100; display: none;" clientidmode="Static">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                            <tr>
                                                <td>
                                                    <strong>&nbsp;&nbsp;
                                                        <asp:Label ID="lblSuccess" runat="server" Text="Poll deleted successfully." Font-Size="Small"></asp:Label>
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
                                                                <a href="#" clientidmode="Static" causesvalidation="false" style="float: left; text-align: center;
                                                                    text-decoration: none; width: 82px; padding-top: 5px; color: #000;" onclick="javascript:messageClosePoll();">
                                                                    Close </a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divCancelPoll" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                                        float: left; width: 500px; padding-top: 0px; position: fixed; margin: -200px 0 0 150px;
                                        z-index: 100; display: none;" clientidmode="Static">
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
                                                                <a clientidmode="Static" causesvalidation="false" style="float: left; text-align: center;
                                                                    text-decoration: none ! important; width: 82px; padding-top: 5px; padding-bottom: 8px;
                                                                    color: #000; cursor: pointer;" onclick="Cancel();">Cancel </a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="cls">
                                    </div>
                                    <div class="searchGroup" style="display: none">
                                        <asp:TextBox ID="txtsearch" AutoCompleteType="Disabled" AutoPostBack="true" runat="server"
                                            ClientIDMode="Static" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                                        <ajax:TextBoxWatermarkExtender TargetControlID="txtsearch" ID="txtwatermarkz" runat="server"
                                            WatermarkText="Search title">
                                        </ajax:TextBoxWatermarkExtender>
                                    </div>
                                    <div class="cls">
                                        <br />
                                    </div>
                                    <div class="cls">
                                    </div>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                    <div style="margin-top: 16px;">
                                        <asp:ListView ID="lstPollsDetails" runat="server" OnItemCommand="lstPollsDetails_ItemCommand"
                                            OnItemDataBound="lstPollsDetails_ItemDataBound">
                                            <LayoutTemplate>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr id="itemPlaceHolder" runat="server">
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr id="Pollrow" runat="server">
                                                    <td>
                                                        <asp:HiddenField ID="hdnPollId" Value='<%# Eval("intPollId") %>' runat="server" />
                                                        <asp:HiddenField ID="hdnRegistrationId" Value='<%#Eval("intRegistrationId") %>' runat="server" />
                                                        <asp:HiddenField ID="hdnstrVoteType" Value='<%#Eval("strVoteType") %>' runat="server" />
                                                        <div style="width: 100% !important; border-bottom: 0px;">
                                                            <div class="searchListTxt" style="width: 925px !important; margin-left: 15px; margin-top: -2px;">
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 745px;" colspan="2">
                                                                        <div class="breakallwords" style="width: 745px;">
                                                                            <asp:LinkButton ClientIDMode="Static" ToolTip="View Poll Details" Style="color: #000000;
                                                                                font-size: 26px; text-decoration: none;" ID="lnkPollName" Text='<%# Eval("strPollName") %>'
                                                                                Font-Underline="false" CommandName="Get Poll" runat="server"></asp:LinkButton>
                                                                                </div>
                                                                            <div style="text-align:right;margin: -15px 0px 0px 0px;" align="right">
                                                                                <span style="margin-left: 00px;"><span style="color: #9c9c9c; font-size: 14px; font-family: Calibri;
                                                                                    font-style: italic;">By</span>
                                                                                    <asp:LinkButton ID="lblPostlink" Style="color: #9c9c9c; margin: -5px 0 0; padding: 0 10px;
                                                                                        font-style: italic; width: 305px; text-decoration: none; padding-left: 2px; font-size: 14px;"
                                                                                        Text='<%# Eval("UserName") %>' CommandName="Details" runat="server"></asp:LinkButton>
                                                                            </div>
                                                                            </span>
                                                                        </td>
                                                                        
                                                                    </tr>
                                                                    <tr id="trDescription" runat="server">
                                                                        <td colspan="2">
                                                                            <p class="breakallwords" style="font-weight: lighter; font-size: 16px; width: 725px; margin-top: 10px;margin-left:-10px;">
                                                                                <asp:Label ID="lblDescription" Text='<%# Eval("strDescription") %>' runat="server"
                                                                                    Style="color: #9c9c9c; font-size: 16px;" CssClass="comment more"></asp:Label>
                                                                            </p>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                        <asp:UpdatePanel ID="updels" runat="server"><ContentTemplate> 
                                                                            <p id="pdiv" runat="server" style="color: #9c9c9c; font-size: 12px; font-family: Calibri;margin-left:-10px;
                                                                                margin-top: 5px;">
                                                                                Votes :
                                                                                <asp:Label ID="lblVoters" Text='<%# Eval("Votes") %>' runat="server"></asp:Label>
                                                                                &nbsp;&nbsp;&nbsp; Created On :
                                                                                <asp:Label ID="lblDate" Text='<%# Eval("dtAddedOn") %>' runat="server"></asp:Label>
                                                                                &nbsp;&nbsp;&nbsp; End's On :
                                                                                <asp:Label ID="lblExpiredt" Text='<%# Eval("dtPollExpireDate") %>' runat="server"></asp:Label>
                                                                                <asp:Label ID="lblExpireTime" Text='<%# Eval("strPollExpireTime") %>' runat="server"></asp:Label>
                                                                                <div class="editDeleteMore" style="margin-top: -25px;">
                                                                                <span class="spEditPoll">
                                                                                    <asp:LinkButton ID="lnkEdit" Font-Underline="false" Visible="false" class="edit"
                                                                                        ClientIDMode="Static" ToolTip="Edit" Text="Edit" CommandName="Edit Poll" CausesValidation="false"
                                                                                        runat="server">
                                                                                    </asp:LinkButton>
                                                                                    </span>
                                                                                    <span class="spDeletePoll">
                                                                                    <asp:LinkButton ID="lnkDelete" Font-Underline="false" Visible="false" class="edit"
                                                                                        ClientIDMode="Static" OnClientClick="javascript:DeletedivOpen();"
                                                                                        ToolTip="Delete" Text="Delete" CommandName="DeletePoll" CausesValidation="false"
                                                                                        runat="server">
                                                                                    </asp:LinkButton>
                                                                                    </span>
                                                                                </div>
                                                                            </p>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="lnkDelete" EventName="Click" />
                                                                            </Triggers>
                                                                             </asp:UpdatePanel>
                                                                            <div style="text-align: right; margin-left: 3px; margin-top: 18px;">
                                                                                <hr align="right" width="915px" />
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        <p id="pLoadMore" runat="server" align="center">
                                            <asp:ImageButton ID="imgLoadMore" runat="server" ClientIDMode="Static" ImageUrl="~/images/loadingIcon.gif"
                                                OnClick="imgLoadMore_OnClick" Height="100px" Width="100px" />
                                        </p>
                                        <p align="center" style="margin-top: 1%;">
                                            <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server"
                                                ClientIDMode="Static" ForeColor="Red" Visible="false"></asp:Label>
                                        </p>
                                        <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
                                        <div style="display: none" class="innerContainer">
                                            <div class="bgLine" id="Div1">
                                                &nbsp;</div>
                                            <div class="cls">
                                            </div>
                                            <div id="dvPage" runat="server" class="pagination" clientidmode="Static">
                                                <asp:LinkButton ID="lnkFirst" runat="server" CssClass="PagingFirst" OnClick="lnkFirst_Click"> </asp:LinkButton>
                                                <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click">&lt;&lt;</asp:LinkButton>
                                                <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand"
                                                    OnItemDataBound="rptDvPage_ItemDataBound">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPageLink" CssClass="Paging" runat="server" ClientIDMode="Static"
                                                            CommandName="PageLink" Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">&gt;&gt;</asp:LinkButton>
                                                <asp:LinkButton ID="lnkLast" runat="server" Style="background: none" OnClick="lnkLast_Click"><img src="images/spacer.gif" class="last" /></asp:LinkButton>
                                                <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="HiddenField6" ClientIDMode="Static" runat="server" />
                                        <asp:HiddenField ID="HiddenField7" ClientIDMode="Static" runat="server" />
                                    </div>
                                </div>
                                <div class="cls">
                                    <p>
                                        &nbsp;</p>
                                </div>
                            </div>
                        </div>
                        <!--left box ends-->
                    </div>
                    <!--left verticle search list ends-->
                </div>
            </div>
       
    <script type="text/jscript">
        function Cancel() {
            document.getElementById("divCancelPoll").style.display = 'none';
            return false;
        }
    </script>
    <script type="text/javascript">
        function messageClosePoll() {
            document.getElementById("divSuccessPolls").style.display = 'none';
        }
    </script>
    <asp:HiddenField ID="hdnLoader" runat="server" ClientIDMode="Static" />
    <script type="text/javascript">        $(document).ready(function () {
            var ID = "#" + $("#hdnLoader").val();
            $(ID).focus();
        }); </script>
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
     </ContentTemplate>
     <Triggers>
     <asp:AsyncPostBackTrigger ControlID="LinkButton1" />
      <asp:AsyncPostBackTrigger ControlID="LinkButton2" />
      <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
      <asp:AsyncPostBackTrigger ControlID="lnkDeleteCancel" />
     </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
            $(document).ready(function () {
                $("#lblNoMoreRslt").css("display", "none");
                $("#imgLoadMore").css("display", "none");
            });
    </script>
    <script type="text/javascript">
        $(window).scroll(function () {
            if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
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
                var showChar = 150;
                var ellipsestext = "...";
                var moretext = "More";
                var lesstext = "less";
                $('.more').each(function () {
                    var content = $(this).html();
                    if (content.length > showChar) {
                        var c = content.substr(0, showChar);
                        var h = content.substr(showChar - 1, content.length - showChar);
                        var html = c + '<span class="moreelipses">' + ellipsestext + '</span>&nbsp;<span class="morecontent"><span>' + h + '</span>&nbsp;&nbsp;<a href="" class="morelink">' + moretext + '</a></span>';
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
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#LinkButton2').on('click', function () {
                $('#LinkButton1').removeClass('recentActive');
                $(this).addClass('recentActive');
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#lnkShare').click(function () {
                $('#divDeletesucess').css("display", "none")
                $('#divPopupMember').css("display", "none")
                $('#divSuccessAcceptMember').css("display", "none")
                $('#dvPopup').css("display", "none")
                $('#divSuccessMess').css("display", "none")
            });

            $('#divCancelPoll').center();
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#lnkShare').click(function () {
                    $('#divDeletesucess').css("display", "none")
                    $('#divPopupMember').css("display", "none")
                    $('#divSuccessAcceptMember').css("display", "none")
                    $('#dvPopup').css("display", "none")
                    $('#divSuccessMess').css("display", "none")
                });

                $('#divCancelPoll').center();
            });
        });

    </script>
    <script type="text/javascript" >
        function docdelete() {
            $('#divDeletesucess').css("display", "block");
        }
        function divCancels() {
            $('#divDeletesucess').css("display", "none");
        }
        function DeletedivOpen() {
            $('#divDeletesucess').css("display", "block");
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("span.spEditPoll").click(function () {
                $(this).children('#lnkEdit').css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $("span.spDeletePoll").click(function () {
                $(this).children('#lnkDelete').css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $('#lnkDeleteConfirm').click(function (e) {
                $(this).css("box-shadow", "0px 0px 5px #00B7E5");
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("span.spEditFolder").click(function () {
                    $(this).children('#lnkEdit').css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $("span.spDeleteFolder").click(function () {
                    $(this).children('#lnkDelete').css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $('#lnkDeleteConfirm').click(function (e) {
                    $(this).css("box-shadow", "0px 0px 5px #00B7E5");
                });
            });
        });

    </script>
</asp:Content>
