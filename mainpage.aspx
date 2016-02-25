<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mainpage.aspx.cs" Inherits="main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="style1">
            <tr>
                <td colspan="2">
                    DashBoard</td>
            </tr>
            <tr>
                <td>
                    Welcome :
                    <asp:Label ID="lbluser" runat="server"></asp:Label>
                    <br />
                       <asp:Label ID="lblCompanyName" runat="server"></asp:Label>
                         <br />
                       <asp:Label ID="lblEmailAdd" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btn_logout" runat="server" onclick="btn_logout_Click" 
                        Text="Logout" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
