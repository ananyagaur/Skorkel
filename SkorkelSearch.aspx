<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="SkorkelSearch.aspx.cs" Inherits="SkorkelSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">

    <script type="text/javascript">

        function mainrowredirect(Url) {
            window.location = Url;
        }

        function ClientPopulated(source, eventArgs) {
            if (source._currentPrefix != null) {
                var list = source.get_completionList();
                var search = source._currentPrefix.toLowerCase();
                for (var i = 0; i < list.childNodes.length; i++) {
                    var text = list.childNodes[i].innerHTML;
                    var index = text.toLowerCase().indexOf(search);
                    if (index != -1) {
                        var value = text.substring(0, index);
                        value += '<span class="AutoComplete_ListItemHiliteText">';
                        value += text.substr(index, search.length);
                        value += '</span>';
                        value += text.substring(index + search.length);
                        list.childNodes[i].innerHTML = value;

                    }
                }
            }
        }

        function ClientItemSelected(source, e) {
            source.get_element().value = (document.all) ? e._item.innerText : e._item.textContent;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="cls">
    </div>
    <!--inner container starts-->
    <div class="cls">
    </div>
    <!--inner container ends-->
    <div class="innerContainer" style="background: #fff; float: left">
        <div class="middleContainer">
            <asp:Panel ID="pnlAllList" runat="server" Width="100%">
                <asp:ListView ID="lstSearchResult" runat="server" OnItemDataBound="lstSearchResult_ItemDataBound">
                    <LayoutTemplate>
                        <table width="95%" border="0" cellpadding="4" cellspacing="0">
                            <tr id="itemPlaceHolder" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="left" valign="top">
                                <div>
                                    <asp:HiddenField ID="hdnContentID" Value='<%# Eval("intContentID") %>' runat="server" />
                                    <asp:HiddenField ID="hdnContentTypeID" Value='<%#Eval("intContentTypeId") %>' runat="server" />
                                    <h1>
                                        <a id="ancRedirect" runat="server" style="cursor: pointer;">
                                            <asp:Label ID="lblTitle" Text='<%# Eval("SearchTitle") %>' runat="server"></asp:Label></a></h1>
                                    <div>
                                        <asp:Label ID="lblyearallList" Text='<%# Eval("intYear") %>' runat="server"></asp:Label></div>
                                    <div>
                                        <asp:Label ID="SearchDesc" Text='<%# Eval("SearchDesc") %>' runat="server"></asp:Label></div>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                <asp:HiddenField ID="hdnYearMain" runat="server" />
                <asp:HiddenField ID="hdnCourtMain" runat="server" />
                <div align="right" style="padding-right: 40px">
                    <div id="dvPage" runat="server" class="datapager" clientidmode="Static" style="width: 90%">
                        <asp:LinkButton ID="lnkFirst" runat="server" CssClass="buttonPaging" OnClick="lnkFirst_Click">First</asp:LinkButton>
                        <asp:LinkButton ID="lnkPrevious" runat="server" CssClass="buttonPaging" OnClick="lnkPrevious_Click">Previous</asp:LinkButton>
                        <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand"
                            OnItemDataBound="rptDvPage_ItemDataBound">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkPageLink" runat="server" CommandName="PageLink" Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:LinkButton ID="lnkNext" runat="server" CssClass="buttonPaging" OnClick="lnkNext_Click">Next</asp:LinkButton>
                        <asp:LinkButton ID="lnkLast" runat="server" CssClass="buttonPaging" OnClick="lnkLast_Click">Last</asp:LinkButton>
                        <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
