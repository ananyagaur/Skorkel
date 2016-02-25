<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Research-Case Details.aspx.cs" Inherits="Research_Case_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("js/jquery.custom-scrollbar.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("js/jquery.mCustomScrollbar.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
    <style type="text/css">
        .preced
        {
            background-color: Yellow;
        }
        .rediod
        {
            background-color: #AECF00;
        }
        .issuss
        {
            background-color: Orange;
        }
        .factss
        {
            background-color: Aqua;
        }
        .highls
        {
            background-color: Lime;
        }
        .orbits
        {
            background-color: Green;
        }
        .ImpPss
        {
            background-color: Fuchsia;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="cls">
        </div>
        <div id="myResearch">
            <div class="innerDocumentContainerSpc" style="margin-top: -1px;">
                <div class="innerContainer">
                    <div class="innerGroupBox">
                      <div class="container-popupreserchdetails" id="divPopp" style="display: none;">
                        <div class="popupreserch" style="width: 200px; font-weight: bold;">
                            <img id="imgSave" src="images/loadingImage.gif" />
                        </div>
                    </div>
                        <!--left box starts-->
                        <div class="innerContainerLeft caseDetails">
                            <div class="subtitle">
                                <div class="recentBlogs">
                                    <span style="font-weight: bold;">
                                        <img src="images/recentBlogs.png" align="middle" alt="" />
                                        <asp:HiddenField ID="hdnBack" runat="server" Value="0" ClientIDMode="Static" />
                                        <a onclick="historyBack();" class="recentactive" style="cursor:pointer;">Back to Results</a>
                                        </span>
                                </div>
                            </div>
                            <div class="cls">
                            </div>
                            <asp:UpdatePanel ID="upDesk" runat="server" UpdateMode="Conditional">
                                     <ContentTemplate>
                            <div class="leftCaseBox">
                                <!--party starts-->
                                <div class="postTitleArea myBlogs createBox">
                                         <p>
                                     <asp:Label ID="lblpartyname" runat="server" Text="Party Name"></asp:Label> </p>
                                    <span class="bigPipe">|</span>
                                    <p>
                                       <asp:Label ID="lblcitation" runat="server" Text="AIR 1984 SC571"></asp:Label> </p>
                                    <span class="bigPipe">|</span>
                                    <p>
                                        <asp:Label ID="lblcourt" runat="server" Text="Supreme Court"></asp:Label> </p>
                                    <span class="bigPipe">|</span>
                                    <p>
                                        <asp:Label ID="lblyear" runat="server" Text="1984"></asp:Label> </p>
                                </div>
                                <!--party ends-->
                                <!--party ends-->
                                <div class="cls">
                                </div>
                                <!--judges starts-->
                                <div class="judgesBox">
                                    <p class="bld">
                                        Judges:</p>
                                 <asp:ListView ID="lstJudgeName" runat="server">
                                    <ItemTemplate>
                                    <p>
                                        <asp:Label ID="lblJudgeName" runat="server" Text='<%#Eval("JudgeName")%>'></asp:Label>
                                        </p>
                                    </ItemTemplate>
                                    </asp:ListView>
                                    </div>
                                <!--judges ends-->
                                <div class="bookmarkImg" style="margin: -26px 0px 0 0;">
                                    <asp:UpdatePanel ID="upresearchp" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="lnkFvrtImage" runat="server" ToolTip="Bookmark" OnClick="lnkFvrtImage_Click"
                                                ClientIDMode="Static" OnClientClick="javascript:callFvrtimg();" >
                                                <img id="imgFvrt" src="images/gray-tag.png" runat="server" clientidmode="Static" alt="" />
                                            </asp:LinkButton>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="lnkFvrtImage" EventName="Command" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="cls">
                                </div>
                                <p class="citiedBy">
                                    Cited By:
                                    <asp:Label ID="lblCitedBy" runat="server" Text=""></asp:Label>
                                    Cases</p>
                                <div class="cls">
                                </div>
                                <!--tab starts-->
                                <asp:UpdatePanel ID="updates" runat="server">
                                <ContentTemplate> 
                                <div class="citedTabs">
                                    <%--<asp:LinkButton ID="lnkComments" runat="server" ClientIDMode="Static" CssClass="citedCited"
                                        Text="Comment" OnClick="lnkComments_Click" PostBackUrl="#Commentsection"></asp:LinkButton>--%>
                                        <asp:LinkButton ID="lnkComments" runat="server" ClientIDMode="Static" CssClass="citedCited"
                                        Text="Comment" OnClick="lnkComments_Click" ></asp:LinkButton>
                                    <asp:LinkButton ID="lnkShare" runat="server" ClientIDMode="Static" CssClass="shareCited"
                                        Text="Share" OnClick="lnkShare_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDownload" runat="server" ClientIDMode="Static" CssClass="downloadCited"
                                        Text="Download" OnClick="lnkDownload_Click" OnClientClick="javascript:DownLoadClicks();return true;" ></asp:LinkButton>
                                    <asp:LinkButton ID="lnkShowOrgtxt" runat="server" ClientIDMode="Static" CssClass="showOriText"
                                        Text="Show Original Text" OnClick="lnkShowOrgtxt_Click"></asp:LinkButton>
                                </div>
                                <!--tab ends-->
                                <div class="cls">
                                </div>
                                <asp:HiddenField ID="intCommentAddedFors" runat="server" ClientIDMode="Static" />
                                <%--<div class="orgTxt" style="background-size: 170px 20px;">
                                    <asp:Label ID="lblOriginalTxt" runat="server" ClientIDMode="Static" style="font-size:16px;"></asp:Label>
                                </div>--%>
                                <div class="orgTxt" style="background-size: 100% 85%;width: 25%;">
                                    <asp:Label ID="lblOriginalTxt" runat="server" ClientIDMode="Static" style="font-size:16px;"></asp:Label>
                                </div>
                                <!--Summary Write starts-->
                                <div id="DivSummary" clientidmode="Static" class="grayBox" style="display: none;
                                    z-index: 100; position: fixed; margin: -24% 0 0 3%;">
                                    <div class="profileBox">
                                        <input id="hdnSummaryTextPost" type="hidden" runat="server" />
                                        <div class="cls">
                                        </div>
                                        <div class="jp-container">
                                            <textarea id="txtSummary" clientidmode="Static" runat="server" class="tabSummary" ></textarea>
                                            <asp:RequiredFieldValidator ValidationGroup="Summary" ID="RequiredFieldValidator6"
                                                runat="server" ControlToValidate="txtSummary" ErrorMessage="Please enter Comment"
                                                ForeColor="Red" Style="position: absolute; padding-left: 0px; padding-top: 0px;
                                                font-size: small"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="tabsCloseBtn">
                                            <asp:LinkButton ID="BtnSaveSummary" CssClass="createBtn" CausesValidation="true" OnClientClick="CallSaveSummaery();"
                                                ClientIDMode="Static" ValidationGroup="Summary" runat="server" Text="Submit"
                                                Style="text-decoration: none; font-weight: bold;" OnClick="BtnSaveSummary_Click"></asp:LinkButton>
                                            <a href="#" onclick="Cancel();" style="text-decoration: none; font-weight: bold;
                                                height: 20px;" class="closeBtn">Cancel</a>
                                        </div>
                                    </div>
                                </div>
                                <!--Summary Write ends-->
                                <div id="PopUpShare" runat="server" clientidmode="Static" style="border: 20px solid rgba(0,0,0,0.5);
                                    float: left; padding-top: 0px; width: 500px; position: fixed; margin: -235px 0 0 10%; z-index: 100; display: none">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td id="tdPopupColor" runat="server" style="width: 25px; height: 40px">
                                                            &nbsp;
                                                        </td>
                                                        <td class="popHeading" style="padding: 0 0 0 0px;text-align: center;">
                                                            <asp:Label ID="lblTitles" Text="Share Document" runat="server"></asp:Label>
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
                                            <td class="popBgLineGray">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style="padding-left: 10px;">
                                                    <div>
                                                        <table>
                                                            <tr>
                                                                <td id="tdDepartment" style="height: 30px; width: 18%">
                                                                    To :
                                                                    <select data-placeholder="Enter members name here" class="chosen-select"
                                                                        id="txtInviteMembers" onchange="getMultipleValues(this.id)" runat="server" clientidmode="Static"
                                                                        multiple style="width: 410px; padding-left: 30px;" tabindex="4">
                                                                    </select>
                                                                    <asp:HiddenField ID="hdnInvId" ClientIDMode="Static" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div style="padding-left: 37px;">
                                                    <asp:Label ID="lblMess" ForeColor="Red" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="groupPhotoBgNew">
                                                    <img id="imgGrp1" runat="server" width="60" height="60" alt="" />
                                                </div>
                                                <div class="groupNameNew">
                                                    <asp:Label ID="lblDocTitle" runat="server"></asp:Label>
                                                    <p>
                                                        <asp:Label ID="lblGroupSummary1" runat="server"></asp:Label>
                                                    </p>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style="padding-left: 15px; padding-top: 10px;">
                                                    <textarea id="txtBody" clientidmode="Static" runat="server" cols="20" rows="2" placeholder="Message" class="forumTitle" style="width: 400px; 
                                                        margin-left: 21px; margin-right: 50px;color:#9c9c9c;  font-family: Arial;"></textarea>
                                                </div>
                                                <div style="padding-top: 1px;">
                                                    <asp:TextBox ID="txtLink" runat="server" placeholder="Paste link" ReadOnly="true" class="forumTitlenew" Style="width: 405px; font-family: Arial;font-size: 14px;  margin-left: 36px;"></asp:TextBox>
                                                </div>
                                                <p>
                                                    <strong>
                                                        <asp:Label ID="lblMessAccept" runat="server"></asp:Label>
                                                        <asp:Label ID="lblMessReject" runat="server"></asp:Label>
                                                    </strong>
                                                </p>
                                                <tr>
                                                    <td align="right">
                                                        <table width="100" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Share"
                                                                        CssClass="joinBtn" OnClick="lnkPopupShare_Click"></asp:LinkButton>
                                                                </td>
                                                                <td style="padding-right: 60px;">
                                                                    <a clientidmode="Static" style="float: left; text-align: center; text-decoration: none;
                                                                        width: 82px; color: #000; padding-right: 20px; padding-bottom: 10px; cursor: pointer;"
                                                                        onclick="Cancel();">Cancel </a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                </ContentTemplate>
                                <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkShare" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkComments" EventName="Click" />
                                </Triggers>
                                 </asp:UpdatePanel>
                                <p class="headNote">
                                    <asp:Label ID="lblCaseTitle" runat="server" ClientIDMode="Static" Text="Title"></asp:Label></p>
                                <div class="headTxt">
                                    <div runat="server" id="DivDesc">
                                                <p>
                                                    <asp:HiddenField runat="server" ID="hdndivvalue" ClientIDMode="Static" />
                                                </p>
                                                <div>
                                                    <br />
                                                    <p>
                                                        <%--<div id="divdisp" name="divdisp" style="font-family: Calibri; font-size: 18px; color: #141414; text-align:justify;
                                                            padding: 0px 15px 0px 15px;" onmouseup="javascript:highlightSelection('#FF0000');" runat="server"
                                                            clientidmode="Static" onkeypress="javascript:return false;" onkeydown="javascript:return false;">--%>
                                                            <div id="divdisp" name="divdisp" style="font-family: Calibri; font-size: 18px; color: #141414; text-align:justify;
                                                            padding: 0px 15px 0px 15px;" runat="server"
                                                            clientidmode="Static" onkeypress="javascript:return false;" onkeydown="javascript:return false;">
                                                        </div>
                                                    </p>
                                                    <p>
                                                        <div id="divGuest" runat="server" clientidmode="Static" style="">
                                                        </div>
                                                    </p>
                                                </div>
                                            
                                    </div>
                                    <!--mark as box starts-->
                                    <div id="DivMenuBar" class="markAsBox" style="display: none;">
                                        <div class="marAsTxt">
                                            Mark As</div>
                                        <ul>
                                            <li>
                                                        <asp:LinkButton ID="lnkFactTab" ClientIDMode="Static" runat="server"  OnClick="lnkFactTab_Click" 
                                                            ToolTip="Fact"><span>Fact</span></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkIssueTab" ClientIDMode="Static" runat="server" OnClick="lnkIssueTab_Click" 
                                                    ToolTip="Issue"><span>Issue</span></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkImparagrph" ClientIDMode="Static" runat="server" OnClick="lnkImportantPara_Click"
                                                    ToolTip="Important Paragraph"><span>Important Paragraph</span></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkPrecedent" ClientIDMode="Static" runat="server" OnClick="lnkPrecedent_Click"
                                                    ToolTip="Precedent"><span>Precedent</span></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkDecidendit" ClientIDMode="Static" runat="server" OnClick="lnkDecidendit_Click"
                                                    ToolTip="Radio Decidendi"><span>Ratio Decidendi</span></asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkOrbite" ClientIDMode="Static" runat="server" OnClick="lnkOrbite_Click"
                                                    ToolTip="Orbiter Dictum"><span>Orbiter Dictum</span></asp:LinkButton></li>
                                            <li style="display:none;">
                                                <asp:LinkButton ID="lnkHighlight" ClientIDMode="Static" runat="server" OnClick="lnkHighlight_Click"
                                                    ToolTip="Highlight"><span>Highlight</span></asp:LinkButton></li>
                                        </ul>
                                    </div>
                                    <!--mark as box ends-->
                                </div>
                                <a name="Commentsection"></a>
                                 <div id="CommentSections" class="CommentSections"></div>
                                <div id="DivCommentContent" runat="server" clientidmode="Static" style="display: none;">
                                    <div>
                                            <asp:UpdatePanel ID="upSaveButton" runat="server">
                                                <ContentTemplate>
                                                    <textarea rows="10" class="entryForumstArea" id="txtComment" runat="server" placeholder="Comment" 
                                                        clientidmode="Static" style="margin: 20px 0px 10px 80px; width: 525px; height: 50px; border: 1px solid #e7e7e7;"></textarea>
                                                        <br />
                                                    <div>
                                                    <asp:RequiredFieldValidator ValidationGroup="Comm" ID="RequiredFieldValidator5" runat="server"
                                                        ControlToValidate="txtComment" ErrorMessage="Please enter Comment"
                                                        ForeColor="Red" Style="position: absolute; padding-left: 80px; margin-top: -10px;
                                                        font-family: Arial; font-size: 14px;"></asp:RequiredFieldValidator>
                                                        </div>
                                                    <asp:LinkButton ID="BtnSaveComment" CausesValidation="true" ValidationGroup="Comm" OnClientClick="CallSaveComment();"
                                                        CssClass="vote" runat="server" Text="Post" Style="float: left; text-decoration: none;
                                                        margin: 0px 0px 15px 67%;" OnClick="BtnSaveComment_Click"></asp:LinkButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                    </div>
                                    <asp:UpdatePanel ID="updateCaseListComment" runat="server">
                                        <ContentTemplate>
                                            <asp:ListView ID="lstComments" runat="server" OnItemDataBound="lstComment_ItemDataBound"
                                                OnItemCommand="lstComment_ItemCommand" OnItemDeleting="lstComment_OnItemDeleting"
                                                DataKeyNames="intCid">
                                                <ItemTemplate>
                                                    <div class="commentContainer" style="background-position: 35px top;">
                                                        <div class="commentIcon">
                                                            <img src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' id="imgprofile" runat="server"
                                                                class="commentPhoto" width="42" height="42" />
                                                            <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                                        </div>
                                                        <div class="comments">
                                                            <img src="images/right-arrow.jpg" class="rightArrow" alt="" />
                                                            <tr onmouseover="Onlyshow('ListComment<%#Eval("intCid")%>','<%#Eval("intAddedby")%>')"
                                                                onmouseout="OnlyHide('ListComment<%#Eval("intCid")%>','<%#Eval("intAddedby")%>')">
                                                                <td>
                                                                    <div class="commentTxtNew">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td width="82%" style="padding-left: 10px" align="left">
                                                                                    <asp:HiddenField ID="hdnTagId" runat="server" Value='<%#Eval("intCid")%>' />
                                                                                    <asp:HiddenField ID="hdnCommentId" runat="server" Value='<%#Eval("intCid")%>' />
                                                                                    <asp:HiddenField ID="hdnContentId" runat="server" Value='<%#Eval("intCaseId") %>' />
                                                                                    <asp:HiddenField ID="hdnContentTypeId" runat="server" Value='<%#Eval("intContentTypeId")%>' />
                                                                                    <asp:HiddenField ID="hdnCommentedText" runat="server" Value='<%#Eval("strCommentedText")%>' />
                                                                                    <asp:HiddenField ID="hdnAddedby" runat="server" Value='<%#Eval("intAddedby")%>' />
                                                                                    <asp:HiddenField ID="hdnLinkUserId" runat="server" Value='<%#Eval("LinkUserId")%>' />
                                                                                    <asp:LinkButton ForeColor="#006699" ID="lnkSharedUserName" CommandName="SharedDetails"
                                                                                        runat="server" Font-Bold="true" Style="font-size: 14px; color: #40BFC4; font-style: normal; cursor:default;
                                                                                        text-decoration: none !important;" Text='<%#Eval("strUserName")+ "&#39;s."%>' Font-Italic="true"></asp:LinkButton>&nbsp;
                                                                                    <asp:Label ID="lblPostComments" ClientIDMode="Static" runat="server" Style="font-size: 14px;
                                                                                        color: #6D6E71" Text='<%#Eval("strComment") %>'></asp:Label>
                                                                                    <asp:Label ID="lblComment" runat="server" Style="font-size: 14px; color: #6D6E71;
                                                                                        display: none" Text='<%#Eval("strCommentedText")%>'></asp:Label>
                                                                                    <br />
                                                                                </td>
                                                                                <td width="18%" valign="top" style="padding-right: 15px;" align="right">
                                                                                    <div style="display: none;">
                                                                                        <%#Eval("strComment")%>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                            </tr>
                                                            </table>
                                                        </div>
                                                        <div class="actions">
                                                            <div class="posted">
                                                                <asp:Label ID="lblEntryDate" runat="server" Text='<%#Eval("dtEntryDate") %>'></asp:Label></div>
                                                        </div>
                                                    </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="lstComments" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                            </div>
                            <!--right box starts-->
                            <div class="rightBoxCase">
                                <div class="cls">
                                </div>
                                <center>
                                    <div style="margin-top: 10px;">
                                        <asp:UpdatePanel ID="updatee" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="lnkWriteButton" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lnkWriteButton" Height="34px" Width="180px" runat="server" Font-Bold="true" style="display:block;font-size:16px;text-decoration:none !important;"
                                                    ClientIDMode="Static" CssClass="WriteSummerys" Text="Summary" OnClientClick="CallWriteSummary();" OnClick="lnkWriteButton_Onclick">
                                                </asp:LinkButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </center>
                                <div class="useracitivityInCase" style="font-size:16px;">
                                    User activity on this case</div>
                                <div>
                                    <asp:ListView ID="lstUserActivityCase" runat="server" OnItemCommand="lstUserActivityCase_ItemCommand"
                                        OnItemDataBound="lstUserActivityCase_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnintCommentAddedFor" runat="server" ClientIDMode="Static"
                                                Value='<%# Eval("intAddedby") %>' />
                                            <asp:HiddenField ID="hdnintContentId" runat="server" ClientIDMode="Static" Value='<%# Eval("intCaseId") %>' />
                                            <!--user comment starts-->
                                            <div class="userRightBox">
                                                <div class="usrimage">
                                                    <img id="imgprofile" runat="server" src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>'
                                                        width="40" height="40" alt="" />
                                                    <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                                </div>
                                                <div class="usrnm">
                                                    <asp:LinkButton Font-Underline="false" CommandName="UserName" ID="lnkName" CssClass="usrnm"
                                                        runat="server" Text='<%#Eval("Name") %>'></asp:LinkButton>
                                                </div>
                                                <div class="cls">
                                                </div>
                                                <p class="usrBrief">
                                                    <asp:LinkButton Font-Underline="false" CommandName="Title" ID="lnkTitle" CssClass="usrBrief"
                                                        Style="color: #B3B3B3;" runat="server" Text='<%#Eval("Title") %>'></asp:LinkButton>
                                                </p>
                                                <p class="usrBriefTxt">
                                                    <asp:Label ID="lblAttachDocs" runat="server" Text='<%#Eval("Descriptions") %>' CssClass="usrBriefTxt"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="userShareBox">
                                                <p>
                                                    <img src="images/blog-comments.png" /><br />
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("TotalComment") %>'></asp:Label></p>
                                                <p style="border: 0">
                                                    <img src="images/blog-view1.png" alt="" /><br />
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("TotalView") %>'></asp:Label></p>
                                            </div>
                                            <!--user comment ends-->
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <div class="cls">
                                </div>
                                <div class="cls">
                                </div>
                                <div class="cls">
                                </div>
                            </div>
                                <asp:HiddenField ID="hdnCommentAddedFor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField runat="server" ID="hdncolour" ClientIDMode="Static" />
                                <asp:HiddenField runat="server" ID="hdnMarkText" ClientIDMode="Static" />
                                <asp:HiddenField runat="server" ID="hdnPasteCode" ClientIDMode="Static" />
                                <asp:HiddenField runat="server" ID="hdnX" ClientIDMode="Static" />
                                <asp:HiddenField runat="server" ID="hdnY" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnhtmlSelectedText" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnAttemptselection" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnPostBackCheck" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnPostDescTxt" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnSelectionCount" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnSlectedText" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnTabId" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnTagtypeId" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnLoginId" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnRatioTabId" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnMainSelectedText" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnComments" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnStartIdx" ClientIDMode="Static" runat="server" />
                                <asp:HiddenField ID="hdnEndIdx" ClientIDMode="Static" runat="server" />
                                <asp:HiddenField ID="hdnMarkMaxCount" Value="0"  ClientIDMode="Static" runat="server" />
                                <asp:HiddenField ID="hdnDivContent" runat="server" ClientIDMode="Static" />
                             </ContentTemplate>
                                <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkFactTab" />
                                <asp:AsyncPostBackTrigger ControlID="lnkIssueTab" />
                                <asp:AsyncPostBackTrigger ControlID="lnkImparagrph" />
                                <asp:AsyncPostBackTrigger ControlID="lnkPrecedent" />
                                <asp:AsyncPostBackTrigger ControlID="lnkDecidendit" />
                                <asp:AsyncPostBackTrigger ControlID="lnkOrbite" />
                                </Triggers>
                                </asp:UpdatePanel>
                            <!--right box ends-->
                            <div class="cls">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
       </div>
    <script type="text/javascript">
            $(document).ready(function () {
                var flagno = "1";
                var Groupvalues = "";
                var CommentUserId = '';
                CommentUserId = $('#intCommentAddedFors').val();
                if (CommentUserId != '') {
                    var body = $("#divdisp").html();
                    body = body.replace(/<span4 class="preced">/g, "");
                    body = body.replace(/<\/span4>/g, "");
                    body = body.replace(/<span5 class="rediod">/g, "");
                    body = body.replace(/<\/span5>/g, "");
                    body = body.replace(/<span2 class="issuss">/g, "");
                    body = body.replace(/<\/span2>/g, "");
                    body = body.replace(/<span1 class="factss">/g, "");
                    body = body.replace(/<\/span1>/g, "");
                    body = body.replace(/<span7 class="highls">/g, "");
                    body = body.replace(/<\/span7>/g, "");
                    body = body.replace(/<span6 class="orbits">/g, "");
                    body = body.replace(/<\/span6>/g, "");
                    body = body.replace(/<span3 class="ImpPss">/g, "");
                    body = body.replace(/<\/span3>/g, "");
                    body = body.replace(/&amp;/g, '&'); 
                    var caseId = '<%= Request.QueryString["cId"]%>';
                    var tagTypeIds = ',';
                    $("#DivCommentContent").css('display', 'block');
                    tagTypeIds += "1,2,3,4,5,6,7";
                    $('#divdisp').unbind('mouseup');
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        data: '{"CaseId":"' + caseId + '","TagTypeIds":"' + tagTypeIds + '","FlagNo":"' + flagno + '","GroupId":"' + Groupvalues + '","CommentUserId":"' + CommentUserId + '"}',
                        url: 'DocumentWebService.asmx/GetDocumentIndexes',
                        async: false,
                        dataType: "json",
                        success: function (data) {
                            body = $.trim(body).replace(/\s(?=\s)/g, '');
                            $(data.d).each(function () {
                                body = SetBackgroundColor(body, this.start, this.end, this.imgtype, this.extralength, this.rownum, this.name, this.NoSpan, this.MaxCount);
                            });

                            $("#divdisp").html(body);
                        }
                    });
                    return false;
                }
            });
    </script>
    <script type="text/javascript">
        var flagno = "1";
        var Groupvalues = "";
        function onlinkclick() {
            var body = $("#divdisp").html();
            body = body.replace(/<span4 class="preced">/g, "");
            body = body.replace(/<\/span4>/g, "");
            body = body.replace(/<span5 class="rediod">/g, "");
            body = body.replace(/<\/span5>/g, "");
            body = body.replace(/<span2 class="issuss">/g, "");
            body = body.replace(/<\/span2>/g, "");
            body = body.replace(/<span1 class="factss">/g, "");
            body = body.replace(/<\/span1>/g, "");
            body = body.replace(/<span7 class="highls">/g, "");
            body = body.replace(/<\/span7>/g, "");
            body = body.replace(/<span6 class="orbits">/g, "");
            body = body.replace(/<\/span6>/g, "");
            body = body.replace(/<span3 class="ImpPss">/g, "");
            body = body.replace(/<\/span3>/g, "");
            body = body.replace(/&amp;/g, '&'); 
            var caseId = '<%= Request.QueryString["cId"]%>';
            var tagTypeIds = ',';
            tagTypeIds += "1,2,3,4,5,6,7";
            var intcommentId = $('#intCommentAddedFors').val();
            flagno = "1";
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: '{"CaseId":"' + caseId + '","TagTypeIds":"' + tagTypeIds + '","FlagNo":"' + flagno + '","GroupId":"' + Groupvalues + '","CommentUserId":"' + intcommentId + '"}',
                url: 'DocumentWebService.asmx/GetDocumentIndexes',
                async: false,
                dataType: "json",
                success: function (data) {
                    body = $.trim(body).replace(/\s(?=\s)/g, '');
                    $(data.d).each(function () {
                        body = SetBackgroundColor(body, this.start, this.end, this.imgtype, this.extralength, this.rownum, this.name, this.NoSpan, this.MaxCount);
                        document.getElementById('divPopp').style.display = 'none';
                    });
                    $("#divdisp").html(body);
                }
            });
            return false;
        }
    </script>
    <script type="text/javascript">
        var flagno = "";
        var Groupvalues = "";
        var EndText = "";
        var imagelength = 0;
        var laststart;
        var isdisplayed = "0";
        var startIndexs = 0;
        function SetBackgroundColor(body, start, end, type, extralength, rownum, name, NoSpan, MaxCount) {
            var part1 = "";
            var part2 = "";
            $('#hdnMarkMaxCount').val(MaxCount);
            $('#lblOriginalTxt').text(name);
            var bodyDetails1 = body;
            var bodyDetails2 = body;
            var Index1 = start;
            var Index2 = end;
            var length = bodyDetails1.length;
            for (var i = 0; i < length; i++) {
                var ind1 = bodyDetails1.indexOf('<');
                var ind2 = bodyDetails1.indexOf('>');
                var len = parseInt(ind2) - parseInt(ind1);
                if ((ind1 <= Index1 || ind2 <= Index2) && ind1 == i) {
                    if (ind1 <= Index1) {
                        Index1 = parseInt(Index1) + len + 1;
                    }
                    Index2 = parseInt(Index2) + len + 1;
                    bodyDetails1 = bodyDetails1.replace('<', '$');
                    bodyDetails1 = bodyDetails1.replace('>', '$');
                }
            }
            //debugger;
            var in1 = "000000" + Index1;
            var in2 = "000000" + Index2;
            in1 = in1.substring(in1.length - 6);
            in2 = in2.substring(in2.length - 6);
            extralength = "0000" + extralength;
            extralength = extralength.substring(extralength.length - 4);
            var imgtype = type;
            imgtype = "000" + imgtype;
            imgtype = imgtype.substring(imgtype.length - 3);
            var oldstart = start;
            oldstart = "000000" + oldstart;
            oldstart = oldstart.substring(oldstart.length - 6);
            img = "";
            part1 = bodyDetails2.substring(0, Index1);
            part2 = bodyDetails2.substring(Index1, Index2);
            part3 = bodyDetails2.substring(Index2, bodyDetails2.length);
            body = part1 + img + part2 + part3;
            if ((laststart != in1) || (laststart == in1 && isdisplayed == "0")) {
                isdisplayed = "1";
                var start = in1;
                var end = in2;
                var extralength = extralength;
                start = parseInt(start,10);
                //end = parseInt(end, 10) + ((parseInt(extralength, 10)));
                end = parseInt(end, 10) + ((parseInt(0, 10)));
                var part1 = body.substring(0, end);
                var part2 = body.substring(end, body.length);
                body = part1 + '</span' + parseInt(NoSpan, 10) + '>' + part2;
                var part1 = body.substr(0, start);
                var part2 = body.substr(start, body.length);
                if (imgtype == 4) {
                    body = part1 + "<span" + NoSpan + " class='preced'>" + part2;
                }
                if (imgtype == 5) {
                    body = part1 + "<span" + NoSpan + " class='rediod'>" + part2;
                }
                if (imgtype == 2) {
                    body = part1 + "<span" + NoSpan + " class='issuss'>" + part2;
                }
                if (imgtype == 1) {
                    body = part1 + "<span" + NoSpan + " class='factss'>" + part2;
                }
                if (imgtype == 7) {
                    body = part1 + "<span" + NoSpan + " class='highls'>" + part2;
                }
                if (imgtype == 6) {
                    body = part1 + "<span" + NoSpan + " class='orbits'>" + part2;
                }
                if (imgtype == 3) {
                    body = part1 + "<span" + NoSpan + " class='ImpPss'>" + part2;
                }
            }
            else {
                isdisplayed = "0";
            }
            isdisplayed = "0";
            laststart = start;
            for (var i = 1; i <= MaxCount; i++) {
                var StarSpanlength = 22;
                var EndSpanlength = 8;
                if (i > 9) {
                    if (i < 100) {
                        StarSpanlength = StarSpanlength + 1;
                        EndSpanlength = EndSpanlength + 1;
                    } else {
                        StarSpanlength = StarSpanlength + 2;
                        EndSpanlength = EndSpanlength + 2;
                    }
                }
                var SI = '<span' + i;
                var EI = '</span' + i + '>';
                var SI1 = '<span';
                var EI1 = '</span';
                var FindSI = body.indexOf(SI);
                var FindEI = body.indexOf(EI);
                if (FindSI != 0 && FindSI != -1 && FindEI != 0 && FindEI != -1) {
                    var data = body.substring(FindSI + StarSpanlength, FindEI);
                   var SIcount = occurrences(data, SI1);
                   var EIcount = occurrences(data, EI1);
                   if (SIcount != 0 && EIcount == 0) {
                       //alert('1');
                       var mSI = data.indexOf(SI1);
                       var ReplaceData = data.substring(mSI, mSI + StarSpanlength);
                       if (i < 4) {
                           var MiddleSI = body.substring(0, FindEI).indexOf(ReplaceData);
                           var part1 = body.substring(0, MiddleSI);
                           var part2 = EI;
                           var part3 = body.substring(MiddleSI + StarSpanlength, body.length).replace(EI, '');
                           var data2 = body.substring(MiddleSI + StarSpanlength, body.length)
                           var SIcount = occurrences(data2, SI1);
                           var EIcount = occurrences(data2, EI1);
                           if (SIcount == 1 && EIcount > 1) {
                               var meSI = data2.indexOf('<s');
                               var ReplaceData2 = data2.substring(meSI, meSI + StarSpanlength + EndSpanlength);
                               if ((ReplaceData2.indexOf('</')) != 0 && (ReplaceData2.indexOf('</')) != -1) {
                                   //                               var inde = ReplaceData2.indexOf('</');
                                   //                               var rep = ReplaceData2.substring(inde, inde+EndSpanlength);
                                   //                               ReplaceData2 = ReplaceData2.replace(rep, '');
                                   //                               alert(ReplaceData2);
                                   //                               var par1 = data2.substring(0, meSI);
                                   //                               var par2 = rep + ReplaceData2;
                                   //                               var par3 = data2.substring(meSI + StarSpanlength + EndSpanlength, data2.length);
                                   //                               part3 = par1 + par2 + par3;
                               } // end if indexOf('</')

                           } // end if SIcount == 1 && EIcount > 1

                       } else {    // end if (i < 4)
                           //alert(data);
                           if (ReplaceData.length == StarSpanlength) {
                               part1 = body.substring(0, FindEI + EndSpanlength).replace(ReplaceData, '');
                               part3 = body.substring(FindEI + EndSpanlength, body.length);
                               part2 = "";
                           } else {
//                               var ReplaceDataLen = ReplaceData.length;
//                               var RemainLen = 0;
//                               //debugger;
//                               if (ReplaceDataLen < StarSpanlength) {
//                                   RemainLen = StarSpanlength - ReplaceDataLen;
//                               }
////                               alert(RemainLen);
//                               part1 = body.replace(ReplaceData, '');
//                               var data = part1.substring(FindSI + StarSpanlength, FindEI);
//                               alert(data);
//                               part1 = part1.substring(0, FindEI + EndSpanlength);
//                               part3 = body.substring(FindEI + EndSpanlength, body.length);

                               part1 = body.substring(0, FindEI + EndSpanlength).replace(ReplaceData, '');
                               part3 = body.substring(FindEI + EndSpanlength, body.length);
                               part2 = "";
                           }
                       }
                       body = part1 + part2 + ReplaceData + part3;
                   } //end if SIcount != 0 && EIcount == 0
                   else if (SIcount == 0 && EIcount != 0) {
//                       alert('2');
                       var part1; var part2;var part3;
                       if (i < 4) {
                           var mSI = data.indexOf(EI1);
                           var ReplaceData = data.substring(mSI, mSI + EndSpanlength);
                           var MiddleSI = body.substring(0, FindEI).indexOf(ReplaceData);
                           part1 = body.substring(0, MiddleSI + EndSpanlength);
                           var Opentag = EI.replace('/', '').replace('>', '');
                           var FSI = part1.indexOf(Opentag);
                           var Replacestr = part1.substring(FSI, FSI + StarSpanlength);
                           part1 = part1.replace(Replacestr, '');
                            part2 = Replacestr;
                            part3 = body.substring(MiddleSI + EndSpanlength, body.length);
                        } else { // end if (i <4)
                            //alert(data);
                           var mSI = data.indexOf(EI1);
                           var ReplaceData = data.substring(mSI, mSI + EndSpanlength);
                            part1 = body.substring(0, FindEI + EndSpanlength).replace(ReplaceData,'');
                            part2 = ReplaceData;
                            part3 = body.substring(FindEI + EndSpanlength, body.length);
                       }
                       body = part1 + part2 + part3;
                   } else if (SIcount == 1 && EIcount == 2) {
                       //alert('3');
                       //alert(data);
                       var mEI = data.indexOf(EI1);
                       var mSI = data.indexOf(SI1);
                       if (mSI > mEI) {
//                           var ReplaceStr = data.substring(mEI, mEI + EndSpanlength);
//                           var part1 = body.substring(0, FindSI);
//                           var part2 = body.substring(FindSI, body.length).replace(ReplaceStr, '');
//                           body = part1 + ReplaceStr + part2;

                           var ReplaceStr = data.substring(mEI, mEI + EndSpanlength);
                           var meEI = body.indexOf(ReplaceStr);
                           var part1 = body.substring(0, meEI + EndSpanlength);
                           var meSI = part1.indexOf(SI);
                           var ReplaceSpan = part1.substring(meSI, meSI+StarSpanlength);
                           var part1 = part1.replace(ReplaceSpan, '');
                           var part2 = body.substring(meEI + EndSpanlength, body.length);
                           body = part1 + ReplaceSpan + part2;

                       } // end if mSI > mEI
                       else if (mSI < mEI) {
                           //alert(data);
                           var Str = data.substring(mEI + EndSpanlength, FindEI);
                           var StrEI = Str.indexOf(EI1)
                           var ReplaceStr = Str.substring(StrEI, StrEI + EndSpanlength);
                           var part1 = body.substring(0, FindSI);
                           var part2 = body.substring(FindSI, FindEI).replace(ReplaceStr, '');
                           var part3 = body.substring(FindEI, body.length).replace(ReplaceStr, '');
                           body = part1 + ReplaceStr + part2 + part3;
//                           var Str = data.substring(mEI + EndSpanlength, FindEI);
//                           var StrEI = Str.indexOf(EI1)
//                           var ReplaceStr = Str.substring(StrEI, StrEI + EndSpanlength);
//                           var part1 = body.substring(0, FindSI);
//                           var part2 = body.substring(FindSI, FindEI);//.replace(ReplaceStr, '');
//                           var part3 = body.substring(FindEI, body.length);//.replace(ReplaceStr, '');
//                           body = part1 + part2 + part3;
                       }
                   } // end else if (SIcount == 1 && EIcount == 2)
                   else if (SIcount > EIcount) {
                       //alert('4');
                       var OpenTag = body.substring(FindEI, FindEI - 1);
                       if (OpenTag == '<') {
                           var part1 = body.substring(0, FindEI);
                           var part2 = body.substring(FindEI, body.length);
                           var ReplaceStr = part2.substring(0, EndSpanlength);
                           part2 = part2.replace(ReplaceStr, '');
                           var CloseTag = part2.indexOf('>');
                           var datas = insert(part2, CloseTag + 1, ReplaceStr);
                           body = part1 + datas;
                       }
                   }
                   else if (SIcount == 1 && EIcount == 1) {
                   //alert('5');
                       var OpenTag = body.substring(FindEI, FindEI - 1);
                       if (OpenTag == '<') {
                           var part1 = body.substring(0, FindEI);
                           var part2 = body.substring(FindEI, body.length);
                           var ReplaceStr = part2.substring(0, EndSpanlength);
                           part2 = part2.replace(ReplaceStr, '');
                           var CloseTag = part2.indexOf('>');
                           var datas = insert(part2, CloseTag + 1, ReplaceStr);
                           body = part1 + datas;
                       }
                   }
                   else if (SIcount == 0 && EIcount == 0) {
                       var In = data.indexOf('<');
                       if (In != 0 && In != -1) {
                           //alert('6');
                           if ((data.substring(In, In + 4))!='<br>') {
                           var Str = data.substring(In, In + 6);
                           data = data.replace(Str, '');
                           var part1 = body.substring(0, FindSI + StarSpanlength);
                           var part2 = body.substring(FindEI, FindEI + EndSpanlength);
                           var part3 = body.substring(FindEI + EndSpanlength, body.length);
                           body = part1 + data + part2 + Str + part3;
                       } // end if of <br> not equal.
                       } // end if In != 0 && In != -1
                   }  // end if SIcount == 0 && EIcount == 0
                } // end if
            }// end for

           $("#divdisp").html(body);
            imagelength = 2;
            imgtype = parseInt(imgtype);
            return body;
        }

        function occurrences(string, subString, allowOverlapping) {
            string += "";
            subString += "";
            if (subString.length <= 0) return (string.length + 1);
            var n = 0,
        pos = 0,
        step = allowOverlapping ? 1 : subString.length;

            while (true) {
                pos = string.indexOf(subString, pos);
                if (pos >= 0) {
                    ++n;
                    pos += step;
                } else break;
            }
            return n;
        }
        function insert(str, index, value) {
            return str.substr(0, index) + value + str.substr(index);
        }
    </script>
    <script type="text/javascript" language="JavaScript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }

        var browserName = navigator.appName;
        function ShowSummaryPost(ContentId, SummaryText) {
            var ContentId = '<%= Request.QueryString["cId"]%>';
            var OtherUID = $('#intCommentAddedFors').val();
            var AddedBy = document.getElementById('hdnLoginId').value;
            if (OtherUID != '') {
                AddedBy = OtherUID;
            } else { }

            document.getElementById("hdnTagtypeId").value = "8";
            document.getElementById("DivSummary").style.display = 'block';
            var file_type = 'Summary';
            $.ajax({
                contentType: "text/html; charset=utf-8",
                data: "{}",
                url: "handler/GetSummaryDetails.ashx?ContentId=" + ContentId + "&TagType=" + file_type + "&AddedBy=" + AddedBy,
                dataType: "html",
                success: function (data) {
                    if (data.length > 0) {
                        var div = document.getElementById('<%=txtSummary.ClientID%>');
                        div.innerHTML = data;
                    } else {
                        $('#txtSummary').text('');
                        $('#txtSummary').attr('placeholder', 'Write your summary here...');
                    }
                }
            });
            var sid = ContentId;
            var add = AddedBy;
            document.getElementById('<%=hdnSummaryTextPost.ClientID%>').value = SummaryText;
        }

        function ShowSummaryPostLoad(ContentId, SummaryText) {
            var ContentId = '<%= Request.QueryString["cId"]%>';
            var AddedBy = document.getElementById('hdnLoginId').value;
            document.getElementById("hdnTagtypeId").value = "8";
            var file_type = 'Summary';
            $.ajax({
                contentType: "text/html; charset=utf-8",
                data: "{}",
                url: "handler/GetSummaryDetails.ashx?ContentId=" + ContentId + "&TagType=" + file_type + "&AddedBy=" + AddedBy,
                dataType: "html",
                success: function (data) {
                    if (data.length > 0) {
                        var div = document.getElementById('<%=txtSummary.ClientID%>');
                        div.innerHTML = data;
                    } else {
                        $('#txtSummary').attr('placeholder', 'Write your summary here...');
                    }
                }
            });
            var sid = ContentId;
            var add = AddedBy;
            document.getElementById('<%=hdnSummaryTextPost.ClientID%>').value = SummaryText;
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#divdisp').focusout(function () {
                $(this).attr('spellcheck', false);
                forceSpellcheck($(this));
            });
            var canCheck = true;
            function forceSpellcheck($textarea) {
                if (canCheck) {
                    canCheck = false;
                    $textarea.focus();
                    $textarea.attr('spellcheck', false);
                    var characterCount = $textarea.val().length;

                    var selection = window.getSelection();
                    for (var i = 0; i < characterCount; i++) {
                        selection.modify("move", "backward", "character");
                    }
                }
            }
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById("divdisp").onmouseup = function () {
                getSelectionCharacterOffsetsWithin(this);
            };
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                document.getElementById("divdisp").onmouseup = function () {
                     getSelectionCharacterOffsetsWithin(this);
                };
            });
        });
         function getSelectionCharacterOffsetsWithin(element) {
            var txt = '';
            var htmlText = '';
            var startOffset = 0, endOffset = 0;
            document.getElementById('DivMenuBar').style.display = 'none';
            if (typeof window.getSelection != "undefined") {
                var range = window.getSelection().getRangeAt(0);
                var preCaretRange = range.cloneRange();
                preCaretRange.selectNodeContents(element);
                preCaretRange.setEnd(range.startContainer, range.startOffset);
                startOffset = preCaretRange.toString().length;
                endOffset = startOffset + range.toString().length;
                if (range != '') {
                    getSelctedtexts(startOffset, endOffset, range);
                }
            } else if (typeof document.selection != "undefined" &&
               document.selection.type != "Control") {
                var textRange = document.selection.createRange();
                var preCaretTextRange = document.body.createTextRange();
                preCaretTextRange.moveToElementText(element);
                preCaretTextRange.setEndPoint("EndToStart", textRange);
                startOffset = preCaretTextRange.text.length;
                endOffset = startOffset + textRange.text.length;
                if (range != '') {
                    getSelctedtexts(startOffset, endOffset, textRange);
                }
            }
        }  // End function getSelectionCharacterOffsetsWithin(element)

        function getSelctedtexts(startOffset, endOffset, textRange) {
            var txt = '';
            var htmlText = '';
            var range;
            var temp = 1;
            var browserName = navigator.appName;
            htmlText = textRange;
            document.getElementById('hdnSlectedText').value = htmlText;
            document.getElementById('hdnMainSelectedText').value = htmlText;
            if (htmlText != "") {
                document.getElementById("DivSummary").style.display = 'none';
            }
            else {
                document.getElementById('DivMenuBar').style.display = 'none';
            }
            document.getElementById("hdnSelectionCount").value = 1;
            if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1) {
                document.getElementById("hdnhtmlSelectedText").value = htmlText;
                document.getElementById("hdnMarkText").value = textRange;
            }

            if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1)
            { document.getElementById("hdnStartIdx").value = startOffset; } // -1; //newH TMLSel.toLowerCase();//  LcDivBodyText.indexOf(newHTMLSel.toLowerCase());
            var intstart = 0;
            try {
                intstart = parseInt(document.getElementById("hdnStartIdx").value);
            }
            catch (err) {
            }
            if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1)
            { document.getElementById("hdnEndIdx").value = endOffset; }

            if (document.getElementById("hdnStartIdx").value >= 0 && document.getElementById("hdnEndIdx").value >= 0)
                document.getElementById("hdnSelectionCount").value = 0;

            colour = document.getElementById("hdncolour").value;
            ///---------For selection check
            if (txt != "" && htmlText != "") {
                document.getElementById("hdnAttemptselection").value = '2';
            }
            else {
                document.getElementById("hdnAttemptselection").value = '0';
            }

            if (document.selection) {
                            if (document.selection.type == "Text") {
                                document.execCommand("BackColor", false, colour);
                                document.selection.empty();
                                temp = 1;
                            }
                        } else if (window.getSelection) {
                            var sel = window.getSelection();
                            temp = 1;
                            if (!sel.isCollapsed) {
                                range = sel.getRangeAt(0);
                                document.designMode = "on";
                                sel.removeAllRanges();
                                sel.addRange(range);
                                if (txt != "")
                                    document.execCommand("HiliteColor", false, colour);
                                document.designMode = "off";
                                sel.removeAllRanges();
                            }
                        }


           var widthnew = getBrowserWidth();
            var a = parseInt(document.body.offsetHeight);
            var x = parseInt(document.getElementById("hdnX").value) + 460;
            var NewX = parseInt(document.getElementById("hdnX").value);
            if (widthnew < x) {
                NewX = widthnew - 480;
            }
            if (temp == "1")  //document.getElementById("hdncolour").value == "#C6DEFF"
            {
                document.getElementById('DivMenuBar').style.position = "fixed";
                document.getElementById('DivMenuBar').style.zIndex = "100000";
                document.getElementById('DivMenuBar').style.display = 'block';
                $("#DivMenuBar").css({
                    position: "fixed",
                    top: document.getElementById("hdnY").value + "px",
                    //left: -635 + "px",
                    top: 850 + "px"
                }).show();
            }
            if (htmlText != "") {
                var AddedBy = document.getElementById('hdnLoginId').value;
                var CommentAddedFor = document.getElementById('hdnCommentAddedFor').value;
                if (CommentAddedFor != '') {
                    if (AddedBy != CommentAddedFor) {
                        document.getElementById('DivMenuBar').style.display = 'none';
                    } else {
                        document.getElementById('DivMenuBar').style.display = 'block';
                    }
                } else {
                    document.getElementById('DivMenuBar').style.display = 'block';
                }
            }
            else {
                document.getElementById('DivMenuBar').style.display = 'none';
            }

        } // End function getSelctedtexts()

        
