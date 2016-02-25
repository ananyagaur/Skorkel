<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Question-Details-SendContact.aspx.cs" Inherits="Question_Details_SendContact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container" style="padding-top: 0px">
        <asp:UpdatePanel ID="upmain" runat="server">
            <ContentTemplate>
                <div class="popUp1" id="PopUpShare" clientidmode="Static" runat="server" style="display: none;">
                    <div class="popUp" id="popUp">
                        <div class="popUpBox" style="margin-top: 25px; height: auto;">
                            <p class="shareContent" style="margin-top: 10px;">
                                Share Q & A</p>
                            <div style="">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td class="popHeading" style="display: none;">
                                                        <asp:Label ID="lblTitle" Text="Share" runat="server"></asp:Label>
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
                                        <td class="popBgLineGray" style="margin-top: 25px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="padding-left: 10px; margin-top: 10px;">
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td id="tdDepartment" style="height: 30px; width: 18%">
                                                                To :
                                                                <select data-placeholder="Enter members name here" class="chosen-select" id="txtInviteMembers"
                                                                    onchange="getMultipleValues(this.id)" runat="server" multiple style="width: 443px;"
                                                                    tabindex="4">
                                                                </select>
                                                                <asp:HiddenField ID="hdnInvId" ClientIDMode="Static" runat="server" />
                                                                <br />
                                                                <asp:Label ID="lblmsg" runat="server" Text="" Style="margin-left: 26px;" ForeColor="Red"
                                                                    ClientIDMode="Static"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="padding-left: 0px; padding-top: 10px;">
                                                <textarea id="txtBody" runat="server" cols="20" rows="2" placeholder="Message" class="forumTitle" style="width: 440px;
                                                    color: #9c9c9c; margin-left: 42px; margin-right: 50px; font: 111% Arial;"></textarea>
                                            </div>
                                            <div style="padding-top: 1px;">
                                                <asp:TextBox ID="txtLink" runat="server" value="Paste link" onblur="if(this.value=='') this.value='Paste link';"
                                                    Enabled="false" onfocus="if(this.value=='Paste link') this.value='';" class="forumTitlenew"
                                                    Style="width: 443px; font-size: small; margin-left: 44px;"></asp:TextBox>
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
                                        <td align="right">
                                            <div>
                                                <div>
                                                    <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Share"
                                                        ValidationGroup="Comment" CausesValidation="true" Style="margin-left: 63%; font-size: 16px;"
                                                        CssClass="joinBtn" OnClick="lnkPopupOK_Click"></asp:LinkButton>
                                                </div>
                                                <br />
                                                <div style="margin: -10px 58px 0px 0px;">
                                                    <a href="#" clientidmode="Static" causesvalidation="false" style="text-align: center;
                                                        text-decoration: none; width: 82px; color: #000; margin-left: 5px; font-size: 16px;"
                                                        onclick="MessClose();return false;">Cancel </a>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="innerDocumentContainerSpc" style="margin-top: 0px;">
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
                                <div class="searchBoxNewQ">
                                    <p class="relatedQ sm">
                                        Related Questions</p>
                                    <ul class="questsrelated">
                                        <asp:ListView ID="lstRelQuestions" runat="server" GroupPlaceholderID="groupPlaceHolder1"
                                            OnItemCommand="lstRelQuestions_ItemCommand" ItemPlaceholderID="itemPlaceHolder1">
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
                                                <asp:HiddenField ID="hdnPostQuestionId" runat="server" Value='<%#Eval("intPostQuestionId")%>' />
                                                <li>
                                                    <p class="breakallwords commentquestionsrply">
                                                        <asp:LinkButton ID="lnkQueAnsDesc" Font-Underline="false" runat="server" Text='<%#Eval("strQuestionDescription")%>'
                                                            CommandName="OpenQ"></asp:LinkButton>
                                                    </p>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ul>
                                    <a href="AllQuestionDetails.aspx" class="readmore new">Show more</a>
                                </div>
                            </div>
                            <!--search box ends-->
                            <!--left box starts-->
                            <div class="innerContainerLeft">
                                <div class="subtitle">
                                    <div class="recentBlogs">
                                        <asp:LinkButton ID="lnkBack" Font-Underline="false" runat="server" CssClass="recentBlogTxt"
                                            OnClick="lnkBack_click" Text="Back to Q&A"></asp:LinkButton>
                                    </div>
                                </div>
                                <div id="divSuccess" runat="server" style="border: 20px solid rgba(0,0,0,0.5); width: 39%;
                                    margin-left: 5%; display: none; position: fixed; top: 300px;" clientidmode="Static">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                        <tr>
                                            <td>
                                                <strong>&nbsp;&nbsp;
                                                    <asp:Label ID="lblSuccess" runat="server" Text="Comment Added Successfully..!" ForeColor="Green"
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
                                <asp:ListView ID="lstParentQADetails" runat="server" OnItemDataBound="lstParentQADetails_ItemDataBound"
                                    OnItemCommand="lstParentQADetails_ItemCommand">
                                    <ItemTemplate>
                                        <div class="forumList" style="width: 100%; border-bottom: 0px; margin-bottom: 0px;
                                            padding-bottom: 0px;">
                                            <div class="searchListTxt" style="width: 725px;">
                                                <asp:HiddenField ID="hdnPostQuestionID" Value='<%# Eval("intPostQuestionId") %>'
                                                    runat="server" />
                                                <asp:HiddenField ID="hdnintAddedBy" Value='<%# Eval("intAddedBy") %>' runat="server" />
                                                <div class="qquestion" style="margin-top: 20px;">
                                                    Q</div>
                                                <div class="questionTxt breakallwords" style="width: 650px;">
                                                    <asp:Label ID="Label1" Font-Underline="false" CommandName="QADetails" runat="server"
                                                        Text='<%#Eval("strQuestionDescription") %>'></asp:Label>
                                                </div>
                                                <p class="authorNameBlk">
                                                    <asp:Label ID="Label2" Font-Underline="false" runat="server" Text='<%#Eval("AuthorName") %>'></asp:Label>
                                                </p>
                                            </div>
                                            <div class="categoryBoxQA" style="margin-left: 56px;">
                                                <div class="categoryTxt" style="width: 615px;">
                                                    <div class="ansTags" style="width: 692px; margin-left: 0px;">
                                                        <ul>
                                                            <asp:ListView ID="lstSubjCategory" runat="server">
                                                                <ItemTemplate>
                                                                    <li id="SubLi" runat="server" style="border: none; margin-right: 5px;">
                                                                        <asp:HiddenField ID="hdnSubCatId" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                                        <asp:LinkButton ID="lnkCatName" ForeColor="WhiteSmoke" Style="text-decoration: none !important;"
                                                                            Font-Underline="false" runat="server" Text='<%#Eval("strCategoryName")%>'></asp:LinkButton>
                                                                        <%-- <span>
                                                                    <%#Eval("strCategoryName")%></span>--%>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="answerTxt mr" style="margin-top: -8px;">
                                                <div class="aans">
                                                    <div style="margin-top: 4%; margin-left: 25%;">
                                                        <asp:LinkButton ID="btnReply" Style="text-decoration: none; color: #9C9C9C; margin-top: -2px;"
                                                            Text="Answers" runat="server" CommandName="ReplyPost" ToolTip="Add Answers"></asp:LinkButton>:
                                                        <asp:Label ID="lblreply" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="aans s">
                                                    <img src="images/blog-share.png" align="absmiddle" />
                                                    <div style="margin-top: -13%; margin-left: 25%;">
                                                        <asp:LinkButton ID="btnShare" Style="text-decoration: none; margin-top: -2px;" Text="Shared :"
                                                            runat="server" CommandName="ShareForum" ToolTip="Shared"></asp:LinkButton>
                                                        <asp:Label ID="lblShare" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="aans s">
                                                    <img src="images/blog-like.png" align="absmiddle" />
                                                    <div style="margin-top: -15%; margin-left: 25%;">
                                                        <asp:LinkButton ID="btnLike" Style="text-decoration: none; margin-top: -2px;" Text="Like :"
                                                            runat="server" CommandName="LikeForum" ToolTip="Like"></asp:LinkButton>
                                                        <asp:Label ID="lblTotallike" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="ansDate" style="margin-right: 2%; margin-top: 1%;">
                                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label></div>
                                            </div>
                                            <div class="bgLine" style="width: 747px; margin-top: 0px; border-bottom: 1px solid #e7e7e7;
                                                background: none;" id="Div2">
                                                &nbsp;</div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                                <asp:Panel ID="pnlComment" ClientIDMode="Static" runat="server">
                                    <div class="forumBoxQAns">
                                        <a name="editqa"></a>
                                        <asp:Label ID="lblMess" ForeColor="Red" runat="server" Text=""></asp:Label>
                                        <textarea id="CKEditorControl" class="uploadDescriptionTxt postAns" runat="server"
                                            style="font-size: 14px;" clientidmode="Static" placeholder="Post your answer here"></textarea>
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="CKEditorControl"
                                            Display="Dynamic" ClientIDMode="Static" ValidationGroup="t" ErrorMessage="Please enter your answer."
                                            ForeColor="Red" Style="margin-left: 7%;"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblSuMess" runat="server" Text="" Style="margin-left: 7%;"></asp:Label>
                                        <div>
                                            <p style="padding-left: 25px;">
                                                <span style="display: none;"><b>Attach File :&nbsp;</b>
                                                    <asp:FileUpload ID="uploadDoc" Style="font-size: 13px;" runat="server" />
                                                </span><span style="margin-left: -150px; display: none;">
                                                    <asp:CheckBox ID="chkPrMess" Style="padding-left: 140px;" runat="server" />&nbsp;Send
                                                    a private message </span>
                                            </p>
                                        </div>
                                    </div>
                                    <div style="width: 700px">
                                        <asp:LinkButton ID="lnkcreateForum" Text="Post Answer" runat="server" ClientIDMode="Static" OnClientClick="javascript:callPostAns();"
                                            Style="margin-bottom: 12px; font-size: 16px;" ValidationGroup="t" OnClick="btnSave_Click"
                                            class="postAnswer"></asp:LinkButton>
                                    </div>
                                </asp:Panel>
                                <div class="cls">
                                </div>
                                <div class="otherAnsQA">
                                    <span style="margin-left: 5px;">Other's Answers</span></div>
                                <div class="forumList" style="width: 700px; border-bottom: 0px;">
                                    <div style="margin-left: 10px;">
                                        <asp:ListView ID="lstAllReplies" runat="server" OnItemCommand="lstAllReplies_ItemCommand"
                                            OnItemDataBound="lstAllReplies_ItemDataBound">
                                            <LayoutTemplate>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr id="itemPlaceholder" runat="server">
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr id="QArep" runat="server">
                                                    <td>
                                                        <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' ClientIDMode="Static"
                                                            runat="server" />
                                                        <asp:HiddenField ID="hdnQAReplyLikeShareId" Value='<%#Eval("intQAReplyLikeShareId") %>'
                                                            ClientIDMode="Static" runat="server" />
                                                        <asp:HiddenField ID="hdnintPrivateMessage" Value='<%#Eval("intPrivateMessage") %>'
                                                            ClientIDMode="Static" runat="server" />
                                                        <div class="otherAnsBox">
                                                            <div class="thumbnailQA" style="width: 100%; height: 50px;">
                                                                <img id="imgprofile" runat="server" style="width: 40px; height: 46px; float: left"
                                                                    src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' />
                                                                <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                                                &nbsp;<asp:LinkButton Font-Underline="false" CommandName="Details" ID="Label1" Style="font-size: 20px;
                                                                    margin: 0; padding: 0; color: #727272;" runat="server" Text='<%#Eval("Name") %>'></asp:LinkButton><br />
                                                                <div class="ansTimes">
                                                                    Commented on
                                                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label>
                                                                </div>
                                                            </div>
                                                            <p class="breakallwords commentquestionsrply">
                                                                <asp:Label ID="lblReplyComment" Style="color: #9C9C9C;" runat="server" Text='<%#((string)Eval("strComment")).Replace("\n", "<br>") %>'></asp:Label>
                                                            </p>
                                                            <div class="cls hgtn">
                                                            </div>
                                                            <div class="qscreenBox" style="margin-right: 5px;">
                                                                <asp:LinkButton Font-Underline="false" CssClass="edit" CommandName="Edit Ans" ID="lnkEdit"
                                                                    runat="server" Visible="false" Text="Edit"></asp:LinkButton>
                                                                <asp:LinkButton Font-Underline="false" CssClass="edit" CommandName="Delete Ans" ID="lnkdelete"
                                                                    Visible="false" runat="server" Text="Delete"></asp:LinkButton>
                                                            </div>
                                                            <div class="cls">
                                                                &nbsp;</div>
                                                        </div>
                                                        <asp:Panel ID="pnlAttachFile" Style="display: none; padding-left: 50px;" runat="server">
                                                            <img src="images/AttachIcon.jpg" />
                                                            <asp:Label ID="lblAttachDocs" runat="server" Text='<%#Eval("strFileName") %>'></asp:Label>
                                                        </asp:Panel>
                                                        <asp:LinkButton Font-Underline="false" CommandName="View Close" ID="lnkClose" Style="color: #40BFC4;
                                                            padding-left: 50px; display: none" runat="server" Text="Close"></asp:LinkButton>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                                <div class="cls">
                                    &nbsp;</div>
                            </div>
                            <!--left box ends-->
                        </div>
                        <!--left verticle search list ends-->
                    </div>
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
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkPopupOK" />
                <asp:AsyncPostBackTrigger ControlID="lnkcreateForum" />
                <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function MessClose() {
            $('#txtInviteMembers').val('').trigger('chosen:updated');
            $('#lblmsg').text('');
            $('#hdnInvId').val('');
            document.getElementById("PopUpShare").style.display = 'none';
            $('#divDeletesucess').css("display", "none");
        }
    </script>
    <script type="text/javascript">
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
            $('#hdnInvId').val(strSelText);

        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#divSuccess').center();
        });
        function messageClose() {
            document.getElementById('divSuccess').style.display = 'none';
        }
        function docdelete() {
            $('#divDeletesucess').css("display", "block");
            document.getElementById("PopUpShare").style.display = 'none';
        }
        function divCancels() {
            $('#divDeletesucess').css("display", "none");
            document.getElementById("PopUpShare").style.display = 'none';
        }
        function callPostAns() {
            $('#lnkcreateForum').css("box-shadow", "0px 0px 5px #00B7E5");
            $('#lnkcreateForum').css("background", "#00A5AA");
            if ($('#CKEditorControl').text() == '') {
                setTimeout(
                function () {
                    $('#lnkcreateForum').css("background", "#0096a1");
                    $('#lnkcreateForum').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
        }
    </script>
</asp:Content>
