<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="SearchGroup.aspx.cs" Inherits="SearchGroup" %>

<%@ Register Src="~/UserControl/AcceptMember.ascx" TagName="AcceptMember" TagPrefix="Accept" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upgroupsearch" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnTempUserId" runat="server" Value="" ClientIDMode="Static" />
            <!--heading ends-->
            <div class="cls">
            </div>
            <!--inner container starts-->
            <div id="divFollUnfollPopup" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                float: left; width: 500px; padding-top: 0px; position: relative; margin: 12% 0 0 17%;
                z-index: 100;" clientidmode="Static">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                    <tr>
                        <td class="popHeading">
                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="lblTitleGroup" Font-Size="Small" runat="server"></asp:Label>
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>&nbsp;&nbsp;
                                <asp:Label ID="Label1" runat="server" Text="" Font-Size="Small"></asp:Label>
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
                                            text-decoration: none; width: 82px; padding-top: 5px; color: #000;" onclick="Cancel();">
                                            Cancel </a>
                                    </td>
                                    <td style="padding-right: 20px;">
                                        <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Yes" CssClass="joinBtn"
                                            OnClick="lnkPopupOK_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divConnDisPopup" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                float: left; width: 500px; padding-top: 0px; position: fixed; margin: 11% 0 0 17%;
                z-index: 100;" clientidmode="Static">
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
                                        <asp:UpdatePanel ID="uplink" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="LinkButton1" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" ClientIDMode="Static" Text="Cancel"
                                                    Style="float: left; text-align: center; text-decoration: none; width: 82px; padding-top: 5px;
                                                    color: #000;" OnClick="lnkCancel_Click"></asp:LinkButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divSuccess" runat="server" style="border: 20px solid rgba(0,0,0,0.5); float: left;
                width: 500px; padding-top: 0px; position: fixed; margin: 12% 0 0 15%; z-index: 100;
                display: none;" clientidmode="Static">
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
                                        <a href="#" clientidmode="Static" causesvalidation="false" style="float: left; text-align: center;
                                            text-decoration: none; width: 82px; padding-top: 5px; color: #000;" onclick="javascript:Cancel();return false;">
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
            <!--inner container ends-->
            <div class="innerContainer" style="background: #fff; float: left">
                <!--left filter further starts-->
                <div class="leftFilterBox">
                    <p class="heading">
                        Filter further:</p>
                    <p class="groups">
                        Groups:</p>
                    <p class="subsubHeading">
                        Contexts</p>
                    <div class="searchBoxNew">
                        <ul>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:ListView ID="lstSerchSubjCategory" runat="server" OnItemCommand="lstSerchSubjCategory_ItemCommand"
                                        OnItemDataBound="lstSerchSubjCategory_ItemDataBound" GroupItemCount="2" GroupPlaceholderID="groupPlaceHolder1"
                                        ItemPlaceholderID="itemPlaceHolder1">
                                        <LayoutTemplate>
                                            <table width="20%">
                                                <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                            </table>
                                        </LayoutTemplate>
                                        <GroupTemplate>
                                            <tr>
                                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                            </tr>
                                        </GroupTemplate>
                                        <ItemTemplate>
                                            <li id="SubLi" runat="server" style="width: 230px;">
                                                <asp:HiddenField ID="hdnSubCatId" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                <asp:LinkButton ID="lnkCatName" Style="text-decoration: none;" Font-Underline="false"
                                                    runat="server" Text='<%#Eval("strCategoryName")%>' CommandName="Subject Category"></asp:LinkButton>
                                            </li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ul>
                    </div>
                    <div style="display: none;">
                        <asp:ListView ID="lstcity" runat="server">
                            <LayoutTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr id="itemPlaceHolder" runat="server">
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div style="padding-top: 10px;">
                                    <asp:HiddenField ID="hdnCityId" Value='<%# Eval("intCategoryId") %>' runat="server"
                                        ClientIDMode="Static" />
                                    <asp:CheckBox Style="width: 215px;" CssClass="checkboxFiveSearch" ID="ChkCity" Text='<%# String.Concat("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", Eval("strCategoryName"),"") %>'
                                        runat="server" AutoPostBack="true" OnCheckedChanged="Location_CheckedChange" />
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <p style="padding-top: 10px; display: none;" class="subsubHeading">
                        Contexts</p>
                    <div style="display: none;">
                        <asp:CheckBox Style="width: 215px;" CssClass="checkboxFiveSearch" ID="ChkCorporateLaw"
                            Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CorporateLaw" runat="server"
                            AutoPostBack="true" OnCheckedChanged="CorporateLaw_CheckedChange" />
                        <label for="ChkCorporateLaw">
                        </label>
                    </div>
                    <div style="display: none;">
                        <asp:CheckBox Style="width: 215px;" CssClass="checkboxFiveSearch" ID="ChkCriminalLaw"
                            Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CriminalLaw" runat="server" AutoPostBack="true"
                            OnCheckedChanged="CriminalLaw_CheckedChange" />
                        <label for="ChkCriminalLaw">
                        </label>
                    </div>
                    <div style="display: none;">
                        <asp:CheckBox Style="width: 215px;" CssClass="checkboxFiveSearch" ID="ChkFamilyLaw"
                            Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FamilyLaw" runat="server" AutoPostBack="true"
                            OnCheckedChanged="FamilyLaw_CheckedChange" />
                        <label for="ChkFamilyLaw">
                        </label>
                    </div>
                    <div class="bgLine">
                    </div>
                    <asp:LinkButton ID="imgReset" CssClass="reset" runat="server" OnClick="imgReset_Click"
                        Style="text-decoration: none;"></asp:LinkButton>
                </div>
                <!--left filter further ends-->
                <!--middle container starts-->
                <div class="middleContainer" style="width: 48%;">
                    <!--search result list starts-->
                    <div style="margin-left: 275px; margin-top: 15px;">
                        <asp:Label ID="lblMessage" runat="server" Text="No Group Found..!" ForeColor="Red"
                            Visible="false"></asp:Label>
                    </div>
                    <asp:ListView ID="lstGroup" runat="server" OnItemCommand="lstGroup_ItemCommand" OnItemDataBound="lstGroup_ItemDataBound">
                        <LayoutTemplate>
                            <table cellpadding="0" cellspacing="0">
                                <tr id="itemPlaceHolder" runat="server">
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr id="Grid" runat="server">
                                <td>
                                    <div class="searchListNew" style="width: 650px;">
                                        <div class="thumbnail">
                                            <img id="imgprofile" runat="server" style="width: 58px; height: 68px; float: left"
                                                src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' />
                                        </div>
                                        <div class="searchListTxt" style="width: 510px;">
                                            <table width="450px">
                                                <tr>
                                                    <td>
                                                        <p class="wordbreaksg heading" style="width: 450px;">
                                                            <asp:LinkButton ID="lblPostlink" Text='<%# Eval("strPostDescription") %>' CommandName="Details"
                                                                CssClass="searchGrouplinnk" runat="server"></asp:LinkButton>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p>
                                                            <div class="wordbreakserchGroup">
                                                                <asp:Label ID="lblSummary" Text='<%# Eval("strSummary") %>' runat="server"></asp:Label>
                                                            </div>
                                                            <asp:HiddenField ID="hdnId" Value='<%# Eval("Id") %>' runat="server" />
                                                            <asp:HiddenField ID="hdnRegistrationId" Value='<%#Eval("intRegistrationId") %>' runat="server" />
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p>
                                                            •
                                                            <asp:Label ID="lblGroupMember" Text='<%# Eval("GroupMembers") %>' runat="server"></asp:Label>
                                                            members</p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="buttons">
                                            <asp:LinkButton ID="btnjoinreq" CssClass="join Joincss" CommandName="JoinGroup" CausesValidation="false"
                                                runat="server" Text="Join" Style="background: url(images/connect-btn.png) repeat scroll 0 0 transparent;
                                                color: #FFFFFF; float: left; height: 22px; margin: 15px 0 0; padding: 5px 0;
                                                text-align: center; text-decoration: none !important; width: 82px; text-decoration: none;"></asp:LinkButton>
                                            <br />
                                            <asp:LinkButton ID="btnDelete" Font-Underline="false" Font-Size="Small" Visible="false" 
                                                ClientIDMode="Static" OnClientClick="javascript:return confirm('Do you want to delete?');"
                                                ToolTip="Delete" Text="Delete" Style="float: right; color: #6D6E71; margin-top: 35px; text-decoration: none !important;"
                                                CommandName="DeleteGroup" CausesValidation="false" runat="server">
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div style="display: none" class="jury">
                                        <asp:Label ID="lblEmailId" Text='<%# Eval("vchrUserName") %>' runat="server"></asp:Label>
                                        <asp:Label ID="lblUserName" Text='<%# Eval("UserName") %>' runat="server"></asp:Label>
                                        <asp:Label ID="lblGrpsAccess" Text='<%# Eval("strAccess") %>' runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                    <div class="cls">
                    </div>
                    <div class="cls">
                    </div>
                    <div id="dvPage" runat="server" class="pagination" style="margin: -4px -250px 0 0;"
                        clientidmode="Static">
                        <asp:LinkButton ID="lnkFirst" runat="server" CssClass="PagingFirst" OnClick="lnkFirst_Click"> </asp:LinkButton>
                        <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click">&lt;&lt;</asp:LinkButton>
                        <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand"
                            OnItemDataBound="rptDvPage_ItemDataBound">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnItemCount" runat="server" Value='<%#Eval("intPageNo") %>'
                                    ClientIDMode="Static" />
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
                        <asp:HiddenField ID="hdnEndPage" runat="server" ClientIDMode="Static" />
                    </div>
                    <!--search result list ends-->
                </div>
                <!--middle container ends-->
                <div style="padding-top: 0px; text-align: center; display: none;">
                    <a title="Create Groups" href="Scrl_UserGroupDetailTbl.aspx" style="color: #00B6BD;
                        text-decoration: none; font-weight: bold;">Create Group</a>
                </div>
                <div class="adv" style="display: none;">
                    <!--right side banner starts-->
                    <img src="images/adv1.jpg" /><br />
                    <br />
                    <img src="images/adv2.jpg" />
                    <!--right side banner ends-->
                </div>
            </div>
            <!--pagination starts-->
            <div class="innerContainer">
                <div class="cls">
                </div>
            </div>
            <!--pagination ends-->
            <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
            <div class="cls">
            </div>
            <asp:UpdateProgress ID="updateProgress" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                        right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/Loadgif.gif"
                            AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; margin-top: 20%;"
                            class="divProgress" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkConnDisconn" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/jscript">
        function Cancel(e) {
            document.getElementById("divFollUnfollPopup").style.display = 'none';
            document.getElementById("divConnDisPopup").style.display = 'none';
            document.getElementById("divSuccess").style.display = 'none';
            return false;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.Joincss').click(function (e) {
                $(this).css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $('#lnkConnDisconn').click(function (e) {
                $(this).css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $('#divConnDisPopup').center();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.Joincss').click(function (e) {
                    $(this).css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $('#lnkConnDisconn').click(function (e) {
                    $(this).css("box-shadow", "0px 0px 5px #00B7E5");
                });
            });
        });
    </script>
</asp:Content>
