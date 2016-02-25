<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="my-saved-searches.aspx.cs"
    Inherits="my_saved_searches" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
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
                            <li title="My Search"><a href=" my-saved-searches.aspx" class="mydocs mydocsactive">
                            </a></li>
                        </ul>
                    </div>
                    <div class="skorkelBox">
                        <p class="headingSk">
                            My Searches</p>
                    </div>
                      <asp:UpdatePanel ID="updatetag" runat="server"><ContentTemplate> 
                    <div id="divSavedSearch" runat="server" style="margin-top:45px;">
                    <!--searches starts-->
                    <asp:ListView ID="lstMySavedSearches" runat="server" OnItemCommand="lstMySavedSearches_ItemCommand">
                        <LayoutTemplate>
                            <table cellpadding="0" cellspacing="0">
                                <tr id="itemPlaceHolder" runat="server">
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnCaseID" ClientIDMode="Static" runat="server" Value="" />
                            <asp:HiddenField ID="hdnintMySearchId" ClientIDMode="Static" runat="server" Value='<%#Eval("intMySearchId") %>' />
                            <asp:HiddenField ID="hdnAmongst" ClientIDMode="Static" runat="server" Value='<%#Eval("strPreSearchedIn") %>' />
                            <asp:HiddenField ID="hdnContextId" ClientIDMode="Static" runat="server" Value='<%#Eval("strContextId") %>' />
                            <asp:HiddenField ID="hdnDocumenttype" ClientIDMode="Static" runat="server" Value='<%#Eval("strDocumentType") %>' />
                            <asp:HiddenField ID="hdnstrSearchQuery" ClientIDMode="Static" runat="server" Value='<%#Eval("strSearchQuery") %>' />
                            <div class="tagDtl searchBoxR" style="float:right;">
                                <div class="bookmarkDtl">
                                    <div class="searchType">
                                        Search Type: <span> <asp:Label ID="lblstrSearchFor" runat="server" Text='<%#Eval("strSearchFor") %>'></asp:Label></span></div>
                                    <div class="searchQuery bld breakallsmyserch">
                                         <asp:LinkButton ID="lnksaveTitle" CommandName="SaveSearch" runat="server" Text='<%#Eval("strSavedMyTitle") %>' style="text-decoration:none;color:#666666;  margin-left: 1px;" ></asp:LinkButton>
                                        </div>
                                    <div>
                                        <p class="searchResults">
                                            Search Result:&nbsp;<asp:Label ID="lblcount" runat="server" Text='<%#Eval("intSearchResultCount") %>'></asp:Label></p>
                                        <p class="searchTime">
                                            <asp:Label ID="lblTime" runat="server" Text='<%#Eval("dtTimes") %>'></asp:Label> on <asp:Label ID="lblDate" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label>
                                        </p>
                                    </div>
                                </div>
                                <div class="bookmarkArrow" style="margin: 34px 0 0 0;">
                                    <asp:ImageButton ID="imgbutton" runat="server" ImageUrl="images/mybookmarkarrow.jpg" CommandName="SaveSearch" />
                                    </div>
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
           <div style="display: none" class="clsFooter">
        </div>
    </div>
    <script type="text/javascript">
        function redirecttoresearch(Url) {
            window.location = Url;
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
