<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="post-new-question.aspx.cs" Inherits="post_new_question" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container" style="padding-top: 0px">
        <div class="innerDocumentContainerSpc">
            <div class="innerContainer">
                <div class="innerGroupBox">
                    <!--search box starts-->
                    <div class="innerContainerLeftMenu">
                        <div class="searchBoxNewQ">
                            <p class="relatedQ">
                                Related Questions</p>
                            <ul class="questsrelated">
                                <asp:ListView ID="lstRelQuestions" runat="server" OnItemCommand="lstRelQuestions_ItemCommand"
                                    GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">
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
                                        <li>
                                            <asp:HiddenField ID="hdnPostQuestionId" runat="server" Value='<%#Eval("intPostQuestionId")%>' />
                                            <p class="breakallwords commentquestionsrply">
                                                <asp:LinkButton ID="lnkQueAnsDesc" Font-Underline="false" runat="server" Text='<%#Eval("strQuestionDescription")%>'
                                                    CommandName="OpenQ"></asp:LinkButton>
                                            </p>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                            <a class="readmore new" href="AllQuestionDetails.aspx">Show More</a>
                        </div>
                    </div>
                    <!--search box ends-->
                    <!--left box starts-->
                    <asp:UpdatePanel ID="updates" runat="server">
                        <ContentTemplate>
                            <div class="innerContainerLeft">
                                <div class="subtitle">
                                    <div class="recentBlogs" style="width: 565px;">
                                        <a href="AllQuestionDetails.aspx" class="recentBlogTxt" style="font-size: 16px; padding-top: 9px">
                                            Back to Q&A</a>
                                    </div>
                                    <asp:LinkButton ID="LinkButton1" Text="Cancel" runat="server" ClientIDMode="Static"
                                        OnClientClick="javascript:callClose();" CssClass="cancel" OnClick="btnClose_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkcreateForum" Text="Post" runat="server" ClientIDMode="Static"
                                        OnClientClick="javascript:CallPostques();" ValidationGroup="Qstn" OnClick="btnSave_Click"
                                        class="postBtn"></asp:LinkButton>
                                    <div id="divSaveimage" style="display: none;" class="LoadingimgQA">
                                        <img id="imgSave" src="images/Loadgif.gif" />
                                    </div>
                                </div>
                                <div style="height: 10px; text-align: center;">
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </div>
                                <div class="cls">
                                    <br />
                                </div>
                                <div id="divSuccess" runat="server" style="border: 20px solid rgba(0,0,0,0.5); width: 39%;
                                    margin-left: 12%; position: fixed; margin-top: -3%; display: none;" clientidmode="Static">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                        <tr>
                                            <td>
                                                <strong>&nbsp;&nbsp;
                                                    <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" Text="Question Created successfully."
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
                                                            <a clientidmode="Static" causesvalidation="false" style="float: left; text-align: center;
                                                                text-decoration: none; width: 82px; padding-top: 5px; color: #000; cursor: pointer;"
                                                                onclick="javascript:messageClose();">Close </a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <textarea rows="10" class="qposttextarea" id="CKDescription" style="color: #D7D7D7;
                                    font-family: Arial; margin-bottom: 5px;" runat="server" placeholder="Write your question here..."
                                    clientidmode="Static"></textarea>
                                <asp:Label ID="lblSuMess" runat="server" Text="" Style="margin: 0px 0px 0px 10px;"></asp:Label>
                                <div style="padding-left: 15px;" class="fieldTxt">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="CKDescription"
                                        Display="Dynamic" ValidationGroup="Qstn" ErrorMessage="Please enter question"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div style="display: none;">
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                    <p style="padding-left: 25px">
                                        <b>Attach File :</b>
                                        <asp:FileUpload ID="uploadDoc" Style="padding-left: 25px" runat="server" /></p>
                                </div>
                                <div class="cls">
                                </div>
                                <p class="classify" style="margin-top: 5px;">
                                    Classify Your Question</p>
                                <div style="margin-left: 10px;">
                                    <select data-placeholder="Type to add a tag" class="chosen-select multipleSubjects"
                                        style="width: 675px;" id="txtSubjectList" onchange="getAllSubjectValue(this.id)"
                                        runat="server" clientidmode="Static" multiple tabindex="4">
                                    </select>
                                    <asp:HiddenField ID="hdnsubject" ClientIDMode="Static" runat="server" />
                                </div>
                                <p>
                                    &nbsp;</p>
                                <div class="cls">
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnkcreateForum" />
                            <asp:AsyncPostBackTrigger ControlID="LinkButton1" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <!--left box ends-->
            </div>
            <!--left verticle search list ends-->
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
            function callClose() {
                $('#txtSubjectList').val('').trigger('chosen:updated');
                $('#divSuccess').css("display", "none");
            }
        </script>
        <script type="text/javascript">
            function getAllMemberValue(ctrlId) {
                $('#divFreeSkorlTargetSearch').find('label.error').remove();
                var control = document.getElementById(ctrlId);
                var strSelTexts = '';
                var cnt = 0;
                for (var i = 0; i < control.length; i++) {
                    if (control.options[i].selected) {
                        if (cnt == 0) {
                            strSelTexts += control.options[i].value;
                        }
                        else {
                            strSelTexts += ',' + control.options[i].value;
                        }
                        cnt++;
                    }
                }
                $('#hdnMembId').val(strSelTexts);
            }
            function getAllSubjectValue(ctrlId) {
                $('#MicroTag').find('label.error').remove();
                var control = document.getElementById(ctrlId);
                var strSelTexts = '';
                var cnt = 0;
                for (var i = 0; i < control.length; i++) {
                    if (control.options[i].selected) {
                        if (cnt == 0) {
                            strSelTexts += control.options[i].value;
                        }
                        else {
                            strSelTexts += ',' + control.options[i].value;
                        }
                        cnt++;
                    }
                }
                $('#hdnsubject').val(strSelTexts);
            }
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                var ID = "#" + $("#hdnTabId").val();
                $(ID).focus();
            });
            function ShowLoading(id) {
                location.href = '#' + id;
            }
        </script>
        <script type="text/javascript">
            function messageClose() {
                $('#txtSubjectList').val('').trigger('chosen:updated');
                document.getElementById('divSuccess').style.display = 'none';
            }
            function getAllSubjectValue(ctrlId) {
                var control = document.getElementById(ctrlId);
                var strSelTexts = '';
                var cnt = 0;
                for (var i = 0; i < control.length; i++) {
                    if (control.options[i].selected) {
                        if (cnt == 0) {
                            strSelTexts += control.options[i].value;
                        }
                        else {
                            strSelTexts += ',' + control.options[i].value;
                        }
                        cnt++;
                    }
                }
                $('#hdnsubject').val(strSelTexts);
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
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#divSuccess').center();
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                    $('#divSuccess').center();
                });
            });
            function CallPostques() {
                $('#divSaveimage').css("display", "block");
                $('#lnkcreateForum').css("text-shadow", "0px 0px 1px #00B7E5");
                $('#lnkcreateForum').css("color", "#00A5AA");
                if ($('#CKDescription').val() == '') {
                    $('#divSaveimage').css("display", "none");
                    setTimeout(
                function () {
                    $('#divSaveimage').css("display", "none");
                    $('#lnkcreateForum').css("color", "#0096a1");
                    $('#lnkcreateForum').css("text-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            } 
            }
            function CallPostimg() {
                $('#divSaveimage').css("display", "block");
            }
            function CallPostimgs() {
                $('#divSaveimage').css("display", "none");
            }
        </script>
    </div>
</asp:Content>
