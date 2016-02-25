<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Polls-VoteComment.aspx.cs" Inherits="Polls_VoteComment" %>

<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
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
        .Chartcss
        {
            height: 296px;
            background-color: Transparent;
            color: Transparent;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updateEvents" runat="server" ChildrenAsTriggers="true">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkVote" />
            <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
            <asp:AsyncPostBackTrigger ControlID="lnkDeleteCancel" />
        </Triggers>
        <ContentTemplate>
            <div class="container">
                <div class="cls">
                </div>
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
                <div class="innerDocumentContainerGroup" style="margin-top: -6px;">
                    <div class="innerContainer">
                        <!--groups top box starts-->
                        <asp:UpdatePanel ID="uppal" runat="server">
                            <ContentTemplate>
                                <Group:GroupDetails ID="GroupDetails1" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!--groups top box ends-->
                        <!--left box starts-->
                        <asp:UpdatePanel ID="update" runat="server">
                            <ContentTemplate>
                                <div class="innerGroupBoxnew" style="width: 960px">
                                    <div class="innerContainerLeft" style="width: 900px; padding-left: 20px;">
                                        <div class="tagContainer" style="width: 900px">
                                            <div class="forumsTabs" style="width: 100%; margin: 2px 0px 0; height: 75px;">
                                                <ul>
                                                    <li>
                                                        <asp:LinkButton ID="lnkProfile" runat="server" Text="Profile" ClientIDMode="Static"
                                                            OnClick="lnkProfile_Click"></asp:LinkButton></li>
                                                    <li>
                                                        <div id="DivHome" runat="server">
                                                            <asp:LinkButton ID="lnkHome" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                                                        </div>
                                                    </li>
                                                    <li>
                                                        <div id="DivForumTab" runat="server" clientidmode="Static">
                                                            <asp:LinkButton ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static"
                                                                OnClick="lnkForumTab_Click"></asp:LinkButton></div>
                                                    </li>
                                                    <li>
                                                        <div id="DivUploadTab" runat="server" clientidmode="Static">
                                                            <asp:LinkButton ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static"
                                                                OnClick="lnkUploadTab_Click"></asp:LinkButton>
                                                        </div>
                                                    </li>
                                                    <li>
                                                        <div id="DivPollTab" runat="server" clientidmode="Static">
                                                            <asp:LinkButton ID="lnkPollTab" runat="server" Text="Poll" ClientIDMode="Static"
                                                                class="forumstabAcitve" OnClick="lnkPollTab_Click"></asp:LinkButton>
                                                        </div>
                                                    </li>
                                                    <li>
                                                        <div id="DivEventTab" runat="server" clientidmode="Static">
                                                            <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                                                                OnClick="lnkEventTab_Click"></asp:LinkButton></div>
                                                    </li>
                                                    <li>
                                                        <div id="DivMemberTab" runat="server" clientidmode="Static">
                                                            <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                                                OnClick="lnkEventMemberTab_Click"></asp:LinkButton></div>
                                                    </li>
                                                </ul>
                                                <div class="cls">
                                                </div>
                                                <div>
                                                    <img src="images/recentBlogs.png" align="absmiddle" style="margin-top: -10px; margin-right: 8px;
                                                        margin-left: -7%;" />
                                                    <asp:LinkButton ID="lnkBack" runat="server" class="recentPoll" OnClick="lnkBack_Click"
                                                        Style="margin-top: -8px;" Text="Back"></asp:LinkButton>
                                                </div>
                                                <div class="cls">
                                                </div>
                                            </div>
                                        </div>
                                        <div style="height: 99%; margin-top: 56px; border: #c6c8ca solid 1px;">
                                            <div class="cls">
                                                <br />
                                            </div>
                                            <br />
                                            <asp:ListView ID="lstPoll" runat="server" OnItemDataBound="lstPoll_ItemDataBound"
                                                OnItemCommand="lstPoll_ItemCommand">
                                                <LayoutTemplate>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr id="itemPlaceHolder" runat="server">
                                                        </tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="hdnRegistrationId" Value='<%# Eval("intRegistrationId") %>'
                                                                runat="server" ClientIDMode="Static" />
                                                            <asp:HiddenField ID="hdnVotingPattern" runat="server" Value='<%# Eval("strVotingPattern") %>'
                                                                ClientIDMode="Static" />
                                                            <div style="border-bottom: 0px; width: 100% !important;">
                                                                <div class="thumbnail" style="display: none;">
                                                                    <img id="imgprofile" runat="server" style="width: 46px; height: 52px; float: left"
                                                                        src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' />
                                                                </div>
                                                                <div class="searchListTxt" style="width: 815px !important; margin-left: 9%; margin-top: 20px;">
                                                                    <table style="width: 600px !important;">
                                                                        <tr>
                                                                            <td>
                                                                                <div class="forumsTitle breakallwords" style="width: 625px;">
                                                                                    <asp:Label ID="lnkPollName" Text='<%# Eval("strPollName") %>' runat="server" Style="font-size: 26px;
                                                                                        color: #666666; line-height: 25px;"></asp:Label></div>
                                                                                <div style="margin-right: 0px; margin-top: 0px; text-align: right;">
                                                                                    <span style="color: #9c9c9c; font-size: 14px; font-family: Calibri; font-style: italic;">
                                                                                        By </span>
                                                                                    <asp:LinkButton ID="lblPostlink" ToolTip="View profile" Style="color: #9c9c9c; font-size: 14px;
                                                                                        font-style: italic; margin: -5px 0 0; padding: 0 10px; width: 305px; text-decoration: none;
                                                                                        padding-left: 2px; font-family: Calibri;" Text='<%# Eval("UserName") %>' CommandName="Details"
                                                                                        runat="server"></asp:LinkButton>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <p class="breakallwords" style="width: 700px; max-width: 100%; margin: 10px 0px 10px 0px;">
                                                                                    <asp:Label ID="lblDescription" Text='<%# Eval("strDescription") %>' runat="server"
                                                                                        Style="color: #9c9c9c; font-size: 16px; font-family: Calibri;"></asp:Label>
                                                                                </p>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:UpdatePanel ID="updele" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <p style="color: #9c9c9c; font-size: 12px; margin: 0px 0px 10px 0px;">
                                                                                            Votes :
                                                                                            <asp:Label ID="lblVoters" Text='<%# Eval("Votes") %>' runat="server"></asp:Label>
                                                                                            &nbsp;&nbsp;&nbsp; Created On :
                                                                                            <asp:Label ID="lblDate" Text='<%# Eval("dtAddedOn") %>' runat="server"></asp:Label>
                                                                                            &nbsp;&nbsp;&nbsp; End's On :
                                                                                            <asp:Label ID="lblExpiredt" Text='<%# Eval("dtPollExpireDate") %>' runat="server"></asp:Label>
                                                                                            <asp:Label ID="lblExpireTime" Text='<%# Eval("strPollExpireTime") %>' runat="server"></asp:Label>
                                                                                        </p>
                                                                                        <div class="cls">
                                                                                        </div>
                                                                                        <div style="margin: 0px 15px 55px 10px;">
                                                                                            <asp:LinkButton ID="lnkEdit1" Font-Underline="false" Visible="false" class="edit"
                                                                                                ClientIDMode="Static" ToolTip="Edit" Text="Edit" CommandName="Edit Poll" CausesValidation="false"
                                                                                                runat="server">
                                                                                            </asp:LinkButton>
                                                                                            <asp:LinkButton ID="lnkDelete1" Font-Underline="false" Visible="false" class="edit"
                                                                                                ClientIDMode="Static" ToolTip="Delete" Text="Delete" CommandName="DeletePoll"
                                                                                                CausesValidation="false" runat="server">
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:AsyncPostBackTrigger ControlID="lnkDelete1" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                                <div id="divHorizontal" runat="server" style="text-align: right; margin-left: 9px;">
                                                                                    <hr align="right" width="815px" />
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <asp:Panel ID="pnlRadio" runat="server" Visible="false">
                                                                            <tr>
                                                                                <td colspan="2" valign="top">
                                                                                    <table width="50%">
                                                                                        <tr id="tr1" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:RadioButton ID="rdbQ1" runat="server" Text='<%#Eval("stroption1") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption1") %>'
                                                                                                    GroupName="grppoll" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr2" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:RadioButton ID="rdbQ2" runat="server" Text='<%#Eval("stroption2") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption2") %>'
                                                                                                    GroupName="grppoll"></asp:RadioButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr3" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:RadioButton ID="rdbQ3" runat="server" Text='<%#Eval("stroption3") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption3") %>'
                                                                                                    GroupName="grppoll"></asp:RadioButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr4" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:RadioButton ID="rdbQ4" runat="server" Text='<%#Eval("stroption4") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption4") %>'
                                                                                                    GroupName="grppoll"></asp:RadioButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr5" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:RadioButton ID="rdbQ5" runat="server" Text='<%#Eval("stroption5") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption5") %>'
                                                                                                    GroupName="grppoll"></asp:RadioButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr6" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:RadioButton ID="rdbQ6" runat="server" Text='<%#Eval("stroption6") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption6") %>'
                                                                                                    GroupName="grppoll"></asp:RadioButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr7" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:RadioButton ID="rdbQ7" runat="server" Text='<%#Eval("stroption7") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption7") %>'
                                                                                                    GroupName="grppoll"></asp:RadioButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr8" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:RadioButton ID="rdbQ8" runat="server" Text='<%#Eval("stroption8") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption8") %>'
                                                                                                    GroupName="grppoll"></asp:RadioButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr9" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:RadioButton ID="rdbQ9" runat="server" Text='<%#Eval("stroption9") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption9") %>'
                                                                                                    GroupName="grppoll"></asp:RadioButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr10" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:RadioButton ID="rdbQ10" runat="server" Text='<%#Eval("stroption10") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption10") %>'
                                                                                                    GroupName="grppoll"></asp:RadioButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlCheckBox" runat="server" Visible="false">
                                                                            <tr>
                                                                                <td colspan="2" valign="top">
                                                                                    <table width="50%">
                                                                                        <tr id="tr11" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:CheckBox ID="chkQ1" runat="server" Text='<%#Eval("stroption1") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption1") %>'
                                                                                                    GroupName="grppoll" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr12" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:CheckBox ID="chkQ2" runat="server" Text='<%#Eval("stroption2") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption2") %>'
                                                                                                    GroupName="grppoll"></asp:CheckBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr13" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:CheckBox ID="chkQ3" runat="server" Text='<%#Eval("stroption3") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption3") %>'
                                                                                                    GroupName="grppoll"></asp:CheckBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr14" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:CheckBox ID="chkQ4" runat="server" Text='<%#Eval("stroption4") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption4") %>'
                                                                                                    GroupName="grppoll"></asp:CheckBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr15" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:CheckBox ID="chkQ5" runat="server" Text='<%#Eval("stroption5") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption5") %>'
                                                                                                    GroupName="grppoll"></asp:CheckBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr16" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:CheckBox ID="chkQ6" runat="server" Text='<%#Eval("stroption6") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption6") %>'
                                                                                                    GroupName="grppoll"></asp:CheckBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr17" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:CheckBox ID="chkQ7" runat="server" Text='<%#Eval("stroption7") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption7") %>'
                                                                                                    GroupName="grppoll"></asp:CheckBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr18" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:CheckBox ID="chkQ8" runat="server" Text='<%#Eval("stroption8") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption8") %>'
                                                                                                    GroupName="grppoll"></asp:CheckBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr19" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:CheckBox ID="chkQ9" runat="server" Text='<%#Eval("stroption9") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption9") %>'
                                                                                                    GroupName="grppoll"></asp:CheckBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="tr20" runat="server">
                                                                                            <td colspan="2">
                                                                                                &nbsp;&nbsp;<asp:CheckBox ID="chkQ10" runat="server" Text='<%#Eval("stroption10") %>'
                                                                                                    CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption10") %>'
                                                                                                    GroupName="grppoll"></asp:CheckBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </asp:Panel>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <div style="position: absolute; width: 46%; text-align: right; margin-left: 445px;
                                                margin-top: -1%; display: none;" clientidmode="Static" id="divBarChart" runat="server">
                                                <asp:Panel ID="pnlBarChart" runat="server" Visible="false" Width="100%">
                                                    <asp:Chart ID="Chart1" runat="server" CssClass="Chartcss" Height="265px" Width="450px">
                                                        <Titles>
                                                            <asp:Title ShadowOffset="3" Name="Items" />
                                                        </Titles>
                                                        <Series>
                                                            <asp:Series Name="Default" BorderWidth="0" />
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1" BorderWidth="0" BackColor="Transparent" />
                                                        </ChartAreas>
                                                    </asp:Chart>
                                                </asp:Panel>
                                            </div>
                                            <div class="cls">
                                            </div>
                                            <div style="padding-left: 75px;">
                                                <asp:UpdatePanel ID="update1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkVote" Text="Vote" Font-Size="Medium" runat="server" CssClass="vote"
                                                            ClientIDMode="Static" OnClick="lnkVote_Click"></asp:LinkButton><br />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                            </div>
                                            <div class="cls">
                                                <br />
                                            </div>
                                            <div class="cls">
                                            </div>
                                            <span style="margin-left: 8%;">
                                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                            </span>
                                            <asp:HiddenField ID="divcommrnt" runat="server" ClientIDMode="Static" />
                                            <div id="dvShowComments" runat="server" style="margin-left: 25px; margin-top: 23%;"
                                                clientidmode="Static">
                                                <div style="width: 96%; border: #c6c8ca solid 1px; height: 46px;">
                                                    <textarea id="txtComment" clientidmode="Static" runat="server" placeholder="Comment" 
                                                        style="width: 85%; height: 18px; margin: 6px; border: none; color: #9c9c9c;"
                                                         validationgroup="PollComment" class="eventTitleField commentA"></textarea>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtComment" Display="Dynamic" ValidationGroup="PollComment"
                                                        ErrorMessage="Please enter comment" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:LinkButton CausesValidation="true" Style="float: right; margin-right: 15px;
                                                        margin-top: -40px;" ID="lnkPost" Font-Bold="true" Text="Post" runat="server"
                                                        Font-Italic="true" ValidationGroup="PollComment" OnClick="lnkPost_Click" CssClass="viewAllTxt"></asp:LinkButton>
                                                </div>
                                                <div class="cls">
                                                </div>
                                                <div class="cls">
                                                    <br />
                                                </div>
                                                <div class="cls">
                                                </div>
                                                <div style="margin-left: 0px;">
                                                    <asp:UpdatePanel ID="upcomment" runat="server">
                                                        <ContentTemplate>
                                                            <asp:HiddenField ID="hdnDeletePostQuestionID" Value="" ClientIDMode="Static" runat="server" />
                                                            <asp:HiddenField ID="hdnstrQuestionDescription" ClientIDMode="Static" runat="server" />
                                                            <asp:ListView ID="lstPollsComment" OnItemDataBound="lstPollsComment_ItemDataBound"
                                                                OnItemCommand="lstPollsComment_ItemCommand" runat="server">
                                                                <LayoutTemplate>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr id="itemPlaceHolder" runat="server">
                                                                        </tr>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:HiddenField ID="hdnRegistrationId" Value='<%# Eval("intRegistrationId") %>'
                                                                                runat="server" ClientIDMode="Static" />
                                                                            <asp:HiddenField ID="hdnintCommentId" Value='<%# Eval("intCommentId") %>' runat="server"
                                                                                ClientIDMode="Static" />
                                                                            <div style="border-bottom: 0px;">
                                                                                <div class="thumbnail">
                                                                                    <img id="imgprofile" runat="server" style="width: 46px; height: 52px; float: left"
                                                                                        src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' />
                                                                                    <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                                                                </div>
                                                                                <div style="width: 880px !important; margin-left: 60px; margin-top: 3px;">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td style="width: 805px;">
                                                                                                <p class="heading">
                                                                                                    <asp:LinkButton ID="lblPostlink" Style="color: #666666; font-size: 20px; margin: -5px 0 0;
                                                                                                        padding: 0 10px; width: 305px; text-decoration: none; padding-left: 2px" Text='<%# Eval("UserName") %>'
                                                                                                        CommandName="Details" runat="server"></asp:LinkButton></p>
                                                                                                <div style="font-size: small; text-align: right; margin-right: 8px;">
                                                                                                    <asp:Label ID="lblDate" Text='<%# Eval("dtAddedOn") %>' runat="server"></asp:Label></div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <p class="breakallwords" style="max-width: 795px;">
                                                                                                    <asp:Label ID="lblName" Text='<%# Eval("strComment") %>' runat="server" CssClass="comment more"
                                                                                                        Style="width: 807px; margin: 2px;"></asp:Label>
                                                                                                </p>
                                                                                                <asp:UpdatePanel ID="updele" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <div class="editDeleteMore" style="margin-top: 0px;">
                                                                                                            <asp:HiddenField ID="hdnintPostQuestionIdelet" Value='<%# Eval("intCommentId") %>'
                                                                                                                ClientIDMode="Static" runat="server" />
                                                                                                            <asp:HiddenField ID="lnkstrQuestionDescription" runat="server" Value='<%#Eval("strComment") %>'
                                                                                                                ClientIDMode="Static"></asp:HiddenField>
                                                                                                                <span class="spEditForum">
                                                                                                            <asp:LinkButton ID="lnkEdits" Font-Underline="false" Visible="false" class="edit"
                                                                                                                ClientIDMode="Static" ToolTip="Edit" Text="Edit" CommandName="Edit Poll" CausesValidation="false"
                                                                                                                runat="server">
                                                                                                            </asp:LinkButton>
                                                                                                            </span>
                                                                                                         <span class="spDeleteForum">
                                                                                                            <asp:LinkButton ID="lnkDeletes" Font-Underline="false" Visible="false" class="edit"
                                                                                                                ClientIDMode="Static" OnClientClick="javascript:docdelete();" ToolTip="Delete"
                                                                                                                Text="Delete" CommandName="DeletePoll" CausesValidation="false" runat="server">
                                                                                                            </asp:LinkButton>
                                                                                                            </span>
                                                                                                        </div>
                                                                                                    </ContentTemplate>
                                                                                                    <Triggers>
                                                                                                        <asp:AsyncPostBackTrigger ControlID="lnkDeletes" />
                                                                                                        <asp:AsyncPostBackTrigger ControlID="lnkEdits" />
                                                                                                    </Triggers>
                                                                                                </asp:UpdatePanel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <td>
                                                                                            <div class="bgLine" id="Div1" style="width: 101%;">
                                                                                                &nbsp;</div>
                                                                                        </td>
                                                                    </tr>
                                                                    </table> </div> </div> </td> </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="">
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
                                            </div>
                                            <div class="cls">
                                                <br />
                                            </div>
                                            <div class="cls">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!--left box ends-->
                    </div>
                    <!--left verticle search list ends-->
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtQuestion').keypress(function () {
                $('#txtQuestion').css('font-weight', 'normal');
            });

            $('#txtDescription').keypress(function () {
                $('#txtDescription').css('font-weight', 'normal');
            });

            if ($('#divcommrnt').val() == '1') {
                $('#dvShowComments').css("margin-top", "1%");
            }
            $('#lnkDelete1').click(function (e) {
                $(this).css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $('#lnkEdit1').click(function (e) {
                $(this).css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $("span.spEditForum").click(function () {
                $(this).children('#lnkEdits').css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $("span.spDeleteForum").click(function () {
                $(this).children('#lnkDeletes').css("box-shadow", "0px 0px 5px #00B7E5");
                setTimeout(
                function () {
                    $(this).children('#lnkDeletes').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#txtQuestion').keypress(function () {
                    $('#txtQuestion').css('font-weight', 'normal');
                });

                $('#txtDescription').keypress(function () {
                    $('#txtDescription').css('font-weight', 'normal');
                });
                if ($('#divcommrnt').val() == '1') {
                    $('#dvShowComments').css("margin-top", "1%");
                }
                $('#lnkDelete1').click(function (e) {
                    $(this).css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $('#lnkEdit1').click(function (e) {
                    $(this).css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $("span.spEditForum").click(function () {
                    $(this).children('#lnkEdits').css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $("span.spDeleteForum").click(function () {
                    $(this).children('#lnkDeletes').css("box-shadow", "0px 0px 5px #00B7E5");
                    setTimeout(
                function () {
                    $(this).children('#lnkDeletes').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
                });
            });
        });
    </script>
    <script type="text/javascript">
            $(document).ready(function () {
                var showChar = 100;
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
            });

            $(document).ready(function () {
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                    var showChar = 100;
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
                });
            });

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#lnkVote").click(function () {
                $("#divBarChart").slideDown();
                $("#divBarChart").slideDown();
            });
            $("div.editDeleteMore").click(function () {
                $('#hdnDeletePostQuestionID').val($(this).children('#hdnintPostQuestionIdelet').val());
                $('#hdnstrQuestionDescription').val($(this).children('#lnkstrQuestionDescription').val());
            });
            $('#lnkDeleteConfirm').click(function (e) {
                $(this).css("box-shadow", "0px 0px 5px #00B7E5");
            });
        });

        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("#lnkVote").click(function () {
                    $("#divBarChart").slideDown();
                    $("#divBarChart").slideDown();
                });

                $("div.editDeleteMore").click(function () {
                    $('#hdnDeletePostQuestionID').val($(this).children('#hdnintPostQuestionIdelet').val());
                    $('#hdnstrQuestionDescription').val($(this).children('#lnkstrQuestionDescription').val());
                });
                $('#lnkDeleteConfirm').click(function (e) {
                    $(this).css("box-shadow", "0px 0px 5px #00B7E5");
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
</asp:Content>
