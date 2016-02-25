<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="AllQuestionDetails.aspx.cs" Inherits="AllQuestionDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="update" runat="server">
        <ContentTemplate>
            <div class="innerDocumentContainerSpc">
                <div id="divDeletesucess" clientidmode="Static" runat="server" class="EditProfilepopupHome"
                    style="display: none;">
                    <div id="divDeleteConfirm" runat="server" class="confirmDeletes" clientidmode="Static">
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
                                        <asp:Label ID="lblConnDisconn" runat="server" Text="Do you want to Delete ?" Font-Size="Small"></asp:Label>
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
                                                    Style="float: left; text-align: center; text-decoration: none ! important; width: 82px;
                                                    margin-top: -5px; color: #000; cursor: pointer;" OnClientClick="javascript:divCancels();return false;"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="innerContainer">
                    <div class="innerGroupBox">
                        <!--search box starts-->
                        <div class="innerContainerLeftMenu">
                            <div>
                                <a class="postNewQuestion" href="post-new-question.aspx"><b>Post New Question</b></a>
                            </div>
                            <div class="searchBoxNew">
                                <asp:UpdatePanel ID="up12" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSearchQuestion" ClientIDMode="Static" CssClass="searchBlogs"
                                            runat="server"></asp:TextBox>
                                        <br />
                                        <ajax:TextBoxWatermarkExtender TargetControlID="txtSearchQuestion" ID="txtwatermarkz"
                                            runat="server" WatermarkText="Search">
                                        </ajax:TextBoxWatermarkExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSearchQuestion"
                                            Display="Dynamic" ValidationGroup="t" ErrorMessage="Please enter keyword" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                Narrow your Search
                                <asp:UpdatePanel ID="UpdateSub" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lstSerchSubjCategory" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:HiddenField ID="hdnSubject" ClientIDMode="Static" runat="server" />
                                        <ul class="ulblogcontaxt">
                                            <asp:ListView ID="lstSerchSubjCategory" runat="server"  
                                                OnItemDataBound="lstSerchSubjCategory_ItemDataBound">
                                                <ItemTemplate>
                                                    <li id="SubLi" runat="server" style="width: auto; cursor: pointer;" >
                                                        <asp:HiddenField ID="hdnSubCatId" runat="server" ClientIDMode="Static" Value='<%#Eval("intCategoryId")%>' />
                                                        <asp:LinkButton ID="lnkCatName" ForeColor="#646161" Style="text-decoration: none;
                                                            color: #999999;" ClientIDMode="Static"  Font-Underline="false"  OnClientClick="return false;"
                                                            runat="server" Text='<%#Eval("strCategoryName")%>' CommandName="Subject Category"></asp:LinkButton>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </ul>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="cls">
                            </div>
                            <div class="viewAll" style="float: right; margin-bottom: 5px;">
                                <asp:LinkButton ID="lnkViewSubj" Text="View all" Font-Underline="false" runat="server"
                                    OnClick="lnkViewSubj_Click" Style="padding-right: 5px;"></asp:LinkButton></div>
                            <br />
                            <asp:LinkButton ID="btnSave" ClientIDMode="Static" CssClass="searchBlog" runat="server"  OnClientClick="javascript:CallWriteQA();"
                                Text="SEARCH" ValidationGroup="t" OnClick="btnSave_Click"></asp:LinkButton>
                            <div style="display: none;">
                                <asp:Button ID="btnSave1" ClientIDMode="Static" runat="server" ValidationGroup="t"  OnClientClick="javascript:CallWriteQA();" OnClick="btnSave_Click" />
                            </div>
                            <div style="display: none;">
                                <asp:Button ID="btnTag" ClientIDMode="Static" runat="server" OnClick="btnTag_Click" />
                            </div>
                        </div>
                        <!--search box ends-->
                        <!--left box starts-->
                        <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkAllBlog" />
                            </Triggers>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkRecentBlogs" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="innerContainerLeft">
                                    <div class="subtitle">
                                        <div class="recentBlogs qa">
                                            <asp:LinkButton ID="lnkAllBlog" runat="server" Text="Most Recent" ClientIDMode="Static"
                                                OnClick="chkRecent_CheckedChanged"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkRecentBlogs" runat="server" Text="Most Popular" ClientIDMode="Static"
                                                OnClick="chkMostPopular_CheckedChanged"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="cls">
                                    </div>
                                    <asp:UpdatePanel ID="UpdateParentQADetails" runat="server" ClientIDMode="Static">
                                        <ContentTemplate>
                                            <div style="margin-left: 275px;">
                                                <asp:Label ID="lblmsg" runat="server" Text="No Record Found..!" ForeColor="Red" Visible="false"></asp:Label>
                                            </div>
                                             <asp:HiddenField ID="hdnDeletePostQuestionID"  ClientIDMode="Static"
                                                            runat="server" />
                                                             <asp:HiddenField ID="hdnstrQuestionDescription"  ClientIDMode="Static"
                                                            runat="server" />
                                            <div class="qabox" style="border: none; margin-bottom: -10px;">
                                                <asp:ListView ID="lstParentQADetails" runat="server" OnItemDataBound="lstParentQADetails_ItemDataBound"
                                                    OnItemCommand="lstParentQADetails_ItemCommand">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnPostQuestionID" Value='<%# Eval("intPostQuestionId") %>' ClientIDMode="Static"
                                                            runat="server" />
                                                        <asp:HiddenField ID="hdnAddedBy" runat="server" Value='<%#Eval("intAddedBy")%>' />
                                                        <div class="qquestion">
                                                            Q</div>
                                                        <div class="questionTxt breakallwords" style="margin-left: -0px;">
                                                            <asp:LinkButton ID="Label1" Font-Underline="false" CommandName="QADetails" CssClass="commentQA moreQA"
                                                                runat="server" Text='<%#Eval("strQuestionDescription") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="categoryBoxQA" id="divCategorys" style="margin-left: 45px; margin-top: -13px;">
                                                            <div class="categoryTxt" style="width: 530px; margin-top: -10px;">
                                                                <ul>
                                                                    <asp:ListView ID="lstSubjCategory" runat="server" OnItemDataBound="lstSubjCategory_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <li id="SubLi" runat="server">
                                                                                <asp:HiddenField ID="hdnSubCatId" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                                                <asp:Label ID="lnkCatName" ForeColor="WhiteSmoke" Style="text-decoration: none !important;"
                                                                                    Font-Underline="false" runat="server" Text='<%#Eval("strCategoryName")%>'></asp:Label>
                                                                            </li>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </ul>
                                                            </div>
                                                            <div class="qsscreenBox">
                                                                <span class="ediDel">
                                                                           <asp:HiddenField ID="hdnintPostQuestionIdelet" Value='<%# Eval("intPostQuestionId") %>' ClientIDMode="Static"
                                                            runat="server"  />
                                                             <asp:HiddenField ID="lnkstrQuestionDescription" runat="server" Value='<%#Eval("strQuestionDescription") %>' ClientIDMode="Static"></asp:HiddenField>
                                                                            <asp:LinkButton ID="lnkEdit" Font-Underline="false" Font-Size="Small" Visible="false"
                                                                                Style="padding: 3px 13px;" ClientIDMode="Static" ToolTip="Delete" Text="Edit"
                                                                                class="edit" CommandName="Edit QA" CausesValidation="false" runat="server">
                                                                            </asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkDelete" Font-Underline="false" Font-Size="Small" Visible="false"
                                                                                Style="padding: 3px 13px;" ClientIDMode="Static" ToolTip="Delete" Text="Delete" OnClientClick="javascript:docdelete();return false;" 
                                                                                class="edit" CommandName="Delete QA" CausesValidation="false" runat="server">
                                                                            </asp:LinkButton>
                                                                </span>
                                                            </div>
                                                            <div class="bgLine" style="width: 700px; margin-left: 2px;" id="Div1">
                                                            </div>
                                                            <div class="answerTxt mr" style="margin-top: -5px; width: 630px;">
                                                                <div class="aanss">
                                                                    <asp:Label ID="btnReply" Style="text-decoration: none;" Text="Answers" runat="server"
                                                                        CommandName="ReplyPost" ToolTip="Add Answers"></asp:Label>:
                                                                    <asp:Label ID="lblreply" runat="server" Text=""></asp:Label></div>
                                                                <div class="aanss">
                                                                    <asp:Label ID="btnShare" Style="text-decoration: none;" Text="Shared" runat="server"
                                                                        CommandName="ShareForum" ToolTip="Shared"></asp:Label>:
                                                                    <asp:Label ID="lblShare" runat="server" Text=""></asp:Label></div>
                                                                <div class="aanss">
                                                                    <asp:Label ID="btnLike" Style="text-decoration: none;" Text="Like" runat="server"
                                                                        CommandName="LikeForum" ToolTip="Like"></asp:Label>:
                                                                    <asp:Label ID="lblTotallike" runat="server" Text=""></asp:Label></div>
                                                                <div class="ansDate">
                                                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label>
                                                                    <br />
                                                                </div>
                                                            </div>
                                                            <div class="bgLine" style="width: 748px; margin-left: -46px;" id="Div2">
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div id="dvPage" runat="server" class="pagingBlog" align="center" clientidmode="Static">
                                        <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click">
                                            <img id="imgPaging" runat="server" src="images/backpaging.jpg" class="opt" /></asp:LinkButton>
                                        <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand"
                                            OnItemDataBound="rptDvPage_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPageLink" runat="server" ClientIDMode="Static" CommandName="PageLink"
                                                    Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:LinkButton ID="lnkNext" runat="server" ClientIDMode="Static" OnClick="lnkNext_Click"><img src="images/nextpaging.jpg" /></asp:LinkButton>
                                        <asp:LinkButton ID="lnkNextshow" runat="server" OnClientClick="return false;" Style="display: none;"
                                            ClientIDMode="Static"><img class="opt" src="images/nextpaging.jpg" /></asp:LinkButton>
                                        <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                                        <asp:HiddenField ID="hdnEndPage" runat="server" ClientIDMode="Static" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <!--left box ends-->
                </div>
                <!--left verticle search list ends-->
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
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm"/>
             <asp:AsyncPostBackTrigger ControlID="btnTag"/>
             <asp:AsyncPostBackTrigger ControlID="lnkNext"/>
             <asp:AsyncPostBackTrigger ControlID="lnkPrevious"/>
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
            $(document).ready(function () {
                $(".categoryTxt ul li").click(function () {
                    $(this).toggleClass("gray");
                });
            });
            function CallTop() {
                $("html, body").animate({ scrollTop: 0 }, 10);
            }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var showChar = 150;
            var ellipsestext = "...";
            var moretext = "read more";
            var lesstext = "less";
            $('.moreQA').each(function () {
                var content = $(this).html();
                if (content.length > showChar) {
                    var c = content.substr(0, showChar);
                    var h = content.substr(showChar - 1, content.length - showChar);
                    var html = c + '<span class="moreelipses">' + ellipsestext + '</span>&nbsp;<span class="morecontent"><span>' + h + '</span>&nbsp;&nbsp;<a href="" class="morelinkQA">' + moretext + '</a></span>';
                    $(this).html(html);
                }
            });
            $(".morelinkQA").click(function () {
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

            if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                $('#lnkNextshow').css("display", "block");
                $('#lnkNext').css("display", "none");
            }
            $("#txtSearchQuestion").keydown(function (e) {
                if (e.keyCode == 13) {
                    $('#btnSave1').click();
                    e.preventDefault();
                }
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                    $('#lnkNextshow').css("display", "block");
                    $('#lnkNext').css("display", "none");
                }
                $("#txtSearchQuestion").keydown(function (e) {
                    if (e.keyCode == 13) {
                        $('#btnSave1').click();
                        e.preventDefault();
                    }
                });
                $('.moreQA').each(function () {
                    var content = $(this).html();
                    if (content.length > showChar) {
                        var c = content.substr(0, showChar);
                        var h = content.substr(showChar - 1, content.length - showChar);
                        var html = c + '<span class="moreelipses">' + ellipsestext + '</span>&nbsp;<span class="morecontent"><span>' + h + '</span>&nbsp;&nbsp;<a href="" class="morelinkQA">' + moretext + '</a></span>';
                        $(this).html(html);
                    }
                });
                $(".morelinkQA").click(function () {
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
        var strSelTexts = '';
        $(document).ready(function () {
            $('ul.ulblogcontaxt li').click(function (e) {
                e.preventDefault();
                $(this).toggleClass('selectBlogLi unselectBlogLi');
                if ($(this).hasClass("selectBlogLi")) {
                    $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                } else {
                    $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                }
                AddSubjectCall($(this).children("#hdnSubCatId").val());
            });

        });

        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('ul.ulblogcontaxt li').click(function (e) {
                    $(this).toggleClass('selectBlogLi unselectBlogLi');
                    if ($(this).hasClass("selectBlogLi")) {
                        $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                    } else {
                        $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                    }
                    AddSubjectCall($(this).children("#hdnSubCatId").val());
                   e.preventDefault();
                });

            });
        });

        function AddSubjectCall(ids) {
            var subVal = $("#hdnSubject").val();
            if (subVal == '') {
                $("#hdnSubject").val(ids);
                //$('#btnTag').click();
            } else {
                strSelTexts = $("#hdnSubject").val();
                strSelTexts += ',' + ids;
                $("#hdnSubject").val(strSelTexts);
                strSelTexts = '';
                //$('#btnTag').click();
            }
        }
        function CallTagSelections() {
            var subVal = $("#hdnSubject").val().split(","); ;
            $.each(subVal, function (i) {
                var sub=subVal[i];
                var vCl = $('.unselectBlogLi').children("#hdnSubCatId").val();
                var myElements = $(".unselectBlogLi");
                for (var i = 0; i < myElements.length; i++) {
                    var dtt=myElements.eq(i).children('#hdnSubCatId').val();
                    if (sub == dtt) {
                        myElements.eq(i).children('#lnkCatName').toggleClass("selectBlogcat unselectBlogcat");
                    } 
                }
            });
        }
    </script>
    <script type="text/javascript">
        function CallWriteQA() {
            $('#btnSave').css("box-shadow", "0px 0px 5px #00B7E5");
            $('#btnSave').css("background", "#00A5AA");
            if ($('#txtblogsearch').text() == '') {
                setTimeout(
                function () {
                    $('#btnSave').css("background", "#0096a1");
                    $('#btnSave').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
        }
        function docdelete() {
            $('#divDeletesucess').css("display", "block");
        }
        function divCancels() {
            $('#divDeletesucess').css("display", "none");
        }
        $(document).ready(function () {
            $('#lnkDelete').click(function () {
                $('#divDeletesucess').css("display", "block");
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#lnkDelete').click(function () {
                    $('#divDeletesucess').css("display", "block");
                });
                $("span.ediDel").click(function () {
                    $('#hdnDeletePostQuestionID').val($(this).children('#hdnintPostQuestionIdelet').val());
                    $('#hdnstrQuestionDescription').val($(this).children('#lnkstrQuestionDescription').val());
                });
            });

            $("span.ediDel").click(function () {
                $('#hdnDeletePostQuestionID').val($(this).children('#hdnintPostQuestionIdelet').val());
                $('#hdnstrQuestionDescription').val($(this).children('#lnkstrQuestionDescription').val());
            });
        });
    </script>
</asp:Content>
