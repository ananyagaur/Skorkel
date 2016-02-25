<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinkedInAccountDetails.aspx.cs" Inherits="LinkedInAccountDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <tr>
    <td>
    First Name:
    </td>
     <td>
         <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
    </td>
    </tr>
    <br />
    <tr>
    <td>
    Last Name:
    </td>
     <td>
         <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
    </td>
    </tr>
       <br />
       <tr>
    <td>
    Email ID:
    </td>
     <td>
         <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
    </td>
    </tr>
    </div>
    </form>
</body>
</html>
