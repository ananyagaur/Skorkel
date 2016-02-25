<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" MasterPageFile="~/Main.master"
    MaintainScrollPositionOnPostback="true" Inherits="Home" %>

<%@ Register Src="~/UserControl/CropImg.ascx" TagName="CropImage" TagPrefix="crp1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ContentPlaceHolderID="headMain" runat="server" ID="contentHead">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="<%=ResolveUrl("js/jquery-1.8.2.min.js")%>" type="text/javascript" language="javascript"></script>
    <script src="<%=ResolveUrl("js/jquery.autocomplete.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("js/ddsmoothmenu.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("js/Blob.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
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
                                        <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes" OnClientClick="divdeletes();" 
                                            CssClass="joinBtn" OnClick="lnkDeleteConfirm_Click"></asp:LinkButton>
                                    </td>
                                    <td style="padding-right: 20px;">
                                        <asp:LinkButton ID="lnkDeleteCancel" runat="server" ClientIDMode="Static" Text="Cancel"
                                            Style="float: left; text-align: center; text-decoration: none; width: 82px;
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
                <div class="innerSignUpTabs">
                    <!--top profile starts-->
                    <asp:HiddenField ID="hdnConnCommandName" runat="server" ClientIDMode="Static" Value="" />
                    <asp:HiddenField ID="hdnloginIds" runat="server" ClientIDMode="Static" Value="" />
                    <asp:HiddenField ID="hdnUserID" runat="server" ClientIDMode="Static" Value="" />
                    <div style="display: none;">
                        <asp:Button ID="btnAccept" runat="server" ClientIDMode="Static" OnClick="ClickAccept" />
                    </div>
                    <asp:UpdatePanel ID="upProfileMain" runat="server">
                        <ContentTemplate>
                            <div class="tabProfileBox">
                                <div class="photoIcon">
                                    <img id="imgUser" runat="server" height="160" width="160" clientidmode="Static" />
                                    <asp:LinkButton ID="lnkChangeImage" CssClass="camera" runat="server" ToolTip="Change Profile Image"
                                        ClientIDMode="Static" OnClick="lnkChangeImage_Click" OnClientClick="javascript:CallCameraload();">
                             <img id="imgCamera" src="images/camera-icon.png" style="display: none;" />
                                    </asp:LinkButton>
                                    <div id="divProilePic" style="display: none; margin: -105px 0px 0px 65px;">
                                        <img id="imgCameraLoad" src="images/Loadgif.gif" style="display: block;" />
                                    </div>
                                </div>
                                <div class="CropImagepopupProfile" id="PopUpCropImage" clientidmode="Static" runat="server"
                                    style="display: none;">
                                    <div class="popUp1" id="PopUpCropImage1" clientidmode="Static" runat="server" style="z-index: 1000;">
                                        <div class="popUp" id="popUp" style="height: 475px; width: 625px;">
                                            <div class="popUpBox" style="height: 480px; margin: 10% 0px 0px 12%; width: 745px;
                                                overflow: auto">
                                                <p class="shareContent" style="margin-top: 5px;">
                                                    Change Image</p>
                                                <div style="">
                                                    <crp1:CropImage ID="crp1" runat="server" />
                                                    <br />
                                                    <asp:LinkButton ID="lnkCancelProfilediv" runat="server" Text="Cancel" CssClass="cancelcropdivHome"
                                                        ClientIDMode="Static" Style="float: left; margin-left: 3px; margin-top: -20px;"> </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="photoIconDtls">
                                    <p class="photoIconDtlsName">
                                        <asp:Label ID="lblUserProfName" runat="server" Text=""></asp:Label>
                                    </p>
                                    <br />
                                    <asp:LinkButton ID="lnkAddFriend" runat="server" ClientIDMode="Static" class="addFriend"
                                        Text="Add Friend" OnClientClick="javascript:ShowAddUserProcess();" Style="display: none;
                                        width: 105px; text-align: center;" OnClick="lnkAddFriend_Click"></asp:LinkButton>
                                    <asp:Button ID="lnkConnectedUser" runat="server" class="ConnectedUser" Text="Connected"
                                        OnClientClick="return false;" Style="display: none;"></asp:Button>
                                    <div id="divAddUser" style="display: none; margin-left: 155px; margin-top: -32px;">
                                        <img src="images/Loadgif.gif" /></div>
                                </div>
                                <div class="endMsg">
                                    <div class="endorsements">
                                        <p class="txtT">
                                            <asp:LinkButton ID="lblEndorseCount" runat="server" Text="" Style="text-decoration: none !important;
                                                color: #00b5bc;" OnClick="lblEndorseCount_click" OnClientClick="divdeletes();" ></asp:LinkButton>
                                        </p>
                                        <p class="subTxtT">
                                            Endorsements</p>
                                    </div>
                                    <div class="messages">
                                        <p class="txtT">
                                            <asp:Label ID="lblMessCount" runat="server" Text=""></asp:Label></p>
                                        <p class="subTxtT">
                                            Score</p>
                                    </div>
                                </div>
                                <!--edit starts-->
                                <div class="EditProfilepopupHome" id="divEditProfile" clientidmode="Static" runat="server"
                                    style="display: none;">
                                    <div class="editNamePop" id="divEditProfiles" style="margin: 11% 0px 0 56%;">
                                        <p>
                                            <asp:TextBox ID="txtName" runat="server" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
                                        </p>
                                        <p>
                                            <asp:TextBox ID="txtGender" runat="server" placeholder="Gender" ClientIDMode="Static"></asp:TextBox>
                                        </p>
                                        <p>
                                            <asp:TextBox ID="txtLanguage" runat="server" placeholder="Languages Known" ClientIDMode="Static"></asp:TextBox>
                                        </p>
                                        <asp:TextBox ID="txtEmailId" runat="server" placeholder="Email Id" Enabled="false"
                                            ClientIDMode="Static"></asp:TextBox>
                                        <p>
                                            <asp:TextBox ID="txtPhone" runat="server" ClientIDMode="Static" MaxLength="11" placeholder="Phone Number"></asp:TextBox>
                                        </p>
                                        <p style="margin-left: 42px;">
                                            <asp:Label ID="lblProfilemsg" runat="server" ForeColor="Red"></asp:Label>
                                        </p>
                                        <p class="btnEdit">
                                            <asp:LinkButton ID="lnkCancelProfile" CssClass="editNameCancel" runat="server" Text="Cancel"
                                                ClientIDMode="Static" OnClientClick="javascript:callBodyEnable();return false;"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkProfileSave" CssClass="vote lnkProfileSaves" runat="server"
                                                Text="Save" OnClick="lnkProfileSave_click" OnClientClick="javascript:callBodyEnable();"></asp:LinkButton>
                                        </p>
                                    </div>
                                </div>
                                <!--edit starts-->
                                <!--edit starts-->
                                <div class="editIcon editName" id="divEditUserProfile" runat="server" style="margin: -85px 20px 0 180px;">
                                    <div id="smoothmenu1" class="ddsmoothmenu">
                                        <ul class="iconUl">
                                            <li><a href="#">
                                                <img src="images/edit.png" /></a>
                                                <ul>
                                                    <li style="border-bottom: 1px solid #cdced0;">
                                                        <asp:LinkButton ID="lnkeditProfile" runat="server" Text="Edit" OnClientClick="javascript:CallUserJSON();javascript:callBodyDisable();return false;"></asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </li>
                                        </ul>
                                        <br style="clear: left" />
                                    </div>
                                </div>
                                <!--edit starts-->
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnkChangeImage" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <!--top profile ends-->
                    <div id="divDisp" class="cls" runat="server" style="height: 230px; display: none;">
                    </div>
                    <!--tabs starts-->
                    <div id="divdisplayWall" runat="server" style="display: block;">
                        <asp:UpdatePanel ID="UpdatePanelTab" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="innerTabBox">
                                    <ul id="homeId" class="tabsProfile">
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkHome" class="innerTabHome" Text="Home" OnClick="lnkHome_Click"></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkDocuments" class="innerTabDoc" Text="Documents"
                                                OnClick="lnkDocuments_Click"></asp:LinkButton></li>
                                        <li style="width: 250px;">
                                            <asp:LinkButton runat="server" ID="lnkWorkex" class="innerWrkex" Text="Work experience"
                                                Width="250px" OnClick="lnkWorkex_Click"></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkEducation" class="innerEdu" Text="Education"
                                                OnClick="lnkEducation_Click"></asp:LinkButton></li>
                                        <li style="border: 0;">
                                            <asp:LinkButton runat="server" ID="lnkAchievements" class="innerAch" Text="Achievements"
                                                OnClick="lnkAchievements_Click"></asp:LinkButton></li>
                                    </ul>
                                </div>
                                <!--tabs ends-->
                                <!--HOME START-->
                                <div id="PnlHome1">
                                    <asp:UpdatePanel ID="PnlHome" runat="server" Visible="false" UpdateMode="Conditional"
                                        ClientIDMode="Static">
                                        <ContentTemplate>
                                            <!--left section starts-->
                                            <div class="leftSection">
                                                <div class="fGroupBox frd" id="divfrdgrp">
                                                    <a href="#" id="imgFriend">
                                                        <img src="images/friends.png" align="absmiddle" style="margin-top: -4px;" />Friends</a>
                                                    <a href="#" id="imgGroup">
                                                        <img src="images/groups.png" align="absmiddle" style="margin-top: -2px;" />Groups</a>
                                                </div>
                                                <div class="cls">
                                                </div>
                                                <!--Friend section starts-->
                                                <div class="groupSection" id="divFriendSection" style="display: block; margin: 10px 0 0 20px;">
                                                    <asp:ListView runat="server" ID="lstFrnds" OnItemDataBound="lstFrnds_ItemDataBound"
                                                        CellPadding="9">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                                            <div class="photoHomeProfile">
                                                                <asp:Image ID="imgfrnd" runat="server" ImageUrl='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>'
                                                                    ToolTip="View Friend Details" Height="37px" Width="37px"></asp:Image>
                                                            </div>
                                                            <div class="viewerName" style="width: 148px; margin: -30px 0px 25px 65px;">
                                                                <p class="viewerCommentTxt">
                                                                    <a style="color: #6D6E71; text-decoration: none;" title="View Friend Details" href='<%= ResolveUrl("Home.aspx") %>?RegId=<%#Eval("intInvitedUserId")%> '>
                                                                        <asp:Label runat="server" ID="lblName" Text='<%#Eval("Name")%>' Style="color: Black;"></asp:Label></a>
                                                                </p>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                    <p>
                                                        &nbsp;</p>
                                                    <center>
                                                        <a id="aAddConnection" runat="server" href="~/SearchPeople.aspx" class="addGroup"
                                                            clientidmode="Static" onclick="CallAddFriends();">Add Friend</a></center>
                                                    <p>
                                                        &nbsp;</p>
                                                    <div class="cls">
                                                    </div>
                                                </div>
                                                <!--Frend section ends-->
                                                <!--group section starts-->
                                                <div class="groupSection" id="divgroupSection" style="display: none; margin: 10px 0 0 20px;">
                                                    <asp:Repeater ID="rptGroup" runat="server" OnItemDataBound="rptGroup_ItemDataBound">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnGroupId" runat="server" ClientIDMode="Static" Value='<%#Eval("inGroupId")%>' />
                                                            <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("strLogoPath") %>' />
                                                            <div class="photoHomeProfile">
                                                                <asp:Image ID="img_myGrp" ToolTip="View Group Details" runat="server" ImageUrl='<%# "CroppedPhoto/"+Eval("strLogoPath")%>'
                                                                    Height="37px" Width="37px"></asp:Image>
                                                            </div>
                                                            <div class="viewerName" style="width: 148px; margin: -30px 0px 25px 65px;">
                                                                <p class="viewerCommentTxt breakallwords">
                                                                    <a style="color: #6D6E71; text-decoration: none;" title="View Friend Details" href='<%# "Group-Profile.aspx?GrpId="+ Eval("inGroupId") %>'>
                                                                        <asp:Label runat="server" ID="lblName" Text='<%#Eval("strGroupName")%>' Style="color: Black;"></asp:Label>
                                                                    </a>
                                                                </p>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <p>
                                                        &nbsp;</p>
                                                    <asp:LinkButton ID="lnkNewGroups" runat="server" Font-Underline="false" Text="Add Group"
                                                        ClientIDMode="Static" CssClass="addGroup" OnClick="lnkAddNewGrp_Click" OnClientClick="CallNewGroups();"></asp:LinkButton>
                                                    <p>
                                                        &nbsp;</p>
                                                </div>
                                                <!--group section ends-->
                                            </div>
                                            <!--left section ends-->
                                            <!--right section starts-->
                                            <asp:UpdatePanel ID="uppostupdate" runat="server">
                                                <ContentTemplate>
                                                    <div class="rightSection">
                                                        <textarea id="txtPostUpdate" runat="server" clientidmode="Static" class="txtAreaTab"
                                                            placeholder="Share an update..." style="font-size: 16px;"></textarea>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPostUpdate"
                                                            Display="Dynamic" ValidationGroup="posts" ErrorMessage="Please write a post."
                                                            ForeColor="Red" ClientIDMode="Static"></asp:RequiredFieldValidator>
                                                        <asp:Label ID="lblPostMsg" ClientIDMode="Static" Text="Please write a post." Style="display: none;
                                                            color: Red;" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnintStatusUpdateId" ClientIDMode="Static" Value="" runat="server" />
                                                        <asp:Label ID="lblintCommentId" Style="display: none;" ClientIDMode="Static" Text=""
                                                            runat="server"></asp:Label>
                                                        <a id="lnkPostUpdate" runat="server" clientidmode="Static" href="#" class="postBtntab"
                                                            onclick="return false;">Post</a>
                                                        <asp:Button ID="lnkDummyPost" class="postBtntab" runat="server" Style="display: none;"
                                                            OnClick="lnkPostUpdate_Click" ClientIDMode="Static" ValidationGroup="posts" CausesValidation="true">
                                                        </asp:Button>
                                                        <div class="fileUploadHome UploadFilesHomeHome">
                                                            <span>Upload Media</span>
                                                            <asp:FileUpload ID="FileUplogo" ClientIDMode="Static" runat="server" CssClass="upload"
                                                                name="file_1" Style="display: block;" />
                                                        </div>
                                                        <div id="divPostimage" style="display: none; margin: 70px 0px 0px 401px;">
                                                            <img id="imgPostSave" src="images/Loadgif.gif" />
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="lblfilename" ClientIDMode="Static" Text="." runat="server" ForeColor="White"></asp:Label>
                                                        </div>
                                                        <asp:ImageButton runat="Server" ID="imgBtnDelete" Style="margin: 0px 0px 0px 1px;
                                                            display: none;" ImageUrl="~/images/deleteCross.png" OnClientClick="javascript:removeimages();"
                                                            CausesValidation="false" ClientIDMode="Static" ToolTip="Remove"></asp:ImageButton>
                                                        <img id="imgselect" src="" width="100" height="100" style="display: none; margin-top: -12px;
                                                            margin-left: 15px;" />
                                                        <video id="Mediavideo" src="" controls="controls" autostart="false" quality="high"
                                                            style="display: none; margin-top: -12px; margin-left: 15px;"></video>
                                                        <asp:Label ID="lblVideomsg" ClientIDMode="Static" Text="Missing Plug-in" runat="server"
                                                            Style="display: none; margin-left: 30px;"></asp:Label>
                                                        <!-- Post Start -->
                                                        <asp:HiddenField ID="hdnintPostId" Value="" ClientIDMode="Static" runat="server" />
                                                        <asp:HiddenField ID="hdnstrPostDescriptiondele" Value="" ClientIDMode="Static" runat="server" />
                                                        <asp:HiddenField ID="hdnintPostceIdelet" Value="" ClientIDMode="Static" runat="server" />
                                                        <asp:HiddenField ID="hdnstrPostDescriptioncedel" Value="" ClientIDMode="Static" runat="server" />
                                                        <asp:ListView ID="lstPostUpdates" runat="server" OnItemCommand="lstPostUpdates_ItemCommand"
                                                            OnItemDataBound="lstPostUpdates_ItemDataBound" OnItemCreated="lstPostUpdates_ItemCreated">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnRegistrationId" Value='<%#Eval("intAddedBy") %>' runat="server" />
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
                                                                <asp:HiddenField ID="hdnstrPostDescription" runat="server" Value='<%# Eval("strPostDescription") %>' />
                                                                <div class="tabContainer">
                                                                    <div class="tabCmtBox">
                                                                        <div class="tabImg showMouseOver">
                                                                            <img src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' id="imgprofile" height="45"
                                                                                width="45" runat="server" />
                                                                            <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                                                        </div>
                                                                        <asp:UpdatePanel ID="UP22" runat="server">
                                                                            <ContentTemplate>
                                                                                <div class="tabTxtInfo">
                                                                                    <p class="name">
                                                                                        <asp:LinkButton Style="color: #38A3AB; text-decoration: none; font-size: 16px; font-weight: bold;"
                                                                                            ID="Label1" Font-Underline="false" CommandName="Details" runat="server" Text='<%#Eval("UserName") %>'></asp:LinkButton>
                                                                                    </p>
                                                                                    <p class="timeT">
                                                                                        <asp:Label ID="lblAddedOn" Text='<%# Eval("dtAddedOn") %>' Style="font-size: 13px;"
                                                                                            runat="server"></asp:Label>
                                                                                    </p>
                                                                                </div>
                                                                                <div class="cls">
                                                                                </div>
                                                                                <!--edit starts-->
                                                                                <div class="editIcon" id="editUser" runat="server" style="display: none;">
                                                                                    <div id="smoothmenu1" class="ddsmoothmenu">
                                                                                        <ul class="iconUl">
                                                                                            <li><a href="#" onclick="return false">
                                                                                                <img src="images/edit.png" /></a>
                                                                                                <ul>
                                                                                                    <li style="border-bottom: 1px solid #cdced0;">
                                                                                                        <asp:LinkButton ID="lnkEditPost" Font-Underline="false" Visible="true" ToolTip="Edit"
                                                                                                            Text="Edit" CommandName="Edit Post" CausesValidation="false" runat="server">                                                                                   
                                                                                                        </asp:LinkButton>
                                                                                                    </li>
                                                                                                    <li><span class="ediDel">
                                                                                                        <asp:HiddenField ID="hdnintPostIdelet" Value='<%# Eval("Id") %>' ClientIDMode="Static"
                                                                                                            runat="server" />
                                                                                                        <asp:HiddenField ID="hdnstrPostDescriptiondel" Value='<%# Eval("strPostDescription") %>'
                                                                                                            ClientIDMode="Static" runat="server" />
                                                                                                        <asp:LinkButton ID="lnkDeletePost" Font-Underline="false" Visible="true" ClientIDMode="Static"
                                                                                                            ToolTip="Delete" Text="Delete" CommandName="Delete Post" CausesValidation="false"
                                                                                                            runat="server" OnClientClick="javascript:docdelete();return false;">                                                                                   
                                                                                                        </asp:LinkButton>
                                                                                                    </span></li>
                                                                                                </ul>
                                                                                            </li>
                                                                                        </ul>
                                                                                        <br style="clear: left" />
                                                                                    </div>
                                                                                </div>
                                                                                <!--edit end-->
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="lnkDeletePost" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                        <div class="cls">
                                                                        </div>
                                                                        <p class="noMar" style="margin-top: 5px; width: 610px;">
                                                                            <asp:Label ID="lblPostDescription" runat="server"></asp:Label>
                                                                        </p>
                                                                        <div class="">
                                                                            <p>
                                                                                <asp:HyperLink ID="hplLinkUrl" ClientIDMode="Static" Target="_blank" ToolTip="Posted Link"
                                                                                    NavigateUrl='<%#"http://"+Eval("strPostLink") %>' Text='<%#"http://"+Eval("strPostLink") %>'
                                                                                    Font-Size="Small" runat="server"></asp:HyperLink>
                                                                                <img src='<%# "UploadedPhoto/"+Eval("strPhotoPath")%>' id="imgPhoto" runat="server"
                                                                                    width="200" height="200" style="padding-left: 10px; padding-top: 10px;" />
                                                                                <div id="dvVideo" runat="server" clientidmode="Static">
                                                                                    <embed id="frm1" src='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>'
                                                                                        clientidmode="Static" starttime="00:00" controls="true" autoplay="false" autostart="false"
                                                                                        quality="high" cache="true" correction="full" pluginurl="http://quicktime.en.softonic.com/download"
                                                                                        pluginspage="http://quicktime.en.softonic.com/download" width="400" height="300"
                                                                                        scale="aspect" pluginspage="http://quicktime.en.softonic.com/download" />
                                                                                    <a id="lbtnControlPanel" title="Play Video" style="cursor: pointer;" runat="server"
                                                                                        clientidmode="Static" onclick="CPhit(this);"></a>
                                                                                </div>
                                                                                <div id="divChrome" runat="server" clientidmode="Static">
                                                                                    <div id='media-player'>
                                                                                <video id='media-video' controls>
                                                                                <source src='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>' type='video/mp4'>
                                                                                <source src='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>' type='video/mp3'>
                                                                                <source src='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>' type='video/webm'>
                                                                                <source src='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>' type='video/x-msvideo'>
                                                                                <object type="application/x-shockwave-flash" >
                                                                                <param name="movie" value='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>' />
                                                                                <param name="flashvars" value="controls=true&file=<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>" />
                                                                                </object>
                                                                                </video>
                                                                                    </div>
                                                                                </div>
                                                                            </p>
                                                                        </div>
                                                                        <div class="divtabLikes divtabLikespost">
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="lnkLike" />
                                                                                </Triggers>
                                                                                <ContentTemplate>
                                                                                    <asp:LinkButton Font-Underline="false" ID="lnkLike" runat="server" CommandName="Like Post"> 
                                                                        <img src="images/tabLike.png" /></asp:LinkButton>
                                                                                    <asp:Label ID="lnkLikePost" runat="server" Text='<%#Eval("Likes") %>' ToolTip="View Likes"
                                                                                        ClientIDMode="Static" Style="margin-left: 15px; color: #bcbec0; font-size: 18px;"></asp:Label>
                                                                                    <asp:HiddenField ID="hdnPostUpdateId" Value='<%# Eval("Id") %>' ClientIDMode="Static"
                                                                                        runat="server" />
                                                                                    <asp:HiddenField ID="hdnPostLikeUserId" Value='<%# Eval("PostLikeUserId") %>' ClientIDMode="Static"
                                                                                        runat="server" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>
                                                                    <!--comments starts-->
                                                                    <div class="tabComments">
                                                                        <p class="grayComments">
                                                                            Comments</p>
                                                                        <asp:ListView ID="lstChild" runat="server" OnItemCommand="lstChild_ItemCommand" DataKeyNames="intID"
                                                                            ClientIDMode="Static">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnintUserTypeId" runat="server" Value='<%#Eval("intUserTypeId") %>' />
                                                                                <asp:HiddenField ID="hdnRegistrationId" runat="server" Value='<%#Eval("intRegistrationId") %>' />
                                                                                <asp:HiddenField ID="intUserId" runat="server" Value='<%#Eval("intUserId") %>' />
                                                                                <div class="tabCmtBox">
                                                                                    <div class="tabImg">
                                                                                        <img src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' height="42" width="42" id="imgCommentpic"
                                                                                            runat="server" />
                                                                                        <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                                                                    </div>
                                                                                    <div class="tabTxtInfo">
                                                                                        <p class="name">
                                                                                            <asp:LinkButton Style="color: #4A4A4C; text-decoration: none;" ID="Label1" Font-Underline="false"
                                                                                                CommandName="Post Comment Details" runat="server" Text='<%#Eval("UserName") %>'></asp:LinkButton>
                                                                                        </p>
                                                                                        <p class="timeT">
                                                                                            <asp:Label ID="lblPostedOn" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label>
                                                                                        </p>
                                                                                        <div class="cls">
                                                                                        </div>
                                                                                        <asp:UpdatePanel ID="upchild" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <!--edit starts-->
                                                                                                <div class="editIcon" id="editUserComment" runat="server" style="display: none;">
                                                                                                    <div id="smoothmenu1" class="ddsmoothmenu">
                                                                                                        <ul class="iconUl">
                                                                                                            <li><a href="#" onclick="return false">
                                                                                                                <img src="images/edit.png" /></a>
                                                                                                                <ul>
                                                                                                                    <li style="border-bottom: 1px solid #cdced0;">
                                                                                                                        <asp:LinkButton ID="lnkEditComment" Font-Underline="false" Visible="true" ToolTip="Edit"
                                                                                                                            Text="Edit" CommandName="Edit Comment" CausesValidation="false" runat="server">
                                                                                                                        </asp:LinkButton>
                                                                                                                    </li>
                                                                                                                    <li><span class="ediDelc">
                                                                                                                        <asp:HiddenField ID="hdnintPostcIdelet" Value='<%# Eval("intID") %>' ClientIDMode="Static"
                                                                                                                            runat="server" />
                                                                                                                        <asp:HiddenField ID="hdnstrPostDescriptioncdel" Value='<%# Eval("strComment") %>'
                                                                                                                            ClientIDMode="Static" runat="server" />
                                                                                                                        <asp:LinkButton ID="lnkDeleteComment" Visible="true" OnClientClick="javascript:docdelete();return false;"
                                                                                                                            ToolTip="Delete" Text="Delete" CommandName="Delete Comment" CausesValidation="false"
                                                                                                                            runat="server">   
                                                                                                                        </asp:LinkButton>
                                                                                                                    </span></li>
                                                                                                                </ul>
                                                                                                            </li>
                                                                                                        </ul>
                                                                                                        <br style="clear: left" />
                                                                                                    </div>
                                                                                                </div>
                                                                                                <!--edit starts-->
                                                                                            </ContentTemplate>
                                                                                            <Triggers>
                                                                                                <asp:AsyncPostBackTrigger ControlID="lnkEditComment" />
                                                                                                <asp:AsyncPostBackTrigger ControlID="lnkDeleteComment" />
                                                                                            </Triggers>
                                                                                        </asp:UpdatePanel>
                                                                                        <div class="cls">
                                                                                        </div>
                                                                                        <p class="noMar" style="width: 545px;">
                                                                                            <asp:Label ID="lblstr" runat="server" Text='<%#Eval("strComment") %>'></asp:Label></p>
                                                                                        <div class="divtabLikes divtabLikespostcomm">
                                                                                            <asp:UpdatePanel ID="upda" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:LinkButton ID="lnkLikes" Font-Underline="false" runat="server" Text="Like" CommandName="Like Comment"
                                                                                                        ToolTip="Like Comment"> 
                                                                                        <img src="images/tabLike.png" width="20px" height="20px" /></asp:LinkButton>
                                                                                                    <asp:Label ID="lnkLikeComment" runat="server" Text='<%#Eval("Likes") %>' ClientIDMode="Static"
                                                                                                        Style="margin-left: 15px; font-size: 18px; color: #bcbec0;" ToolTip="View Likes"></asp:Label>
                                                                                                    <asp:HiddenField ID="hdnCommentId" runat="server" ClientIDMode="Static" Value='<%#Eval("intID") %>' />
                                                                                                    <asp:HiddenField ID="hdnCommentLikeUserId" Value='<%# Eval("CommentLikeUserId") %>'
                                                                                                        ClientIDMode="Static" runat="server" />
                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:AsyncPostBackTrigger ControlID="lnkLikes" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                        <div class="tabCmtBox">
                                                                            <div class="tabImg">
                                                                                <img id="imgComment" runat="server" width="40" height="40" /></div>
                                                                            <div class="tabTxtInfo">
                                                                                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>--%>
                                                                                <asp:Panel ID="pnlEnterComment" runat="server" ClientIDMode="Predictable" DefaultButton="lnkEnterComment">
                                                                                <span class="spComments">
                                                                                    <asp:HiddenField ID="hdnPostIDs" Value='<%# Eval("Id") %>' ClientIDMode="Static"
                                                                                        runat="server" />
                                                                                    <asp:TextBox ID="txtComment" runat="server" CssClass="addCommentTxt"></asp:TextBox>
                                                                                    </span>
                                                                                    <ajax:TextBoxWatermarkExtender TargetControlID="txtComment" ID="TextBoxWatermarkExtender2"
                                                                                        runat="server" WatermarkText="Add your comment...">
                                                                                    </ajax:TextBoxWatermarkExtender>
                                                                                    <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment"
                                                                                        Display="Dynamic" ValidationGroup="comment" Style="padding-left: 11px;" ErrorMessage="Please write a comment"
                                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    <asp:Label Style="display: none;" ID="lblCommentError" runat="server"></asp:Label>
                                                                                    <asp:LinkButton ID="lnkEnterComment" Style="display: none" CommandArgument='<%# Eval("Id") %>'
                                                                                        CssClass="lnkEnterCommentcss" CommandName="EnterComment" runat="server" Text="Enter"></asp:LinkButton>
                                                                                    <%-- <asp:Button ID="lnkEnterComment" Style="display: none" CommandArgument='<%# Eval("Id") %>' ClientIDMode="Static"
                                                                                            CommandName="EnterComment" runat="server" Text="Enter" />--%>
                                                                                </asp:Panel>
                                                                                <%--</ContentTemplate>
                                                                            </asp:UpdatePanel>--%>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <!--comments ends-->
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <div class="cls"></div>
                                                        <p id="pLoadMore" runat="server" align="center" clientidmode="Static"  Style="display: none;">
                                                        <asp:ImageButton ID="imgLoadMore" runat="server" ClientIDMode="Static" ImageUrl="~/images/loadingIcon.gif"
                                                            CssClass="imageLoadmoreHome" OnClick="imgLoadMore_OnClick" Height="100px" Width="100px" />
                                                        <asp:Button ID="imgLoadMore1" runat="server" ClientIDMode="Static" Style="display: none;"
                                                            CssClass="imageLoadmoreHome" OnClick="imgLoadMore_OnClick" Height="100px" Width="100px" />
                                                    </p>
                                                    <p align="center">
                                                        <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server"
                                                            ClientIDMode="Static" ForeColor="Red" Visible="false"></asp:Label>
                                                    </p>

                                                    </div>
                                                    
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
                                                            <asp:LinkButton ID="lnkLast" runat="server" Style="background: none" OnClick="lnkLast_Click"><img src="<%=ResolveUrl("images/spacer.gif")%>" class="last" /><img 
                                                    src="<%=ResolveUrl("images/spacer.gif")%>" class="last" /></asp:LinkButton>
                                                            <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                                                            <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                                                            <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                                                            <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                                                            <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                                                        </div>
                                                    </div>
                                                    <!-- Post Ends -->
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="lnkDummyPost" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <!--right section ends-->
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!--HOME ends-->
                                <!--DOCUMENT START-->
                                <div id="PnlDocuments1">
                                    <asp:UpdatePanel ID="PnlDocuments" runat="server" Visible="false" UpdateMode="Conditional"
                                        ClientIDMode="Static">
                                        <ContentTemplate>
                                            <!--main section starts-->
                                            <asp:UpdatePanel ID="upFileUpl" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="mainSection">
                                                        <div class="cls">
                                                        </div>
                                                        <p class="newGroupHeading">
                                                            Existing Documents</p>
                                                        <!--document starts-->
                                                        <asp:HiddenField ID="hdnintdocIdelete" Value="" ClientIDMode="Static" runat="server" />
                                                        <asp:HiddenField ID="hdnstrdocDescriptiondele" Value="" ClientIDMode="Static" runat="server" />
                                                        <asp:HiddenField ID="hdnfilestrFilePathe" Value="" ClientIDMode="Static" runat="server" />
                                                        <asp:ListView ID="LstDocument" runat="server" GroupItemCount="4" GroupPlaceholderID="groupPlaceHolder1"
                                                            OnItemCommand="LstDocument_ItemCommand" OnItemDataBound="LstDocument_ItemDataBound"
                                                            ItemPlaceholderID="itemPlaceHolder1">
                                                            <LayoutTemplate>
                                                                <table id="tblDoc" cellpadding="0" cellspacing="0" style="position: static;">
                                                                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <GroupTemplate>
                                                                <tr>
                                                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                                </tr>
                                                            </GroupTemplate>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnDocId" Value='<%#Eval("intDocId") %>' runat="server" />
                                                                <asp:HiddenField ID="hdnDocsSee" Value='<%#Eval("intDocsSee") %>' runat="server" />
                                                                <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' runat="server" />
                                                                <asp:HiddenField ID="hdnMaxcount" Value='<%#Eval("Maxcount") %>' runat="server" ClientIDMode="Static" />
                                                                <asp:HiddenField ID="hdnstrFilePath" Value='<%#Eval("strFilePath") %>' runat="server"
                                                                    ClientIDMode="Static" />
                                                                <asp:HiddenField ID="hdnstrDocTitle" Value='<%#Eval("strDocTitle") %>' runat="server"
                                                                    ClientIDMode="Static" />
                                                                <asp:HiddenField ID="hdnIsDocsDownload" Value='<%#Eval("IsDocsDownload") %>' runat="server"
                                                                    ClientIDMode="Static" />
                                                                <td style="width: 150px;">
                                                                    <div class="cls">
                                                                    </div>
                                                                    <div class="documentDts">
                                                                        <img src="images/snapshot.jpg" />
                                                                        <p class="docName">
                                                                            <asp:Label ID="lblDocument" runat="server" Text='<%#Eval("strDocTitle") %>' Style="display: none;"></asp:Label>
                                                                            <asp:HyperLink ID="lblDocument1" ClientIDMode="Static" Style="width: inherit; text-decoration: none;
                                                                                color: #666666;" Target="_blank" ToolTip="Download file" NavigateUrl='<%# "~/UploadDocument/"+Eval("strFilePath")%>'
                                                                                Text='<%#Eval("strDocTitle") %>' Font-Size="Small" runat="server"></asp:HyperLink>
                                                                        </p>
                                                                        <!--edit starts-->
                                                                        <div class="editIcon editName" id="editUserComment" runat="server">
                                                                            <div id="smoothmenu1" class="ddsmoothmenu">
                                                                                <ul class="iconUl">
                                                                                    <li><a href="#" onclick="return false">
                                                                                        <img src="images/edit.png" /></a>
                                                                                        <ul>
                                                                                            <li style="border-bottom: 1px solid #cdced0;">
                                                                                                <asp:LinkButton ID="lnkEditDoc" Font-Underline="false" Visible="true" ToolTip="Edit"
                                                                                                    CssClass="scroll" Text="Edit" CommandName="EditDocs" CausesValidation="false"
                                                                                                    runat="server">                             
                                                                                                </asp:LinkButton>
                                                                                            </li>
                                                                                            <li><span class="ediDeldoc">
                                                                                                <asp:HiddenField ID="hdnintdocIdelet" Value='<%# Eval("intDocId") %>' ClientIDMode="Static"
                                                                                                    runat="server" />
                                                                                                <asp:HiddenField ID="hdnstrdocDescriptiondel" Value='<%# Eval("strDocTitle") %>'
                                                                                                    ClientIDMode="Static" runat="server" />
                                                                                                <asp:HiddenField ID="hdnfilestrFilePath" Value='<%# Eval("strFilePath") %>' ClientIDMode="Static"
                                                                                                    runat="server" />
                                                                                                <asp:LinkButton ID="lnkDeleteDoc" Visible="true" OnClientClick="javascript:docdelete();return false;"
                                                                                                    ToolTip="Delete" Text="Delete" CommandName="DeleteDocs" CausesValidation="false"
                                                                                                    runat="server">                                       
                                                                                                </asp:LinkButton>
                                                                                            </span></li>
                                                                                        </ul>
                                                                                    </li>
                                                                                </ul>
                                                                                <br style="clear: left" />
                                                                            </div>
                                                                        </div>
                                                                        <!--edit end-->
                                                                    </div>
                                                                </td>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <div class="cls">
                                                            <p>
                                                                &nbsp;</p>
                                                        </div>
                                                        <div class="uploadBg" id="divDocumentUplaod" clientidmode="Static" runat="server"
                                                            style="display: none;">
                                                            <div style="margin-left: 20px;">
                                                                <a name="docssection" id="aDivDoc"></a>
                                                                <p class="imgVer">
                                                                    <asp:TextBox ID="txtDocTitle" runat="server" ClientIDMode="Static" class="newGroupInput skilset"
                                                                        ValidationGroup="docUpload" Style="height: 28px;" placeholder="Name of the Document"></asp:TextBox>
                                                                </p>
                                                                <div class="makethiprv">
                                                                    <tabel style="width: 100%;">
                                                    <tr>
                                                    <td>
                                                    <asp:HiddenField ID="hdnimgPrivate" runat="server" ClientIDMode="Static" Value="0" />
                                                     <img id="imgPrivate" clientidmode="Static" runat="server" src="images/unchk1.png" onclick="fncsave();"  />
                                                     </td>
                                                     <td style="vertical-align:top;">
                                                     Make this private
                                                     </td>
                                                    <td> 
                                                    <asp:HiddenField ID="hdnimgDownload" runat="server" ClientIDMode="Static" Value="1" />
                                                    <img id="imgDownload" clientidmode="Static" class="secImg" runat="server" src="images/chk1.png" onclick="fncsavedow();" />
                                                    </td>
                                                    <td style="vertical-align:top;"> 
                                                    Downloadable
                                                    </td> 
                                                    </tr> 
                                                    </tabel>
                                                                    <asp:Button ID="savebtn" runat="server" OnClick="imgPrivate_Click" Style="display: none" />
                                                                    <asp:Button ID="savebtndow" runat="server" OnClick="imgDownload_Click" Style="display: none" />
                                                                </div>
                                                                <p>
                                                                    <textarea id="txtDescrition" runat="server" placeholder="Description" class="newGroupDescp"></textarea>
                                                                </p>
                                                                <p class="addcontect">
                                                                    Context:</p>
                                                                <div class="cls">
                                                                </div>
                                                                <div style="margin-top: 10px;">
                                                                    <asp:HiddenField ID="hdnSubject" ClientIDMode="Static" runat="server" />
                                                                    <ul class="context">
                                                                        <asp:ListView ID="lstSubjCategory" runat="server" OnItemDataBound="LstSubjCategory_ItemDataBound"
                                                                            GroupItemCount="4" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">
                                                                            <LayoutTemplate>
                                                                                <table width="90%">
                                                                                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                                                                </table>
                                                                            </LayoutTemplate>
                                                                            <GroupTemplate>
                                                                                <tr>
                                                                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                                                </tr>
                                                                            </GroupTemplate>
                                                                            <ItemTemplate>
                                                                                <li id="SubLi" runat="server" style="cursor: pointer;">
                                                                                    <asp:HiddenField ID="hdnSubCatId" ClientIDMode="Static" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                                                    <asp:HiddenField ID="hdnCountSub" ClientIDMode="Static" runat="server" Value='<%#Eval("CountSub")%>' />
                                                                                    <asp:Label ID="lnkCatName" ClientIDMode="Static" CssClass="unselectedtagnameGroup"
                                                                                        runat="server" Text='<%#Eval("strCategoryName")%>' CommandName="Subject Category"></asp:Label>
                                                                                </li>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </ul>
                                                                </div>
                                                                <div class="uploadBox fileUploadHome uploadDocsHomeHome" id="upload" runat="server"
                                                                    clientidmode="Static">
                                                                    <span>Upload Document</span>
                                                                    <asp:FileUpload ID="uploadDoc" ClientIDMode="Static" runat="server" CssClass="upload"
                                                                        Style="display: block;" />
                                                                </div>
                                                                <div class="cls">
                                                                </div>
                                                                <p>
                                                                    <asp:Label ID="lblfilenamee" runat="server" Style="color: Black;" ClientIDMode="Static">
                                                                    </asp:Label>
                                                                    <asp:HiddenField ID="hdnUploadFile" runat="server" ClientIDMode="Static" />
                                                                    <asp:HiddenField ID="hdnUploadFile1" runat="server" ClientIDMode="Static" />
                                                                    <asp:HiddenField ID="hdnFilePath" runat="server" ClientIDMode="Static" />
                                                                    <br />
                                                                    <asp:LinkButton ID="lnkDeleteDoc123" ClientIDMode="Static" Style="color: #00B6BD;
                                                                        display: none;" runat="server" OnClientClick="javascript:return confirm('Do you want to delete?');"
                                                                        OnClick="lnkDelete_Click">Delete</asp:LinkButton>
                                                                </p>
                                                                <div class="cls">
                                                                </div>
                                                                <p>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDocTitle"
                                                                        Display="Dynamic" ValidationGroup="docUpload" ErrorMessage="Please enter Title"
                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </p>
                                                                <asp:LinkButton ID="LinkSave" CssClass="createGroup LinkSave" runat="server" Text="Save"
                                                                    ValidationGroup="docUpload" OnClick="btnSave_Click" ClientIDMode="Static" OnClientClick="javascript:callDocSave();"></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkCancelDoc" CssClass="cancelGroup" runat="server" Text="Cancel"
                                                                    ClientIDMode="Static" OnClick="lnkCancelDoc_Click" OnClientClick="javascript:callDoccancel();"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="cls">
                                                            <p>
                                                                &nbsp;</p>
                                                            <p>
                                                                &nbsp;</p>
                                                        </div>
                                                        <asp:LinkButton class="addWorkEx" ID="lnkuploadDoc" runat="server" ClientIDMode="Static" OnClientClick="CallUploadDoc();" 
                                                            Text="Upload Document" OnClick="lnkuploadDoc_click"></asp:LinkButton>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <!--main section ends-->
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!--DOCUMENT END-->
                                <!--Work Exp START-->
                                <div id="PnlWorkex1">
                                    <asp:UpdatePanel ID="PnlWorkex" runat="server" Visible="false" UpdateMode="Conditional"
                                        ClientIDMode="Static">
                                        <ContentTemplate>
                                            <!--main section starts-->
                                            <div class="mainSection">
                                                <div class="cls">
                                                </div>
                                                <div>
                                                    <p class="newGroupHeading">
                                                        Work Experience</p>
                                                    <asp:HiddenField ID="hdnintworkeIdelet" Value="" ClientIDMode="Static" runat="server" />
                                                    <asp:HiddenField ID="hdnstrworkeDescriptiondel" Value="" ClientIDMode="Static" runat="server" />
                                                    <asp:ListView runat="server" ID="lstWorkExperience" OnItemDataBound="lstWorkExperience_ItemDataBound"
                                                        OnItemCommand="lstWorkExperience_ItemCommand" CellPadding="9">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnintExperienceId" Value='<%#Eval("intExperienceId") %>' runat="server" />
                                                            <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' runat="server" />
                                                            <p class="newGroupContext ex">
                                                                <asp:Label ID="lblCompanyName" runat="server" Text='<%#Eval("strCompanyName") %>' /></p>
                                                            <p class="workExDesignation">
                                                                <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("strDesignation") %>' /></p>
                                                            <p class="exDuration">
                                                                <asp:Label ID="lblinFromtMonth" runat="server" Text='<%#Eval("inFromtMonth") %>' />
                                                                <asp:Label ID="lblFromYear" runat="server" Text='<%#Eval("intFromYear") %>' />
                                                                -
                                                                <asp:Label ID="lblintToMonth" runat="server" Text='<%#Eval("intToMonth") %>' />
                                                                <asp:Label ID="lblToYear" runat="server" Text='<%#Eval("intToYear") %>' />
                                                            </p>
                                                            <div class="cls">
                                                            </div>
                                                            <!--edit starts-->
                                                            <div class="editIcon exBox" id="divUserexperenceED" style="margin: -93px 20px 0 70px;"
                                                                runat="server">
                                                                <div id="smoothmenu1" class="ddsmoothmenu">
                                                                    <ul class="iconUl">
                                                                        <li><a href="#" onclick="return false">
                                                                            <img src="images/edit.png" /></a>
                                                                            <ul>
                                                                                <li style="border-bottom: 1px solid #cdced0;">
                                                                                    <asp:LinkButton ID="lnkEditDoc" Font-Underline="false" Visible="true" ToolTip="Edit"
                                                                                        Text="Edit" CommandName="EditExp" CausesValidation="false" runat="server"> 
                                                                                    </asp:LinkButton>
                                                                                </li>
                                                                                <li><span class="ediDelwork">
                                                                                    <asp:HiddenField ID="hdnintworkIdelet" Value='<%# Eval("intExperienceId") %>' ClientIDMode="Static"
                                                                                        runat="server" />
                                                                                    <asp:HiddenField ID="hdnstrworkDescriptiondel" Value='<%# Eval("strCompanyName") %>'
                                                                                        ClientIDMode="Static" runat="server" />
                                                                                    <asp:LinkButton ID="lnkDeleteDoc" Font-Underline="false" Visible="true" ToolTip="Delete"
                                                                                        Text="Delete" CommandName="DeleteExp" CausesValidation="false" runat="server"
                                                                                        OnClientClick="javascript:docdelete();return false;"> 
                                                                                    </asp:LinkButton>
                                                                                </span></li>
                                                                            </ul>
                                                                        </li>
                                                                    </ul>
                                                                    <br style="clear: left" />
                                                                </div>
                                                            </div>
                                                            <!--edit starts-->
                                                            <div class="cls">
                                                            </div>
                                                            <p style="font-size: 15px; color: Black; margin-bottom: 30px;">
                                                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("strDescription") %>' /></p>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                                <div class="cls">
                                                </div>
                                                <!--add exp starts-->
                                                <div class="uploadBg" id="AddWorkExp" runat="server" style="display: none;" clientidmode="Static">
                                                    <div style="margin-left: 20px;">
                                                        <p>
                                                            <asp:TextBox ID="txtInstituteName" runat="server" placeholder="Company name" class="newGroupInput"
                                                                ClientIDMode="Static" ValidationGroup="workExp"></asp:TextBox>
                                                        </p>
                                                        <p>
                                                            <asp:TextBox ID="txtDegree" ClientIDMode="Static" runat="server" placeholder="Position"
                                                                class="newGroupInput"></asp:TextBox>
                                                        </p>
                                                        <asp:UpdatePanel ID="upda" runat="server">
                                                            <ContentTemplate>
                                                                <p>
                                                                    Timeframe
                                                                    <select name="select" id="fromMonth" runat="server">
                                                                        <option>Month</option>
                                                                        <option>Jan</option>
                                                                        <option>Feb</option>
                                                                        <option>Mar</option>
                                                                        <option>Apr</option>
                                                                        <option>May</option>
                                                                        <option>Jun</option>
                                                                        <option>Jul</option>
                                                                        <option>Aug</option>
                                                                        <option>Sep</option>
                                                                        <option>Oct</option>
                                                                        <option>Nov</option>
                                                                        <option>Dec</option>
                                                                    </select>
                                                                    <asp:DropDownList ID="txtFromYear" runat="server" CssClass="signInputY" ClientIDMode="Static">
                                                                    </asp:DropDownList>
                                                                    <span class="spanDash">- </span>
                                                                    <asp:DropDownList ID="toMonth" runat="server" ClientIDMode="Static">
                                                                        <asp:ListItem Selected="True">Month</asp:ListItem>
                                                                        <asp:ListItem>Jan</asp:ListItem>
                                                                        <asp:ListItem>Feb</asp:ListItem>
                                                                        <asp:ListItem>Mar</asp:ListItem>
                                                                        <asp:ListItem>Apr</asp:ListItem>
                                                                        <asp:ListItem>May</asp:ListItem>
                                                                        <asp:ListItem>Jun</asp:ListItem>
                                                                        <asp:ListItem>Jul</asp:ListItem>
                                                                        <asp:ListItem>Aug</asp:ListItem>
                                                                        <asp:ListItem>Sep</asp:ListItem>
                                                                        <asp:ListItem>Oct</asp:ListItem>
                                                                        <asp:ListItem>Nov</asp:ListItem>
                                                                        <asp:ListItem>Dec</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="txtToYear" runat="server" CssClass="signInputY" ClientIDMode="Static">
                                                                    </asp:DropDownList>
                                                                    <asp:CheckBox ID="chkAtPresent" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="chkAtPresent_CheckedChanged"
                                                                        runat="server" />
                                                                    Currently employer
                                                                </p>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="chkAtPresent" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                        <p>
                                                            <textarea class="newGroupDescp" id="txtDescription" runat="server" placeholder="Description"></textarea>
                                                        </p>
                                                        <p>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInstituteName"
                                                                ValidationGroup="workExp" ErrorMessage="Please enter company name." EnableClientScript="true"
                                                                ForeColor="Red"></asp:RequiredFieldValidator></p>
                                                        <p>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDegree"
                                                                Display="Dynamic" ValidationGroup="workExp" ErrorMessage="Please enter position."
                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </p>
                                                        <p>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="fromMonth"
                                                                InitialValue="Month" Display="Dynamic" ValidationGroup="workExp" ErrorMessage="Please select from month."
                                                                ForeColor="Red"></asp:RequiredFieldValidator></p>
                                                        <p>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromYear"
                                                                Display="Dynamic" ValidationGroup="workExp" ErrorMessage="Please enter from year."
                                                                InitialValue="Year" ForeColor="Red"></asp:RequiredFieldValidator></p>
                                                        <p>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="toMonth"
                                                                InitialValue="Month" Display="Dynamic" ValidationGroup="workExp" ErrorMessage="Please select to month."
                                                                ForeColor="Red"></asp:RequiredFieldValidator></p>
                                                        <p>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtToYear"
                                                                Display="Dynamic" ValidationGroup="workExp" ErrorMessage="Please enter to year."
                                                                InitialValue="Year" ForeColor="Red"></asp:RequiredFieldValidator></p>
                                                        <asp:LinkButton runat="server" ID="lnlSaveExp" Text="Save" CssClass="createGroup"
                                                            ClientIDMode="Static" ValidationGroup="workExp" OnClick="lnlSaveExp_Click" OnClientClick="javascript:callSaveExp();"></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="LinkButton1" Text="Cancel" CssClass="cancelGroup"
                                                            OnClick="lnlCancel_Click" ClientIDMode="Static" Visible="true" OnClientClick="javascript:callCancelExp();"></asp:LinkButton>
                                                    </div>
                                                </div>
                                                <!--add exp ends-->
                                                <div class="cls">
                                                    <p>
                                                        &nbsp;</p>
                                                </div>
                                                <a name="captchaAnchor"></a>
                                                <asp:LinkButton class="addWorkEx" ID="aAddworkExp" runat="server" ClientIDMode="Static" OnClientClick="CallaAddworkExp();"
                                                    Text="Add Work Experience" OnClick="aAddworkExp_click"></asp:LinkButton>
                                                <p class="newGroupHeading sk">
                                                    Skillset</p>
                                                <div class="cls">
                                                </div>
                                                <a name="SkillsetSection"></a>
                                                <!--edit starts-->
                                                <div class="editIcon skBox" id="divSkillEditdelete" runat="server">
                                                    <div id="smoothmenu1" class="ddsmoothmenu">
                                                        <ul class="iconUl">
                                                            <li><a href="#" onclick="return false">
                                                                <img src="images/edit.png" /></a>
                                                                <ul>
                                                                    <li style="border-bottom: 1px solid #cdced0;">
                                                                        <%--<a href="#">Edit</a>--%>
                                                                        <asp:LinkButton ID="lnkEditSkill" Font-Underline="false" Visible="true" ToolTip="Edit"
                                                                            Text="Edit" CausesValidation="false" runat="server" OnClick="lnkEditSkill_Click"> 
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                </ul>
                                                            </li>
                                                        </ul>
                                                        <br style="clear: left" />
                                                    </div>
                                                </div>
                                                <!--edit starts-->
                                                <div class="cls">
                                                </div>
                                                <asp:UpdatePanel ID="upskillsets" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <ul class="skillsetList" runat="server" id="skillsets" clientidmode="Static">
                                                            <asp:ListView ID="lstAreaExpert" runat="server" OnItemCommand="lstAreaExpert_ItemCommand"
                                                                OnItemDataBound="lstAreaExpert_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdnintUserSkillId" runat="server" Value='<%#Eval("intUserSkillId")%>' />
                                                                    <li class="endronsecssli" style="cursor: pointer;">
                                                                        <asp:UpdatePanel ID="upskillend" runat="server" ClientIDMode="Static">
                                                                            <ContentTemplate>
                                                                                <asp:Label ID="lblEndronseCount" runat="server" class="sscount" Text="0"></asp:Label>
                                                                                <asp:Label ID="lblstrSkillName" runat="server" class="ssField" Text='<%#Eval("strSkillName")%>'></asp:Label>
                                                                                <img id="imgPlus" runat="server" src="images/plus.png" clientidmode="Static" visible="false"
                                                                                    class="endronsecssimg" style="cursor: pointer;" />
                                                                                <asp:LinkButton ID="lnkEndrose" ClientIDMode="Static" class="endronsecss" runat="server"
                                                                                    CommandName="EndronseSkill" Text="Endorse" Style="display: none;"></asp:LinkButton>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="lnkEndrose" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </ul>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lstAreaExpert" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <div class="cls">
                                                    <p>
                                                        &nbsp;</p>
                                                </div>
                                                <!--add exp starts-->
                                                <div class="uploadBg" id="divAddskill" runat="server" style="display: none;" clientidmode="Static">
                                                    <asp:UpdatePanel ID="updateskills" runat="server">
                                                        <ContentTemplate>
                                                            <div style="margin-left: 20px;">
                                                                <p>
                                                                    <asp:TextBox ID="txtAreaExpert" runat="server" placeholder="Add your area of expertise"
                                                                        ValidationGroup="skills" class="newGroupInput skilset" ClientIDMode="Static"></asp:TextBox>
                                                                    <asp:LinkButton ID="btnAreaExpSave" runat="server" Text="Add" ClientIDMode="Static"
                                                                        OnClientClick="javascript:CallAddSkill();" ValidationGroup="skills" CssClass="createGroup addsk"
                                                                        OnClick="btnAreaExpSave_Click"></asp:LinkButton>
                                                                        <div style="display:none;">
                                                                    <asp:Button ID="btnAreaExpSave1" runat="server" Text="Add" ClientIDMode="Static"
                                                                        ValidationGroup="skills" CssClass="createGroup addsk" OnClick="btnAreaExpSave_Click" />
                                                                        </div>
                                                                </p>
                                                                <ul class="skillsetList editss">
                                                                    <asp:ListView ID="lstAreaSkill" runat="server" OnItemCommand="lstAreaSkill_ItemCommand"
                                                                        GroupItemCount="3" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">
                                                                        <GroupTemplate>
                                                                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                                        </GroupTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hdnintUserSkillId" runat="server" Value='<%#Eval("intUserSkillId")%>' />
                                                                            <li><span class="sscount">0</span> <span class="ssField">
                                                                                <%#Eval("strSkillName")%>
                                                                                <asp:LinkButton ID="lnkdDelete" runat="server" CommandName="DeleteExp">
                                                            <img src="images/close.png" /></asp:LinkButton>
                                                                            </span></li>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </ul>
                                                                <p>
                                                                    <asp:Label ID="lblareaskill" ClientIDMode="Static" runat="server" ForeColor="Red"></asp:Label>
                                                                </p>
                                                                <asp:LinkButton ID="lnkSaveSkill" runat="server" Text="Save" ClientIDMode="Static"
                                                                    CssClass="createGroup" OnClick="lnkSaveSkill_Click" OnClientClick="javascript:callSaveSkill();"></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkCancelSkill" runat="server" Text="Cancel" ClientIDMode="Static"
                                                                    CssClass="cancelGroup" OnClick="lnkCancelSkill_Click" OnClientClick="javascript:callCancelSkill();"></asp:LinkButton>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="lnkCancelSkill" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnAreaExpSave" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnAreaExpSave1" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <!--add exp ends-->
                                                <div class="cls">
                                                    <p>
                                                        &nbsp;</p>
                                                </div>
                                                <div class="cls">
                                                </div>
                                                <asp:LinkButton ID="lnkAddskill" runat="server" OnClick="lnkAddSkill_Click" CssClass="addWorkEx"
                                                    Text="Add Skill" OnClientClick="CalllnkAddskill();" ></asp:LinkButton>
                                                <div class="cls">
                                                    <p>
                                                        &nbsp;</p>
                                                    <p>
                                                        &nbsp;</p>
                                                </div>
                                            </div>
                                            <!--main section ends-->
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="lnlSaveExp" />
                                            <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
                                            <asp:AsyncPostBackTrigger ControlID="lnkSaveSkill" />
                                            <asp:AsyncPostBackTrigger ControlID="lnkCancelSkill" />
                                            <asp:AsyncPostBackTrigger ControlID="LinkButton1" />
                                            <asp:AsyncPostBackTrigger ControlID="aAddworkExp" />
                                            <asp:AsyncPostBackTrigger ControlID="lnkAddskill" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <!--Work Exp End-->
                                <!--Education START-->
                                <asp:UpdatePanel ID="PnlEduction" runat="server" Visible="false" UpdateMode="Conditional"
                                    ClientIDMode="Static">
                                    <ContentTemplate>
                                        <!--main section starts-->
                                        <div class="mainSection">
                                            <div class="cls">
                                            </div>
                                            <p class="newGroupHeading">
                                                Education</p>
                                            <div class="cls">
                                            </div>
                                            <asp:HiddenField ID="hdnintedueIdelet" Value="" ClientIDMode="Static" runat="server" />
                                            <asp:HiddenField ID="hdnstredueDescriptiondel" Value="" ClientIDMode="Static" runat="server" />
                                            <asp:ListView runat="server" ID="lstEducation" OnItemDataBound="lstEducation_ItemDataBound"
                                                OnItemCommand="lstEducation_ItemCommand" CellPadding="9">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnintEducationId" Value='<%#Eval("intEducationId") %>' runat="server" />
                                                    <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' runat="server" />
                                                    <asp:HiddenField ID="hdnToMonth" Value='<%#Eval("ToMonth") %>' runat="server" />
                                                    <p class="newGroupContext ex">
                                                        <asp:Label ID="lblInstituteName" runat="server" Text='<%#Eval("strInstituteName") %>' /></p>
                                                    <p class="workExDesignation">
                                                        <asp:Label ID="lblEducationAchievement" runat="server" Text='<%#Eval("strDegree") %>' /></p>
                                                    <p class="exDuration">
                                                        <asp:Label ID="lblFromMonth" runat="server" Text='<%#Eval("intMonth") %>' />
                                                        <asp:Label ID="lblFromYear" runat="server" Text='<%#Eval("intYear") %>' />
                                                        -
                                                        <asp:Label ID="lblintToMonth" runat="server" Text='<%#Eval("intToMonth") %>' />
                                                        <asp:Label ID="lblintToYear" runat="server" Text='<%#Eval("intToYear") %>' />
                                                        <asp:Label ID="lblPresentDay" runat="server" />
                                                    </p>
                                                    <div class="cls">
                                                    </div>
                                                    <div class="cls">
                                                    </div>
                                                    <!--edit starts-->
                                                    <div class="editIcon exBox" id="divEducationED" runat="server" style="margin: -90px 20px 0 70px;"
                                                        clientidmode="Static">
                                                        <div id="smoothmenu1" class="ddsmoothmenu">
                                                            <ul class="iconUl">
                                                                <li><a href="#" onclick="return false">
                                                                    <img src="images/edit.png" /></a>
                                                                    <ul>
                                                                        <li style="border-bottom: 1px solid #cdced0;">
                                                                            <asp:LinkButton ID="lnkEditDoc" Font-Underline="false" Visible="true" ToolTip="Edit"
                                                                                Text="Edit" CommandName="EditEdu" CausesValidation="false" runat="server">               
                                                                            </asp:LinkButton>
                                                                        </li>
                                                                        <li><span class="ediDeledu">
                                                                            <asp:HiddenField ID="hdninteduIdelet" Value='<%# Eval("intEducationId") %>' ClientIDMode="Static"
                                                                                runat="server" />
                                                                            <asp:HiddenField ID="hdnstreduDescriptiondel" Value='<%# Eval("strInstituteName") %>'
                                                                                ClientIDMode="Static" runat="server" />
                                                                            <asp:LinkButton ID="lnkDeleteDoc" Visible="true" OnClientClick="javascript:docdelete();return false;"
                                                                                ToolTip="Delete" Text="Delete" CommandName="DeleteEdu" CausesValidation="false"
                                                                                runat="server">                                  
                                                                            </asp:LinkButton>
                                                                        </span></li>
                                                                    </ul>
                                                                </li>
                                                            </ul>
                                                            <br style="clear: left" />
                                                        </div>
                                                    </div>
                                                    <!--edit starts-->
                                                    <div class="cls">
                                                    </div>
                                                    <p style="font-size: 15px; color: Black; margin-bottom: 30px;">
                                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("strSpclLibrary") %>' /></p>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <div class="cls">
                                            </div>
                                            <!--add exp starts-->
                                            <div class="uploadBg" id="divEducation" runat="server" style="display: none;" clientidmode="Static">
                                                <div style="margin-left: 20px;">
                                                    <p>
                                                        <asp:TextBox ID="txtInstitute" runat="server" ClientIDMode="Static" placeholder="Institute Name"
                                                            class="newGroupInput"></asp:TextBox>
                                                        <ajax:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                            CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtInstitute"
                                                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                        </ajax:AutoCompleteExtender>
                                                    </p>
                                                    <p>
                                                        <asp:TextBox ID="txtTitle" runat="server" ClientIDMode="Static" placeholder="Degree"
                                                            class="newGroupInput"></asp:TextBox>
                                                        <ajax:AutoCompleteExtender ServiceMethod="GetDegreeList" MinimumPrefixLength="1"
                                                            CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtTitle"
                                                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                                        </ajax:AutoCompleteExtender>
                                                    </p>
                                                    <asp:UpdatePanel ID="updates" runat="server">
                                                        <ContentTemplate>
                                                            <p>
                                                                Timeframe
                                                                <select name="select" id="ddlFromMonth" runat="server" clientidmode="Static">
                                                                    <option>Month</option>
                                                                    <option>Jan</option>
                                                                    <option>Feb</option>
                                                                    <option>Mar</option>
                                                                    <option>Apr</option>
                                                                    <option>May</option>
                                                                    <option>Jun</option>
                                                                    <option>Jul</option>
                                                                    <option>Aug</option>
                                                                    <option>Sep</option>
                                                                    <option>Oct</option>
                                                                    <option>Nov</option>
                                                                    <option>Dec</option>
                                                                </select>
                                                                <asp:DropDownList ID="txtEducationFromdt" runat="server" CssClass="signInputY" ClientIDMode="Static">
                                                                </asp:DropDownList>
                                                                <span class="spanDash">- </span>
                                                                <asp:DropDownList ID="ddlToMonth" runat="server" ClientIDMode="Static">
                                                                    <asp:ListItem Selected="True">Month</asp:ListItem>
                                                                    <asp:ListItem>Jan</asp:ListItem>
                                                                    <asp:ListItem>Feb</asp:ListItem>
                                                                    <asp:ListItem>Mar</asp:ListItem>
                                                                    <asp:ListItem>Apr</asp:ListItem>
                                                                    <asp:ListItem>May</asp:ListItem>
                                                                    <asp:ListItem>Jun</asp:ListItem>
                                                                    <asp:ListItem>Jul</asp:ListItem>
                                                                    <asp:ListItem>Aug</asp:ListItem>
                                                                    <asp:ListItem>Sep</asp:ListItem>
                                                                    <asp:ListItem>Oct</asp:ListItem>
                                                                    <asp:ListItem>Nov</asp:ListItem>
                                                                    <asp:ListItem>Dec</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="txtEducationTodt" runat="server" CssClass="signInputY" ClientIDMode="Static">
                                                                </asp:DropDownList>
                                                                <asp:CheckBox ID="chkEducation" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="chkAtPresent_CheckedChanged1"
                                                                    runat="server" />
                                                                Currently studying
                                                            </p>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="chkEducation" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                    <p>
                                                        <textarea class="newGroupDescp" id="txtEduDescription" runat="server" placeholder="Description"
                                                            clientidmode="Static"></textarea></p>
                                                    <p>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtInstitute"
                                                            Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please enter Institute / University name"
                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </p>
                                                    <p>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtTitle"
                                                            Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please enter Degree"
                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </p>
                                                    <p>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlFromMonth"
                                                            Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please select from month."
                                                            InitialValue="Month" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </p>
                                                    <p>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtEducationFromdt"
                                                            Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please select from year."
                                                            InitialValue="Year" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </p>
                                                    <p>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlToMonth"
                                                            Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please select to month."
                                                            InitialValue="Month" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </p>
                                                    <p>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtEducationTodt"
                                                            Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please select to year."
                                                            InitialValue="Year" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </p>
                                                    <p>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                    </p>
                                                    <asp:LinkButton runat="server" ID="lnkSaveEducation" Text="Save" CssClass="createGroup"
                                                        ClientIDMode="Static" ValidationGroup="validationEdu" OnClick="lnkSaveEducation_Click"
                                                        OnClientClick="javascript:callSaveEdu();"></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkCancelEducation" Text="Cancel" CssClass="cancelGroup"
                                                        ClientIDMode="Static" OnClick="lnkCancelEducation_Click" OnClientClick="javascript:callCancelEdu();"></asp:LinkButton>
                                                </div>
                                            </div>
                                            <!--add exp ends-->
                                            <div class="cls">
                                                <p>
                                                    &nbsp;</p>
                                            </div>
                                            <asp:LinkButton runat="server" ID="lnlAddEducation" Text="Add Education" CssClass="addWorkEx"
                                                OnClick="lnlAddEducation_Click" OnClientClick="CalllnlAddEducation();"></asp:LinkButton>
                                            <div class="cls">
                                            </div>
                                        </div>
                                        <!--add exp ends-->
                                        </div>
                                        <!--main section ends-->
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkSaveEducation" />
                                        <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
                                        <asp:AsyncPostBackTrigger ControlID="lnkCancelEducation" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <!--Education End-->
                                <!--Achivement START-->
                                <asp:UpdatePanel ID="PnlAchivement" runat="server" Visible="false" UpdateMode="Conditional"
                                    ClientIDMode="Static">
                                    <ContentTemplate>
                                        <!--main section starts-->
                                        <div class="mainSection">
                                            <div class="cls">
                                            </div>
                                            <p class="newGroupHeading">
                                                Achievements</p>
                                            <asp:HiddenField ID="hdnintacheIdelet" Value="" ClientIDMode="Static" runat="server" />
                                            <asp:HiddenField ID="hdnstracheDescriptiondel" Value="" ClientIDMode="Static" runat="server" />
                                            <asp:ListView runat="server" ID="lstAchivement" OnItemDataBound="lstAchivement_ItemDataBound"
                                                OnItemCommand="lstAchivement_ItemCommand" CellPadding="9">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnintAchivmentId" Value='<%#Eval("intAchivmentId") %>' runat="server"
                                                        ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' runat="server" />
                                                    <p class="newGroupContext ex">
                                                        <asp:Label ID="lblstrCompititionName" runat="server" Text='<%#Eval("strCompititionName") %>' /></p>
                                                    <p class="workExDesignation">
                                                        <asp:Label ID="lblstrPosition" runat="server" Text='<%#Eval("strPosition") %>' /></p>
                                                    <!--edit starts-->
                                                    <div class="editIcon exBox" id="divAchivementED" style="margin: -100px 20px 0 70px;"
                                                        runat="server">
                                                        <div id="smoothmenu1" class="ddsmoothmenu">
                                                            <ul class="iconUl" style="margin-top: 56px;">
                                                                <li><a href="#" onclick="return false">
                                                                    <img src="images/edit.png" /></a>
                                                                    <ul>
                                                                        <li style="border-bottom: 1px solid #cdced0;">
                                                                            <asp:LinkButton ID="lnkEditDoc" Font-Underline="false" Visible="true" ToolTip="Edit"
                                                                                Text="Edit" CommandName="EditAch" CausesValidation="false" runat="server">
                                                                            </asp:LinkButton>
                                                                        </li>
                                                                        <li><span class="ediDelach">
                                                                            <asp:HiddenField ID="hdnintachIdelet" Value='<%# Eval("intAchivmentId") %>' ClientIDMode="Static"
                                                                                runat="server" />
                                                                            <asp:HiddenField ID="hdnstrachDescriptiondel" Value='<%# Eval("strCompititionName") %>'
                                                                                ClientIDMode="Static" runat="server" />
                                                                            <asp:LinkButton ID="lnkDeleteDoc" Visible="true" OnClientClick="javascript:docdelete();return false;"
                                                                                ToolTip="Delete" Text="Delete" CommandName="DeleteAch" CausesValidation="false"
                                                                                runat="server">                                   
                                                                            </asp:LinkButton>
                                                                        </span></li>
                                                                    </ul>
                                                                </li>
                                                            </ul>
                                                            <br style="clear: left" />
                                                        </div>
                                                    </div>
                                                    <!--edit starts-->
                                                    <p style="font-size: 15px; color: Black; margin-bottom: 30px; margin-top: 8px;">
                                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("strDescription") %>' /></p>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <div class="cls">
                                            </div>
                                            <!--add exp starts-->
                                            <div class="uploadBg" id="divAchivement" runat="server" style="display: none;" clientidmode="Static">
                                                <div style="margin-left: 20px;">
                                                    <p>
                                                        <asp:DropDownList ID="ddlMilestone" runat="server" ClientIDMode="Static" class="consellist"
                                                            ValidationGroup="validationAchiv" Style="width: 340px;">
                                                        </asp:DropDownList>
                                                    </p>
                                                    <p>
                                                        <asp:TextBox ID="txtCompitition" runat="server" placeholder="Name of Competition"
                                                            ClientIDMode="Static" class="newGroupInput"></asp:TextBox>
                                                        <ajax:AutoCompleteExtender ServiceMethod="GetCompititionLists" MinimumPrefixLength="1"
                                                            CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtCompitition"
                                                            ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                                                        </ajax:AutoCompleteExtender>
                                                    </p>
                                                    <p>
                                                        <asp:DropDownList ID="ddlPosition" runat="server" ClientIDMode="Static" class="consellist"
                                                            ValidationGroup="validationAchiv">
                                                        </asp:DropDownList>
                                                    </p>
                                                    <p>
                                                        <asp:TextBox ID="txtAdditionalAwrd" runat="server" placeholder="Additional Award"
                                                            class="newGroupInput"></asp:TextBox>
                                                    </p>
                                                    <p>
                                                        <textarea id="txtAchivDescription" runat="server" placeholder="Description" class="newGroupDescp"></textarea></p>
                                                    <p>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlMilestone"
                                                            Display="Dynamic" ValidationGroup="validationAchiv" ErrorMessage="Please select milestone."
                                                            InitialValue="Type of Milestone" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </p>
                                                    <p>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtCompitition"
                                                            Display="Dynamic" ValidationGroup="validationAchiv" ErrorMessage="Please enter competition"
                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </p>
                                                    <p>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlPosition"
                                                            Display="Dynamic" ValidationGroup="validationAchiv" ErrorMessage="Please select position."
                                                            InitialValue="Position" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </p>
                                                    <p>
                                                        <asp:Label ID="lblAchivmentmsg" runat="server" ForeColor="Red" ClientIDMode="Static"></asp:Label>
                                                    </p>
                                                    <asp:LinkButton ID="lnkSaveAchivemnt" runat="server" ClientIDMode="Static" CssClass="createGroup"
                                                        ValidationGroup="validationAchiv" Text="Save" OnClick="lnkSaveAchivemnt_Click"
                                                        OnClientClick="javascript:callSaveAch();"></asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton4" runat="server" ClientIDMode="Static" CssClass="cancelGroup"
                                                        Text="Cancel" OnClick="lnkCancelAchivemnt_Click" OnClientClick="javascript:callCancelAch();"></asp:LinkButton>
                                                </div>
                                            </div>
                                            <!--add exp ends-->
                                            <div class="cls">
                                            </div>
                                            <asp:LinkButton ID="lnkAddachive" runat="server" class="addWorkEx" Style="margin-top: 25px;"
                                                Text="Add Achievements and Milestones" OnClientClick="CalllnkAddachive();" OnClick="lnkAddachive_Click"></asp:LinkButton>
                                            <div class="cls">
                                                <p>
                                                    &nbsp;</p>
                                                <p>
                                                    &nbsp;</p>
                                            </div>
                                        </div>
                                        <!--main section ends-->
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkSaveAchivemnt" />
                                        <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
                                        <asp:AsyncPostBackTrigger ControlID="LinkButton4" />
                                        <asp:AsyncPostBackTrigger ControlID="lnkAddachive" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <!--Achivement End  background-color: #000000;-->
                                <div style="display:none;">
                                <asp:Button ID="btnPostCommentSave" runat="server" ClientIDMode="Static" OnClick="btnPostCommentSave_Click" />
                                </div>
                                <asp:UpdateProgress id="updateProgress" runat="server">
                            <ProgressTemplate>
                              <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999;  opacity: 0.7;">
                                 <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/loadingImage.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;margin-top:20%;" class="divProgress" />
                              </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="LinkSave" />
                                <asp:AsyncPostBackTrigger ControlID="lnkuploadDoc" />
                                <asp:AsyncPostBackTrigger ControlID="lnkCancelDoc" />
                                <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
                                <asp:AsyncPostBackTrigger ControlID="btnPostCommentSave" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="cls">
                    <asp:HiddenField ID="hdnPostDelete" ClientIDMode="Static" runat="server" Value="" />
                    <asp:HiddenField ID="hdnAchivmentId" runat="server" ClientIDMode="Static" />
                </div>
                <!--left verticle search list ends-->
            </div>
        </div>
    </div>
    <div class="cls">
    </div>
    <script type="text/javascript">
        function CallHomeMiddle() {
            $("html, body").animate({ scrollTop: 300 }, 1);
        }
        function CallDocMiddle() {
            var offset = $("#txtDocTitle").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $(window).scrollTop();
            var txt = document.getElementById("txtDocTitle");
            var p = GetScreenCordinates(txt);
            $("html, body").animate({ scrollTop: $(window).height() }, 10);
        }
        function CallWorkMiddle() {
            var offset = $("#aAddworkExp").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $("#aAddworkExp").position().top - 500;
            $("html, body").animate({ scrollTop: y }, 10);
        }
        function CallSkillMiddle() {
            var offset = $("#divAddskill").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $("#divAddskill").position().top;
            $("html, body").animate({ scrollTop: $(window).height() + 2000 }, 10);
        }
        function CallEducationMiddle() {
            var offset = $("#txtEduDescription").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $(window).scrollTop();
            $("html, body").animate({ scrollTop: $(window).height() + 2000 }, 10);
        }
        function CallAchiveMiddle() {
            var offset = $("#divAchivement").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $("#divAchivement").position().top + 800;
            $("html, body").animate({ scrollTop: $(window).height() + 2000 }, 10);
        }
        function GetScreenCordinates(obj) {
            var p = {};
            p.x = obj.offsetLeft;
            p.y = obj.offsetTop;
            while (obj.offsetParent) {
                p.x = p.x + obj.offsetParent.offsetLeft;
                p.y = p.y + obj.offsetParent.offsetTop;
                if (obj == document.getElementsByTagName("body")[0]) {
                    break;
                }
                else {
                    obj = obj.offsetParent;
                }
            }
            return p;
        }
        function callBodyDisable() {
            $("body").css("position", "fixed");
            $("body").css("overflow-y", "scroll");
        }
        function callBodyEnable() {
            $('#PopUpCropImage').css('display', 'none');
            $('#lnkCancelProfile').css("box-shadow", "0px 0px 5px #BCBDCE");
            $('.lnkProfileSaves').css("box-shadow", "0px 0px 5px #00B7E5");
            $('#divEditProfile').css('display', 'none');
            $("body").css("position", "static");
            $("body").css("overflow-y", "auto");
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var ir = 0;
            var RegId = '<%= Request.QueryString["RegId"]%>';
            if (RegId == '') {
                $(".endronsecssli").toggleClass('endronsecssli', 'endronsecsslit');
            } else {
                $(".endronsecsslit").toggleClass('endronsecsslit', 'endronsecssli');
            }
            $("ul.skillsetList").children("li").each(function () {
                $(this).children("div").addClass('divends');
                $(".endronsecssli").mouseover(function () {
                    $(this).children('.divends').children("a.endronsecss").css('display', 'block');
                });
                $(".endronsecssli").mouseout(function () {
                    $(this).children('.divends').children("a.endronsecss").css('display', 'none');
                });
            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                var RegId = '<%= Request.QueryString["RegId"]%>';
                if (RegId == '') {
                    $(".endronsecssli").toggleClass('endronsecssli', 'endronsecsslit');
                } else {
                    $(".endronsecsslit").toggleClass('endronsecsslit', 'endronsecssli');
                }

                $("ul.skillsetList").children("li").each(function () {
                    $(this).children("div").addClass('divends');
                    $(".endronsecssli").mouseover(function () {
                        $(this).children('.divends').children("a.endronsecss").css('display', 'block');
                    });
                    $(".endronsecssli").mouseout(function () {
                        $(this).children('.divends').children("a.endronsecss").css('display', 'none');
                    });
                });
            });
        });
    </script>
    <script type="text/javascript">
        var strSelTexts = '';
        $(document).ready(function () {
            $('ul.context li').click(function () {
                $(this).toggleClass('selectedcreateGroup graycreateGroup');
                if ($(this).hasClass("selectedcreateGroup")) {
                    $(this).children("#lnkCatName").removeAttr('style');
                    $(this).children(".unselectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
                } else {
                    $(this).children("#lnkCatName").removeAttr('style');
                    $(this).children(".selectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
                }
                AddSubjectCall($(this).children("#hdnSubCatId").val());
            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('ul.context li').click(function () {
                    $(this).toggleClass('selectedcreateGroup graycreateGroup');
                    if ($(this).hasClass("selectedcreateGroup")) {
                        $(this).children("#lnkCatName").removeAttr('style');
                        $(this).children(".unselectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
                    } else {
                        $(this).children("#lnkCatName").removeAttr('style');
                        $(this).children(".selectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
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
            $('#AddWorkExp').css("display", "none");
        }
        function divCancels() {
            $('#hdnintPostId').val('');
            $('#hdnintPostceIdelet').val('');
            $('#hdnintacheIdelet').val('');
            $('#hdnintedueIdelet').val('');
            $('#hdnintdocIdelete').val('');
            $('#divDeletesucess').css("display", "none");
            $('#lnkDeleteConfirm').css("box-shadow", "0px 0px 0px #00B7E5");
        }
        function CallUserJSON() {
            $.ajax({
                url: 'Home.aspx/GetUserJSONdata',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (data) {
                    $('#divEditProfile').css('display', 'block');
                    var parsed = $.parseJSON(data.d);
                    $.each(parsed, function (i, jsondata) {
                        $('#txtName').val(jsondata.NAME);
                        $('#txtGender').val(jsondata.chrSex);
                        $('#txtLanguage').val(jsondata.vchrLanguages);
                        $('#txtEmailId').val(jsondata.vchrUserName);
                        $('#txtPhone').val(jsondata.intMobile);
                    });
                }
            });
        }
        function getIEVersion() {
            var agent = navigator.userAgent;
            var reg = /MSIE\s?(\d+)(?:\.(\d+))?/i;
            var matches = agent.match(reg);
            if (matches != null) {
                return { major: matches[1], minor: matches[2] };
            }
            return { major: "-1", minor: "-1" };
        }
        function callposts() {
            var ie_version = getIEVersion();
            var is_ie10 = ie_version.major == 10;
            if (is_ie10 != false) {
            }
        }
        var ie_version = getIEVersion();
        var is_ie10 = ie_version.major == 10;
        if (is_ie10 != false) {
            $("#FileUplogo").live("change", function (event) {
                var tmppath = URL.createObjectURL(event.target.files[0]);
                var ext = $('#FileUplogo').val().split('.').pop().toLowerCase();
                if (ext != '') {
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'bmp']) == -1) {
                        if (ext == 'pdf' || ext == 'xlsx' || ext == 'txt' || ext == 'doc' || ext == 'docx' || ext == 'xls' || ext == 'odt' || ext == 'ods') {
                            alert('Please select image or video.');
                        } else {
                            $("#Mediavideo").fadeIn("fast").attr('src', URL.createObjectURL(event.target.files[0]));
                            $("#imgselect").css("display", "none");
                            $("#imgBtnDelete").css("display", "block");
                        }
                    } else {
                        $("#imgselect").fadeIn("fast").attr('src', URL.createObjectURL(event.target.files[0]));
                        $("#Mediavideo").css("display", "none");
                        $("#imgBtnDelete").css("display", "block");
                    }
                } else {
                    $("#imgselect").css("display", "none");
                    $("#Mediavideo").css("display", "none");
                    $("#imgBtnDelete").css("display", "none");
                }
            });
            //---------------------------------------------------------
            $('#lnkPostUpdate').live("click", function (event) {
                if (document.getElementById("txtPostUpdate").value == "" || document.getElementById("txtPostUpdate").value == "Share an update...") {
                    document.getElementById('lblPostMsg').style.display = 'block';
                }
                else {

                    var fileUpload = $("#FileUplogo").get(0);
                    var files = fileUpload.files;
                    var data = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        data.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        type: "POST",
                        url: "handler/FileUploadHandlerHome.ashx",
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (result) {
                            if (result == 'File format not supported.' || result == 'File size should be less than or equal to 3MB.' || result == 'Please choose a video file less than 12MB.' || result == 'please') {
                                $("#hdnErrorMsg").val(result)
                            }
                            else {
                                var v = result.split(":");
                                document.getElementById("hdnPhoto").value = v[0];
                                document.getElementById("hdnDocName").value = v[1];
                            }
                            $("#FileUplogo").val('');
                            document.getElementById("lnkDummyPost").click();
                        },
                        error: function () {
                            alert("There was error uploading files!");
                        }
                    });
                }
            });
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("span.ediDel").click(function () {
                $('#hdnintPostId').val($(this).children('#hdnintPostIdelet').val());
                $('#hdnstrPostDescriptiondele').val($(this).children('#hdnstrPostDescriptiondel').val());
            });

            $("span.ediDelc").click(function () {
                $('#hdnintPostceIdelet').val($(this).children('#hdnintPostcIdelet').val());
                $('#hdnstrPostDescriptioncedel').val($(this).children('#hdnstrPostDescriptioncdel').val());
            });

            $("span.ediDeldoc").click(function () {
                $('#hdnintdocIdelete').val($(this).children('#hdnintdocIdelet').val());
                $('#hdnstrdocDescriptiondele').val($(this).children('#hdnstrdocDescriptiondel').val());
                $('#hdnfilestrFilePathe').val($(this).children('#hdnfilestrFilePath').val());
            });

            $("span.ediDelwork").click(function () {
                $('#hdnintworkeIdelet').val($(this).children('#hdnintworkIdelet').val());
                $('#hdnstrworkeDescriptiondel').val($(this).children('#hdnstrworkDescriptiondel').val());
            });

            $("span.ediDeledu").click(function () {
                $('#hdnintedueIdelet').val($(this).children('#hdninteduIdelet').val());
                $('#hdnstredueDescriptiondel').val($(this).children('#hdnstreduDescriptiondel').val());
            });

            $("span.ediDelach").click(function () {
                $('#hdnintacheIdelet').val($(this).children('#hdnintachIdelet').val());
                $('#hdnstracheDescriptiondel').val($(this).children('#hdnstrachDescriptiondel').val());
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("span.ediDel").click(function () {
                    $('#hdnintPostId').val($(this).children('#hdnintPostIdelet').val());
                    $('#hdnstrPostDescriptiondele').val($(this).children('#hdnstrPostDescriptiondel').val());
                });

                $("span.ediDelc").click(function () {
                    $('#hdnintPostceIdelet').val($(this).children('#hdnintPostcIdelet').val());
                    $('#hdnstrPostDescriptioncedel').val($(this).children('#hdnstrPostDescriptioncdel').val());
                });

                $("span.ediDeldoc").click(function () {
                    $('#hdnintdocIdelete').val($(this).children('#hdnintdocIdelet').val());
                    $('#hdnstrdocDescriptiondele').val($(this).children('#hdnstrdocDescriptiondel').val());
                    $('#hdnfilestrFilePathe').val($(this).children('#hdnfilestrFilePath').val());
                });

                $("span.ediDelwork").click(function () {
                    $('#hdnintworkeIdelet').val($(this).children('#hdnintworkIdelet').val());
                    $('#hdnstrworkeDescriptiondel').val($(this).children('#hdnstrworkDescriptiondel').val());
                });

                $("span.ediDeledu").click(function () {
                    $('#hdnintedueIdelet').val($(this).children('#hdninteduIdelet').val());
                    $('#hdnstredueDescriptiondel').val($(this).children('#hdnstreduDescriptiondel').val());
                });

                $("span.ediDelach").click(function () {
                    $('#hdnintacheIdelet').val($(this).children('#hdnintachIdelet').val());
                    $('#hdnstracheDescriptiondel').val($(this).children('#hdnstrachDescriptiondel').val());
                });
            });
        });
    </script>
    <script type="text/javascript">
        ddsmoothmenu.init({
            mainmenuid: "smoothmenu1", //menu DIV id
            orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
            classname: 'ddsmoothmenu', //class added to menu's outer DIV
            contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
        })
        $(document).ready(function () {
            $("#txtPhone").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $(".photoIcon").mouseover(function () {
                $("#imgCamera").css('display', 'block');
            });
            $(".photoIcon").mouseout(function () {
                $("#imgCamera").css('display', 'none');
            });
            $("#FileUplogo").live("change", function (event) {
                $("#lblVideomsg").css("display", "none");
                $(".divProgress").css("display", "none");
                var URL = window.URL || window.webkitURL || window.mozURL || window.msURL || window.oURL;
                var tmppath = URL.createObjectURL(event.target.files[0]);
                var ext = $('#FileUplogo').val().split('.').pop().toLowerCase();
                if (ext != '') {
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'bmp']) == -1) {
                        if (ext == 'pdf' || ext == 'xlsx' || ext == 'txt' || ext == 'doc' || ext == 'docx' || ext == 'xls' || ext == 'odt' || ext == 'ods') {
                            alert('Please select image or video.');
                        } else {

                            if (navigator.userAgent.indexOf('Safari') != -1 && navigator.userAgent.indexOf('Chrome') == -1) {
                                var pathsimg;
                                //alert('Its Safari');
                                var fileUpload = $("#FileUplogo").get(0);
                                var files = fileUpload.files;
                                var data = new FormData();
                                for (var i = 0; i < files.length; i++) {
                                    data.append(files[i].name, files[i]);
                                }
                                $.ajax({
                                    type: "POST",
                                    url: "handler/FileUploadHandlerHome.ashx",
                                    contentType: false,
                                    processData: false,
                                    data: data,
                                    success: function (result) {
                                        if (result == 'File format not supported.' || result == 'File size should be less than or equal to 3MB.' || result == 'Please choose a video file less than 12MB.' || result == 'please') {
                                        }
                                        else {
                                            var v = result.split(":");
                                            pathsimg = "VideoFiles/" + v[0];
                                            $("#Mediavideo").fadeIn("fast").attr('src', pathsimg);
                                        }
                                    },
                                    error: function () {
                                        alert("There was error uploading files!");
                                    }
                                });

                                $("#Mediavideo").fadeIn("fast").attr('src', pathsimg);
                                $("#imgselect").css("display", "none");
                                $("#imgBtnDelete").css("display", "block");
                                $("#lblVideomsg").css("display", "block");
                            } else {
                                $("#Mediavideo").fadeIn("fast").attr('src', URL.createObjectURL(event.target.files[0]));
                                $("#imgselect").css("display", "none");
                                $("#imgBtnDelete").css("display", "block");
                            }
                        }
                    } else {
                        if (navigator.userAgent.indexOf('Safari') != -1 && navigator.userAgent.indexOf('Chrome') == -1) {
                            var pathsimg;
                            var fileUpload = $("#FileUplogo").get(0);
                            var files = fileUpload.files;
                            var data = new FormData();
                            for (var i = 0; i < files.length; i++) {
                                data.append(files[i].name, files[i]);
                            }
                            $.ajax({
                                type: "POST",
                                url: "handler/FileUploadHandlerHome.ashx",
                                contentType: false,
                                processData: false,
                                data: data,
                                success: function (result) {
                                    if (result == 'File format not supported.' || result == 'File size should be less than or equal to 3MB.' || result == 'Please choose a video file less than 12MB.' || result == 'please') {
                                    }
                                    else {
                                        var v = result.split(":");
                                        pathsimg = "UploadedPhoto/" + v[0];
                                        $("#imgselect").attr('src', pathsimg);
                                    }
                                },
                                error: function () {
                                    alert("There was error uploading files!");
                                }
                            });

                            $("#imgselect").fadeIn("fast").attr('src', pathsimg);
                            $("#Mediavideo").css("display", "none");
                            $("#imgBtnDelete").css("display", "block");
                        } else {
                            $("#imgselect").fadeIn("fast").attr('src', URL.createObjectURL(event.target.files[0]));
                            $("#Mediavideo").css("display", "none");
                            $("#imgBtnDelete").css("display", "block");
                        }
                    }
                } else {
                    $("#imgselect").css("display", "none");
                    $("#Mediavideo").css("display", "none");
                    $("#imgBtnDelete").css("display", "none");
                }
            });
            $("#uploadDoc").change(function (event) {
                var tmppath = URL.createObjectURL(event.target.files[0]);
                $("#lblfilenamee").text($("#uploadDoc").val().split('\\').pop());
                $("#lblfilenamee").removeClass("RedErrormsg")
            });

            $(".showMouseOver").mouseover(function () {
                $(this).parent().children(".imageRolloverBg").css('display', 'block');
            });

            $(".showMouseOver").mouseout(function () {
                $(this).parent().children(".imageRolloverBg").css('display', 'none');
            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#lnkPostUpdate').click(function () {
                    $(".divProgress").css("display", "none");
                    $('#lnkPostUpdate').css("background", "#19A0AA");
                    $('#lnkPostUpdate').css("box-shadow", "0px 0px 5px #00B7E5");
                    if ($('#txtPostUpdate').val().trim() == "" || $('#txtPostUpdate').val().trim() == "Share an update...") {
                        document.getElementById('RequiredFieldValidator2').style.display = "block";
                        setTimeout(
                        function () {
                            $('#lnkPostUpdate').css("background", "#0096a1");
                            $('#lnkPostUpdate').css("box-shadow", "0px 0px 0px #00B7E5");
                        }, 1000);
                    }
                    else {
                        $('#divPostimage').css("display", "block");
                        $('#imgBtnDelete').css("margin", "-41px -2px -2px 1px");
                        $('.UploadFilesHomeHome').css("cursor", "progress");
                        $('#lnkPostUpdate').css("cursor", "progress");
                        var fileUpload = $("#FileUplogo").get(0);
                        var files = fileUpload.files;
                        var data = new FormData();
                        for (var i = 0; i < files.length; i++) {
                            data.append(files[i].name, files[i]);
                        }
                        if ($("#FileUplogo").val() == "") {
                            data = null;
                        }
                        $.ajax({
                            type: "POST",
                            url: "handler/FileUploadHandlerHome.ashx",
                            contentType: false,
                            processData: false,
                            data: data,
                            success: function (result) {
                                if (result == 'File format not supported.' || result == 'File size should be less than or equal to 3MB.' || result == 'Please choose a video file less than 12MB.' || result == 'please') {
                                    $("#hdnErrorMsg").val(result)
                                    $("#hdnDocName").val('');
                                    $("#hdnPhoto").val('');
                                }
                                else {
                                    var v = result.split(":");
                                    $("#hdnPhoto").val(v[0]);
                                    $("#hdnDocName").val(v[1]);
                                }
                                $("#FileUplogo").val('');
                                document.getElementById('lnkDummyPost').click();
                            },
                            error: function () {
                                alert("There was error uploading files!");
                                document.getElementById('lnkDummyPost').click();
                            }
                        });
                    }
                });
            });
        })
        $(document).ready(function () {
            $('#lnkPostUpdate').click(function () {
                $(".divProgress").css("display", "none");
                $('#lnkPostUpdate').css("background", "#19A0AA");
                $('#lnkPostUpdate').css("box-shadow", "0px 0px 5px #00B7E5");
                if ($('#txtPostUpdate').val().trim() == "" || $('#txtPostUpdate').val().trim() == "Share an update...") {
                    document.getElementById('RequiredFieldValidator2').style.display = "block";
                    setTimeout(
                        function () {
                            $('#lnkPostUpdate').css("background", "#0096a1");
                            $('#lnkPostUpdate').css("box-shadow", "0px 0px 0px #00B7E5");
                        }, 1000);
                }
                else {
                    $('#divPostimage').css("display", "block");
                    $('#imgBtnDelete').css("margin", "-41px -2px -2px 1px");
                    $('.UploadFilesHomeHome').css("cursor", "progress");
                    $('#lnkPostUpdate').css("cursor", "progress");
                    var fileUpload = $("#FileUplogo").get(0);
                    var files = fileUpload.files;
                    var data = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        data.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        type: "POST",
                        url: "handler/FileUploadHandlerHome.ashx",
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (result) {
                            if (result == 'File format not supported.' || result == 'File size should be less than or equal to 3MB.' || result == 'Please choose a video file less than 12MB.' || result == 'please') {
                                $("#hdnErrorMsg").val(result)
                                $("#hdnDocName").val('');
                                $("#hdnPhoto").val('');
                            }
                            else {
                                var v = result.split(":");
                                $("#hdnPhoto").val(v[0]);
                                $("#hdnDocName").val(v[1]);
                            }
                            $("#FileUplogo").val('');
                            document.getElementById("lnkDummyPost").click();
                        },
                        error: function () {
                            alert("There was error uploading files!");
                            document.getElementById('lnkDummyPost').click();
                        }
                    });
                }
            });
        })
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById('pLoadMore').style.display = 'none';
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                ddsmoothmenu.init({
                    mainmenuid: "smoothmenu1", //menu DIV id
                    orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
                    classname: 'ddsmoothmenu', //class added to menu's outer DIV
                    contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
                })
                var ir = 0;
                $(".endronsecssimg").mouseover(function () {
                    $(this).parent().children(".endronsecss").css('display', 'block');
                });

                $(".endronsecss").mouseout(function () {
                    $(this).parent().children(".endronsecss").css('display', 'none');
                });

                $(".endronsecssimg").mouseout(function () {
                    $(this).parent().children(".endronsecss").css('display', 'none');
                });
                $(".scroll").click(function (event) {
                    $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 500);
                });
                $(".showMouseOver").mouseover(function () {
                    $(this).parent().children(".imageRolloverBg").css('display', 'block');
                });
                $(".showMouseOver").mouseout(function () {
                    $(this).parent().children(".imageRolloverBg").css('display', 'none');
                });

                $(".photoIcon").mouseover(function () {
                    $("#imgCamera").css('display', 'block');
                });
                $(".photoIcon").mouseout(function () {
                    $("#imgCamera").css('display', 'none');
                });
                $("#uploadDoc").change(function (event) {
                    var tmppath = URL.createObjectURL(event.target.files[0]);
                    $("#lblfilenamee").text($("#uploadDoc").val().split('\\').pop());
                    var fileUpload = $("#uploadDoc").get(0);
                    var files = fileUpload.files;
                    var data = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        data.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        type: "POST",
                        url: "handler/FileUploadHandler.ashx",
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (result) {
                            if (result == "File format not supported." || result == "File size should be less than or equal to 3MB.") {
                                $("#hdnErrorMsg").val(result)
                            }
                            else {
                                var v = result.split(":");
                                $("#hdnFilePath").val(v[0]);
                                $("#hdnDocName").val(v[1]);
                            }
                        },
                        error: function () {
                            alert("There was error uploading files!");
                        }
                    });
                });
                $("#imgGroup").click(function () {
                    $("#divfrdgrp").removeClass("fGroupBox frd").addClass("fGroupBox grp");
                    $("#divgroupSection").show();
                    $("#divFriendSection").hide();
                    return false;
                });
                $("#imgFriend").click(function () {
                    $("#divfrdgrp").removeClass("fGroupBox grp").addClass("fGroupBox frd");
                    $("#divFriendSection").show();
                    $("#divgroupSection").hide();
                    return false;
                });
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById('pLoadMore').style.display = 'none';
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(window).scroll(function () {
                    if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                        $(".divProgress").css("display", "none");
                        //document.getElementById('pLoadMore').style.display = 'block';
                        if ($("#hdnTabIds").val() == 0) {
                            var v = $("#lblNoMoreRslt").text();
                            var maxCount = $("#hdnMaxcount").val();
                            if (maxCount <= 10) {
                                $("#lblNoMoreRslt").css("display", "none");
                            } else {
                                if (v != "No more results available...") {
                                    var elm = '#imgLoadMore1';
                                    $(elm).click();
                                } else {
                                    document.getElementById('pLoadMore').style.display = 'none';
                                }
                            }
                        }
                    }
                });
                if ($("#lblNotifyCount").text() == '0') {
                    document.getElementById("divNotification1").style.display = "none";
                }
                if ($("#lblInboxCount").text() == '0') {
                    document.getElementById("divInbox").style.display = "none";
                }
                $("#txtPhone").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
                $("#txtFromYear").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
                $("#txtToYear").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
                $("#txtEducationFromdt").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
                $("#txtEducationTodt").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
            });
            $('#lnkUpload').click(function (event) {
                $('#FileUplogo').click();
            });
            $('#FileUplogo').change(function (click) {
                $('#lblfilename').val(this.value);
            });
        });
    </script>
    <script type="text/javascript">
        $(window).scroll(function () {
            if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                $(".divProgress").css("display", "none");
                document.getElementById('pLoadMore').style.display = 'block';
                if ($("#hdnTabIds").val() == 0) {
                    var v = $("#lblNoMoreRslt").text();
                    var maxCount = $("#hdnMaxcount").val();
                    if (maxCount <= 10) {
                        document.getElementById('pLoadMore').style.display = 'none';
                        $("#lblNoMoreRslt").css("display", "none");
                    } else {
                        if (v != "No more results available...") {
                            document.getElementById('imgLoadMore1').click();
                        }
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
            $("#imgGroup").click(function () {
                $("#divfrdgrp").removeClass("fGroupBox frd").addClass("fGroupBox grp");
                $("#divgroupSection").show();
                $("#divFriendSection").hide();
                return false;
            });
            $("#imgFriend").click(function () {
                $("#divfrdgrp").removeClass("fGroupBox grp").addClass("fGroupBox frd");
                $("#divFriendSection").show();
                $("#divgroupSection").hide();
                return false;
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#rdDownloadYes').click(function () {
                $('#rdDownloadYes').prop('checked', true);
                $('#rdDownloadNo').prop('checked', false);
            });
            $('#rdDownloadNo').click(function () {
                $('#rdDownloadYes').prop('checked', false);
                $('#rdDownloadNo').prop('checked', true);
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //called when key is pressed in textbox
            $("#txtFromYear").keypress(function (e) {
                //if the letter is not digit then display error and don't type anything
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $("#txtToYear").keypress(function (e) {
                //if the letter is not digit then display error and don't type anything
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            //called when key is pressed in textbox
            $("#txtEducationFromdt").keypress(function (e) {
                //if the letter is not digit then display error and don't type anything
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $("#txtEducationTodt").keypress(function (e) {
                //if the letter is not digit then display error and don't type anything
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        });
    </script>
    <script type="text/javascript">
        function CallExpNumaric() {
            //called when key is pressed in textbox
            $("#txtFromYear").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $("#txtToYear").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        }
    </script>
    <script type="text/javascript">
        function callAcivemet() {
            $("#divAchivement").show();
            CallAchiveMiddle();
        }
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("#LinkButton1").click(function () {
                    $("#AddWorkExp").hide();
                    ClearExpControls();
                });
                $("#lnkAddskill").click(function () {
                    $("#divAddskill").show();
                    CallSkillMiddle();
                });
                $("#lnkCancelEducation").click(function () {
                    $("#divEducation").hide();
                    ClearEducation();
                });
                $("#LinkButton4").click(function () {
                    $("#divAchivement").hide();
                    ClearAchieveMents();
                });
            });
        });
        function ClearExpControls() {
            $("#txtInstituteName").val('');
            $("#txtDegree").val('');
            $("#fromMonth").val('');
            $("#txtFromYear").val('');
            $("#toMonth").val('');
            $("#txtToYear").val('');
            $("#ctl00_ContentPlaceHolder1_txtDescription").val('');
            $("#chkAtPresent").attr('checked', false);
        }
        function ClearEducation() {
            $("#txtInstitute").val('');
            $("#txtTitle").val('');
            $("#ddlFromMonth").val('');
            $("#txtEducationFromdt").val('');
            $("#ddlToMonth").val('');
            $("#txtEducationTodt").val('');
            $("#txtEduDescription").val('');
            $("#chkEducation").attr('checked', false);
        }
        function ClearAchieveMents() {
            $("#txtCompitition").val('');
            $("#ctl00_ContentPlaceHolder1_txtAdditionalAwrd").val('');
            $("#ctl00_ContentPlaceHolder1_txtAchivDescription").val('');
            $("#ddlMilestone").val('0');
            $("#ddlPosition").val('0');
        }
    </script>
    <script type="text/javascript">
        function removeimages() {
            $("#Mediavideo").fadeIn("fast").attr('src', '');
            $("#Mediavideo").css("display", "none");
            $("#imgselect").fadeIn("fast").attr('src', '');
            $("#imgselect").css("display", "none");
            $('#FileUplogo').val('');
            $("#imgBtnDelete").css("display", "none");
            $("#lblVideomsg").css("display", "none");
        }
        function fncsave() {
            if ($("#imgPrivate").attr('src') == "images/unchk1.png") {
                $("#imgPrivate").attr('src', 'images/chk1.png')

                if ($("#hdnimgPrivate").val() == 0) {
                    $("#hdnimgPrivate").val('1');
                }

            } else {
                if ($("#hdnimgPrivate").val() == 1) {
                    $("#hdnimgPrivate").val('0');
                }

                $("#imgPrivate").attr('src', 'images/unchk1.png')
            }
        }
        function fncsavedow() {
            if ($("#imgDownload").attr('src') == "images/unchk1.png") {
                $("#imgDownload").attr('src', 'images/chk1.png')
                if ($("#hdnimgDownload").val() == 0) {
                    $("#hdnimgDownload").val('1');
                }

            } else {
                $("#imgDownload").attr('src', 'images/unchk1.png')

                if ($("#hdnimgDownload").val() == 1) {
                    $("#hdnimgDownload").val('0');
                }
            }

        }
        function groupConnChange() {
            $("#imgGroup").click(function () {
                $("#divfrdgrp").removeClass("fGroupBox frd").addClass("fGroupBox grp");
                $("#divgroupSection").show();
                $("#divFriendSection").hide();
                return false;
            });
            $("#imgFriend").click(function () {
                $("#divfrdgrp").removeClass("fGroupBox grp").addClass("fGroupBox frd");
                $("#divFriendSection").show();
                $("#divgroupSection").hide();
                return false;
            });
            if ($("#lblNotifyCount").text() == '0') {
                document.getElementById("divNotification1").style.display = "none";
            }
            if ($("#lblInboxCount").text() == '0') {
                document.getElementById("divInbox").style.display = "none";
            }
        }
        function callDocUpload() {
            $("#uploadDoc").change(function (event) {
                var tmppath = URL.createObjectURL(event.target.files[0]);
                $("#lblfilenamee").text($("#uploadDoc").val().split('\\').pop());
            });
        }
    </script>
    <div class="cls">
    </div>
    <asp:HiddenField ID="hdnTabIds" Value="0" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnPhoto" Value="" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnDocName" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnErrorMsg" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnImageProfile" runat="server" ClientIDMode="Static" />
    <script type="text/javascript">
        $(".viewAllComments").click(function () {
            $("#lstChild").slideToggle("slow");
            $(".viewAllComments").toggle();
        });
    </script>
    <script type="text/jscript">
        function Cancel() {
            document.getElementById("PopUpCropImage").style.display = 'none';
            document.getElementById("divCancelPopup").style.display = 'none';
            return false;
        }
        function CallMessageNotification() {
            if ($("#lblNotifyCount").text() == '0') {
                document.getElementById("divNotification1").style.display = "none";
            }
            if ($("#lblInboxCount").text() == '0') {
                document.getElementById("divInbox").style.display = "none";
            }
        }
    </script>
    <script type="text/javascript">
        function ToogleComment(ctl) {
            $(ctl).find("#commentTxt1").slideToggle("slow");
            $(ctl).find("#commnetBtn1 img").toggle();
        }
    </script>
    <script type="text/javascript">
        function ShowLoading(id) {
            location.href = '#' + id;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.innerTabHome').click(function () {
                $("#imgUser").attr("src", $("#hdnImageProfile").val());
                $("#PopUpCropImage").css("display", "none");
                $('#hdnTabIds').val(0);
            });
            $('.innerTabDoc').click(function () {
                $("#imgUser").attr("src", $("#hdnImageProfile").val());
                $("#PopUpCropImage").css("display", "none");
                $('#hdnTabIds').val(1);
                $('#divDocumentUplaod').css('display', 'none');
            });
            $('.innerWrkex').click(function () {
                $("#imgUser").attr("src", $("#hdnImageProfile").val());
                $("#PopUpCropImage").css("display", "none");
                $('#hdnTabIds').val(1);
            });
            $('.innerEdu').click(function () {
                $("#imgUser").attr("src", $("#hdnImageProfile").val());
                $("#PopUpCropImage").css("display", "none");
                $('#hdnTabIds').val(1);
            });
            $('.innerAch').click(function () {
                $("#imgUser").attr("src", $("#hdnImageProfile").val());
                $("#PopUpCropImage").css("display", "none");
                $('#hdnTabIds').val(1);
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.innerTabHome').click(function () {
                    $("#imgUser").attr("src", $("#hdnImageProfile").val());
                    $("#PopUpCropImage").css("display", "none");
                    $('#hdnTabIds').val(0);
                });
                $('.innerTabDoc').click(function () {
                    $("#imgUser").attr("src", $("#hdnImageProfile").val());
                    $("#PopUpCropImage").css("display", "none");
                    $('#hdnTabIds').val(1);
                    $('#divDocumentUplaod').css('display', 'none');
                });
                $('.innerWrkex').click(function () {
                    $("#imgUser").attr("src", $("#hdnImageProfile").val());
                    $("#PopUpCropImage").css("display", "none");
                    $('#hdnTabIds').val(1);
                });
                $('.innerEdu').click(function () {
                    $("#imgUser").attr("src", $("#hdnImageProfile").val());
                    $("#PopUpCropImage").css("display", "none");
                    $('#hdnTabIds').val(1);
                });
                $('.innerAch').click(function () {
                    $("#imgUser").attr("src", $("#hdnImageProfile").val());
                    $("#PopUpCropImage").css("display", "none");
                    $('#hdnTabIds').val(1);
                });
            });
        });
        function callinnerGroupBox() {
            $('.innerGroupBox').css('height', 'auto');
        }
        $(document).ready(function () {
            $("#homeId li:first").addClass('active');
        });
        function CallDocLI() {
            $("#PopUpCropImage").css("display", "none");
            $("#imgUser").attr("src", $("#hdnImageProfile").val());
        }
        function CallWorkLI() {
            $("#PopUpCropImage").css("display", "none");
            $("#imgUser").attr("src", $("#hdnImageProfile").val());
        }
        function CallEduLI() {
            $("#PopUpCropImage").css("display", "none");
            $("#imgUser").attr("src", $("#hdnImageProfile").val());
            $('#lnkDeleteConfirm').css("box-shadow", "0px 0px 0px #00B7E5");
        }
        function CallAchLI() {
            $("#PopUpCropImage").css("display", "none");
            $("#imgUser").attr("src", $("#hdnImageProfile").val());
        }
        function CallHomeLI() {
            $('.UploadFilesHomeHome').css("cursor", "pointer");
            $('#lnkPostUpdate').css("cursor", "pointer");
            $("#PopUpCropImage").css("display", "none");
            $("#imgUser").attr("src", $("#hdnImageProfile").val());
        }
        function OpenDocentry() {
            $('#divDocumentUplaod').css('display', 'block');
        }
        function ShowAddUserProcess() {
            $('#divAddUser').css('display', 'block');
            $('#lnkAddFriend').css('cursor', 'wait');
        }
        function HideAddUserProcess() {
            $('#divAddUser').css('display', 'none');
            $('#lnkAddFriend').css('cursor', 'pointer');
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.divtabLikespost').click(function (e) {
                e.preventDefault();
                var LikepostCount = $(this).find('#lnkLikePost').text();
                if ($(this).find('#hdnPostLikeUserId').val() != $('#hdnUserID').val()) {
                    var d = parseInt(LikepostCount) + parseInt(1);
                    $(this).find('#lnkLikePost').text(d);
                    $(this).find('#hdnPostLikeUserId').val($('#hdnUserID').val())
                }
                var UserId = $('#hdnUserID').val();
                var hdnPostUpdateId = $(this).find('#hdnPostUpdateId').val();
                $.ajax({
                    url: 'Home.aspx/UpdateUserStatusLike',
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{UserId:'" + UserId + "',hdnPostUpdateId:'" + hdnPostUpdateId + "'}",
                    async: true,
                    success: function (data) {
                    }
                });
            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.divtabLikespost').click(function (e) {
                    e.preventDefault();
                    var LikepostCount = $(this).find('#lnkLikePost').text();
                    if ($(this).find('#hdnPostLikeUserId').val() != $('#hdnUserID').val()) {
                        var d = parseInt(LikepostCount) + parseInt(1);
                        $(this).find('#lnkLikePost').text(d);
                        $(this).find('#hdnPostLikeUserId').val($('#hdnUserID').val())
                    }
                    var UserId = $('#hdnUserID').val()
                    var hdnPostUpdateId = $(this).find('#hdnPostUpdateId').val();
                    $.ajax({
                        url: 'Home.aspx/UpdateUserStatusLike',
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: "{UserId:'" + UserId + "',hdnPostUpdateId:'" + hdnPostUpdateId + "'}",
                        async: true,
                        success: function (data) {
                        }
                    });
                });
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.divtabLikespostcomm').click(function (e) {
                e.preventDefault();
                var LikepostCount = $(this).find('#lnkLikeComment').text();
                if ($(this).find('#hdnCommentLikeUserId').val() != $('#hdnUserID').val()) {
                    var d = parseInt(LikepostCount) + parseInt(1);
                    $(this).find('#lnkLikeComment').text(d)
                    $(this).find('#hdnCommentLikeUserId').val($('#hdnUserID').val())
                }
                var UserId = $('#hdnUserID').val()
                var hdnCommentId = $(this).find('#hdnCommentId').val();
                $.ajax({
                    url: 'Home.aspx/UpdateUserStatusCommLike',
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{UserId:'" + UserId + "',hdnCommentId:'" + hdnCommentId + "'}",
                    async: true,
                    success: function (data) {
                        //                        var msg = eval('(' + data.d + ')');
                        //                        if (msg[0].Action != 0) {
                        //                        }
                    }
                });
            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.divtabLikespostcomm').click(function (e) {
                    e.preventDefault();
                    var LikepostCount = $(this).find('#lnkLikeComment').text();
                    if ($(this).find('#hdnCommentLikeUserId').val() != $('#hdnUserID').val()) {
                        var d = parseInt(LikepostCount) + parseInt(1);
                        $(this).find('#lnkLikeComment').text(d)
                        $(this).find('#hdnCommentLikeUserId').val($('#hdnUserID').val())
                    }
                    var UserId = $('#hdnUserID').val()
                    var hdnCommentId = $(this).find('#hdnCommentId').val();
                    $.ajax({
                        url: 'Home.aspx/UpdateUserStatusCommLike',
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: "{UserId:'" + UserId + "',hdnCommentId:'" + hdnCommentId + "'}",
                        async: true,
                        success: function (data) {
                        }
                    });
                });
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#lnkCancelProfilediv').click(function (e) {
                e.preventDefault();
                $('#lnkCancelProfilediv').css("background", "#0096a1");
                $('#lnkCancelProfilediv').css("box-shadow", "0px 0px 0px #00B7E5");
                var UserId = $('#hdnUserID').val()
                $.ajax({
                    url: 'Home.aspx/LoadProfileImage',
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{UserId:'" + UserId + "'}",
                    async: true,
                    success: function (data) {
                        var msg = eval('(' + data.d + ')');
                        $("#PopUpCropImage").css("display", "none");
                        $("#imgUser").attr("src", msg[0].path);
                        $("#imgProfilePic").attr("src", msg[0].path);
                        $("#hdnImageProfile").val(msg[0].path);
                    }
                });
            });
            $('.UploadFilesHomeHome').click(function (e) {
                $('.UploadFilesHomeHome').css("box-shadow", "0px 0px 5px #BCBDCE");
            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#lnkCancelProfilediv').click(function (e) {
                    e.preventDefault();
                    $('#lnkCancelProfilediv').css("background", "#0096a1");
                    $('#lnkCancelProfilediv').css("box-shadow", "0px 0px 0px #00B7E5");
                    var UserId = $('#hdnUserID').val()
                    $.ajax({
                        url: 'Home.aspx/LoadProfileImage',
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: "{UserId:'" + UserId + "'}",
                        async: true,
                        success: function (data) {
                            $("#PopUpCropImage").css("display", "none");
                            var msg = eval('(' + data.d + ')');
                            $("#imgUser").attr("src", msg[0].path);
                            $("#imgProfilePic").attr("src", msg[0].path);
                            $("#hdnImageProfile").val(msg[0].path);
                        }
                    });
                });
            });
        });
        function CallCameraload() {
            $(".divProgress").css("display", "none");
            $("#imgCamera").css("display", "none");
            $("#divProilePic").css("display", "block");
        }
    </script>
    <script type="text/javascript">
        function callDocSave() {
            $('#LinkSave').css("box-shadow", "0px 0px 5px #00B7E5");
            $('#LinkSave').css("background", "#00A5AA");
            if ($('#txtDocTitle').text() == '') {
                setTimeout(
                function () {
                    $('#LinkSave').css("background", "#0096a1");
                    $('#LinkSave').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
            if ($('#lblfilenamee').text() == 'Please select file to upload.') {
                $('#LinkSave').css("box-shadow", "0px 0px 5px #00B7E5");
                $('#LinkSave').css("background", "#00A5AA");
                setTimeout(
                function () {
                    $('#LinkSave').css("background", "#0096a1");
                    $('#LinkSave').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
        }
        function callDoccancel() {
            $('#lnkCancelDoc').css("background", "#D0D0D0");
            $('#lnkCancelDoc').css("border", "2px solid #BCBDCE");
        }
        function callSaveExp() {
            $('#lnlSaveExp').css("background", "#00A5AA");
            $('#lnlSaveExp').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtInstituteName').text() == '' || $('#txtDegree').text() == '' || $('#fromMonth').text() == 'Month'
            || $('#txtFromYear').text() == 'Year' || $('#toMonth').text() == 'Month' || $('#txtToYear').text() == 'Year') {
                setTimeout(
                function () {
                    $('#lnlSaveExp').css("background", "#0096a1");
                    $('#lnlSaveExp').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
        }
        function CallAddSkill() {
            $("#PopUpCropImage").css("display", "none");
            $('#btnAreaExpSave').css("background", "#00A5AA");
            $('#btnAreaExpSave').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#lblareaskill').text() == 'Please enter area of expertise.') {
                setTimeout(
                function () {
                    $('#btnAreaExpSave').css("background", "#0096a1");
                    $('#btnAreaExpSave').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
        }
        function callCancelExp() {
            $('#LinkButton1').css("background", "#D0D0D0");
            $('#LinkButton1').css("border", "2px solid #BCBDCE");
        }
        function callSaveSkill() {
            $('#lnkSaveSkill').css("background", "#00A5AA");
            $('#lnkSaveSkill').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#lblareaskill').text() == 'Please add skill.') {
                setTimeout(
                function () {
                    $('#lnkSaveSkill').css("background", "#0096a1");
                    $('#lnkSaveSkill').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
        }
        function callCancelSkill() {
            $('#lnkCancelSkill').css("background", "#D0D0D0");
            $('#lnkCancelSkill').css("border", "2px solid #BCBDCE");
        }
        function callSaveEdu() {
            $('#lnkSaveEducation').css("background", "#00A5AA");
            $('#lnkSaveEducation').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtInstitute').text() == '' || $('#txtTitle').text() == '' || $('#ddlFromMonth').text() == 'Month'
            || $('#txtEducationFromdt').text() == 'Year' || $('#ddlToMonth').text() == 'Month' || $('#txtEducationTodt').text() == 'Year') {
                setTimeout(
                function () {
                    $('#lnkSaveEducation').css("background", "#0096a1");
                    $('#lnkSaveEducation').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
        }
        function callCancelEdu() {
            $('#lnkCancelEducation').css("background", "#D0D0D0");
            $('#lnkCancelEducation').css("border", "2px solid #BCBDCE");
        }
        function callSaveAch() {
            $('#lnkSaveAchivemnt').css("background", "#00A5AA");
            $('#lnkSaveAchivemnt').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtCompitition').text() == '' || $('#ddlMilestone').text() == 'Type of Milestone' || $('#ddlPosition').text() == 'Position') {
                setTimeout(
                function () {
                    $('#lnkSaveAchivemnt').css("background", "#0096a1");
                    $('#lnkSaveAchivemnt').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
            if ($('#lblAchivmentmsg').text() == 'Please select milestone.' || $('#lblAchivmentmsg').text() == 'Please select position.') {
                setTimeout(
                function () {
                    $('#lnkSaveAchivemnt').css("background", "#0096a1");
                    $('#lnkSaveAchivemnt').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
        }
        function callCancelAch() {
            $('#LinkButton4').css("background", "#D0D0D0");
            $('#LinkButton4').css("border", "1px solid #BCBDCE");
        }
        function CallAddFriends() {
            $(".divProgress").css("display", "none");
            $('#aAddConnection').css("background", "#D0D0D0");
            $('#aAddConnection').css("box-shadow", "0px 0px 5px #00B7E5");
        }
        function CallNewGroups() {
            $(".divProgress").css("display", "none");
            $('#lnkNewGroups').css("background", "#D0D0D0");
            $('#lnkNewGroups').css("box-shadow", "0px 0px 5px #00B7E5");
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtDocTitle").keydown(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $("#txtInstituteName").keydown(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $("#txtDegree").keydown(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $("#txtInstitute").keydown(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $("#txtTitle").keydown(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $("#txtCompitition").keydown(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $("#ctl00_ContentPlaceHolder1_txtAdditionalAwrd").keydown(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $("#txtAreaExpert").keydown(function (e) {
                if (e.keyCode == 13) {
                    $("#btnAreaExpSave1").click();
                    return false;
                }
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("#txtDocTitle").keydown(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $("#txtInstituteName").keydown(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $("#txtDegree").keydown(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $("#txtInstitute").keydown(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $("#txtTitle").keydown(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $("#txtCompitition").keydown(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $("#ctl00_ContentPlaceHolder1_txtAdditionalAwrd").keydown(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $("#txtAreaExpert").keydown(function (e) {
                    if (e.keyCode == 13) {
                        $("#btnAreaExpSave1").click();
                        return false;
                    }
                });
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("span.spComments").live("keydown", function (e) {
                if (e.keyCode == 13) {
                    if ($(this).find('.addCommentTxt').val().trim() != '') {
                        if (navigator.userAgent.indexOf('Safari') != -1) {
                            e.preventDefault();
                            $.ajax({
                                url: 'Home.aspx/InsertPostCommentSafari',
                                type: 'POST',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: "{PostComment:'" + $(this).find('.addCommentTxt').val() + "',PostIDS:'" + $(this).find('#hdnPostIDs').val() + "',intCommentId:'" + $('#lblintCommentId').text() + "'}",
                                async: true,
                                success: function (data) {
                                    $('#btnPostCommentSave').click();
                                    $('#lblintCommentId').text('');
                                }
                            });
                        }
                    }
                }
            });
        });
        function divdeletes() {
            $('#lnkDeleteConfirm').css("box-shadow", "0px 0px 5px #00B7E5");
            $(".divProgress").css("display", "none");
            $('#divDeletesucess').css("display", "none");
        }
        function CallUploadDoc() {
            $(".divProgress").css("display", "none");
            $('#lnkuploadDoc').css("box-shadow", "0px 0px 2px #00B7E5");
            $('#lnkuploadDoc').css("background", "#DCDBDB");
        }
        function CallaAddworkExp() {
            $('#aAddworkExp').css("box-shadow", "0px 0px 2px #00B7E5");
            $('#aAddworkExp').css("background", "#DCDBDB");
        }
        function CalllnkAddskill() {
            $('#ctl00_ContentPlaceHolder1_lnkAddskill').css("box-shadow", "0px 0px 2px #00B7E5");
            $('#ctl00_ContentPlaceHolder1_lnkAddskill').css("background", "#DCDBDB");
        }
        function CalllnlAddEducation() {
            $('#ctl00_ContentPlaceHolder1_lnlAddEducation').css("box-shadow", "0px 0px 2px #00B7E5");
            $('#ctl00_ContentPlaceHolder1_lnlAddEducation').css("background", "#DCDBDB");
        }
        function CalllnkAddachive() {
            $('#ctl00_ContentPlaceHolder1_lnkAddachive').css("box-shadow", "0px 0px 2px #00B7E5");
            $('#ctl00_ContentPlaceHolder1_lnkAddachive').css("background", "#DCDBDB");
        }
        function CallProfilename() {
            $('#ctl00_lblUserName').text($('#ctl00_ContentPlaceHolder1_lblUserProfName').text());
        }
    </script>
</asp:Content>
