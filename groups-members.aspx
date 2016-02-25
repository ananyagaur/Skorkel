<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="groups-members.aspx.cs"
    Inherits="groups_members" %>

<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Src="~/UserControl/Share.ascx" TagName="ShareDetails" TagPrefix="Share" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("js/jquery.jcarousel.min.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upmain" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="cls">
                </div>
                <div class="innerDocumentContainerGroup" style="margin-top: 0px;">
                    <div class="innerContainer">
                        <!--groups top box starts-->
                        <asp:UpdatePanel ID="updatepl" runat="server">
                            <ContentTemplate>
                                <Group:GroupDetails ID="grpDetails" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!--groups top box ends-->
                        <!--left box starts-->
                        <div class="innerGroupBoxnew">
                            <div class="innerContainerLeft" style="width: 900px; padding-left: 20px;">
                                <div class="tagContainer" style="width: 900px">
                                    <div class="forumsTabsMember">
                                        <ul>
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
                                            <li id="DivMemberTab" runat="server" clientidmode="Static">
                                                <div>
                                                    <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                                        class="forumstabAcitve" OnClick="lnkEventMemberTab_Click"></asp:LinkButton></div>
                                            </li>
                                        </ul>
                                        <div class="cls">
                                        </div>
                                    </div>
                                </div>
                                <div class="divmiddlecontentgrup" style="width: 938px;">
                                    <div style="margin-top: 12px;">
                                        <asp:TextBox ID="txtsearch" AutoPostBack="true" runat="server" ClientIDMode="Static"
                                            class="searchMembers" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                                        <br />
                                        <ajax:TextBoxWatermarkExtender TargetControlID="txtsearch" ID="txtwatermarkz" runat="server"
                                            WatermarkText="Search Members">
                                        </ajax:TextBoxWatermarkExtender>
                                        <p class="numberMembers" style="margin-right: 12px; margin-top: -3%;">
                                            <asp:Label ID="lblTotalMember" runat="server"></asp:Label>
                                            Members</p>
                                        <div class="cls">
                                        </div>
                                    </div>
                                    <div class="createForumBox members" style="margin-top: 1%;">
                                        <div class="cls">
                                            <br />
                                        </div>
                                        <div class="cls">
                                        </div>
                                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        <br />
                                        <div id="dvPopup" runat="server" style="border: 20px solid rgba(0,0,0,0.5); float: left;
                                            width: 500px; position: fixed; margin: -295px 0 0 11%; z-index: 100; display: none"
                                            clientidmode="Static">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                                <tr>
                                                    <td>
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td class="popHeading">
                                                                    <span style="color: #9c9c9c;">Message</span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="popBgLineGray">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p>
                                                            <asp:TextBox ID="txtSubject" CssClass="MessageSubjectGroupMem" placeholder="Subject"
                                                                runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubject"
                                                                Style="margin-left: 50px;" ErrorMessage="Please enter subject." ValidationGroup="Mess"></asp:RequiredFieldValidator>
                                                        </p>
                                                        <p>
                                                            <textarea id="txtBody" runat="server" placeholder="Message" rows="0" cols="0" class="MessageBodyGroupMem"></textarea>
                                                        </p>
                                                        <div style="padding-left: 50px; margin-top: -13px;">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Style="font-size: 15px;"
                                                                ControlToValidate="txtBody" Display="Dynamic" ValidationGroup="Mess" ErrorMessage="Please enter message"
                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="popBgLineGray">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="padding-right: 40px; padding-bottom: 5px;">
                                                        <table width="100" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Send"
                                                                        Style="width: 83px;" ValidationGroup="Mess" CssClass="joinBtn" OnClick="lnkPopupOK_Click"></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="btnCancel" CommandName="Join" CausesValidation="false" Style="float: right;
                                                                        text-align: center; text-decoration: none; width: 82px; padding-top: 7px; color: #000;"
                                                                        runat="server" Text="Cancel" OnClick="btnCancel_Click"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="divSuccessMess" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                                            float: left; width: 500px; padding-top: 0px; position: fixed; margin: -210px 0 0 11%;
                                            z-index: 100; display: none;" clientidmode="Static">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                                <tr>
                                                    <td>
                                                        <strong>&nbsp;&nbsp;
                                                            <asp:Label ID="lblSuccess" runat="server" Text="" Font-Size="Small"></asp:Label>
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
                                                                    <a clientidmode="Static" style="float: left; text-align: center; text-decoration: none;
                                                                        width: 82px; padding-top: 1px; color: #000; cursor: pointer" onclick="javascript:Cancel();">
                                                                        Close </a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="divPopupMember" clientidmode="Static" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                                            float: left; width: 500px; position: fixed; margin: -19% 0 0 12%; z-index: 100;
                                            display: none;">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                                <tr>
                                                    <td class="popHeading">
                                                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
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
                                                    <td class="popBgLineGray">
                                                    </td>
                                                </tr>
                                                <tr id="tdddlRoles" runat="server" style="display: none;">
                                                    <td>
                                                        &nbsp;&nbsp;<asp:DropDownList ID="ddlRoleDetails" CssClass="txtFieldNew" ClientIDMode="Static"
                                                            Style="width: 170px;" runat="server">
                                                        </asp:DropDownList>
                                                        <br />
                                                        &nbsp;&nbsp;
                                                        <asp:RequiredFieldValidator ID="reqRole" runat="server" InitialValue="Select Role"
                                                            ControlToValidate="ddlRoleDetails" ErrorMessage="Select role from the list. "
                                                            ValidationGroup="Insert" Text="Select role from the list." Font-Size="Small"></asp:RequiredFieldValidator>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <strong>&nbsp;&nbsp;
                                                            <asp:Label ID="lblMessageacc" runat="server" Font-Size="Small"></asp:Label>
                                                            <textarea id="txtBodyacc" runat="server" cols="20" rows="2" value="Message" onblur="if(this.value=='') this.value='Message';"
                                                                onfocus="if(this.value=='Message') this.value='';" class="forumTitle" style="width: 400px;
                                                                font-size: small; margin-left: 21px; margin-right: 50px; margin-top: 5px"></textarea>
                                                        </strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table width="100" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" ClientIDMode="Static" Text="Yes" OnClientClick="javascript:CallYes();"
                                                                        CssClass="joinBtn" ValidationGroup="Insert" OnClick="lnkPopupacc_Click"></asp:LinkButton>
                                                                </td>
                                                                <td style="padding-right: 20px;">
                                                                    <asp:LinkButton ID="lnkcose" runat="server" CommandName="Join" CausesValidation="false"
                                                                        Style="float: left; text-align: center; text-decoration: none; width: 82px; color: #000;
                                                                        padding-bottom: 11px;" ClientIDMode="Static" OnClick="lnkcose_Click">Close </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="divSuccessAcceptMember" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                                            float: left; width: 500px; padding-top: 0px; position: fixed; margin: -200px 0 0 11%;
                                            z-index: 100; display: none;" clientidmode="Static">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                                <tr>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="lblSuccessacc" runat="server" Font-Size="Small"></asp:Label>
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
                                                                    <asp:LinkButton ID="lnkSuccessFailure" runat="server" Text="Ok" ClientIDMode="Static"
                                                                        CausesValidation="false" Style="float: left; text-align: center; text-decoration: none;
                                                                        width: 82px; padding-top: 5px; color: #000;" OnClick="lnkSuccessFailure_Click"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <script type="text/javascript">
                                                function JobMesClose() {
                                                    document.getElementById("divPopupMember").style.display = 'none';
                                                }
                                            </script>
                                        </div>
                                        <div style="margin-top: -3%; margin-left: 1%;">
                                            <asp:ListView ID="lstPostUpdates" runat="server" OnItemCommand="lstPostUpdates_ItemCommand"
                                                OnItemDataBound="lstPostUpdates_ItemDataBound">
                                                <LayoutTemplate>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr id="itemPlaceHolder" runat="server">
                                                        </tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="tbl" runat="server">
                                                        <td>
                                                            <div class="searchListMember" style="width: 905px; margin-top: 15px;">
                                                                <div class="thumbnail" style="width: 45px;">
                                                                    <img id="imgprofile" runat="server" style="width: 46px; height: 52px; float: left"
                                                                        alt="" src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' />
                                                                    <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                                                </div>
                                                                <div class="searchListTxt" style="width: 500px !important;">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <p class="heading">
                                                                                    <asp:LinkButton ID="lblPostlink" Style="color: #666666; font-size: 20px; margin: -5px 0 0;
                                                                                        padding: 0 10px; width: 305px; text-decoration: none; padding-left: 2px" Text='<%# Eval("Name") %>'
                                                                                        CommandName="Details" runat="server"></asp:LinkButton></p>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <p style="color: #9c9c9c; font-size: 16px;">
                                                                                    <asp:Label ID="lblName" Text='<%# Eval("vchrInstituteName") %>' runat="server"></asp:Label>
                                                                                    <asp:HiddenField ID="hdnId" Value='<%# Eval("Id") %>' runat="server" />
                                                                                    <asp:HiddenField ID="hdnRegistrationId" Value='<%#Eval("intRegistrationId") %>' runat="server" />
                                                                                </p>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <p class="label">
                                                                                    <asp:Label ID="lblCityState" Text='<%# Eval("CityState") %>' runat="server"></asp:Label>
                                                                                </p>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="trRecommend" visible="false" runat="server">
                                                                            <td align="right" valign="top">
                                                                                <fieldset>
                                                                                    <table style="width: 240px; height: auto;">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td id="Footers" align="right" colspan="3">
                                                                                                    <asp:LinkButton ClientIDMode="Static" CommandName="Close" CssClass="linkClass" ID="lnkClose"
                                                                                                        runat="server" Text="X"></asp:LinkButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 45px; text-align: left">
                                                                                                    Recommend
                                                                                                </td>
                                                                                                <td style="width: 10px; text-align: center">
                                                                                                    :
                                                                                                </td>
                                                                                                <td style="width: 185px; text-align: left">
                                                                                                    <textarea id="txtRecDetails" class="EmpTextAreaCss" clientidmode="Static" runat="server"
                                                                                                        cols="28" rows="3"></textarea>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="3">
                                                                                                    <asp:RequiredFieldValidator ID="req" runat="server" ControlToValidate="txtRecDetails"
                                                                                                        Display="Dynamic" ErrorMessage="Please enter text" ValidationGroup="btn"></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td id="Footer" colspan="3" align="right">
                                                                                                    <asp:LinkButton ID="btnSubmitRecmnd" CommandName="SubmitRecom" CausesValidation="true"
                                                                                                        ValidationGroup="btn" runat="server" Text="Post"></asp:LinkButton>
                                                                                                    &nbsp;
                                                                                                    <asp:LinkButton ID="btnCancelRecmnd" CommandName="CancelRecom" CausesValidation="false"
                                                                                                        runat="server" Text="Cancel"></asp:LinkButton>
                                                                                                    <label class="TextCss" style="display: none; color: Red;" id="lblErrorRemark">
                                                                                                        Please Enter text.
                                                                                                    </label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </fieldset>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <asp:UpdatePanel ID="upeditdel" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="groupNameTxt" style="float: right; width: 175px;">
                                                                            <span class="spcssMsg">
                                                                                <asp:LinkButton ID="btnRecmnd" Visible="false" CommandName="Recommendation" Style="margin-right: 0px;"
                                                                                    CausesValidation="false" runat="server" Text="Message" CssClass="cssMsg"></asp:LinkButton>
                                                                            </span><span class="spcssAcc">
                                                                                <asp:LinkButton ID="lnkAccept" Visible="false" CommandName="Accept" Style="margin-right: 0px;"
                                                                                    CausesValidation="false" runat="server" Text="Accept" CssClass="cssAcc"></asp:LinkButton>
                                                                            </span><span class="spcssRej">
                                                                                <asp:LinkButton ID="lnkRejected" Visible="false" CommandName="Reject" Style="margin-right: -3px;"
                                                                                    CausesValidation="false" runat="server" Text="Reject" CssClass="cssRej"></asp:LinkButton>
                                                                            </span><span class="spcssConn">
                                                                                <asp:LinkButton Visible="false" ID="lnkConnected" CausesValidation="false" Style="color: #40CF8F;
                                                                                    float: left; height: 25px; margin: 10px 45px 0; padding: 5px 0; text-align: center;
                                                                                    text-decoration: none; width: 82px; text-decoration: none; border: none;" runat="server"
                                                                                    Text="Connected" CssClass="cssConn"></asp:LinkButton></span>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="lnkAccept" />
                                                                        <asp:AsyncPostBackTrigger ControlID="lnkRejected" />
                                                                        <asp:AsyncPostBackTrigger ControlID="btnRecmnd" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="bgLine" style="width: 94%; margin-left: 55px; margin-top: 12px;" id="Div1">
                                                                &nbsp;</div>
                                                            <div style="display: none" class="jury">
                                                                <asp:Label ID="lblEmailId" Text='<%# Eval("vchrUserName") %>' runat="server"></asp:Label></div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <p id="pLoadMore" runat="server" align="center">
                                                <asp:ImageButton ID="imgLoadMore" runat="server" ClientIDMode="Static" ImageUrl="~/images/loadingIcon.gif"
                                                    OnClick="imgLoadMore_OnClick" />
                                            </p>
                                            <p align="center">
                                                <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server"
                                                    ClientIDMode="Static" ForeColor="Red" Visible="false"></asp:Label>
                                            </p>
                                            <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
                                            <div style="display: none" class="innerContainer">
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
                                                    <asp:LinkButton ID="lnkLast" runat="server" Style="background: none" OnClick="lnkLast_Click"><img src="images/spacer.gif" class="last" alt="" /></asp:LinkButton>
                                                    <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
                                            <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
                                            <asp:HiddenField ID="hdnOwnerID" ClientIDMode="Static" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="cls">
                                    <p>
                                        &nbsp;</p>
                                </div>
                            </div>
                            <Share:ShareDetails ID="ShareDetails" runat="server" />
                        </div>
                        <!--left box ends-->
                    </div>
                    <!--left verticle search list ends-->
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
            <asp:HiddenField ID="hdnLoader" runat="server" ClientIDMode="Static" />
            <script type="text/javascript">
                $(document).ready(function () {
                    var ID = "#" + $("#hdnLoader").val();
                    $(ID).focus();

                });
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkPopupOK" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkcose" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkSuccessFailure" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#lblNoMoreRslt").css("display", "none");
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
        function Cancel() {
            document.getElementById("divSuccessMess").style.display = 'none';
            return false;
        }
        function CommonMessage() {
            document.getElementById("dvPopup").style.display = 'none';
            return false;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("span.spcssMsg").click(function () {
                $(this).children('.cssMsg').css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $("span.spcssAcc").click(function () {
                $(this).children('.cssAcc').css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $("span.spcssRej").click(function () {
                $(this).children('.cssRej').css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $("span.spcssConn").click(function () {
                $(this).children('.cssConn').css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $('#ctl00_ContentPlaceHolder1_txtSubject').keypress(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("span.spcssMsg").click(function () {
                    $(this).children('.cssMsg').css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $("span.spcssAcc").click(function () {
                    $(this).children('.cssAcc').css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $("span.spcssRej").click(function () {
                    $(this).children('.cssRej').css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $("span.spcssConn").click(function () {
                    $(this).children('.cssConn').css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $('#ctl00_ContentPlaceHolder1_txtSubject').keypress(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
            });
        });
        function CallYes() {
            $('#LinkButton1').css("box-shadow", "0px 0px 5px #00B7E5");
        }
    </script>
</asp:Content>
