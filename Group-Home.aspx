<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Group-Home.aspx.cs" MasterPageFile="~/Main.master"
    Inherits="Group_Home" %>

<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControl/Share.ascx" TagName="ShareDetails" TagPrefix="Share" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("js/jquery.custom-scrollbar.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container" style="padding-top: 40px">
        <div class="cls">
        </div>
        <div class="innerDocumentContainerGroup">
            <div id="divDeletesucess" clientidmode="Static" runat="server" class="EditProfilepopupHome"
                style="display: none;">
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
                                                CssClass="joinBtn" OnClick="lnkDeleteConfirm_Click" OnClientClick="javascript:divCancels();"></asp:LinkButton>
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
            <!--groups top box starts-->
            <asp:UpdatePanel ID="uppanel" runat="server">
                <ContentTemplate>
                    <Group:GroupDetails ID="grpDetails" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <!--groups top box ends-->
            <!--left box starts-->
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div id="divSecondWall" runat="server" clientidmode="Static">
                        <div class="innerGroupBoxnew" style="margin-top: 6px;">
                            <div class="innerContainerLeft" style="width: 900px">
                                <div class="tagContainer" style="width: 900px">
                                    <div class="forumsTabsHome" style="border: none;">
                                        <ul style="margin-left: 20px; padding: 0px;">
                                            <li style="margin-right: 54px;">
                                                <asp:LinkButton ID="lnkProfile" runat="server" Text="Profile" ClientIDMode="Static"
                                                    OnClick="lnkProfile_Click"></asp:LinkButton></li>
                                            <li id="DivHome" runat="server" style="margin-right: 54px;">
                                                <div>
                                                    <asp:LinkButton ID="lnkHome" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"
                                                        class="forumstabAcitve"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li id="DivForumTab" runat="server" clientidmode="Static" style="display: block;
                                                margin-right: 54px;">
                                                <div>
                                                    <asp:LinkButton ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static"
                                                        OnClick="lnkForumTab_Click"></asp:LinkButton></div>
                                            </li>
                                            <li id="DivUploadTab" runat="server" clientidmode="Static" style="display: block;
                                                margin-right: 54px;">
                                                <div>
                                                    <asp:LinkButton ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static"
                                                        OnClick="lnkUploadTab_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li id="DivPollTab" runat="server" clientidmode="Static" style="display: block; margin-right: 54px;">
                                                <div>
                                                    <asp:LinkButton ID="lnkPollTab" runat="server" Text="Polls" ClientIDMode="Static"
                                                        OnClick="lnkPollTab_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li id="DivEventTab" runat="server" clientidmode="Static" style="display: block;
                                                margin-right: 54px;">
                                                <div>
                                                    <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                                                        OnClick="lnkEventTab_Click"></asp:LinkButton></div>
                                            </li>
                                            <li id="DivMemberTab" runat="server" clientidmode="Static" style="display: block;
                                                margin-right: 54px;">
                                                <div>
                                                    <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                                        OnClick="lnkEventMemberTab_Click"></asp:LinkButton></div>
                                            </li>
                                        </ul>
                                        <div class="cls">
                                        </div>
                                    </div>
                                </div>
                                <div class="wallContainer" style="border: #e7e7e7 solid 1px; height: 99%; margin-top: 11px;
                                    width: 938px;">
                                    <br />
                                    <br />
                                    <div class="wallRightContainer" style="margin-top: -5%; margin-left: 2%; width: 75%;">
                                        <div class="createForumBox members Mem profileSection">
                                            <ul class="icons" style="list-style: none outside none;">
                                                <li>
                                                    <img id="icon1" src="images/icon-1.jpg" title="Update Post" style="cursor: pointer" alt=""
                                                        onclick="showImage('1');" /></li>
                                                <li>
                                                    <img id="icon2" src="images/icon-2.jpg" title="Upload Photo" style="cursor: pointer" alt=""
                                                        onclick="showImage('2');" /></li>
                                                <li>
                                                    <img id="icon3" src="images/icon-3.jpg" title="Upload Video" style="cursor: pointer" alt=""
                                                        onclick="showImage('3');" /></li>
                                                <li>
                                                    <img id="icon4" src="images/icon-4.jpg" title="Update Link" style="cursor: pointer" alt=""
                                                        onclick="showImage('4');" /></li>
                                            </ul>
                                            <asp:Panel ID="pnlWhatsMind" runat="server" DefaultButton="lnkDummyPost">
                                                <div class="iconsForm">
                                                    <div class="whtField">
                                                        <div class="iconArrow">
                                                            <img src="images/left-arrow.jpg" alt="" /></div>
                                                        <div style="display: block;" id="div1" runat="server" clientidmode="Static">
                                                            <textarea id="txtPostUpdate" clientidmode="Static" runat="server" class="uploadDescriptionTxt profile" rows="0" cols="0"
                                                                placeholder="What's on your mind?"></textarea>
                                                            <asp:RequiredFieldValidator Style="padding-left: 2px;" ClientIDMode="Static" ID="RequiredFieldValidator1"
                                                                runat="server" ControlToValidate="txtPostUpdate" Display="Dynamic" ValidationGroup="post"
                                                                ErrorMessage="Please write a post" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            <asp:HiddenField ID="hdnvdeonme" runat="server" Value="" />
                                                            <asp:HiddenField ID="hdnPhotoName" runat="server" Value="" />
                                                        </div>
                                                        <br />
                                                        <div class="cls">
                                                        </div>
                                                        <div style="display: none; width: 500px;" id="div2" runat="server" clientidmode="Static">
                                                            <div class="choosePhoto">
                                                                Choose Photo to upload:
                                                                <asp:FileUpload ID="PhotoUpload" ClientIDMode="Static" runat="server" />
                                                            </div>
                                                            <br />
                                                            <asp:Label ID="lblErrorMess" ClientIDMode="Static" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </div>
                                                        <div style="display: none; width: 500px;" id="div3" runat="server" clientidmode="Static">
                                                            <div class="choosePhoto">
                                                                Choose video to upload:
                                                                <asp:FileUpload ID="VideoUpload" ClientIDMode="Static" runat="server" />
                                                            </div>
                                                            <br />
                                                            <asp:Label ID="lblErrorvideo" ClientIDMode="Static" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </div>
                                                        <div style="display: none; width: 500px;" id="div4" runat="server" clientidmode="Static">
                                                            <asp:TextBox ID="txtPostLink" AutoCompleteType="Disabled" ClientIDMode="Static" CssClass="uploadTxtField"
                                                                Style="margin: 8px 10px; width: 500px;" runat="server"></asp:TextBox>
                                                            <ajax:TextBoxWatermarkExtender TargetControlID="txtPostLink" ID="TextBoxWatermarkExtender1"
                                                                runat="server" WatermarkText="Link">
                                                            </ajax:TextBoxWatermarkExtender>
                                                            <br />
                                                            <asp:Label ID="lblLink" ClientIDMode="Static" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                            <asp:HiddenField ID="hdnintStatusUpdateId" ClientIDMode="Static" Value="" runat="server" />
                                                            <asp:Label ID="lblintCommentId" Style="display: none;" ClientIDMode="Static" Text=""
                                                                runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="dropList" id="ddlpost">
                                                        <asp:DropDownList ID="ddlPostType" runat="server" CssClass="selectBox" ClientIDMode="Static"
                                                            Visible="true">
                                                            <asp:ListItem Text="Public" Value="public"></asp:ListItem>
                                                            <asp:ListItem Text="Private" Value="private"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="dropList">
                                                        <asp:LinkButton ID="lnkDummyPost" runat="server" CssClass="vote" ClientIDMode="Static"
                                                            ValidationGroup="post" CausesValidation="true" Text="Post" Style="display: none;"
                                                            OnClick="btnPostUpdate_Click"></asp:LinkButton>
                                                        <a id="lnkPostUpdate" runat="server" clientidmode="Static" class="vote" href="#"
                                                            onclick="return validate();">Post</a>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" ClientIDMode="Static" CausesValidation="true"
                                                            Text="Cancel" CssClass="cancel" OnClientClick="ClearOrgText();"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="cls">
                                        </div>
                                        <!--box ends-->
                                        <!--section starts-->
                                        <strong style="text-align: center; margin: 3px 3px 3px 80px;">
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        </strong>
                                        <Share:ShareDetails ID="ShareDetails" runat="server" />
                                        <asp:ListView ID="lstPostUpdates" runat="server" OnItemCommand="lstPostUpdates_ItemCommand"
                                            OnItemDataBound="lstPostUpdates_ItemDataBound" OnItemCreated="lstPostUpdates_ItemCreated">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnPostUpdateId" Value='<%# Eval("Id") %>' runat="server" />
                                                <asp:HiddenField ID="hdnRegistrationId" Value='<%#Eval("intRegistrationId") %>' runat="server" />
                                                <asp:HiddenField ID="hdnVideoName" Value='<%# Eval("strVideoPath") %>' runat="server" />
                                                <asp:HiddenField ID="hdnPhotoPath" Value='<%# Eval("strPhotoPath") %>' runat="server" />
                                                <asp:HiddenField ID="hdnPostLink" Value='<%# Eval("strPostLink") %>' runat="server" />
                                                <asp:HiddenField ID="hdnintUserTypeId" runat="server" Value='<%#Eval("intUserTypeId") %>' />
                                                <asp:HiddenField ID="hdnIframe" Value='<%# "VideoFiles/"+Eval("strVideoPath")%>'
                                                    ClientIDMode="Static" runat="server" />
                                                <asp:HiddenField ID="hdnShared" Value='<%# Eval("intShared") %>' runat="server" />
                                                <asp:HiddenField ID="hdnSharedId" Value='<%# Eval("intSharedId") %>' runat="server" />
                                                <asp:HiddenField ID="hdnSharedUserTypeId" Value='<%# Eval("intSharedUserTypeId") %>'
                                                    runat="server" />
                                                <asp:HiddenField ID="hdnLikeUserPost" Value='<%# Eval("LikeUserPostId") %>' runat="server" />
                                                <div class="commentContainer" style="width: 100%;">
                                                    <div class="commentIcon">
                                                        <img src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' id="imgprofile" runat="server" alt=""
                                                            class="commentPhoto" width="42" height="42" />
                                                        <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                                    </div>
                                                    <div class="comments">
                                                        <img src="images/right-arrow.jpg" class="rightArrow" alt="" />
                                                        <div class="commentTxtNew">
                                                            <span>
                                                                <asp:LinkButton Style="color: #40bfc4; font-size: 14px; text-decoration: none;" ID="Label1"
                                                                    Font-Underline="false" CommandName="Details" runat="server" Text='<%#Eval("UserName") %>'></asp:LinkButton>
                                                                <div id="divshare" style="display: none" runat="server">
                                                                    Shared&nbsp;
                                                                    <asp:LinkButton ForeColor="#006699" ID="lnkSharedUserName" CommandName="SharedDetails"
                                                                        runat="server" Font-Bold="false" Text='<%#Eval("SharedUserName")+ "&#39;s"%>'
                                                                        Font-Italic="true"></asp:LinkButton>&nbsp;
                                                                    <asp:Label ID="lblSahreType" runat="server" Font-Size="Small" ForeColor="black" Font-Italic="true"
                                                                        Font-Bold="false"></asp:Label>
                                                                </div>
                                                            </span>&nbsp;&nbsp;<asp:Label Style="color: #6D6E71; font-size: 13px; padding-bottom: 10px;"
                                                                ID="lblPostDescription" Text='<%# Eval("strPostDescription") %>' runat="server"></asp:Label>
                                                        </div>
                                                        <div class="">
                                                            <p>
                                                                <asp:HyperLink ID="hplLinkUrl" ClientIDMode="Static" Target="_blank" ToolTip="Posted Link"
                                                                    NavigateUrl='<%#"http://"+Eval("strPostLink") %>' Text='<%#"http://"+Eval("strPostLink") %>'
                                                                    Font-Size="Small" runat="server"></asp:HyperLink>
                                                                <img src='<%# "UploadedPhoto/"+Eval("strPhotoPath")%>' id="imgPhoto" runat="server" alt=""
                                                                    width="200" height="200" style="padding-left: 10px; padding-top: 10px;" />
                                                                <div id="dvVideo" runat="server" clientidmode="Static">
                                                                    <embed id="frm1" src='<%# "VideoFiles/"+Eval("strVideoPath")%>' clientidmode="Static"
                                                                        starttime="00:00" controls="true" autoplay="false" autostart="false" quality="high"
                                                                        cache="true" correction="full" pluginurl="http://quicktime.en.softonic.com/download"
                                                                        width="400" height="300" scale="aspect" pluginspage="http://quicktime.en.softonic.com/download" />
                                                                    <a id="lbtnControlPanel" title="Play Video" style="cursor: pointer;" runat="server"
                                                                        clientidmode="Static" onclick="CPhit(this);"></a>
                                                                </div>
                                                                <div id="divChrome" runat="server" clientidmode="Static">
                                                                    <div id='media-player'>
                                                                        <video id='media-video' controls>
                                                                                <source src='<%# "VideoFiles/"+Eval("strVideoPath")%>' type='video/mp4'>
                                                                                <source src='<%# "VideoFiles/"+Eval("strVideoPath")%>' type='video/mp3'>
                                                                                <source src='<%# "VideoFiles/"+Eval("strVideoPath")%>' type='video/webm'>
                                                                                <source src='<%# "VideoFiles/"+Eval("strVideoPath")%>' type='video/x-msvideo'>
                                                                                <object type="application/x-shockwave-flash"
                                                                                <param name="movie" value='<%# "VideoFiles/"+Eval("strVideoPath")%>' />
                                                                                <param name="flashvars" value="controls=true&file=<%# "VideoFiles/"+Eval("strVideoPath")%>" />
                                                                                </object>
                                                                            </video>
                                                                    </div>
                                                                </div>
                                                            </p>
                                                        </div>
                                                        <div class="actions">
                                                            <div class="posted">
                                                                <asp:Label ID="lblAddedOn" Text='<%# Eval("dtAddedOn") %>' Style="font-size: 12px;"
                                                                    runat="server"></asp:Label></div>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="forumLike">
                                                                        <img src="<%=ResolveUrl("images/like-gray.png")%>" alt="">
                                                                        <asp:Label ID="lnkLikePost" runat="server" Text='<%#Eval("Likes") %>' ToolTip="View Likes"></asp:Label>.
                                                                        <asp:LinkButton Style="color: #6D6E71;" Font-Underline="false" ID="lnkLike" runat="server"
                                                                            Text="Like" CommandName="Like Post"></asp:LinkButton></div>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="lnkLike" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                            <div class="forumReply">
                                                                <img src="<%=ResolveUrl("images/reply-gray.png")%>" alt="" />
                                                                <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Comments") %>' ClientIDMode="Static"></asp:Label>.
                                                                <asp:LinkButton Style="color: #6D6E71;" ID="lnkComment" Font-Underline="false" Text="Comment"
                                                                    runat="server" CommandName="Comment"></asp:LinkButton>
                                                            </div>
                                                            <div id="dvSharelink" style="display: none;" runat="server" class="forumShare">
                                                                <asp:UpdatePanel ID="up1" runat="server">
                                                                    <ContentTemplate>
                                                                        <img src="images/share-gray.png" alt="">
                                                                        <asp:Label ID="lblShare" runat="server" Text='<%#Eval("Share") %>'></asp:Label>.
                                                                        <asp:LinkButton Style="color: #6D6E71;" ID="lnkShare" Font-Underline="false" Text="Share"
                                                                            runat="server" CommandName="Share" ToolTip="Share"></asp:LinkButton>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="lnkShare" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div style="float: right; margin-right: 3%;">
                                                                <asp:LinkButton ID="lnkEditPost" Font-Size="Small" Font-Underline="false" Visible="false"
                                                                    ToolTip="Edit" Text="Edit" Style="color: #6D6E71" CommandName="Edit Post" CausesValidation="false"
                                                                    runat="server">                                      
                                                                </asp:LinkButton>
                                                                <asp:Label ID="lblPipe" runat="server" Font-Size="Small" Visible="false" ForeColor="black"
                                                                    Text="&nbsp;&nbsp;|&nbsp;&nbsp;" Font-Bold="false"></asp:Label>
                                                                <asp:LinkButton ID="lnkDeleteComment" Font-Underline="false" Font-Size="Small" Visible="false"
                                                                    ToolTip="Delete" Text="Delete" Style="float: right; color: #6D6E71" CausesValidation="false"
                                                                    runat="server" CommandName="Delete Post" OnClientClick="docdelete();">                                       
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="cls">
                                                        </div>
                                                        <!--comments section starts-->
                                                        <div class="commentSection">
                                                            <asp:UpdatePanel ID="upcmnty" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="viewAllComments">
                                                                        <asp:LinkButton ID="lbchldlstHideShow" CommandName="Hide/ShowComments" runat="server"
                                                                            ToolTip="View Comments" Style="color: #6D6E71; text-decoration: none;" Text='<%# String.Concat("View all","  " , Eval("Comments")," ", "Comments")%>'
                                                                            ClientIDMode="Static"></asp:LinkButton>
                                                                        <asp:ImageButton ID="imgUpDown" CommandName="Hide/ShowComments" runat="server" ImageUrl="images/up.jpg" />
                                                                    </div>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="lbchldlstHideShow" EventName="Command" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                            <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ListView ID="lstChild" runat="server" OnItemCommand="lstChild_ItemCommand" DataKeyNames="intID">
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hdnCommentId" runat="server" Value='<%#Eval("intID") %>' />
                                                                            <asp:HiddenField ID="hdnintUserTypeId" runat="server" Value='<%#Eval("intUserTypeId") %>' />
                                                                            <asp:HiddenField ID="hdnRegistrationId" runat="server" Value='<%#Eval("intRegistrationId") %>' />
                                                                            <asp:HiddenField ID="hdnCommentLikeId" runat="server" Value='<%#Eval("CommentLikeId") %>' />
                                                                            <!--comment set start-->
                                                                            <div class="commentSet">
                                                                                <div class="commnetBy">
                                                                                    <img src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' id="imgCommentpic" runat="server" alt=""
                                                                                        class="commentPhoto" width="35" height="35" />
                                                                                    <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                                                                </div>
                                                                                <div class="commentInfo">
                                                                                    <p>
                                                                                        <span>
                                                                                            <asp:LinkButton Style="color: #40bfc4; font-size: 12px; text-decoration: none;" ID="Label1"
                                                                                                Font-Underline="false" CommandName="Post Comment Details" runat="server" Text='<%#Eval("UserName") %>'></asp:LinkButton>.</span>
                                                                                        &nbsp;<asp:Label ID="lblstr" Style="color: #6D6E71; font-size: 13px; padding-bottom: 10px;"
                                                                                            runat="server" Text='<%#Eval("strComment") %>'></asp:Label></p>
                                                                                    <div class="commentD">
                                                                                        <asp:UpdatePanel ID="upda" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <div class="posted">
                                                                                                    <asp:Label ID="lblPostedOn" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label>
                                                                                                </div>
                                                                                                <div class="forumLike">
                                                                                                    <img src="images/like-gray.png" alt="">
                                                                                                    <asp:Label ID="lnkLikeComment" runat="server" Text='<%#Eval("Likes") %>' ClientIDMode="Static"
                                                                                                        ToolTip="View Likes"></asp:Label>.
                                                                                                    <asp:LinkButton Style="color: #6D6E71;" ID="lnkLikes" Font-Underline="false" runat="server"
                                                                                                        Text="Like" CommandName="Like Comment" ToolTip="Like Comment"></asp:LinkButton></div>
                                                                                                <div style="float: right;">
                                                                                                    <asp:LinkButton ID="lnkEditComment" Font-Size="Small" Font-Underline="false" Visible="false"
                                                                                                        ToolTip="Edit" Text="Edit" Style="color: #6D6E71" CommandName="Edit Comment"
                                                                                                        CausesValidation="false" runat="server">                                       
                                                                                                    </asp:LinkButton>
                                                                                                    <asp:Label ID="lblPipe" runat="server" Font-Size="Small" Visible="false" ForeColor="black"
                                                                                                        Text="&nbsp;&nbsp;|&nbsp;&nbsp;" Font-Bold="false"></asp:Label>
                                                                                                    <asp:LinkButton ID="lnkDeleteComment" Font-Underline="false" Font-Size="Small" Visible="false"
                                                                                                        ToolTip="Delete" OnClientClick="javascript:docdelete();" Text="Delete" Style="float: right;
                                                                                                        color: #6D6E71" CommandName="Delete Comment" CausesValidation="false" runat="server">                                        
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                            <Triggers>
                                                                                                <asp:AsyncPostBackTrigger ControlID="lnkLikes" />
                                                                                            </Triggers>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <!--comment set end-->
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Panel ID="pnlEnterComment" runat="server" ClientIDMode="Predictable" DefaultButton="lnkEnterComment">
                                                                                <asp:TextBox ID="txtComment" runat="server" CssClass="commentWrite"></asp:TextBox>
                                                                                <ajax:TextBoxWatermarkExtender TargetControlID="txtComment" ID="TextBoxWatermarkExtender2"
                                                                                    runat="server" WatermarkText="Write a comment">
                                                                                </ajax:TextBoxWatermarkExtender>
                                                                                <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment"
                                                                                    Display="Dynamic" ValidationGroup="comment" Style="padding-left: 11px;" ErrorMessage="Please write a comment"
                                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                <asp:Label Style="display: none;" ID="lblCommentError" runat="server"></asp:Label>
                                                                                <asp:LinkButton ID="lnkEnterComment" Style="display: none" CommandArgument='<%# Eval("Id") %>'
                                                                                    CommandName="EnterComment" runat="server" Text="Enter"></asp:LinkButton>
                                                                            </asp:Panel>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <!--comments section ends-->
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        <p id="pLoadMore" runat="server" align="center">
                                            <asp:ImageButton ID="imgLoadMore" runat="server" ClientIDMode="Static" ImageUrl="~/images/loadingIcon.gif"
                                                OnClick="imgLoadMore_OnClick" Height="100px" Width="100px" />
                                        </p>
                                        <p align="center">
                                            <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server"
                                                ClientIDMode="Static" ForeColor="Red" Visible="false"></asp:Label>
                                        </p>
                                        <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
                                        <div style="display: none" class="innerContainer">
                                            <div class="bgLine" id="pagination">
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
                                                <asp:LinkButton ID="lnkLast" runat="server" Style="background: none" OnClick="lnkLast_Click"><img src="<%=ResolveUrl("images/spacer.gif")%>" class="last" alt="" /></asp:LinkButton>
                                                <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                                            </div>
                                        </div>
                                        <!--section ends-->
                                    </div>
                                    <!--wall right column starts-->
                                    <asp:HiddenField ID="hdnTabId" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnPostId" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnLoader" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdncommentfocus" runat="server" ClientIDMode="Static" />
                                </div>
                                <%-- </div>--%>
                                <div class="cls">
                                    <p>
                                        &nbsp;</p>
                                </div>
                            </div>
                        </div>
                        <!--box starts-->
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkDummyPost" />
                    <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
                    <asp:AsyncPostBackTrigger ControlID="LinkButton1" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hdnPhoto" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnDocName" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnErrorMsg" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnImage" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnVideo" runat="server" ClientIDMode="Static" />
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
                    var ID = "#" + $("#hdnTabId").val();
                    $(ID).focus();
                });

                $(document).ready(function () {
                    var ID = "#" + $("#hdnPostId").val();
                    $(ID).focus();
                });

                $(document).ready(function () {
                    var ID = "#" + $("#hdnLoader").val();
                    $(ID).focus();

                });

                $(document).ready(function () {
                    var ID = "#" + $("#hdncommentfocus").val();
                    $(ID).focus();

                });

                $(document).ready(function () {
                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                    prm.add_endRequest(function () {
                        var ID = "#" + $("#hdnTabId").val();
                        $(ID).focus();

                        var ID1 = "#" + $("#hdnPostId").val();
                        $(ID1).focus();

                        var ID2 = "#" + $("#hdnLoader").val();
                        $(ID2).focus();

                        var ID3 = "#" + $("#hdncommentfocus").val();
                        $(ID3).focus();
                    });
                });
            </script>
            <script type="text/javascript">
                $(".viewAllComments").click(function () {
                    $("#lstChild").slideToggle("slow");
                    $(".viewAllComments").toggle();
                });
            </script>
            <script type="text/jscript">
                function Cancel() {
                    document.getElementById("divCancelPopup").style.display = 'none';
                    return false;
                }
            </script>
        </div>
    </div>
    <script type="text/javascript">
            function showImage(image) {
                var src = "";
                if (image == 1) {
                    src = $("#icon1").attr("src");
                }
                else if (image == 2) {
                    src = $("#icon2").attr("src");
                }
                else if (image == 3) {
                    src = $("#icon3").attr("src");
                }
                else if (image == 4) {
                    src = $("#icon4").attr("src");
                }

                if (src == "images/icon-1.jpg") {
                    document.getElementById("icon1").src = src;
                    document.getElementById("icon2").src = "images/icon-2.jpg";
                    document.getElementById("icon3").src = "images/icon-3.jpg";
                    document.getElementById("icon4").src = "images/icon-4.jpg";
                    document.getElementById('div1').style.display = 'block';
                    document.getElementById('div2').style.display = 'none';
                    document.getElementById('div3').style.display = 'none';
                    document.getElementById('div4').style.display = 'none';
                    $("#ddlPostType").css("margin-top", "3px");
                    return;
                }
                else if (src == "images/icon-2.jpg") {
                    document.getElementById("icon1").src = src;
                    document.getElementById("icon2").src = "images/icon-1.jpg";
                    document.getElementById("icon3").src = "images/icon-3.jpg";
                    document.getElementById("icon4").src = "images/icon-4.jpg";
                    document.getElementById('div1').style.display = 'block';
                    document.getElementById('div2').style.display = 'block';
                    document.getElementById('div3').style.display = 'none';
                    document.getElementById('div4').style.display = 'none';
                    $("#ddlPostType").css("margin-top", "-40px");
                }

                else if (src == "images/icon-3.jpg") {
                    document.getElementById("icon1").src = src;
                    document.getElementById("icon2").src = "images/icon-1.jpg";
                    document.getElementById("icon3").src = "images/icon-2.jpg";
                    document.getElementById("icon4").src = "images/icon-4.jpg";
                    document.getElementById('div1').style.display = 'block';
                    document.getElementById('div2').style.display = 'none';
                    document.getElementById('div3').style.display = 'block';
                    document.getElementById('div4').style.display = 'none';
                    $("#ddlPostType").css("margin-top", "-40px");
                    return;
                }
                else if (src == "images/icon-4.jpg") {
                    document.getElementById("icon1").src = src;
                    document.getElementById("icon2").src = "images/icon-1.jpg";
                    document.getElementById("icon3").src = "images/icon-2.jpg";
                    document.getElementById("icon4").src = "images/icon-3.jpg";
                    document.getElementById('div1').style.display = 'block';
                    document.getElementById('div2').style.display = 'none';
                    document.getElementById('div3').style.display = 'none';
                    document.getElementById('div4').style.display = 'block';
                    $("#ddlPostType").css("margin-top", "-60px");
                    return;
                }
            }   
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(":input[data-watermark]").each(function () {
                $(this).val($(this).attr("data-watermark"));
                $(this).bind('focus', function () {
                    if ($(this).val() == $(this).attr("data-watermark")) $(this).val('');
                });
                $(this).bind('blur', function () {
                    if ($(this).val() == '') $(this).val($(this).attr("data-watermark"));
                    $(this).css('color', '#a8a8a8');
                });
            });
        });

        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(":input[data-watermark]").each(function () {
                    $(this).val($(this).attr("data-watermark"));
                    $(this).bind('focus', function () {
                        if ($(this).val() == $(this).attr("data-watermark")) $(this).val('');
                    });
                    $(this).bind('blur', function () {
                        if ($(this).val() == '') $(this).val($(this).attr("data-watermark"));
                        $(this).css('color', '#a8a8a8');
                    });
                });
            });
        });
    </script>
    <script type="text/javascript">
        function ShowLoading(id) {
            location.href = '#' + id;
        }
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            document.getElementById("hdnVideo").value = "";
            document.getElementById("hdnImage").value = "";
            document.getElementById("PhotoUpload").onchange = function () {
                document.getElementById("hdnImage").value = this.value;
                document.getElementById("hdnVideo").value = "";
            };
            document.getElementById("VideoUpload").onchange = function () {
                document.getElementById("hdnVideo").value = this.value;
                document.getElementById("hdnImage").value = "";
            };
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                document.getElementById("hdnVideo").value = "";
                document.getElementById("hdnImage").value = "";
                document.getElementById("PhotoUpload").onchange = function () {
                    document.getElementById("hdnImage").value = this.value;
                    document.getElementById("hdnVideo").value = "";
                };
                document.getElementById("VideoUpload").onchange = function () {
                    document.getElementById("hdnVideo").value = this.value;
                    document.getElementById("hdnImage").value = "";
                };
            });
        });
        function validate() {
            $('#lnkPostUpdate').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtPostUpdate').text() == '') {
                                setTimeout(
                                function () {
                                    $('#lnkPostUpdate').css("box-shadow", "0px 0px 0px #00B7E5");
                                }, 1000);
            }
            $("#hdnPhoto").val('');
            $("#hdnDocName").val('');
            $("#hdnErrorMsg").val('');
            if ($('#txtPostUpdate').val() == "" || $('#txtPostUpdate').val() == "What's on your mind?") {
                document.getElementById('RequiredFieldValidator1').style.display = "block";
                return false;
            }
            if (document.getElementById('div2').style.display == 'block') {
                var fileUpload = $("#PhotoUpload").get(0);
                if (fileUpload.files.length > 0) {
                    SaveFile();
                }
                else {
                    document.getElementById('lblErrorMess').innerHTML = 'Please select a photo.';
                    return false;
                }
            }
            else if (document.getElementById('div3').style.display == 'block') {
                var fileUpload = $("#VideoUpload").get(0);
                if (fileUpload.files.length > 0) {
                    SaveFile();
                }
                else {
                    document.getElementById('lblErrorvideo').innerHTML = 'Please select a video.';
                    return false;
                }
            }
            else if (document.getElementById('div4').style.display == 'block') {
                var PostLink = document.getElementById('txtPostLink').value;
                if (PostLink == "Link") {
                    document.getElementById('lblLink').innerHTML = 'Please paste a link.';
                    return false;
                }
                else {
                    document.getElementById('lnkDummyPost').click();
                }

            }
            else {
                document.getElementById('lnkDummyPost').click();
            }
        }  //End of function validate.
        function ClearOrgText() {
            document.getElementById('hdnintStatusUpdateId').value = "";
            document.getElementById('txtPostUpdate').value = "";
            $('#txtPostUpdate').attr("placeholder", "What's on  your mind ?").blur();
            document.getElementById('txtPostLink').value = "Link";
            document.getElementById('PhotoUpload').value = "";
            document.getElementById('VideoUpload').value = "";
            return true;
        }
    </script>
    <script type="text/javascript">
        function MesProfileClose() {
            document.getElementById("divGroupSucces").style.display = 'none';
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById('imgLoadMore').style.display = 'none';
        });

        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                document.getElementById('imgLoadMore').style.display = 'none';
            });
        });
    </script>
    <script type="text/javascript">
        $(window).scroll(function () {
            if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                document.getElementById('imgLoadMore').style.display = 'block';
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
        function docdelete() {
            $('#divDeletesucess').css("display", "block");
        }
        function divCancels() {
            $('#divDeletesucess').css("display", "none");
        }
    </script>
    <script type="text/javascript">
        function SaveFile() {
            var fileUpload = $("#PhotoUpload").get(0);
            var videoUpload = $("#VideoUpload").get(0);
            if (fileUpload.files.length > 0) {
                var files = fileUpload.files;
                var data = new FormData();
                for (var i = 0; i < files.length; i++) {
                    data.append(files[i].name, files[i]);
                }
            }
            else if (videoUpload.files.length > 0) {
                var files = videoUpload.files;
                var data = new FormData();
                for (var i = 0; i < files.length; i++) {
                    data.append(files[i].name, files[i]);
                }
            }
            $.ajax({
                type: "POST",
                url: "handler/FileUploadHandlerHome.ashx",
                contentType: false,
                processData: false,
                data: data,
                success: function (result) {
                    if (result == 'File format not supported.' || result == 'File size should be less than or equal to 3MB.' || result == 'Please choose a video file less than 12MB.') {
                        $("#hdnErrorMsg").val(result)
                    }
                    else {
                        var v = result.split(":");
                        $("#hdnPhoto").val(v[0]);
                        $("#hdnDocName").val(v[1]);
                    }
                    document.getElementById("lnkDummyPost").click();
                },
                error: function () {
                    alert("There was error uploading files!");
                }
            });
        }
    </script>
</asp:Content>
