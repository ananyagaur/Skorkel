<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="BlogsComments.aspx.cs" Inherits="BlogsComments" %>

<%@ Register Src="~/UserControl/BlogPopulerPost.ascx" TagName="BlogPopulerPost" TagPrefix="BlogPopulerPost" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <!--pop up starts-->
            <div class="popUp1" id="PopUpShare" clientidmode="Static" runat="server" style="display: none;">
                <div class="popUp" id="popUp">
                    <div class="popUpBox">
                        <p class="shareContent" style="margin-top: 5px;">
                            Share Blogs</p>
                        <div style="">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                <tr>
                                    <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="popHeading" style="display: none;">
                                                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>
                                            <asp:Label ID="lblTitleGroup" runat="server"></asp:Label>
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="popBgLineGray" style="margin-top: 20px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="padding-left: 10px; margin-top: 6px;">
                                            <div>
                                                <table>
                                                    <tr>
                                                        <td id="tdDepartment" style="height: 30px; width: 18%">
                                                            <span style="font-size: 16px;">To :</span>
                                                            <select data-placeholder="Enter members name here" class="chosen-select" id="txtInviteMembers"
                                                                onchange="getMultipleValues(this.id)" runat="server" multiple style="width: 450px;"
                                                                tabindex="4">
                                                            </select>
                                                            <asp:HiddenField ID="hdnDepartmentIds" ClientIDMode="Static" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div style="padding-left: 45px;">
                                            <asp:Label ID="lblMess" ForeColor="Red" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="padding-left: 15px; padding-top: 10px;">
                                            <textarea id="txtBody" runat="server" placeholder="Message" class="forumTitle" style="width: 445px;
                                                margin-left: 26px; margin-right: 50px; color: #9c9c9c; font: 111% Arial;"></textarea>
                                        </div>
                                        <div style="padding-top: 1px;">
                                            <asp:TextBox ID="txtLink" runat="server" value="Paste link" data-watermark="Paste link"
                                                Enabled="false" class="forumTitlenew" Style="width: 450px; font-size: small;
                                                margin-left: 42px;"></asp:TextBox>
                                        </div>
                                        <p>
                                            <strong>
                                                <asp:Label ID="lblMessAccept" runat="server"></asp:Label>
                                                <asp:Label ID="lblMessReject" runat="server"></asp:Label>
                                            </strong>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="">
                                        <div>
                                            <div>
                                                <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Share"
                                                    Style="margin-left: 338px; font-size: 16px;" CssClass="joinBtn" OnClick="lnkPopupOK_Click"></asp:LinkButton>
                                            </div>
                                            <br />
                                            <div>
                                                <a onclick="CancelPopup();" causesvalidation="false" style="float: left; text-align: center;
                                                    text-decoration: none; width: 82px; color: #000; margin-top: -9px; cursor: pointer;
                                                    font-size: 16px;">Cancel</a>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!--pop up ends-->
            &nbsp;<!--heading ends--><div class="cls">
            </div>
            <!--inner container starts-->
            <div class="cls">
            </div>
            <div id="divSuccess" runat="server" style="border: 20px solid rgba(0,0,0,0.5); float: left;
                width: 500px; padding-top: 0px; position: fixed; margin: 9% 0 0 16%; z-index: 100;
                display: none;" clientidmode="Static">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                    <tr>
                        <td>
                            <strong>&nbsp;&nbsp;
                                <asp:Label ID="lblSuccess" runat="server" Text="comment added successfully." Font-Size="Small"></asp:Label>
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
                                            text-decoration: none; width: 82px; padding-top: 5px; color: #000;" onclick="javascript:messageClose();return false;">
                                            Close </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="cls">
            </div>
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
                            <asp:UpdatePanel ID="upmains" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hdnSubject" ClientIDMode="Static" runat="server" />
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
                                        <ul class="ulblogcontaxt">
                                            <asp:ListView ID="lstSerchSubjCategory" runat="server" OnItemDataBound="lstSerchSubjCategory_ItemDataBound">
                                                <ItemTemplate>
                                                    <li id="SubLi" runat="server" style="width: auto; cursor: pointer;">
                                                        <asp:HiddenField ID="hdnSubCatId" ClientIDMode="Static" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                        <asp:LinkButton ID="lnkCatName" Style="text-decoration: none;" Font-Underline="false"
                                                            ClientIDMode="Static" OnClientClick="return false;" runat="server" Text='<%#Eval("strCategoryName")%>'
                                                            CommandName="Subject Category"></asp:LinkButton>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </ul>
                                        <div class="cls">
                                        </div>
                                        <div class="viewAll" style="float: right; margin-bottom: 5px; margin-right: 2px;">
                                            <asp:LinkButton ID="lnkViewSubj1" Text="View all" Font-Underline="false" runat="server"
                                                OnClick="lnkViewSubj1_Click" Style="padding-right: 5px;"></asp:LinkButton></div>
                                    </div>
                                    <div>
                                        <br />
                                        <asp:LinkButton ID="btnSave" CssClass="searchBlog" CausesValidation="true" runat="server" OnClientClick="javascript:CallWritebolgs();"
                                            ClientIDMode="Static" Text="SEARCH" ValidationGroup="blogg" OnClick="btnSave_Click"></asp:LinkButton>
                                        <div style="display: none;">
                                            <asp:Button ID="btnSave1" ClientIDMode="Static" runat="server" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                    <asp:AsyncPostBackTrigger ControlID="lnkViewSubj1" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="popularPost">
                                <p class="popularHeading">
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
                        <div class="innerContainerLeft">
                            <div class="subtitle">
                                <div class="recentBlogs">
                                    <asp:LinkButton ID="lnkBacklink" runat="server" ClientIDMode="Static" Text="My Blogs"
                                        CssClass="recentBlogTxt" Font-Underline="false" OnClick="lnkBacklink_Click"></asp:LinkButton>
                                </div>
                                <div class="writeYourBlogEntry imgBlog">
                                    <img src="images/write-blog.jpg" align="absmiddle" />
                                    <asp:LinkButton ID="lnkSubmitBlog" runat="server" ClientIDMode="Static" Text="Write Your Blog Entry"
                                        CssClass="menuLinkColor" Font-Underline="false" OnClick="lnkSubmitBlog_Click"></asp:LinkButton>
                                </div>
                            </div>
                            <div class="cls">
                            </div>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <div class="categoryTxt" style="width: 100%">
                                <div class="listHeading" style="margin-left: 0px; width: 728px;">
                                    <asp:ListView ID="lstAllBlogs" runat="server" OnItemCommand="lstAllBlogs_ItemCommand"
                                        OnItemDataBound="lstAllBlogs_ItemDataBound" GroupItemCount="3" GroupPlaceholderID="groupPlaceHolder1"
                                        ItemPlaceholderID="itemPlaceHolder1">
                                        <GroupTemplate>
                                            <tr>
                                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                            </tr>
                                        </GroupTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnBlogLikeUserId" runat="server" Value='<%#Eval("BlogLikeUserId") %>' />
                                            <asp:HiddenField ID="hdnintAddedBy" runat="server" Value='<%#Eval("intAddedBy") %>' />
                                            <div class="postArea">
                                                <div class="postTitle">
                                                    <p class="postTxt">
                                                        <div class="brekwordsBlogPopular">
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("strBlogHeading")%>' CssClass="BlogHeadingFont"></asp:Label>
                                                        </div>
                                                    </p>
                                                    <p class="postedOn" style="margin-top: -15px;">
                                                        <span style="color: #B3B3B3; font-style: italic;">Posted on</span>
                                                        <asp:Label ID="lblDate" Font-Bold="false" runat="server" Style="color: #B3B3B3; font-style: italic;"
                                                            Text='<%#Eval("dtAddedOn")%>'></asp:Label>
                                                        <span style="color: #B3B3B3; font-style: italic;">by</span>
                                                        <asp:Label ID="lnkAddBy" Font-Bold="false" runat="server" Style="color: #B3B3B3;
                                                            font-style: italic;" Text='<%#Eval("strAddedBy")%>'></asp:Label>
                                                    </p>
                                                </div>
                                                <div class="postReview" style="width: 122px;">
                                                    <div class="commentsGiven">
                                                        <img src="images/blog-comments.png" align="absmiddle" />
                                                        <asp:Label ID="lnkComments" Font-Bold="false" ForeColor="#9c9c9c" Style="text-decoration: none;"
                                                            Font-Underline="false" runat="server" Text='<%#Eval("CommentCount")%>'></asp:Label></div>
                                                    <div class="viewed">
                                                        <img src="images/blog-view.png" align="absmiddle" />
                                                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("ViewCount")%>'></asp:Label></div>
                                                </div>
                                                <br />
                                                <div class="categoryBoxBlog" style="margin-left: -25px; width: 690px;">
                                                    <div class="ansTagsBlog" style="margin-left: 0px; border: none;">
                                                        <ul>
                                                            <asp:ListView ID="lstSubjCategory" runat="server">
                                                                <ItemTemplate>
                                                                    <li id="SubLi" runat="server" style="border: none; margin-right: -3%; display: inline;">
                                                                        <asp:HiddenField ID="hdnSubCatId" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                                        <asp:LinkButton ID="btnBlogShare" CssClass="tagsBlog smtxt" Text='<%#Eval("strCategoryName")%>'
                                                                            Style="text-decoration: none !important;" runat="server"></asp:LinkButton>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--post area ends-->
                                            <!--post txt starts-->
                                            <div class="postTxtArea">
                                                <div class="breakwordsdescriptionbc">
                                                    <asp:Label ID="Label2" runat="server" Style="color: #B3B3B3;" Text='<%#Eval("strBlogTitle").ToString().Replace(Environment.NewLine,"<br />")%>'></asp:Label>
                                                </div>
                                            </div>
                                            <div class="qscreenBox" style="margin-right: 0px;">
                                                <asp:LinkButton Font-Underline="false" CssClass="edit" CommandName="Edit Blog" ID="lnkEdit"
                                                    runat="server" Visible="false" Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton Font-Underline="false" CssClass="edit" CommandName="Delete Blog"
                                                    ID="lnkdelete" Visible="false" runat="server" Text="Delete"></asp:LinkButton>
                                            </div>
                                            <!--post txt ends-->
                                            <div class="cls">
                                            </div>
                                            <div class="commentsShareLike txt">
                                                <div class="commentsarea" style="margin-left: 0px;">
                                                    <img src="images/blog-comments1.png" align="absmiddle" style="margin-left: -75px;
                                                        margin-bottom: 6px;" height="22px" width="25px" />
                                                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                                        <ContentTemplate>
                                                            <div style="margin-right: -50px; margin-top: -25px;">
                                                                <asp:Label ID="Label4" runat="server" Style="text-decoration: none; color: #B3B3B3;
                                                                    display: none;" Text='<%#Eval("CommentCount")%>'></asp:Label>
                                                                <a id="lnkComment" style="text-decoration: none; color: #B3B3B3; font-weight: normal;
                                                                    cursor: pointer;" clientidmode="Static">Comment </a>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="sharearea">
                                                    <img src="images/blog-share.png" align="absmiddle" style="margin-left: -45px; margin-bottom: 6px;"
                                                        height="22px" width="25px" />
                                                    <div style="margin-right: -55px; margin-top: -25px;">
                                                        <asp:Label ID="lblBlogShare" runat="server" Style="text-decoration: none; color: #B3B3B3;
                                                            display: none;" Text='<%#Eval("TotalShare")%>'></asp:Label>
                                                        <asp:LinkButton ID="btnBlogShare" Style="text-decoration: none; color: #B3B3B3;"
                                                            Text="Share" runat="server" CommandName="ShareBlog" ToolTip="Share"></asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="likearea">
                                                    <img src="images/blog-like.png" align="absmiddle" style="margin-bottom: 6px;" height="22px"
                                                        width="25px" />
                                                    <asp:UpdatePanel ID="UpdateBlogPanel" UpdateMode="Conditional" runat="server">
                                                        <ContentTemplate>
                                                            <div style="margin-right: -80px; margin-top: -25px;">
                                                                <asp:Label ID="lblTotalBloglike" Style="text-decoration: none; color: #B3B3B3; display: none;"
                                                                    runat="server" Text='<%#Eval("TotalLike")%>' ToolTip="View Likes"></asp:Label>
                                                                <asp:LinkButton ID="btnBlogLike" Style="text-decoration: none; color: #B3B3B3;" Text="Like"
                                                                    runat="server" CommandName="LikeBlog" ToolTip="Like"></asp:LinkButton>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="cls">
                                            </div>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <!--search result list starts-->
                                <div style="height: 22px; background-color: #DADADA; width: 750px; margin-left: 0px;">
                                    <p style="font-weight: bold">
                                        &nbsp;&nbsp; Comments
                                    </p>
                                </div>
                                <div id="divCommentDisplay" clientidmode="Static" style="margin-left: 21px; width: 728px;">
                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                    <div class="BlogtextEditor" style="width: 95%;">
                                        <textarea id="CKDescription" clientidmode="Static" runat="server" cols="20" rows="10" placeholder="Comment"
                                            class="uploadDescriptionTxt postAns" style="margin-left: 18px;"></textarea>
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            Style="margin-left: 3%;" ControlToValidate="CKDescription" Display="Dynamic"
                                            ValidationGroup="blog" ErrorMessage="Please enter comment." ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                    <div style="float: right; margin-right: 11%;">
                                        &nbsp;<asp:LinkButton ID="lnkPostComent" runat="server" ClientIDMode="Static" Text="Post Comment"
                                            ValidationGroup="blog" CssClass="vote" OnClick="lnkPostComent_Click"></asp:LinkButton>
                                    </div>
                                </div>
                                <div style="width: 640px;">
                                    <div class="listHeading" style="margin-left: 20px;">
                                        <br />
                                        <asp:Repeater ID="RepPopPost" runat="server" OnItemCommand="RepPopPost_ItemCommand"
                                            OnItemDataBound="RepPopPost_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="commentArea" style="margin-left: 5px; border: none;">
                                                    <div class="viewerDetails" style="border-bottom: 1px solid #e7e7e7; margin-top: -22px;">
                                                        <div class="photo" style="width: 35px;">
                                                            <img id="img1" runat="server" src='<%# Eval("vchrPhotoPath")%>' width="40" height="40" />
                                                            <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                                        </div>
                                                        <asp:HiddenField ID="hdnBlogCommentUser" runat="server" Value='<%#Eval("BlogCommentUser")%>' />
                                                        <asp:HiddenField ID="hdnCommentID" runat="server" Value='<%#Eval("intCommentId")%>' />
                                                        <asp:HiddenField ID="hdnintAddedBy" runat="server" Value='<%#Eval("intAddedBy")%>' />
                                                        
                                                        <div class="viewerName">
                                                            <p class="viewerNameTxt">
                                                                <asp:LinkButton ID="lnkAddedby" ForeColor="#727272" ToolTip="View profile" Style="text-decoration: none;
                                                                    color: #727272;" Font-Underline="false" CommandName="View profile"
                                                                    runat="server" Text='<%#Eval("strAddedBy")%>'></asp:LinkButton>
                                                            </p>
                                                            <p class="viewerCommentTxt">
                                                                Commented on
                                                                <asp:Label ID="lblPostedOn" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label>
                                                            </p>
                                                        </div>
                                                        <div class="cls">
                                                        </div>
                                                        <p>
                                                            <p class="breakwordsbc">
                                                                <asp:Label ID="lblComment" Visible="false" Font-Size="Small" runat="server" Text='<%#Eval("strComment")%>'></asp:Label>
                                                                <asp:Label ID="lblCommentAll" Font-Size="Small" runat="server" Text='<%#((string)Eval("strCommentAll")).Replace("\n", "<br>")%>'></asp:Label>
                                                            </p>
                                                            <div class="qscreenBox">
                                                                <asp:LinkButton Font-Underline="false" CssClass="edit" CommandName="Edit Blog" ID="lnkEdit"
                                                                    runat="server" Visible="false" Text="Edit"></asp:LinkButton>
                                                                <asp:LinkButton Font-Underline="false" CssClass="edit" CommandName="Delete Blog"
                                                                    ID="lnkdelete" Visible="false" runat="server" Text="Delete"></asp:LinkButton>
                                                            </div>
                                                            <div class="cls">
                                                            </div>
                                                        </p>
                                                        <asp:UpdatePanel ID="upRep1" UpdateMode="Conditional" runat="server" Visible="false">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="lnkViewSubj" Text="view more" runat="server" OnClick="lnkViewSubj_Click"
                                                                    Style="padding-right: 5px; color: Gray" CommandName="ViewMore"></asp:LinkButton>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        </p>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div style="text-align: right;">
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
                            <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
                            <div class="cls">
                            </div>
                        </div>
                        <div class="cls">
                        </div>
                    </div>
                    <!--left box ends-->
                </div>
                <!--left verticle search list ends-->
                <script type="text/javascript">
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
            </div>
            <!--inner container ends-->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkPostComent" />
            <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
            <asp:AsyncPostBackTrigger ControlID="lnkPopupOK" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
            $(document).ready(function () {
                $(".categoryTxt ul li").click(function () {
                    $(this).toggleClass("gray");
                });
            });
            function getMultipleValues(ctrlId) {
                $('#tdDepartment').find('label.error').remove();
                var control = document.getElementById(ctrlId);
                var strSelText = '';
                var cnt = 0;
                for (var i = 0; i < control.length; i++) {
                    if (control.options[i].selected) {
                        if (cnt == 0) {
                            strSelText += control.options[i].value;
                        }
                        else {
                            strSelText += ',' + control.options[i].value;
                        }
                        cnt++;
                    }
                }
                $('#hdnDepartmentIds').val(strSelText);
            }
            function CancelPopup() {
                document.getElementById("PopUpShare").style.display = 'none';
                document.getElementById("divSuccess").style.display = 'none';
                return false;
            }
    </script>
    <script type="text/javascript">
        function messageClose() {
            document.getElementById('divSuccess').style.display = 'none';
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#lnkComment").click(function () {
                $("#CKDescription").focus();
            });
            $("#txtblogsearch").keydown(function (e) {
                if (e.keyCode == 13) {
                    if ($("#txtblogsearch").val() != '') {
                        e.returnValue = true;
                        $('#btnSave1').click();
                        return false;
                    }
                }
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("#lnkComment").click(function () {
                    $("#CKDescription").focus();
                });
                $("#txtblogsearch").keydown(function (e) {
                    if (e.keyCode == 13) {
                        if ($("#txtblogsearch").val() != '') {
                            e.returnValue = true;
                            $('#btnSave1').click();
                            return false;
                        }
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
            } else {
                strSelTexts = $("#hdnSubject").val();
                strSelTexts += ',' + ids;
                $("#hdnSubject").val(strSelTexts);
                strSelTexts = '';
            }
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
