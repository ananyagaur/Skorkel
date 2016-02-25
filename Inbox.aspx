<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="Inbox.aspx.cs" Inherits="Inbox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
   </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--heading ends-->
    <div class="cls">
    </div>
    <!--inner container starts-->
    <div class="cls">
    </div>
    <!--inner container ends-->
    <div class="container">
        <div class="innerDocumentContainer" id="dvInBox" runat="server" style="background: #fff;
            float: left; height: auto;">
            <div class="NmiddleContainer" style="width: 805px;">
            <asp:UpdatePanel ID="upmains" runat="server"><ContentTemplate> 
                <div id="dvPopup" runat="server" class="SelectBroser" style="border: 20px solid rgba(0,0,0,0.5);
                    float: left; width: 40%; position: fixed; z-index: 100; display: none" clientidmode="Static">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="popHeading">
                                            Message
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
                                <p id="MessTo" runat="server" style="display: none; margin: 0px 0px -5px 65px;" clientidmode="Static">
                                    <strong>To:</strong>
                                    <asp:Label ID="lblTo" Style="padding-left: 3px; color: #666666; margin-left: -3px;"
                                        runat="server"></asp:Label>
                                </p>
                                <p id="MessFrom" runat="server" clientidmode="Static">
                                    <strong style="padding-left: 47px;">From:</strong>
                                    <asp:Label ID="lblFrom" Style="padding-left: 0px; color: #666666; margin-left: -2px;"
                                        runat="server"></asp:Label></p>
                                <p id="pMessageSubBox" style="display: none;" clientidmode="Static">
                                    <asp:TextBox ID="txtSubject" ClientIDMode="Static" CssClass="MessageSubjectInbox" placeholder="Subject"
                                       runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubject"
                                        Style="margin-left: 6%;" ErrorMessage="Please enter Subject."
                                        ValidationGroup="Mess"></asp:RequiredFieldValidator>
                                </p>
                                <p id="pMessageBox" style="display: none; margin: -0px 2px 0px 0px;" clientidmode="Static">
                                    <textarea id="txtMessage" runat="server" clientidmode="Static" cols="20" rows="2" placeholder="Message"
                                        class="MessageBodyInbox" style="margin: -10px 0px 16px 32px;"></textarea>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        Style="margin-left: -407px;" ControlToValidate="txtMessage" Display="Dynamic"
                                        ValidationGroup="Mess" ErrorMessage="Please enter message." ForeColor="Red"></asp:RequiredFieldValidator>
                                    <%-- </strong>--%>
                                </p>
                                <p id="PlblMess" style="padding-left: 22px;">
                                    <strong>Message:</strong>
                                    <asp:Label ID="lblMessesage" Style="padding-left: 0px; margin-left: -3px;color: #666666;" ClientIDMode="Static"
                                        runat="server"></asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td class="popBgLineGray" style="margin-top: 18px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-right: 40px;">
                                <table width="100" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <a id="aReplyMess" style="cursor: pointer; text-decoration: none ! important;" clinetidmode="Static"
                                                runat="server" onclick="ReplyMess();" class="connectLink">&nbsp;&nbsp;Reply&nbsp;&nbsp;&nbsp;</a>
                                            <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Send"
                                                Style="display: none" ValidationGroup="Mess" CssClass="joinBtn" OnClick="lnkPopupOK_Click"></asp:LinkButton>
                                        </td>
                                        <td>
                                            <a id="aClose" clinetidmode="Static" style="float: left; text-align: center; text-decoration: none ! important;
                                                width: 82px; color: #000; padding-bottom: 5px; cursor: pointer" onclick="PopClose();">
                                                Cancel</a> <a id="aCancel" clinetidmode="Static" style="float: left; text-align: center;
                                                    text-decoration: none ! important; width: 82px; color: #000; padding-bottom: 5px;
                                                    display: none; cursor: pointer" onclick="PopClose();">Cancel</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divSuccessMess" runat="server" class="SelectBroser" style="border: 20px solid rgba(0,0,0,0.5);
                    margin: 6% 0 0 13%; float: left; width: 500px; padding-top: 0px; position: fixed;
                    z-index: 100; display: none;" clientidmode="Static">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                        <tr>
                            <td>
                                <strong>&nbsp;&nbsp;
                                    <asp:Label ID="lblSuccess" ForeColor="Green" runat="server" Text="" Font-Size="Small"></asp:Label>
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
                                                text-decoration: none; width: 82px; padding-top: 5px; color: #000; cursor: pointer"
                                                onclick="PopClose();">Close </a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <table width="115%" style="border-style: solid; border-width: 1px;">
                    <tr>
                        <td id="divmainHeader" valign="top" style="padding-left: 10px; background-color: #EAEFF5;
                            border-right-style: solid; border-width: 1px;">
                            <div id="menu8" style="vertical-align: top; display: block; float: left;">
                                <table id="AdminForEmployeeMenu" runat="server" width="100%" border="0" cellspacing="4"
                                    cellpadding="4">
                                    <tr>
                                        <td width="9%" align="center">
                                            <img src="images/inboxImage.png" width="15" height="15">
                                        </td>
                                        <td width="91%" align="left" style="font-size: 17px;">
                                            <asp:LinkButton ID="lnkInbox" OnClick="lnkInbox_Click" Style="text-decoration: none ! important;
                                                font-size: medium" runat="server">
                                                Inbox(<asp:Label ID="lblTotalInbox" Style="font-size: medium" runat="server"></asp:Label>)</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr id="trEmpAttendance" runat="server">
                                        <td width="9%" align="center">
                                            <img src="images/sentMail.png" width="20" height="20">
                                        </td>
                                        <td width="91%" align="left" style="font-size: 17px;">
                                            <asp:LinkButton ID="lnkOutBox" OnClick="lnkOutBox_Click" Style="text-decoration: none ! important;
                                                font-size: medium" runat="server">
                                                        Sent<%--(<asp:Label ID="lblSendBox" Style="font-size: medium" runat="server"></asp:Label>)--%></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                        </td>
                        <td width="86%" id="Td1" valign="top">
                            <div style="padding-left: 20px; padding-bottom: 10px;">
                                <asp:ListView ID="lstInbox" runat="server" OnItemDataBound="lstInbox_ItemDataBound"
                                    OnItemCommand="lstInbox_ItemCommand">
                                    <LayoutTemplate>
                                        <table width="100%" cellspacing="0" cellpadding="4">
                                            <tr id="Tr1" runat="server" style="border-bottom-style: solid; border-width: 1px;">
                                                <th align="left" style="border-bottom-style: solid; border-width: 1px;" width="60%">
                                                    Subject
                                                </th>
                                                <th id="thFrom" clientidmode="Static" runat="server" align="left" style="border-bottom-style: solid;
                                                    border-width: 1px;" width="25%">
                                                    <asp:Label ID="lblinboxFrom" runat="server" Text="From"></asp:Label>
                                                </th>
                                                <th id="thTo" clientidmode="Static" runat="server" align="left" style="border-bottom-style: solid;
                                                    border-width: 1px;" width="100%">
                                                    <asp:Label ID="lblinboxTo" runat="server" Text="To"></asp:Label>
                                                </th>
                                                <th align="left" style="border-bottom-style: solid; border-width: 1px;" width="15%">
                                                    Date
                                                </th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <table width="100%" cellspacing="0" cellpadding="4" style="border-bottom-style: solid;
                                            border-width: 1px; margin-left: 3px;">
                                            <tr>
                                                <asp:HiddenField ID="hdnGrpId" runat="server" Value='<%#Eval("intGroupId") %>' />
                                                <asp:HiddenField ID="hdnInviUserID" runat="server" Value='<%#Eval("striInvitedUserId") %>' />
                                                <asp:HiddenField ID="hdnAddedBy" runat="server" Value='<%#Eval("intAddedBy") %>' />
                                                <asp:HiddenField ID="hdnMaxCount" runat="server" Value='<%#Eval("Maxcount") %>' />
                                                <asp:HiddenField ID="hdnMessId" runat="server" Value='<%#Eval("intMessageId") %>' />
                                                <asp:HiddenField ID="hdnIsRead" runat="server" Value='<%#Eval("IsRead") %>' />
                                                <asp:HiddenField ID="hdnIsReadSent" runat="server" Value='<%#Eval("IsReadSent") %>' />
                                                <asp:HiddenField ID="hdnSubjectmsg" runat="server" Value='<%#Eval("Subjectmsg") %>' />
                                                <asp:HiddenField ID="hdnstrTotalGrpMemberID" runat="server" Value='<%#Eval("strTotalGrpMemberID") %>' />

                                                <asp:UpdatePanel ID="upinbox" runat="server"><ContentTemplate> 
                                                <td align="left" style="width: 60%">
                                                    <div class="txtst">
                                                        <asp:LinkButton ID="lnksubject" Style="text-decoration: none;" runat="server" ClientIDMode="Static"
                                                            CommandName="GetMessageSetails" Text='<%#Eval("strSubject") %>'></asp:LinkButton>
                                                    </div>
                                                </td>
                                                <td align="left" style="width: 25%">
                                                    <asp:LinkButton ID="lblSenderName" Style="text-decoration: none;" runat="server"
                                                        ClientIDMode="Static" CommandName="GetMessageSetails" Text='<%#Eval("NAME") %>'></asp:LinkButton>
                                                </td>
                                                <td align="left" style="width: 15%">
                                                    <asp:LinkButton ID="lblDate" Style="text-decoration: none;" runat="server" ClientIDMode="Static"
                                                        CommandName="GetMessageSetails" Text='<%#Eval("dtAddedOn") %>'></asp:LinkButton>
                                                </td>
                                               </ContentTemplate>
                                                <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="lnksubject" />
                                                <asp:AsyncPostBackTrigger ControlID="lblSenderName" />
                                                <asp:AsyncPostBackTrigger ControlID="lblDate" />
                                                </Triggers>
                                                 </asp:UpdatePanel>
                                            
                                            </tr>
                                            <tr>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblMessage" ForeColor="Red" Text="No Message Found." runat="server"></asp:Label>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="cleaner">
                </div>
                <div id="dvPage" runat="server" class="pagingBlog" align="center" style="margin: 0px 40%;" clientidmode="Static">
                    <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click" ClientIDMode="Static">
                        <img id="imgPaging" runat="server" src="images/backpaging.jpg" clientidmode="Static"
                            class="opt" style="display: none;" /></asp:LinkButton>
                    <asp:LinkButton ID="lnkprev" runat="server" OnClientClick="return false;" ClientIDMode="Static"
                        Style="display: none;">
                        <img id="img2" runat="server" src="images/backpaging.jpg" clientidmode="Static" class="opt" /></asp:LinkButton>
                    <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand"
                        OnItemDataBound="rptDvPage_ItemDataBound">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPageLink" runat="server" ClientIDMode="Static" CommandName="PageLink"
                                Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click" ClientIDMode="Static"><img src="images/nextpaging.jpg" /></asp:LinkButton>
                    <asp:LinkButton ID="lnkNextshow" runat="server" OnClientClick="return false;" Style="display: none;"
                        ClientIDMode="Static"><img class="opt" src="images/nextpaging.jpg" /></asp:LinkButton>
                    <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="HiddenField2" runat="server" ClientIDMode="Static" Value="1" />
                    <asp:HiddenField ID="hdnEndPage" runat="server" ClientIDMode="Static" />
                </div>
                <asp:UpdateProgress id="updateProgress" runat="server">
                            <ProgressTemplate>
                              <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999;  opacity: 0.7;">
                                 <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/Loadgif.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;margin-top:20%;" class="divProgress" />
                              </div>
                            </ProgressTemplate>
                            </asp:UpdateProgress>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkPopupOK" />
                </Triggers>
                 </asp:UpdatePanel>
            </div>
            <!--middle container ends-->
            <div class="adv" style="display: none">
                <!--right side banner starts-->
                <img src="images/adv1.jpg" /><br />
                <br />
                <img src="images/adv2.jpg" />
                <!--right side banner ends-->
            </div>
            <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
            <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
        </div>
    </div>
    <div class="cls">
    </div>
   <script type="text/javascript">
       $(document).ready(function () {
           if ($('#hdnCurrentPage').val() == '1') {
               $('#imgPaging').css("display", "none");
               $('#lnkprev').css("display", "block");
           } else {
               $('#imgPaging').css("display", "block");
               $('#lnkprev').css("display", "none");
           }
           if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
               $('#lnkNextshow').css("display", "block");
               $('#lnkNext').css("display", "none");
           }
           $('#lnkPageLink').click(function () {
               $(this).removeClass('Paging');
               $(this).addClass('ahover');
           });
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           prm.add_endRequest(function () {
               if ($('#hdnCurrentPage').val() == '1') {
                   $('#imgPaging').css("display", "none");
                   $('#lnkprev').css("display", "block");
               } else {
                   $('#imgPaging').css("display", "block");
                   $('#lnkprev').css("display", "none");
               }
               if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                   $('#lnkNextshow').css("display", "block");
                   $('#lnkNext').css("display", "none");
               }
               $('#lnkPageLink').click(function () {
                   $(this).removeClass('Paging');
                   $(this).addClass('ahover');
               });
           });
       });
    </script>
    <script type="text/javascript">
        function PopClose() {
            document.getElementById("dvPopup").style.display = 'none';
            document.getElementById("divSuccessMess").style.display = 'none';
        }
        function ReplyMess() {
            document.getElementById("MessTo").style.display = 'block';
            document.getElementById("MessFrom").style.display = 'none';
            document.getElementById("lblMessesage").style.display = 'none';
            document.getElementById("pMessageBox").style.display = 'block';
            document.getElementById("pMessageSubBox").style.display = 'block';
            document.getElementById("PlblMess").style.display = 'none';
            document.getElementById("aCancel").style.display = 'block';
            document.getElementById("aClose").style.display = 'none';
            document.getElementById("lnkPopupOK").style.display = 'block';
            document.getElementById("ctl00_ContentPlaceHolder1_aReplyMess").style.display = 'none';
            $('#MessTo').css({ 'margin': '0px 0px -5px 30px' });
        } 
    </script>
</asp:Content>