// OLD SELECTION CONTENT CODE  6 Nov 2015
//        function highlightSelection(colour) {
//            var txt = '';
//            var htmlText = '';
//            var range;
//            var temp = 0;
//            var browserName = navigator.appName;
//            $(".h2").removeAttr("title");
//            if (window.getSelection) {
//                txt = window.getSelection();
//                if (txt.rangeCount > 0) {
//                    range = txt.getRangeAt(0);
//                    var clonedSelection = range.cloneContents();
//                    var div = document.createElement('div');
//                    div.appendChild(clonedSelection);
//                    htmlText = $(div).text(); //div.innerHTML;
//                    document.getElementById('hdnSlectedText').value = htmlText;
//                    document.getElementById('hdnMainSelectedText').value = htmlText;
//                    if (htmlText != "") {
//                        document.getElementById("DivSummary").style.display = 'none';
//                    }
//                    else {
//                        document.getElementById('DivMenuBar').style.display = 'none';
//                    }
//                }
//            }
//            else if (document.getSelection) {
//                txt = document.getSelection();
//                range = document.selection.createRange();
//                htmlText = range.text; //range.htmlText;
//                document.getElementById('hdnSlectedText').value = htmlText;
//                document.getElementById('txtIssue').value = document.getElementById('hdnSlectedText').value;

