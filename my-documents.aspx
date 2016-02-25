<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="my-documents.aspx.cs"
    Inherits="my_documents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="cls">
        </div>
        <!--inner container ends-->
        <div class="innerDocumentContainerSpc">
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
                                                Style="float: left; text-align: center; text-decoration: none ! important; width: 82px;
                                                margin-top: -5px; color: #000; cursor: pointer;" OnClientClick="javascript:divCancels();return false;"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="innerContainer">
                <div class="innerGroupBox">
                    <div class="skorelScoreMenu" style="height: 500px;">
                        <ul>
                            <li title="My Score"><a href="my-points.aspx" class="myscore"></a></li>
                            <li title="My Tags"><a href="my-tag.aspx" class="mytag"></a></li>
                            <li title="My Documents"><a href="my-documents.aspx" class="mybookmark mybookmarkactive">
                            </a></li>
                            <li title="My Bookmarks "><a href="my-bookmarks.aspx" class="mysearches"></a></li>
                            <li title="My Search"><a href=" my-saved-searches.aspx" class="mydocs"></a></li>
                        </ul>
                    </div>
                    <div class="skorkelBox">
                        <p class="headingSk">
                            My Documents</p>
                        <p>
                            <a href="SkorkeluplodDocuments.aspx?updateid=1" class="createForum"><b>Upload </b>
                            </a>
                        </p>
                    </div>
                    <div class="workInProgress">
                        Work in Progress</div>
                    <div class="cls" style="height: 10px">
                    </div>
                    <div id="sliderContent" class="ui-corner-all">
                        <div class="viewer ui-corner-all" style="height: 208px">
                            <div class="content-conveyor ui-helper-clearfix">
                                <asp:ListView ID="LstDocument" runat="server" GroupItemCount="500" GroupPlaceholderID="groupPlaceHolder1"
                                    OnItemDataBound="LstDocument_ItemDataBound" OnItemCommand="LstDocument_ItemCommand"
                                    ItemPlaceholderID="itemPlaceHolder1">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <GroupTemplate>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnDocsId" Value='<%#Eval("intDocId") %>' runat="server" />
                                        <asp:HiddenField ID="hdnstrDocTitle" Value='<%#Eval("strDocTitle") %>' runat="server" />
                                         <asp:HiddenField ID="hdnstrFilePath" Value='<%#Eval("strFilePath") %>' runat="server" />
                                        <div class="itemDocument">
                                            <div class="rightTag">
                                                <div class="docuPreview">
                                                    <img src="images/docpreview.jpg" />
                                                    <asp:ImageButton runat="Server" ID="DeleteButton" CssClass="deleteCross" ImageUrl="~/images/deleteCross.png"
                                                        CommandName="Deletedoc" CausesValidation="false"></asp:ImageButton>
                                                </div>
                                                <div class="docFileName" style="width: 76px; text-align: center;">
                                                    <asp:HyperLink ID="hrp_DocPath" ClientIDMode="Static" Style="width: inherit; text-decoration: none;
                                                        color: #333333; font-size: 14px; font-weight: bold;" Target="_blank" ToolTip="Download file"
                                                        NavigateUrl='<%# "UploadDocument/"+Eval("strFilePath")%>' Text='<%#Eval("strDocTitle") %>'
                                                        Font-Size="Small" runat="server"></asp:HyperLink></div>
                                            </div>
                                            <div class="cls">
                                            </div>
                                        </div>
                                        <div class="progessList workHeadingwidthgrup" style="display: none;">
                                            <div class="icon">
                                                <img src="images/gray-document-icon.png" /></div>
                                            <div class="workList" style="width: 80%;">
                                                <span class="subheading"></span>
                                                <br />
                                                <asp:Label ID="lblDiscrption" runat="server" Text='<%#Eval("strDescrition") %>'></asp:Label>
                                                <br />
                                                <span class="date">Last edited:
                                                    <asp:Label ID="lblDateAddedOn" Visible="false" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label>
                                                    <asp:Label ID="lblDateModOn" Visible="false" runat="server" Text='<%#Eval("dtModifiedOn") %>'></asp:Label>
                                                </span>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                    <div id="slider" style="background-color: #fff;">
                    </div>
                    <div class="cls sliderNewBg">
                    </div>
                    <div class="cls">
                    </div>
                    <div class="workInProgress" style="margin-top: -40px; display: none;">
                        Published Documents</div>
                    <div class="cls" style="height: 10px">
                    </div>
                    <div id="sliderContent1" class="ui-corner-all" style="display: none;">
                        <div class="viewer ui-corner-all" style="height: 208px">
                            <div class="content-conveyor ui-helper-clearfix">
                                <asp:ListView ID="lstPublishDocs" runat="server" GroupItemCount="500" GroupPlaceholderID="groupPlaceHolder1"
                                    ItemPlaceholderID="itemPlaceHolder1">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <GroupTemplate>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnDocsId" Value='<%#Eval("intDocId") %>' runat="server" />
                                        <div class="itemDocument">
                                            <div class="rightTag">
                                                <div class="docuPreview">
                                                    <img src="images/docpreview.jpg" /></div>
                                                <div class="docFileName">
                                                    <asp:Label ID="lblDocument" runat="server" Text='<%#Eval("strDocTitle") %>'></asp:Label></div>
                                            </div>
                                            <div class="cls">
                                            </div>
                                            <div class="dTitle" style="display: none;">
                                                <br />
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("strDocName") %>'></asp:Label>
                                            </div>
                                            <div class="cls">
                                            </div>
                                            <div class="publishOn" style="display: none;">
                                                Published on
                                                <br />
                                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("PublishON") %>'></asp:Label></div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                    <div id="slider1" style="display: none;">
                    </div>
                    <div class="cls sliderNewBg next2" style="display: none;">
                    </div>
                </div>
                <!--left box ends-->
            </div>
            <!--left verticle search list ends-->
        </div>
        <div class="cls">
        </div>
        <div class="innerDocumentContainer" style="display: none;">
            <div class="innerContainer">
                <div class="leftDocumentPanel" style="width: 990px;">
                    <!--left verticle icons starts-->
                    <div class="leftLeftPanel">
                        <ul>
                            <li title="My Scores"><a href="my-points.aspx">
                                <img src="images/my-points-icon.png" /></a></li>
                            <li title="My Tags"><a href="my-tag.aspx">
                                <img src="images/my-tag1-icon.png" /></a></li>
                            <li id="active" title="My Documents"><a href="my-documents.aspx">
                                <img src="images/my-document-icon.png" align="" /></a></li>
                            <li title="My Bookmarks"><a href="my-bookmarks.aspx">
                                <img src="images/my-tag-icon.png" /></a></li>
                            <li title="My Searches"><a href="my-saved-searches.aspx">
                                <img src="images/my-search-icon.png" /></a></li>
                            <li title="My Downloads"><a href="my-downloads.aspx">
                                <img src="images/my-download-icon.png" /></a></li>
                        </ul>
                    </div>
                    <!--left verticle icons ends-->
                    <div class="leftRightPanelDown">
                        <div class="top">
                        </div>
                        <div class="center">
                            <div style="margin-left: 30px; margin-top: 20px;">
                                <div class="workHeading workHeadingwidthgrup">
                                    <span style="text-align: left" class="left">My Documents(<asp:Label ID="lblTotalDocs"
                                        runat="server" Text="Label"></asp:Label>)</span><span class="right"> |
                                            <asp:LinkButton ID="lnkViewAll" runat="server" Text="View All" OnClick="lnkViewAll_OnClick"></asp:LinkButton>
                                        </span>
                                </div>
                                <div class="cls">
                                    <br />
                                </div>
                                <div class="cls">
                                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                </div>
                                <!--list starts-->
                                <div class="cls" style="height: 40px">
                                </div>
                                <!--publishedDocument starts-->
                                <div class="publishedDocument workHeadingwidthgrup">
                                    <p style="text-align: left" class="heading">
                                        Published Documents(<asp:Label ID="lblPublishDocs" runat="server" Text=""></asp:Label>)</p>
                                    <div class="sliderBox">
                                        <div class="list_carousel" style="height: 200px">
                                            <ul id="foo4" style="height: 200px; width: 500px">
                                            </ul>
                                            <div class="clearfix">
                                            </div>
                                            <div class="prev">
                                                <a id="prev4" href="#">
                                                    <img src="images/previous.jpg" /></a></div>
                                            <div class="next">
                                                <a id="next4" href="#">
                                                    <img src="images/next.jpg" /></a></div>
                                            <div id="pager4" class="pager">
                                            </div>
                                        </div>
                                    </div>
                                    <!--publishedDocument ends-->
                                    <div class="cls" style="height: 40px">
                                    </div>
                                </div>
                                <!--publishedDocument ends-->
                                <div class="cls" style="height: 40px">
                                </div>
                                <!--publishedDocument starts-->
                                <div class="publishedDocument workHeadingwidthgrup">
                                    <p style="text-align: left" class="heading">
                                        Documents on Sale(<asp:Label ID="lblSale" runat="server" Text="Label"></asp:Label>)</p>
                                    <div class="sliderBox">
                                        <div class="list_carousel" style="height: 200px">
                                            <ul id="foo5" style="height: 200px; width: 500px">
                                                <asp:ListView ID="lstDocSale" runat="server" GroupItemCount="500" GroupPlaceholderID="groupPlaceHolder1"
                                                    ItemPlaceholderID="itemPlaceHolder1">
                                                    <LayoutTemplate>
                                                        <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                                    </LayoutTemplate>
                                                    <GroupTemplate>
                                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                    </GroupTemplate>
                                                    <ItemTemplate>
                                                        <li class="documentBg">
                                                            <asp:HiddenField ID="hdnDocsId" Value='<%#Eval("intDocId") %>' runat="server" />
                                                            <div class="dTitle">
                                                                <asp:Label ID="lblDocument" runat="server" Text='<%#Eval("strDocTitle") %>'></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("strDocName") %>'></asp:Label>
                                                                <div class="sale" id="divSale" runat="server">
                                                                    <br />
                                                                    <img src="images/sale.png" /></div>
                                                            </div>
                                                            <div class="cls">
                                                            </div>
                                                            <div class="publishOn">
                                                                Published on
                                                                <br />
                                                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("PublishON") %>'></asp:Label></div>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </ul>
                                            <div class="clearfix">
                                            </div>
                                            <div class="prev">
                                                <a id="prev5" href="#">
                                                    <img src="images/previous.jpg" /></a></div>
                                            <div class="next">
                                                <a id="next5" href="#">
                                                    <img src="images/next.jpg" /></a></div>
                                            <div id="pager5" class="pager">
                                            </div>
                                        </div>
                                    </div>
                                    <!--publishedDocument ends-->
                                    <div class="cls" style="height: 40px">
                                    </div>
                                </div>
                                <div class="cls" style="height: 40px">
                                </div>
                            </div>
                        </div>
                        <div class="bottom">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript" language="javascript">
            $(function () {
                $('#foo4').carouFredSel({
                    auto: false,
                    prev: '#prev4',
                    next: '#next4',
                    pagination: "#pager4",
                    mousewheel: true,
                    swipe: {
                        onMouse: false,
                        onTouch: true
                    }
                });
                $('#foo5').carouFredSel({
                    auto: false,
                    prev: '#prev5',
                    next: '#next5',
                    pagination: "#pager5",
                    mousewheel: true,
                    swipe: {
                        onMouse: false,
                        onTouch: true
                    }
                });
            });
        </script>
        <script type="text/javascript">
            $(function () {
                var conveyor = $(".content-conveyor", $("#sliderContent")),
		    item = $(".itemDocument", $("#sliderContent"));
                //set length of conveyor
                conveyor.css("width", (item.length * parseInt(item.css("width"))));
                var sliderOpts = {
                    max: (item.length * parseInt(item.css("width"))) - parseInt($(".viewer", $("#sliderContent")).css("width")),
                    slide: function (e, ui) {
                        conveyor.css("left", "-" + (ui.value) + "px");
                    }
                };
                $("#slider").slider(sliderOpts);
            });
            $(function () {
                var conveyor = $(".content-conveyor", $("#sliderContent1")),
		    item = $(".itemDocument", $("#sliderContent1"));
                //set length of conveyor
                conveyor.css("width", (item.length * parseInt(item.css("width"))));
                var sliderOpts = {
                    max: (item.length * parseInt(item.css("width"))) - parseInt($(".viewer", $("#sliderContent1")).css("width")),
                    slide: function (e, ui) {
                        conveyor.css("left", "-" + (ui.value) + "px");
                    }
                };
                $("#slider1").slider(sliderOpts);
            });
        </script>
        <script type="text/javascript">
            function docdelete() {
                $('#divDeletesucess').css("display", "block");
            }
            function divCancels() {
                $('#divDeletesucess').css("display", "none");
            }
        </script>
</asp:Content>
