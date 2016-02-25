<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="create-forum.aspx.cs"
    Inherits="create_forum" %>

<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("js/jquery.custom-scrollbar.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="container" style="padding-top: 25px">
                <div class="cls">
                </div>
                <!--inner container ends-->
                <div class="innerDocumentContainerGroup">
                    <div class="innerContainer">
                        <!--groups top box starts-->
                        <asp:UpdatePanel ID="uppln" runat="server">
                            <ContentTemplate>
                                <Group:GroupDetails ID="grpDetails" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="innerGroupBoxnew" style="width: 960px">
                            <div class="innerContainerLeft" style="width: 900px">
                                <div class="tagContainer" style="width: 900px">
                                    <div class="forumsTabs" style="height: 80px; margin-top: 2px;">
                                        <ul>
                                            <li style="margin-right: 65px;">
                                                <asp:LinkButton ID="lnkProfile" runat="server" Text="Profile" ClientIDMode="Static"
                                                    OnClick="lnkProfile_Click"></asp:LinkButton></li>
                                            <li id="DivHome" runat="server" style="display: none; margin-right: 65px;">
                                                <div>
                                                    <asp:LinkButton ID="lnkHome" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li id="DivForumTab" runat="server" clientidmode="Static" style="margin-right: 65px;">
                                                <div>
                                                    <asp:LinkButton ID="tagfor" runat="server" Text="Forums" ClientIDMode="Static" class="forumstabAcitve"
                                                        OnClick="lnkForumTab_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li id="DivUploadTab" runat="server" clientidmode="Static" style="display: none;
                                                margin-right: 65px;">
                                                <div>
                                                    <asp:LinkButton ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static"
                                                        OnClick="lnkUploadTab_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li id="DivPollTab" runat="server" clientidmode="Static" style="display: none; margin-right: 65px;">
                                                <div>
                                                    <asp:LinkButton ID="lnkPollTab" runat="server" Text="Polls" ClientIDMode="Static"
                                                        OnClick="lnkPollTab_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li id="DivEventTab" runat="server" clientidmode="Static" style="display: none; margin-right: 65px;">
                                                <div>
                                                    <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                                                        OnClick="lnkEventTab_Click"></asp:LinkButton></div>
                                            </li>
                                            <li id="DivMemberTab" runat="server" clientidmode="Static" style="display: none;
                                                margin-right: 65px;">
                                                <div>
                                                    <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                                        OnClick="lnkEventMemberTab_Click"></asp:LinkButton></div>
                                            </li>
                                        </ul>
                                        <div class="cls">
                                        </div>
                                        <img src="images/recentBlogs.png" align="absmiddle" style="margin-right: 8px; margin-left: -7%;
                                            margin-top: -10px;" />
                                        <asp:LinkButton ID="lnkBack" runat="server" class="recentPoll" OnClick="lnkBack_Click"
                                            Style="margin-left: 3%; margin-top: -8px;" Text="Back"></asp:LinkButton>
                                        <p class="headingCreateNewForum" style="margin-top: -22px;">
                                            <asp:Label ID="lblcreatefrm" runat="server" Text="Create New Forum"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="cls">
                                    </div>
                                </div>
                            </div>
                            <div style="height: 99%; margin-left: 9px; margin-top: 66px; border: #e7e7e7 solid 1px;
                                width: 938px;">
                                <div class="cls">
                                    <br />
                                </div>
                                <br />
                                <div id="divForumSuccess" runat="server" class="divcreateForum" clientidmode="Static">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                        <tr>
                                            <td>
                                                <strong>&nbsp;&nbsp;
                                                    <asp:Label ID="lblSuccess" ForeColor="Green" runat="server" Text="Forum Created successfully."
                                                        Font-Size="Small"></asp:Label>
                                                    <asp:Label ID="lblEditSuccess" runat="server" ForeColor="Green" Text="Forum Updated successfully."
                                                        Font-Size="Small"></asp:Label>
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
                                                            <asp:LinkButton ID="lnkBacks" runat="server" Text="Close" ClientIDMode="Static" CausesValidation="false"
                                                                Style="float: left; text-align: center; text-decoration: none; width: 82px; padding-top: 5px;
                                                                color: #000;" OnClick="lnkBacks_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div align="left">
                                    <b>
                                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></b>
                                </div>
                                <div style="margin-top: 30px;">
                                </div>
                                <div class="createForumBox" style="margin-left: 18px;">
                                    <div>
                                        <asp:TextBox ID="txtTitle" class="entryForumsTitle" placeholder="Title" ClientIDMode="Static"
                                            runat="server" Style="font-size: 23px; color: #D7D7D7;"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                                            Display="Dynamic" ValidationGroup="t" ErrorMessage="Please enter Title" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                    <textarea rows="10" class="entryForumstArea" id="CKDescription" runat="server" placeholder="Description"
                                        clientidmode="Static" style="margin-top: 20px; color: #D7D7D7; font-size: 18px;"></textarea>
                                    <div class="checkBox" style="padding-left: 0px; display: none;">
                                        <asp:UpdatePanel ID="upd" UpdateMode="Conditional" ClientIDMode="Static" runat="server">
                                            <ContentTemplate>
                                                <asp:CheckBox ID="chkPrivateForm" Text="Private Forum" ClientIDMode="Static" runat="server"
                                                    Onclick="ShowpnlNotify();" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <p>
                                        &nbsp;</p>
                                    <asp:LinkButton ID="btnSave" class="vote" runat="server" Font-Bold="true" Text="Create Forum"
                                        ValidationGroup="t" OnClick="btnSave_Click" OnClientClick="javascript:CallCreateForum();"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:LinkButton ID="lnkUpdate" class="vote" runat="server" Text="Update Forum" ValidationGroup="t"
                                        OnClick="lnkUpdate_Click" OnClientClick="javascript:CallCreateForum();"></asp:LinkButton>
                                    <asp:LinkButton ID="Close" Style="float: left; text-align: center; display: none;
                                        text-decoration: none; width: 82px; padding-top: 14px; color: #000;" runat="server"
                                        Text="Cancel" OnClick="btnClose_Click"></asp:LinkButton>
                                        <div style="display:none;">
                                         <asp:Button ID="btnSave1" runat="server" Font-Bold="true" Text="Create Forum" ClientIDMode="Static"
                                        ValidationGroup="t" OnClick="btnSave_Click" OnClientClick="javascript:CallCreateForum();" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="lnkUpdate1" runat="server" Text="Update Forum" ValidationGroup="t" ClientIDMode="Static"
                                        OnClick="lnkUpdate_Click" OnClientClick="javascript:CallCreateForum();" />
                                        </div>
                                </div>
                                <div class="cls">
                                </div>
                                <p align="center">
                                </p>
                            </div>
                            <div class="cls">
                                <p>
                                    &nbsp;</p>
                            </div>
                        </div>
                        <!--left box ends-->
                    </div>
                    <!--left verticle search list ends-->
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="lnkUpdate" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtInvitee_chosen').css("width", "420px");
            $('#Close').click(function () {
                $("#txtTitle").val('Title');
                $("#CKDescription").val('Description');
            });
        });

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
        $(".chosen-container").css("width", "670px");
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
            $(".chosen-container").css("width", "670px");
        });
    </script>
    <script type="text/javascript">
        function AlertMsg(ctl) {
            if (ctl == "1") {
                alert('Forum Updated Successfully.');
                $("#btnGo").click();
            }
            else {
                alert('New forum created successfully.');
                $("#btnGo").click();
            }
        }
    </script>
    <script type="text/javascript">
        function MesForumClose() {
            document.getElementById("divForumSuccess").style.display = 'none';
        }
        function CallCreateForum() {
            $('#ctl00_ContentPlaceHolder1_btnSave').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtTitle').text() == '') {
                setTimeout(
                function () {
                    $('#ctl00_ContentPlaceHolder1_btnSave').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
        }
        $(document).ready(function () {
            $('#txtTitle').keypress(function (e) {
                if (e.keyCode == 13) {
                    $('#btnSave1').click();
                        return false;
                }
            });
        });
    </script>
    <asp:HiddenField ID="hdnForumID" runat="server" ClientIDMode="Static" />
    <div style="display: none">
        <asp:Button ID="btnGo" runat="server" ClientIDMode="Static" OnClick="btnGo_Click" />
    </div>
</asp:Content>