//            }
//            else if (document.selection) {
//                txt = document.selection.createRange().Text;
//                range = document.selection.createRange();
//                htmlText = range.text; //range.htmlText;
//                txt = range.text; //range.htmlText;
//                var SlectedText = range.text;
//                document.getElementById('hdnSlectedText').value = SlectedText;
//                document.getElementById('txtIssue').value = document.getElementById('hdnSlectedText').value;
//            }
//            document.getElementById("hdnSelectionCount").value = 1;
//            if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1) {
//                document.getElementById("hdnhtmlSelectedText").value = htmlText;
//                document.getElementById("hdnMarkText").value = txt;
//            }
//            var DivBodyText = $("#divdisp").text();
//            var LcDivBodyText = DivBodyText.toLowerCase();
//            //LcDivBodyText = LcDivBodyText.replace(/(^\s*)|(\s*$)/gi, "");
//            LcDivBodyText = LcDivBodyText.replace(/[ ]{2,}/gi, " ");
//            LcDivBodyText = LcDivBodyText.replace(/\n /, "\n");
//            if (browserName == 'Microsoft Internet Explorer') {
//                var IdIndex = -1, ClassIndex = -1;
//                var ActualId = '', BodyForID = '', ClasName = '';
//                BodyForID = LcDivBodyText;
//                while (BodyForID.indexOf("id=") > 0) {
//                    BodyForID = BodyForID.substring(BodyForID.indexOf("id="));
//                    ActualId = BodyForID.substring(BodyForID.indexOf("id=") + 3, BodyForID.indexOf(" class="));

