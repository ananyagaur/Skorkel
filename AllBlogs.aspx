<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="AllBlogs.aspx.cs" Inherits="AllBlogs" %>

<%@ Register Src="~/UserControl/BlogPopulerPost.ascx" TagName="BlogPopulerPost" TagPrefix="BlogPopulerPost" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <!--inner container ends-->
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
                            <asp:HiddenField ID="hdnSubject" ClientIDMode="Static" runat="server" />
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <div class="searchBoxNew">
                                <asp:UpdatePanel ID="er" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtblogsearch" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtblogsearch" ClientIDMode="Static" CssClass="searchBlogs" runat="server"
                                            CausesValidation="true"></asp:TextBox>
                                        <br />
                                        <ajax:TextBoxWatermarkExtender TargetControlID="txtblogsearch" ID="txtwatermarkz"
                                            runat="server" WatermarkText="Search Blogs">
                                        </ajax:TextBoxWatermarkExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtblogsearch"
                                            Display="Dynamic" ValidationGroup="blogg" ErrorMessage="Please enter keyword"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                Search Using Tags Only
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <ul class="ulblogcontaxt">
                                            <asp:ListView ID="lstSerchSubjCategory" runat="server" OnItemDataBound="lstSerchSubjCategory_ItemDataBound">
                                                <ItemTemplate>
                                                    <li id="SubLi" runat="server" style="width: 203px; cursor: pointer;">
                                                        <asp:HiddenField ID="hdnSubCatId" ClientIDMode="Static" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                        <asp:LinkButton ID="lnkCatName" Style="text-decoration: none;" Font-Underline="false"
                                                            ClientIDMode="Static" OnClientClick="return false;" runat="server" Text='<%#Eval("strCategoryName")%>'
                                                            CommandName="Subject Category"></asp:LinkButton>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </ul>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="cls">
                                </div>
                                <br />
                                <br />
                            </div>
                            <div>
                                <br />
                                <asp:LinkButton ID="btnSave" CssClass="searchBlog" CausesValidation="true" runat="server"
                                    OnClientClick="javascript:CallWritebolgs();" ClientIDMode="Static" Text="SEARCH"
                                    ValidationGroup="blogg" OnClick="btnSave_Click"></asp:LinkButton>
                                <div style="display: none;">
                                    <asp:Button ID="btnSave1" ClientIDMode="Static" ValidationGroup="blogg" OnClientClick="javascript:CallWritebolgs();"
                                        runat="server" OnClick="btnSave_Click" />
                                </div>
                            </div>
                            <!--popular post starts-->
                            <div class="popularPost">
                                <p class="popularHeading">
                                    <br />
                                    Popular Posts</p>
                                <div>
                                    <asp:UpdatePanel ID="update" runat="server">
                                        <ContentTemplate>
                                            <BlogPopulerPost:BlogPopulerPost ID="ucBlogPopulerPost" runat="server" AllowDirectUpdate="true" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <!--popular post ends-->
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
                                        <div class="recentBlogs">
                                            <asp:LinkButton ID="lnkAllBlog" runat="server" Text="My Blogs" ClientIDMode="Static"
                                                CssClass="recentactive" OnClick="lnkAllBlog_Click"></asp:LinkButton>
                                            <span class="pipeBlack">|</span>
                                            <asp:LinkButton ID="lnkRecentBlogs" runat="server" Text="Recent Blogs" ClientIDMode="Static"
                                                CssClass="crtRecentBtn" OnClick="lnkRecentBlogs_Click"></asp:LinkButton>
                                        </div>
                                        <div class="writeYourBlogEntry imgBlog">
                                            <img src="images/write-blog.jpg" align="absmiddle" alt="" />
                                            <a href="write-blog.aspx" class="menuLinkColor" style="margin-left: 2px;">Write Your
                                                Blog Entry</a>
                                        </div>
                                    </div>
                                    <div class="cls">
                                    </div>
                                    <!--post title starts-->
                                    <div class="myBlogs">
                                        <div style="margin-left: 275px; margin-top: 15px;">
                                            <asp:Label ID="lblmsg" runat="server" Text="No Blog Found..!" ForeColor="Red" Visible="false"></asp:Label>
                                        </div>
                                        <asp:ListView ID="lstAllBlogs" runat="server" OnItemCommand="lstAllBlogs_ItemCommand"
                                            OnItemDataBound="lstAllBlogs_ItemDataBound" GroupItemCount="3" GroupPlaceholderID="groupPlaceHolder1"
                                            ItemPlaceholderID="itemPlaceHolder1">
                                            <GroupTemplate>
                                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                            </GroupTemplate>
                                            <ItemTemplate>
                                                <div class="postTitleArea" style="margin-top: 0px;">
                                                    <div class="postTime" style="width: 720px; margin-top: 0px;">
                                                        <asp:Label ID="lblDate" runat="server" Text='<%#Eval("dtAddedOn")%>'></asp:Label>
                                                        <div style="text-align: right; font-weight: normal; font-style: italic; margin-top: -16px;
                                                            color: #9C9C9C;">
                                                            By
                                                            <asp:Label ID="lnkAddBy" Font-Bold="false" Style="color: #9C9C9C;" runat="server"
                                                                Text='<%#Eval("strAddedBy")%>'></asp:Label></div>
                                                    </div>
                                                    <div class="cls">
                                                    </div>
                                                    <div class="postByTxt">
                                                        <div class="breakallwords breakalls postTxts">
                                                            <asp:HiddenField ID="hdnintBlogId" runat="server" Value='<%#Eval("intBlogId")%>' ClientIDMode="Static" />
                                                            <asp:HiddenField ID="hdnAddedBy" runat="server" Value='<%#Eval("intAddedBy")%>' ClientIDMode="Static" />
                                                            <asp:LinkButton ID="lnkBlogHeading" ToolTip="View Details" Font-Underline="false"
                                                                runat="server" Text='<%#Eval("strBlogHeading")%>' CssClass="blogHeadingName"
                                                                CommandName="BlogsDetails"  ClientIDMode="Static"></asp:LinkButton>
                                                        </div>
                                                        <div class="breakallwords breakalls">
                                                            <asp:Label ID="lblDescription" Style="color: #B3B3B3; font-size: 15px" runat="server"
                                                                Text='<%#Eval("strBlogTitle").ToString().Replace(Environment.NewLine,"<br />")%>'></asp:Label>
                                                        </div>
                                                        <div class="postReview">
                                                            <div class="editDeleteMore">
                                                                <asp:UpdatePanel ID="upDelete" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton ID="lnkEdit" Font-Underline="false" Visible="false" CssClass="edit"
                                                                            ClientIDMode="Static" ToolTip="Edit" Text="Edit" CommandName="Edit Blog" CausesValidation="false"
                                                                            runat="server">
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDelete" Font-Underline="false" Visible="false" CssClass="edit"
                                                                            ClientIDMode="Static" ToolTip="Delete" Text="Delete" CommandName="Delete Blog"
                                                                            CausesValidation="false" runat="server">
                                                                        </asp:LinkButton>
                                                                        <div class="commentsGiven">
                                                                            <img src="images/blog-comments.png" align="absmiddle" alt="" />
                                                                            <asp:LinkButton ID="lnkComments" ForeColor="#646161" Font-Bold="false" Style="text-decoration: none;
                                                                                color: #9c9c9c;" Font-Underline="false" runat="server" Text='<%#Eval("CommentCount")%>'
                                                                                CommandName="Comments"></asp:LinkButton>
                                                                        </div>
                                                                        <div class="viewed">
                                                                            <img src="images/blog-view.png" align="absmiddle" alt="" />
                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("ViewCount")%>'></asp:Label>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="lnkDelete" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                                <br />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        <div class="cls">
                                        </div>
                                        <div class="cls">
                                        </div>
                                    </div>
                                    <div>
                                        <asp:ValidationSummary ID="blogsummry" Style="padding-top: 750px;" CssClass="RedErrormsg"
                                            runat="server" ValidationGroup="blog" ClientIDMode="Static" ForeColor="Red" Font-Names="Verdana" />
                                    </div>
                                    <div id="dvPage" runat="server" class="pagingBlog" style="width: 475px;" align="center"
                                        clientidmode="Static">
                                        <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click" ClientIDMode="Static">
                                            <img id="imgPaging" runat="server" src="images/backpaging.jpg" clientidmode="Static"
                                                alt="" class="opt" style="display: none;" /></asp:LinkButton>
                                        <asp:LinkButton ID="lnkprev" runat="server" OnClientClick="return false;" ClientIDMode="Static"
                                            Style="display: block;">
                                            <img id="img2" runat="server" src="images/backpaging.jpg" clientidmode="Static" class="opt"
                                                alt="" /></asp:LinkButton>
                                        <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand"
                                            OnItemDataBound="rptDvPage_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPageLink" runat="server" ClientIDMode="Static" CommandName="PageLink"
                                                    Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click" ClientIDMode="Static"><img src="images/nextpaging.jpg" alt="" /></asp:LinkButton>
                                        <asp:LinkButton ID="lnkNextshow" runat="server" OnClientClick="return false;" Style="display: none;"
                                            ClientIDMode="Static"><img class="opt" src="images/nextpaging.jpg" alt="" /></asp:LinkButton>
                                        <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                                        <asp:HiddenField ID="hdnEndPage" runat="server" ClientIDMode="Static" />
                                    </div>
                                    <br />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="cls">
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
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
            <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="lnkNext" />
            <asp:AsyncPostBackTrigger ControlID="lnkPrevious" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('#hdnCurrentPage').val() == '1') {
                $('#imgPaging').css("display", "none");
                $('#lnkprev').css("display", "block");
            } else {
                $('#imgPaging').css("display", "block");
                $('#lnkprev').css("display", "none");
            }

            if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                $('#lnkNextshow').css("display", "block");
                $('#lnkNext').css("display", "none");
            }
            $("#txtblogsearch").keydown(function (e) {
                if (e.keyCode == 13) {
                    $('#btnSave1').click();
                    e.preventDefault();
                }
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                if ($('#hdnCurrentPage').val() == '1') {
                    $('#imgPaging').css("display", "none");
                    $('#lnkprev').css("display", "block");
                } else {
                    $('#imgPaging').css("display", "block");
                    $('#lnkprev').css("display", "none");
                }

                if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                    $('#lnkNextshow').css("display", "block");
                    $('#lnkNext').css("display", "none");
                }
                $("#txtblogsearch").keydown(function (e) {
                    if (e.keyCode == 13) {
                        $('#btnSave1').click();
                        e.preventDefault();
                    }
                });
            });
        });
    </script>
    <script type="text/javascript">
        var strSelTexts = '';
        $(document).ready(function () {
            $('ul.ulblogcontaxt li').click(function () {
                $(this).toggleClass('selectBlogLi unselectBlogLi');
                if ($(this).hasClass("selectBlogLi")) {
                    $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                } else {
                    $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                }
                AddSubjectCall($(this).children("#hdnSubCatId").val());
            });

            $('.postTxts').click(function () {
                var BlogId = $(this).children("#hdnintBlogId").val();
                var BlogUserId = $(this).children("#hdnAddedBy").val();
                //window.location("BlogsComments.aspx?intBlogId=" + BlogId + "&hdnAddedBy=" + BlogUserId+"&Blogtype="+);
            });

        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('ul.ulblogcontaxt li').click(function () {
                    $(this).toggleClass('selectBlogLi unselectBlogLi');
                    if ($(this).hasClass("selectBlogLi")) {
                        $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                    } else {
                        $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                    }
                    AddSubjectCall($(this).children("#hdnSubCatId").val());
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
                var sub = subVal[i];
                var vCl = $('.unselectBlogLi').children("#hdnSubCatId").val();
                var myElements = $(".unselectBlogLi");
                for (var i = 0; i < myElements.length; i++) {
                    var dtt = myElements.eq(i).children('#hdnSubCatId').val();
                    if (sub == dtt) {
                        myElements.eq(i).children('#lnkCatName').toggleClass("selectBlogcat unselectBlogcat");
                    }
                }
            });
        }
    </script>
    <script type="text/javascript">
        function docdelete() {
            $('#divDeletesucess').css("display", "block");
        }
        function divCancels() {
            $('#divDeletesucess').css("display", "none");
        }
        function CallWritebolgs() {
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
    </script>
</asp:Content>
