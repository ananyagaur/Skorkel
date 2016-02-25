<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="Scrl_UserGroupDetailTbl.aspx.cs"
    Inherits="Scrl_UserGroupDetailTbl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="headMain" runat="Server">
    <link rel="stylesheet" type="text/css" href="../css/Pages.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--heading ends-->
    <div class="cls">
    </div>
    <!--inner container starts-->
    <div class="cls">
    </div>
    <!--inner container ends-->
    <div class="innerContainer" style="background: #fff; float: left">
        <table width="100%">
            <tr>
                <td>
                    <div>
                        <table width="100%">
                            <tr>
                                <td style="float: left;">
                                    <h1 class="blueLink">
                                        GROUPS
                                    </h1>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Image ID="imgheader" ImageUrl="~/Images/GroupHeader.png" runat="server" Height="2px"
                                        Width="100%" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:UpdatePanel ID="updtScrl_UserGroupDetailTbl" runat="server" UpdateMode="Always">
        <ContentTemplate>--%>
                    <legend>
                        <asp:LinkButton ID="lnkAdd" runat="server" Text="+Add New" ForeColor="Blue" OnClick="lnkAdd_Click"></asp:LinkButton>
                        <%--<asp:LinkButton ID="lnkGrpStatus" runat="server" Text="Request Groups" ForeColor="Blue" PostBackUrl='<%# "Scrl_UserGroupStatus.aspx?GroupId="+ Eval("inGroupId")+ "&GroupName="+Eval("strGroupName") %>'></asp:LinkButton>--%>
                    </legend>
                    <asp:Panel ID="PnlAdd" runat="server">
                        <table width="100%" cellspacing="2" cellpadding="2">
                            <tr>
                                <td width="10%">
                                    <asp:Label ID="lblGroupName" runat="server" Text="GroupName" CssClass="cssLable"></asp:Label>
                                </td>
                                <td width="2%">
                                    :
                                </td>
                                <td width="88%" align="left">
                                    <asp:TextBox ID="txtGroupName" runat="server" CssClass="TextField"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rf_grp" runat="server" ControlToValidate="txtGroupName"
                                        ErrorMessage="Please enter group name" ValidationGroup="t"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblSummary" runat="server" Text="Summary" CssClass="cssLable"></asp:Label>
                                </td>
                                <td valign="top">
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSummary" runat="server" TextMode="MultiLine" CssClass="TextFieldMulti"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rf_sum" runat="server" ControlToValidate="txtSummary"
                                        ErrorMessage="Please enter summary" ValidationGroup="t"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblGroupType" runat="server" Text="GroupType" CssClass="cssLable"></asp:Label>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpGroupType" runat="server" CssClass="cssDropDown">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblDescription" runat="server" Text="Description" CssClass="cssLable"></asp:Label>
                                </td>
                                <td valign="top">
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="TextFieldMulti"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rf_desc" runat="server" ControlToValidate="txtDescription"
                                        ErrorMessage="Please enter description" ValidationGroup="t"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAccess" runat="server" Text="Access" CssClass="cssLable"></asp:Label>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <%--<asp:TextBox ID="txtAccess" runat="server" CssClass="cssTextBox"></asp:TextBox>--%>
                                    <asp:RadioButtonList ID="RdBList_Access" runat="server" RepeatDirection="Horizontal"
                                        Style="font-size: small; margin-left: 2px;">
                                        <asp:ListItem Selected="True" Text="Auto Join" Value="A">  </asp:ListItem>
                                        <asp:ListItem Text="Request to Join" Value="R">  </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Private/Public" CssClass="cssLable"></asp:Label>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <%--<asp:TextBox ID="txtAccess" runat="server" CssClass="cssTextBox"></asp:TextBox>--%>
                                    <asp:RadioButtonList ID="RdblistPrivPub" runat="server" RepeatDirection="Horizontal"
                                        Style="font-size: small; margin-left: 2px;">
                                        <asp:ListItem Selected="True" Text="Public" Value="0"> </asp:ListItem>
                                        <asp:ListItem Text="Private" Value="1">  </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblLogoPath" runat="server" Text="Upload&nbsp;Logo" CssClass="cssLable"></asp:Label>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <%--<asp:TextBox ID="txtLogoPath" runat="server" CssClass="cssTextBox"></asp:TextBox>--%>
                                    <asp:FileUpload ID="fileupload_LogoPath" runat="server" />
                                    <%--<asp:RequiredFieldValidator ID="rf_logo" runat="server" ControlToValidate="fileupload_LogoPath"
                                ErrorMessage="Please upload logo" ValidationGroup="t"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="cssButton" OnClick="btnSubmit_Click"
                                        Width="80px" ValidationGroup="t"></asp:Button>
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="cssButton" OnClick="btnCancel_Click"
                                        Width="80px"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblId" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="PnlView" runat="server">
                        <asp:ListView ID="dtlScrl_UserGroupDetailTbl" runat="server" DataKeyNames="inGroupId"
                            OnItemCommand="dtlScrl_UserGroupDetailTbl_ItemCommand" OnItemDeleting="dtlScrl_UserGroupDetailTbl_ItemDeleting"
                            OnItemEditing="dtlScrl_UserGroupDetailTbl_ItemEditing">
                            <LayoutTemplate>
                                <table style="border-color: #4D90FE; border-width: thin;" width="100%" cellspacing="0">
                                    <tr id="Tr1" class="tableHeader" runat="server">
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <asp:HiddenField ID="hdninGroupId" runat="server" Value='<%#Eval("inGroupId") %>' />
                                <tr>
                                    <td colspan="2" width="80%" align="left">
                                        <div class="rounded-corners" style="width: 600px;">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <label>
                                                                        <strong>Group Name </strong>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblstrGroupName" runat="server" Text='<%#Eval("strGroupName") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label>
                                                                        <strong>Summary </strong>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblstrSummary" runat="server" Text='<%#Eval("strSummary") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label>
                                                                        <strong>Group Type </strong>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblstrGroupType" runat="server" Text='<%#Eval("strGroupType") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label>
                                                                        <strong>Description </strong>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblstrDescription" runat="server" Text='<%#Eval("strDescription") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label>
                                                                        <strong>Access </strong>
                                                                    </label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblstrAccess" runat="server" Text='<%#Eval("strAccess") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 20%" valign="top" align="right">
                                                        <asp:LinkButton runat="Server" ID="EditButton" Text="Edit" ToolTip="Edit" CommandName="Edit"
                                                            ForeColor="blue" Font-Underline="true" CausesValidation="false">
                                                        </asp:LinkButton>
                                                        &nbsp;|&nbsp;
                                                        <asp:LinkButton runat="Server" ID="DeleteButton" Text="Delete" ToolTip="Delete" CommandName="Delete"
                                                            ForeColor="blue" Font-Underline="true" CausesValidation="false" OnClientClick="javascript:return confirm('Do you want to delete?');">
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </asp:Panel>
                    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