//                    while (LcDivBodyText.indexOf("id=" + ActualId) > 0 && IdIndex < LcDivBodyText.indexOf("id=" + ActualId) && !ActualId.startsWith("\"")) {
//                        LcDivBodyText = LcDivBodyText.replace("id=" + ActualId, "id=\"" + ActualId + "\"");
//                    }
//                    BodyForID = BodyForID.substring(BodyForID.indexOf("id=") + 3);
//                }
//                BodyForID = LcDivBodyText;

//                while (BodyForID.indexOf("class=") > 0) {
//                    BodyForID = BodyForID.substring(BodyForID.indexOf("class="));
//                    var classdetails = BodyForID.substring(BodyForID.indexOf("class=") + 6, BodyForID.indexOf(">"));
//                    ClasName = BodyForID.substring(BodyForID.indexOf("class=") + 6, BodyForID.indexOf(">"));
//                    ClasName = classdetails.substring(0, 2);
//                    while (LcDivBodyText.indexOf(" class=" + ClasName) > 0 && !ClasName.startsWith("\"")) {
//                        LcDivBodyText = LcDivBodyText.replace(" class=" + ClasName, " class=\"" + ClasName + "\"");
//                    }
//                    if (!ClasName.startsWith("\""))
//                        BodyForID = BodyForID.substring(BodyForID.indexOf("class=") + 7);
//                }
//                BodyForID = LcDivBodyText;
//                while (BodyForID.indexOf("onclick=") > 0) {
//                    BodyForID = BodyForID.substring(BodyForID.indexOf("onclick="));
//                    var Clickdetails = BodyForID.substring(BodyForID.indexOf("onclick=") + 8, BodyForID.indexOf(">"));
//                    ClasName = Clickdetails.substring(0, Clickdetails.indexOf(");") + 2);
//                    while (LcDivBodyText.indexOf("onclick=" + ClasName) > 0 && !ClasName.startsWith("\"")) {
//                        LcDivBodyText = LcDivBodyText.replace("onclick=" + ClasName, "onclick=\"" + ClasName + "\"");
//                    }
//                    BodyForID = BodyForID.substring(BodyForID.indexOf("onclick=") + 9);
//                }
//            }
//            var BodyHtmlRepl = '';
//            BodyHtmlRepl = htmlText;
//            while (BodyHtmlRepl.indexOf("class=") > 0) {
//                BodyHtmlRepl = BodyHtmlRepl.substring(BodyHtmlRepl.indexOf("class="));
//                var classdetails = BodyHtmlRepl.substring(BodyHtmlRepl.indexOf("class=") + 6, BodyHtmlRepl.indexOf(">"));
//                ClasName = BodyHtmlRepl.substring(BodyHtmlRepl.indexOf("class=") + 6, BodyHtmlRepl.indexOf(">"));
//                ClasName = classdetails.substring(0, 2);
//                while (htmlText.indexOf("class=" + ClasName) > 0 && !ClasName.startsWith("\"")) {
//                    htmlText = htmlText.replace("class=" + ClasName, "class=\"" + ClasName + "\"");
//                }
//                if (!ClasName.startsWith("\""))
//                    BodyHtmlRepl = BodyHtmlRepl.substring(BodyForID.indexOf("class=") + 7);
//            }
//            var lchtmlText = htmlText.toLowerCase();
//            var newHTMLSel = "";
//            newHTMLSel = htmlText;
//            var browserName = navigator.appName;
//            var lcNewHTML = newHTMLSel.toLowerCase().trim();
//            //lcNewHTML = lcNewHTML.replace(/(^\s*)|(\s*$)/gi, "");
//            lcNewHTML = lcNewHTML.replace(/[ ]{2,}/gi, " ");
//            lcNewHTML = lcNewHTML.replace("\n", "");
//            lcNewHTML = lcNewHTML.replace("\r\n", "");
//            lcNewHTML = lcNewHTML.replace("\r", "").replace(/\n/g, "").replace(/\r/g, "");
//            while (LcDivBodyText.indexOf("<br> ") > 0) {
//                LcDivBodyText = LcDivBodyText.replace("<br> ", "<br>");
//            }
//            while (LcDivBodyText.indexOf(" <br>") > 0) {
//                LcDivBodyText = LcDivBodyText.replace(" <br>", "<br>");
//            }
//            while (LcDivBodyText.indexOf("\r\n") > 0) {
//                LcDivBodyText = LcDivBodyText.replace("\r\n", "");
//            }
//            if (lchtmlText.indexOf("</span>") >= 0 || lchtmlText.indexOf("<span ") >= 0) {
//                var index = -1;
//                var SelLength = lchtmlText.length;

