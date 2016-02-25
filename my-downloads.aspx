<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="my-downloads.aspx.cs"
    Inherits="my_downloads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <link href="css/jquery.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .workHeadingwidth
        {
            width: 90%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="cls">
        </div>
        <div class="innerDocumentContainerSpc">
            <div class="innerContainer">
                <div class="innerGroupBox">
                    <div class="skorelScoreMenu">
                        <ul>
                            <li title="My Score"><a href="my-points.aspx" class="myscore"></a></li>
                            <li title="My Tags"><a href="my-tag.aspx" class="mytag"></a></li>
                            <li title="My Documents"><a href="my-documents.aspx" class="mybookmark"></a></li>
                            <li title="My Bookmarks "><a href="my-bookmarks.aspx" class="mysearches"></a></li>
                            <li title="My Search"><a href=" my-saved-searches.aspx" class="mydocs"></a></li>
                            <li title="My Downloads"><a href="my-downloads.aspx" class="mydocsupload mydocsuploadactive">
                            </a></li>
                            
                        </ul>
                    </div>
                    <div class="skorkelBox">
                        <p class="headingSk">
                            My Download</p>
                    </div>
                   
                    <asp:HiddenField ID="hdnContentTypeID" runat="server" Value="1" ClientIDMode="Static" />
                    <asp:ListView ID="lstFreeDownload" runat="server" OnItemCommand="lstFreeDownload_ItemCommand">
                        <LayoutTemplate>
                            <table cellpadding="0" cellspacing="0">
                                <tr id="itemPlaceHolder" runat="server">
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnCaseID" ClientIDMode="Static" runat="server" Value='<%#Eval("intCaseId") %>' />
                            <div class="progessList workHeadingwidth">
                                <div class="icon">
                                    <img src="images/gray-document-icon.png" /></div>
                                <div class="workList" style="width: 80%;">
                                    <span class="subheading">
                                        <asp:LinkButton ID="lnkTitle" ToolTip="View document" Style="text-decoration: none;
                                            color: #333333; font-size: 14px; font-weight: bold;" runat="server" CommandName="Title"
                                            Text='<%#Eval("strCaseTitle") %>'></asp:LinkButton>
                                    </span>
                                    <br />
                                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("strDescription") %>'></asp:Label>
                                    <br />
                                    <span class="author" style="text-decoration: none; color: #333333; font-size: 14px;
                                        font-weight: normal;">Download date: <span class="downloadedDate">
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("AddedOn") %>' Style="text-decoration: none;
                                                color: #333333; font-size: 14px; font-weight: normal;"></asp:Label></span></span>
                                </div>
                                <div class="close" style="float: right;">
                                    <asp:LinkButton ID="lnkDeleteDoc" ToolTip="Delete Download" Style="text-decoration: none;"
                                        runat="server" CommandName="Delete Download"><img src="images/gray-close.png" /></asp:LinkButton>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                    <div id="divCancelPopup" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                        float: left; width: 500px; padding-top: 0px; position: relative; margin: -200px 0 0 50px;
                        z-index: 100; display: none;" clientidmode="Static">
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
                                                <a href="#" clientidmode="Static" causesvalidation="false" style="float: left; text-align: center;
                                                    text-decoration: none; width: 82px; padding-top: 5px; color: #000;" onclick="Cancel();">
                                                    Cancel </a>
                                            </td>
                                            <td style="padding-right: 20px;">
                                                <asp:LinkButton ID="lnkConnDisconn" runat="server" ClientIDMode="Static" Text="Yes"
                                                    CssClass="joinBtn" OnClick="lnkConnDisconn_Click"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divSuccess" runat="server" style="border: 20px solid rgba(0,0,0,0.5); float: left;
                        width: 500px; padding-top: 0px; position: relative; margin: -200px 0 0 50px;
                        z-index: 100; display: none;" clientidmode="Static">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                            <tr>
                                <td>
                                    <b>
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>&nbsp;&nbsp;
                                        <asp:Label ID="Label2" runat="server" Text="Downloaded document removed successfully."
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
                                            <td style="padding-right: 20px;">
                                                <a href="#" clientidmode="Static" causesvalidation="false" style="float: left; text-align: center;
                                                    text-decoration: none; width: 82px; padding-top: 5px; color: #000;" onclick="Cancel();">
                                                    Close </a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <!--left box ends-->
            </div>
            <!--left verticle search list ends-->
        </div>
        <div class="innerDocumentContainer" style="display:none;">
            <div class="innerContainer">
                <div class="leftDocumentPanel" style="width: 990px;">
                    <div class="leftLeftPanel">
                        <ul>
                            <li title="My Scores"><a href="my-points.aspx">
                                <img src="images/my-points-icon.png" /></a></li>
                            <li title="My Tags"><a href="my-tag.aspx">
                                <img src="images/my-tag1-icon.png" /></a></li>
                            <li title="My Documents"><a href="my-documents.aspx">
                                <img src="images/my-document-icon.png" align="" /></a></li>
                            <li title="My Boolmarks"><a href="my-bookmarks.aspx">
                                <img src="images/my-tag-icon.png" /></a></li>
                            <li title="My Searches"><a href="my-saved-searches.aspx">
                                <img src="images/my-search-icon.png" /></a></li>
                            <li id="active" title="My Downloads"><a href="my-downloads.aspx">
                                <img src="images/my-download-icon.png" /></a></li>
                        </ul>
                    </div>
                    <!--left verticle icons ends-->
                    <div class="leftRightPanelDown">
                        <div class="top">
                        </div>
                        <div id="divdownload" runat="server" class="center" style="height: 600px;">
                            <div style="margin-left: 30px; margin-top: 20px;">
                                <div class="documentHeading">
                                    My Downloads</div>
                                <div class="workHeading workHeadingwidth">
                                    <span style="text-align: left;" class="left">
                                        <asp:Label ID="lblFreeDownload" runat="server"></asp:Label></span><span class="right">
                                            <asp:LinkButton ID="lnkFreeViewAll" runat="server" Text="View all" OnClick="lnkFreeViewAll_OnClick"></asp:LinkButton>
                                        </span>
                                </div>
                                <div class="cls">
                                </div>
                                <!--list starts-->
                                <!--list starts-->
                                <div class="cls" style="height: 40px">
                                </div>
                                <div class="cls" style="height: 40px">
                                </div>
                            </div>
                        </div>
                        <div class="bottom">
                        </div>
                    </div>
                </div>
                <div style="display: none;" class="clsFooter">
                </div>
            </div>
        </div>
    </div>
    <script type="text/jscript">
        function Cancel() {
            document.getElementById("divCancelPopup").style.display = 'none';
            document.getElementById("divSuccess").style.display = 'none';
            return false;
        }
    </script>
</asp:Content>
