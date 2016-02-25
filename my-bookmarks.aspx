<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="my-bookmarks.aspx.cs"
    Inherits="my_bookmarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="cls">
        </div>
        <!--inner container ends-->
        <div class="innerDocumentContainerSpc">
            <div class="innerContainer">
                <div class="innerGroupBox">
                    <div class="skorelScoreMenu">
                        <ul>
                            <li title="My Score"><a href="my-points.aspx" class="myscore"></a></li>
                            <li title="My Tags"><a href="my-tag.aspx" class="mytag"></a></li>
                            <li title="My Documents"><a href="my-documents.aspx" class="mybookmark"></a></li>
                            <li title="My Bookmarks "><a href="my-bookmarks.aspx" class="mysearches mysearchesactive">
                            </a></li>
                            <li title="My Search"><a href=" my-saved-searches.aspx" class="mydocs"></a></li>
                        </ul>
                    </div>
                    <div class="skorkelBox">
                        <p class="headingSk">
                            My Bookmarks</p>
                    </div>
                    <!--bookmark starts-->
                    <!--bookmark ends-->
                    <div>
                        <div id="divCancelPopup" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
                            float: left; width: 500px; padding-top: 0px; position: fixed; margin: 65px 0 0 14%;
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
                            width: 500px; padding-top: 0px; position: relative; margin: 65px 0 0 14%; z-index: 100;
                            display: none;" clientidmode="Static">
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
                                            <asp:Label ID="Label2" runat="server" Text="Book mark removed successfully." Font-Size="Small"></asp:Label>
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
                    <asp:UpdatePanel ID="updatetag" runat="server"><ContentTemplate> 
                    <div id="divbookmark" runat="server" style="margin-top:45px;">
                        <asp:HiddenField ID="hdnContentTypeID" runat="server" Value="1" ClientIDMode="Static" />
                        <asp:ListView ID="lstBookMark" runat="server" OnItemCommand="lstBookMark_ItemCommand">
                            <LayoutTemplate>
                                <table cellpadding="0" cellspacing="0">
                                    <tr id="itemPlaceHolder" runat="server">
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnCaseID" ClientIDMode="Static" runat="server" Value='<%#Eval("intCaseId") %>' />
                                <asp:HiddenField ID="hdnstrJudgeNames" ClientIDMode="Static" runat="server" Value='<%#Eval("strJudgeNames") %>' />
                                <div class="tagDtl" style="float:right;">
                                    <div class="bookmarkDtl">
                                        <div class="partyName">
                                           <asp:Label ID="lblstrPartyNames" runat="server" Text='<%#Eval("strPartyNames") %>'></asp:Label></div>
                                        <div class="partyName">
                                           <asp:Label ID="lblEq_Citations" runat="server" Text='<%#Eval("Eq_Citations") %>'></asp:Label> </div>
                                        <div class="partyName">
                                           <asp:Label ID="lblstrJurisdiction" runat="server" Text='<%#Eval("strJurisdiction") %>'></asp:Label></div>
                                        <div class="partyName">
                                          <asp:Label ID="lblintYear" runat="server" Text='<%#Eval("intYear") %>'></asp:Label> </div>
                                        <div class="cls">
                                        </div>
                                        <p class="bookMarkTxt">
                                            <asp:LinkButton ID="lnkTitle" ToolTip="View document" Style="color: Black; text-decoration: none; font-weight: normal; "
                                                runat="server" CommandName="Title" Text='<%#Eval("strCaseTitle") %>'></asp:LinkButton>
                                        </p>
                                    </div>
                                    <div class="bookmarkArrow">
                                        <img src="images/mybookmarkarrow.jpg" /></div>
                                    <div class="close" style="display: none;">
                                        <asp:LinkButton ID="lnkDeleteDoc" ToolTip="Delete bookmark" Style="text-decoration: none;
                                            float: right;" runat="server" CommandName="Delete Bookmark"><img src="images/gray-close.png" /></asp:LinkButton>
                                    </div>
                                    <span class="lastEditedDate" style="display: none;">Last edited :
                                        <asp:Label ID="lbldate" runat="server" Text='<%#Eval("AddedOn") %>'></asp:Label></span>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                        <p id="pLoadMore" runat="server" align="center">
                            <asp:ImageButton ID="imgLoadMore" runat="server" ClientIDMode="Static" ImageUrl="~/images/loadingIcon.gif"
                                OnClick="imgLoadMore_OnClick" style="display:none;" />
                        </p>
                        <p align="center">
                            <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server"
                                ClientIDMode="Static" ForeColor="Red" Visible="false"></asp:Label>
                        </p>
                    <!--searches ends-->
                        <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
                        <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                        <asp:HiddenField ID="hdnLoader" runat="server" ClientIDMode="Static" />
                    </div>
                     </ContentTemplate> </asp:UpdatePanel>
                    <div class="cls">
                    </div>
                </div>
                <!--left box ends-->
            </div>
            <!--left verticle search list ends-->
        </div>
        <div class="cls">
        </div>
    </div>
    <script type="text/jscript">
        function Cancel() {
            document.getElementById("divCancelPopup").style.display = 'none';
            document.getElementById("divSuccess").style.display = 'none';
            return false;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(window).scroll(function () {
                if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                    $("#imgLoadMore").css("display", "block");
                    var v = $("#lblNoMoreRslt").text();
                    var maxCount = $("#hdnMaxcount").val();
                    if (maxCount <= 10) {
                        $("#lblNoMoreRslt").css("display", "block");
                        $("#imgLoadMore").css("display", "none");
                    } else {
                        if (v != "No more results available...") {
                            document.getElementById('<%= imgLoadMore.ClientID %>').click();
                        }
                    }
                }
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(window).scroll(function () {
                    if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                        $("#imgLoadMore").css("display", "block");
                        var v = $("#lblNoMoreRslt").text();
                        var maxCount = $("#hdnMaxcount").val();
                        if (maxCount <= 10) {
                            $("#lblNoMoreRslt").css("display", "none");
                            $("#imgLoadMore").css("display", "none");
                        } else {
                            if (v != "No more results available...") {
                                document.getElementById('<%= imgLoadMore.ClientID %>').click();
                            }
                        }
                    }
                });
            });
        });
    </script>
</asp:Content>