//                if (lchtmlText.substr(SelLength - 8, SelLength).indexOf("</span>") >= 0 && lchtmlText.substr(0, 7).indexOf("<span ") >= 0) {

//                    index = htmlText.indexOf(">");
//                    newHTMLSel = htmlText.substr(index + 1);
//                    if (browserName != 'Microsoft Internet Explorer') {
//                        while (newHTMLSel.substr(0, 7).indexOf("<span ") >= 0) {
//                            index = newHTMLSel.indexOf(">");
//                            newHTMLSel = newHTMLSel.substr(index + 1);
//                        }
//                    }
//                    else {
//                        while (newHTMLSel.substr(0, 7).indexOf("<SPAN ") >= 0) {
//                            index = newHTMLSel.indexOf(">");
//                            newHTMLSel = newHTMLSel.substr(index + 1);
//                        }
//                    }
//                    var SelLen = newHTMLSel.length;
//                    index = lchtmlText.indexOf("</s");
//                    newHTMLSel = newHTMLSel.substr(0, SelLen - 7);
//                    if (browserName != 'Microsoft Internet Explorer') {
//                        while (newHTMLSel.substr(SelLen - 8, SelLen).indexOf("</span>") >= 0) {
//                            index = newHTMLSel.indexOf("</s");
//                            newHTMLSel = newHTMLSel.substr(0, SelLen - 7);
//                        }
//                    }
//                    else {
//                        while (newHTMLSel.substr(SelLen - 8, SelLen).indexOf("</SPAN>") >= 0) {
//                            index = newHTMLSel.indexOf("</S");
//                            newHTMLSel = newHTMLSel.substr(0, SelLen - 7);
//                        }
//                    }
//                } else if (lchtmlText.substr(0, 7).indexOf("<span ") >= 0) {
//                    index = htmlText.indexOf(">");
//                    newHTMLSel = htmlText.substr(index + 1);
//                    if (browserName != 'Microsoft Internet Explorer') {
//                        while (newHTMLSel.substr(0, 7).indexOf("<span ") >= 0) {
//                            index = newHTMLSel.indexOf(">");
//                            newHTMLSel = newHTMLSel.substr(index + 1);
//                        }
//                    }
//                    else {
//                        while (newHTMLSel.substr(0, 7).indexOf("<SPAN ") >= 0) {
//                            index = newHTMLSel.indexOf(">");
//                            newHTMLSel = newHTMLSel.substr(index + 1);
//                        }
//                    }
//                } else if (lchtmlText.substr(SelLength - 8, SelLength).indexOf("</span>") >= 0) {
//                    index = lchtmlText.indexOf("</s");
//                    newHTMLSel = htmlText.substr(0, index);
//                    if (browserName != 'Microsoft Internet Explorer') {
//                        while (newHTMLSel.substr(SelLength - 8, SelLength).indexOf("</span>") >= 0) {
//                            index = newHTMLSel.indexOf("</s");
//                            newHTMLSel = newHTMLSel.substr(0, index);
//                        }
//                    }
//                    else {
//                        while (newHTMLSel.substr(SelLength - 8, SelLength).indexOf("</SPAN>") >= 0) {
//                            index = newHTMLSel.indexOf("</S");
//                            newHTMLSel = newHTMLSel.substr(0, index);
//                        }
//                    }
//                }
//                else {
//                    newHTMLSel = htmlText;
//                }
//                var LCFinal = newHTMLSel.toLowerCase();
//                LCFinal = LCFinal.replace(/(^\s*)|(\s*$)/gi, "");
//                LCFinal = LCFinal.replace(/[ ]{2,}/gi, " ");
//                LCFinal = LCFinal.replace(/\n /, "\n");
//                while (LCFinal.indexOf("\r\n") > 0) {
//                    LCFinal = LCFinal.replace("\r\n", "");
//                }
//                LCFinal = LCFinal.replace(/(\r\n|\n|\r)/gm, " ");
//                while (LCFinal.indexOf("<br> ") > 0) {
//                    LCFinal = LCFinal.replace("<br> ", "<br>");
//                }
//                while (LCFinal.indexOf(" <br>") > 0) {
//                    LCFinal = LCFinal.replace(" <br>", "<br>");
//                }
//                if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1)
//                { document.getElementById("hdnStartIdx").value = LcDivBodyText.indexOf(LCFinal); } // -1; //newH TMLSel.toLowerCase();//  LcDivBodyText.indexOf(newHTMLSel.toLowerCase());
//                var intstart = 0;
//                try {
//                    intstart = parseInt(document.getElementById("hdnStartIdx").value);
//                }
//                catch (err) {
//                }
//                if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1)
//                { document.getElementById("hdnEndIdx").value = intstart + (LCFinal.length); }
//            }
//            else {
//                while (lcNewHTML.indexOf("\r\n") > 0) {
//                    lcNewHTML = lcNewHTML.replace("\r\n", "");
//                }

