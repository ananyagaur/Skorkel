<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="my-tag.aspx.cs"
    Inherits="my_tag" %>

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
                            <li title="My Tags"><a href="my-tag.aspx" class="mytag mytagactive"></a></li>
                            <li title="My Documents"><a href="my-documents.aspx" class="mybookmark"></a></li>
                            <li title="My Bookmarks "><a href="my-bookmarks.aspx" class="mysearches"></a></li>
                            <li title="My Search"><a href=" my-saved-searches.aspx" class="mydocs"></a></li>
                        </ul>
                    </div>
                    <div class="skorkelBox">
                        <p class="headingSk">
                            My Tags</p>
                    </div>
                    <asp:UpdatePanel ID="updatetag" runat="server"><ContentTemplate> 
                    <div id="divtag" runat="server">
                        <asp:HiddenField ID="hdnContentTypeID" runat="server" Value="1" ClientIDMode="Static" />
                        <asp:ListView ID="lstMainMyTag" runat="server" OnItemDataBound="lstMainMyTag_ItemDataBound"
                            OnItemCommand="lstMainMyTag_ItemCommand">
                            <ItemTemplate>
                                <!--tag box starts-->
                                <div class="tagDtl">
                                    <asp:HiddenField ID="hdnCaseId" Value='<%#Eval("intCaseId") %>' runat="server" />
                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("intCaseId") %>' />
                                    <p class="facts">
                                        <asp:Label ID="lblTagName" Text='<%#Eval("TagName") %>' runat="server"></asp:Label></p>
                                    <p class="factsDtl">
                                        <asp:Label ID="lblContent" runat="server" Text='<%#Eval("strSelectedContent") %>'></asp:Label></p>
                                    <p class="factsTxt">
                                        <asp:LinkButton ID="lnkTitle" ToolTip="View document" Style="color: #B3B3B3; text-decoration: none;"
                                            runat="server" CommandName="Title" Text='<%#Eval("CaseTitle") %>'></asp:LinkButton></p>
                                    <p class="factsDate">
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("dtaddedOn") %>'></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Times") %>'></asp:Label>
                                    </p>
                                </div>
                                <!--tag box ends-->
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
    </div>
    <script type="text/javascript">
        function ToogleComment(ctl) {
            $(ctl).find("#commentTxt1").slideToggle("slow");
            $(ctl).find("#commnetBtn1 img").toggle();
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
                            $("#lblNoMoreRslt").css("display", "none");
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
