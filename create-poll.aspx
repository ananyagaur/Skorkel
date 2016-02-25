<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="create-poll.aspx.cs" Inherits="create_poll" %>

<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <link href="<%=ResolveUrl("Styles/jquery.datepick.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("js/jquery.datepick.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="cls">
        </div>
        <!--inner container ends-->
        <div class="innerDocumentContainerGroup" style="margin-top: 0px;">
            <div class="innerContainer">
                <!--groups top box starts-->
                <Group:GroupDetails ID="grpDetails" runat="server" />
                <!--groups top box ends-->
                <!--left box starts-->
                <div class="innerGroupBoxnew" style="width: 960px; margin-top: 15px;">
                    <div class="innerContainerLeft containerwidth">
                        <div class="tagContainer containerwidth">
                            <div class="forumsPollsme">
                                <ul style="margin-bottom: 5px;">
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
                                                OnClick="lnkForumTab_Click"></asp:LinkButton>
                                    </li>
                                    <li id="DivUploadTab" runat="server" clientidmode="Static" style="display: block">
                                        <div>
                                            <asp:LinkButton ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static"
                                                OnClick="lnkUploadTab_Click"></asp:LinkButton>
                                        </div>
                                    </li>
                                    <li id="DivPollTab" runat="server" clientidmode="Static">
                                        <div>
                                            <asp:LinkButton ID="lnkPollTab" runat="server" Text="Polls" ClientIDMode="Static"
                                                class="forumstabAcitve" OnClick="lnkPollTab_Click"></asp:LinkButton>
                                        </div>
                                    </li>
                                    <li id="DivEventTab" runat="server" clientidmode="Static" style="display: block">
                                        <div>
                                            <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                                                OnClick="lnkEventTab_Click"></asp:LinkButton></div>
                                    </li>
                                    <li id="DivMemberTab" runat="server" clientidmode="Static" style="display: block">
                                        <div>
                                            <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                                OnClick="lnkEventMemberTab_Click"></asp:LinkButton></div>
                                    </li>
                                </ul>
                                <div class="cls">
                                </div>
                                <div>
                                    <img src="images/recentBlogs.png" align="absmiddle" style="margin-top: 0px; margin-right: 8px;
                                        margin-left: -7%;" />
                                    <asp:LinkButton ID="lnkBack" runat="server" class="recentPoll" OnClick="lnkBack_Click"
                                        Style="margin-top: 1px;" Text="Back"></asp:LinkButton>
                                    <p class="headingCreateNewForum" style="margin-top: -20px;">
                                        <asp:Label ID="lblpolls" runat="server" Text="Create Poll"></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="up1" runat="server">
                        <ContentTemplate>
                            <div style="height: 99%; margin-left: 13px; width: 97%; margin-top: 34px; border: #c6c8ca solid 1px;">
                                <div id="divSuccessPolls" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                                    float: left; width: 500px; padding-top: 0px; position: fixed; margin: -110px 0 0 11%;
                                    z-index: 100; display: none;" clientidmode="Static">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                        <tr>
                                            <td>
                                                <strong>&nbsp;&nbsp;
                                                    <asp:Label ID="lblSuccess" runat="server" Text="Poll created successfully." Font-Size="Small"
                                                        ForeColor="Green"></asp:Label>
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
                                                            <asp:LinkButton ID="lnkSuccessPoll" runat="server" Text="Close" ClientIDMode="Static"
                                                                CausesValidation="false" Style="float: left; text-align: center; text-decoration: none;
                                                                width: 82px; padding-top: 5px; color: #000;" OnClick="lnkSuccessPoll_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="cls">
                                    <br />
                                </div>
                                <br />
                                <div class="createForumBox polls" style="margin: 20px 25px 25px;">
                                    <asp:Label ID="lblMessage" Font-Bold="true" runat="server"></asp:Label>
                                    <br />
                                    <div style="margin-left: 0px; margin-top: 0%; font-size: 19px;">
                                        <asp:TextBox ID="txtQuestion" class="entryForumsTitle" placeholder="Type question here"
                                            Style="width: 886px; font-size: 19px; color: #D7D7D7;" runat="server" Height="30px"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div style="padding-top: 5px; padding-bottom: 8px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuestion"
                                            Display="Dynamic" ValidationGroup="Polls" ErrorMessage="Please enter Question"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                    <div style="margin-left: 0px;">
                                        <textarea id="txtDescription" runat="server" clientidmode="Static" placeholder="Description"
                                            class="entryForumstArea"></textarea>
                                    </div>
                                    <div style="padding-top: 8px; padding-bottom: 8px;">
                                    </div>
                                    <div style="display: block; margin-left: 0px; height: 60px;" id="div1" runat="server"
                                        clientidmode="Static">
                                        <asp:TextBox ID="TextBox1" runat="server" ClientIDMode="Static" class="pollsoption"
                                            value="Option 1" onblur="if(this.value=='') this.value='Option 1';" onfocus="if(this.value=='Option 1') this.value='';"></asp:TextBox>
                                        <a onclick="Removeattach('1');" id="deleteimage1">
                                            <asp:Image ID="crossimg" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" /></a>
                                    </div>
                                    <div style="display: block; margin-left: 0px; margin-top: 0px; height: 60px;" id="div2"
                                        runat="server" clientidmode="Static">
                                        <asp:TextBox ID="TextBox2" ClientIDMode="Static" runat="server" class="pollsoption"
                                            name="txtUsername" value="Option 2" onblur="if(this.value=='') this.value='Option 2';"
                                            onfocus="if(this.value=='Option 2') this.value='';"></asp:TextBox>
                                        <a onclick="Removeattach('2');" id="deleteimage2">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" /></a>
                                    </div>
                                    <div style="display: none; margin-left: 0px; margin-top: 0px; height: 60px;" id="div3"
                                        runat="server" clientidmode="Static">
                                        <asp:TextBox ID="TextBox3" ClientIDMode="Static" runat="server" class="pollsoption"
                                            name="txtUsername" value="Option 3" onblur="if(this.value=='') this.value='Option 3';"
                                            onfocus="if(this.value=='Option 3') this.value='';"></asp:TextBox>
                                        <a onclick="Removeattach('3');" id="deleteimage3">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" /></a>
                                    </div>
                                    <div style="display: none; margin-left: 0px; margin-top: 0px; height: 60px;" id="div4"
                                        runat="server" clientidmode="Static">
                                        <asp:TextBox ID="TextBox4" ClientIDMode="Static" runat="server" class="pollsoption"
                                            name="txtUsername" value="Option 4" onblur="if(this.value=='') this.value='Option 4';"
                                            onfocus="if(this.value=='Option 4') this.value='';"></asp:TextBox>
                                        <a onclick="Removeattach('4');" id="deleteimage4">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" /></a>
                                    </div>
                                    <div style="display: none; margin-left: 0px; margin-top: 0px; height: 60px;" id="div5"
                                        runat="server" clientidmode="Static">
                                        <asp:TextBox ID="TextBox5" ClientIDMode="Static" runat="server" class="pollsoption"
                                            name="txtUsername" value="Option 5" onblur="if(this.value=='') this.value='Option 5';"
                                            onfocus="if(this.value=='Option 5') this.value='';"></asp:TextBox>
                                        <a onclick="Removeattach('5');" id="deleteimage5">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" /></a>
                                    </div>
                                    <div style="display: none; margin-left: 0px; margin-top: 0px; height: 60px;" id="div6"
                                        runat="server" clientidmode="Static">
                                        <asp:TextBox ID="TextBox6" ClientIDMode="Static" runat="server" class="pollsoption"
                                            name="txtUsername" value="Option 6" onblur="if(this.value=='') this.value='Option 6';"
                                            onfocus="if(this.value=='Option 6') this.value='';"></asp:TextBox>
                                        <a onclick="Removeattach('6');" id="deleteimage6">
                                            <asp:Image ID="Image5" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" /></a>
                                    </div>
                                    <div style="display: none; margin-left: 0px; margin-top: 0px; height: 60px;" id="div7"
                                        runat="server" clientidmode="Static">
                                        <asp:TextBox ID="TextBox7" ClientIDMode="Static" runat="server" class="pollsoption"
                                            name="txtUsername" value="Option 7" onblur="if(this.value=='') this.value='Option 7';"
                                            onfocus="if(this.value=='Option 7') this.value='';"></asp:TextBox>
                                        <a onclick="Removeattach('7');" id="deleteimage7">
                                            <asp:Image ID="Image6" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" /></a>
                                    </div>
                                    <div style="display: none; margin-left: 0px; margin-top: 0px; height: 60px;" id="div8"
                                        runat="server" clientidmode="Static">
                                        <asp:TextBox ID="TextBox8" ClientIDMode="Static" runat="server" class="pollsoption"
                                            name="txtUsername" value="Option 8" onblur="if(this.value=='') this.value='Option 8';"
                                            onfocus="if(this.value=='Option 8') this.value='';"></asp:TextBox>
                                        <a onclick="Removeattach('8');" id="deleteimage8">
                                            <asp:Image ID="Image7" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" /></a>
                                    </div>
                                    <div style="display: none; margin-left: 0px; margin-top: 0px; height: 60px;" id="div9"
                                        runat="server" clientidmode="Static">
                                        <asp:TextBox ID="TextBox9" ClientIDMode="Static" runat="server" class="pollsoption"
                                            name="txtUsername" value="Option 9" onblur="if(this.value=='') this.value='Option 9';"
                                            onfocus="if(this.value=='Option 9') this.value='';"></asp:TextBox>
                                        <a onclick="Removeattach('9');" id="deleteimage9">
                                            <asp:Image ID="Image8" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" /></a>
                                    </div>
                                    <div style="display: none; margin-left: 0px; margin-top: 0px; height: 60px;" id="div10"
                                        runat="server" clientidmode="Static">
                                        <asp:TextBox ID="TextBox10" ClientIDMode="Static" runat="server" class="pollsoption"
                                            name="txtUsername" value="Option 10" onblur="if(this.value=='') this.value='Option 10';"
                                            onfocus="if(this.value=='Option 10') this.value='';"></asp:TextBox>
                                        <a onclick="Removeattach('10');" id="deleteimage10">
                                            <asp:Image ID="Image9" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" /></a>
                                    </div>
                                    <div style="padding-top: 8px; padding-bottom: 8px;">
                                    </div>
                                    <div style="float: left; text-align: left; margin-left: 0%; margin-top: -8px;">
                                        <asp:LinkButton ID="Button2" ClientIDMode="Static" runat="server" Style="float: left;
                                            height: 20px; width: 130px;" CssClass="vote" Text="Add Option" OnClientClick="javascript:showfile1();return false;">
                                        </asp:LinkButton>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="cls" style="border-bottom: 1px solid #e7e7e7;">
                                        <br />
                                    </div>
                                    <div class="votingBox" style="border: none; font-size: 16px; color: #9c9c9c;">
                                        <strong>Voting Pattern</strong>
                                        <br />
                                        <br />
                                        <asp:CheckBox ID="rdbSinglePattern" Text="&nbsp; Allow only one option selection"
                                            ClientIDMode="Static" GroupName="Voting Pattern" runat="server" />&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="rdbMultiplePattern" Text="&nbsp; Allow multiple option selection"
                                            ClientIDMode="Static" GroupName="Voting Pattern" runat="server" />
                                    </div>
                                    <div style="padding-top: 8px; padding-bottom: 8px;">
                                    </div>
                                    <div class="cls" style="border-bottom: 1px solid #e7e7e7;">
                                        <br />
                                    </div>
                                    <div class="votingBox" style="border: none; font-size: 16px; color: #9c9c9c;">
                                        <strong>Voting Ends</strong>
                                        <br />
                                        <br />
                                        <asp:CheckBox ID="rdVotingNeverEnds" Text="&nbsp; Never" GroupName="Voting Ends"
                                            ClientIDMode="Static" runat="server" onclick="rdoChanged(this);" /><br />
                                        <div style="margin-top: 20px;">
                                            <asp:CheckBox ID="rdVotingEnds" GroupName="Voting Ends" runat="server" onclick="rdoChanged(this);"
                                                ClientIDMode="Static" />
                                            <asp:DropDownList Enabled="false" ID="ddlTime" runat="server" CssClass="votingTime"
                                                ClientIDMode="Static">
                                                <asp:ListItem Text="1 AM" Value="1:00 AM"></asp:ListItem>
                                                <asp:ListItem Text="2 AM" Value="2:00 AM"></asp:ListItem>
                                                <asp:ListItem Text="3 AM" Value="3:00 AM"></asp:ListItem>
                                                <asp:ListItem Text="4 AM" Value="4:00 AM"></asp:ListItem>
                                                <asp:ListItem Text="5 AM" Value="5:00 AM"></asp:ListItem>
                                                <asp:ListItem Text="6 AM" Value="6:00 AM"></asp:ListItem>
                                                <asp:ListItem Text="7 AM" Value="7:00 AM"></asp:ListItem>
                                                <asp:ListItem Text="8 AM" Value="8:00 AM"></asp:ListItem>
                                                <asp:ListItem Text="9 AM" Value="9:00 AM"></asp:ListItem>
                                                <asp:ListItem Text="10 AM" Value="10:00 AM"></asp:ListItem>
                                                <asp:ListItem Text="11 AM" Value="11:00 AM"></asp:ListItem>
                                                <asp:ListItem Text="12 AM" Value="12:00 AM"></asp:ListItem>
                                                <asp:ListItem Text="1 PM" Value="1:00 PM"></asp:ListItem>
                                                <asp:ListItem Text="2 PM" Value="2:00 PM"></asp:ListItem>
                                                <asp:ListItem Text="3 PM" Value="3:00 PM"></asp:ListItem>
                                                <asp:ListItem Text="4 PM" Value="4:00 PM"></asp:ListItem>
                                                <asp:ListItem Text="5 PM" Value="5:00 PM"></asp:ListItem>
                                                <asp:ListItem Text="6 PM" Value="6:00 PM"></asp:ListItem>
                                                <asp:ListItem Text="7 PM" Value="7:00 PM"></asp:ListItem>
                                                <asp:ListItem Text="8 PM" Value="8:00 PM"></asp:ListItem>
                                                <asp:ListItem Text="9 PM" Value="9:00 PM"></asp:ListItem>
                                                <asp:ListItem Text="10 PM" Value="10:00 PM"></asp:ListItem>
                                                <asp:ListItem Text="11 PM" Value="11:00 PM"></asp:ListItem>
                                                <asp:ListItem Text="12 PM" Value="12:00 PM"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                            <asp:TextBox ID="txtExpireDate" runat="server" ClientIDMode="Static" Width="95px" placeholder="Select Date"
                                                onkeypress="event.returnValue = false;" onkeydown="event.returnValue = false;" MaxLength="50"></asp:TextBox>
                                            <div id="divCal" style="margin-top: -31px; margin-left: 74%;">
                                                <img src="images/calendar1.png" align="absmiddle" class="calendarImg" id="img1" />
                                            </div>
                                            <%-- <div id="divCal" class="GroupPollCal">
                                                <img src="images/calendar1.png" align="absmiddle" class="calendarImg" id="img2" />
                                            </div>--%>
                                        </div>
                                    </div>
                                    <div>
                                        <asp:Label ID="lblDateMessage" ClientIDMode="Static" Style="display: none;" ForeColor="Red"
                                            runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="votingBox polls" style="display: none;">
                                        <strong>Who can Vote</strong>
                                        <br />
                                        <asp:CheckBox ID="rdVoteTypePublic" Text="&nbsp; Public" GroupName="Who can Vote"
                                            ClientIDMode="Static" runat="server" />&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="rdVoteTypeMember" Text="&nbsp; Group members only" GroupName="Who can Vote"
                                            ClientIDMode="Static" runat="server" />
                                    </div>
                                    <div class="cls">
                                    </div>
                                    <p style="float: left; padding-top: 28px; margin-left: 27px;" align="left">
                                        <asp:LinkButton ID="lnkCancelPoll" CausesValidation="false" Style="float: right;
                                            font-size: 16px; padding-top: 15px; text-align: center; text-decoration: none;
                                            width: 85px; color: #9c9c9c;" runat="server" Text="Cancel" OnClick="lnkCancelPoll_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkSavePoll" Text="Create Poll" runat="server" ClientIDMode="Static" OnClientClick="CallSavePoll();"
                                            ValidationGroup="Polls" OnClick="lnkSavePoll_Click" CssClass="vote"></asp:LinkButton>
                                    </p>
                                    <div class="cls">
                                    </div>
                                    <div class="cls">
                                        <br />
                                    </div>
                                </div>
                                <div class="cls">
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnkSavePoll" />
                            <asp:AsyncPostBackTrigger ControlID="Button2" />
                            <asp:AsyncPostBackTrigger ControlID="lnkSuccessPoll" />
                        </Triggers>
                    </asp:UpdatePanel>
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
            function DateControl() {
                $(function () {
                    $('#txtExpireDate').datepick({ showTrigger: '#imgInterview' });
                });
            }
            $(document).ready(function () {
                DateControl();
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                    $('#txtExpireDate').datepick({ showTrigger: '#imgInterview' });
                });
            });
    </script>
    <script type="text/javascript" language="javascript">
        function goBack() {
            window.history.back()
        }
        function showfile1() {
                    $('#Button2').css("box-shadow", "0px 0px 0px #00B7E5");
            if (document.getElementById('div1').style.display == 'none') {
                document.getElementById('div1').style.display = 'block';
                document.getElementById('Button2').value = 'Add More Option';
                return;
            }
            if (document.getElementById('div2').style.display == 'none') {
                document.getElementById('div2').style.display = 'block';
                document.getElementById('Button2').value = 'Add More Option';
                return;
            }
            if (document.getElementById('div3').style.display == 'none') {
                document.getElementById('div3').style.display = 'block';
                document.getElementById('Button2').value = 'Add More Option';
                return;
            }
            if (document.getElementById('div4').style.display == 'none') {
                document.getElementById('div4').style.display = 'block';
                document.getElementById('Button2').value = 'Add More Option';
                return;
            }
            if (document.getElementById('div5').style.display == 'none') {
                document.getElementById('div5').style.display = 'block';
                document.getElementById('Button2').value = 'Add More Option';
                return;
            }
            if (document.getElementById('div6').style.display == 'none') {
                document.getElementById('div6').style.display = 'block';
                document.getElementById('Button2').value = 'Add More Option';
                return;
            }

            if (document.getElementById('div7').style.display == 'none') {
                document.getElementById('div7').style.display = 'block';
                document.getElementById('Button2').value = 'Add More Option';
                return;
            }
            if (document.getElementById('div8').style.display == 'none') {
                document.getElementById('div8').style.display = 'block';
                document.getElementById('Button2').value = 'Add More Option';
                return;
            }
            if (document.getElementById('div9').style.display == 'none') {
                document.getElementById('div9').style.display = 'block';
                document.getElementById('Button2').value = 'Add More Option';
                return;
            }
            if (document.getElementById('div10').style.display == 'none') {
                document.getElementById('div10').style.display = 'block';
                document.getElementById('Button2').style.display = 'none';
                return;
            }
        }
        function showDiv(id) {

            var id1 = 'Div' + id;
            document.getElementById(id1).value = '';
            document.getElementById(id1).style.display = 'block';
        }
        function Removeattach(id) {
            var id2 = '';
            id2 = 'TextBox' + id;
            document.getElementById(id2).value = 'Option ' + id;
            var who = document.getElementById(id2);
            var who2 = who.cloneNode(false);
            who2.onchange = who.onchange;
            who.parentNode.replaceChild(who2, who);
            id2 = 'div' + id;
            if (id2 == 'div1') {

            } else if (id2 == 'div2') {

            } else {
                document.getElementById(id2).style.display = 'none';
            }
            document.getElementById('Button2').style.display = 'block';
            var i = 1;
            var dispval = 0;
            for (i = 1; i <= 10; i = i + 1) {
                id2 = 'div' + i;

                if (document.getElementById(id2).style.display == 'block')
                    dispval = 1;
            }

            if (dispval == 1) {
                document.getElementById('Button2').value = 'Add More Option';
            }
            else {
                document.getElementById('Button2').value = 'Add Option';
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function rdoChanged(rdo) {
            if (rdo.id == '<%= rdVotingNeverEnds.ClientID %>') {
                document.getElementById('<%= ddlTime.ClientID %>').disabled = true;
                document.getElementById('<%= txtExpireDate.ClientID %>').disabled = true;
                $('#lblDateMessage').text('');

            }
            else if (rdo.id == '<%= rdVotingEnds.ClientID %>') {
                document.getElementById('<%= ddlTime.ClientID %>').disabled = false;
                document.getElementById('<%= txtExpireDate.ClientID %>').disabled = false;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtQuestion').keypress(function () {
                $('#txtQuestion').css('font-weight', 'normal');
            });

            $('#txtDescription').keypress(function () {
                $('#txtDescription').css('font-weight', 'normal');
            });
            $('#TextBox1').keypress(function () {
                $('#TextBox1').css('font-weight', 'normal');
            });
            $('#TextBox2').keypress(function () {
                $('#TextBox2').css('font-weight', 'normal');
            });
            $('#TextBox3').keypress(function () {
                $('#TextBox3').css('font-weight', 'normal');
            });
            $('#TextBox4').keypress(function () {
                $('#TextBox4').css('font-weight', 'normal');
            });
            $('#TextBox5').keypress(function () {
                $('#TextBox5').css('font-weight', 'normal');
            });
            $('#TextBox6').keypress(function () {
                $('#TextBox6').css('font-weight', 'normal');
            });
            $('#TextBox7').keypress(function () {
                $('#TextBox7').css('font-weight', 'normal');
            });
            $('#TextBox8').keypress(function () {
                $('#TextBox8').css('font-weight', 'normal');
            });
            $('#TextBox9').keypress(function () {
                $('#TextBox9').css('font-weight', 'normal');
            });
            $('#TextBox10').keypress(function () {
                $('#TextBox10').css('font-weight', 'normal');
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#txtQuestion').keypress(function () {
                    $('#txtQuestion').css('font-weight', 'normal');
                });

                $('#txtDescription').keypress(function () {
                    $('#txtDescription').css('font-weight', 'normal');
                });

                $('#TextBox1').keypress(function () {
                    $('#TextBox1').css('font-weight', 'normal');
                });
                $('#TextBox2').keypress(function () {
                    $('#TextBox2').css('font-weight', 'normal');
                });
                $('#TextBox3').keypress(function () {
                    $('#TextBox3').css('font-weight', 'normal');
                });
                $('#TextBox4').keypress(function () {
                    $('#TextBox4').css('font-weight', 'normal');
                });
                $('#TextBox5').keypress(function () {
                    $('#TextBox5').css('font-weight', 'normal');
                });
                $('#TextBox6').keypress(function () {
                    $('#TextBox6').css('font-weight', 'normal');
                });
                $('#TextBox7').keypress(function () {
                    $('#TextBox7').css('font-weight', 'normal');
                });
                $('#TextBox8').keypress(function () {
                    $('#TextBox8').css('font-weight', 'normal');
                });
                $('#TextBox9').keypress(function () {
                    $('#TextBox9').css('font-weight', 'normal');
                });
                $('#TextBox10').keypress(function () {
                    $('#TextBox10').css('font-weight', 'normal');
                });
            });
            if (navigator.userAgent.indexOf('Mozilla') != -1 && navigator.userAgent.indexOf('Chrome') == -1) {
                $('#divCal').css('margin-left', '74%');
            }

        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#rdbSinglePattern').prop('checked', true);
            $('#rdVotingNeverEnds').prop('checked', true);
            $('#rdVoteTypePublic').prop('checked', true);

            if ($('#rdVotingEnds').is(':checked') == true) {
                $('#rdVotingNeverEnds').prop('checked', false);
                document.getElementById('<%= ddlTime.ClientID %>').disabled = false;
                document.getElementById('<%= txtExpireDate.ClientID %>').disabled = false;
            }

            if ($('#rdbMultiplePattern').is(':checked') == true) {
                $('#rdbSinglePattern').prop('checked', false);
            }

            $('#rdbSinglePattern').click(function () {
                $('#rdbSinglePattern').prop('checked', true);
                $('#rdbMultiplePattern').prop('checked', false);

            });

            $('#rdbMultiplePattern').click(function () {
                $('#rdbSinglePattern').prop('checked', false);
                $('#rdbMultiplePattern').prop('checked', true);

            });
            $('#rdVotingNeverEnds').click(function () {
                $('#rdVotingNeverEnds').prop('checked', true);
                $('#rdVotingEnds').prop('checked', false);

            });

            $('#rdVotingEnds').click(function () {
                $('#rdVotingEnds').prop('checked', true);
                $('#rdVotingNeverEnds').prop('checked', false);

            });
            $('#rdVoteTypePublic').click(function () {
                $('#rdVoteTypePublic').prop('checked', true);
                $('#rdVoteTypeMember').prop('checked', false);

            });

            $('#rdVoteTypeMember').click(function () {
                $('#rdVoteTypeMember').prop('checked', true);
                $('#rdVoteTypePublic').prop('checked', false);

            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#rdbSinglePattern').prop('checked', true);
                $('#rdVotingNeverEnds').prop('checked', true);
                $('#rdVoteTypePublic').prop('checked', true);

                if ($('#rdVotingEnds').is(':checked') == true) {
                    $('#rdVotingNeverEnds').prop('checked', false);
                    document.getElementById('<%= ddlTime.ClientID %>').disabled = false;
                    document.getElementById('<%= txtExpireDate.ClientID %>').disabled = false;
                }

                if ($('#rdbMultiplePattern').is(':checked') == true) {
                    $('#rdbSinglePattern').prop('checked', false);
                }
                $('#rdbSinglePattern').click(function () {
                    $('#rdbSinglePattern').prop('checked', true);
                    $('#rdbMultiplePattern').prop('checked', false);

                });

                $('#rdbMultiplePattern').click(function () {
                    $('#rdbSinglePattern').prop('checked', false);
                    $('#rdbMultiplePattern').prop('checked', true);

                });

                $('#rdVotingNeverEnds').click(function () {
                    $('#rdVotingNeverEnds').prop('checked', true);
                    $('#rdVotingEnds').prop('checked', false);

                });

                $('#rdVotingEnds').click(function () {
                    $('#rdVotingEnds').prop('checked', true);
                    $('#rdVotingNeverEnds').prop('checked', false);

                });
                $('#rdVoteTypePublic').click(function () {
                    $('#rdVoteTypePublic').prop('checked', true);
                    $('#rdVoteTypeMember').prop('checked', false);

                });
                $('#rdVoteTypeMember').click(function () {
                    $('#rdVoteTypeMember').prop('checked', true);
                    $('#rdVoteTypePublic').prop('checked', false);

                });
            });

        });
        function calldivSucess() {
            $('#divSuccessPolls').css('display', 'block');
        }
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
        function CallSavePoll() {
            $('#lnkSavePoll').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtQuestion').text() == '') {
                setTimeout(
                function () {
                    $('#lnkSavePoll').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
        }
        $(document).ready(function () {
            $('#txtQuestion').keypress(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
        });
    </script>
</asp:Content>