//                while (lcNewHTML.indexOf("<br> ") > 0) {
//                    lcNewHTML = lcNewHTML.replace("<br> ", "<br>");
//                }
//                while (lcNewHTML.indexOf(" <br>") > 0) {
//                    lcNewHTML = lcNewHTML.replace(" <br>", "<br>");
//                }
//                if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1)
//                { document.getElementById("hdnStartIdx").value = LcDivBodyText.indexOf(lcNewHTML); }
//                var intstart = 0;
//                try {
//                    intstart = parseInt(document.getElementById("hdnStartIdx").value);
//                }
//                catch (err) {
//                }
//                if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1)
//                { document.getElementById("hdnEndIdx").value = intstart + lcNewHTML.length; }
//            }
//            if (document.getElementById("hdnStartIdx").value >= 0 && document.getElementById("hdnEndIdx").value >= 0)
//                document.getElementById("hdnSelectionCount").value = 0;

//            colour = document.getElementById("hdncolour").value;
//            ///---------For selection check
//            if (txt != "" && htmlText != "") {
//                document.getElementById("hdnAttemptselection").value = '2';
//            }
//            else {
//                document.getElementById("hdnAttemptselection").value = '0';
//            }
//            //////End code Attempt selection 
//            if (document.selection) {
//                // IE case 
//                if (document.selection.type == "Text") {
//                    document.execCommand("BackColor", false, colour);
//                    document.selection.empty();
//                    temp = 1;
//                }
//            } else if (window.getSelection) {
//                var sel = window.getSelection();
//                temp = 1;
//                if (!sel.isCollapsed) {
//                    range = sel.getRangeAt(0);
//                    document.designMode = "on";
//                    sel.removeAllRanges();
//                    sel.addRange(range);
//                    if (txt != "")
//                        document.execCommand("HiliteColor", false, colour);
//                    document.designMode = "off";
//                    sel.removeAllRanges();
//                }
//            }
//            var widthnew = getBrowserWidth();
//            var a = parseInt(document.body.offsetHeight);
//            var x = parseInt(document.getElementById("hdnX").value) + 460;
//            var NewX = parseInt(document.getElementById("hdnX").value);
//            if (widthnew < x) {
//                NewX = widthnew - 480;
//            }
//            if (temp == "1")  //document.getElementById("hdncolour").value == "#C6DEFF"
//            {
//                document.getElementById('DivMenuBar').style.position = "fixed";
//                document.getElementById('DivMenuBar').style.zIndex = "100000";
//                document.getElementById('DivMenuBar').style.display = 'block';
//                $("#DivMenuBar").css({
//                    position: "fixed",
//                    top: document.getElementById("hdnY").value + "px",
//                    //left: -635 + "px",
//                    top: 850 + "px"
//                }).show();
//            }
//            if (htmlText != "") {

