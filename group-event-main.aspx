<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="group-event-main.aspx.cs" Inherits="group_event_main" %>

<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <style type="text/css">
        .myCalendar .myCalendarNextPrev:nth-child(1) a
        {
            color: #FFFFFF !important;
            margin-left: 60px;
            margin-top: 30px;
        }
        .myCalendar .myCalendarNextPrev:nth-child(3) a
        {
            color: #FFFFFF !important;
            margin-right: 60px;
        }
        .myCalendar .myCalendarTitle
        {
            font-weight: normal;
            height: 20px;
            line-height: 20px;
        }
    </style>
    <script src="<%=ResolveUrl("js/jquery.datepick.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <asp:UpdatePanel ID="upmains" runat="server">
            <ContentTemplate>
                <div class="cls">
                </div>
                <div class="innerDocumentContainerGroup" style="margin-top: 0px;">
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
                                                        Style="float: left; text-align: center; text-decoration: none; width: 82px; margin-top: -5px;
                                                        color: #000; cursor: pointer;" OnClientClick="javascript:divCancels();return false;"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="innerContainer">
                        <!--groups top box starts-->
                        <asp:UpdatePanel ID="upgrdetails" runat="server">
                            <ContentTemplate>
                                <Group:GroupDetails ID="grpDetails" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!--groups top box ends-->
                        <!--left box starts-->
                        <div class="innerGroupBoxnew">
                            <div class="innerContainerLeft" style="width: 900px">
                                <div class="tagContainer" style="width: 900px">
                                    <div class="forumsTabs" style="margin: 3px 0px 0; height: 77px;">
                                        <ul style="margin-bottom: 5px;">
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
                                            <li id="DivUploadTab" runat="server" clientidmode="Static" style="display: block;">
                                                <div>
                                                    <asp:LinkButton ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static"
                                                        OnClick="lnkUploadTab_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li id="DivPollTab" runat="server" clientidmode="Static" style="display: block;">
                                                <div>
                                                    <asp:LinkButton ID="lnkPollTab" runat="server" Text="Polls" ClientIDMode="Static"
                                                        OnClick="lnkPollTab_Click"></asp:LinkButton>
                                                </div>
                                            </li>
                                            <li id="DivEventTab" runat="server" clientidmode="Static">
                                                <div>
                                                    <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                                                        class="forumstabAcitve" OnClick="lnkEventTab_Click"></asp:LinkButton></div>
                                            </li>
                                            <li id="DivMemberTab" runat="server" clientidmode="Static" style="display: block;">
                                                <div>
                                                    <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                                        OnClick="lnkEventMemberTab_Click"></asp:LinkButton></div>
                                            </li>
                                        </ul>
                                        <div class="cls">
                                        </div>
                                        <p class="headingCreateEvent" style="margin-top: -2px; margin: 0px 0px 0px 42%; float: left;">
                                            Create Event</p>
                                        <div class="cls">
                                        </div>
                                    </div>
                                </div>
                                <div style="height: auto; width: 104%; margin-top: 35px; border: 1px solid #e7e7e7;
                                    float: left;">
                                    <div style="margin-left: 1%; margin-top: 3%;">
                                        <div class="cls">
                                            <br />
                                        </div>
                                        <div id="divSuccessEvents" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                                            width: 39%; z-index: 100; display: none;" clientidmode="Static">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                                <tr>
                                                    <td>
                                                        <strong>&nbsp;&nbsp;
                                                            <asp:Label ID="Label2" runat="server" Text="Event Deleted Successfully." ForeColor="Green"
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
                                                                        text-decoration: none; width: 82px; padding-top: 5px; color: #000;" onclick="javascript:messageCloseEvent();return false;">
                                                                        Close </a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="divCancelEvent" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                                            width: 39%; z-index: 100; display: none;" clientidmode="Static">
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
                                                                    <a href="#" clientidmode="Static" causesvalidation="false" style="float: left; text-align: center;
                                                                        text-decoration: none; width: 82px; padding-top: 5px; padding-bottom: 8px; color: #000;"
                                                                        onclick="Cancel();">Cancel </a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <!--calendar starts-->
                                        <div class="cls">
                                        </div>
                                        <div id="divSuccessEvent" runat="server" class="divSucessevent" clientidmode="Static">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                                <tr>
                                                    <td>
                                                        <strong>&nbsp;&nbsp;
                                                            <asp:Label ID="lblSuccess" runat="server" Text="Event Created Successfully." ForeColor="Green"
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
                                                                    <asp:LinkButton ID="lnksucessClose" runat="server" ClientIDMode="Static" CausesValidation="false"
                                                                        Style="float: left; text-align: center; text-decoration: none; width: 82px; padding-top: 5px;
                                                                        color: #000;" OnClick="lnksucessClose_Click">
                                                                Close </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="leftCreateEvents">
                                            <asp:TextBox ID="txtTitle" ClientIDMode="Static" runat="server" placeholder="Title"
                                                class="eventTitleField" Style="font-size: 16px; color: #D7D7D7;" name="txtUsername"></asp:TextBox>
                                            <div style="padding-bottom: 5px;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                                                    Display="Dynamic" ValidationGroup="Events" ErrorMessage="Please enter Title"
                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <textarea id="txtDescription" clientidmode="Static" runat="server" placeholder="Description"
                                                style="font-family: Arial; font-size: 16px !important; color: #D7D7D7;" class="eventDescriptionField"></textarea>
                                        </div>
                                        <div class="rightCreateEvents">
                                            <strong>Events Calendar</strong>
                                            <asp:UpdatePanel ID="updateEvents" runat="server">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="CalendarEvent" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:Calendar ID="CalendarEvent" runat="server" BackColor="White" BorderColor="#e7e7e7"
                                                        BorderWidth="1px" CellPadding="1" DayNameFormat="Short" Font-Names="Verdana"
                                                        CssClass="myCalendar" Font-Size="8pt" ForeColor="#003399" Width="100%" FirstDayOfWeek="Monday"
                                                        Height="90%" OnDayRender="CalendarEvent_DayRender" OnSelectionChanged="CalendarEvent_SelectionChanged"
                                                        NextPrevStyle-BorderStyle="None">
                                                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                        <SelectorStyle BackColor="#00b6bd" ForeColor="#fff" />
                                                        <WeekendDayStyle BackColor="#ffffff" />
                                                        <OtherMonthDayStyle ForeColor="#999999" />
                                                        <TodayDayStyle BackColor="LightCyan" ForeColor="#003399" />
                                                        <NextPrevStyle Font-Size="15pt" ForeColor="#ffffff" Font-Underline="false" Width="5%"
                                                            VerticalAlign="Middle" HorizontalAlign="Center" CssClass="myCalendarNextPrev" />
                                                        <DayHeaderStyle BackColor="#00b6bd" ForeColor="#ffffff" Font-Bold="true" BorderWidth="1px"
                                                            VerticalAlign="Middle" Height="25px" HorizontalAlign="Center" BorderColor="#e7e7e7" />
                                                        <TitleStyle BackColor="#00b6bd" BorderColor="#e7e7e7" BorderWidth="0px" CssClass="myCalendarTitle"
                                                            Width="5%" Font-Size="16pt" ForeColor="#ffffff" Height="30px" VerticalAlign="Middle"
                                                            HorizontalAlign="Center" />
                                                        <DayStyle Height="40px" VerticalAlign="Top" HorizontalAlign="Right" BorderColor="#e7e7e7"
                                                            BorderWidth="1px" />
                                                    </asp:Calendar>
                                                    <div id="dvPopup" runat="server" clientidmode="Static" style="border: 20px solid rgba(0,0,0,0.5);
                                                        float: left; width: 500px; position: fixed; margin: -700px 0 0 -253px; z-index: 100;
                                                        display: none">
                                                        <asp:Panel ID="ShowTitle" runat="server" Visible="false">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                                                <tr>
                                                                    <td>
                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                            <asp:ListView ID="lstViewEvents" runat="server" OnItemCommand="lstViewEvents_ItemCommand">
                                                                                <LayoutTemplate>
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <p class="center">
                                                                                                <strong>&nbsp; Select Option</strong></p>
                                                                                        </tr>
                                                                                        <tr id="itemPlaceHolder" runat="server">
                                                                                        </tr>
                                                                                        <tr>
                                                                                            &nbsp;&nbsp; <a clientidmode="Static" causesvalidation="false" style="float: right;
                                                                                                text-align: center; text-decoration: none !important; width: 82px; cursor: pointer"
                                                                                                onclick="javascript:CloseCalPopup();">Cancel </a>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <asp:Label ID="textLine" runat="server" CssClass="popBgLineGray"></asp:Label>
                                                                                        </tr>
                                                                                    </table>
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    &nbsp;
                                                                                    <asp:LinkButton ID="lnkEvent" runat="server" ClientIDMode="Static" CssClass="SrNo"
                                                                                        CommandName="EventDetails" Text='<%# "Event " + Eval("RowNumber") +"&nbsp;&nbsp;&nbsp;"%>'></asp:LinkButton>
                                                                                    <asp:HiddenField ID="hdnEventId" Value='<%# Eval("intGrpEventtId") %>' runat="server"
                                                                                        ClientIDMode="Static" />
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="ShowDetails" runat="server" Visible="false">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                                                <tr>
                                                                    <td>
                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td id="tdPopupColor" runat="server" style="width: 25px; height: 40px; float: right;
                                                                                    margin-top: 3px;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="vertical-align: middle" width="94%" class="popHeading">
                                                                                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
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
                                                                        <p style="padding-left: 7px; padding-right: 3px;">
                                                                            &nbsp;
                                                                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                                                        </p>
                                                                        <p>
                                                                            &nbsp; <strong>Date:</strong>
                                                                            <asp:Label ID="lblDate" runat="server"></asp:Label></p>
                                                                        &nbsp; <strong>Venue:</strong>
                                                                        <asp:Label ID="lblVenue" runat="server"></asp:Label>
                                                                        </p>
                                                                        <p>
                                                                            &nbsp; <strong>Contact:</strong>
                                                                            <asp:Label ID="lblContactPerson" runat="server"></asp:Label>
                                                                            - +91-<asp:Label ID="lblContactNumber" runat="server"></asp:Label></p>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="popBgLineGray">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <table width="100" border="0" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <a clientidmode="Static" causesvalidation="false" style="background: url(images/connect-btn.png) left -38px;
                                                                                        color: #FFFFFF; float: left; height: 22px; margin: 15px 0 0; padding: 5px 0;
                                                                                        text-align: center; text-decoration: none !important; width: 82px; cursor: pointer"
                                                                                        onclick="javascript:CloseCalPopup();">Ok </a>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="cls">
                                        </div>
                                        <div class="createEventFrom">
                                            <p>
                                                <strong>From</strong></p>
                                            <p>
                                                <asp:TextBox ID="txtFromDate" runat="server" ClientIDMode="Static" placeholder="From Date"
                                                    CssClass="eventTitleField" onkeypress="event.returnValue = false;" onkeydown="event.returnValue = false;"
                                                    Style="width: 40%; margin-top: -21px; color: #C2C2C2;"></asp:TextBox>
                                                <div style="display: none">
                                                    <img id="imgFromdate" src="images/calendar1.png" align="absmiddle" class="calendarImg ui-datepicker-trigger"
                                                        style="margin-left: 7px;" />
                                                </div>
                                            </p>
                                            <div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFromDate"
                                                    Display="Dynamic" ValidationGroup="Events" ErrorMessage="Please select Date"
                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="createEventFrom to">
                                            <p>
                                                <strong>To</strong></p>
                                            <p>
                                                <asp:TextBox ID="txtToDate" runat="server" ClientIDMode="Static" CssClass="eventTitleField"
                                                    onkeypress="event.returnValue = false;" onkeydown="event.returnValue = false;"
                                                    placeholder="To Date" Style="width: 40%; margin: -24px -60px 0px 0px; color: #C2C2C2;"></asp:TextBox>
                                                <div style="display: none">
                                                    <img id="imgTodate" src="images/calendar1.png" align="absmiddle" class="calendarImg" />
                                                </div>
                                            </p>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtToDate"
                                                Display="Dynamic" ValidationGroup="Events" ErrorMessage="Please select Date"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div style="padding-top: 0px;" class="cls">
                                            <asp:Label ID="lblDateMessage1" ClientIDMode="Static" Style="display: none;" ForeColor="Red"
                                                runat="server" Text=""></asp:Label>
                                        </div>
                                        <asp:DropDownList Style="margin-left: 0px; color: #C2C2C2;" ID="ddlPriorityType"
                                            ClientIDMode="Static" runat="server" CssClass="eventType">
                                            <asp:ListItem Text="Select Priority" Value="S"></asp:ListItem>
                                            <asp:ListItem Text="Normal" Value="N"></asp:ListItem>
                                            <asp:ListItem Text="Important" Value="I"></asp:ListItem>
                                            <asp:ListItem Text="Very Importatnt" Value="V"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblDateMessage" ClientIDMode="Static" Style="display: none;" ForeColor="Red"
                                            runat="server" Text=""></asp:Label>
                                        <div style="padding-top: 15px;" class="cls">
                                        </div>
                                        <div class="cls">
                                            <br />
                                        </div>
                                        <div class="calEvnt">
                                            <div class="eventTitlete">
                                                Choose Colour:</div>
                                            <div class="colorPicker">
                                                <div class="color1" onclick="getSelText1();" clientidmode="Static" style="cursor: pointer">
                                                </div>
                                                <asp:TextBox ID="txtFiColor" ClientIDMode="Static" Style="display: none" Text="#ff3131"
                                                    runat="server"></asp:TextBox>
                                                <div class="color2" onclick="getSelText2();" style="cursor: pointer">
                                                    <asp:TextBox ID="txtSeColor" ClientIDMode="Static" Style="display: none" Text="#68bee1"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div class="color3" onclick="getSelText3();" style="cursor: pointer">
                                                    <asp:TextBox ID="txtthColor" ClientIDMode="Static" Style="display: none" Text="#fdaf18"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div class="color4" onclick="getSelText4();" style="cursor: pointer">
                                                    <asp:TextBox ID="txtFoColor" ClientIDMode="Static" Style="display: none" Text="#c66905"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div class="color5" onclick="getSelText5();" style="cursor: pointer">
                                                    <asp:TextBox ID="txtFivColor" ClientIDMode="Static" Style="display: none" Text="#23ec02"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div class="color6" onclick="getSelText6();" style="cursor: pointer">
                                                    <asp:TextBox ID="txtSiColor" ClientIDMode="Static" Style="display: none" Text="#021dec"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div class="color7" onclick="getSelText7();" style="cursor: pointer">
                                                    <asp:TextBox ID="txteSeColor" ClientIDMode="Static" Style="display: none" Text="#9a9a9d"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div class="color8" onclick="getSelText8();" style="cursor: pointer">
                                                    <asp:TextBox ID="txtEigColor" ClientIDMode="Static" Style="display: none" Text="#10132a"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div class="color9" onclick="getSelText9();" style="cursor: pointer">
                                                    <asp:TextBox ID="txtNiColor" ClientIDMode="Static" Style="display: none" Text="#70759a"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div class="color10" onclick="getSelText10();" style="cursor: pointer">
                                                    <asp:TextBox ID="txtTenColor" ClientIDMode="Static" Style="display: none" Text="#e77cf4"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div class="color11" onclick="getSelText11();" style="cursor: pointer">
                                                    <asp:TextBox ID="txtEleColor" ClientIDMode="Static" Style="display: none" Text="#8ff1fa"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div class="color12" onclick="getSelText12();" style="cursor: pointer">
                                                    <asp:TextBox ID="txtTweColor" ClientIDMode="Static" Style="display: none" Text="#27950d"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div class="color13" onclick="getSelText13();" style="cursor: pointer">
                                                    <asp:TextBox ID="txtThiColor" ClientIDMode="Static" Style="display: none" Text="#723e14"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div class="color14" onclick="getSelText14();" style="cursor: pointer">
                                                    <asp:TextBox ID="txtFouColor" ClientIDMode="Static" Style="display: none" Text="#f93583"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <asp:TextBox ID="txtColorCode" Style="display: none" ClientIDMode="Static" runat="server"></asp:TextBox>
                                            </div>
                                            <div style="padding-top: 5px; color: #9c9c9c; font-family: Arial; font-size: 16px;"
                                                class="calEvntTxt">
                                                <asp:Label ID="Label1" runat="server" Text="Colour Selected:"></asp:Label>
                                            </div>
                                            <div style="padding-top: 5px;" class="colorPicker">
                                                <asp:TextBox ID="TextBox1" Style="float: left; height: 15px; padding-left: 20px;
                                                    width: 15px;" ClientIDMode="Static" runat="server"></asp:TextBox>
                                                <br />
                                                <div style="width: 250px; margin-top: 5px;">
                                                    <asp:Label ID="lbleventColor" ClientIDMode="Static" Style="display: none;" ForeColor="Red"
                                                        runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="cls">
                                                    <br />
                                                </div>
                                                <div style="padding-bottom: 25px; margin-left: 0%" class="calEvnt" align="left">
                                                    <asp:LinkButton ID="lnkSave" runat="server" Text="Create Event" OnClientClick="CallSaveEvent();"
                                                        CssClass="vote" Style="float: left; margin-left: 0%; font-weight: bold;" ValidationGroup="Events"
                                                        OnClick="lnkSave_Click"></asp:LinkButton>
                                                </div>
                                                <div style="margin-top: -12px; display: none;">
                                                    <a clientidmode="Static" causesvalidation="false" style="margin-left: 9%; text-decoration: none;
                                                        width: 82px; margin-top: 2px; color: #000; cursor: pointer" onclick="javascript:Clear();">
                                                        Cancel </a>
                                                </div>
                                            </div>
                                            <div class="leftCreateEvents" style="display: none;">
                                                <br />
                                                <asp:TextBox ID="txtVenue" ClientIDMode="Static" runat="server" onfocus="if(this.value=='Venue') this.value='';"
                                                    Visible="false" onblur="if(this.value=='') this.value='Venue';" value="Venue"
                                                    class="eventTitleField" name="txtVenue" Style="width: 60%; margin: 0;"></asp:TextBox>
                                                <div>
                                                </div>
                                                <asp:TextBox ID="txtContactPerson" ClientIDMode="Static" runat="server" onfocus="if(this.value=='Contact person') this.value='';"
                                                    onblur="if(this.value=='') this.value='Contact person';" value="Contact person"
                                                    Visible="false" class="eventTitleField" Style="width: 60%; margin: 0;"></asp:TextBox>
                                                <asp:TextBox ID="txtContactPerNumber" ClientIDMode="Static" runat="server" onkeypress="return isNumber(event)"
                                                    Visible="false" onfocus="if(this.value=='Contact person number') this.value='';"
                                                    onblur="if(this.value=='') this.value='Contact person number';" value="Contact person number"
                                                    MaxLength="10" class="PollTitlegrup" name="txtContactPerNumber" Style="width: 60%;
                                                    margin: 0;"></asp:TextBox>
                                            </div>
                                            <div class="rightCreateEvents">
                                            </div>
                                            <div class="cls">
                                                <br />
                                                <br />
                                            </div>
                                            <div class="myevents" id="divmyevents" runat="server" style="margin-left: -1%; width: 905px;
                                                font-family: Arial; font-weight: bold; display: none;">
                                                My Events</div>
                                            <div class="myEventsList" style="border: none;">
                                                <asp:UpdatePanel ID="upevent" runat="server">
                                                    <ContentTemplate>
                                                        <asp:HiddenField ID="hdnDeletePostQuestionID" Value="" ClientIDMode="Static" runat="server" />
                                                        <asp:HiddenField ID="hdnstrQuestionDescription" ClientIDMode="Static" runat="server" />
                                                        <asp:Repeater ID="RptCreatedEvent" OnItemDataBound="RptCreatedEvent_ItemDataBound"
                                                            runat="server" OnItemCommand="RptCreatedEvent_ItemCommand">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnColor" Value='<%# Eval("strColor") %>' runat="server" />
                                                                <asp:HiddenField ID="hdnEventId" Value='<%# Eval("intGrpEventtId") %>' runat="server" />
                                                                <asp:UpdatePanel ID="upev" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="eventsBox" style="vertical-align: middle;">
                                                                            <div id="dvcolor" style="height: 50px; width: 50px; margin-top: -2%;" runat="server">
                                                                                <asp:Label ID="lblColor" runat="server"></asp:Label>
                                                                            </div>
                                                                            <asp:Label ID="lblTitles" runat="server" Text='<%# Eval("strTitle") %>'></asp:Label></span>
                                                                            <div style="float: right; width: 145px;" class="editDeleteMorev">
                                                                                <asp:HiddenField ID="hdnintPostQuestionIdelet" Value='<%# Eval("intGrpEventtId") %>'
                                                                                    ClientIDMode="Static" runat="server" />
                                                                                <asp:HiddenField ID="lnkstrQuestionDescription" runat="server" Value='<%#Eval("strTitle") %>'
                                                                                    ClientIDMode="Static"></asp:HiddenField>
                                                                                <span class="spEditPoll">
                                                                                    <asp:LinkButton ID="lnkEditEvent" Font-Underline="false" class="edit" ClientIDMode="Static"
                                                                                        ToolTip="Edit" Text="Edit" CommandName="Edit" CausesValidation="false" runat="server">
                                                                                    </asp:LinkButton>
                                                                                </span><span class="spDeletePoll">
                                                                                    <asp:LinkButton ID="lnkDeleteEvent" Font-Underline="false" class="edit" ClientIDMode="Static"
                                                                                        ToolTip="Delete" Text="Delete" CommandName="Delete" CausesValidation="false"
                                                                                        OnClientClick="javascript:docdelete()" runat="server">
                                                                                    </asp:LinkButton>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="lnkEditEvent" />
                                                                        <asp:AsyncPostBackTrigger ControlID="lnkDeleteEvent" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="RptCreatedEvent" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                            <br />
                                        </div>
                                    </div>
                                    <div class="cls">
                                        <p>
                                            &nbsp;</p>
                                    </div>
                                </div>
                                <!--left box ends-->
                            </div>
                        </div>
                        <!--Event Mouse Over Popup Start Hear-->
                        <div class="eventDetailsPop" id="eventPop" clientidmode="Static">
                        </div>
                        <!--Event Mouse Over Popup End Hear-->
                    </div>
                    <asp:HiddenField ID="hdnEventPopupRefresh" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdnUpdateEventId" runat="server" Value="" ClientIDMode="Static" />
                    <script type="text/javascript">
                        $(document).ready(function () {
                            var ID = "#" + $("#hdnEventPopupRefresh").val();
                            $(ID).focus();
                        });
                        $(document).ready(function () {
                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                            prm.add_endRequest(function () {
                                var ID = "#" + $("#hdnEventPopupRefresh").val();
                                $(ID).focus();
                            });
                        });
                    </script>
                    <script type="text/javascript">
                        function CloseCalPopup() {
                            $('#dvPopup').hide();
                        }
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
                    <script type="text/javascript">
                        function DateControl() {
                            $(function () {
                                $('#txtFromDate').datepick({ showTrigger: '#imgFromdate' });
                                $('#txtToDate').datepick({ showTrigger: '#imgTodate' });
                            });
                        }
                        DateControl();
                        $(document).ready(function () {
                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                            prm.add_endRequest(function () {
                                $('#txtFromDate').datepick({ showTrigger: '#imgFromdate' });
                                $('#txtToDate').datepick({ showTrigger: '#imgTodate' });
                            });
                        });
                    </script>
                    <script type="text/jscript">
                        function Cancel() {
                            document.getElementById("divCancelEvent").style.display = 'none';
                            return false;
                        }
                    </script>
                    <script type="text/jscript">
                        function messageCloseEvent() {
                            document.getElementById("divSuccessEvents").style.display = 'none';
                            return false;
                        }
                    </script>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
                <asp:AsyncPostBackTrigger ControlID="lnksucessClose" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function CloseMessage() {
            document.getElementById('divSuccessEvent').style.display = 'none';
        }
    </script>
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function Close() {
            document.getElementById('dvPopup').style.display = "none";
        }
        function getSelText1() {
            document.getElementById("txtColorCode").value = document.getElementById("txtFiColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtFiColor").value;
        }

        function getSelText2() {

            document.getElementById("txtColorCode").value = document.getElementById("txtSeColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtSeColor").value;
        }
        function getSelText3() {

            var a = document.getElementById("txtthColor").value; ;
            document.getElementById("txtColorCode").value = document.getElementById("txtthColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtthColor").value;
        }
        function getSelText4() {

            document.getElementById("txtColorCode").value = document.getElementById("txtFoColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtFoColor").value;
        }
        function getSelText4() {

            document.getElementById("txtColorCode").value = document.getElementById("txtFoColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtFoColor").value;
        }
        function getSelText5() {

            document.getElementById("txtColorCode").value = document.getElementById("txtFivColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtFivColor").value;
        }
        function getSelText6() {

            document.getElementById("txtColorCode").value = document.getElementById("txtSiColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtSiColor").value;
        }
        function getSelText7() {

            document.getElementById("txtColorCode").value = document.getElementById("txteSeColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txteSeColor").value;
        }
        function getSelText8() {

            document.getElementById("txtColorCode").value = document.getElementById("txtEigColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtEigColor").value;
        }
        function getSelText9() {

            document.getElementById("txtColorCode").value = document.getElementById("txtNiColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtNiColor").value;
        }
        function getSelText10() {

            document.getElementById("txtColorCode").value = document.getElementById("txtTenColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtTenColor").value;
        }
        function getSelText11() {

            document.getElementById("txtColorCode").value = document.getElementById("txtEleColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtEleColor").value;
        }
        function getSelText12() {

            document.getElementById("txtColorCode").value = document.getElementById("txtTweColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtTweColor").value;
        }
        function getSelText13() {

            document.getElementById("txtColorCode").value = document.getElementById("txtThiColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtThiColor").value;
        }
        function getSelText14() {

            document.getElementById("txtColorCode").value = document.getElementById("txtFouColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtFouColor").value;
        }

    </script>
    <script type="text/javascript">
        function Clear() {
            $('#txtTitle').val("");
            $('#txtDescription').val("");
            $('#txtVenue').val("Venue");
            $('#txtContactPerson').val("Contact person");
            $('#txtContactPerNumber').val("Contact person number");
            $('#txtFromDate').val("From Date");
            $('#txtToDate').val("To Date");
            $('#txtColorCode').val("");
            $('#ddlPriorityType').val(0);
            $("#TextBox1").css("background-color", "");
            $("#TextBox1").val("");
            $('#hdnUpdateEventId').val("");
            $('#lblDateMessage').val("");
            $('#lblDateMessage1').val("");
            document.getElementById('lblDateMessage').style.display = "none";
            document.getElementById('lblDateMessage1').style.display = "none";
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtTitle').keypress(function () {
                $('#txtTitle').css('font-weight', 'normal');
            });
            $('#txtDescription').keypress(function () {
                $('#txtDescription').css('font-weight', 'normal');
            });
            $('#txtVenue').keypress(function () {
                $('#txtVenue').css('font-weight', 'normal');
            });
            $('#txtContactPerson').keypress(function () {
                $('#txtContactPerson').css('font-weight', 'normal');
            });
            $('#txtContactPerNumber').keypress(function () {
                $('#txtContactPerNumber').css('font-weight', 'normal');
            });
            $('#txtFromDate').keypress(function () {
                $('#txtFromDate').css('font-weight', 'normal');
            });
            $('#txtToDate').keypress(function () {
                $('#txtToDate').css('font-weight', 'normal');
            });
        });

        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#txtTitle').keypress(function () {
                    $('#txtTitle').css('font-weight', 'normal');
                });
                $('#txtDescription').keypress(function () {
                    $('#txtDescription').css('font-weight', 'normal');
                });
                $('#txtVenue').keypress(function () {
                    $('#txtVenue').css('font-weight', 'normal');
                });
                $('#txtContactPerson').keypress(function () {
                    $('#txtContactPerson').css('font-weight', 'normal');
                });
                $('#txtContactPerNumber').keypress(function () {
                    $('#txtContactPerNumber').css('font-weight', 'normal');
                });
                $('#txtFromDate').keypress(function () {
                    $('#txtFromDate').css('font-weight', 'normal');
                });
                $('#txtToDate').keypress(function () {
                    $('#txtToDate').css('font-weight', 'normal');
                });

            });
        });
    </script>
    <script type="text/javascript">
        function showpopevnt(str, GrpEventId, intGroupId) {
            $.ajax({
                type: "POST",
                url: "group-event-main.aspx/getData",
                data: "{frmDate:'" + str + "',EventId:'" + GrpEventId + "',intGroupId:'" + intGroupId + "'}",
                contentType: "application/json; charset=utf-8",
                datatype: "jsondata",
                async: "true",
                success: function (response) {
                    var msg = eval('(' + response.d + ')');
                    var table = '<div id="eventpoparrows" class="eventpoparrow"><img src="images/eventpoparrow.png" /></div>';
                    for (var i = 0; i <= (msg.length); i++) {
                        table += '<div class="eventPopDetails" style="width:520px;"><div id="dvcolor" class="color1Pop" style="background-color:' + msg[i].strColor + '"> </div> <div class="headingPop"> <label id="lblTitle">' + msg[i].strTitle + '</label></div>  <div class="eventDtl"><label id="strDescription">' + msg[i].strDescription + '</label></div><div class="eventTiming"><label id="dtFromDates" >' + msg[i].dtFromDate + ' - ' + msg[i].dtTodate + '</label></div></div><div class="cls"></div>';
                        $('#eventPop').html(table);
                    }
                }
            });

            document.getElementById('eventPop').style.display = "block";
        }
        function hidepopevnt() {
            document.getElementById('eventPop').style.display = "none";
        }
        window.onmousemove = function (e) {
            $("#eventPop").offset({ right: e.pageX, top: e.pageY + 30 });
            $("#eventpoparrows").offset({ left: e.pageX, top: e.pageY + 15 });
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#divCancelEvent').center();
            $('#divSuccessEvents').center();
            $('.overout').mouseout(function () {
                document.getElementById('eventPop').style.display = "none";
            });

            $("div.editDeleteMorev").click(function () {
                $('#hdnDeletePostQuestionID').val($(this).children('#hdnintPostQuestionIdelet').val());
                $('#hdnstrQuestionDescription').val($(this).children('#lnkstrQuestionDescription').val());
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.overout').mouseout(function () {
                    document.getElementById('eventPop').style.display = "none";
                });
                $("div.editDeleteMorev").click(function () {
                    $('#hdnDeletePostQuestionID').val($(this).children('#hdnintPostQuestionIdelet').val());
                    $('#hdnstrQuestionDescription').val($(this).children('#lnkstrQuestionDescription').val());
                });
            })
        });
    </script>
    <script type="text/javascript">
        function docdelete() {
            $('#divDeletesucess').css("display", "block");
        }
        function divCancels() {
            $('#divDeletesucess').css("display", "none");
        }
        function CallSaveEvent() {
            $('#ctl00_ContentPlaceHolder1_lnkSave').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtTitle').text() == '' || $('#txtFromDate').text() == '' || $('#txtToDate').text() == '') {
                setTimeout(
                function () {
                    $('#ctl00_ContentPlaceHolder1_lnkSave').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("span.spEditPoll").click(function () {
                $(this).children('#lnkEditEvent').css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $("span.spDeletePoll").click(function () {
                $(this).children('#lnkDeleteEvent').css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $('#txtTitle').keypress(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $('#lnkDeleteConfirm').click(function (e) {
                $(this).css("box-shadow", "0px 0px 5px #00B7E5");
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("span.spEditFolder").click(function () {
                    $(this).children('#lnkEditEvent').css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $("span.spDeleteFolder").click(function () {
                    $(this).children('#lnkDeleteEvent').css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $('#txtTitle').keypress(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $('#lnkDeleteConfirm').click(function (e) {
                    $(this).css("box-shadow", "0px 0px 5px #00B7E5");
                });
            });
        });
    </script>
</asp:Content>
