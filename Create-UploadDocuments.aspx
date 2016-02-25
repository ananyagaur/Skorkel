<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="Create-UploadDocuments.aspx.cs"
    Inherits="Create_UploadDocuments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("js/jquery.filedrop.js")%>" type="text/javascript"></script>
     <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container" style="padding-top: 25px">
        <div class="innerDocumentContainerGroup">
            <div class="innerContainer">
                <!--groups top box starts-->
                <Group:GroupDetails ID="grpDetails" runat="server" />
                <!--groups top box ends-->
                <!--left box starts-->
                <div class="innerGroupBoxnew">
                    <div class="innerContainerLeft" style="width: 900px">
                        <div class="tagContainer" style="width: 900px">
                            <div class="forumsTabs" style="height: 80px; margin-top: -2px;">
                                <ul>
                                    <li>
                                        <asp:LinkButton ID="lnkProfile" runat="server" Text="Profile" ClientIDMode="Static"
                                            OnClick="lnkProfile_Click"></asp:LinkButton></li>
                                    <li id="DivHome" runat="server" style="display: block;">
                                        <div>
                                            <asp:LinkButton ID="lnkHome" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                                        </div>
                                    </li>
                                    <li id="DivForumTab" runat="server" clientidmode="Static" style="display: block;">
                                        <div>
                                            <asp:LinkButton ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static"
                                                OnClick="lnkForumTab_Click"></asp:LinkButton></div>
                                    </li>
                                    <li id="DivUploadTab" runat="server" clientidmode="Static">
                                        <div>
                                            <asp:LinkButton ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static"
                                                class="forumstabAcitve" OnClick="lnkUploadTab_Click"></asp:LinkButton>
                                        </div>
                                    </li>
                                    <li id="DivPollTab" runat="server" clientidmode="Static" style="display: block;">
                                        <div>
                                            <asp:LinkButton ID="lnkPollTab" runat="server" Text="Polls" ClientIDMode="Static"
                                                OnClick="lnkPollTab_Click"></asp:LinkButton>
                                        </div>
                                    </li>
                                    <li id="DivEventTab" runat="server" clientidmode="Static" style="display: block;">
                                        <div>
                                            <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                                                OnClick="lnkEventTab_Click"></asp:LinkButton></div>
                                    </li>
                                    <li id="DivMemberTab" runat="server" clientidmode="Static" style="display: block;">
                                        <div>
                                            <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                                OnClick="lnkEventMemberTab_Click"></asp:LinkButton></div>
                                    </li>
                                </ul>
                                <div class="cls">
                                </div>
                                <img src="images/recentBlogs.png" align="absmiddle" style="margin-right: 8px; margin-left: -7%;
                                    margin-top: -10px;" />
                                <asp:LinkButton ID="lnkAllUpload" runat="server" class="recentPoll" OnClick="lnkAllUpload_Click" 
                                    Style="margin-top: -5px; width:30px;" ClientIDMode="Static" Text="Back"></asp:LinkButton>
                                    <asp:Label ID="lblm" runat="server" ClientIDMode="Static" style="margin: 0px 0px 0px 325px;  font-weight: bold;">Upload New File</asp:Label>
                                    <div class="cls">
                            </div>
                            </div>
                            <div class="cls">
                            </div>
                        </div>
                    </div>
                    <div style="height: 99%; margin-left: 8px; width: 938px; margin-top: 62px; border: #e7e7e7 solid 1px;">
                        <div class="cls">
                            <br />
                        </div>
                        <div class="cls">
                            <br />
                        </div>
                        <div id="divDocsSuccess" runat="server" class="divDocsucess" clientidmode="Static">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                <tr>
                                    <td>
                                        <strong>&nbsp;&nbsp;
                                            <asp:Label ID="lblSuccess" runat="server" Text="Document Uploaded Successfully..!"
                                                ForeColor="Green" Font-Size="Small"></asp:Label>
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
                                                    <a clientidmode="Static" causesvalidation="false" style="float: left; text-align: center;
                                                        text-decoration: none; width: 82px; padding-top: 5px; color: #000; cursor: pointer;"
                                                        onclick="javascript:MesCloseDocs();">Close </a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="center">
                            <br />
                            <div style="margin-top: 0px; margin-left: 10px;">
                                <asp:Label ID="lnkFirst" runat="server" ForeColor="#A7A8AA"></asp:Label><asp:Label
                                    ID="lblFirst" runat="server" Visible="false" Text="&nbsp;>>"></asp:Label>
                                <asp:Label ID="lnkSecond" runat="server" ForeColor="#A7A8AA"></asp:Label>
                                <asp:Label ID="lblSecond" runat="server" Visible="false" Text="&nbsp;>>"></asp:Label>
                                <asp:Label ID="lnkThird" runat="server" ForeColor="#A7A8AA"></asp:Label>
                                <asp:Label ID="lblThird" runat="server" Visible="false" Text="&nbsp;>>"></asp:Label>
                            </div>
                            <div class="fieldTxtUpload">
                                <div class="dropnDrag" id="dvDest">
                                    <p class="uploadDragDrop">
                                        Drag and Drop File<br />
                                        +</p>
                                    <p>
                                        <asp:Label ID="lblfilenamee" runat="server" ClientIDMode="Static">
                                        </asp:Label><asp:FileUpload ID="upload" runat="server" CssClass="uploadcss" />
                                        <asp:HiddenField ID="hdnUploadFile" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnOrgFileName" runat="server" ClientIDMode="Static" />
                                    </p>
                                    <br />
                                    <asp:Label ID="lblDocName" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lnkDelete" ClientIDMode="Static" Style="display: none; margin-left: 45%;
                                        color: #00B6BD;" runat="server" OnClientClick="javascript:return confirm('Do you want to delete?');"
                                        OnClick="lnkDelete_Click">Delete</asp:LinkButton>
                                </div>
                            </div>
                            <div class="cls">
                                &nbsp;
                            </div>
                            <div style="margin-left: 21px; margin-top: 2%; font-weight: bold;">
                                Title</div>
                            <div>
                                <asp:TextBox ID="txtDocTitle" runat="server" ClientIDMode="Static" CssClass="uploadDescription"
                                    Style="width: 50%;" placeholder="Title" MaxLength="50"></asp:TextBox>
                            </div>
                            <div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    CssClass="uploadDescription" Style="width: 1%; border: none;" ControlToValidate="txtDocTitle"
                                    Display="Dynamic" ValidationGroup="t" ErrorMessage="Please select Title&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                            </div>
                            <div style="margin-left: 21px; margin-top: 2%; font-weight: bold;">
                                Add Description</div>
                            <div>
                                <textarea id="txtAuthors" runat="server" class="uploadDescription" clientidmode="Static"  placeholder="Description" style="width: 470px;"></textarea>
                            </div>
                            <div style="margin-left: 21px; margin-top: 2%; font-weight: bold;">
                                Classify Document</div><br />
                            <div class="cls">
                            </div>
                            <div class="uploadRadio">
                                <asp:CheckBoxList ID="ddlDocumentType" runat="server" data-toggle="radio" RepeatDirection="Horizontal"
                                    Style="display: inline; display: none;">
                                </asp:CheckBoxList>
                                <asp:CheckBox ID="ChkCases" Text="&nbsp; Cases" GroupName="Who can Vote" ClientIDMode="Static"
                                    runat="server" />&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="ChkArticles" Text="&nbsp;Articles" GroupName="Who can Vote" ClientIDMode="Static"
                                    runat="server" />
                                <asp:CheckBox ID="ChkNotes" Text="&nbsp;Notes" GroupName="Who can Vote" ClientIDMode="Static"
                                    runat="server" />
                                <asp:CheckBox ID="ChkOthers" Text="&nbsp;Others" GroupName="Who can Vote" ClientIDMode="Static"
                                    runat="server" />
                            </div>
                            <div style="padding-left: 5px;" class="fieldTxtUpload">
                            </div>
                            <div class="cls">
                                &nbsp;
                            </div>
                            <!--category box starts-->
                            <div style="padding-top: 10px; width: 730px; float: left; margin-left: 2%; display: none;
                                padding-left: 5px; width: 893px" class="categoryBox">
                                <asp:UpdatePanel ID="UpdateSub" runat="server">
                                    <ContentTemplate>
                                        <div class="categoryTxt" style="width: 650px; float: left;">
                                            <ul>
                                                <asp:ListView ID="lstSubjCategory" runat="server" OnItemCommand="LstSubjCategory_ItemCommand"
                                                    OnItemDataBound="LstSubjCategory_ItemDataBound" GroupItemCount="4" GroupPlaceholderID="groupPlaceHolder1"
                                                    ItemPlaceholderID="itemPlaceHolder1">
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
                                                        <li id="SubLi" runat="server">
                                                            <asp:HiddenField ID="hdnSubCatId" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                            <asp:HiddenField ID="hdnCountSub" runat="server" Value='<%#Eval("CountSub")%>' />
                                                            <asp:LinkButton ID="lnkCatName" ForeColor="#646161" Style="text-decoration: none;"
                                                                Font-Underline="false" runat="server" Text='<%#Eval("strCategoryName")%>' CommandName="Subject Category"></asp:LinkButton>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </ul>
                                        </div>
                                        <div class="cls">
                                        </div>
                                        <div class="viewAll" style="margin-right: 28%;">
                                            <asp:LinkButton ID="lnkViewSubj" Text="View all" Font-Underline="false" runat="server"
                                                OnClick="lnkViewSubj_Click"></asp:LinkButton></div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="cls">
                                &nbsp;
                            </div>
                            <!--category box ends-->
                            <div class="cls">
                                &nbsp;
                            </div>
                            <div style="padding-top: 5px; padding-left: 5px; margin-left: 21px; margin-top: -2%;
                                font-weight: bold;" class="fieldTxtUpload">
                                Mark as
                            </div>
                            <div style="padding-top: 10px; padding-bottom: 5px; padding-left: 0px; margin-left: 2%;">
                                <asp:CheckBoxList ID="ddlintDocsSee" runat="server" RepeatDirection="Horizontal"
                                    Style="display: inline;">
                                    <asp:ListItem Text="Private Document" Value="Private"></asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                            <div class="cls" style="height: 10px">
                            </div>
                            <div class="bgLine" style="margin-bottom: 0px">
                                <img src="images/spacer.gif" height="2" width="730" /></div>
                            <div style="text-align: left" class="errorMsg" id="Div1">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </div>
                            <div style="padding-left: 360px; margin-top: 0px; width: 358px;" class="savebutton">
                                <asp:LinkButton ID="LinkButton1" CssClass="vote" runat="server" Text="Upload" ClientIDMode="Static"
                                    ValidationGroup="t" OnClick="btnSave_Click" OnClientClick="CallUploaddoc();" ></asp:LinkButton>
                                <asp:LinkButton ID="btnCancelExperience" CommandName="Join" CausesValidation="false"
                                    Style="float: left; text-align: center; text-decoration: none; width: 82px; margin-top: 14px;
                                    color: Black;" runat="server" Text="Cancel" OnClick="btnCancel_Click"></asp:LinkButton>
                            </div>
                            <div style="float: left; margin-top: -58px;">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Style="text-align: left"
                                    ValidationGroup="t" ForeColor="Red" Font-Names="Verdana" />
                            </div>
                        </div>
                        <div class="cls">
                        </div>
                        <br />
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
    <script type="text/javascript">
            $(document).ready(function () {
                $('#lnkShare').css("margin-top", "30px");
            });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".categoryTxt ul li").click(function () {
                $(this).toggleClass("gray");
            });
            $(".viewAll a").click(function () {
                $(this).parent().parent().toggleClass("categoryBoxToggle");
                var abc = this.text;
                if (abc == "Close") {
                    $(this).text("View all");
                }
                if (abc == "View all") {
                    $(this).text("Close");
                }
            });
        });
    </script>
    <script type="text/javascript">
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#lblNotifyCount").text() == '0') {
                document.getElementById("divNotification1").style.display = "none";
            } 
            if ($("#lblInboxCount").text() == '0') {
                document.getElementById("divInbox").style.display = "none";
            } 
            $("#uploadTrigger").click(function () {
                $("#uploadDoc").click();
            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {

                if ($("#lblNotifyCount").text() == '0') {
                    document.getElementById("divNotification1").style.display = "none";
                } 
                if ($("#lblInboxCount").text() == '0') {
                    document.getElementById("divInbox").style.display = "none";
                } 
                $("#uploadTrigger").click(function () {
                    $("#uploadDoc").click();
                });
            });
        });
    </script>
    <script type="text/javascript">
            $(document).ready(function () {
                var box;
                box = document.getElementById("dvDest");
                box.addEventListener("dragenter", OnDragEnter, false);
                box.addEventListener("dragover", OnDragOver, false);
                box.addEventListener("drop", OnDrop, false);
            });
            function OnDragEnter(e) {
                e.stopPropagation();
                e.preventDefault();
            }
            function OnDragOver(e) {
                e.stopPropagation();
                e.preventDefault();
            }
            function OnDrop(e) {
                e.stopPropagation();
                e.preventDefault();
                selectedFiles = e.dataTransfer.files;
                $('#lblfilenamee').show();
                $('#lblfilenamee').text(selectedFiles[0].name);
                $('#hdnOrgFileName').val(selectedFiles[0].name);
                $('#ctl00_ContentPlaceHolder1_upload').hide();
                var data = new FormData();
                for (var i = 0; i < selectedFiles.length; i++) {
                    data.append(selectedFiles[i].name, selectedFiles[i]);
                }
                $.ajax({
                    type: "POST",
                    url: "handler/UploadDocument.ashx",
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (result) {
                        $('#hdnUploadFile').val(result);
                        $('#lnkDelete').show();
                    },
                    error: function () {
                        alert("There was error uploading files!");
                    }
                });
            }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ChkCases').click(function () {
                $('#ChkArticles').prop('checked', false);
                $('#ChkNotes').prop('checked', false);
                $('#ChkOthers').prop('checked', false);
            });
            $('#ChkArticles').click(function () {
                $('#ChkCases').prop('checked', false);
                $('#ChkNotes').prop('checked', false);
                $('#ChkOthers').prop('checked', false);
            });
            $('#ChkNotes').click(function () {
                $('#ChkCases').prop('checked', false);
                $('#ChkArticles').prop('checked', false);
                $('#ChkOthers').prop('checked', false);
            });
            $('#ChkOthers').click(function () {
                $('#ChkCases').prop('checked', false);
                $('#ChkArticles').prop('checked', false);
                $('#ChkNotes').prop('checked', false);
            });
        });
    </script>
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
    <script type="text/javascript">
        function MesCloseDocs() {
            document.getElementById("divDocsSuccess").style.display = 'none';
        }
        function CallUploaddoc() {
            $('#LinkButton1').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtTitle').text() == '') {
                setTimeout(
                function () {
                    $('#LinkButton1').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
        }
        $(document).ready(function () {
            $('#txtDocTitle').keypress(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
        });
    </script>
</asp:Content>