//                var AddedBy = document.getElementById('hdnLoginId').value;
//                var CommentAddedFor = document.getElementById('hdnCommentAddedFor').value;
//                if (CommentAddedFor != '') {
//                    if (AddedBy != CommentAddedFor) {
//                        document.getElementById('DivMenuBar').style.display = 'none';
//                    } else {
//                        document.getElementById('DivMenuBar').style.display = 'block';
//                    }
//                } else {
//                    document.getElementById('DivMenuBar').style.display = 'block';
//                }
//            }
//            else {
//                document.getElementById('DivMenuBar').style.display = 'none';
//            }
//        }

        function getBrowserWidth() {
            var myWidth = 0;
            if (typeof (window.innerWidth) == 'number') {
                myWidth = window.innerWidth; //Non-IE
            }
            else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
                myWidth = document.documentElement.clientWidth; //IE 6+ in 'standards compliant mode'
            }
            else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
                myWidth = document.body.clientWidth; //IE 4 compatible
            }
            return myWidth;
        }
        
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            ShowSummaryPostLoad(1, 'kiran');
        });
    </script>
    <script type="text/javascript" language="javascript">
        function Cancel() {
            document.getElementById("DivMenuBar").style.display = 'none';
            document.getElementById("DivSummary").style.display = 'none';
            document.getElementById("PopUpShare").style.display = 'none';
            $('#txtBody').text('');
            $('#txtLink').text('');
            return false;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var ID = "#" + $("#hdnTabId").val();
            $(ID).focus();
        });

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
                $('#DivSummary').center();
                var placeholder = document.getElementById("txtSummary").getAttribute("placeholder");
                var nAgt = navigator.userAgent;
                if ((verOffset = nAgt.indexOf("MSIE")) != -1) {
                    var browserName = "Microsoft Internet Explorer";
                    var fullVersion = nAgt.substring(verOffset + 5);
                }
            });
            function writeSummary() {
                $("#DivSummary").css('display', 'block');
            }
    </script>
    <script type="text/javascript">
        function historyBack() {
            var b = $('#hdnBack').val();
            history.go(-b);
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
                function callchosencss() {
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
        }
        </script>
    <script type="text/javascript">
            function getMultipleValues(ctrlId) {
                $('#tdDepartments').find('label.error').remove();
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
                $('#hdnInvId').val(strSelTexts);
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
            function DownLoadClicks() {
                var data = $("#divdisp").html();
                data = data.replace(/colspan="&quot;&quot;"/gm, "").replace(/colspan="&quot;0&quot;"/gm, "");
                data = data.replace(/rowspan="&quot;&quot;"/gm, "").replace(/rowspan="&quot;0&quot;"/gm, "");
                $("#hdnDivContent").val(data);
            }
            function callDivHandler() {
                var caseId = '<%= Request.QueryString["cId"]%>';
                $.ajax({
                    url: "handler/PDFDownload.ashx?ContentId=" + caseId,
                    dataType: "html",
                    success: function (data) { return true },
                    error: function (data) { return true }
                });
            }
        </script>
    <script type="text/javascript">
            $(document).ready(function () {
                $("#lnkFactTab").one('click', function (event) {
                    document.getElementById('DivMenuBar').style.display = 'none';
                    document.getElementById('divPopp').style.display = 'block';
                });
                $("#lnkIssueTab").one('click', function (event) {
                    document.getElementById('DivMenuBar').style.display = 'none';
                    document.getElementById('divPopp').style.display = 'block';
                });
                $("#lnkImparagrph").one('click', function (event) {
                    document.getElementById('DivMenuBar').style.display = 'none';
                    document.getElementById('divPopp').style.display = 'block';
                });
                $("#lnkPrecedent").one('click', function (event) {
                    document.getElementById('DivMenuBar').style.display = 'none';
                    document.getElementById('divPopp').style.display = 'block';
                });
                $("#lnkDecidendit").one('click', function (event) {
                    document.getElementById('DivMenuBar').style.display = 'none';
                    document.getElementById('divPopp').style.display = 'block';
                });
                $("#lnkOrbite").one('click', function (event) {
                    document.getElementById('DivMenuBar').style.display = 'none';
                    document.getElementById('divPopp').style.display = 'block';
                });
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                    $("#lnkFactTab").one('click', function (event) {
                        document.getElementById('DivMenuBar').style.display = 'none';
                        document.getElementById('divPopp').style.display = 'block';
                    });
                    $("#lnkIssueTab").one('click', function (event) {
                        document.getElementById('DivMenuBar').style.display = 'none';
                        document.getElementById('divPopp').style.display = 'block';
                    });
                    $("#lnkImparagrph").one('click', function (event) {
                        document.getElementById('DivMenuBar').style.display = 'none';
                        document.getElementById('divPopp').style.display = 'block';
                    });
                    $("#lnkPrecedent").one('click', function (event) {
                        document.getElementById('DivMenuBar').style.display = 'none';
                        document.getElementById('divPopp').style.display = 'block';
                    });
                    $("#lnkDecidendit").one('click', function (event) {
                        document.getElementById('DivMenuBar').style.display = 'none';
                        document.getElementById('divPopp').style.display = 'block';
                    });
                    $("#lnkOrbite").one('click', function (event) {
                        document.getElementById('DivMenuBar').style.display = 'none';
                        document.getElementById('divPopp').style.display = 'block';
                    });
                });
            });
            function CallWriteSummary() {
                $('#lnkWriteButton').css("box-shadow", "0px 0px 5px #00B7E5");
            }
            function CallSaveSummaery() {
                $('#BtnSaveSummary').css("box-shadow", "0px 0px 5px #00B7E5");
            }
            function CallSaveComment() {
                document.getElementById('DivMenuBar').style.display = 'none';
                document.getElementById('divPopp').style.display = 'block';
                $('#ctl00_ContentPlaceHolder1_BtnSaveComment').css("box-shadow", "0px 0px 5px #00B7E5");
                if ($('#txtComment').text() == '') {
                    document.getElementById('divPopp').style.display = 'none';
                    setTimeout(
                function () {
                    $('#ctl00_ContentPlaceHolder1_BtnSaveComment').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
                }
        }
        function CallLoadImg() {
            document.getElementById('divPopp').style.display = 'none';
        }
        function callFvrtimg() {
            if ($('#imgFvrt').attr("src") == 'images/gray-tag.png') {
                $("#imgFvrt").attr("src", 'images/red-tag.png');
            } else {
                $("#imgFvrt").attr("src", 'images/gray-tag.png');
            }
        }
        function callCommentDiv() {
            document.getElementById('DivCommentContent').style.display = 'block';
            var divPosition = $('.CommentSections').offset();
            $('html, body').animate({ scrollTop: divPosition.top }, "slow");
        }
        
        </script>
</asp:Content>
